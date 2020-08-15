using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;
using UnityEngine.Events;


namespace Fisiomed.FirebaseServices
{
    [System.Serializable]
    public class FirebaseUserEvent : UnityEvent<FirebaseUser>
    {

    }
    /// <summary>
    /// This should be added to a loading scene. It will initialize Firebase then indicate with an event when
    /// initialization is complete, storing itself into a persisting singleton instance. I've added convenience methods to
    /// support Editor specific use cases (lazily initializing instances in editor as needed), but these are designed to
    /// fail on device.
    /// </summary>
    /// 
    public class FirebaseManager : MonoBehaviour
    {
        private static FirebaseManager _instance;
        public static FirebaseManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = LazyInitFirebaseManager();
                }
                return _instance;
            }
        }

        private FirebaseApp _app;
        public FirebaseApp App
        {
            get
            {
                if (_app == null)
                {
                    _app = GetAppSynchronous();
                }
                return _app;
            }
        }

        private FirebaseAuth _auth;
        public FirebaseAuth Auth
        {
            get
            {
                if (_auth == null)
                {
                    _auth = FirebaseAuth.GetAuth(App);
                }
                return _auth;
            }
        }

        private FirebaseDatabase _database;
        public FirebaseDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    _database = FirebaseDatabase.DefaultInstance;
                }
                return _database;
            }
        }

        private FirebaseUser _user;
        public FirebaseUser User
        {
            get
            {
                if (_user == null)
                {
                    _user = _auth.CurrentUser;
                }
                return _user;
            }
            set
            {
                _user = value;
            }
        }

        // TODO: Change for non editor serializable:
        [HideInInspector] public UnityEvent OnFirebaseInitialized = new UnityEvent();
        [HideInInspector] public FirebaseUserEvent OnFirebaseUserSignedIn = new FirebaseUserEvent();
        [HideInInspector] public UnityEvent OnFirebaseUserSignedOut = new UnityEvent();
        [HideInInspector] public UnityEvent OnFirebaseUserNull = new UnityEvent();

        public bool iAmFirst;

        private async void Awake()
        {
            FirebaseManager[] managers = FindObjectsOfType(typeof(FirebaseManager)) as FirebaseManager[];
            if (managers.Length > 1)
            {
                for (int i = 0; i < managers.Length; i++)
                {
                    if (!managers[i].iAmFirst)
                    {
                        DestroyImmediate(managers[i].gameObject);
                    }
                }
            }
            else
            {
                iAmFirst = true;
                if (_instance == null)
                {
                    DontDestroyOnLoad(gameObject);
                    _instance = this;
                    await InitializeFirebase();
                }
            }
        }

        private async Task InitializeFirebase()
        {
            Log.Color("Initializing Firebase...", this);
            var dependencyResult = await FirebaseApp.CheckAndFixDependenciesAsync();
            if (dependencyResult == DependencyStatus.Available)
            {
                _app = FirebaseApp.DefaultInstance;
                OnFirebaseInitialized.Invoke();
                InitializeAuth();
                PrintMessage($"Firebase initialized successfully.");                
            }
            else
            {
                Log.Color($"Failed to Initialize Firebase .Could not resolve all Firebase dependencies: {dependencyResult}", this);
            }
        }

        private void InitializeAuth()
        {
            Log.Color("Setting up Firebase Auth...", this);
            _auth = FirebaseAuth.GetAuth(App);
            _auth.StateChanged += AuthStateChanged;
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _auth.StateChanged -= AuthStateChanged;
                _instance = null;
                _auth = null;
                _database = null;
                _app = null;
                _user = null;
            }           
        }

        // Track state changes of the auth object.
        public void AuthStateChanged(object sender, EventArgs eventArgs)
        {
            Log.Color("Firebase Auth state changed! " + eventArgs, this);
            try
            {
                if (_auth.CurrentUser != _user)
                {
                    bool signedIn = _user != _auth.CurrentUser && _auth.CurrentUser != null;
                    if (!signedIn && _user != null)
                    {
                        PrintMessage("Signed Out" + _user.UserId);
                        OnFirebaseUserSignedOut.Invoke();
                    }
                    _user = _auth.CurrentUser;
                    if (signedIn)
                    {
                        PrintMessage("Signed in " + _user.DisplayName + "(" + _user.UserId + ")");
                        OnFirebaseUserSignedIn.Invoke(_user);
                    }
                }
                if (_user == null)
                {
                    PrintMessage("User doesn't exist.");
                    OnFirebaseUserNull.Invoke();
                }
            }
            catch
            {

            }
        }

        private void PrintMessage(string message)
        {
            Log.Color(message, this);
            Popup.PopupManager.Instance.PrintMessage(message);
        }

        private static FirebaseManager LazyInitFirebaseManager()
        {
            Debug.LogWarning("Firebase Manager is being initialized Lazily. This is highly discouraged.");

            if (Application.isPlaying)
            {
                var go = new GameObject($"{nameof(FirebaseManager)}");
                go.hideFlags = HideFlags.HideAndDontSave;
                return go.AddComponent<FirebaseManager>();
            }

            Debug.LogError($"{nameof(FirebaseManager)} requested before initialization");
            return null;
        }

        private FirebaseApp GetAppSynchronous()
        {
            Debug.LogWarning("You are getting the FirebaseApp synchronously. You cannot resolve dependencies this way");
            if (FirebaseApp.CheckDependencies() != DependencyStatus.Available)
            {
                throw new Exception($"Firebase not available with {FirebaseApp.CheckDependencies()}");
            }

            return FirebaseApp.DefaultInstance;
        }
    }
}
