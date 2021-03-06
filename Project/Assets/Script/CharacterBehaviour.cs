﻿using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;


public class CharacterBehaviour : MonoBehaviour {

    public float VerticalSpeed;
    public float Amplitude;
    public Transform PointCenter;
    public Transform Character;
    public GameObject Floor;
    public Button ScreenInput;
    public Camera Cam;

    public GameObject MagneticField;

    public float CamShakeAmt = 0.1f;
    public GameObject AppManager;

    public bool JumpedUp;
    public bool ChangePos;

    public Vector3 NewPositionUp; // The target position
    public Vector3 OldPositionDown; // The target position

    public float Index;

    [Header("States")] 
    public GameObject Happy;
    public GameObject Damaged;
    public GameObject Hat;

    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip CoinSound;
    public AudioClip PowerUpSound;

    [Header("Trigger PowerUp")]
    public bool ShieldTrigger;
    public bool AttractionTrigger;
    public bool SlowdownTrigger;
    public bool RandomTrigger;

    // Private Variables
    private Transform _trans;	// Will hold this.transform.
    private CameraShake _camShake;
    private bool _inputAllowed;
    private Transform _magneticFieldTrans;

    void Start()
    {
        NewPositionUp = PointCenter.position;
        OldPositionDown = Character.position;
        JumpedUp = false;
        ChangePos = false;
        _inputAllowed = true;
        _camShake = AppManager.gameObject.GetComponent<CameraShake>();
        _magneticFieldTrans = MagneticField.transform;
        ShieldTrigger = false;
        AttractionTrigger = false;
        SlowdownTrigger = false;
        RandomTrigger = false;
    }

    void Awake()
    {
        _trans = transform; 
    }
/*
    void Update()
    {

        if (Character.CompareTag("Woman") && Input.GetKeyDown(KeyCode.DownArrow) || Character.CompareTag("Man") && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_inputAllowed)
            {
                if (!JumpedUp)
                {
                    ChangePos = true;
                    JumpedUp = true;
                }
                else
                {
                    JumpedUp = false; //impedisce di entrare nell'if dell'ondeggio
                    ChangePos = false; //dovrebbe permettere di tornare a terra
                }
            }
        }

        //posizionamento al centro delle due calamitine
        if (ChangePos)
        {
            ChangePosition(NewPositionUp);

        }//fa tornare sulla rispettiva superficie la calamita 
        else 
        {
            ChangePosition(OldPositionDown);
        }

        //esegue l'ondeggio
        if (JumpedUp)
        {
            Index += Time.deltaTime;
            float y = Mathf.Abs(VerticalSpeed * Mathf.Sin(Amplitude * Index));
            transform.localPosition += new Vector3(0, y, 0);
        }
    }
*/
    public void MoveMagnet()
    {
        if (_inputAllowed)
        {
            if (!JumpedUp)
            {
                ChangePos = true;
                JumpedUp = true;
            }
            else
            {
                JumpedUp = false; //impedisce di entrare nell'if dell'ondeggio
                ChangePos = false; //dovrebbe permettere di tornare a terra
            }
        } 
    }

    void Update()
    {
        //posizionamento al centro delle due calamitine
        if (ChangePos)
        {
            ChangePosition(NewPositionUp);

        }//fa tornare sulla rispettiva superficie la calamita 
        else 
        {
            ChangePosition(OldPositionDown);
        }

        //esegue l'ondeggio
        if (JumpedUp)
        {
            Index += Time.unscaledDeltaTime;
            float y = Mathf.Abs(VerticalSpeed * Mathf.Sin(Amplitude * Index));
            transform.localPosition += new Vector3(0, y, 0);
        }
    }

