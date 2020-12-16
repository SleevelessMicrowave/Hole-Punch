using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gm : MonoBehaviour
{
    public Transform player;

    Vector3 initialPosition;

    // Start is called before the first frame update
    void Awake()
    {
        initialPosition = player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player.position.y < -15)
        {
            player.position = initialPosition;
        }
    }
}
