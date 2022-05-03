using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform focusOne;
    public Transform focusTwo;
    public float minZoom;
    public float maxZoom;
    public float maxFocusDistance;

    private CinemachineFreeLook vCam;
    private Transform cameraTarget;
    private Vector3 startPos;
    private Camera cam;

    private void Awake()
    {
        vCam = GetComponent<CinemachineFreeLook>();
        cameraTarget = vCam.LookAt;
        startPos = cameraTarget.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float distance = Vector3.Distance(focusOne.position, focusTwo.position);

        if(distance <= maxFocusDistance)
        {
            //zoom
            vCam.m_YAxis.Value = Mathf.Lerp(minZoom, maxZoom, distance / maxFocusDistance);
        }

        //center on players
        Vector3 center = Vector3.Lerp(focusOne.position, focusTwo.position, 0.5f);
        cameraTarget.position = Vector3.Lerp(center, startPos, distance / maxFocusDistance);
    }
}
