using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SokoBlock : MonoBehaviour {

    public GameObject player;
    private Rigidbody rb;

    private Vector3 xDir = Vector3.zero;
    private Vector3 zDir = Vector3.zero;

    public float pushingSpeed = 1.0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public void Pushed()
    {
        if (transform.position.x - player.transform.position.x > transform.position.z - player.transform.position.z)
        {
            xDir = new Vector3(transform.position.x - player.transform.position.x, 0, 0);
            rb.AddForce(xDir, ForceMode.Force);
        }
        else
        {
            zDir = new Vector3(0, 0, transform.position.z - player.transform.position.z);
            rb.AddForce(zDir, ForceMode.Force);
        }

    }
}
