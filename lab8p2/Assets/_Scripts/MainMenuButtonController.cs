using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuButtonController : MonoBehaviour 
{

	public void ButtonControllerStartGame() 
	{
		SceneManager.LoadSceneAsync (Globals.GAME_SCENE);
	}

	public void ButtonControllerLoadGame() 
	{
		StartCoroutine(LoadGame());
	}

	IEnumerator LoadGame ()
	{
		DontDestroyOnLoad(gameObject);
		AsyncOperation loadingOperation = SceneManager.LoadSceneAsync (Globals.GAME_SCENE);
		while (!loadingOperation.isDone)
			yield return null;
		FindObjectOfType<SavegameController>().Load();
		Destroy(gameObject);
	}
}
