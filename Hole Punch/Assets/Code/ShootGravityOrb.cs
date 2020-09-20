using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using UnityEngine;

public class ShootGravityOrb : MonoBehaviour
{

    public GameObject prefab;
    private float speed = 30f;

    private bool shot;

    private Rigidbody instBulletRigidbody;


    // Start is called before the first frame update
    void Start()
    {
        shot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && shot == false)
        {
            GameObject instBullet = Instantiate(prefab, transform.position + Camera.main.transform.forward * 5 + Camera.main.transform.up * 3 / 2, Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z)) as GameObject;
            instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
            instBulletRigidbody.velocity = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z) * new Vector3(0, 0, speed);
            //instBulletRigidbody.AddForce(Vector3.left * speed);
            //Camera.main.transform.forward * speed
            shot = true;
            
        }
        else if (Input.GetKeyDown(KeyCode.F) && shot == true)
        {
            //stops bullet if F is press again
            instBulletRigidbody.velocity = instBulletRigidbody.velocity = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z) * new Vector3(0, 0, 0); ;
            shot = false;
        }

    }
}
