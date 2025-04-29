using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    public float cameraSpeed;
    public float mouseSens;
    public float maxVertAngle;
    private float rotationX = 0f;
    private float rotationY = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirect= new Vector3(0,0,0);
        if (Input.GetKey(KeyCode.W)) moveDirect += transform.forward;
        if (Input.GetKey(KeyCode.S)) moveDirect -= transform.forward;
        if (Input.GetKey(KeyCode.D)) moveDirect += transform.right;
        if (Input.GetKey(KeyCode.A)) moveDirect -= transform.right;
        if (Input.GetKey(KeyCode.E)) moveDirect += Vector3.up;
        if (Input.GetKey(KeyCode.Q)) moveDirect -= Vector3.up;
        transform.position+=moveDirect*cameraSpeed*Time.deltaTime;

        if (Input.GetMouseButton(1))
        {
            rotationX+=Input.GetAxis("Mouse X")*mouseSens;
            rotationY-=Input.GetAxis("Mouse Y")*mouseSens;
            rotationY=Mathf.Clamp(rotationY, -maxVertAngle, maxVertAngle);

            transform.rotation=Quaternion.Euler(rotationY, rotationX, 0);
        }
    }
}
