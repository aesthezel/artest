using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] buildings;
    private Indicator spawnMark;

    void Start()
    {
        spawnMark = FindObjectOfType<Indicator>();
    }

    void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            GameObject obj = Instantiate(buildings[Random.Range(0, buildings.Length)], spawnMark.transform.position, spawnMark.transform.rotation);
        }
    }

}
