using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Set position to follow player without rotation.
public class PlayerCarryHud : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;


    private void Awake()
    {
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
