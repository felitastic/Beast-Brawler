using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject controlmenu;
    public GameObject titlemenu;
    public GameObject creditwindow;
    public GameObject titlebuttons;


    public void Play()
    {
        SceneManager.LoadSceneAsync("Stage1");
    }

    public void Controls()
    {
        controlmenu.gameObject.SetActive(true);
        titlebuttons.gameObject.SetActive(false);
    }
    
    public void CloseControls()
    {
        controlmenu.gameObject.SetActive(false);
        titlebuttons.gameObject.SetActive(true);
    }

    public void CloseCredits()
    {
        creditwindow.gameObject.SetActive(false);
        titlebuttons.gameObject.SetActive(true);
    }

    public void Credits()
    {
        creditwindow.gameObject.SetActive(true);
        titlebuttons.gameObject.SetActive(false);
    }

    public void Exit()
    {
         Application.Quit();
    }
}
