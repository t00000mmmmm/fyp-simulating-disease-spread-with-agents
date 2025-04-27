using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour 
{

	public GameObject ballPrefab;	
	public float firepower;
	[SerializeField]
	private int maximumAmmuniton;
	public int MaximumAmmunition {get {return maximumAmmuniton;}}
	[SerializeField]
	private int currentAmmunition;
	public int CurrentAmmunition 
		{get {return currentAmmunition;} 
		set  {
			int oldAmmuniton = currentAmmunition;
			currentAmmunition = Mathf.Max(0, Mathf.Min(MaximumAmmunition, value));
			if (oldAmmuniton < currentAmmunition)
				Audio.PlayOneShot(increaseAmmoSound);
			else if (oldAmmuniton > currentAmmunition)
				Audio.PlayOneShot(firingSound);
			}}
	private Transform HoopTransform {get; set;}
	private AudioSource Audio {get; set;}
    public AudioClip firingSound;
    public AudioClip clickingSound;
	public AudioClip increaseAmmoSound;
	
	void Awake ()
	{
		HoopTransform = FindObjectOfType<HoopBehaviour>().transform;
		Audio 		  = GetComponentInParent<AudioSource>();
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			if (CurrentAmmunition > 0
			&& !GameManager.GameOver)
				FireBall();
			else 
				Audio.PlayOneShot(clickingSound);
		}
	}

	void FireBall ()
	{
		GameObject ball = GameObject.Instantiate(ballPrefab, transform.position + 0.25f*transform.forward, transform.rotation);
		ball.GetComponent<Rigidbody>().AddForce(firepower * transform.forward, ForceMode.Impulse);
		BallBehaviour bb = ball.AddComponent<BallBehaviour>();
		bb.distanceOnShooting = Vector3.Distance(transform.position, HoopTransform.position);
		CurrentAmmunition = CurrentAmmunition - 1;
	}

	public PlayerRecord ToRecord()
	{
		return new PlayerRecord(transform.parent.position, transform.parent.rotation, transform.localRotation, CurrentAmmunition);
	}
	
	public static PlayerController FromRecord (PlayerRecord record)
	{
		GameObject obj = Instantiate(PrefabProvider.FPSControllerPrefab, record.position, record.rotation);
		PlayerController controller = obj.GetComponentInChildren<PlayerController>();
		controller.transform.localRotation   = record.cameraRotation;
		controller.currentAmmunition 		 = record.currentAmmunition;
		return controller;
	}
}

[Serializable]
public struct PlayerRecord
{
	public Vector3 position;
	public Quaternion rotation;
	public Quaternion cameraRotation;
	public int currentAmmunition;

	public PlayerRecord(Vector3 position, Quaternion rotation, Quaternion cameraRotation, int currentAmmunition)
	{
		this.position = position;
		this.rotation = rotation;
		this.cameraRotation = cameraRotation;
		this.currentAmmunition = currentAmmunition;
	}
}
