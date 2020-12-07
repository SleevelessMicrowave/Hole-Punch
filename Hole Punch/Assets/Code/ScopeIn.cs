using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ScopeIn : MonoBehaviour
{
    public GameObject weaponCamera;
    public Camera mainCamera;

    public float scopedFOV = 15f;
    private float normalFOV;

//controls animations
    public Animator sniper;

    public GameObject ScopeOverlay;
    public GameObject blackLeft;
    public GameObject blackRight;

    private bool isScoped = false;

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
        //if right click and weapon is sniper 
        if (Input.GetButtonDown("Fire2") && WeaponSwitching.selectedWeapon == 0)
        {
            isScoped = !isScoped;
            //runs animation
            sniper.SetBool("Scoped", isScoped);


            //ScopeOverlay.SetActive(isScoped);
            //blackLeft.SetActive(isScoped);
            //blackRight.SetActive(isScoped);

            if (isScoped)
            {
                //kind of like wait statements and runs that class
                StartCoroutine(OnScoped());
            }
            else
                OnUnscoped();
        }
        if (isScoped)
        {
            PlayerMovement.speed = 4;
            if (Input.GetKeyDown(shoot))
            {
                Shoot();
                //runs only once
                isScoped = false;
                //sniper.SetBool("Scoped", false);
                //reload animation
                sniper.SetBool("Reload", true);
                StartCoroutine(wait());
                
            }

        }

    }

    void OnUnscoped()
    {
        ScopeOverlay.SetActive(false);
        blackLeft.SetActive(false);
        blackRight.SetActive(false);
        weaponCamera.SetActive(true);
        sniper.SetBool("Reload", false);
        sniper.SetBool("Scoped", false);
        PlayerMovement.speed = 8;
        mainCamera.fieldOfView = normalFOV;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(.15f);
        OnUnscoped();
    }

    IEnumerator OnScoped()
    {
        //wait time
        yield return new WaitForSeconds(.15f);
        //the scope iamge 
        ScopeOverlay.SetActive(true);
        blackLeft.SetActive(true);
        blackRight.SetActive(true);
        weaponCamera.SetActive(false);
        //what you see on the screen changes
        normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopedFOV;
    }
    
    void Shoot()
    {
        GameObject instBullet = Instantiate(prefab, transform.position + Camera.main.transform.forward * 5 + Camera.main.transform.up * 3 / 2, Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z)) as GameObject;
        Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
        instBulletRigidbody.velocity = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z) * new Vector3(0, 0, speed);
    }
}
