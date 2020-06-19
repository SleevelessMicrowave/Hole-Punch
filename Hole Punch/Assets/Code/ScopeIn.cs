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
        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            sniper.SetBool("Scoped", isScoped);


            //ScopeOverlay.SetActive(isScoped);
            //blackLeft.SetActive(isScoped);
            //blackRight.SetActive(isScoped);

            if (isScoped)
            {
                StartCoroutine(OnScoped());
            }
            else
                OnUnscoped();
        }
        if (isScoped)
        {

            if (Input.GetKeyDown(shoot))
            {
                Shoot();

                isScoped = false;
                sniper.SetBool("Scoped", false);
                OnUnscoped();
            }

        }

    }

    void OnUnscoped()
    {
        ScopeOverlay.SetActive(false);
        blackLeft.SetActive(false);
        blackRight.SetActive(false);
        weaponCamera.SetActive(true);

        mainCamera.fieldOfView = normalFOV;
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);

        ScopeOverlay.SetActive(true);
        blackLeft.SetActive(true);
        blackRight.SetActive(true);
        weaponCamera.SetActive(false);

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
