using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseGame : MonoBehaviour {

    //public GameObject PausePanel;
    //public AudioMixerSnapshot Paused;
    //public AudioMixerSnapshot Unpaused;
    //public ApplicationManager appman;
    public bool isPaused;

    // Use this for initialization
    void Start()
    {
        isPaused = false;
    }

    /*void Lowpass()
    {
        if (Time.timeScale == 0)
        {
            Paused.TransitionTo(.01f);
        }
        else
        {
            Unpaused.TransitionTo(.01f);
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            PauseGame(true);
        }
        else
        {
            PauseGame(false);
        }

        /*if (Input.GetKeyDown("p")){
            switchPause();
        }**/

        if (Input.GetKeyDown ("p"))
        {
            if (isPaused == false)
            {
                isPaused = true;
            }
            else
            {
                isPaused = false;
            }
        }
    }

    void PauseGame(bool state)
    {
        if (state)
        {
            Time.timeScale = 0.0f;
            //Attack.GetComponent<Button>().enabled = false;
            /*if (appman.attack)
            {
                appman.AttackWindow.SetActive(false);
            }
            else if (appman.inventory)
            {
                appman.InventoryWindow.SetActive(false);
            }*/
        }
        else
        {
            Time.timeScale = 1.0f;
            //Attack.GetComponent<Button>().enabled = true;
            //Inventory.GetComponent<Button>().enabled = true;
            //Escape.GetComponent<Button>().enabled = true;
        }
        //PausePanel.SetActive(state);
        //Lowpass();
    }
}
