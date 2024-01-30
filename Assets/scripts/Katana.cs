using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
    public Character_Controller plyrchr;
    public int katanaNo, katanaDmg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Health>()!=null)
        {
          if(katanaNo==1)
            {
                if (plyrchr.LeftAtt == true)
                {
                    other.gameObject.GetComponent<Health>().TakeDamage(katanaDmg);
                    return;

                }
            }
          if (katanaNo == 2)
            {
                if (plyrchr.RightAtt == true)
                {
                    other.gameObject.GetComponent<Health>().TakeDamage(katanaDmg);
                    return;
                }
            }
        }
    }
   
}
