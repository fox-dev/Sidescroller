﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Script handles the results screen GUI
public class ResultsPanel : MonoBehaviour {

    //RectTransforms of each of the columns on the results GUI
    [SerializeField]
    private RectTransform score, enemiesDestroyed, enemiesMissed, damageDone, timesHit, damageTaken;

    //Text of each of the columns
    [SerializeField]
    private Text scoreText, enemiesDestroyedText, enemiesMissedText, damageDoneText, timesHitText, damageTakenText;

    //Integer values used for the text, needed to increment values from 0 to current on the Results Screen
    [SerializeField]
    private float scoreValue, enemiesDestroyedValue, enemiesMissedValue, damageDoneValue, timesHitValue, damageTakenValue;

    //Increment speed
    [SerializeField]
    private float incrementSpeed = 0f;
    private float inc = 0f;

    // Use this for initialization
    void Start () {
        scoreValue = enemiesDestroyedValue = enemiesMissedValue = damageDoneValue = timesHitValue = damageTakenValue = 0;
    }

	// Update is called once per frame
	void Update () {
        incrementSpeed += Time.deltaTime;

        inc = incrementSpeed/0.5f;

        incrementScore();
        incrementEnemiesDestroyed();
        incrementEnemiesMissed();
        incrementDamageDone();
        incrementTimesHit();
        incrementDamageTaken();

	}

    //Button on the results screen
    public void nextButton()
    {
        scoreValue = enemiesDestroyedValue = enemiesMissedValue = damageDoneValue = timesHitValue = damageTakenValue = 0;
        GameManager.gm.state = GameManager.gameState.setup;
    }

    //Lerp Value methods//
    public void incrementScore()
    {
        scoreText.text = "SCORE : " + Mathf.RoundToInt(scoreValue);
        scoreValue = Mathf.Lerp(scoreValue, GameManager.gm.gameStats.roundScore, inc);
        incrementSpeed = 0f;


    }

    public void incrementEnemiesDestroyed()
    {
        enemiesDestroyedText.text = "ENEMIES DOWNED : " + Mathf.RoundToInt(enemiesDestroyedValue);
        enemiesDestroyedValue = Mathf.Lerp(enemiesDestroyedValue, GameManager.gm.gameStats.enemiesDestroyed, inc);
        incrementSpeed = 0f;
    }

    public void incrementEnemiesMissed()
    {
        enemiesMissedText.text = "ENEMIESED MISSED : " + Mathf.RoundToInt(enemiesMissedValue);
        enemiesMissedValue = Mathf.Lerp(enemiesMissedValue, GameManager.gm.gameStats.enemiesMissed, inc);
        incrementSpeed = 0f;
    }

    public void incrementDamageDone()
    {
        damageDoneText.text = "TOTAL DAMAGE DONE : " + Mathf.RoundToInt(damageDoneValue);
        damageDoneValue = Mathf.Lerp(damageDoneValue, GameManager.gm.gameStats.totalDamageDone, inc);
        incrementSpeed = 0f;
    }

    public void incrementTimesHit()
    {
        timesHitText.text = "HITS TAKEN : " + Mathf.RoundToInt(timesHitValue);
        timesHitValue = Mathf.Lerp(timesHitValue, GameManager.gm.gameStats.timesHit, inc);
        incrementSpeed = 0f;
    }

    public void incrementDamageTaken()
    {
        damageTakenText.text = "TOTAL DAMAGE TAKEN : " + Mathf.RoundToInt(damageTakenValue);
        damageTakenValue = Mathf.Lerp(damageTakenValue, GameManager.gm.gameStats.totalDamageTaken, inc);
        incrementSpeed = 0f;
    }
    /////////////////


}