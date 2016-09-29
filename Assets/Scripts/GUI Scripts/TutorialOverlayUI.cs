using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialOverlayUI : MonoBehaviour {

    public static TutorialOverlayUI current;

    public Button move, jump, fire;


    [SerializeField]
    private Text descText;
    [SerializeField]
    private RectTransform desc;
    [SerializeField]
    private RectTransform arrow; //used for the GUIspace arrow
    [SerializeField]
    private GameObject crosshairArrow; //used for the Worldspace Arrow

    public Animator ani;

    //Phase1 tutorial bools
    public bool goPressed, jumpPressed, crosshairMoved, firePressed;

    //Phase2 tutorial bools
    public bool enemyOneKilled;

    //Phase3 tutorial bools
    public bool enemyTwoKilled;

    // Use this for initialization
    void Start() {

        current = this;
        crosshairArrow.SetActive(false);
        goPressed = jumpPressed = crosshairMoved = firePressed = false;

        ani = desc.GetComponent<Animator>();

        fire.interactable = false;
    }


    // Update is called once per frame
    void Update()
    {

        if (goPressed)
        {
            arrow.anchoredPosition = new Vector2(685, -140);
            descText.text = "TAP 'JUMP' TO DODGE AND BECOME INVULNERABLE.";

            if (jumpPressed)
            {
           
                descText.text = "TAP ON-SCREEN TO MOVE THE CROSSHAIR.";

                if (crosshairMoved)
                {
                    arrow.anchoredPosition = new Vector2(762, -140);
                    descText.text = "TAP 'FIRE' TO SHOOT TOWARDS THE CROSSHAIR.";

                    if (firePressed)
                    {
                        GameManager.gm.state = GameManager.gameState.tutorial_2;
                        descText.text = "SHOOT THE ENEMY. ENEMY HEALTH IS INDICATED BY IT'S HEALTH BAR.";
                    }
                }
            }
        }

        if(GameManager.gm.state == GameManager.gameState.tutorial_2)
        {
            if (enemyOneKilled)
            {
                arrow.anchoredPosition = new Vector2(351, -176);
                descText.text = "TAP 'SUPER' WHEN CHARGED TO DESTROY ENEMIES AND PROJECTILES.";
                GameManager.gm.state = GameManager.gameState.tutorial_3;
            }
        }

        if(GameManager.gm.state == GameManager.gameState.tutorial_3)
        {
            if (enemyTwoKilled)
            {
                descText.text = "NOW YOU'RE READY FOR THE REAL THING! GOOD LUCK!";
            }
        }

    }

    public void pressedGo()
    {
        if (this.isActiveAndEnabled)
        {

            if (!goPressed)
            {
                ani.Play("Description_Main", -1, 0f);
                goPressed = true;
            }
            
        }

    }

    public void pressedJump()
    {
        if (this.isActiveAndEnabled)
        {

            if (goPressed && !jumpPressed)
            {
                jumpPressed = true;
                ani.Play("Description_Main", -1, 0f);
                arrow.gameObject.SetActive(false);
                crosshairArrow.SetActive(true);
            }
            
        }

    }

    public void movedCrosshair()
    {
        if (this.isActiveAndEnabled)
        {

            if (goPressed && jumpPressed && !crosshairMoved)
            {
                ani.Play("Description_Main", -1, 0f);
                crosshairMoved = true;
                crosshairArrow.SetActive(false);
                arrow.gameObject.SetActive(true);
                fire.interactable = true;
            }

        }
    }

    public void pressedFire()
    {
        if (this.isActiveAndEnabled)
        {

            if (goPressed && jumpPressed && crosshairMoved && !firePressed && fire.interactable)
            {
                arrow.gameObject.SetActive(false);
                ani.Play("Description_Main", -1, 0f);
                firePressed = true;
            }
        }

    }

    public void oneKilled()
    {
        if (!enemyOneKilled)
        {
            arrow.gameObject.SetActive(true);
            ani.Play("Description_Main", -1, 0f);
            enemyOneKilled = true;
            fire.interactable = false;
        }
        
    }

    public void twoKilled()
    {
        if (!enemyTwoKilled)
        {
            arrow.gameObject.SetActive(false);
            ani.Play("Description_Main", -1, 0f);
            enemyTwoKilled = true;
            fire.interactable = true;
            StartCoroutine(switchToMenu());
        }

    }

    IEnumerator switchToMenu()
    {
   
        yield return new WaitForSeconds(5f);

        GameManager.gm.state = GameManager.gameState.menu;
    }

    void OnEnable()
    {
        fire.interactable = false;
        if(ani != null)
        {
            ani.Play("Description_Main", -1, 0f);
        }
        
    }

    void OnDisable()
    {
        if (crosshairArrow != null)
        {
            crosshairArrow.SetActive(false);
        }
        
        arrow.gameObject.SetActive(true);
        arrow.anchoredPosition = new Vector2(40, -140);
        goPressed = jumpPressed = crosshairMoved = firePressed = enemyOneKilled = enemyTwoKilled = false;
        fire.interactable = true;
        

        descText.text = "HOLD 'GO' TO MOVE FORWARD";

    }


}
