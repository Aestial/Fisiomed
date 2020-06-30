using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Fisiomed.User
{
    public class UserDisplay : MonoBehaviour
    {
        [SerializeField] new TMP_Text name = default;

        void Start()
        {
            name.text = UserManager.Instance.User.properties["name"];
        }
    }
}
