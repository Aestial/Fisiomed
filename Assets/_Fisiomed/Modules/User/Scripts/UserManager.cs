using System.IO;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

namespace Fisiomed.User
{
    using FirebaseServices;
    using Popup;
    using System;

    public class UserManager : Singleton<UserManager>
    {
        [SerializeField] string filename = default;
        private string filePath;
        private readonly StringStringDictionary temp = new StringStringDictionary();
        private FirebaseDatabase _database;
        private FirebaseUser _firebaseUser;

        public bool isSignUp = false;

        public FirebaseUser FirebaseUser
        {
            get
            {
                if (_firebaseUser == null)
                {
                    return FirebaseManager.Instance.User;
                }
                return _firebaseUser;
            }
            set
            {
                _firebaseUser = value;
                _database = FirebaseManager.Instance.Database;
                Log.Color(_database.ToString(), this);
                if (isSignUp)
                {
                    PrintUser(_firebaseUser);
                }
                else
                {
                    RetrieveFromDB(_firebaseUser.UserId);
                    PrintUser(_firebaseUser);
                }
                
            }
        }
        [SerializeField] UserData _userData = new UserData();
        public UserData UserData
        {
            get
            {
                if (_userData == null)
                {
                    _userData = new UserData();                    
                }
                return _userData;
            }
            set
            {
                _userData = value;
            }
        }


        public void SaveUserProgress(int index, string progress)
        {
            if (UserData.progress.ContainsKey(index))
                UserData.progress[index] = progress;
            else
                UserData.progress.Add(index, progress);

            string json = JsonUtility.ToJson(UserData);
            SaveJSONLocal(json);
            SaveOnDB(json);
        }

        private async void SaveOnDB(string json)
        {
            // Save user in Database
            try
            {
                await _database.GetReference("users").Child(_firebaseUser.UserId).SetRawJsonValueAsync(json);
                Debug.LogFormat("User data saved (DB): {0} ({1})", _firebaseUser.DisplayName, _firebaseUser.UserId);
                PopupManager.Instance.PrintMessage("User saved in DB: " + FirebaseManager.Instance.User.DisplayName);
            }
            catch (AggregateException ex)
            {
                Log.ColorError("SetRawJsonValueAsync encountered an error: " + ex.Message + " Data: " + ex.Data.Values, this);
                PopupManager.Instance.PrintMessage("Saving in DB user error: " + ex.Message);
            }
        }

