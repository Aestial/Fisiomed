using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberDropdownPopulator : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] int start;
    [SerializeField] int end;
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
