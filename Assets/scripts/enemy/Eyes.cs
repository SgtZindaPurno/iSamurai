using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    public Transform EyePoint;
    public bool hitplayer;
    public bool playerInCone;
    public Transform player;
    public EnemyAi ctrller;
    public string hitname;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInCone==true)
        {
            EyePoint.transform.LookAt(player);

            RaycastHit eyeRay;

            Physics.Raycast(EyePoint.position, EyePoint.forward, out eyeRay);
            if(eyeRay.collider!=null)
            {
                hitname = eyeRay.collider.name;

                if(eyeRay.collider.CompareTag("Player"))
                {
                    hitplayer = true;
                    ctrller.SawPlayer = true;

                }
                else
                {
                    hitplayer = false;
                }    
            }
            else
            {
                hitplayer = false;
            }
        }
        else
        {
            hitplayer = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInCone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInCone = false;
        }
    }
}
