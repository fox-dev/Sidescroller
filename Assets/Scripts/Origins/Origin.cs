using UnityEngine;
using System.Collections;

public class Origin : MonoBehaviour {

    public GameObject player;
    public GameObject origin, origin2;
    Rigidbody rb;

    public LayerMask collisionMask;

    public float displacement;

    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    float horizontalRaySpacing;
    float verticalRaySpacing;

    const float skinWidth = .015f;
    BoxCollider collider;

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
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    OriginController controller;

   
    float max_displacement = 50;
    float speed_Up = 30;
    float speed_Down = 30;
   
    public LayerMask shootableMask;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

        controller = GetComponent<OriginController>();

        //gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        gravity = -300f;
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);


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

        

       

        if (transform.tag == "PlayerOrigin")
        {

            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(transform.position.x, origin.transform.position.x, origin2.transform.position.x);
            if (origin.GetComponent<OriginController>().collisions.climbingSlope || origin2.GetComponent<OriginController>().collisions.climbingSlope)
            {
                clampedPosition.y = Mathf.Clamp(transform.position.y, origin.transform.position.y, Mathf.Infinity); //for climbing slopes, to prevent falling through terrain
            }

            transform.position = clampedPosition;


            velocity = player.GetComponent<PlayerMovement>().getGroundedVelocity();

            if(transform.position.x > player.transform.position.x)
            {
                velocity.x-=5;
                velocity.y = -20f;
            }
            else if (transform.position.x < player.transform.position.x)
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

        /*

        currentPos = new Vector2(transform.position.x, transform.position.y);
        targetPos = new Vector2(transform.position.x, transform.position.y - 10);
        shootRay.origin = new Vector3(transform.position.x, transform.position.y + 1.015f, 0);
        shootRay.direction = (targetPos - currentPos);

        VerticalCollisions();

    */

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

    void VerticalCollisions()
    {


        if (Physics.Raycast(shootRay, out shootHit, 100, shootableMask))
        {
            print(shootHit.point);
            //hit an enemy goes here


            if (shootHit.transform.gameObject.layer != LayerMask.NameToLayer("Road"))
            {
                shootHit.transform.gameObject.SetActive(false);
            }
            print("HITHITHIT");

            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + 1.015f, 0) , new Vector3(shootHit.point.x, shootHit.point.y, shootHit.point.z), Color.red);
        }
        else
        {
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + 1.015f, 0), new Vector3(shootHit.point.x, shootHit.point.y, shootHit.point.z), Color.red);
        }


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
