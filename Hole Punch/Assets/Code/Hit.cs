using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{

    public Rigidbody rb;

    public float duration;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Destroy(gameObject, duration);
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        // Rotate the object so that the y-axis faces along the normal of the surface
        Destroy(gameObject);
    }
}
