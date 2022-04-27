using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGenerator : MonoBehaviour
{
    [Header("Pickup Zone")]
    public GameObject[] pickups = new GameObject[3];
    public Vector3 size;
    public Vector3 offset;


    public void GeneratePickup()
    {
        int randomPickup = Random.Range(0, 3);
        Vector3 randomLocation = transform.position + offset + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));

        Instantiate(pickups[randomPickup], randomLocation, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + offset, size);
    }
}
