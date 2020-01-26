using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Fisiomed.UI
{
    public class NumberDropdownPopulator : MonoBehaviour
    {
        [SerializeField] TMP_Dropdown dropdown;
        public int start;
        public int end;
        List<string> options;
        void Start()
        {
            options = new List<string>();
            for (int i = start; i <= end; i++)
            {
                string option = i.ToString();
                options.Add(option);
            }
            dropdown.AddOptions(options);
        }
    }
}
