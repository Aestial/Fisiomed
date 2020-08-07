using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Fisiomed.User
{
    public class UserDisplay : MonoBehaviour
    {
        [SerializeField] string format = "{0}";
        [SerializeField] string[] keys = { "name" };
        [SerializeField] TMP_Text text = default;
        List<string> values = new List<string>();
        string content;

        void Start()
        {
            try
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    values.Add(UserManager.Instance.UserData.properties[keys[i]]);
                }
                content = string.Format(format, values.ToArray());
                text.text = content;
            }
            catch (KeyNotFoundException e)
            {
                Log.Color("Keys not found. " + e.Message, this);
            }
        }
    }
}