using UnityEngine;

namespace Triangle
{
public class PrefabProvider : MonoBehaviour
{
    public GameObject cubePrefab;
    public static GameObject CubePrefab {get; private set;}

    void Awake ()
    {
        CubePrefab = cubePrefab;
    }
}
}