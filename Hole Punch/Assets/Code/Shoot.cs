using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using UnityEngine;

public class Shoot: MonoBehaviour
{

    GameObject prefab;
    public float speed = 100f;

    public KeyCode shoot;

    // Start is called before the first frame update
    void Start()
    {
        prefab = Resources.Load("bullet") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(shoot))
        {
            GameObject instBullet = Instantiate(prefab, transform.position+Camera.main.transform.forward*5+Camera.main.transform.up*3/2, Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z)) as GameObject;
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
            instBulletRigidbody.velocity = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z) * new Vector3(0, 0, speed);
            //instBulletRigidbody.AddForce(Vector3.left * speed);
            //Camera.main.transform.forward * speed
        }
    }
}
