using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class GetReadyGUI : MonoBehaviour {

    public static GetReadyGUI current;

    [SerializeField]
    public Animator getReadyGUIAnim;

    // Use this for initialization
    void Start () {
        current = this;

        if (getReadyGUIAnim == null)
        {
            Debug.LogError("No bossGuiAnim referenced");
        }
    }
	
}
