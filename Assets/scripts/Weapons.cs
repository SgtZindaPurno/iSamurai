using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public int weaponNo;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitAttack(int arm)
    {
        if (arm == 1)
        {
            anim.SetBool("attackingLeft", true);
        }
        if (arm == 2)
        {
            anim.SetBool("attackingRight", true);
        }
    }
    public void FinishAttack(int ArmNo)
    {
        if (ArmNo == 1)
        {
            anim.SetBool("attackingLeft",false);
        }
        if (ArmNo == 2)
        {
            anim.SetBool("attackingRight", false);
        }
    }
}
