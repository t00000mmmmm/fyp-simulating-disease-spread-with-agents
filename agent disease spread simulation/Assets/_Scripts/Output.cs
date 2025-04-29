using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Output : MonoBehaviour
{
    // Start is called before the first frame update
    public FirstPersonController user;
    public Text recovered;
    public Text infected;
    public Text population;
    void Start()
    {
        user = FindFirstObjectByType<FirstPersonController>();
        population.text = "population: "+user.population;
        for (int x=0;x<user.dailyInfected.Count;x++){
            infected.text =infected.text + user.dailyInfected[x] +", ";
            recovered.text =recovered.text + user.dailyRecovered[x] +", ";
            Debug.Log(user.dailyRecovered[x]);
        }
        Destroy(user);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
