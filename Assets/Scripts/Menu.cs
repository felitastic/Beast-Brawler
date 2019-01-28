using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Button SettingButton;

    private void Start()
    {
        SettingButton.interactable = false;
    }

    public void Play()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void Settings()
    {
        //nothing yet
    }

    public void Exit()
    {
        Application.Quit();
    }

}
