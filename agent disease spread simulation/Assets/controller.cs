using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class controller : MonoBehaviour
{
    ///// Fields /////
    [Header("UI")]
        
        public Text timeText;

        private Vector3 _startPosition;
        private Vector3 m_Input;
        private double currentTime;
        private bool timer;
    [Header("Movement")]

        public float speed;
        private Vector3 m_MoveDir = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        timer = true;
        StartCoroutine(WorldTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate(){
        GetInput();
        if (Input.GetKey(KeyCode.R))
            {
                ResetPosition();
            }
        Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x + transform.up * m_Input.z;
        gameObject.transform.Translate(desiredMove);

    }
    IEnumerator WorldTime()
        {
            while (true)
            {
                currentTime += Time.deltaTime;
                timeText.text = string.Format("Timer: " + (Math.Floor(currentTime * 100) / 100));
                if (!timer)
                {
                    yield break;
                }
                yield return null;
            }
        }
    private void ResetPosition () {
            gameObject.transform.position = _startPosition;

        }
        private void GetInput()
        {
            // Read input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float altitude = Input.GetAxis("altitude");

            m_Input = new Vector3(horizontal,vertical,altitude);
        }
}
