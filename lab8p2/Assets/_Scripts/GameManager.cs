using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance {get; set;}
    public static int score;
    public static float timeRemaining;
    private static float timeAccumulator;
    private static bool loadingMenu;
    public static bool GameOver {get {return timeRemaining == 0;}}
    private static AudioSource Audio {get; set;}
    public AudioClip increaseScoreSound;
	public AudioClip timerTickSound;
	public AudioClip gameOverSound;

    public static void FromRecord (GameManagerRecord record)
    {
        score           = record.score;
        timeRemaining   = record.timeRemaining;
        timeAccumulator = record.timeAccumulator;
        loadingMenu     = false; // This relies on the fact that we cannot save the state when the game is over!
    }

    public static GameManagerRecord ToRecord()
    {
        return new GameManagerRecord(score, timeRemaining, timeAccumulator);
    }

    void Awake ()
    {
        timeRemaining = 30;
        Audio         = GetComponent<AudioSource>();
        Instance      = this;
        loadingMenu   = false;
    }

    void Update ()
    {
        if (!GameOver)
        {
            timeAccumulator += Time.deltaTime;
            while (timeAccumulator >= 1)
            {
                TimerTick();
                timeAccumulator -= 1;
            }
        }
        else
        {
            timeAccumulator += Time.deltaTime;
            if (timeAccumulator >= gameOverSound.length + 3
            && !loadingMenu)
            {
                loadingMenu = true;
		        SceneManager.LoadSceneAsync (Globals.MENU_SCENE);
            }
        }
    }

	void TimerTick() {
		timeRemaining -= 1;
		if (timeRemaining > 0) 
        {
			Audio.PlayOneShot (timerTickSound);
		} 
        else 
        {
            timeAccumulator = 0;
			Audio.PlayOneShot (gameOverSound);
		}
	}

    public static void IncreaseScore (float distance)
    {
        score += (int) Mathf.Ceil(distance);
        Audio.PlayOneShot(Instance.increaseScoreSound);
    }
}

[Serializable]
public struct GameManagerRecord
{

    public int score;
    public float timeRemaining;    
    public float timeAccumulator;

    public GameManagerRecord (int score, float timeRemaining, float timeAccumulator)
    {
        this.score           = score;
        this.timeRemaining   = timeRemaining;
        this.timeAccumulator = timeAccumulator;
    }
}