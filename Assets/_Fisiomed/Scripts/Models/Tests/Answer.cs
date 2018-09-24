using System.Xml;
using System.Xml.Serialization;

public class Answer
{
	[XmlAttribute("text")]
	public string text;
	[XmlAttribute("value")]
	public bool value;
}