using UnityEngine;
using System.Collections;
using System;

public class BallBehaviour : MonoBehaviour
{
    public static HoopBehaviour hoop;
    public float distanceOnShooting;
    public int signOfDistanceToHoopPlaneOnShooting;
    private float lifetimeRemaining;

    void Awake ()
    {
        if (hoop == null)
            hoop = FindObjectOfType<HoopBehaviour>();
        Initialise();
    }

    private void Initialise ()
    {
        lifetimeRemaining = 10;
        this.signOfDistanceToHoopPlaneOnShooting = 
            (int) Mathf.Sign(hoop.signedDistanceToSupportingPlane(transform.position));
    }

    void Update ()
    {
        lifetimeRemaining -= Time.deltaTime;
        if (lifetimeRemaining <= 0.0f)
            GameObject.Destroy(gameObject);

    }

    public BallRecord ToRecord()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        return new BallRecord(transform.position, transform.rotation, body.velocity, body.angularVelocity,
            distanceOnShooting, signOfDistanceToHoopPlaneOnShooting, lifetimeRemaining);
    }

    public static BallBehaviour FromRecord(BallRecord record)
    {
        GameObject obj = Instantiate(PrefabProvider.BallPrefab);
        BallBehaviour behaviour = obj.AddComponent<BallBehaviour>();
        behaviour.transform.position = record.position;
        behaviour.transform.rotation = record.rotation;
        Rigidbody body = behaviour.GetComponent<Rigidbody>();
        body.velocity = record.velocity;
        body.angularVelocity = record.angularVelocity;
        behaviour.distanceOnShooting = record.distanceOnShooting;
        behaviour.signOfDistanceToHoopPlaneOnShooting = record.signOfDistanceToHoopPlaneOnShooting;
        behaviour.lifetimeRemaining = record.lifetimeRemaining;
        return behaviour;
    }
}

[Serializable]
public struct BallRecord
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
    public Vector3 angularVelocity;
    public float distanceOnShooting;
    public int signOfDistanceToHoopPlaneOnShooting;
    public float lifetimeRemaining;

    public BallRecord (Vector3 position, Quaternion rotation, Vector3 velocity, Vector3 angularVelocity,
            float distanceOnShooting, int signOfDistanceToHoopPlaneOnShooting, float lifetimeRemaining)
    {
        this.position = position;
        this.rotation = rotation;
        this.velocity = velocity;
        this.angularVelocity = angularVelocity;
        this.distanceOnShooting = distanceOnShooting;
        this.signOfDistanceToHoopPlaneOnShooting = signOfDistanceToHoopPlaneOnShooting;
        this.lifetimeRemaining = lifetimeRemaining;
    }
}