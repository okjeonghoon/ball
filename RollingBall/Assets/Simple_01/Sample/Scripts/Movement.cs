using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rigid;
    public float moveSpeed;
    public float maxSpeed;


    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal"); // a,d Ű (x�� �̵�)
        float vertical = Input.GetAxis("Vertical");     // w,s Ű (z�� �̵�)

       


        if (rigid.velocity.magnitude < maxSpeed)
        {
            rigid.AddForce(new Vector3(horizontal, 0, vertical) * moveSpeed, ForceMode.Impulse);
        }

        if(rigid.velocity.magnitude > maxSpeed)
        {
            rigid.velocity = rigid.velocity.normalized * maxSpeed;
        }
    }
}
