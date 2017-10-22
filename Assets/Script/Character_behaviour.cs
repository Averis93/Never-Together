using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_behaviour : MonoBehaviour {

    public float VerticalSpeed;
    public float amplitude;
    public Transform PointCenter;
    public Transform Character;

    public bool JumpedUp = false;
    public bool JumpedDown = false;
    public bool changePos = false;

    public Vector3 tempPosition;

    // Public Variables
    public Vector3 newPosition_UP; // The target position
    public Vector3 oldPosition_DOWN; // The target position

    float index;

    // Private Variables
    private Transform trans;	// Will hold this.transform.

    void Start()
    {
        tempPosition = PointCenter.position;
        newPosition_UP = PointCenter.position;
        oldPosition_DOWN = Character.position;
    }

    void Awake()
    {
        trans = transform;
    }

    void Update()
    {
        Rigidbody2D rbWoman = GetComponent<Rigidbody2D>();

        if (Character.tag == "Woman" && Input.GetKeyDown("a"))
        {
            if (!JumpedDown)
            {
                Debug.Log("Entrato in !JumpedDown falso ed inizia a ondeggiare");
                changePos = true;
                JumpedUp = true;
            }
            else
            {
                Debug.Log("Entrato in JumpedDown vero smette di ondeggiare");
                JumpedUp = false; //impedisce di entrare nell'if dell'ondeggio
                JumpedDown = false; //permette di rieseguire il salto
                changePos = false; //dovrebbe permettere di tornare a terra
            }
        }

        if (Character.tag == "Man" && Input.GetKeyDown("d"))
        {
            if (!JumpedDown)
            {
                Debug.Log("Entrato in !JumpedDown falso ed inizia a ondeggiare");
                changePos = true;
                JumpedUp = true;
            }
            else
            {
                Debug.Log("Entrato in JumpedDown vero smette di ondeggiare");
                JumpedUp = false; //impedisce di entrare nell'if dell'ondeggio
                JumpedDown = false; //permette di rieseguire il salto
                changePos = false; //dovrebbe permettere di tornare a terra
            }
        }

        //posizionamento al centro delle due calamitine
        if (changePos)
        {
            Debug.Log("Entrato in changePos UP");
            trans.position = Vector3.Lerp(trans.position, newPosition_UP, Time.deltaTime * 4f);

            if (Mathf.Abs(newPosition_UP.y - trans.position.y) < 0.02)
            {
                trans.position = newPosition_UP;
                Debug.Log(trans.position);
            }
        }//fa tornare sulla rispettiva superficie la calamita 
        else if(!changePos)
        {
            Debug.Log("Entrato in changePos DOWN");
            trans.position = Vector3.Lerp(trans.position, oldPosition_DOWN, Time.deltaTime * 4f);

            if (Mathf.Abs(oldPosition_DOWN.y - trans.position.y) < 0.02)
            {
                trans.position = oldPosition_DOWN;
                Debug.Log(trans.position);
            }
        }

        //esegue l'ondeggio
        if (JumpedUp)
        {
            Debug.Log("Entrato in JumpedUP ed esegue ondeggio");
            Debug.Log(tempPosition);
            index += Time.deltaTime;
            float y = Mathf.Abs(VerticalSpeed * Mathf.Sin(amplitude * index));
            transform.localPosition += new Vector3(0, y, 0);
            JumpedDown = true;
        }
    }
}
