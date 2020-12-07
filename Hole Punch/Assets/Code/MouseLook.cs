using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity;

    public Transform playerBody;

    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
        Cursor.visible = false;
        //wcursor dissapear and locks in place for like movement stuff
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //sensivity * mouse movement x and y inputs  
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //
        xRotation -= mouseY;
        //stops rotation so that you can only look straight up and down for a certain angle
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //on the transform it changes the rotation based on where lookinh
        //if not attached to anything attached to camera object bc script is attached to the object so  rotates the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //rotates the first person player so the plaer movement works. This changes the transform on the player 
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
