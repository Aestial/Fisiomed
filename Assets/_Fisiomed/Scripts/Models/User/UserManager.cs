using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserManager : MonoBehaviour 
{
	[SerializeField] TMP_Text Username;
	[SerializeField] TMP_Text UserRange;
	
	void Start () 
	{
		Username.text = PlayerPrefs.GetString("Username");
		UserRange.text = PlayerPrefs.GetString("UserRange");
	}
	
	void Update () 
	{
		
	}
}
