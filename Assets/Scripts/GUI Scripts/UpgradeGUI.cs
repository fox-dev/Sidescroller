using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradeGUI : MonoBehaviour {

    public RectTransform[] panels;

    private RectTransform activePanel;
    private RectTransform inactivePanel;
   
    private Vector3 offScreenPos;
    private Vector3 onScreenPos;

    private int _panelIndex = 0;

    private bool occupied = false; // for coroutine
    private float speed = 5f;

	// Use this for initialization
	void Start () {
        offScreenPos = panels[1].localPosition;
        onScreenPos = panels[0].localPosition;
        activePanel = panels[0];
        inactivePanel = panels[1];
	}
	
    public void close()
    {
        //Close is already handled by the MainUIController in UIOverlay
        GameManager.gm.state = GameManager.gameState.ready;
       
    }

    public void nextPanel()
    {
        if (!occupied)
        {
            _panelIndex++;
            if (_panelIndex >= panels.Length)
            {
                _panelIndex = 0;
            }
            inactivePanel = activePanel;
            activePanel = panels[_panelIndex];

            StartCoroutine(slidePanels());
        }
        
    }

    public void prevPanel()
    {
        if (!occupied)
        {
            _panelIndex--;
            if (_panelIndex < 0)
            {
                _panelIndex = panels.Length - 1;
            }
            inactivePanel = activePanel;
            activePanel = panels[_panelIndex];

            StartCoroutine(slidePanels());
        }
        

    }

    IEnumerator slidePanels()
    {
        occupied = true;
        float timeToStart = Time.time;
        while(activePanel.localPosition != onScreenPos && inactivePanel.localPosition != offScreenPos)
        {
            activePanel.localPosition = Vector3.Lerp(activePanel.localPosition, onScreenPos,  speed * (Time.time - timeToStart));
            inactivePanel.localPosition = Vector3.Lerp(inactivePanel.localPosition, offScreenPos, speed * (Time.time - timeToStart));

            yield return null;
        }

    
        occupied = false;
        
    }

}
