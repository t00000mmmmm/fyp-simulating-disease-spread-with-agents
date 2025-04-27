using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Triangle
{
public class SavegameController : MonoBehaviour 
{
	private const string SAVEGAME_FILE = "Assets/Saves/triangle-savegame.xml";

    void Awake ()
    {
    }

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

    private GameState ToRecord ()
    {
        CubeController[] cubes  = FindObjectsOfType<CubeController>();
        CubeState[]      states = new CubeState[cubes.Length];
        for (int i = 0; i < cubes.Length; i++)
        {
            states[i] = cubes[i].ToRecord();
        }
        return new GameState(states);
    }

    private void FromRecord (GameState state)
    {
        ClearScene();
        Dictionary<string, GameObject> objects = CreateObjects(state);
        Dictionary<string, string>     links   = CreateLinks(state);
        LinkObjects(objects, links);
    }

    private void ClearScene ()
    {
        CubeController[] cubes = FindObjectsOfType<CubeController>();   
        for (int i = 0; i < cubes.Length; i++)
            GameObject.Destroy(cubes[i].gameObject);
    }

    private Dictionary<string, GameObject> CreateObjects(GameState state)
    {
        Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>(50);
        for (int i = 0; i < state.cubeStates.Length; i++)
        {
            GameObject obj = CubeController.FromRecord(state.cubeStates[i]);
            objects.Add(obj.name, obj);
        }
        return objects;
    }

    private Dictionary<string, string> CreateLinks(GameState state)
    {
        Dictionary<string, string> links = new Dictionary<string, string>(50);
        for (int i = 0; i < state.cubeStates.Length; i++)
        {
            links.Add(state.cubeStates[i].name, state.cubeStates[i].targetName);
        }
        return links;
    }

    private void LinkObjects(Dictionary<string, GameObject> objects, Dictionary<string, string> links)
    {
        foreach (GameObject obj in objects.Values)
        {
            CubeController cont = obj.GetComponent<CubeController>();
            cont.Link(objects, links);
        }
    }

	private void Save(string filename)
	{
        GameState state = ToRecord();

		XmlDocument xmlDocument    = new XmlDocument();
		XmlSerializer serializer   = new XmlSerializer(typeof(GameState));
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
		
		GameState state;
		using (StringReader read = new StringReader(xmlString))
		{
			XmlSerializer serializer = new XmlSerializer(typeof(GameState));
			using (XmlReader reader  = new XmlTextReader(read))
			{
				state = (GameState) serializer.Deserialize(reader);
			}
		}
		
		FromRecord(state);
	}
}

[Serializable]
public struct GameState
{
    public CubeState[] cubeStates;

    public GameState(CubeState[] cubeStates)
    {
        this.cubeStates = cubeStates;
    }
}
}