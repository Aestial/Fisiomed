using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot ("DialogueCollection")]
public class ChatContainer
{
	[XmlArray("Dialogues")]
	[XmlArrayItem("Dialogue")]
	public List<ChatDialogues> chatDialogues = new List<ChatDialogues> ();

	public static ChatContainer Load(string path)
	{
		TextAsset _xml = Resources.Load<TextAsset> (path);
		XmlSerializer serializer = new XmlSerializer (typeof(ChatContainer));
		StringReader reader = new StringReader (_xml.text);
		ChatContainer items = serializer.Deserialize (reader) as ChatContainer;
		reader.Close ();
		return items;
	}
}
