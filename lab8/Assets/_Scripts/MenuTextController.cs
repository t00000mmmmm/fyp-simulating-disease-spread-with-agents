using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTextController : MonoBehaviour {

    public Text highScoreText;

    void Start() {
        RemoteHighScoreManager.Instance.GetHighScoreCR();
    }

    void UpdateUI(int score) {
        if (score > 0) highScoreText.text = "High Score: " + score + "!";
        else highScoreText.text = "No High Score!";
    }

    public void ButtonHandlerReset() {
        RemoteHighScoreManager.Instance.SetHighScoreCR(0);
    }

    void ResetOnComplete() {
        UpdateUI(0);
    }

}
