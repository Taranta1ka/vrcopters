using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class droneControllergamep : MonoBehaviour
{
    private Rigidbody rb;
    private Transform tr;
    public float maxSpeed=15f;
    [SerializeField] private float throttle=0f;
    private float accelerationthr=0.01f;
    public float rotateSpeed=0.2f;
    public float droneForce=3f;
    public bool isAcro=false;
    public bool isArmed=false;
    IEnumerator Waitforsmth()
    {

        yield return new WaitForSeconds(10);
        Debug.Log("das2");
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("das");
        StartCoroutine("Waitforsmth");

    }
    void Start()
    {
        if(PlayerPrefs.GetInt("ControlMode")==0)
        {
            Destroy(gameObject.GetComponent<droneControllergamep>());
        }
        rb=GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
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
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            if (isAcro)
            {
                isAcro=false;
                Debug.Log("neacro");
            }
            else
            {
                isAcro=true;
                Debug.Log("acro");
            }
        }
    }
    void FixedUpdate()
    {
        if (!isArmed) return;
        throttle=(Input.GetAxis("Vertical")+1f)/2f;
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        tr.Rotate(0, rotateSpeed*throttle*Input.GetAxis("Horizontal"), 0);
        tr.Rotate(rotateSpeed*throttle*Input.GetAxis("Look Y"),0,0);
        tr.Rotate(0,0,-rotateSpeed*throttle*Input.GetAxis("Look X"));
        if (isAcro&&Input.GetAxis("Look X")==0&&Input.GetAxis("Look Y")==0&&Input.GetAxis("Vertical")==0)
        {
            Quaternion target = Quaternion.Euler(0, tr.eulerAngles.y, 0);
            tr.rotation=Quaternion.Slerp(tr.rotation, target, 3.0f*Time.deltaTime);
            rb.velocity= new Vector3(0,0,0);
            Vector3 localUpForce = -1 * rb.mass * Physics.gravity;
            rb.AddForce(localUpForce, ForceMode.Force);
        }
        else
        {
            Vector3 localUpForce = transform.up * droneForce* 0.1f*throttle;
            rb.AddForce(localUpForce, ForceMode.Force);
        }
        Debug.Log(Input.GetAxis("Look X")==0);
        Debug.Log(Input.GetAxis("Look Y")==0);
        Debug.Log(Input.GetAxis("Vertical")==0);
    }
}

