using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    
   
    // Start is called before the first frame update
    void Start()
    {

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
            if (LpressStartTime < 0.1f)
            {
                if (Running == false)
                {
                    if(Jumping==false)
                    {
                        Attack(1);
                    }
                   
                }
            }
        }
        if (Input.GetButtonUp("Fire2"))
        {
            if (RpressStartTime < 0.1f)
            {
                if(Running==false)
                {
                    if(Jumping==false)
                    {
                        Attack(2);
                    }
                }
                
            }
        }


        if (!Input.GetButton("Fire1") )
        {
           
            LongPressingLeft = false;
           
            LpressStartTime = 0f;
           
        }
        if (!Input.GetButton("Fire2"))
        {
           

            LongPressingRight = false;
           
            RpressStartTime = 0f;
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
            if (zoomed==false)
            {
                Running = true;
            }
            if (zoomed==true)
            {
                Running = false;
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

   
}
