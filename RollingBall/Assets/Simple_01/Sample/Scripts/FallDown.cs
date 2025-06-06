using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour
{
    public Spawner spawner;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }

        if(collision.collider.CompareTag("Enemy"))
        {
            spawner.RemoveMonster(collision.gameObject.GetComponent<Trace>());
            Destroy(collision.gameObject);
        }
    }
}
