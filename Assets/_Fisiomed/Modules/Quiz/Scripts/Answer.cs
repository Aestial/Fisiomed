using System.Xml;
using System.Xml.Serialization;

namespace Fisiomed.Questions_OLD
{
	public class Answer
	{
		[XmlAttribute("text")]
		public string text;
		[XmlAttribute("value")]
		public bool value;
		[XmlAttribute("feedback")]
		public string feedback;
	}
}
