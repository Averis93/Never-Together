using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriggerController : MonoBehaviour {

    public Text LevelNumber;

    [Header("Characters")]
    public GameObject Man;
    public GameObject Woman;

    [Header("Tutorial Images")]
    public GameObject[] FirstTutorial;
    public GameObject[] SecondTutorial;
    public GameObject[] ThirdTutorial;
    public GameObject[] FourthTutorial;

    [Header("Effect")]
    public GameObject ExplosionEffect;
    public GameObject EndExplosionEffect;

    [Header("Light")]
    public Light[] RedLight;

    [Header("Screen Inputs")]
    public GameObject[] screenInput;

    public bool _isFreeze;
    public bool _showed;
    private int _lenghtTutorial;
    private int _numTutorial;
    public bool _explosion;

    private bool _nextImage;

    // Use this for initialization
    void Start () {
        _isFreeze = false;
        _showed = false;
        _nextImage = false;
        _explosion = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(_isFreeze)
        {
            Time.timeScale = 0f;
        }

        if (_showed)
        {
            Time.timeScale = 1f;
        }
    }
    

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("StartLevel"))
        {
            StartCoroutine(FadeTextIn(0.7f));
        }
        else if (other.gameObject.CompareTag("FirstTutorial"))
        {
            _numTutorial = 1;
            _lenghtTutorial = FirstTutorial.Length - 1;

            if (!_isFreeze && !_showed)
            {
                _isFreeze = true;
                StartCoroutine(ShowTutorial(FirstTutorial, null));
            }
        }
        else if (other.gameObject.CompareTag("SecondTutorial"))
        {
            _numTutorial = 2;
            _lenghtTutorial = SecondTutorial.Length - 1;

            if (!_isFreeze && !_showed)
            {
                _isFreeze = true;
                StartCoroutine(ShowTutorial(SecondTutorial, null));
            }
            
        }
        else if (other.gameObject.CompareTag("ThirdTutorial"))
        {
            _numTutorial = 3;
            _lenghtTutorial = ThirdTutorial.Length - 1;

            if (!_isFreeze && !_showed)
            {
                _isFreeze = true;
                StartCoroutine(ShowExplosionTutorial(ThirdTutorial, ExplosionEffect));
            }
        }
        else if (other.gameObject.CompareTag("EndExplosionEffect"))
        {
            _numTutorial = 1;
            _lenghtTutorial = FirstTutorial.Length - 1;

            if (!_isFreeze && !_showed)
            {
                Debug.Log(_numTutorial);
                _isFreeze = true;
                StartCoroutine(ShowTutorial(FirstTutorial, EndExplosionEffect));
            }
        }
        else if (other.gameObject.CompareTag("FourthTutorial"))
        {
            _numTutorial = 4;
            _lenghtTutorial = FourthTutorial.Length - 1;

            if (!_isFreeze && !_showed)
            {
                _isFreeze = true;
                StartCoroutine(ShowTutorial(FourthTutorial, null));
            }
        }
        else if (other.gameObject.CompareTag("Explosion"))
        {
            _numTutorial = 0;
            _explosion = true;

            if (!_isFreeze && !_showed)
            {
                _isFreeze = true;
                StartCoroutine(ShowExplosionTutorial(null, ExplosionEffect));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FirstTutorial"))
        {
            _isFreeze = false;
            _showed = false;
        }
        else if (other.gameObject.CompareTag("SecondTutorial"))
        {
            _isFreeze = false;
            _showed = false;

        }
        else if (other.gameObject.CompareTag("ThirdTutorial"))
        {
            _isFreeze = false;
            _showed = false;
        }
        else if (other.gameObject.CompareTag("FourthTutorial"))
        {
            _isFreeze = false;
            _showed = false;
        }
        else if (other.gameObject.CompareTag("EndExplosionEffect"))
        {
            _isFreeze = false;
            _showed = false;
            _explosion = false;
        }
        else if (other.gameObject.CompareTag("Explosion"))
        {
            _isFreeze = false;
            _showed = false;
        }
    }

    //Show the several image onClick based on the specific tutorial
    public void NextImageTutorial()
    {
        if (_isFreeze && _nextImage)
        {
            //First tutorial about magnet's control
            if (_numTutorial == 1)
            {
                if (_lenghtTutorial == 3)
                {
                    FirstTutorial[_lenghtTutorial - 2].SetActive(true);
                    FirstTutorial[_lenghtTutorial - 3].SetActive(false);
                    _lenghtTutorial--;
                }
                else if(_lenghtTutorial == 2)
                {
                    FirstTutorial[_lenghtTutorial].SetActive(true);
                    FirstTutorial[_lenghtTutorial - 1].SetActive(false);
                    _lenghtTutorial--;
                }
                else if(_lenghtTutorial == 1)
                {
                    FirstTutorial[_lenghtTutorial + 2].SetActive(true);
                    FirstTutorial[_lenghtTutorial + 1].SetActive(false);
                    _lenghtTutorial--;
                }
                else if(_lenghtTutorial == 0)
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
            //second tutorial about the collision agaist a bot
            else if (_numTutorial == 2)
            {
                if (_lenghtTutorial == 1)
                {
                    SecondTutorial[_lenghtTutorial].SetActive(true);
                    SecondTutorial[_lenghtTutorial - 1].SetActive(false);
                    _lenghtTutorial--;

                }else if(_lenghtTutorial == 0)
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
            //Third tutorial about the change of the control in the explosion part
            else if (_numTutorial == 3)
            {
                if (_lenghtTutorial == 1)
                {
                    ThirdTutorial[_lenghtTutorial].SetActive(true);
                    ThirdTutorial[_lenghtTutorial - 1].SetActive(false);
                    _lenghtTutorial--;
                    Debug.Log(_lenghtTutorial);

                }
                else if(_lenghtTutorial == 0)
                {
                    Debug.Log(_lenghtTutorial);
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
            //Fourth tutorial about the collision agaist obstacles
            else if (_numTutorial == 4)
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
        }
    }

    // Fade in the text 
    IEnumerator FadeTextIn(float t)
    {
        while (LevelNumber.color.a < 1.0f)
        {
            LevelNumber.color = new Color(LevelNumber.color.r, LevelNumber.color.g, LevelNumber.color.b, LevelNumber.color.a + (Time.deltaTime / t));
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(FadeTextOut());
    }

    // Fade out the text l
    IEnumerator FadeTextOut()
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
            tutorial[0].SetActive(true);
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(1f));

            //Change input for tutorial
            screenInput[0].SetActive(false);
            screenInput[1].SetActive(false);
            screenInput[2].SetActive(false);
            screenInput[3].SetActive(false);
            screenInput[4].SetActive(true); //set Tutorial
            screenInput[5].SetActive(true); //set Tutorial
        }else if (_explosion)
        {
            effect.SetActive(true);
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(3f));
            effect.SetActive(false);
            screenInput[0].SetActive(true); //set normal control
            screenInput[1].SetActive(true); //set normal control
            screenInput[2].SetActive(false);
            screenInput[3].SetActive(false);
            screenInput[4].SetActive(false);
            screenInput[5].SetActive(false);
            Man.GetComponent<CharacterBehaviour>().SetInput(true);
            Woman.GetComponent<CharacterBehaviour>().SetInput(true);
            _showed = true;
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

        if (_numTutorial != 0)
        {
            tutorial[0].SetActive(true);
            tutorial[1].SetActive(false);
            //Change input for tutorial
            screenInput[0].SetActive(false);
            screenInput[1].SetActive(false);
            screenInput[2].SetActive(false);
            screenInput[3].SetActive(false);
            screenInput[4].SetActive(true); //set Tutorial
            screenInput[5].SetActive(true); //set Tutorial

        } else if(_numTutorial == 0) //in case of single explosion without tutorial
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
