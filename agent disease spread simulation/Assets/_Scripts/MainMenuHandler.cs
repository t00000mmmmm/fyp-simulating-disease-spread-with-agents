using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{

	AudioSource m_MyAudioSource;
	int fontSize;
	int sneezing;
	int coughing;
	int pop;
	[SerializeField] Text play;
	[SerializeField] Text gui;
	[SerializeField] Text load;
	[SerializeField] Text savebut;
	[SerializeField] Slider population;
	[SerializeField] Text coughingBut;
	[SerializeField] Text sneezingBut;
	[SerializeField] Text popLbl;
	private void Start () {
		LoadPrefs();
		fontChange();
		symptomChange();
	}
	public void ButtonHandlerPlay()
	{
		StartCoroutine(playing(1));

	}
	public void ButtonHandlerSave()
	{
		Save("settings");
	}
	public void ButtonHandlerSneeze()
	{
		if (sneezing==1){
			sneezing = 0;
		}else{
			sneezing = 1;
		}
		symptomChange();
		SavePrefs();
	}
	public void ButtonHandlerCough()
	{
		if (coughing==1){
			coughing = 0;
		}else{
			coughing = 1;
		}
		symptomChange();
		SavePrefs();
	}
	public void symptomChange(){
		sneezingBut.text = "sneezing: " + sneezing;
		coughingBut.text = "coughing: " + coughing;
		population.value = pop;
	}
	public void fontChange(){
		play.fontSize = fontSize;
		gui.fontSize = fontSize;
		load.fontSize =fontSize;
		savebut.fontSize = fontSize;
		sneezingBut.fontSize = fontSize;
		coughingBut.fontSize =fontSize;
		popLbl.fontSize =fontSize;
		if (fontSize == 14){
			gui.text = "GUI: Small";
		}else if(fontSize == 18){
			gui.text = "GUI: Medium";
		}else {
			gui.text = "GUI: Large";
		}
	}
	public void popSlider(){
		pop=(int)population.value;
		popLbl.text = "population: " + pop;
		SavePrefs();
	}
	public void ButtonHandlerLoad()
	{
		Load("settings");
	}
	public void ButtonHandlerGUI()
	{
		if (fontSize == 14){
			fontSize = 18;
		}else if(fontSize == 18){
			fontSize = 24;
		}else {
			fontSize = 14;
		}
		fontChange();
		SavePrefs();
	}


    IEnumerator playing(int level)
	{
		m_MyAudioSource = GetComponent<AudioSource>();
		m_MyAudioSource.Play();
		yield return new WaitForSeconds(2);
		SceneManager.LoadSceneAsync(level);
	}
	public void SavePrefs()

	{

		PlayerPrefs.SetInt("FontSize", fontSize);
		PlayerPrefs.SetInt("sneezing", sneezing);
		PlayerPrefs.SetInt("coughing", coughing);
		PlayerPrefs.SetInt("population", pop);
		PlayerPrefs.Save();

	}
	public void LoadPrefs(){
		fontSize = PlayerPrefs.GetInt("FontSize", 14);
		sneezing = PlayerPrefs.GetInt("sneezing", 1);
		coughing = PlayerPrefs.GetInt("coughing", 1);
		pop = PlayerPrefs.GetInt("population", 100);
		popLbl.text = "population: " + pop;
	}
	private void Load(string filename)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filename);
            string xmlString = xmlDocument.OuterXml;
            serial state;
            using (StringReader read = new StringReader(xmlString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(serial));
                using (XmlReader reader = new XmlTextReader(read))
                {
                    state = (serial)serializer.Deserialize(reader);
                }
            }
            coughing = state.coughing;
            sneezing = state.sneezing;
			pop = state.pop;
			symptomChange();
			SavePrefs();
        }
		private void Save(string filename)
        {
            XmlDocument xmlDocument = new XmlDocument();
            serial state = new serial(sneezing, coughing,pop);
            XmlSerializer serializer = new XmlSerializer(typeof(serial));
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, state);
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.Save(filename);
            }
        }

}
