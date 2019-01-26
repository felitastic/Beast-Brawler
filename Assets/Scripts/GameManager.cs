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
    public GameObject GameOverScreen;
    public GameObject RestartButton;
    public GameObject PauseScreen;

    public Image hpBarGreen1;
    public Image hpBarRed1;
    public Image hpBarGreen2;
    public Image hpBarRed2;

    [Header("Other values")]
    public float lerpTimer;
    public float lerpCooldown = 1f;  
    public bool lerpUI = false;
    public float delay = 2f;    //for old menu stuff

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

        hpBarRed1.fillAmount = maxHitpoints1;
        hpBarRed2.fillAmount = maxHitpoints2;
    }

    void Update()
    {
        UpdateHits();

        LerpTiming();

        TestInput();

        switch (GameMode)
        {
            case eGameMode.Running:
                Unpause();

                if (Input.GetButtonDown("Start"))
                {
                    GameMode = eGameMode.Pause;
                    Pause();
                }
                break;
            case eGameMode.Pause:
                if (Input.GetButtonDown("Start"))
                    GameMode = eGameMode.Running;
                break;
            case eGameMode.StageClear:
                break;
            case eGameMode.GameOver:
                GameOver();
                //Pause();
                //UpdateUI();
                break;
            default:
                break;
        }

        if (Player1.GetComponent<Player>().state == ePlayerState.Dead && hpBarRed1.fillAmount == 0)
            GameMode = eGameMode.GameOver;
        if (Player2.GetComponent<Player>().state == ePlayerState.Dead && hpBarRed2.fillAmount == 0)
            GameMode = eGameMode.GameOver;

    }

    void LerpTiming()
    {
        if (lerpTimer > 0)
        {
            lerpTimer -= Time.deltaTime;
        }
        if (lerpTimer == 0)
        {
            LerpUI();
        }
        if (lerpTimer < 0)
        {
            lerpTimer = 0f;
        }
    }

    void TestInput() //HACK testshit
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

        //reset lerptimer if damage is taken
        if (lerpTimer <= 0)
            lerpTimer = lerpCooldown;
    }

    //called by player on hit
    public void UpdateHits()
    {
        hpBarGreen1.fillAmount = Hitpoints1 / maxHitpoints1;
        hpBarGreen2.fillAmount = Hitpoints2 / maxHitpoints2;
        //print("Red1: " + hpBarRed1.fillAmount+ " Green1: " + hpBarGreen1.fillAmount);
        //print("Red1: " + hpBarRed1.fillAmount + " Green2: " + hpBarGreen2.fillAmount);
    }

    void LerpUI()   //TODO lerpt schaden von roter bar runter
    {
        //print("Red1 new: "+hpBarRed1.fillAmount);
        //print("Red2 new: " + hpBarRed2.fillAmount);
        hpBarRed1.fillAmount = Mathf.Lerp(hpBarRed1.fillAmount, Hitpoints1 / maxHitpoints1, Time.deltaTime * 3);
        hpBarRed2.fillAmount = Mathf.Lerp(hpBarRed2.fillAmount, Hitpoints2 / maxHitpoints2, Time.deltaTime * 3);
    }


    public void Pause()
    {
        PauseScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        PauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(delay);
        GameOverScreen.gameObject.SetActive(true);
        Pause();
        yield return new WaitForSeconds(delay);
        RestartButton.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameMode = eGameMode.Running;
    }

    public void ToTitle()
    {
        //SceneManager.LoadScene("Title");
        //GameMode = eGameMode.Running;
    }
}
