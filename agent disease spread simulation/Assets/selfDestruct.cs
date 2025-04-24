using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    private int counter;
    void Start()
    {
        counter=0;
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        if (counter > 10){
         Destroy(gameObject);
        }
    }
}
