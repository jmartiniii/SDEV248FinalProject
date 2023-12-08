using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Parallaxa : MonoBehaviour
{
    [SerializeField] private float parallaxEffectMultiplier;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private CinemachineVirtualCamera vCam;

    
    void Start()
    {
        //cameraTransform = Camera.main.transform;
        lastCameraPosition = vCam.transform.position;
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        Vector3 deltaMovement = vCam.transform.position - lastCameraPosition;
        transform.position += deltaMovement * parallaxEffectMultiplier;
        lastCameraPosition = vCam.transform.position;
    }
}
