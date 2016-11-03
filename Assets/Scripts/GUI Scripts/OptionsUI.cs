using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsUI : MonoBehaviour {

    //The ad controller;
    private AdController adController;

    //ShowAd buttons
    public Button showAds_On, showAds_Off;

    //Volume slider
    public Slider volSlider;

    //ResetHighscore pane//
    public Button resetHighScoreButton;
    public RectTransform confirmationPanel;
    public Text highScoreText;
    public Button yesButton, noButton;

    ///////////////////////
     
	// Use this for initialization
	void Start () {

        GameObject adControllerObject = GameObject.FindWithTag("AdController");
        if (adControllerObject != null)
        {
            adController = adControllerObject.GetComponent<AdController>();
        }
        if (adController == null)
        {
            Debug.Log("Cannot find 'AdController' script");
        }

        volSlider.value = AudioManager.current.ml.volume;

    }

    void OnEnable()
    {
        if(GameManager.highScore == 0)
        {
            resetHighScoreButton.interactable = false;
        }
        else
        {
            resetHighScoreButton.interactable = true;
        }
    }


	

    //Used by the options button
    public void showOptions()
    {
        this.gameObject.SetActive(true);
    }
    //Used by ">" button on options panel
    public void closeOptions()
    {
        this.gameObject.SetActive(false);
    }

    //////////////////////////////
    ///SHOW AD BUTTON FUNCTIONS///
    //////////////////////////////

    public void turnOffAds()
    {
        showAds_On.interactable = true;
        showAds_Off.interactable = false;
    }

    public void turnOnAds()
    {
        showAds_On.interactable = false;
        showAds_Off.interactable = true;
    }

    //////////////////////////////
    ///VOLUME FUNCTIONS ////////// 
    //////////////////////////////

    //Called when slider changes
    public void adjustVolume()
    {
        AudioManager.current.adjustMainListenerVolume(volSlider.value);
    }

    //////////////////////////////
    ///Reset Highscore Functions//
    //////////////////////////////

    public void toggleConfirmationPanel()
    {
        if(confirmationPanel.gameObject.activeSelf)
        {
            confirmationPanel.gameObject.SetActive(false);
            highScoreText.text = GameManager.highScore.ToString();
        }
        else
        {
            confirmationPanel.gameObject.SetActive(true);
            highScoreText.text = GameManager.highScore.ToString();
        }
        
    }

    public void resetHighScore()
    {
        GameManager.resetHighScore();
        toggleConfirmationPanel();
    }


}
