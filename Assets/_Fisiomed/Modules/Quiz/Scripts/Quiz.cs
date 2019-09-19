using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Fisiomed.Questions_OLD
{
	[XmlRoot("quiz")]
	public class Quiz
	{
		[XmlAttribute("title")]
		public string title;
		[XmlArray("questions"), XmlArrayItem("question")]
		public Question[] questions;

		public void Save(string path)
		{
			var serializer = new XmlSerializer(typeof(Quiz));
			using(var stream = new FileStream(path, FileMode.Create))
			{
				serializer.Serialize(stream, this);
			}
		}
		
		public static Quiz Load(string path)
		{
			var serializer = new XmlSerializer(typeof(Quiz));
			using(var stream = new FileStream(path, FileMode.Open))
			{
				return serializer.Deserialize(stream) as Quiz;
			}
		}

		public static Quiz LoadFromText(string text)
		{
			var serializer = new XmlSerializer(typeof(Quiz));
			return serializer.Deserialize(new StringReader(text)) as Quiz;
		}
	}
}
