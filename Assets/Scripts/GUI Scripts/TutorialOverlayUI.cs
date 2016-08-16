using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialOverlayUI : MonoBehaviour {

    public static TutorialOverlayUI current;
    

    [SerializeField]
    private Text descText;
    [SerializeField]
    private RectTransform desc;
    [SerializeField]
    private RectTransform arrow;
    [SerializeField]
    private GameObject crosshairArrow;

    public Animator ani;

    public bool goPressed, jumpPressed, crosshairMoved, firePressed;

	// Use this for initialization
	void Start () {

        current = this;
        crosshairArrow.SetActive(false);
        goPressed = jumpPressed = crosshairMoved = firePressed = false;

        ani = desc.GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {

        if (goPressed)
        {
            arrow.anchoredPosition = new Vector2(685, -140);
            descText.text = "TAP 'JUMP' TO DODGE AND BECOME INVULNERABLE";
            ani.SetBool("displayText", true);
            if (jumpPressed)
            {
                crosshairArrow.SetActive(true);
                descText.text = "TAP ONSCREEN TO MOVE THE CROSSHAIR";
                ani.SetBool("displayText", true);

                if (crosshairMoved)
                {
                    crosshairArrow.SetActive(false);
                    arrow.gameObject.SetActive(true);
                    arrow.anchoredPosition = new Vector2(762, -140);
                    descText.text = "TAP 'FIRE' TO SHOOT TOWARDS THE CROSSHAIR";
                    ani.SetBool("displayText", true);
                }
            }
        }

    }

    public void pressedGo()
    {
        if (this.isActiveAndEnabled)
        {
            ani.Play("Description_Main", -1, 0f);

            if(!goPressed) ani.SetBool("displayText", false);

            goPressed = true;
        }
        
    }

    public void pressedJump()
    {
        if (this.isActiveAndEnabled)
        {
            arrow.gameObject.SetActive(false);

            if(!jumpPressed) ani.Play("Description_Main", -1, 0f);

            jumpPressed = true;
        }

    }

    public void pressedFire()
    {
        if (this.isActiveAndEnabled)
        {

            if(!firePressed) ani.Play("Description_Main", -1, 0f);

            firePressed = true;
        }

    }
}
