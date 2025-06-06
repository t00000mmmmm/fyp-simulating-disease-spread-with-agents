using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        public Text timeText;
        public Person indiviual;
        private Camera m_Camera;
        private float m_YRotation;
        private Vector3 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private Vector3 m_OriginalCameraPosition;
        private Vector3 originalPos;
        private float unpausedTime = 1;
        private bool pause;
        private bool reset;
        public float currentTime;
        public int population;
        public int maxHouseholdSize;
        public float timeCycle;
        public GameObject spawnArea;
        public int InitialInfected;
        public int infected;
        public int recovered;
        public float recoveryTime;
        public List<float> dailyInfected = new List<float>();
        public List<float> dailyRecovered = new List<float>();
        [SerializeField] private Text time;
        [SerializeField] private Text days;
        [SerializeField] private Text numberInfected;
        [SerializeField] private Text recoveredText;
        [SerializeField] private Text reproductionNumber;
        int fontSize;
        int sneezing;
        int coughing;
        int pop;

        // Use this for initialization
        private void Start()
        {
            LoadPrefs();
            population = pop;
            time.fontSize = fontSize;
            days.fontSize = fontSize;
            numberInfected.fontSize = fontSize;
            recoveredText.fontSize = fontSize;
            reproductionNumber.fontSize = fontSize;
            originalPos = gameObject.transform.position;
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_MouseLook.Init(transform, m_Camera.transform);
            float spawnXOffset = spawnArea.transform.localScale.x * 10 / 2;
            float spawnXIncrement = spawnArea.transform.localScale.x * 10 / (population);
            infected = InitialInfected + 1;
            for (int x = 0; x < population; x++)
            {
                Person a = Instantiate(indiviual, new Vector3(-spawnXOffset + spawnXIncrement * x + 1, 1, UnityEngine.Random.Range(-spawnArea.transform.lossyScale.z * 5, spawnArea.transform.lossyScale.z * 5)), Quaternion.identity);
                a.controller = this;
                a.timeToRecover = recoveryTime;
                if (sneezing ==1){
                    a.sneezing = true;
                }
                if (coughing ==1){
                    a.coughing = true;
                }
                if (UnityEngine.Random.Range(x * infected, infected * population) >= population)
                {
                    a.infected = true;
                    a.GetComponent<Renderer>().material.color = Color.red;
                    infected--;
                }
            }
            infected = InitialInfected;
            dailyInfected.Add(infected);
            dailyRecovered.Add(0);
        }



        // Update is called once per frame
        private void Update()
        {
            RotateView();
            time.text = "Time: " + currentTime;
            days.text = "day cycles: " + currentTime / timeCycle;
            numberInfected.text = "infected count: " + (infected - recovered);
            recoveredText.text = "recovered: " + recovered;
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (pause)
                {
                    Time.timeScale = unpausedTime;
                    pause = !pause;
                }
                else
                {
                    unpausedTime = Time.timeScale;
                    pause = !pause;
                    Time.timeScale = 0;
                }
            }
            Time.timeScale = Time.timeScale += CrossPlatformInputManager.GetAxis("Mouse ScrollWheel");
        }



        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Respawn")
            {
                reset = true;
            }


        }



        private void FixedUpdate()
        {
            float speed;
            currentTime += (float)0.1;
            if (Math.Round(currentTime, 1) % timeCycle == 0 & (int)(currentTime / timeCycle) > 0)
            {
                dailyInfected.Add(infected - recovered);
                dailyRecovered.Add(recovered);
                reproductionNumber.text = "R number: " + dailyInfected[(int)(currentTime / timeCycle)] / dailyInfected[(int)(currentTime / timeCycle) - 1];
                if (infected - recovered == 0 & !pause)
                {
                    DontDestroyOnLoad(this);
                    SceneManager.LoadSceneAsync(2);
                    pause = true;
                }
            }
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * m_Input.x + transform.up * m_Input.y + transform.right * m_Input.z;


            m_MoveDir.x = desiredMove.x * speed;
            m_MoveDir.z = desiredMove.z * speed;
            m_MoveDir.y = desiredMove.y * speed;

            m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);


            m_MouseLook.UpdateCursorLock();
            if (reset || Input.GetKey(KeyCode.R))
            {
                gameObject.transform.position = originalPos;
                m_Camera.transform.localPosition = m_OriginalCameraPosition;
                reset = false;
            }


        }








        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");
            float altitude = Input.GetAxis("altitude");
            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector3(vertical, altitude, horizontal);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                //m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used

        }


        private void RotateView()
        {
            m_MouseLook.LookRotation(transform, m_Camera.transform);
        }

        public void LoadPrefs()
        {
            fontSize = PlayerPrefs.GetInt("FontSize", 14);
            sneezing = PlayerPrefs.GetInt("sneezing", 1);
            coughing = PlayerPrefs.GetInt("coughing", 1);
            pop = PlayerPrefs.GetInt("population", 100);
        }



    }

}
