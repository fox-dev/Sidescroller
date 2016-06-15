using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager gm;

    void Start()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }
        

    }

    public static void KillEnemy(Enemy enemy)
    {
       
        if(enemy.tag == "Boss")
        {

            Destroy(enemy.gameObject);
            

        }
        else
        {
           
            Destroy(enemy.gameObject);
        }
 
    }




	
}
