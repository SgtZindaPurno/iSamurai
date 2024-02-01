using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAi : MonoBehaviour
{

    public Animator AiAnim;
    public bool Patrol, SawPlayer,at01,at02,attacking;
    public Transform PatrolPoint01, PatrolPoint02;
    public NavMeshAgent Nav;
    public float Dist01,Dist02, timeAt2,timeAt1;
    public float WaitTime;
    public float DistPlayer;
    public Transform player;
    public float StoppingDist;
    public int attackValue; //1 for light, 2 for heavy and 3 for mixed
    public bool underAttack;
    public int lightDmg, heavyDmg;//alwasy even numbers
    public float rand;
    private Character_Controller PlyHP;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        PlyHP = player.GetComponent<Character_Controller>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {

            if (PlyHP.playerHealth > 0)
            {
                if (SawPlayer == false)
                {
                    if (Patrol == true)
                    {
                        Patroling();
                    }
                }
                if (SawPlayer == true)
                {
                    Nav.stoppingDistance = StoppingDist;
                    DistPlayer = Vector3.Distance(transform.position, player.position);
                    if (DistPlayer >= StoppingDist)
                    {

                        MoveToPlayer();
                    }
                    if (DistPlayer < StoppingDist)
                    {
                        Vector3 Facetowards = new Vector3(player.position.x, this.transform.position.y, player.position.z);
                        this.transform.LookAt(Facetowards);


                        if (attacking == false)
                        {
                            AttackPlayer(attackValue);
                            Debug.Log("start attak");
                        }
                    }

                }
                if (underAttack == true)
                {
                    SawPlayer = true;
                }
            }
            if (PlyHP.playerHealth <= 0)
            {
                SawPlayer = false;
                underAttack = false;
                AiAnim.SetInteger("motion", 0);
            }



        }
    }
    public void DmgPlayer()
    {
        if (DistPlayer <= StoppingDist)
        {

            if (attackValue == 1)
            {
                PlyHP.playerTakeDmg(lightDmg,1);
            }
            if (attackValue == 2)
            {
                PlyHP.playerTakeDmg(heavyDmg,2);
            }
            if (attackValue == 3)
            {
                if (rand <= 0.5)
                {
                    PlyHP.playerTakeDmg(lightDmg,1);
                }
                else if (rand > 0.5)
                {
                    PlyHP.playerTakeDmg(heavyDmg,2);
                }
            }
        }

    }
    void AttackPlayer(int attacktype)
    {
        attacking = true;
        
        if(attacktype==1)
        {
            AiAnim.SetInteger("motion", 3);
           
        }
        if(attacktype==2)
        {
            AiAnim.SetInteger("motion", 4);
            
        }
        if (attacktype==3)
        {
             rand = Random.value;
            if(rand<=0.5)
            {
                AiAnim.SetInteger("motion", 3);
            }
            else if(rand>0.5)
            {
                AiAnim.SetInteger("motion", 4);
            }
        }
       
    }
        
    void MoveToPlayer()
    {
        attacking = false;
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
