using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    const float G = 6.674f;

    public Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //find objects with attractor script and puts them in an array
        Attractor[] Attractors = FindObjectsOfType<Attractor>();
        //for each one of the objects in the array
        foreach (Attractor attractor in Attractors)
        {
            //attracts the other things that aren'y itself
            if (attractor != this)
            {
                Attract(attractor);
            }
        }
    }

//takes the rigid body of attractor that is not the object
    void Attract(Attractor objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;

    //distance between two attarctors
        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;
    //path to curve around the gravity orb 
        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        //direction.normalized removes the direction from it 
        Vector3 force = direction.normalized * forceMagnitude;
        //if close enough and object is not the bullet and adds force going into it
        if(distance < 15 && gameObject.name != "bullet(Clone)")
        {
            rbToAttract.AddForce(force);
        }
    }
}
