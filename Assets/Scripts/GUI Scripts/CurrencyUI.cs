using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class CurrencyUI : MonoBehaviour
{

    private Text currencyText;

    // Use this for initialization
    void Start()
    {
        currencyText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        currencyText.text = "Currency: " + GameManager.currency.ToString();

    }
}
