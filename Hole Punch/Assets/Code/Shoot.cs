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
    public static bool fireAmmo;
    public KeyCode shoot;


    // Start is called before the first frame update
    void Start()
    {
        //folder where assets are 
        //prefab is an instance that is not in the scene 
        //puts the bullet in the scene to make as many as you want
        prefab = Resources.Load("bullet") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //key left click
        if(Input.GetKeyDown(shoot) && fireAmmo == true)
        {
            //makes the object and positions it inside the barrel with transform and rotation of the camera
            GameObject instBullet = Instantiate(prefab, transform.position+Camera.main.transform.forward*10+Camera.main.transform.up*3/2, Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z)) as GameObject;
            //make bullet interact with physics
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
            //Gets rotation of camera to make sure there's no x and y movement and only forwaerd movement 
            instBulletRigidbody.velocity = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z) * new Vector3(0, 0, speed);
            //instBulletRigidbody.AddForce(Vector3.left * speed);
            //Camera.main.transform.forward * speed
            
        }
}
}
