using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Movement>().maxSpeed *= 2;
            other.gameObject.GetComponent<Power>().powerPoint = 20;

            Destroy(gameObject);

        }
    }
}
