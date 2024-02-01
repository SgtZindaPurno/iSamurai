using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Health : MonoBehaviour
{
    
    public int HealthPoint=100;
    public EnemyAi brain;
    public Animator deathanim;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int dmg)
    {
        HealthPoint = HealthPoint - dmg;

        if (brain != null)
        {
            brain.underAttack = true;
        }
        if(HealthPoint<=0)
        {
            brain.enabled = false;
            deathanim.SetBool("dead", true);
           
        }
    }
    

    
}
