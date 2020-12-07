using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFramerate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //to fix frame rate and keepp it constant
    void Update()
    {
        Application.targetFrameRate = 50;
    }
}
