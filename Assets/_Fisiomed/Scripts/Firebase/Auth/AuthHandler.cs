using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AuthHandler : MonoBehaviour 
{
	protected Firebase.Auth.FirebaseAuth auth;
  	protected Firebase.Auth.FirebaseAuth otherAuth;
  	protected Dictionary<string, Firebase.Auth.FirebaseUser> userByAuth =
	  new Dictionary<string, Firebase.Auth.FirebaseUser>();
	
	// protected string email = "";
	// protected string password = "";
	// protected string displayName = "";

	private UserController uc;

	// Whether to sign in / link or reauthentication *and* fetch user profile data.
	protected bool signInAndFetchProfile = false;
	// Flag set when a token is being fetched.  This is used to avoid printing the token
	// in IdTokenChanged() when the user presses the get token button.
	private bool fetchingToken = false;

	// Options used to setup secondary authentication object.
	private Firebase.AppOptions otherAuthOptions = new Firebase.AppOptions {
		ApiKey = "",
		AppId = "",
		ProjectId = ""
	};

	Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;

	// When the app starts, check to make sure that we have
	// the required dependencies to use Firebase, and if not,
	// add them if possible.
	public virtual void Start() {
		Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
			dependencyStatus = task.Result;
			if (dependencyStatus == Firebase.DependencyStatus.Available) {
				InitializeFirebase();
			} else {
				Debug.LogError(
				"Could not resolve all Firebase dependencies: " + dependencyStatus);
			}
		});
	}

	private void Awake() 
	{
		uc = GetComponent<UserController>();
	}

	// Handle initialization of the necessary firebase modules:
	protected void InitializeFirebase() {
		Debug.Log("Setting up Firebase Auth");
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		auth.StateChanged += AuthStateChanged;
		auth.IdTokenChanged += IdTokenChanged;
		// Specify valid options to construct a secondary authentication object.
		if (otherAuthOptions != null &&
			!(String.IsNullOrEmpty(otherAuthOptions.ApiKey) ||
			String.IsNullOrEmpty(otherAuthOptions.AppId) ||
			String.IsNullOrEmpty(otherAuthOptions.ProjectId))) {
		try {
			otherAuth = Firebase.Auth.FirebaseAuth.GetAuth(Firebase.FirebaseApp.Create(
			otherAuthOptions, "Secondary"));
			otherAuth.StateChanged += AuthStateChanged;
			otherAuth.IdTokenChanged += IdTokenChanged;
		} catch (Exception) {
			Debug.Log("ERROR: Failed to initialize secondary authentication object.");
		}
		}
		AuthStateChanged(this, null);
	}

	// Track state changes of the auth object.
	void AuthStateChanged(object sender, System.EventArgs eventArgs) {
		Firebase.Auth.FirebaseAuth senderAuth = sender as Firebase.Auth.FirebaseAuth;
		Firebase.Auth.FirebaseUser user = null;
		if (senderAuth != null) userByAuth.TryGetValue(senderAuth.App.Name, out user);
		if (senderAuth == auth && senderAuth.CurrentUser != user) {
		bool signedIn = user != senderAuth.CurrentUser && senderAuth.CurrentUser != null;
		if (!signedIn && user != null) {
			Debug.Log("Signed out " + user.UserId);
		}
		user = senderAuth.CurrentUser;
		userByAuth[senderAuth.App.Name] = user;
		if (signedIn) {
			Debug.Log("Signed in " + user.UserId);
			uc.myUser.name = user.DisplayName ?? "";
			// DisplayDetailedUserInfo(user, 1);
		}
		}
	}

	// Track ID token changes.
	void IdTokenChanged(object sender, System.EventArgs eventArgs) {
		Firebase.Auth.FirebaseAuth senderAuth = sender as Firebase.Auth.FirebaseAuth;
		if (senderAuth == auth && senderAuth.CurrentUser != null && !fetchingToken) {
		senderAuth.CurrentUser.TokenAsync(false).ContinueWith(
			task => Debug.Log(String.Format("Token[0:8] = {0}", task.Result.Substring(0, 8))));
		}
	}

	// Log the result of the specified task, returning true if the task
	// completed successfully, false otherwise.
	protected bool LogTaskCompletion(Task task, string operation) {
		bool complete = false;
		if (task.IsCanceled) {
		Debug.Log(operation + " canceled.");
		} else if (task.IsFaulted) {
		Debug.Log(operation + " encounted an error.");
		foreach (Exception exception in task.Exception.Flatten().InnerExceptions) {
			string authErrorCode = "";
			Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
			if (firebaseEx != null) {
			authErrorCode = String.Format("AuthError.{0}: ",
				((Firebase.Auth.AuthError)firebaseEx.ErrorCode).ToString());
			}
			Debug.Log(authErrorCode + exception.ToString());
			uc.DisplayMessage(exception.ToString().Replace("Firebase.FirebaseException: ",""));
		}
		} else if (task.IsCompleted) {
		Debug.Log(operation + " completed");
		complete = true;
		}
		return complete;
	}

	// Create a user with the email and password.
	public System.Threading.Tasks.Task CreateUserWithEmailAsync() {
		Debug.Log(String.Format("Attempting to create user {0}...", uc.myUser.email));
		// DisableUI();

		// This passes the current displayName through to HandleCreateUserAsync
		// so that it can be passed to UpdateUserProfile().  displayName will be
		// reset by AuthStateChanged() when the new user is created and signed in.
		string newDisplayName = uc.myUser.name;
		// Debug.Log("<color=cyan>"+uc.myUser.password+"</color>");
		return auth.CreateUserWithEmailAndPasswordAsync(uc.myUser.email, uc.myUser.password)
		.ContinueWith((task) => {
			// EnableUI();
			if (LogTaskCompletion(task, "User Creation")) {
				var user = task.Result;
				user.SendEmailVerificationAsync();
				uc.DisplayMessage("¡Correo de verificación enviado!");
				uc.ChangeScreenDelay(false);
				// DisplayDetailedUserInfo(user, 1);
				return UpdateUserProfileAsync(newDisplayName: newDisplayName);
			}
			return task;
		}).Unwrap();
	}

	// Update the user's display name with the currently selected display name.
	public Task UpdateUserProfileAsync(string newDisplayName = null) {
		if (auth.CurrentUser == null) {
			Debug.Log("Not signed in, unable to update user profile");
			return Task.FromResult(0);
		}
		uc.myUser.name = newDisplayName ?? uc.myUser.name;
		Debug.Log("Updating user profile");
		// DisableUI();
		return auth.CurrentUser.UpdateUserProfileAsync(new Firebase.Auth.UserProfile {
			DisplayName = uc.myUser.name,
			PhotoUrl = auth.CurrentUser.PhotoUrl,
		}).ContinueWith(task => {
			// EnableUI();
			if (LogTaskCompletion(task, "User profile")) {
			// DisplayDetailedUserInfo(auth.CurrentUser, 1);
			}
		});
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnDestroy() {
		auth.StateChanged -= AuthStateChanged;
		auth.IdTokenChanged -= IdTokenChanged;
		auth = null;
		if (otherAuth != null) {
		otherAuth.StateChanged -= AuthStateChanged;
		otherAuth.IdTokenChanged -= IdTokenChanged;
		otherAuth = null;
		}
	}
}
