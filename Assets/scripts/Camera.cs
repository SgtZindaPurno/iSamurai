using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float RotationSpeed;
   public Transform cameraPivot;//target is the main camera
    float mouseX, mouseY;

    
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = cameraPivot.position;
        CamControl();
    }
    public void ResetRotation()
    {
        mouseX = 0f;
        mouseY = 0f;
    }

    void CamControl()
    {


        mouseX += Input.GetAxis("Mouse X") * RotationSpeed * 2f; 
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed * 2f;
        mouseY = Mathf.Clamp(mouseY, -75, 50);


        float Yrot = transform.localRotation.x + mouseY;
        
        float Xrot = transform.localRotation.y + mouseX;
        transform.localRotation = Quaternion.Euler(Yrot, Xrot, 0);


    }
}
