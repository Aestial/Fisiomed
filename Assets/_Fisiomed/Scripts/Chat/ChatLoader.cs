using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatLoader : MonoBehaviour {

	public const string path = "Resources/Dialogue_XML/insomio_dialogue.xml";

	// Use this for initialization
	void Start () {
		ChatContainer cC = ChatContainer.Load (path);

		foreach (ChatDialogues items in cC.chatDialogues) {
			print (items.text);
		}
		
	}
}
