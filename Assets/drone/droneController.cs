using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class droneController : MonoBehaviour
{
    private Rigidbody rb;
    public float maxSpeed=15f;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            //rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
