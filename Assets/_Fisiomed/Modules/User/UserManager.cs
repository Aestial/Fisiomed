using UnityEngine;
using TMPro;

public class UserManager : MonoBehaviour 
{
	[SerializeField] TMP_Text userName;
	//[SerializeField] TMP_Text userRange;
	
	void Start () 
	{
		string username = PlayerPrefs.GetString("Username");
		//string range =  PlayerPrefs.GetString("UserRange");

		userName.text = string.IsNullOrEmpty(username) ? username : "Usuario";
		//userRange.text = string.IsNullOrEmpty(range) ? range : "Principiante";
	}
}
