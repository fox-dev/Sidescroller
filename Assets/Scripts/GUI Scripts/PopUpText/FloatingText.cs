using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//Floating text for collectibles
public class FloatingText : MonoBehaviour {

    public Text text;
    private Animator ani;

    void Awake()
    {
        ani = text.GetComponent<Animator>();
        StartCoroutine(setInactive());
    }

    void OnEnable()
    {
        StartCoroutine(setInactive());
    }

    public void setText(string s)
    {
        text.text = s;
    }

    IEnumerator setInactive()
    {
        yield return new WaitForSeconds(0.75f);
        this.gameObject.SetActive(false);
    }

}
