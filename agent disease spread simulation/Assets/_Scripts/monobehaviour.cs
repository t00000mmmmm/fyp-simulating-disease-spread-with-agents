using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monobehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AudioSource m_MyAudioSource = GetComponent<AudioSource>();
		m_MyAudioSource.Play();
    }
}
