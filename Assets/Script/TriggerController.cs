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

    [Header("Effect")]
    public GameObject ExplosionEffect;

    [Header("Light")]
    public Light[] RedLight;

    [Header("Screen Inputs")]
    public GameObject[] screenInput;

    private bool _isFreeze;
    private bool _showed;

    // Use this for initialization
    void Start () {
        _isFreeze = false;
        _showed = false;
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
            if (!_isFreeze && !_showed)
            {
                _isFreeze = true;
                StartCoroutine(ShowTutorial(FirstTutorial));
            }
        }
        else if (other.gameObject.CompareTag("SecondTutorial"))
        {
            if (!_isFreeze && !_showed)
            {
                _isFreeze = true;
                StartCoroutine(ShowTutorial(SecondTutorial));
            }
            
        }
        else if (other.gameObject.CompareTag("ThirdTutorial"))
        {
            if (!_isFreeze && !_showed)
            {
                _isFreeze = true;
                StartCoroutine(ShowExplosionTutorial(ThirdTutorial, ExplosionEffect));
            }
        }
        else if (other.gameObject.CompareTag("EndExplosionEffect"))
        {
            screenInput[0].SetActive(true);
            screenInput[1].SetActive(true);
            screenInput[2].SetActive(false);
            screenInput[3].SetActive(false);
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
    }

    // Fade in the text 'Level n' at the beginning of a level
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

    // Fade out the text 'Level n' at the beginning of a level
    IEnumerator FadeTextOut()
    {
        while (LevelNumber.color.a > 0.0f)
        {
            LevelNumber.color = new Color(LevelNumber.color.r, LevelNumber.color.g, LevelNumber.color.b, LevelNumber.color.a - (Time.deltaTime));
            yield return null;
        }
    }

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
        tutorial[0].SetActive(true);
        tutorial[1].SetActive(false);
        yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(2f));
        tutorial[0].SetActive(false);
        tutorial[1].SetActive(true);
        yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(2f));
        tutorial[1].SetActive(false);

        //change the screen input
        screenInput[0].SetActive(false);
        screenInput[1].SetActive(false);
        screenInput[2].SetActive(true);
        screenInput[3].SetActive(true);

        Man.GetComponent<CharacterBehaviour>().SetInput(true);
        Woman.GetComponent<CharacterBehaviour>().SetInput(true);
        StopCoroutine("ShowExplosionTutorial");
        _showed = true;
    }

    IEnumerator ShowTutorial(GameObject[] tutorial)
    {
        Man.GetComponent<CharacterBehaviour>().SetInput(false);
        Woman.GetComponent<CharacterBehaviour>().SetInput(false);
        tutorial[0].SetActive(true);
        tutorial[1].SetActive(false);
        yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(2f));
        tutorial[0].SetActive(false);
        tutorial[1].SetActive(true);
        yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(2f));
        tutorial[1].SetActive(false);
        Man.GetComponent<CharacterBehaviour>().SetInput(true);
        Woman.GetComponent<CharacterBehaviour>().SetInput(true);
        StopCoroutine("ShowTutorial");
        _showed = true;
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
