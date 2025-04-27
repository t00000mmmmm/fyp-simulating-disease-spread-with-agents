using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class Person : MonoBehaviour
{
    public FirstPersonController controller;
    public bool infected;
    public GameObject sneezeCone;
    private int infectedTime = 0;
    public float timeToRecover;
    private bool recovered;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (infected&!recovered& (int)(controller.currentTime*4%controller.timeCycle) == 0)
        {

            gameObject.name= "infected individual";
            Instantiate(sneezeCone,transform);
        }
        if (infected& !recovered & timeToRecover<(controller.currentTime-infectedTime)/controller.timeCycle){
            recovered = true;
            controller.recovered= controller.recovered+1;
            gameObject.name= "recovered individual";
            gameObject.GetComponent<Renderer> ().material.color = Color.green;
        }
    }
    private void OnTriggerEnter (Collider other) {
            
            if (other.gameObject.tag == "infection" & !infected)
            {
                infected = true;
                controller.infected= controller.infected+1;
                infectedTime = (int)controller.currentTime;
                gameObject.GetComponent<Renderer> ().material.color = Color.red;
            }
            
            
        }

}