        public void SetUserProperty(string value, string key)
        {
            if (UserData.properties.ContainsKey(key))
                UserData.properties[key] = value;
            else
                UserData.properties.Add(key, value);       
        }
        public void SetUserLoginData(string value, string key)
        {
            if (temp.ContainsKey(key))
                temp[key] = value;
            else
                temp.Add(key, value);
        }        
        public async void SignUp()
        {
            string email = UserData.properties["email"];
            string password = UserData.properties["password"];
            string json = JsonUtility.ToJson(UserData);

            Log.Color("Signing Up User: " + email, this);

            isSignUp = true;
            SaveJSONLocal(json);           

            // Create Auth user
            try
            {
                var createdResult = await FirebaseManager.Instance.Auth.CreateUserWithEmailAndPasswordAsync(email, password);
                _firebaseUser = FirebaseManager.Instance.User = createdResult;
                Debug.LogFormat("Firebase user created successfully: {0} ({1},{2}) ",
                            FirebaseManager.Instance.User.DisplayName,
                            FirebaseManager.Instance.User.UserId,
                            FirebaseManager.Instance.User.Email);
                PopupManager.Instance.PrintMessage("Firebase user created successfully: " + FirebaseManager.Instance.User.Email);

                UserProfile profile = new UserProfile();
                // TODO: Change for Username
                profile.DisplayName = UserData.properties["name"];
                // TODO: Set Photo URL
                // profile.PhotoUrl = _user.properties["photoUrl"];     

                // Update user Auth Profile
                try
                {
                    await FirebaseManager.Instance.User.UpdateUserProfileAsync(profile);
                    Debug.LogFormat("User data updated: {0} ({1})", _firebaseUser.DisplayName, _firebaseUser.UserId);
                    PopupManager.Instance.PrintMessage("User saved: " + FirebaseManager.Instance.User.DisplayName);
                }
                catch (AggregateException ex)
                {
                    Log.ColorError("UpdateUserProfileAsync encountered an error: " + ex.Message + " Data: " + ex.Data.Values, this);
                    PopupManager.Instance.PrintMessage("Updating user error: " + ex.Message);
                }                               
                // Save user in Database
                try
                {
                    await _database.GetReference("users").Child(_firebaseUser.UserId).SetRawJsonValueAsync(json);
                    Debug.LogFormat("User data saved (DB): {0} ({1})", _firebaseUser.DisplayName, _firebaseUser.UserId);
                    PopupManager.Instance.PrintMessage("User saved in DB: " + FirebaseManager.Instance.User.DisplayName);
                }
                catch (AggregateException ex)
                {
                    Log.ColorError("SetRawJsonValueAsync encountered an error: " + ex.Message + " Data: " + ex.Data.Values, this);
                    PopupManager.Instance.PrintMessage("Saving in DB user error: " + ex.Message);
                }
            }
            catch (AggregateException ex)
            {
                // The exception will be caught because you've awaited the call in an async method.
                Log.ColorError("CreateUserWithEmailAndPasswordAsync encountered an error: " + ex.Message + " Data: " + ex.Data.Values, this);
                PopupManager.Instance.PrintMessage("Creating user error: " + ex.Message);
            }

            isSignUp = false;
        }
        public async void LogIn()
        {
            string email = temp["email"];
            string password = temp["password"];
            Log.Color("Logging In User: " + email, this);

            try
            {
                var loginResult = await FirebaseManager.Instance.Auth.SignInWithEmailAndPasswordAsync(email, password);
                _firebaseUser = FirebaseManager.Instance.User = loginResult;
                Debug.LogFormat("Firebase user retrieved successfully: {0} ({1},{2}) ",
                            FirebaseManager.Instance.User.DisplayName,
                            FirebaseManager.Instance.User.UserId,
                            FirebaseManager.Instance.User.Email);
                PopupManager.Instance.PrintMessage("Firebase user logged successfully: " + FirebaseManager.Instance.User.Email);
                RetrieveFromDB(_firebaseUser.UserId);                                
            }
            catch (AggregateException ex)
            {
                // The exception will be caught because you've awaited the call in an async method.
                Log.ColorError("SignInWithEmailAndPasswordAsync encountered an error: " + ex.Message + " Data: " + ex.Data.Values, this);
                PopupManager.Instance.PrintMessage("Logging error: " + ex.Message);
            }
        }

        private async void RetrieveFromDB(string userId)
        {
            try
            {
                DataSnapshot dss = await _database.GetReference("users").Child(_firebaseUser.UserId).GetValueAsync();
                Log.Color(dss.ToString(), this);
                string json = dss.GetRawJsonValue();
                Log.Color(json, this);
                UserData = GetFromJson(json);
                SaveJSONLocal(json);
            }
            catch (AggregateException ex)
            {
                Log.ColorError("GetRawJsonValue encountered an error: " + ex.Message + " Data: " + ex.Data.Values, this);
                PopupManager.Instance.PrintMessage("Getting user from DB error: " + ex.Message);
            }
        }

        public void SignOut()
        {
            Log.Color("Signing out user: " + FirebaseUser.DisplayName, this);
            PopupManager.Instance.PrintMessage("Signing out: " + FirebaseUser.DisplayName);
            FirebaseManager.Instance.Auth.SignOut();
        }

        void Awake()
        {
            string path = Application.persistentDataPath;
            filePath = Path.Combine(path, filename);
            if (File.Exists(filePath))
            {
                Log.Color("User file exists in:" + filePath, this);
                UserData = GetFromJson(File.ReadAllText(filePath));
            }
            //else
            //{
            //    UserDataAux = new UserData();
            //}
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SignOut();
            }
        }
        private UserData GetFromJson(string json)
        {
            Log.Color("Getting User from JSON: " + json, this);
            UserData user = JsonUtility.FromJson<UserData>(json);
            return user;
        }
        private void SaveJSONLocal(string json)
        {
            Log.Color("Saving JSON locally: " + json, this);
            File.WriteAllText(filePath, json);
        }
        private void PrintUser(FirebaseUser user)
        {
            Log.Color("Current User: " + user.DisplayName, this);
            PopupManager.Instance.PrintMessage("Current: " + user.DisplayName);
        }
    }
}
