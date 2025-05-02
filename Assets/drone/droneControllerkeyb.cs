using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class droneControllerkeyb : MonoBehaviour
{
    private Rigidbody rb;
    private Transform tr;
    public float maxSpeed=15f;
    [SerializeField] private float throttle=0f;
    private float accelerationthr=0.01f;
    public float rotateSpeed=0.2f;
    public float droneForce=3f;
    private bool isUp = false;
    private bool isDown = false;
    private bool isRight = false;
    private bool isLeft = false;
    private bool isPravo = false;
    private bool isLevo = false;
    public bool isAcro=false;
    public static bool stabilized=false;
    [SerializeField] protected bool isArmed=false;
    private bool isthr=false;
    private bool isdwn=false;
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
        if(PlayerPrefs.GetInt("ControlMode")==1)
        {
            Destroy(gameObject.GetComponent<droneControllerkeyb>());
        }
        rb=GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
    }
    void Update()
    {
        isUp = Input.GetKey(KeyCode.UpArrow);
        isDown = Input.GetKey(KeyCode.DownArrow);
        isRight = Input.GetKey(KeyCode.RightArrow);
        isLeft = Input.GetKey(KeyCode.LeftArrow);
        isLevo = Input.GetKey("a");
        isPravo=Input.GetKey("d");
        isthr = Input.GetKey("w");
        isdwn = Input.GetKey("s");

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
        if (Input.GetKeyDown("m"))
        {
            if (isAcro)
            {
                isAcro=false;
                Debug.Log("asd");
            }
            else
            {
                isAcro=true;
                Debug.Log("asd1");
            }
        }
    }
    void FixedUpdate()
    {
        if (!isArmed) return;
        if (isthr && throttle<1)
        {
            throttle = throttle+accelerationthr;
        }
        if (isdwn && throttle>0)
        {
            throttle = throttle-accelerationthr;
        }
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        if (isUp && isArmed)
        {
            tr.Rotate(rotateSpeed*throttle,0,0);
        }
        if (isDown&&isArmed)
        {
            tr.Rotate(-rotateSpeed*throttle, 0, 0);
        }
        if (isRight&&isArmed)
        {
            tr.Rotate(0,0,-rotateSpeed*throttle);
        }
        if (isLeft&&isArmed)
        {
            tr.Rotate(0,0,rotateSpeed*throttle);
        }
        if (isLevo&&isArmed)
        {
            tr.Rotate(0,-rotateSpeed*throttle,0);
        }
        if (isPravo&&isArmed)
        {
            tr.Rotate(0, rotateSpeed*throttle, 0);
        }
        if (!isthr&&!isdwn&&!isUp && !isDown && !isLeft && !isRight&&!stabilized&&isAcro)
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
    }
}

