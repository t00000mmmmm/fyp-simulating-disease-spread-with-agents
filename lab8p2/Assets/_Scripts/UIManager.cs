using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{

	public Text scoreText;
	public Text timerText;
	public Text ammoText;

    public PlayerController player;


	void Awake() 
    {
	}

    void Update ()
    {
		if (player == null) player = FindObjectOfType<PlayerController>();
        UpdateScore(GameManager.score);
        UpdateAmmo (player.CurrentAmmunition);
        UpdateTimer(GameManager.timeRemaining);
    }

	public void UpdateScore(int score) 
    {
		scoreText.text = "Score: " + score.ToString ();
	}

	public void UpdateAmmo(int ammo) 
    {
		ammoText.text = ammo.ToString ();
	}

	public void UpdateTimer(float time) 
    {
		timerText.text = ((int) time).ToString ();
	}


}