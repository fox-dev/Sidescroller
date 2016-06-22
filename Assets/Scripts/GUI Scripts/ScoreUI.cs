using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class ScoreUI : MonoBehaviour {

    private Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        scoreText.text = "Score: " +  GameManager.score.ToString();
	
	}
}
