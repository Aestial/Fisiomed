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

    public class UserManager : Singleton<UserManager>
    {
        [SerializeField] string filename;

        private string filePath;
        private readonly StringStringDictionary temp;

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
        
        public void SignUpUser()
        {
            string email = User.properties["email"];
            string password = User.properties["password"];
            Log.Color("Signing Up User: " + email, this);
            FirebaseManager.Instance.Auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Log.ColorError("CreateUserWithEmailAndPasswordAsync was canceled.", this);
                    return;
                }
                if (task.IsFaulted)
                {
                    Log.ColorError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception, this);
                    return;
                }
                // Firebase user has been created.
                //Firebase.Auth.FirebaseUser user = FirebaseManager.Instance.User;
                FirebaseManager.Instance.User = task.Result;

                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                        FirebaseManager.Instance.User.DisplayName,
                        FirebaseManager.Instance.User.UserId);
                PopupManager.Instance.PrintMessage("Firebase user created successfully: " +
                        FirebaseManager.Instance.User.UserId);

                UserProfile profile = new UserProfile();
                profile.DisplayName = User.properties["name"]; // TODO: Change for Username
                //profile.PhotoUrl = ; // TODO: Set Photo URL

                //FirebaseManager.Instance.User.UpdateUserProfileAsync(profile).ContinueWith(profileTask =>
                //{
                //    if (profileTask.IsCanceled)
                //    {
                //        Log.ColorError("UpdateUserProfileAsync was canceled.", this);
                //        return;
                //    }
                //    if (profileTask.IsFaulted)
                //    {
                //        Log.ColorError("UpdateUserProfileAsync encountered an error: " + profileTask.Exception, this);
                //        return;
                //    }
                //    Save(User);

                    
                //});
            });
        }

        public void LogInUser()
        {
            string email = temp["email"];
            string password = temp["password"];
            Log.Color("Log In User: " + email, this);
            FirebaseManager.Instance.Auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(signTask => {
                if (signTask.IsCanceled)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (signTask.IsFaulted)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + signTask.Exception);
                    return;
                }
                FirebaseManager.Instance.User = signTask.Result;

                _firebaseUser = FirebaseManager.Instance.User;
                
                _database.GetReference(_firebaseUser.UserId).GetValueAsync().ContinueWith(dataTask =>{
                    if (dataTask.IsFaulted)
                    {
                        // Handle the error...
                    }
                    else if (dataTask.IsCompleted)
                    {
                        DataSnapshot dss = dataTask.Result;
                        // Do something with snapshot...

                        OnDataLoaded(dss.GetRawJsonValue());
                        Save(User);

                        Debug.LogFormat("User signed in successfully: {0} ({1})",
                            _firebaseUser.DisplayName,_firebaseUser.UserId);
                        PopupManager.Instance.PrintMessage("User created: " + FirebaseManager.Instance.User.UserId);
                        //AppManager.Instance.ChangeSceneDelayBefore("MainMenuScene");
                    }
                });
            });
        }

        public void SignOutUser()
        {
            Log.Color("Signing out user: " + _firebaseUser.DisplayName, this);
            PopupManager.Instance.PrintMessage("Signing out: " + _firebaseUser.DisplayName);
            FirebaseManager.Instance.Auth.SignOut();
        }

        void Awake()
        {
            //StartCoroutine(Downloader.Instance.LoadJSON(filePath, OnDataLoaded));

            string path = Application.persistentDataPath;
            filePath = Path.Combine(path, filename);

            if (File.Exists(filePath))
            {
                Log.Color("File path:" + filePath, this);
                OnDataLoaded(File.ReadAllText(filePath));
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

        private void OnDataLoaded(string json)
        {
            Log.Color(json, this);
            User = JsonUtility.FromJson<User>(json);
            Log.Color(User.ToString(), this);
        }

        private void Save(User u)
        {
            string json = JsonUtility.ToJson(u);
            SaveJSONLocal(json);
            SaveJSONDatabase(json);
        }

        private void SaveJSONLocal(string json)
        {
            Log.Color("Saving JSON Local: " + json, this);
            File.WriteAllText(filePath, json);
        }        

        private void SaveJSONDatabase(string json)
        {
            Log.Color("Saving JSON Database: " + json, this);
            _database.GetReference(_firebaseUser.UserId).SetRawJsonValueAsync(json)
            .ContinueWith(dbtask =>
            {
                AppManager.Instance.ChangeScene("MainMenuScene");
            });
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
