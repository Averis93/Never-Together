using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationPowerUp : MonoBehaviour {

    public float BackgroundUnion;
    public Transform PosInfiniteSequence;
    public GameObject LightEffect;
    public GameObject[] ChoosenPowerUp;

    public float Speed;
    private Vector3 StartPos;
    private float ActualSpeed;

    void Start()
    {
        //Speed = 20;
        StartPos = PosInfiniteSequence.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Speed > 1.5)
        {
            Speed -= 4 * Time.deltaTime;
            transform.Translate((new Vector3(0, -1, 0)) * Speed * Time.deltaTime);
        }
        else
        {
            Speed = 1.5f;
            transform.Translate((new Vector3(0, -1, 0)) * Speed * Time.deltaTime);

            //effetto di luce
            LightEffect.SetActive(true);
            StartCoroutine(FadePanelIn(ChoosenPowerUp[0], 4f));
        }

        //StartCoroutine(SlowdownRotation());

        if (transform.position.y < BackgroundUnion)
            transform.position = StartPos;
    }

    IEnumerator FadePanelIn(GameObject panel, float t)
    {
        var img = panel.GetComponent<SpriteRenderer>();
        while (img.color.a < 1.0f)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + (Time.deltaTime / t));
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
    }
}
