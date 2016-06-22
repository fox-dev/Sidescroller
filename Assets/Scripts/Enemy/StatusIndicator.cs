using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class StatusIndicator : MonoBehaviour {

    [SerializeField]
    private RectTransform healthBarRect;

    [Header("Optional:")]
    [SerializeField]
    private Text healthText;

    void Start()
    {
        if(healthBarRect == null)
        {
            Debug.LogError("Status indicator: No health bar object referenced");
        }

       /*
        if (healthText == null)
        {
            Debug.LogError("Status indicator: No health text object referenced");
        }
        */
    }

    public void SetHealth(int _cur, int _max)
    {
        float _value = (float)_cur / _max;

        healthBarRect.localScale = new Vector3(_value, healthBarRect.localScale.y, healthBarRect.localScale.z);

        if(_value <= .45)
        {
            healthBarRect.gameObject.GetComponent<Image>().color = Color.red;
        }
        else
        {
            healthBarRect.gameObject.GetComponent<Image>().color = Color.green;
        }

        if(healthText != null)
        {
            healthText.text = _cur + "/" + _max + " HP";
        }
        
    }

   

}
