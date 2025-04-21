using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Utility;
using UnityStandardAssets.Characters.FirstPerson;
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
        public GameObject m_Camera;
        private Vector3 m_MoveDir = Vector3.zero;
        [SerializeField] private MouseLook m_MouseLook;
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        timer = true;
        m_MouseLook.Init(transform, m_Camera.transform);
        StartCoroutine(WorldTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate(){
        RotateView();
        GetInput();
        if (Input.GetKey(KeyCode.R))
            {
                ResetPosition();
            }
        Vector3 desiredMove = transform.forward * m_Input.x + transform.up * m_Input.y + transform.right * m_Input.z;
        m_MouseLook.UpdateCursorLock();
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

            m_Input = new Vector3(vertical,horizontal,altitude);
        }
    private void RotateView()
        {
            m_MouseLook.LookRotation(transform, m_Camera.transform);
        }
}
