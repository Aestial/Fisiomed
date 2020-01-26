using UnityEngine;
using TMPro;

namespace Fisiomed.User
{
    public class UserManager : Singleton<UserManager>
    {
        public User user = new User();
        public void Save()
        {
            Debug.Log(JsonUtility.ToJson(user));
        }
    }
}
