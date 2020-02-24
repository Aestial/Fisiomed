using System.IO;
using UnityEngine;

namespace Fisiomed.User
{
    public class UserManager : Singleton<UserManager>
    {
        [SerializeField] string filename;
        public User user = new User();
        string filePath;
        void Start()
        {
            string path = Application.persistentDataPath;
            //string path = Path.Combine(Application.persistentDataPath, "user");
            //if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            filePath = Path.Combine(path, filename);
        }
        void Save(User u)
        {
            string json = JsonUtility.ToJson(u);
            Log.Color(json, this);
            File.WriteAllText(filePath, json);
        }
        public void Save()
        {
            Save(user);
        }
    }
}
