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
	
	void OnEnable()
    {
        StartCoroutine(slidePanel());
    }

    void OnDisable()
    {
        resultsPanel.localPosition = offScreenPos;
    }

    IEnumerator slidePanel()
    {
        float timeToStart = Time.time;

        while(resultsPanel.localPosition != onScreenPos)
        {
            resultsPanel.localPosition = Vector3.Lerp(resultsPanel.localPosition, onScreenPos, Time.time - timeToStart);
            yield return null;
        }
        
    }
}
