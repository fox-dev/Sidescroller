using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager gm;

    
    public float moveSpeed;

    void Start()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }

        moveSpeed = 0f;
        

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
