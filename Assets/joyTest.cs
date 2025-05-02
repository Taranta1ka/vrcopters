using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joyTest : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform tr;
    void Start()
    {
        tr=GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        tr.position= new Vector3(h, v, 0);
    }
}
