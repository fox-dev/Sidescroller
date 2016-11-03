using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class HighscoreUI : MonoBehaviour {

    private Text HighscoreText;

    public static HighscoreUI current;

    // Use this for initialization
    void Start () {

        current = this;
        HighscoreText = GetComponent<Text>();
        HighscoreText.text = "HighScore: " + GameManager.highScore.ToString();

    }

    public void UpdateText()
    {
        HighscoreText.text = "HighScore: " + GameManager.highScore.ToString();
    }
}
