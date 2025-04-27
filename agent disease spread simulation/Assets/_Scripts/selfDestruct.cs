using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    private float counter;
    void Start()
    {
        counter=0;
    }

    // Update is called once per frame
    void Update()
    {
        counter+= (float)0.1;
        if (counter > 10){
         Destroy(gameObject);
        }
    }
}
