using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour {

    public float VerticalSpeed;
    public float Amplitude;
    public Transform PointCenter;
    public Transform Character;
    public GameObject Floor;

    public bool JumpedUp;
    public bool ChangePos;

    public Vector3 TempPosition;

    // Public Variables
    public Vector3 NewPositionUp; // The target position
    public Vector3 OldPositionDown; // The target position

    public float Index;

    // Private Variables
    private Transform _trans;	// Will hold this.transform.

    private bool _inputAllowed;

    void Start()
    {
        TempPosition = PointCenter.position;
        NewPositionUp = PointCenter.position;
        OldPositionDown = Character.position;
        JumpedUp = false;
        ChangePos = false;
        _inputAllowed = true;
    }

    void Awake()
    {
        _trans = transform;
    }

    void Update()
    {

        if (Character.CompareTag("Woman") && Input.GetKeyDown("a") || Character.CompareTag("Man") && Input.GetKeyDown("d"))
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
        else if(!ChangePos)
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

    // Pick up coins
    void OnTriggerEnter2D(Collider2D other)
    {
        // If the object with which the magnets collide is a coin
        if (other.gameObject.CompareTag("Coin"))
        {
            // Make the coin disappear
            other.gameObject.SetActive(false);
            
            // Increment the coins count
            transform.parent.GetComponent<MagnetsController>().SetCount();
            transform.parent.GetComponent<MagnetsController>().SetCountText();
        }
        // If the object with which the magnets collide is a tree
        else if (other.gameObject.CompareTag("Branch"))
        {
            transform.parent.GetComponent<MagnetsController>().RemoveLife();
        }
    }

    // Go up or down
    void ChangePosition(Vector3 newPosition)
    {
        _trans.position = Vector3.Lerp(_trans.position, newPosition, Time.deltaTime * 4f);

        if (Mathf.Abs(newPosition.y - _trans.position.y) < 0.02)
        {
            _trans.position = newPosition;
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

    public void SetInput(bool input)
    {
        _inputAllowed = input;
    }
}
