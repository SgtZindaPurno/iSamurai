using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Health : MonoBehaviour
{
   
    public int HealthPoint=100; 
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

        if(HealthPoint<=0)
        {
            Debug.Log("Die");
        }
    }
}
