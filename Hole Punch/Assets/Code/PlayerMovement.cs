//variables pulled and linked through unity 
//axis is attached to guy
using System.Collections;
using System.Collections.Generic;
//unity libraries
using UnityEngine;
//link to unity

public class PlayerMovement : MonoBehaviour
{
    //t5ransform into object
    [SerializeField] private Transform debughitPointtransform;

    //controller for characteer
    public CharacterController controller;

    public GameObject character;
    public GameObject gun;

    public Camera player;

    public ParticleSystem wind;

//decimal vars
    public static float speed = 8f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

//keycode var but set equal to a key 
    public KeyCode sprint;
    public KeyCode crouch;

//attatch a transform for positions
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    //object that you can assign to layers
    public LayerMask groundMask;

//3d vectpr
    Vector3 velocity;
    bool isGrounded;

//state you're in
    private State state;
    private Vector3 hookshotPosition;
//sets up two states
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
        //sprint key down
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
//checks if player is on the ground and touching. Makes sure you are on the ground at all times
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    //which direction you are facing returns from -1 to 1 wasd
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
    
    //transform.right says move along the x axis * direction * the speed
        Vector3 move = transform.right * x * speed + transform.forward * z * speed + transform.up * velocity.y;
        //jump is equal to spacebar pressd. If on ground jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

    //every frame time in seconds since last frame so it's more accurate if franme rate changes 
        velocity.y += gravity * Time.deltaTime;

    //move method of charactar ontroller runs the move vector3
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

    //if e is pressed 
    private void HandleHookshotStart()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Checks if raycast hits something from origin, direction, and boolean if something is hit
            //raycast shoots invisible line from point A to point B 
            //If something is detected in RaycasHit raycastHit where the player is looking from the players position 
            if (Physics.Raycast(player.transform.position, player.transform.forward, out RaycastHit raycastHit)){
                debughitPointtransform.position = raycastHit.point; //places object at the point when raycast hits it
                hookshotPosition = raycastHit.point;
                state = State.HookshotFlyingPlayer; //modifies state that called handlehookshotMovement
            }
        }
    }

    private void HandlehookshotMovement()
    {
        //hookshopt position and - location of player to figure the distance to move 
        Vector3 hookshotDir = (hookshotPosition - transform.position).normalized;

        float hookshotSpeed = Vector3.Distance(transform.position, hookshotPosition);
        float hookshotSpeedMultiplier = 2f;

        controller.Move(hookshotDir * hookshotSpeed * hookshotSpeedMultiplier * Time.deltaTime);

        float reachedHookshotPositionDistance = 3f;
        //distance between orb and person cant go below three, then goes back to normal no teleporting state
        if (Vector3.Distance(transform.position, hookshotPosition) < reachedHookshotPositionDistance)
        {
            //reached hookshot position
            state = State.Normal;
        }
        //if click e again stop immideatly
        else if (Input.GetKeyDown(KeyCode.E))
        {
            state = State.Normal;
        }
    }
}
