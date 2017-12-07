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

    private bool isFreeze;
    private bool showed;

    // Use this for initialization
    void Start () {
        isFreeze = false;
        showed = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(isFreeze)
        {
            Time.timeScale = 0f;
        }

        if (showed)
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
            if (!isFreeze && !showed)
            {
                isFreeze = true;
                StartCoroutine(ShowTutorial(FirstTutorial));
            }
        }
        else if (other.gameObject.CompareTag("SecondTutorial"))
        {
            if (!isFreeze && !showed)
            {
                isFreeze = true;
                StartCoroutine(ShowTutorial(SecondTutorial));
            }
            
        }
        else if (other.gameObject.CompareTag("ThirdTutorial"))
        {
            if (!isFreeze && !showed)
            {
                isFreeze = true;
                StartCoroutine(ShowTutorial(ThirdTutorial));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FirstTutorial"))
        {
            isFreeze = false;
            showed = false;
        }
        else if (other.gameObject.CompareTag("SecondTutorial"))
        {
            isFreeze = false;
            showed = false;

        }
        else if (other.gameObject.CompareTag("ThirdTutorial"))
        {
            isFreeze = false;
            showed = false;
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
        showed = true;
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
