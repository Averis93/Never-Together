using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChristmasLight : MonoBehaviour {
    
    private Color[] colours = new Color[7];

    void Start()
    {
        colours[0] = Color.blue;
        colours[1] = Color.cyan;
        colours[2] = Color.green;
        colours[4] = Color.magenta;
        colours[5] = Color.red;
        colours[6] = Color.yellow;
        StartCoroutine("ChangeColour", 1f);
    }

    void Update()
    {
            //StartCoroutine("ChangeColour", 3f);
            //InvokeRepeating("ChangeColour", time, repeatRate);
    }

    IEnumerator ChangeColour()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            gameObject.GetComponent<Light>().color = colours[Random.Range(0, colours.Length - 1)];
        }
    }
}
