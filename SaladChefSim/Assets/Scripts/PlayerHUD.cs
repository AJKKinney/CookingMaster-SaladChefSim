using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Set position to follow player without rotation.
public class PlayerHUD : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;


    private void Awake()
    {
        offset = transform.position - player.position;
    }

    private void Update()
    {
        transform.position = player.position + offset;
    }
}
