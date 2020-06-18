using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{

    public Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        // Rotate the object so that the y-axis faces along the normal of the surface
        Destroy(gameObject);
    }
}
