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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

}
