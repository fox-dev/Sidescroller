using UnityEngine;
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
    private float incrementSpeed = 1f;
    private float inc = 0f;

    // Use this for initialization
    void Start () {
        scoreValue = enemiesDestroyedValue = enemiesMissedValue = damageDoneValue = timesHitValue = damageTakenValue = 0;
    }


    void OnEnable()
    {
        if (GameManager.gm != null)
        {
            StartCoroutine(incrementScore());
            incrementEnemiesDestroyed();
            incrementEnemiesMissed();
            incrementDamageDone();
            incrementTimesHit();
            incrementDamageTaken();
        }
        
    }

    //Button on the results screen
    public void nextButton()
    {
        scoreValue = enemiesDestroyedValue = enemiesMissedValue = damageDoneValue = timesHitValue = damageTakenValue = 0;
        GameManager.gm.state = GameManager.gameState.setup;
    }

    //Lerp Value methods//

    IEnumerator incrementScore()
    {
        float timeToStart = Time.time;
        while(scoreValue != GameManager.gm.gameStats.roundScore)
        {
            scoreValue = Mathf.Lerp(scoreValue, GameManager.gm.gameStats.roundScore, incrementSpeed * (Time.time - timeToStart));
            scoreText.text = "SCORE : " + Mathf.RoundToInt(scoreValue);

            yield return null;
        }
        
        
    }
   

    public void incrementEnemiesDestroyed()
    {
      
        enemiesDestroyedValue += GameManager.gm.gameStats.enemiesDestroyed;
        enemiesDestroyedText.text = "ENEMIES DOWNED : " + enemiesDestroyedValue;

    }

    public void incrementEnemiesMissed()
    {
        enemiesMissedText.text = "ENEMIESED MISSED : " + GameManager.gm.gameStats.enemiesMissed;



    }

    public void incrementDamageDone()
    {
        damageDoneText.text = "TOTAL DAMAGE DONE : " + GameManager.gm.gameStats.totalDamageDone;


    }

    public void incrementTimesHit()
    {
        timesHitText.text = "HITS TAKEN : " + GameManager.gm.gameStats.timesHit;
     
    }

    public void incrementDamageTaken()
    {
        damageTakenText.text = "TOTAL DAMAGE TAKEN : " + GameManager.gm.gameStats.totalDamageTaken;



    }
    /////////////////


}
