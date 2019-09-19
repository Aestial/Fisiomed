using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Fisiomed.Questions_OLD
{
	[XmlRoot("question")]
	public class Question
	{
		[XmlAttribute("text")]
		public string text;
		[XmlAttribute("hasFeedback")]
		public bool hasFeedback;
		[XmlArray("answers"), XmlArrayItem("answer")]
		public Answer[] answers;

		public void Save(string path)
		{
			var serializer = new XmlSerializer(typeof(Question));
			using(var stream = new FileStream(path, FileMode.Create))
			{
				serializer.Serialize(stream, this);
			}
		}
		
		public static Question Load(string path)
		{
			var serializer = new XmlSerializer(typeof(Question));
			using(var stream = new FileStream(path, FileMode.Open))
			{
				return serializer.Deserialize(stream) as Question;
			}
		}

		public static Question LoadFromText(string text)
		{
			var serializer = new XmlSerializer(typeof(Question));
			return serializer.Deserialize(new StringReader(text)) as Question;
		}
	}
}
