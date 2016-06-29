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

	// Use this for initialization
	void Start () {
        offScreenPos = panels[1].position;
        onScreenPos = panels[0].position;
        activePanel = panels[0];
        inactivePanel = panels[1];
	}
	
	// Update is called once per frame
	void Update () {
        activePanel.position = Vector3.Lerp(activePanel.position, onScreenPos, 10f * Time.deltaTime);
        inactivePanel.position = Vector3.Lerp(inactivePanel.position, offScreenPos, 10f * Time.deltaTime);
    }

    public void close()
    {
        //Close is already handled by the MainUIController in UIOverlay
        GameManager.gm.state = GameManager.gameState.ready;
       
    }

    public void nextPanel()
    {
        _panelIndex++;
        if(_panelIndex >= panels.Length)
        {
            _panelIndex = 0;
        }
        inactivePanel = activePanel;
        activePanel = panels[_panelIndex];
        
    }

    public void prevPanel()
    {
        _panelIndex--;
        if (_panelIndex < 0)
        {
            _panelIndex = panels.Length - 1;
        }
        inactivePanel = activePanel;
        activePanel = panels[_panelIndex];

    }
}
