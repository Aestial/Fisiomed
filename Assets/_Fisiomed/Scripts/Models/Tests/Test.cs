using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("test")]
public class Test
{
	[XmlAttribute("title")]
	public string title;
	[XmlArray("questions"), XmlArrayItem("question")]
	public Question[] questions;

	public void Save(string path)
 	{
 		var serializer = new XmlSerializer(typeof(Test));
 		using(var stream = new FileStream(path, FileMode.Create))
 		{
 			serializer.Serialize(stream, this);
 		}
 	}
	
	public static Test Load(string path)
	{
		var serializer = new XmlSerializer(typeof(Test));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as Test;
		}
	}

	public static Test LoadFromText(string text)
	{
		var serializer = new XmlSerializer(typeof(Test));
		return serializer.Deserialize(new StringReader(text)) as Test;
	}
}