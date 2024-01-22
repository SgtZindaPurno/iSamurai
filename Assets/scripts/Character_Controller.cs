using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public Transform PlayerMesh, Camera, orientation;
    public float rotation_speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewDir = PlayerMesh.position - new Vector3(Camera.position.x, PlayerMesh.position.y, Camera.position.z);
        orientation.forward = viewDir;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(inputDir != Vector3.zero)
        {
            PlayerMesh.forward = Vector3.Slerp(PlayerMesh.forward, inputDir.normalized, Time.deltaTime * rotation_speed);
        }

    }
}
