using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class ScoreUI : MonoBehaviour {

    private Text scoreText;

    public static ScoreUI current;

	// Use this for initialization
	void Start () {
        current = this;
        scoreText = GetComponent<Text>();
        scoreText.text = "Score: " + GameManager.score.ToString();

    }

    public void UpdateText()
    {
        scoreText.text = "Score: " + GameManager.score.ToString();
    }
}
