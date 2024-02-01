using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;



public class Camera : MonoBehaviour
{
    public float RotationSpeed;
    public Transform cameraPivot;//target is the main camera
    float mouseX, mouseY;
    public Vector3 CamPosOut, CamPosZoom;
    public Transform Cam;
    public MultiAimConstraint[] AimIks;
    
    

    
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Time.timeScale == 1)
        {

            transform.position = cameraPivot.position;
            CamControl();
        }
    }
    public void ResetRotation()
    {
        mouseX = 0f;
        mouseY = 0f;
    }
    public void Zoom()
    {

        Cam.localPosition = Vector3.Slerp(Cam.localPosition, CamPosZoom, 2f);
        
        foreach( MultiAimConstraint m_spt in AimIks)
        {
            m_spt.weight = 0.5f;
        }
    }
    public void ZoomOut()
    {
        Cam.localPosition = Vector3.Slerp(Cam.localPosition, CamPosOut,2f);
        foreach (MultiAimConstraint m_spt in AimIks)
        {
            m_spt.weight = 0f;
        }
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
