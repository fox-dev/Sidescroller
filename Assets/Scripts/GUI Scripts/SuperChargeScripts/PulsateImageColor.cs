using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PulsateImageColor : MonoBehaviour {


    public Image thisImage;
    public float start, end;
    private bool glow;
    public float pulseRate;
    Color c; 
    

	// Use this for initialization
	void Start () {
        thisImage = GetComponent<Image>();
        c = thisImage.color;
        glow = false;
    }

    // Update is called once per frame
    void Update()
    {
  
        if (glow)
        {

            c.a = Mathf.Lerp(c.a, end, pulseRate * Time.deltaTime);
        }
        else
        {

            c.a = Mathf.Lerp(c.a, start, pulseRate * Time.deltaTime);
        }

        if (c.a >= end - 10 && glow)
        {
            glow = false;
        }
        else if (c.a <= start + 10 && !glow)
        {
            glow = true;
        }
       
       thisImage.color = ConvertColor(c.r, c.g, c.b, c.a);
    }

    public static Color ConvertColor(float r, float g, float b, float a)
    {
        return new Color(r , g , b , a / 255.0f);
    }

    void OnDisable()
    {
        c = new Color(c.r, c.g, c.b, 0);
    }

}
