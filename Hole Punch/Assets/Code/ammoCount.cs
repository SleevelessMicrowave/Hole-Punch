using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ammoCount : MonoBehaviour {
    public static int remainingShots = 15;
    public bool isFiring;
    public Text ammoDisplay;

    public static bool fireAmmo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ammoDisplay.text = "Sniper Bullets: " + remainingShots.ToString();

        if(Input.GetMouseButtonDown(0) && !isFiring && remainingShots > 0 && WeaponSwitching.selectedWeapon == 0)
        {
            isFiring = true;
            remainingShots--;
            isFiring = false;
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            remainingShots = 15;
        }
    }
}
