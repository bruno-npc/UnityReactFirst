using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playGame (){
        SceneManager.LoadScene("Game");
    }

    public void createUser (){
        Application.OpenURL("www.google.com"); 
    }

    public void exit(){
        Application.Quit();
    }
}