    // Go up or down
    void ChangePosition(Vector3 newPosition)
    {
        _trans.position = Vector3.Lerp(_trans.position, newPosition, Time.unscaledDeltaTime * 5.5f);
        _magneticFieldTrans.position = Vector3.Lerp(_trans.position, newPosition, Time.unscaledDeltaTime * 4f);

        if (Mathf.Abs(newPosition.y - _trans.position.y) < 0.02)
        {
            _trans.position = newPosition;
            _magneticFieldTrans.position = newPosition; 
        }

        if (JumpedUp)
        {
            Floor.SetActive(false);
        }
        else
        {
            Floor.SetActive(true);
        }
    }

    // Pick up coins
    void OnTriggerEnter2D(Collider2D other)
    {   
        if (!AppManager.GetComponent<InLevelManager>().Gameover)
        {
            // If the object with which the magnets collide is a coin
            if (other.gameObject.CompareTag("Coin"))
            {
                // Make the coin disappear
                other.gameObject.SetActive(false);

                //Play sound
                audioSource.PlayOneShot(CoinSound);

                // Increment the coins count
                AppManager.GetComponent<InLevelManager>().SetCount(true);
            }
            // If the object with which the magnets collide is a power-up
            else if (other.gameObject.CompareTag("PowerUp"))
            {
                other.gameObject.SetActive(false);

                //Play sound
                audioSource.PlayOneShot(PowerUpSound);

                switch (other.gameObject.name)
                {
                    case "Attraction":
                        AppManager.GetComponent<InLevelManager>().Attraction();
                        AttractionTrigger = true;
                        break;
                        
                    case "Slowdown":
                        AppManager.GetComponent<InLevelManager>().Slowdown();
                        SlowdownTrigger = true;
                        break;
                    
                    case "Shield":
                        AppManager.GetComponent<InLevelManager>().Shield();
                        ShieldTrigger = true;
                        break;
                        
                    case "Random":
                        AppManager.GetComponent<InLevelManager>().Random();
                        RandomTrigger = true;
                        break;
                }
            }
            // If the object with which the magnets collide is a tree
            else if (_inputAllowed && (other.gameObject.CompareTag("Branch") || other.gameObject.CompareTag("Bot")))
            {
                if (!AppManager.GetComponent<InLevelManager>().ShieldActive)
                {
                    Damaged.SetActive(true);
                    AppManager.GetComponent<InLevelManager>()
                        .SetTimeAfterCollision(Cam.WorldToViewportPoint(transform.position));
                    StartCoroutine(Blink(3, 0.2f, 0.4f));
                    _camShake.Shake(CamShakeAmt, 0.1f);
                    AppManager.GetComponent<InLevelManager>().RemoveLife();
                }
                else
                {
                    AppManager.GetComponent<InLevelManager>()
                        .SetCoinsAfterCollision(Cam.WorldToViewportPoint(transform.position));
                    
                    audioSource.PlayOneShot(CoinSound);
                    
                }
            }
            // If the object with which the magnets collide is the finish line
            else if (other.gameObject.CompareTag("Finish"))
            {
                ChangePos = false;
                JumpedUp = false;
                ScreenInput.gameObject.SetActive(false);
                AppManager.GetComponent<InLevelManager>().ShowStatistics();
            }
        }
    }

    // Enable (input = true) or disable (input = false) the input from keyboard
    public void SetInput(bool input)
    {
        _inputAllowed = input;
    }
    
    // Flicker when a magnet hits a tree
    IEnumerator Blink(int nTimes, float timeOn, float timeOff)
    {
        while (nTimes > 0)
        {
            GetComponent<Renderer>().enabled = true;
            Damaged.GetComponent<Renderer>().enabled = true;
            Happy.GetComponent<Renderer>().enabled = true;
            Hat.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(timeOn);
            GetComponent<Renderer>().enabled = false;
            Damaged.GetComponent<Renderer>().enabled = false;
            Happy.GetComponent<Renderer>().enabled = false;
            Hat.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(timeOff);
            nTimes--;
        }
        
        GetComponent<Renderer>().enabled = true;
        Happy.GetComponent<Renderer>().enabled = true;
        Hat.GetComponent<Renderer>().enabled = true;
        Damaged.SetActive(false);
    }
}
