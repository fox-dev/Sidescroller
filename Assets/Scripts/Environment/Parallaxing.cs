using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing = 1f;
    float commonY;

    [Header("Between 0 - 6")]
    public float speedMultiplier;
    private float speedFactor;



    private Transform cam;
    private Vector3 prevCamPos;
   

    void Awake()
    {
        //set up camera reference
        cam = Camera.main.transform;
    }

	// Use this for initialization
	void Start () {
      
        //The previous frame that had the current frame's camera pos
        prevCamPos = cam.position;
   
        
        parallaxScales = new float[backgrounds.Length];

        //assign corresponding parallaxScales
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = (backgrounds[i].position.z) * -1f;
            
        }
    }
	
   
	// Update is called once per frame
	void LateUpdate () {

        

        //for each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (i == 0) //Foreground
            {
                speedFactor = speedMultiplier * 4;
            }
            if (i == 3)//Frontground
            {
                speedFactor = speedMultiplier * -3;
            }
            if (i == 1) //background
            {
                speedFactor = speedMultiplier * 3/5;
            }
            if (i == 2) //background2
            {
                //speed at 15
                //speedFactor = speedMultiplier * -1/5; 
                speedFactor = speedMultiplier * 0.05f;
            }
            if (i == 4) //platforms
            {
                speedFactor = speedMultiplier * 0.8f;

            }
       




            //the parallax is the opposite of the camera movement b/c prev frame multiplied by the scale
            Vector3 parallax = ((prevCamPos - cam.position) + new Vector3(speedFactor, 0, 0 )) * (parallaxScales[i]);

            
           
   

            //Set a target x position which is the current position plus the parallax
            //Vector3 backgroundTargetPos = new Vector3(backgrounds[i].position.x + parallax.x, backgrounds[i].position.y + parallax.y, backgrounds[i].position.z);
			Vector3 backgroundTargetPos = new Vector3(backgrounds[i].position.x + parallax.x, backgrounds[i].position.y, backgrounds[i].position.z);
            if(i == 4)
            {

                //backgroundTargetPos = new Vector3(backgrounds[i].localPosition.x + parallax.x/100f, backgrounds[i].localPosition.y, backgrounds[i].localPosition.z);
                //print(backgrounds[i].localPosition.x);
            }



            //create a target pos which is the background's current pos with it's target x position
            //Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between current pos and the target pos using lerp
            if (i == 4)
            {
               // backgrounds[i].localPosition = Vector3.Lerp(backgrounds[i].localPosition, backgroundTargetPos, smoothing * Time.deltaTime);
                //backgrounds[i].localPosition = backgroundTargetPos;


            }
            else
            {
                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
            }
            
        }





        //set prevCamPos to the camera's position at the end of the frame
        prevCamPos = cam.position;
    
      


    }

   
}
