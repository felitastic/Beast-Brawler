using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;




public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public eGameMode GameMode = eGameMode.Running;

    [Header("Menu Drag n Drop")]
    //public GameObject GameOverScreen;
    //public GameObject RestartButton;
    //public GameObject PauseScreen;
    //public Camera Cam;

    public Image hpBarGreen1;
    public Image hpBarRed1;
    public Image hpBarGreen2;
    public Image hpBarRed2;

    [Header("Other values")]
    public float hpTime = 0f;
    public float hpDelay = 0.3f;
    public float delay = 2f;
    public bool lerpUI = false;

    [Header("Do not touch")]
    public GameObject Player1 = null;
    public GameObject Player2 = null;
    public float Hitpoints1;
    public float Hitpoints2;
    public float maxHitpoints1;
    public float maxHitpoints2;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            //Destroy this - singleton pattern, there can only ever be one instance of GameManager
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<Player>().PlayerIndex == 0)
            {
                Player1 = player;
            }
            if (player.GetComponent<Player>().PlayerIndex == 1)
            {
                Player2 = player;
            }
        }

        maxHitpoints1 = Player1.GetComponent<Player>().maxHitPoints;
        maxHitpoints2 = Player2.GetComponent<Player>().maxHitPoints;
    }
    
    void Update()
    {
        UpdateHP();
        LerpUI();

        if (Time.deltaTime == hpTime)
        {
            lerpUI = true;
            hpTime = 0f;
        }

        TestInput(); //HACK: Für Testzwecke    

        switch (GameMode)
        {
            case eGameMode.Running:
                //Unpause();
                break;
            case eGameMode.Pause:
                //Pause();
                break;
            case eGameMode.StageClear:
                break;
            case eGameMode.GameOver:
                //UpdateUI();
                break;
            default:
                break;
        }
    }

    void TestInput()
    {
        if (Input.GetKeyDown("space"))
            print("gimme space");
    }

    public void UpdateHP()
    {
        Hitpoints1 = Player1.GetComponent<Player>().hitPoints;
        Hitpoints2 = Player2.GetComponent<Player>().hitPoints;
        if (Hitpoints1 > maxHitpoints1)
            Hitpoints1 = maxHitpoints1;
        if (Hitpoints2 > maxHitpoints2)
            Hitpoints2 = maxHitpoints2;
    }

    //called by player on hit
    public void UpdateHits()
    {
        lerpUI = false;
        hpBarGreen1.fillAmount = Hitpoints1 / maxHitpoints1;
        hpBarGreen2.fillAmount = Hitpoints2 / maxHitpoints2;
        hpTime = Time.deltaTime + hpDelay;
    }    

    void LerpUI()   //TODO lerpt schaden von roter bar runter
    {
        if (lerpUI)
        {
            print("Lerp HP Red");
            hpBarRed1.fillAmount = Mathf.Lerp(hpBarRed1.fillAmount, Hitpoints1 / maxHitpoints1, Time.deltaTime * 3);
            hpBarRed2.fillAmount = Mathf.Lerp(hpBarRed2.fillAmount, Hitpoints2 / maxHitpoints2, Time.deltaTime * 3);
        }
    }


    public void Pause()
    {
        //PauseScreen.gameObject.SetActive(true);
        //Time.timeScale = 0;
    }

    public void Unpause()
    {
        //PauseScreen.gameObject.SetActive(false);
        //Time.timeScale = 1;
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(delay);
        //GameOverScreen.gameObject.SetActive(true);
        //Pause();
        //yield return new WaitForSeconds(delay);
        //RestartButton.gameObject.SetActive(true);
    }

    public void Continue()
    {
        //GameMode = eGameMode.Running;
    }

    public void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //GameMode = eGameMode.Running;
    }

    public void ToTitle()
    {
        //SceneManager.LoadScene("Title");
        //GameMode = eGameMode.Running;
    }
}
