using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public Transform Player, Camera, orientation;
    public float rotation_speed, walk_speed, inputAngle,angleDiff;
    public float orientAngle;
    private float horizontalInput, verticalInput;
    public int Turning;
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
        if (Input.GetButton("Run"))
        {
            Running = true;
        }
        if (!Input.GetButton("Run"))
        {
            Running = false;
        }

        Vector3 viewDir = Player.position - new Vector3(Camera.position.x, Player.position.y, Camera.position.z);
        orientation.forward = viewDir;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;


        if (inputDir != Vector3.zero)
        {
            Player.forward = Vector3.Slerp(Player.forward, inputDir.normalized, Time.deltaTime * rotation_speed);
        }


        checkRotationDir();
        AnimateMovement();
        MoveCharacter(Running);


    }
    void MoveCharacter(bool runstate)
    {


        if (runstate == true)
        {
            rb.velocity = inputDir * walk_speed * 4 * Time.deltaTime;
        }
        else
        {
            rb.velocity = inputDir * walk_speed * Time.deltaTime;
        }

    }
    void AnimateMovement()
    {


        if (horizontalInput == 0 && verticalInput == 0)
        {
            anim.SetInteger("motion_state", 0);
            Turning = 0;
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
    void checkRotationDir()
    {
        
        inputAngle = Mathf.Atan2(-horizontalInput, verticalInput) * Mathf.Rad2Deg ;
        orientAngle = orientation.localEulerAngles.y;
        orientAngle = (orientAngle + 180f) % 360f - 180f;
        angleDiff = Mathf.Abs(inputAngle - orientAngle);
       
        if (angleDiff > 20)
        {
            if (orientation.localRotation.y < inputAngle)
            {
                Turning = 2;//left
            }
            if (orientation.localRotation.y > inputAngle)
            {
                Turning = 1; //right
            }
        }
        else if(angleDiff<20)
        {
            Turning = 0;
        }
    }
}
