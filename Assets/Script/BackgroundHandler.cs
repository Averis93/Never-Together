using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundHandler : MonoBehaviour {

    public GameObject BackgroundPrefab;
    public GameObject Player;

    List<GameObject> spawnedBackgrounds = new List<GameObject>();

    int spawnIndex = 2;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < spawnIndex; i++)
        {
            spawnedBackgrounds.Add(
                (GameObject)Instantiate(BackgroundPrefab, new Vector3(i * 54.2f - 27.1f, -0.04f, 0), Quaternion.identity)
                );
        }
    }

    void Update()
    {
        bool addNewBackground = false;
        foreach (GameObject bgObject in spawnedBackgrounds)
        {
            if (bgObject != null)
            {
                if (bgObject.activeInHierarchy)
                {
                    float PosbgObject = bgObject.transform.position.x;
                    float PosPlayer = Player.transform.position.x;
                    var distanceFromPlayer = PosPlayer - PosbgObject;

                    if (distanceFromPlayer > 51.2f)
                    {
                        Destroy(bgObject);
                        bgObject.SetActive(false);
                        addNewBackground = true;
                    }
                }
            }
        }

        if (addNewBackground)
        {
            spawnedBackgrounds.Add(
                (GameObject)Instantiate(BackgroundPrefab,
                                        new Vector3(spawnIndex * 51.2f - 25.6f, 0, 0.01f),
                                        Quaternion.identity));
            spawnIndex++;
        }
    }
}
