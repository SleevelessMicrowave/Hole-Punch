using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    //interact w physics
    public Rigidbody rb;

    public float duration;

    void Start()
    {
        //get rigidbody component of object
        rb = gameObject.GetComponent<Rigidbody>();
        //destroys bullet after 30 sec
        Destroy(gameObject, duration);
    }

    void Update()
    {
        //rotation of the bullet to face way it's shooting
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

//when two objects collife and takees a collision arghument
    void OnCollisionEnter(Collision collision)
    {
        //first contact point
        ContactPoint contact = collision.contacts[0];

        // Rotate the object so that the y-axis faces along the normal of the surface
        //destroy after collision
        Destroy(gameObject);
    }
}
