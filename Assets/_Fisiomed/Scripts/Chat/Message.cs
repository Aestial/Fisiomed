using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;

public class Message
{
	//	Lleva como nombre Message ("Dialogue")
//	public RawImage imagePerfil;
//	public Text nombreUsuario;
//	public GameObject cuadroDialogo;
	[XmlAttribute("text")]
	public string dialogue_text;
	[XmlAttribute("value")]
	public int value_int;
}
