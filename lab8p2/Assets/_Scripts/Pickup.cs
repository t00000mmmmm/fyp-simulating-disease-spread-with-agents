using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{

    private float timeUntilActive = 0;

    public void FromRecord(PickupRecord record)
    {
        this.timeUntilActive = record.timeUntilActive;
        Hide();
    }

    public PickupRecord ToRecord()
    {
        return new PickupRecord(timeUntilActive);
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController pc  = other.GetComponentInChildren<PlayerController>();
            int oldAmmuniton     = pc.CurrentAmmunition;
            pc.CurrentAmmunition = pc.CurrentAmmunition + 10;
            if (pc.CurrentAmmunition > oldAmmuniton)
            {
                timeUntilActive      = 5;
                Hide();
            }
        }
    }

    private void Reveal()
    {
        GetComponent<Collider>().enabled     = true;
        GetComponent<MeshRenderer>().enabled = true;
    }

    private void Hide()
    {
        GetComponent<Collider>().enabled     = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {
        if (timeUntilActive > 0)
        {
            Hide();
        }
        else
        {
            Reveal();
        }
        timeUntilActive -= Time.deltaTime;
    }
}

public struct PickupRecord
{
    public float timeUntilActive;

    public PickupRecord(float timeUntilActive)
    {
        this.timeUntilActive = timeUntilActive;
    }
}