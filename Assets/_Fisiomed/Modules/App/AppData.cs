using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppData : MonoBehaviour
{
    public void PassUrl(string value)
    {
        PlayerPrefs.SetString("url", value);
    }   
}
