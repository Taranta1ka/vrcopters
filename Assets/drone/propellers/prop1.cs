using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class prop1 : MonoBehaviour
{
    private Rigidbody rb;

    public float droneForce=3f;
    private float rothrottle=1f;
    public float maxSpeed=15f;
    [SerializeField] private float throttle=0f;
    private float accelerationthr=0.01f;
    [SerializeField] protected bool isArmed=false;
    private bool isthr=false;
    private bool isdwn=false;
    private bool isUp = false;
    private bool isDown = false;
    private bool isRight = false;
    private bool isLeft = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
    void Update()
    {
        isthr = Input.GetKey("w");
        isdwn = Input.GetKey("s");
        isUp = Input.GetKey(KeyCode.UpArrow);
        isDown = Input.GetKey(KeyCode.DownArrow);
        isRight = Input.GetKey(KeyCode.RightArrow);
        isLeft = Input.GetKey(KeyCode.LeftArrow);

        if (Input.GetKeyDown("r"))
        {
            if (isArmed)
            {
                isArmed=false;
                Debug.Log("asd");
            }
            else
            {
                isArmed=true;
                Debug.Log("asd1");
            }
        }
    }
    void FixedUpdate()
    {
        if (!isArmed) return;
        rothrottle=1;
        if (isthr && throttle<1)
        {
            throttle = throttle+accelerationthr;
        }
        if (isdwn && throttle>0)
        {
            throttle = throttle-accelerationthr;
        }
        if (isUp || isRight)
        {
            rothrottle = 0.95f;
        }
        if (isLeft || isDown)
        {
            rothrottle = 1.05f;
        }
        Vector3 localUpForce = transform.up * droneForce* 0.1f*throttle*rothrottle;
        rb.AddForce(localUpForce, ForceMode.Force);
        if (rb.velocity.magnitude > maxSpeed)
        {
            //rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    // Update is called once per frame
}
