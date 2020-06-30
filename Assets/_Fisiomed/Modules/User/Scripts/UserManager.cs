using System.IO;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fisiomed.User
{
    using App;
    using FirebaseServices;
    using Popup;
    using System;

    public class UserManager : Singleton<UserManager>
    {
        [SerializeField] string filename;

        private string filePath;
        private readonly StringStringDictionary temp = new StringStringDictionary();

        private FirebaseDatabase _database;
        private FirebaseUser _firebaseUser;
        public FirebaseUser FirebaseUser
        {
            get
            {
                return _firebaseUser;
            }
            set
            {
                _firebaseUser = value;
                PrintUser(_firebaseUser);
            }
        }       

        public User User { get; set; } = new User();

        public void SetUserProperty(string value, string key)
        {
            if (User.properties.ContainsKey(key))
                User.properties[key] = value;
            else
                User.properties.Add(key, value);       
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
            string email = User.properties["email"];
            string password = User.properties["password"];
            Log.Color("Signing Up User: " + email, this);
            try
            {
                var createdResult = await FirebaseManager.Instance.Auth.CreateUserWithEmailAndPasswordAsync(email, password);                
                FirebaseManager.Instance.User = createdResult;
                _firebaseUser = FirebaseManager.Instance.User;
                Debug.LogFormat("Firebase user created successfully: {0} ({1},{2}) ",
                            FirebaseManager.Instance.User.DisplayName,
                            FirebaseManager.Instance.User.UserId,
                            FirebaseManager.Instance.User.Email);
                PopupManager.Instance.PrintMessage("Firebase user created successfully: " +
                        FirebaseManager.Instance.User.Email);

                UserProfile profile = new UserProfile();
                // TODO: Change for Username
                profile.DisplayName = User.properties["name"];
                // TODO: Set Photo URL
                //string url;
                //profile.PhotoUrl = url; 
                await FirebaseManager.Instance.User.UpdateUserProfileAsync(profile);

                string json = JsonUtility.ToJson(User);
                await _database.GetReference(_firebaseUser.UserId).SetRawJsonValueAsync(json);
                SaveJSONLocal(json);

                Debug.LogFormat("User data saved: {0} ({1})",
                    _firebaseUser.DisplayName, _firebaseUser.UserId);
                PopupManager.Instance.PrintMessage("User saved: " + FirebaseManager.Instance.User.DisplayName);
            }
            catch (AggregateException ex)
            {
                // The exception will be caught because you've awaited the call in an async method.
                Log.ColorError("CreateUserWithEmailAndPasswordAsync encountered an error: " + ex.Message + " Data: " + ex.Data.Values, this);
                PopupManager.Instance.PrintMessage("Creating error: " + ex.Message);
            }           
        }

        public async void LogIn()
        {
            string email = temp["email"];
            string password = temp["password"];
            Log.Color("Logging In User: " + email, this);

            try
            {
                var loginResult = await FirebaseManager.Instance.Auth.SignInWithEmailAndPasswordAsync(email, password);
                FirebaseManager.Instance.User = loginResult;
                _firebaseUser = FirebaseManager.Instance.User;
                Debug.LogFormat("Firebase user retrieved successfully: {0} ({1},{2}) ",
                            FirebaseManager.Instance.User.DisplayName,
                            FirebaseManager.Instance.User.UserId,
                            FirebaseManager.Instance.User.Email);
                PopupManager.Instance.PrintMessage("Firebase user logged successfully: " +
                        FirebaseManager.Instance.User.Email);

                DataSnapshot dss = await _database.GetReference(_firebaseUser.UserId).GetValueAsync();
                string json = dss.GetRawJsonValue();
                User = GetFromJson(json);
                SaveJSONLocal(json);

                Debug.LogFormat("User logged in successfully: {0} ({1})",
                    _firebaseUser.DisplayName, _firebaseUser.UserId);
                PopupManager.Instance.PrintMessage("User logged: " + FirebaseManager.Instance.User.UserId);                
            }
            catch (AggregateException ex)
            {
                // The exception will be caught because you've awaited the call in an async method.
                Log.ColorError("SignInWithEmailAndPasswordAsync encountered an error: " + ex.Message + " Data: " + ex.Data.Values, this);
                PopupManager.Instance.PrintMessage("Logging error: " + ex.Message);
            }
        }

        public void SignOut()
        {
            Log.Color("Signing out user: " + _firebaseUser.DisplayName, this);
            PopupManager.Instance.PrintMessage("Signing out: " + _firebaseUser.DisplayName);
            FirebaseManager.Instance.Auth.SignOut();
        }

        void Awake()
        {
            string path = Application.persistentDataPath;
            filePath = Path.Combine(path, filename);

            if (File.Exists(filePath))
            {
                Log.Color("File path:" + filePath, this);
                User = GetFromJson(File.ReadAllText(filePath));
            }
            else
            {
                User = new User();
            }
        }
        private void Start()
        {
            _database = FirebaseDatabase.DefaultInstance;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SignOut();
            }
        }
        private User GetFromJson(string json)
        {
            Log.Color(json, this);
            User user = JsonUtility.FromJson<User>(json);
            Log.Color(user.ToString(), this);
            return user;
        }
        private void SaveJSONLocal(string json)
        {
            Log.Color("Saving JSON Local: " + json, this);
            File.WriteAllText(filePath, json);
        }
        private void PrintUser(FirebaseUser user)
        {
            Log.Color("Current User: " + user.DisplayName, this);
            PopupManager.Instance.PrintMessage("Current: " + user.DisplayName);
        }

        //public async Task<bool> SaveJSONOnDatabase(string userData)
        //{
        //    FirebaseUser fUser = FirebaseManager.Instance.User;
        //    var dataSnapshot = await _database.GetReference(fUser.UserId).SetRawJsonValueAsync(userData);
        //    return dataSnapshot.Exists;
        //}
        //void OnEnable()
        //{
        //    SceneManager.sceneLoaded += OnSceneLoaded;
        //}
        //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        //{
        //    Log.Color("On Scene Loaded: " + scene.name, this);            
        //}
        //void OnDisable()
        //{
        //    SceneManager.sceneLoaded -= OnSceneLoaded;
        //}
    }
}
