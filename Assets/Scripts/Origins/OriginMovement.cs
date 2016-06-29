using UnityEngine;
using System.Collections;

public class OriginMovement : MonoBehaviour {

    public GameObject player;
    public GameObject origin, origin2;


    public LayerMask collisionMask;

    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    float horizontalRaySpacing;
    float verticalRaySpacing;

    const float skinWidth = .015f;

    Vector2 currentPos;
    Vector2 targetPos;

    Ray shootRay;
    RaycastHit shootHit;

    ////////////////////
    public float jumpHeight = 3.5f;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .1f;
    float accelerationTimeGrounded = .1f;
    public float moveSpd = 0f;

    float gravity;
    
    public Vector3 velocity;
    float velocityXSmoothing;

    OriginController controller;
    private Transform myTransform;
   
    public LayerMask shootableMask;

    // Use this for initialization
    void Start () {
   

        controller = GetComponent<OriginController>();
        myTransform = transform;

        //gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        gravity = -300f;
       
        velocity = player.GetComponent<PlayerMovement>().groundedVelocity;


    }
	
	// Update is called once per frame
	void Update () {


        //rb.velocity = player.GetComponent<PlayerMovement>().getVelocity();
        //transform.position = player.transform.localPosition - new Vector3(10  + displacement, 0 , 0);

        //Change position/velocity of origin using the same process that moves the player, not using rigidbody
        //transform.Translate(player.GetComponent<PlayerMovement>().getVelocity() * Time.deltaTime);

        //Since position is not being tracked by raycasting, like the Player is, this line is needed to maintain the Y-position;
        //transform.position = new Vector3(transform.position.x, 1.015f , 0);
        //transform.position = new Vector3(transform.position.x, player.transform.position.y, 0);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;

        }

        

       

        if (myTransform.tag == "PlayerOrigin")
        {

            Vector3 clampedPosition = myTransform.position;
            clampedPosition.x = Mathf.Clamp(myTransform.position.x, origin.transform.position.x, origin2.transform.position.x);
            if (origin.GetComponent<OriginController>().collisions.climbingSlope || origin2.GetComponent<OriginController>().collisions.climbingSlope)
            {
                clampedPosition.y = Mathf.Clamp(myTransform.position.y, origin.transform.position.y, Mathf.Infinity); //for climbing slopes, to prevent falling through terrain
            }

            myTransform.position = clampedPosition;


            //velocity = player.GetComponent<PlayerMovement>().getGroundedVelocity();

            if(myTransform.position.x > player.transform.position.x)
            {
                velocity.x-=5;
                velocity.y = -20f;
            }
            else if (myTransform.position.x < player.transform.position.x)
            {
                velocity.y = -20f;
                velocity.x+=5;
            }
            else
            {
                velocity.y = -20f;
                velocity = player.GetComponent<PlayerMovement>().getGroundedVelocity();
            }
            moveSpd = player.GetComponent<PlayerMovement>().moveSpd;

        }
        else
        {
            moveSpd = GameManager.gm.moveSpeed;
        }



        if (controller.collisions.below)
        {
            //float targetVelocityX_input = input.x * moveSpd;
            float targetVelocityX_input = moveSpd;
            //float targetVelocityX = Mathf.Sin(controller.collisions.currentSlopeAngle * Mathf.Deg2Rad) * -gravity;
            //velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX + targetVelocityX_input, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX_input, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

            controller.collisions.speedBeforeJump = velocity.x;
        }
        else
        {
            //float targetVelocityX_input = input.x * moveSpd;
            float targetVelocityX_input = controller.collisions.speedBeforeJump;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX_input, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        }
        //velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        //velocity.x = Mathf.SmoothDamp(velocity.x, 30f, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        //velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX + targetVelocityX_input, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);



        
        velocity.y += gravity * Time.deltaTime;
        
        controller.Move(velocity * Time.deltaTime);

    }

    public Vector3 getPointPosition()
    {
        return new Vector3(shootHit.point.x, shootHit.point.y, 0);
    }

    public Vector3 getVelocity()
    {
        return velocity;
    }



}
