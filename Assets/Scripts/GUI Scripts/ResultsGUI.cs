using UnityEngine;
using System.Collections;

//ResultsGUI//
public class ResultsGUI : MonoBehaviour {

    public RectTransform resultsPanel, activePanelPos;
    private Vector3 offScreenPos, onScreenPos;

	// Use this for initialization
	void Start ()
    {
        offScreenPos = resultsPanel.localPosition;
        onScreenPos = activePanelPos.localPosition;
	}
	
	void FixedUpdate () {
        resultsPanel.localPosition = Vector3.Lerp(resultsPanel.localPosition, onScreenPos, 10 * Time.deltaTime);
	}

    void OnDisable()
    {
        resultsPanel.localPosition = offScreenPos;
    }
}
