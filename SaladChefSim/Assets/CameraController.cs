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
    private Vector3 startPos;

    private void Awake()
    {
        vCam = GetComponent<CinemachineFreeLook>();
        startPos = vCam.Follow.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float distance = Vector3.Distance(focusOne.position, startPos);
        float distanceTwo = Vector3.Distance(focusTwo.position, startPos);

        if(distanceTwo > distance)
        {
            distance = distanceTwo;
        }

        if(distance <= maxFocusDistance)
        {
            //zoom
            vCam.m_YAxis.Value = Mathf.Lerp(minZoom, maxZoom, distance / maxFocusDistance);
        }
    }
}
