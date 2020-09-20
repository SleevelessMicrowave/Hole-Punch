using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform debughitPointtransform;

    public CharacterController controller;

    public GameObject character;
    public GameObject gun;

    public Camera player;

    public ParticleSystem wind;

    public static float speed = 8f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public KeyCode sprint;
    public KeyCode crouch;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private State state;
    private Vector3 hookshotPosition;

    private enum State
    {
        Normal,
        HookshotFlyingPlayer
    }

    // Start is called before the first frame update
    void Awake()
    {
        state = State.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Normal:
                HandleHookshotStart();
                MovePlayer();
                break;
            case State.HookshotFlyingPlayer:
                HandlehookshotMovement();
                break;
        }
        
        
    }

    private void MovePlayer()
    {
        /*if (Input.GetKeyDown(sprint))
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
        }*/

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x * speed + transform.forward * z * speed + transform.up * velocity.y;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(move * Time.deltaTime);

        /*if (Input.GetKeyDown(crouch))
        {
            speed = 6;
            character.transform.localScale = new Vector3(1, .4f, 1);
            gun.transform.localScale = new Vector3(.40686f, 1, .40686f);
        }
        else if (Input.GetKeyUp(crouch))
        {
            speed = 8;
            character.transform.localScale = new Vector3(1, 1, 1);
            gun.transform.localScale = new Vector3(.40686f, .40686f, .40686f);
        }*/
    }

    private void HandleHookshotStart()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Checks if raycast hits something from origin, direction, and boolean if something is hit
            if (Physics.Raycast(player.transform.position, player.transform.forward, out RaycastHit raycastHit)){
                debughitPointtransform.position = raycastHit.point; //places object at the point when raycast hits it
                hookshotPosition = raycastHit.point;
                state = State.HookshotFlyingPlayer; //modifies state that called handlehookshotMovement
            }
        }
    }

    private void HandlehookshotMovement()
    {
        Vector3 hookshotDir = (hookshotPosition - transform.position).normalized;

        float hookshotSpeed = Vector3.Distance(transform.position, hookshotPosition);
        float hookshotSpeedMultiplier = 2f;

        controller.Move(hookshotDir * hookshotSpeed * hookshotSpeedMultiplier * Time.deltaTime);

        float reachedHookshotPositionDistance = 3f;
        if (Vector3.Distance(transform.position, hookshotPosition) < reachedHookshotPositionDistance)
        {
            //reached hookshot position
            state = State.Normal;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            state = State.Normal;
        }
    }
}
