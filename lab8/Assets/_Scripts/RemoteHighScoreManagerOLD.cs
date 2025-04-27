using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;



public class RemoteHighScoreManagerOLD : MonoBehaviour {

    //public static RemoteHighScoreManager Instance { get; private set; }

    //void Awake() {
    //    // force singleton instance
    //    if (Instance == null) { Instance = this; } else { Destroy(gameObject); }

    //    // don't destroy this object when we load scene
    //    DontDestroyOnLoad(gameObject);
    //}

    //// TODO #1 - change the signature to be a Coroutine, add callback parameter with int parameter
    //public IEnumerator GetHighScoreCR(Action<int> onCompleteCallback) {
    //    // TODO #2 - construct the url for our request, including objectid!
    //    string url = "http://api.backendless.com/v1/data/TableHighScore/"; // you need to add your OWN object id here!!

    //    // TODO #3 - create a GET UnityWebRequest, passing in our URL
    //    UnityWebRequest webreq = UnityWebRequest.Get(url);

    //    // TODO #4 - set the request headers as dictated by the backendless documentation (3 headers)
    //    webreq.SetRequestHeader("application-id", Globals.APPLICATION_ID);
    //    webreq.SetRequestHeader("secret-key", Globals.REST_SECRET_KEY);
    //    webreq.SetRequestHeader("application-type", "REST");

    //    // TODO #5 - Send the webrequest and yield (so the script waits until it returns with a result)
    //    yield return webreq.Send();

    //    // TODO #6 - check for webrequest errors
    //    if (webreq.isNetworkError) {
    //        Debug.Log(webreq.error);
    //    } else {
    //        // TODO #7 - Convert the downloadHandler.text property to HighScoreResult (currently JSON)
    //        HighScoreResult highScoreData = JsonUtility.FromJson<HighScoreResult>(webreq.downloadHandler.text);

    //        // TODO #8 - check that there are no backendless errors
    //        if (!string.IsNullOrEmpty(highScoreData.code)) {
    //            Debug.Log("Error:" + highScoreData.code + " " + highScoreData.message);
    //        } else // TODO #9 - call the callback function, passing the score as the parameter
    //          {
    //            onCompleteCallback(highScoreData.Score);
    //        }
    //    }
    //}

    //// TODO #1 - change the signature to be a Coroutine, add callback parameter
    //public IEnumerator SetHighScoreCR(int score, Action onCompleteCallback) {
    //    // TODO #2 - construct the url for our request, including objectid!
    //    string url = "http://api.backendless.com/v1/data/TableHighScore/"; // you need to add your OWN object id here!!

    //    // TODO #3 - construct JSON string for data we want to send
    //    string data = JsonUtility.ToJson(new HighScoreResult { Score = score });

    //    // TODO #4 - create PUT UnityWebRequest passing our url and data
    //    UnityWebRequest webreq = UnityWebRequest.Put(url, data);

    //    // TODO #5 set the request headers as dictated by the backendless documentation (4 headers)
    //    webreq.SetRequestHeader("Content-Type", "application/json");
    //    webreq.SetRequestHeader("application-id", Globals.APPLICATION_ID);
    //    webreq.SetRequestHeader("secret-key", Globals.REST_SECRET_KEY);
    //    webreq.SetRequestHeader("application-type", "REST");

    //    // TODO #6 - Send the webrequest and yield (so the script waits until it returns with a result)
    //    yield return webreq.Send();

    //    // TODO #7 - check for webrequest errors
    //    if (webreq.isNetworkError) {
    //        Debug.Log(webreq.error);
    //    } else {
    //        // TODO #7 - Convert the downloadHandler.text property to HighScoreResult (currently JSON)
    //        HighScoreResult highScoreData = JsonUtility.FromJson<HighScoreResult>(webreq.downloadHandler.text);

    //        // TODO #8 - check that there are no backendless errors
    //        if (!string.IsNullOrEmpty(highScoreData.code)) {
    //            Debug.Log("Error:" + highScoreData.code + " " + highScoreData.message);
    //        } else // TODO #9 - call the callback function to signal success
    //          {
    //            onCompleteCallback();
    //        }
    //    }
    //}

}
