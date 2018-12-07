using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class ChatDialogues
{
	[XmlAttribute ("text")]
	public string text;
	[XmlAttribute ("value")]
	public int value;
}
