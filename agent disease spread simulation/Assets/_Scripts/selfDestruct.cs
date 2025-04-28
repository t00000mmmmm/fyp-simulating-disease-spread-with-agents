using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    private float counter;
    public Person owner;
    void Start()
    {
        counter=0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter+= (float)0.1;
        if (counter > 5){
         Destroy(gameObject);
        }
    }
    private void OnTriggerEnter (Collider other) {
            
            if (other.gameObject.tag == "Individual")
            {
                owner.infections++;
            }
            
            
        }
}
