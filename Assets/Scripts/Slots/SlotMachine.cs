using UnityEngine;
using System.Collections;

public class SlotMachine : MonoBehaviour {

    public GameObject s1, s2, s3;

    MeshRenderer slot1, slot2, slot3;

    Material m1, m2, m3;

    Vector2 off1, off2, off3;

    bool occupied;

    bool spin1, spin2, spin3;

    public bool roll; //engage spin

   
    private float[] outcomes = new float[] { 0, 0.25f, 0.50f, 0.75f };

    [SerializeField]
    private int stopVal;
    

	// Use this for initialization
	void Start () {
        slot1 = s1.GetComponent<MeshRenderer>();
        slot2 = s2.GetComponent<MeshRenderer>();
        slot3 = s3.GetComponent<MeshRenderer>();

        m1 = slot1.material;
        m2 = slot2.material;
        m3 = slot3.material;

        off1 = m1.mainTextureOffset;
        off2 = m2.mainTextureOffset;
        off3 = m3.mainTextureOffset;

        spin1 = spin2 = spin3 = false;
        occupied = false;
        roll = false;

        stopVal = 0;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("a"))
        {
            roll = true;
        }

        if (spin1) //Spin slot reel 1 offset
        {
            off1.y += 0.025f;

            if (off1.y > 1f)
            {
                off1.y = 0f;
            }
            
        }
        if (spin2) //Spin slot reel 2 offset
        {
            off2.y += 0.025f;

            if (off2.y > 1f)
            {
                off2.y = 0f;
            }

        }
        if (spin3) //Spin slot reel 3 offset
        {
            off3.y += 0.025f;

            if (off3.y > 1f)
            {
                off3.y = 0f;
            }

        }

        if (!occupied && roll)
        {
            StartCoroutine(stopSpin());
        }





        m1.mainTextureOffset = off1;
        m2.mainTextureOffset = off2;
        m3.mainTextureOffset = off3;

    }

    IEnumerator stopSpin()
    {
        occupied = true;

        roll = true;

        yield return new WaitForSeconds(0f); //Start spinning slot 1;
        spin1 = true;

        yield return new WaitForSeconds(0.5f); //Start spinning slot 2;
        spin2 = true;

        yield return new WaitForSeconds(0.5f); //Start spinning slot 2;
        spin3 = true;

        yield return new WaitForSeconds(3f); //Stop spinning slot 1
        spin1 = false;
        int out1 = Random.Range(0, 4);
        off1.y = outcomes[out1]; //Assign random value outcome

        yield return new WaitForSeconds(0.5f); //Stop spinning slot 2
        spin2 = false;
        int out2 = Random.Range(0, 4);
        off2.y = outcomes[out2]; //Assign random value outcome

        yield return new WaitForSeconds(0.5f); //Stop spinning slot 3
        spin3 = false;
        int out3 = Random.Range(0, 4);
        off3.y = outcomes[out3]; //Assign random value outcome












        roll = false;

        occupied = false;


    }
}
