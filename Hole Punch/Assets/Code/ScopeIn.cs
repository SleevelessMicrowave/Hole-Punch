using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        
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
                StartCoroutine(OnScoped());
            else
                OnUnscoped();
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
}
