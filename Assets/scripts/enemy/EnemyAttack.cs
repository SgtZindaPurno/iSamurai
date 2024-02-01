using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyAi brain;
    public Character_Controller plyr;
    public int scrap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FinishAttack()
    {

        brain.attacking = false;

    }
    public void GiveDmgPlayer()
    {
        brain.DmgPlayer();
    }

    public void GiveReward()
    {
        plyr.AddScrap(scrap);
    }
}
