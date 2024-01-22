using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public Transform PlayerMesh, Camera, orientation;
    public float rotation_speed, walk_speed,inputAngle;
    private float horizontalInput, verticalInput;
    
    public Rigidbody rb;
    public Vector3 inputDir;
    public bool Running;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Run"))
        {
            Running = true;
        }
        if(!Input.GetButton("Run"))
        {
            Running = false;
        }

        Vector3 viewDir = PlayerMesh.position - new Vector3(Camera.position.x, PlayerMesh.position.y, Camera.position.z);
        orientation.forward = viewDir;

         horizontalInput = Input.GetAxis("Horizontal");
         verticalInput = Input.GetAxis("Vertical");
        inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        

        if(inputDir != Vector3.zero)
        {
            PlayerMesh.forward = Vector3.Slerp(PlayerMesh.forward, inputDir.normalized, Time.deltaTime * rotation_speed);
        }


        checkRotationDir();
        AnimateMovement();
        MoveCharacter(Running);

       
    }
    void MoveCharacter(bool runstate)
    {

        if(runstate==true)
        {
            rb.velocity = inputDir * walk_speed *4* Time.deltaTime;
        }
        else
        {
            rb.velocity = inputDir * walk_speed * Time.deltaTime;
        }
        
    }
    void AnimateMovement()
    {
      

        if(horizontalInput==0 && verticalInput ==0)
        {
            anim.SetInteger("motion_state", 0);
        }
        else if(horizontalInput != 0 || verticalInput != 0)
        {
            if(Running==true)
            {
                anim.SetInteger("motion_state", 2);
            }
            if(Running==false)
            {
                anim.SetInteger("motion_state", 1);
            }
        }
    }
    void checkRotationDir()
    {
       
    }
}
