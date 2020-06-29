using System;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;
using UnityEngine.Events;


namespace Fisiomed.FirebaseServices
{
    /// <summary>
    /// This should be added to a loading scene. It will initialize Firebase then indicate with an event when
    /// initialization is complete, storing itself into a persisting singleton instance. I've added convenience methods to
    /// support Editor specific use cases (lazily initializing instances in editor as needed), but these are designed to
    /// fail on device.
    /// </summary>
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
            {                if (_database == null)
                {
                    _database = FirebaseDatabase.DefaultInstance;
                }                return _database;            }
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

        public UnityEvent OnFirebaseInitialized = new UnityEvent();

        private async void Awake()
        {
            if (_instance == null)
            {
                DontDestroyOnLoad(gameObject);
                _instance = this;

                var dependencyResult = await FirebaseApp.CheckAndFixDependenciesAsync();
                if (dependencyResult == DependencyStatus.Available)
                {
                    _app = FirebaseApp.DefaultInstance;
                    OnFirebaseInitialized.Invoke();
                    Log.Color($"Initialized successful.", this);
                }
                else
                {
                    Log.Color($"Failed to Initialize Firebase .Could not resolve all Firebase dependencies: {dependencyResult}", this);
                }
            }
        }

        private void OnDestroy()
        {
            _auth = null;
            _app = null;
            if (_instance == this)
            {
                _instance = null;
            }
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
