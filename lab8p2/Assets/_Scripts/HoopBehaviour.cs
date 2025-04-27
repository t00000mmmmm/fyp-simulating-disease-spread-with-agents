using UnityEngine;
using System.Collections.Generic;

public class HoopBehaviour : MonoBehaviour
{
	private Dictionary<BallBehaviour, int> signsOnEntry;
	private Vector3 translation;
	private Vector3 normal;

	void Awake ()
	{
		this.signsOnEntry = new Dictionary<BallBehaviour, int>(100);
		this.translation = transform.position;
		this.normal		 = transform.up;
	}

	public float signedDistanceToSupportingPlane(Vector3 point)
	{
		return Vector3.Dot(normal, point) - Vector3.Dot(normal, translation);
	}

    void OnTriggerEnter(Collider other) 
    {
		if (!GameManager.GameOver && other.tag == Globals.BALL_TAG) 
        {
			BallBehaviour behaviour = other.GetComponent<BallBehaviour>();
			int sign = behaviour.signOfDistanceToHoopPlaneOnShooting;
			signsOnEntry.Add(behaviour, sign);
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (!GameManager.GameOver && other.tag == Globals.BALL_TAG) 
        {
			int oldSign;
			int newSign =  (int) Mathf.Sign(signedDistanceToSupportingPlane(other.transform.position));
			BallBehaviour behaviour = other.GetComponent<BallBehaviour>();
			if (signsOnEntry.TryGetValue(behaviour, out oldSign)
			&&	oldSign != newSign)
			{
				float distance = behaviour.distanceOnShooting;
				GameManager.IncreaseScore(distance);
			}
			signsOnEntry.Remove(behaviour);
		}
	}
}
