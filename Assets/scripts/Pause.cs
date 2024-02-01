using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool paused;
    public GameObject pauseScreen;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {


            if (paused == false)
            {

                paused = true;
                PauseGame();
                return;
            }
            if (paused == true)
            {
                paused = false;
                Unpausegame();
                return;
            }

        }
        
    }
      public void PauseGame()
      {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
      }
       public  void Unpausegame()
       {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
        Debug.Log("QuitApplication");
        #endif
        Application.Quit();

    }
    
}
