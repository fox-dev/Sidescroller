using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class CurrencyUI : MonoBehaviour
{

    private Text currencyText;

    public static CurrencyUI current;

    // Use this for initialization
    void Start()
    {
        current = this;
        currencyText = GetComponent<Text>();
        currencyText.text = "Currency: " + GameManager.currency.ToString();

    }


    public void UpdateText()
    {
        currencyText.text = "Currency: " + GameManager.currency.ToString();
    }
}
