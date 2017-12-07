using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour {

    public float BackgroundUnion;
    //Point of restart movement
    public Transform PosInfiniteBackground;

    public GameObject[] Surfaces;
    public GameObject Man;
    public GameObject Woman;

    private CharacterBehaviour surfacesMan;
    private CharacterBehaviour surfacesWoman;
    private Vector3 StartPos;

    void Start()
    {
        surfacesMan = Man.gameObject.GetComponent<CharacterBehaviour>();
        surfacesWoman = Woman.gameObject.GetComponent<CharacterBehaviour>();
        StartPos = PosInfiniteBackground.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (surfacesMan.JumpedUp)
            Surfaces[0].SetActive(false);
        else
            Surfaces[0].SetActive(true);

        if (surfacesWoman.JumpedUp)
            Surfaces[1].SetActive(false);
        else
            Surfaces[1].SetActive(true);

        if (transform.position.x < BackgroundUnion)
            transform.position = StartPos;
	}
}
