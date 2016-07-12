using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SideGradesPanel : MonoBehaviour {
    [SerializeField]
    private GameObject player;
    private GameObject buddy;

    [SerializeField]
    private Button buddy_purcahseButton;
    private Text buddy_purchaseText;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        buddy_purchaseText = buddy_purcahseButton.GetComponentInChildren<Text>();

        foreach(Transform child in player.transform)
        {
            if(child.name == "Buddy")
            {
                buddy = child.gameObject;
            }
        }

    }

    public void purcahseBuddy()
    {
        int cost = 1000;

        if(GameManager.currency >= cost)
        {
            GameManager.SubtractCurrency(cost);
            buddy.SetActive(true);
            buddy_purcahseButton.interactable = false;
            buddy_purchaseText.text = "PURCHASED";
        }
        
    }

    
}
