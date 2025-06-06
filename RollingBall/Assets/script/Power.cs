using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Power : MonoBehaviour
{
    public float powerPoint = 1;
 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {

            Rigidbody enemy = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 CollisionPoint = collision.contacts[0].point;

            Vector3 disrection = (CollisionPoint - transform.position).normalized;

            enemy.AddForce(disrection * powerPoint, ForceMode.Impulse);

        }
    }
}
