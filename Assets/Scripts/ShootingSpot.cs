using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpot : MonoBehaviour
{
    private void Start()
    {
    }

    public Transform GetCameraPosition()
    {
        return transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Change Mesh
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            // Change Mesh
        }
    }
}
