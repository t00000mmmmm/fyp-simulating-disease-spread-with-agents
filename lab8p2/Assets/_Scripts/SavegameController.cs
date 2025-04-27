using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityStandardAssets.Characters.FirstPerson;

public class SavegameController : MonoBehaviour
{

    private const string savefileName = "Assets/Saves/game.xml";

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F1) && !GameManager.GameOver)
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Load();
        }
    }

    public void Save ()
	{
		XmlDocument xmlDocument    = new XmlDocument();
		GameRecord record          = ToRecord();
		XmlSerializer serializer   = new XmlSerializer(typeof(GameRecord));
		using (MemoryStream stream = new MemoryStream())
		{
			serializer.Serialize(stream, record);
			stream.Position = 0;
			xmlDocument.Load(stream);
			xmlDocument.Save(savefileName);
		}
	}

    public void Load ()
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(savefileName);
		string xmlString = xmlDocument.OuterXml;
		
		GameRecord record;
		using (StringReader read = new StringReader(xmlString))
		{
			XmlSerializer serializer = new XmlSerializer(typeof(GameRecord));
			using (XmlReader reader  = new XmlTextReader(read))
			{
				record = (GameRecord) serializer.Deserialize(reader);
			}
		}
		
		FromRecord (record);
	}

    private void FromRecord (GameRecord record)
    {
        Pickup pickup = FindObjectOfType<Pickup>();
        ClearScene();
        for (int i = 0; i < record.balls.Length; i++)
        {
            BallBehaviour.FromRecord(record.balls[i]);
        }
        PlayerController.FromRecord(record.player);
        pickup.FromRecord(record.pickup);
        pickup.gameObject.SetActive(true);
        GameManager.FromRecord(record.gameManager);
    }

    private void ClearScene()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        Destroy(player.transform.parent.gameObject);        
        BallBehaviour[] balls   = FindObjectsOfType<BallBehaviour>();
        for (int i = 0; i < balls.Length; i++)
        {
            Destroy(balls[i].gameObject);
        }
        Pickup pickup = FindObjectOfType<Pickup>();
        pickup.gameObject.SetActive(false);
    }

    private GameRecord ToRecord ()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        BallBehaviour[] balls   = FindObjectsOfType<BallBehaviour>();
        BallRecord[]    ballRecords = new BallRecord[balls.Length];
        for (int i = 0; i < ballRecords.Length; i++)
        {
            ballRecords[i] = balls[i].ToRecord();
        }
        PickupRecord pickup = FindObjectOfType<Pickup>().ToRecord();
        return new GameRecord(player.ToRecord(), ballRecords, pickup, GameManager.ToRecord());
    }
}

[Serializable]
public struct GameRecord
{
    public PlayerRecord player;
    public BallRecord[] balls;
    public PickupRecord pickup;
    public GameManagerRecord gameManager;
    

    public GameRecord(PlayerRecord player, BallRecord[] balls, PickupRecord pickup, GameManagerRecord gameManager)
    {
        this.player = player;
        this.balls  = balls;
        this.pickup = pickup;
        this.gameManager = gameManager;
    }
}