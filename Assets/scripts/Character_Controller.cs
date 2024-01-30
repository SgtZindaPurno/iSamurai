using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Character_Controller : MonoBehaviour
{
    public Transform Player, Camera, orientation;
    public float rotation_speed, walk_speed;
    public float orientAngle,Jumpforce;
    private float horizontalInput, verticalInput;
    public string HitCollider;
    public Rigidbody rb;
    public Vector3 inputDir;
    public bool Running, Jumping, grounded,zoomed;
    public Animator anim;
    public Camera CamScript;
    public float LongPressDuration=1f, RpressStartTime,LpressStartTime;
    public bool LongPressingRight, LongPressingLeft;
    public Weapons wpn;
    public bool LeftAtt,RightAtt;

    public int AmmoLeft, AmmoRight;
    public TextMeshProUGUI LeftCounter, RightCounter, HealthCounter;

    public int playerHealth = 100;
    
    
    
   
    // Start is called before the first frame update
    void Start()
    {
        LeftCounter.text = AmmoLeft.ToString();
        RightCounter.text = AmmoRight.ToString();
        HealthCounter.text = "+" + playerHealth.ToString();
            
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit groundRay; 
        Physics.Raycast(transform.position, -Vector3.up, out groundRay);
        

        if(groundRay.collider == null)
        {
            HitCollider = "nothing";
        }

        if (groundRay.collider!=null)
        {
            HitCollider = groundRay.collider.name + groundRay.distance;
            Debug.DrawRay(transform.position, -Vector3.up * groundRay.distance, Color.yellow);

            if (groundRay.distance < 0.9f)
            {
                grounded = true;
                Jumping = false;
                anim.SetBool("jump", false);
            }
            else if(groundRay.distance>=0.9f)
            {
                grounded = false;
            }
        }
        if(Input.GetButtonDown ("Jump"))
        {
            if (grounded == true)
            {
                if (Jumping == false)
                {
                    Jump();
                   
                }
            }
        }

        



        

        Vector3 viewDir = Player.position - new Vector3(Camera.position.x, Player.position.y, Camera.position.z);
        orientation.forward = viewDir;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (Input.GetButton("Fire1") || Input.GetButton("Fire2"))
        {
            if (LongPressingLeft == true || LongPressingRight == true)
            {
                zoomed = true;
            }
        }
        if (!Input.GetButton("Fire1") && !Input.GetButton("Fire2"))
        {
            zoomed = false;
          
        }

        if(Input.GetButtonUp("Fire1"))
        {
            if (Jumping == false)
            {
                if (Running == false)
                {
                    if (wpn.weaponLeft == 0)
                    {
                        if (LpressStartTime < 0.15f)
                        {
                            if (LeftAtt == false)
                            {
                                Attack(1);
                                LeftAtt = true;
                            }
                        }
                    }
                    if (wpn.weaponLeft == 1)
                    {
                        if (LongPressingLeft == true)
                        {
                            if (AmmoLeft > 0)
                            {
                                Shoot(1);
                                AmmoLeft--;
                                LeftCounter.text = AmmoLeft.ToString();
                                RightCounter.text = AmmoRight.ToString();

                            }
                        }
                    }
                   
                }
            }
        }
        if (Input.GetButtonUp("Fire2"))
        {
            if (Jumping == false)
            {
                if(Running==false)
                {
                    if (wpn.weaponRight == 0)
                    {
                        if (RpressStartTime < 0.15f)
                        {
                            if (RightAtt == false)
                            {
                                Attack(2);
                                RightAtt = true;
                            }
                        }
                    }
                    if (wpn.weaponRight == 1)
                    {
                        if (LongPressingRight == true)
                        {
                            if (AmmoRight > 0)
                            {
                                Shoot(2);
                                AmmoRight--;
                                LeftCounter.text = AmmoLeft.ToString();
                                RightCounter.text = AmmoRight.ToString();

                            }
                        }
                    }
                    
                }
                
            }
           

            
        }
      
        if(Input.GetButton("Fire1"))
        {
            LpressStartTime += Time.deltaTime;


            if (LpressStartTime >= LongPressDuration)
            {
                LongPressingLeft = true;
              
            }
        }

        if (Input.GetButton("Fire2"))
        {
            RpressStartTime += Time.deltaTime;


            if (RpressStartTime >= LongPressDuration)
            {
                LongPressingRight = true;
               
            }
        }

        if (Input.GetButton("Run"))
        {
            if (LeftAtt == false && RightAtt == false)
            {


                if (zoomed == false)
                {
                    Running = true;
                }
                if (zoomed == true)
                {
                    Running = false;
                }
            }
        }
        if (!Input.GetButton("Run"))
        {
            Running = false;
        }

        if (zoomed==true)
        {
            
            Player.forward =  orientation.forward;
            CamScript.Zoom();
            if(LongPressingLeft==true)
            {
                if(wpn.weaponLeft==1)
                {
                    wpn.AimWeapon(1);
                }
                
                
                anim.SetBool("zoomLeft", true);
            }
            if(LongPressingRight==true)
            {
                if(wpn.weaponRight==1)
                {
                    wpn.AimWeapon(2);
                }

                
                anim.SetBool("zoomRight", true);
            }
        }
        if (zoomed == false)
        {
            if (inputDir != Vector3.zero)
            {
                Player.forward = Vector3.Slerp(Player.forward, inputDir.normalized, Time.deltaTime * rotation_speed);
            }

            CamScript.ZoomOut();
        }




        AnimateMovement();
        MoveCharacter(Running);


    }
    private void LateUpdate()
    {
        if (!Input.GetButton("Fire1"))
        {
          

             LongPressingLeft = false;

            LpressStartTime = 0f;
            anim.SetBool("zoomLeft", false);

            if(wpn.weaponLeft==1)
            {
                wpn.AimDown(1);
            }


        }
        if (!Input.GetButton("Fire2"))
        {
            

            LongPressingRight = false;

            RpressStartTime = 0f;
             anim.SetBool("zoomRight", false);


            if (wpn.weaponRight == 1)
            {
                wpn.AimDown(2);
            }
        }
    }
    void Jump()
    {
        anim.SetBool("jump", true);
        Jumping = true;
        rb.AddForce(transform.up * Jumpforce);
       
        
    }
    void Attack(int side)
    { 
        wpn.InitAttack(side);
    }  
    void Shoot(int Side)
    {
        wpn.InitShoot(Side);
    }

    void MoveCharacter(bool runstate)
    {


        if (runstate == true)
        {


            rb.MovePosition(transform.position + (inputDir * walk_speed * 4 * Time.deltaTime));
        }
        else
        {
          
                rb.MovePosition(transform.position + (inputDir * walk_speed * Time.deltaTime));
            
        }

    }
    void AnimateMovement()
    {


        if (horizontalInput == 0 && verticalInput == 0)
        {
            anim.SetInteger("motion_state", 0);
           
            
        }
        else if (horizontalInput != 0 || verticalInput != 0)
        {
            if (Running == true)
            {
                anim.SetInteger("motion_state", 2);
            }
            if (Running == false)
            {
                anim.SetInteger("motion_state", 1);
            }
           

        }
    }
    public void playerTakeDmg(int Damage)
    {
        playerHealth = playerHealth - Damage;
        if(playerHealth<=0)
        {
            Debug.Log("PlayerDead");
        }
    }

   
}
