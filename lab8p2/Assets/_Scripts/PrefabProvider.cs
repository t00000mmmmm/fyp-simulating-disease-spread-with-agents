using UnityEngine;

public class PrefabProvider : MonoBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private GameObject fpsControllerPrefab;
    public static GameObject BallPrefab {get; private set;}
    public static GameObject FPSControllerPrefab {get; private set;}

    void Awake ()
    {
        BallPrefab = ballPrefab;
        FPSControllerPrefab = fpsControllerPrefab;
    }
}