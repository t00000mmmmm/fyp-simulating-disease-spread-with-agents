using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class Person : MonoBehaviour
{
    public FirstPersonController controller;
    public bool infected;
    public GameObject sneezeCone;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (infected)
        {
            Instantiate(sneezeCone,transform);
        }
    }
    private void OnTriggerEnter (Collider other) {
            
            if (other.gameObject.tag == "infection" & !infected)
            {
                infected = true;
                controller.infected= controller.infected+1;
            }
            
            
        }

}
