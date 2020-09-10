using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject building;
    private Indicator spawnMark;

    void Start()
    {
        spawnMark = FindObjectOfType<Indicator>();
    }

    void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            GameObject obj = Instantiate(building, spawnMark.transform.position, spawnMark.transform.rotation);
        }
    }

}
