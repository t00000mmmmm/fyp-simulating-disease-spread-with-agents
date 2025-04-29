using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class Person : MonoBehaviour
{
    public FirstPersonController controller;
    public bool infected;
    public selfDestruct sneezeCone;
    private int infectedTime = 0;
    public float timeToRecover;
    private bool recovered;
    public int infections;
    public bool sneezing=false;
    public bool coughing=false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sneezing)
        {
            if (infected & !recovered & Math.Round(controller.currentTime,1) * 4 % controller.timeCycle == 0)
            {

                gameObject.name = "infected individual";
                selfDestruct s = Instantiate(sneezeCone, transform.position, transform.rotation);
                s.owner = this;
            }
        }
        if (coughing)
        {
            if (infected & !recovered & Math.Round(controller.currentTime,1) * 2 % controller.timeCycle == 0)
            {
                gameObject.name = "infected individual";
                selfDestruct s = Instantiate(sneezeCone, transform.position, transform.rotation);
                s.owner = this;
                s.transform.localScale = new Vector3(1,1,(float)0.33);
                s.name= "coughCone";
            }
        }
        if (infected & !recovered & timeToRecover < (controller.currentTime - infectedTime) / controller.timeCycle)
        {
            recovered = true;
            controller.recovered = controller.recovered + 1;
            gameObject.name = "recovered individual";
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "infection" & !infected)
        {
            infected = true;
            controller.infected = controller.infected + 1;
            infectedTime = (int)controller.currentTime;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }


    }

}
