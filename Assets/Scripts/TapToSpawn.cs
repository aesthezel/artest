using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToSpawn : MonoBehaviour
{

    private ARRaycastManager arMan; // Origen de la sesión de conexión de la cámara y motor con el entorno AR
    private Pose locPose; // Estructura que describe la posición y rotación de un elemento en el espacio AR
    private bool locPoseIsValid = false;
    
    public GameObject indicator;
    public GameObject objectToSpawn;

    void Start()
    {
        arMan = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLocationPose(); // Actualiza la posición de dónde caerá el objeto
        UpdateLocationIndicator(); // Actualiza y muestra el indicador cuando es debido

        if (locPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            SpawnObject();
        }
    }

    private void UpdateLocationPose()
    {
        var screenPoint = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f)); // Area de contacto y sensibilidad
        var hits = new List<ARRaycastHit>(); // Toques de la pantalla
        arMan.Raycast(screenPoint, hits, TrackableType.Planes); // Manda un punto en cualquier parte de la pantalla a las superficies del supuesto mundo real

        locPoseIsValid = hits.Count > 0;
        if (locPoseIsValid)
        {
            locPose = hits[0].pose;
            var cameraFwd = Camera.current.transform.forward;
            var cameraNewAngle = new Vector3(cameraFwd.x, 0, cameraFwd.z).normalized;
            locPose.rotation = Quaternion.LookRotation(cameraNewAngle);
        }
    }

    private void UpdateLocationIndicator()
    {
        if (locPoseIsValid)
        {
            indicator.SetActive(true);
            indicator.transform.SetPositionAndRotation(locPose.position, locPose.rotation);
        }
        else
        {
            indicator.SetActive(false);
        }
    }

    private void SpawnObject()
    {
        Instantiate(objectToSpawn, locPose.position, locPose.rotation);
    }

}
