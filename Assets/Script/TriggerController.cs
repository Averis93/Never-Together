using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TriggerController : MonoBehaviour {
    
    public Text LearningPart;
    public Text LevelNumber;

    [Header("Characters")]
    public GameObject Man;
    public GameObject Woman;

    [Header("Tutorial Images")]
    public GameObject BlackBackground;
    public GameObject[] FirstTutorial;
    public GameObject[] SecondTutorial;
    public GameObject[] ThirdTutorial;
    public GameObject[] FourthTutorial;
    public GameObject[] FifthTutorial;

    [Header("Props without Tutorial")]
    public GameObject Props;
    public GameObject[] TutorialTriggers;

    [Header("Explosion Effect")]
    public GameObject ExplosionEffect;
    public GameObject EndExplosionEffect;

    [Header("Interference Effect")]
    public Text StartInterference;
    public GameObject[] InvisibleEffect;

    [Header("Light")]
    public Light[] RedLight;

    [Header("Screen Inputs")]
    public GameObject[] screenInput;

    public bool _isFreeze;
    public bool _showed;
    public bool _interferenceStart;
    public bool _interferenceEnd;
    private int _lenghtTutorial;
    private int _numTutorial;
    private int _numExplosion;
    private int _lenghtRedLight;
    private bool _nextImage;
    private int _tutorialLenght;

    public bool _explosion;
    
    private String _currentScene;

    public static bool _TutorialSeen_LVL1;
    public static bool _TutorialSeen_LVL4;
    
    // Use this for initialization
    void Start () {
        _isFreeze = false;
        _showed = false;
        _nextImage = false;
        _explosion = false;
        _interferenceStart = false;
        _interferenceEnd = false;
        _currentScene = SceneManager.GetActiveScene().name;
        _tutorialLenght = TutorialTriggers.Length;

    }
	
	// Update is called once per frame
	void Update () {
        if (_isFreeze)
        {
            Time.timeScale = 0f;            
        }

        if (_showed)
        {
            Time.timeScale = 1f;
            BlackBackground.SetActive(false);
        }

        if (_TutorialSeen_LVL1 && _currentScene == "Level1")
        {
            LearningPart.text = "LEVEL 1";
            Props.SetActive(true);
            for (int i = 0; i < _tutorialLenght; i++)
                TutorialTriggers[i].SetActive(false);

        }

        if (_TutorialSeen_LVL1 && _TutorialSeen_LVL4 && _currentScene == "Level4")
        {
            TutorialTriggers[0].tag = "Explosion";
            TutorialTriggers[0].GetComponent<SpriteRenderer>().enabled = false;
            TutorialTriggers[1].tag = "EndExplosionEffect";
            TutorialTriggers[1].GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    
    //Avvia un effetto o un tutorial in base all'entrata in uno specifico trigger
    void OnTriggerStay2D(Collider2D other)
    {
        var TriggerName = other.gameObject.tag;

        switch (TriggerName)
        {
            case "StartLevel":
                {
                    StartCoroutine(FadeTextIn(LevelNumber, 0.7f));
                }
                break;

            //the magnet control
            case "FirstTutorial":
                {
                    _numTutorial = 1;
                    _lenghtTutorial = FirstTutorial.Length - 1;

                    if (!_isFreeze && !_showed)
                    {
                        _isFreeze = true;
                        StartCoroutine(ShowTutorial(FirstTutorial, null));
                    }
                }
                break;

            //against an obstacle
            case "SecondTutorial":
                {
                    _numTutorial = 2;
                    _lenghtTutorial = SecondTutorial.Length - 1;

                    if (!_isFreeze && !_showed)
                    {
                        _isFreeze = true;
                        StartCoroutine(ShowTutorial(SecondTutorial, null));
                    }
                }
                break;

            //against a bot
            case "ThirdTutorial":
                {
                    _numTutorial = 3;
                    _lenghtTutorial = ThirdTutorial.Length - 1;

                    if (!_isFreeze && !_showed)
                    {
                        _isFreeze = true;
                        StartCoroutine(ShowExplosionTutorial(ThirdTutorial, ExplosionEffect));
                    }
                }
                break;

            //Inversion of magnet control
            case "FourthTutorial":
                {
                    _numTutorial = 4;
                    _lenghtTutorial = FourthTutorial.Length - 1;

                    if (!_isFreeze && !_showed)
                    {
                        _isFreeze = true;
                        StartCoroutine(ShowTutorial(FourthTutorial, null));
                    }
                }
                break;

            //Avoid the collision between them
            case "FifthTutorial":
                {
                    _numTutorial = 5;
                    _lenghtTutorial = FifthTutorial.Length - 1;

                    if (!_isFreeze && !_showed)
                    {
                        _isFreeze = true;
                        StartCoroutine(ShowTutorial(FifthTutorial, null));
                    }
                }
                break;

            //Interference part
            case "Interference":
                {
                    StartCoroutine(FadeTextIn(StartInterference, 0.7f));
                    for (int i = 0; i < RedLight.Length; i++)
                    {
                        RedLight[i].color = Color.red;
                    }

                    InvisibleEffect[0].SetActive(true);
                    _interferenceStart = true;
                    InvisibleEffect[1].SetActive(true);
                    InvisibleEffect[3].SetActive(true);
                    InvisibleEffect[4].SetActive(true);
                }
                break;

            //End Interference part
            case "EndInterference":
                {
                    InvisibleEffect[0].SetActive(false);
                    InvisibleEffect[1].SetActive(false);
                    InvisibleEffect[2].SetActive(false);
                    InvisibleEffect[3].SetActive(false);
                    InvisibleEffect[4].SetActive(false);
                    InvisibleEffect[5].SetActive(false);
                    _interferenceEnd = true;
                    RedLight[_lenghtRedLight].color = Color.grey;
                }
                break;

            //useful to play the explosion effect
            case "Explosion":
                {
                    _numExplosion = 0;
                    _explosion = true;
                    _lenghtRedLight = RedLight.Length - 1;

                    if (!_isFreeze && !_showed)
                    {
                        _isFreeze = true;
                        StartCoroutine(ShowExplosionTutorial(null, ExplosionEffect));
                    }
                }
                break;

            //End explosion part in the fourth level
            case "EndExplosionEffect_First":
                {
                    _numTutorial = 1;
                    _explosion = true;
                    _lenghtTutorial = FirstTutorial.Length - 1;

                    if (!_isFreeze && !_showed)
                    {
                        _isFreeze = true;
                        StartCoroutine(ShowExplosionTutorial(FirstTutorial, EndExplosionEffect));
                    }
                }
                break;

            //End explosion part in the rest of the level
            case "EndExplosionEffect":
                {
                    _numExplosion = 1;
                    _explosion = true;
                    _lenghtTutorial = FirstTutorial.Length - 1;

                    if (!_isFreeze && !_showed)
                    {
                        _isFreeze = true;
                        StartCoroutine(ShowExplosionTutorial(null, EndExplosionEffect));
                    }
                }
                break;

            default:
                Debug.Log("Unknown trigger name");
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var TriggerName = other.gameObject.tag;

        switch (TriggerName)
        {
            case "FirstTutorial":
                {
                    _isFreeze = false;
                    _showed = false;
                }
                break;

            case "SecondTutorial":
                {
                    _isFreeze = false;
                    _showed = false;
                    _TutorialSeen_LVL1 = true;
                }
                break;

            case "ThirdTutorial":
                {
                    _isFreeze = false;
                    _showed = false;
                }
                break;

            case "FourthTutorial":
                {
                    _isFreeze = false;
                    _showed = false;
                }
                break;

            case "FifthTutorial":
                {
                    _isFreeze = false;
                    _showed = false;
                }
                break;

            case "EndExplosionEffect":
                {
                    _isFreeze = false;
                    _showed = false;
                    _explosion = false;                    
                }
                break;

            case "EndExplosionEffect_First":
                {
                    _isFreeze = false;
                    _showed = false;
                    _explosion = false;
                    _TutorialSeen_LVL4 = true;
                }
                break;

            case "Explosion":
                {
                    _isFreeze = false;
                    _showed = false;
                    _explosion = false;
                }
                break;

            default:
                Debug.Log("Unknown trigger name");
                break;
        }
    }

    //Show the several image onClick based on the specific tutorial
    public void NextImageTutorial()
    {
        if (_isFreeze && _nextImage)
        {
            switch (_numTutorial)
            {
                //First tutorial about magnet's control
                case 1:
                    {
                        if (_lenghtTutorial == 3)
                        {
                            FirstTutorial[_lenghtTutorial - 2].SetActive(true);
                            FirstTutorial[_lenghtTutorial - 3].SetActive(false);
                            _lenghtTutorial--;
                        }
                        else if (_lenghtTutorial == 2)
                        {
                            FirstTutorial[_lenghtTutorial].SetActive(true);
                            FirstTutorial[_lenghtTutorial - 1].SetActive(false);
                            _lenghtTutorial--;
                        }
                        else if (_lenghtTutorial == 1)
                        {
                            FirstTutorial[_lenghtTutorial + 2].SetActive(true);
                            FirstTutorial[_lenghtTutorial + 1].SetActive(false);
                            _lenghtTutorial--;
                        }
                        else if (_lenghtTutorial == 0)
                        {
                            FirstTutorial[_lenghtTutorial + 3].SetActive(false);
                            Man.GetComponent<CharacterBehaviour>().SetInput(true);
                            Woman.GetComponent<CharacterBehaviour>().SetInput(true);
                            screenInput[0].SetActive(true);
                            screenInput[1].SetActive(true);
                            screenInput[2].SetActive(false);
                            screenInput[3].SetActive(false);
                            screenInput[4].SetActive(false);
                            screenInput[5].SetActive(false);
                            _showed = true;
                        }
                    }
                    break;

                //second tutorial about the collision agaist a bot
                case 2:
                    {
                        if (_lenghtTutorial == 1)
                        {
                            SecondTutorial[_lenghtTutorial].SetActive(true);
                            SecondTutorial[_lenghtTutorial - 1].SetActive(false);
                            _lenghtTutorial--;

                        }
                        else if (_lenghtTutorial == 0)
                        {
                            SecondTutorial[_lenghtTutorial + 1].SetActive(false);
                            Man.GetComponent<CharacterBehaviour>().SetInput(true);
                            Woman.GetComponent<CharacterBehaviour>().SetInput(true);
                            screenInput[0].SetActive(true);
                            screenInput[1].SetActive(true);
                            screenInput[2].SetActive(false);
                            screenInput[3].SetActive(false);
                            screenInput[4].SetActive(false);
                            screenInput[5].SetActive(false);
                            _showed = true;
                        }
                    }
                    break;

                //Third tutorial about the change of the control in the explosion part
                case 3:
                    {
                        if (_lenghtTutorial == 1)
                        {
                            ThirdTutorial[_lenghtTutorial].SetActive(true);
                            ThirdTutorial[_lenghtTutorial - 1].SetActive(false);
                            _lenghtTutorial--;

                        }
                        else if (_lenghtTutorial == 0)
                        {
                            ThirdTutorial[_lenghtTutorial + 1].SetActive(false);
                            Man.GetComponent<CharacterBehaviour>().SetInput(true);
                            Woman.GetComponent<CharacterBehaviour>().SetInput(true);
                            screenInput[2].SetActive(true);
                            screenInput[3].SetActive(true);
                            screenInput[4].SetActive(false);
                            screenInput[5].SetActive(false);
                            _showed = true;
                        }
                    }
                    break;

                //Fourth tutorial about the collision agaist obstacles
                case 4:
                    {
                        if (_lenghtTutorial == 1)
                        {
                            FourthTutorial[_lenghtTutorial].SetActive(true);
                            FourthTutorial[_lenghtTutorial - 1].SetActive(false);
                            _lenghtTutorial--;

                        }
                        else if (_lenghtTutorial == 0)
                        {
                            FourthTutorial[_lenghtTutorial + 1].SetActive(false);
                            Man.GetComponent<CharacterBehaviour>().SetInput(true);
                            Woman.GetComponent<CharacterBehaviour>().SetInput(true);
                            screenInput[0].SetActive(true);
                            screenInput[1].SetActive(true);
                            screenInput[2].SetActive(false);
                            screenInput[3].SetActive(false);
                            screenInput[4].SetActive(false);
                            screenInput[5].SetActive(false);
                            _showed = true;
                        }
                    }
                    break;

                //Fifth tutorial about the collision between the two magnets
                case 5:
                    {
                        if (_lenghtTutorial == 1)
                        {
                            FifthTutorial[_lenghtTutorial].SetActive(true);
                            FifthTutorial[_lenghtTutorial - 1].SetActive(false);
                            _lenghtTutorial--;

                        }
                        else if (_lenghtTutorial == 0)
                        {
                            FifthTutorial[_lenghtTutorial + 1].SetActive(false);
                            Man.GetComponent<CharacterBehaviour>().SetInput(true);
                            Woman.GetComponent<CharacterBehaviour>().SetInput(true);
                            screenInput[0].SetActive(true);
                            screenInput[1].SetActive(true);
                            screenInput[2].SetActive(false);
                            screenInput[3].SetActive(false);
                            screenInput[4].SetActive(false);
                            screenInput[5].SetActive(false);
                            _showed = true;
                        }
                    }
                    break;

                default:
                    Debug.Log("Unknown num tutorial");
                    break;
            }
        }
    }

    // Fade in the text 
    IEnumerator FadeTextIn(Text LevelNumber, float t)
    {
        while (LevelNumber.color.a < 1.0f)
        {
            LevelNumber.color = new Color(LevelNumber.color.r, LevelNumber.color.g, LevelNumber.color.b, LevelNumber.color.a + (Time.deltaTime / t));
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(FadeTextOut(LevelNumber));
    }

    // Fade out the text l
    IEnumerator FadeTextOut(Text LevelNumber)
    {
        while (LevelNumber.color.a > 0.0f)
        {
            LevelNumber.color = new Color(LevelNumber.color.r, LevelNumber.color.g, LevelNumber.color.b, LevelNumber.color.a - (Time.deltaTime));
            yield return null;
        }
    }

    //Show the FIRST and the SECOND TUTORIAL
    IEnumerator ShowTutorial(GameObject[] tutorial, GameObject effect)
    {
        Man.GetComponent<CharacterBehaviour>().SetInput(false);
        Woman.GetComponent<CharacterBehaviour>().SetInput(false);

        if (_numTutorial != 0 && !_explosion)
        {
            BlackBackground.SetActive(true);
            tutorial[0].SetActive(true);
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.2f));

            //Change input for tutorial
            screenInput[0].SetActive(false);
            screenInput[1].SetActive(false);
            screenInput[2].SetActive(false);
            screenInput[3].SetActive(false);
            screenInput[4].SetActive(true); //set Tutorial
            screenInput[5].SetActive(true); //set Tutorial
        }

        StopCoroutine("ShowTutorial");
        _nextImage = true;
    }

    //Show the THIRD TUTORIAL
    IEnumerator ShowExplosionTutorial(GameObject[] tutorial, GameObject effect)
    {
        Man.GetComponent<CharacterBehaviour>().SetInput(false);
        Woman.GetComponent<CharacterBehaviour>().SetInput(false);
        effect.SetActive(true);

        yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(1f));

        for (int i = 0; i < RedLight.Length; i++)
        {
            RedLight[i].color = Color.red;
        }

        yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(2.3f));
        effect.SetActive(false);

        if (_numTutorial != 0) //in every explosion with tutorial
        {
            BlackBackground.SetActive(true);
            tutorial[0].SetActive(true);
            tutorial[1].SetActive(false);
            //Change input for tutorial
            screenInput[0].SetActive(false);
            screenInput[1].SetActive(false);
            screenInput[2].SetActive(false);
            screenInput[3].SetActive(false);
            screenInput[4].SetActive(true); //set Tutorial
            screenInput[5].SetActive(true); //set Tutorial

        }
        else if (_numExplosion == 0) //at the start of the explosion without tutorial
        {
            screenInput[0].SetActive(false);
            screenInput[1].SetActive(false);
            screenInput[2].SetActive(true); //set Explosion control
            screenInput[3].SetActive(true); //set Explosion control
            screenInput[4].SetActive(false);
            screenInput[5].SetActive(false);
            Man.GetComponent<CharacterBehaviour>().SetInput(true);
            Woman.GetComponent<CharacterBehaviour>().SetInput(true);
            _showed = true;
        }
        else if (_numExplosion == 1) //at the end of the explosion without tutorial
        {
            RedLight[_lenghtRedLight].color = Color.grey;
            screenInput[0].SetActive(true);
            screenInput[1].SetActive(true);
            screenInput[2].SetActive(false);
            screenInput[3].SetActive(false);
            screenInput[4].SetActive(false);
            screenInput[5].SetActive(false);
            Man.GetComponent<CharacterBehaviour>().SetInput(true);
            Woman.GetComponent<CharacterBehaviour>().SetInput(true);
            _showed = true;
        }

        StopCoroutine("ShowExplosionTutorial");
        _nextImage = true;
    }

    static class CoroutineUtilities
    {
        public static IEnumerator WaitForRealTime(float timeS)
        {
            while (true)
            {
                float pauseEndTime = Time.realtimeSinceStartup + timeS;
                while (Time.realtimeSinceStartup < pauseEndTime)
                {
                    yield return 0;
                }
                break;
            }
        }
    }
}
