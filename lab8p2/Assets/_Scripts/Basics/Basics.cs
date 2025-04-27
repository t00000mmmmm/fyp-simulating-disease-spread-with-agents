using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

namespace Basics
{
public class Basics : MonoBehaviour 
{

	[SerializeField]
	private GameObject cube;

	private const string SAVEGAME_FILE = "Assets/Saves/basics-savegame.xml";

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			Save(SAVEGAME_FILE);
			print("saved.");
		}
		else if (Input.GetKeyDown(KeyCode.F2))
		{
			Load(SAVEGAME_FILE);
			print("loaded.");
		}
	}

	private void Save(string filename)
	{
		XmlDocument xmlDocument = new XmlDocument();
		CubeState state = new CubeState(transform.position, cube.transform.rotation);
		XmlSerializer serializer = new XmlSerializer(typeof(CubeState));
		using (MemoryStream stream = new MemoryStream())
		{
			serializer.Serialize(stream, state);
			stream.Position = 0;
			xmlDocument.Load(stream);
			xmlDocument.Save(filename);
		}
	}

	private void Load(string filename)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(filename);
		string xmlString = xmlDocument.OuterXml;
		
		CubeState state;
		using (StringReader read = new StringReader(xmlString))
		{
			XmlSerializer serializer = new XmlSerializer(typeof(CubeState));
			using (XmlReader reader  = new XmlTextReader(read))
			{
				state = (CubeState) serializer.Deserialize(reader);
			}
		}
		
		cube.transform.position = state.position;
		transform.rotation = state.rotation;
	}
}

[Serializable]
public struct CubeState
{
	public Vector3 position;
	public Quaternion rotation;

	public CubeState (Vector3 position, Quaternion rotation)
	{
		this.position = position;
		this.rotation = rotation;
	}
}
}