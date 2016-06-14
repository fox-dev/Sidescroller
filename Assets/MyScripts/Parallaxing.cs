using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

    GameObject origin, origin2;
    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing = 1f;

    [Header("Between 0 - 1")]
    public float speedFactor, x, y;

    private Transform cam;
    private Vector3 prevCamPos;

    void Awake()
    {
        //set up camera reference
        cam = Camera.main.transform;
    }

	// Use this for initialization
	void Start () {
        origin = GameObject.FindGameObjectWithTag("Origin");
        origin2 = GameObject.FindGameObjectWithTag("Origin2");
        //The previous frame that had the current frame's camera pos
        prevCamPos = cam.position;
        parallaxScales = new float[backgrounds.Length];

        //assign corresponding parallaxScales
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = (backgrounds[i].position.z) * -1f;
            
        }

        parallaxScales[4] = parallaxScales[0];

        

        parallaxScales[2] = backgrounds[2].position.z * -4f;
        parallaxScales[1] = backgrounds[1].position.z * -2f;
    }
	
   
	// Update is called once per frame
	void LateUpdate () {

        

        //for each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            //the parallax is the opposite of the camera movement b/c prev frame multiplied by the scale
            //float parallax = (prevCamPos.x - cam.position.x) * parallaxScales[i];
            Vector3 parallax = (prevCamPos - cam.position) * (parallaxScales[i] / (smoothing));
            //parallax = parallax + new Vector3(0.2f, 0, 0);

            //Set a target x position which is the current position plus the parallax
            //float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3(backgrounds[i].position.x - speedFactor + parallax.x , backgrounds[i].position.y + parallax.y, backgrounds[i].position.z);
            if(i == 2)
            {
                backgroundTargetPos = new Vector3(backgrounds[i].position.x - 0.3f + parallax.x, backgrounds[i].position.y + parallax.y, backgrounds[i].position.z);
            }

            if (i == 5)
            {
                backgroundTargetPos = new Vector3(backgrounds[i].position.x - 0.3f + parallax.x, backgrounds[i].position.y, backgrounds[i].position.z);
            }


            //create a target pos which is the background's current pos with it's target x position
            //Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between current pos and the target pos using lerp
            //backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
            backgrounds[i].position = backgroundTargetPos;
        }

        //set prevCamPos to the camera's position at the end of the frame
        prevCamPos = cam.position;

    }
}
