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
        float horizontal = Input.GetAxis("Horizontal"); // a,d 키 (x축 이동)
        float vertical = Input.GetAxis("Vertical");     // w,s 키 (z축 이동)

       


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
