﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{

    public float jumpHeight = 3.5f;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .1f;
    float accelerationTimeGrounded = .1f;
    public float moveSpd = 0f;

    float gravity;
    float jumpVelocity;
    public Vector3 velocity, groundedVelocity;
    float velocityXSmoothing;

    PlayerController controller;
    private Transform myTransform;


    public float speed_Up = 40f;
    public float speed_Down = -25;
    public GameObject origin, origin2;
    public bool maxed_Up, maxed_Down = false;
    public bool moveUp = false;
    bool desc;

    public bool jumping = false;

    private bool moveForward = false;
    private bool jump = false;



    // Use this for initialization
    void Start()
    {
        desc = false;
        controller = GetComponent<PlayerController>();
        myTransform = transform;

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }


    void Update()
    {
        groundedVelocity = velocity;
        groundedVelocity.y = 0;

        jumping = !controller.collisions.below; //if in the air, jumping is true

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        
        //if jump boolean is true or key is pressed, and collisions.below, then perform jump
        if ((jump || Input.GetKeyDown(KeyCode.Space)) && controller.collisions.below)
        {
            jump = false;
            velocity.y = jumpVelocity;
            AudioManager.current.PlaySound("Jump");

        }


        // print(transform.position.x - origin.transform.position.x);
        if (controller.collisions.descendingSlope && origin2.GetComponent<OriginController>().collisions.descendingSlope)
        {
            desc = true;
        }
        else if (!controller.collisions.descendingSlope && !origin2.GetComponent<OriginController>().collisions.descendingSlope)
        {
            desc = false;
        }



        Vector3 clampedPosition = myTransform.position;
        clampedPosition.x = Mathf.Clamp(myTransform.position.x, origin.transform.position.x, origin2.transform.position.x);
        if (origin.GetComponent<OriginController>().collisions.climbingSlope || origin2.GetComponent<OriginController>().collisions.climbingSlope)
        {
            clampedPosition.y = Mathf.Clamp(myTransform.position.y, origin.transform.position.y, Mathf.Infinity); //for climbing slopes, to prevent falling through terrain
        }

        myTransform.position = clampedPosition;


        if (Input.GetKey("d") || moveForward)
        {

            moveUp = true;
            maxed_Down = false;

            if (!maxed_Up)
            {

                velocity.x = speed_Up;


            }

            if (myTransform.position.x >= origin2.transform.position.x)
            {

                maxed_Up = true;
                //velocity = Vector3.zero;
                //moveSpd = 15f;
                moveSpd = GameManager.gm.moveSpeed;
                //velocity = new Vector3(moveSpd, velocity.y, 0);
                myTransform.position = Vector3.MoveTowards(myTransform.position, new Vector3(origin2.transform.position.x, myTransform.position.y, myTransform.position.z), 10 * Time.deltaTime);

            }

        }

        else
        {
            moveUp = false;
            maxed_Up = false;


            if (!maxed_Down)
            {


                velocity.x = speed_Down;



            }

            if (myTransform.position.x <= origin.transform.position.x)
            {

                maxed_Down = true;


                velocity = new Vector3(moveSpd, velocity.y, 0);
                myTransform.position = Vector3.MoveTowards(myTransform.position, new Vector3(origin.transform.position.x, myTransform.position.y, myTransform.position.z), 100 * Time.deltaTime);
            }


        }

        //print(controller.collisions.descendingSlope + " " + controller.collisions.climbingSlope);


        if (controller.collisions.below)
        {
            //float targetVelocityX_input = input.x * moveSpd;
            float targetVelocityX_input = moveSpd;
            //float targetVelocityX = Mathf.Sin(controller.collisions.currentSlopeAngle * Mathf.Deg2Rad) * -gravity; // Speed based of slope;
            //velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX + targetVelocityX_input, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX_input, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

            controller.collisions.speedBeforeJump = velocity.x;
        }
        else
        {
            //float targetVelocityX_input = input.x * moveSpd;
            //float targetVelocityX_input = controller.collisions.speedBeforeJump/1.75f;
            float targetVelocityX_input = controller.collisions.speedBeforeJump / 1.75f;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX_input, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        }



        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


    }

    public Vector3 getVelocity()
    {
        return velocity;
    }

    public Vector3 getGroundedVelocity()
    {
        return groundedVelocity;
    }

    //For onscreen button usage//
    public void moveForwardPressed()
    {
        if(GameManager.gm.state == GameManager.gameState.tutorial_1)
        {
            if (TutorialOverlayUI.current.move.interactable)
            {
                moveForward = true;
            }
        }
        else
        {
            moveForward = true;
        }
        
    }

    public void moveForwardReleased()
    {
        moveForward = false;
    }

    public void jumpPressed()
    {
        jump = true;
        
    }

    public void jumpReleased()
    {
        jump = false;
    }
    ///////////////////////////
}
