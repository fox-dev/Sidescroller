using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Charge blast is handled in LaserBlase and PlayerWeapon scripts
[RequireComponent(typeof(Animator))]
public class ChargeMeterGUI : MonoBehaviour {

    public static ChargeMeterGUI current;

    public Image chargeGauge_Small, chargeGauge_Large; //Recharging = small image(inActive), fullCharge = big image(active)

    public Animator chargeMeterAnim;

    public Button button;

    public Player player;

    private bool occupied; //for ienumerator use in update;
    // Use this for initialization
    void Awake()
    {
        button.interactable = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        current = this;
        occupied = false;

        if (chargeMeterAnim == null)
        {
            Debug.LogError("No chargeMeterAnim referenced");
        }

    }

    void Update()
    {
        SetGauge((int)player.wep.currentCharge, 100);

        if (player.wep.readyToFire && player.wep.charge && !player.wep.firing)
        {
            button.interactable = true;
            chargeMeterAnim.SetBool("Charged", true);
        }
        else if (player.wep.currentCharge <= 0 && !player.wep.charge)
        {
            button.interactable = false;
            StartCoroutine(waitForAnimation());
        }
    
    }


    public void SetGauge(int _cur, int _max)
    {
  
        chargeGauge_Small.fillAmount = (float)_cur / _max;
        chargeGauge_Large.fillAmount = (float)_cur / _max;
    }

    IEnumerator waitForAnimation()
    {
        occupied = true;

        chargeMeterAnim.SetBool("Recharge", true);

        yield return new WaitForSeconds(1f);

        chargeMeterAnim.SetBool("Charged", false);

        yield return new WaitForSeconds(1f);

        chargeMeterAnim.SetBool("Recharge", false);

        occupied = false;
    }
}
