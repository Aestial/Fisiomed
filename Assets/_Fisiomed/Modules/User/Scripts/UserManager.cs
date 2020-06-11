using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Loader;

namespace Fisiomed.User
{
    public class UserManager : Singleton<UserManager>
    {
        [SerializeField] string filename;
        public User User;
        string filePath;
        void Awake()
        {            
            //string path = Path.Combine(Application.persistentDataPath, "user");
            //if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string path = Application.persistentDataPath;
            filePath = Path.Combine(path, filename);
            if (File.Exists(filePath))
            {
                Log.Color("File path:" + filePath, this);
                StartCoroutine(Downloader.Instance.LoadJSON(filePath, OnDataLoaded));
            }
            else
            {
                User = new User();
            }                
        }
        //void OnEnable()
        //{
        //    SceneManager.sceneLoaded += OnSceneLoaded;
        //}
        //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        //{
        //    Log.Color("On Scene Loaded: " + scene.name, this);
        //    StartCoroutine(Downloader.Instance.LoadJSON(filePath, OnDataLoaded));
        //}
        //void OnDisable()
        //{
        //    SceneManager.sceneLoaded -= OnSceneLoaded;
        //}
        private void OnDataLoaded(string json)
        {
            User = JsonUtility.FromJson<User>(json);
        }
        void Save(User u)
        {
            string json = JsonUtility.ToJson(u);
            Log.Color(json, this);
            File.WriteAllText(filePath, json);
        }        
        public void Save()
        {
            Save(User);
        }
    }
}
