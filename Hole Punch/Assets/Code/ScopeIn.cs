using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeIn : MonoBehaviour
{
    public KeyCode scope;
    public Animator sniper;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(scope))
        {
            sniper.SetBool("Scoped", true);
        }
        else if (Input.GetKeyUp(scope))
        {
            sniper.SetBool("Scoped", false);
        }
    }
}
