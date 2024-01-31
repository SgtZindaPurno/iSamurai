using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAi : MonoBehaviour
{

    public Animator AiAnim;
    public bool Patrol, SawPlayer,at01,at02;
    public Transform PatrolPoint01, PatrolPoint02;
    public NavMeshAgent Nav;
    public float Dist01,Dist02, timeAt2,timeAt1;
    public float WaitTime;
    public float DistPlayer;
    public Transform player;
    public float StoppingDist;
    
    public int attackValue; //1 for light, 2 for heavy and 3 for mixed
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(SawPlayer == false)
        {
            if(Patrol == true)
            {
                Patroling();
            }
        }
       if(SawPlayer==true)
        {
            Nav.stoppingDistance = StoppingDist;
            DistPlayer = Vector3.Distance(transform.position, player.position);
            if(DistPlayer>StoppingDist)
            {
                
                MoveToPlayer();
            }
            if(DistPlayer<=StoppingDist)
            {
                Vector3 Facetowards = new Vector3(player.position.x, this.transform.position.y, player.position.z);
                this.transform.LookAt(Facetowards);


                
                {
                    AttackPlayer(attackValue);
                   
                }
            }

        }

    }
    void AttackPlayer(int attacktype)
    {
      

        if(attacktype==1)
        {
            AiAnim.SetInteger("motion", 3);
            //give dmg and finsh attack
        }
        if(attacktype==2)
        {
            AiAnim.SetInteger("motion", 4);
            //give dmg and finsh attack
        }
        if (attacktype==3)
        {
            float rand = Random.value;
            if(rand<=0.5)
            {
                //lightattack
            }
            else if(rand>0.5)
            {
                //heavyattack
            }
        }
       
    }
        
    void MoveToPlayer()
    {
       
        Nav.SetDestination(player.position);
        AiAnim.SetInteger("motion", 2);
        Nav.speed = 4f;
    }
    void Patroling()
    {
        
        Dist01 = Vector3.Distance(transform.position, PatrolPoint01.position);
         Dist02 = Vector3.Distance(transform.position, PatrolPoint02.position);

        if(Dist01<=1)
        {
            at01 = true;
            at02 = false;
        }
        if(Dist02<=1)
        {
            at02 = true;
            at01 = false;
        }
        if(at01==true)
        {
            timeAt1 = timeAt1 + Time.deltaTime;
            timeAt2 = 0;
        }
        if (at02 == true)
        {
            timeAt2 = timeAt2 + Time.deltaTime;
            timeAt1 = 0;
        }




        if (at01==true&&timeAt1>=WaitTime)
        {
            Nav.SetDestination(PatrolPoint02.position);
            AiAnim.SetInteger("motion", 1);
        }
        if(at02==true&&timeAt2>=WaitTime)
        {
            Nav.SetDestination(PatrolPoint01.position);
            AiAnim.SetInteger("motion", 1);
        }

        if (at01 == true && timeAt1 < WaitTime)
        {
            
            AiAnim.SetInteger("motion", 0);
        }
        if (at02 == true && timeAt2 < WaitTime)
        {
            
            AiAnim.SetInteger("motion", 0);
        }



        if (at01==false && at02==false)
        {
            Nav.SetDestination(PatrolPoint01.position);
            AiAnim.SetInteger("motion", 1);
        }
        
    }
}
