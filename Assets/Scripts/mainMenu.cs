using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void PlayGame ()
    {
        //Jump two indexes because of the two menus in the beginning
        SceneManager.LoadScene(2);
    }
    public void GoToSelectionScr()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void TogglePause()
    {
        SceneManage.TogglePause();
    }

    public void callRotate()
    {
        CameraRotate.rotateCam.Invoke();
    }
}
