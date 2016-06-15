using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {

    public LayerMask collisionMask; 

    const float skinWidth = .015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    float maxClimbAngle = 45;
    float maxDescendAngle = 45;

    float horizontalRaySpacing;
    float verticalRaySpacing;

    BoxCollider collider;
    RaycastOrigins raycastOrigins;
    public CollisionInfo collisions;

    public Transform playerModel;

    

    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public bool climbingSlope;
        public bool descendingSlope;
        public float slopeAngle, slopeAngleOld, currentSlopeAngle;

        public Quaternion target;

        public float speedBeforeJump;

        public void Start()
        {
            target = Quaternion.Euler(0, 0, 0);
        }

        public void Reset()
        {
            above = below = false;
            left = right = false;
            climbingSlope = false;
            descendingSlope = false;

            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
            
        }
    }

    // Use this for initialization
    void Start () {

        collider = GetComponent<BoxCollider>();
        collisions.Start();
        CalculateRaySpacing();

    }

    public void Move(Vector3 velocity)
    {
        //print(velocity.y);
       // print(collisions.descendingSlope);
        UpdateRaycastOrigins();
        collisions.Reset();

        if(velocity.y < 0)
        {
            DescendSlope(ref velocity);
        }
        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }
        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }

        
        playerModel.transform.rotation = Quaternion.Lerp(playerModel.transform.rotation, collisions.target, 20*Time.deltaTime);
        
        transform.Translate(velocity);
    }

    void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit hit;
            bool hitSomething = Physics.Raycast(rayOrigin, Vector2.right * directionX, out hit, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);
            
            if (hitSomething)
            {
               
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

                
                if (i == 0 && slopeAngle <= maxClimbAngle)
                {
                    float distanceToSlopeStart = 0;
                    if (slopeAngle != collisions.slopeAngleOld)
                    {
                        distanceToSlopeStart = hit.distance - skinWidth;
                        velocity.x -= distanceToSlopeStart * directionX;
                    }
                    ClimbSlope(ref velocity, slopeAngle);
                    //get slope at current location
                    collisions.target = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                    velocity.x += distanceToSlopeStart * directionX;
                }

                if (!collisions.climbingSlope || slopeAngle > maxClimbAngle)
                {
                    
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    rayLength = hit.distance;

                    if (collisions.climbingSlope)
                    {
                        velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                    }

                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }
               
               

            }
           
        }
    }


    void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);

            RaycastHit hit;
            bool hitSomething = Physics.Raycast(rayOrigin, Vector2.up * directionY, out hit, rayLength, collisionMask);
           

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hitSomething)
            {
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;
             

                if (collisions.climbingSlope)
                {
                    velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                }
               

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }
        if (collisions.climbingSlope)
        {
            float directionX = Mathf.Sign(velocity.x);
            rayLength = Mathf.Abs(velocity.x) + skinWidth;
            Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * velocity.y;
            RaycastHit hit;
            bool hitSomething = Physics.Raycast(rayOrigin, Vector2.right * directionX, out hit, rayLength, collisionMask);
            if (hitSomething)
            {
                
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            

                if (slopeAngle != collisions.slopeAngle)
                {
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    
                    
                }
            }

        }
        
    }

    void ClimbSlope(ref Vector3 velocity, float slopeAngle)
    {
        float moveDistance = Mathf.Abs(velocity.x);
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;


        if (velocity.y <= climbVelocityY)
        {
            //print("HERE");
            velocity.y = climbVelocityY;
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
            collisions.below = true;
            collisions.climbingSlope = true;
        }
        
    }

    void DescendSlope(ref Vector3 velocity)
    {
        
        float directionX = Mathf.Sign(velocity.x);
        Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
        RaycastHit hit;
        bool hitSomething = Physics.Raycast(rayOrigin, -Vector2.up, out hit, Mathf.Infinity, collisionMask);

        if (hitSomething)
        {
            
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            
            //Get slope at current location
            collisions.target = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            
            collisions.currentSlopeAngle = slopeAngle;

            
            if (slopeAngle != 0 && slopeAngle <= maxDescendAngle)
            {
                //print("HERE");
                if (Mathf.Sign(hit.normal.x) == directionX)
                {
                    if(hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x))
                    {
                        float moveDistance = Mathf.Abs(velocity.x);
                        float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                        velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                        velocity.y -= descendVelocityY;

                        collisions.descendingSlope = true;
                        collisions.below = true;
                        
                        
                    }
                }
            }
            
            
        }
    }

    /*
    // Update is called once per frame
    void Update()
    {
        UpdateRaycastOrigins();
        CalculateRaySpacing();

        for (int i = 0; i < verticalRayCount; i++)
        {
            Debug.DrawRay(raycastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.up * -2, Color.red);
        }
    }
    */

    void UpdateRaycastOrigins()
    {
        BoxCollider bounds = collider;
        bounds.bounds.Expand(skinWidth * -2);
        

        raycastOrigins.bottomLeft = new Vector3(bounds.bounds.min.x, bounds.bounds.min.y);
        raycastOrigins.bottomRight = new Vector3(bounds.bounds.max.x, bounds.bounds.min.y);
        raycastOrigins.topLeft = new Vector3(bounds.bounds.min.x, bounds.bounds.max.y);
        raycastOrigins.topRight = new Vector3(bounds.bounds.max.x, bounds.bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        BoxCollider bounds = collider;
        bounds.bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }
}
