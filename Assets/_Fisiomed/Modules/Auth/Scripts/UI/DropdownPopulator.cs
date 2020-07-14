using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Fisiomed.UI
{
    public class DropdownPopulator : MonoBehaviour
    {
        [SerializeField] TMP_Dropdown dropdown = default;
        public int startValue;
        public int endValue;
        List<string> options;
        void Start()
        {
            options = new List<string>();
            for (int i = startValue; i <= endValue; i++)
            {
                string option = i.ToString();
                options.Add(option);
            }
            dropdown.AddOptions(options);
        }
    }
}
