using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Indicator : MonoBehaviour
{
    private ARRaycastManager rayMan;
    private GameObject indicator;

    void Start()
    {
        // Cargar los componentes
        rayMan = FindObjectOfType<ARRaycastManager>();
        indicator = transform.GetChild(0).gameObject;

        // Ocultar el indicador
        indicator.SetActive(false);
    }

    void Update()
    {
        // Lanzar un raycast al centro de la pantalla
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayMan.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        // Cuando un rayo toca el plano del suelo, actualizar la posición y rotación
        if(hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            if(!indicator.activeInHierarchy)
                indicator.SetActive(true);
        }
    }
}
