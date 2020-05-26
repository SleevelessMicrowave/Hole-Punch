using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public GameObject character;

    public Camera player;

    public ParticleSystem wind;

    public float speed = 8f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public KeyCode sprint;
    public KeyCode crouch;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(sprint))
        {
            speed = 12;
            player.fieldOfView = 68;
            wind.Play();
            wind.enableEmission = true;
        }
        else if (Input.GetKeyUp(sprint))
        {
            speed = 8;
            player.fieldOfView = 60;
            wind.enableEmission = false;
            wind.Stop();
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(crouch))
        {
            speed = 6;
            character.transform.localScale = new Vector3(1, .4f, 1);
        }
        else if (Input.GetKeyUp(crouch))
        {
            speed = 8;
            character.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
