using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebugCameraMovement : MonoBehaviour {
    public float movementSpeed = 5.0f;
    //public float yRotation = 0.0F;
    //public float ForwardBack = 0.0f;
    //public float LeftRight = 0.0f;
    public Camera CenterEyeAnchor;
    public Vector3 OriginLocation;
    public Quaternion OriginRotation;
    // Use this for initialization
    void Start () {
        OriginLocation = transform.position;
        OriginRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            //transform.position = new Vector3(0, 0, ForwardBack);
            //ForwardBack += 0.1f;
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.position = new Vector3(0, 0, ForwardBack);
            //ForwardBack -= 0.1f;
            transform.position -= transform.forward * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -0.5f, 0);
            //LeftRight = 5.0f;
            //transform.Rotate(transform.position.z * Time.deltaTime * 8.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0.5f, 0);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position -= transform.up * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += transform.up * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.PageUp))
        {
            transform.Rotate(-0.5f, 0, 0);
        }
        if (Input.GetKey(KeyCode.PageDown))
        {
            transform.Rotate(0.5f, 0, 0);
        }
        if (Input.GetKey(KeyCode.Home))
        {
            transform.position = OriginLocation;
            transform.rotation = OriginRotation;
        }
    }
}
