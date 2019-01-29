using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// It's the game manager. It manages the game. Duh.
/// </summary>

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Tooltip("Countdown time for the first stage")]
    public float TimerStage1 = 30f;
    public float TimerStage2 = 30f;

    public eGameMode GameMode = eGameMode.Running;
    public eStage Stage;


    [Header("Menu Drag n Drop")]
    public GameObject GameOverScreen;
    public GameObject MatchOverScreen;
    public GameObject RestartButton;
    public GameObject PauseScreen;

    public Image hpBarGreen1;
    public Image hpBarRed1;
    public Image hpBarGreen2;
    public Image hpBarRed2;

    public Image clock;
    public Text timerText;
    public Text MatchWinText;
    public Text StageWinText;

    public Transform Stage1UpLeft;
    public Transform Stage1DownRight;
    public LayerMask Ground;

    [Header("Other values")]
    public float lerpTimer;
    public float lerpCooldown = 1f;  
    public bool lerpUI = false;
    public float delay = 2f;    //temporary delay for timers

    [Header("For the Countdown")]
    public float countdown;
    public float maxTime;   //Stage maxTime for Lerp

    [Header("Do not touch")]
    public GameObject Player1 = null;
    public GameObject Player2 = null;
    public GameObject winner = null;
    public GameObject loser = null;
    public int P1Wins;
    public int P2Wins;
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

        Stage = eStage.Stage1;
        if (Stage == eStage.Stage1)
        {
            maxTime = TimerStage1;
            countdown = TimerStage1;
        }

        else if (Stage == eStage.Stage2)
        {
            maxTime = TimerStage1;
            countdown = TimerStage2;
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
        clock.fillAmount = countdown;
    }

    void Update()
    {
        P1Wins = Player1.GetComponent<Player>().wins;
        P2Wins = Player2.GetComponent<Player>().wins;
        UpdateHits();

        TestInput();

        switch (GameMode)
        {
            case eGameMode.Running:
                Unpause();
                
                LerpTiming();
                StageCountdown();

                if (Input.GetButtonDown("Start"))
                {
                    GameMode = eGameMode.Pause;
                    Pause();
                }

                break;
            case eGameMode.Pause:
                PauseScreen.gameObject.SetActive(true);
                if (Input.GetButtonDown("Start"))
                    GameMode = eGameMode.Running;
                break;
            case eGameMode.MatchOver:
                StartCoroutine(MatchOver(winner, loser));
                StageCountdown();
                LerpUI();
                break;
            case eGameMode.GameOver:
                GameOver();
                StartCoroutine(MatchOver(winner, loser));
                //Pause();
                //UpdateUI();
                break;
            default:
                break;
        }
    }

    void TestInput() //HACK testshit
    {
        if (Input.GetKeyDown("space"))
            print("gimme space");
    }

    //Countdown of the stages
    void StageCountdown()
    {
        int curtime = Mathf.RoundToInt(countdown);   

        //For when time has run out
        if (countdown <= 0)
        {
            //TODO counter for how many matches have been played
            if (Hitpoints1 < Hitpoints2)
            {
                print("p2 wins");
                winner = Player2;
                loser = Player1;
                GameMode = eGameMode.MatchOver;
                //StartCoroutine(MatchOver(winner, loser));
            }
            else if (Hitpoints2 < Hitpoints1)
            {
                print("p1 wins");
                winner = Player1;
                loser = Player2;
                GameMode = eGameMode.MatchOver;
                //StartCoroutine(MatchOver(winner, loser));
            }
            else if (Hitpoints1 == Hitpoints2)
            {
                print("Draw, Hot Potato!");
            }
        }
        else if (Player1.GetComponent<Player>().state == ePlayerState.Dead || Player2.GetComponent<Player>().state == ePlayerState.Dead)
        {
            //TODO counter for how many matches have been played
            if (Hitpoints1 < Hitpoints2)
            {
                print("p2 wins");
                winner = Player2;
                loser = Player1;
                GameMode = eGameMode.MatchOver;
                //StartCoroutine(MatchOver(winner, loser));
            }
            else if (Hitpoints2 < Hitpoints1)
            {
                print("p1 wins");
                winner = Player1;
                loser = Player2;
                GameMode = eGameMode.MatchOver;
                //StartCoroutine(MatchOver(winner, loser));
            }
        }
        else
        {
            countdown -= Time.deltaTime;

            //For the timer image and text countdown
            if (curtime >= 0)
            {
                clock.fillAmount = Mathf.Lerp(clock.fillAmount, curtime / maxTime, Time.deltaTime * 3);
                timerText.text = (countdown).ToString("0");
            }
            else if (curtime <= 0)
            {
                clock.fillAmount = 0;
                timerText.text = ("0");
            }
        }
    }

    //Runs after a match has finished
    public IEnumerator MatchOver(GameObject winner, GameObject loser)
    {
        print("Match over");
        loser.GetComponent<Player>().hitPoints = 0f;
        UpdateHP();
        winner.GetComponent<Player>().wins += 1;
        name = winner.name;
        MatchWinText.text = ("Match Over \n" + name+" wins");
        if (winner == Player1)
        {
            hpBarRed1.fillAmount = Mathf.Lerp(hpBarRed1.fillAmount, Hitpoints1 / maxHitpoints1, Time.deltaTime * 5);
        }
        else if (winner == Player2)
        {
            hpBarRed2.fillAmount = Mathf.Lerp(hpBarRed2.fillAmount, Hitpoints2 / maxHitpoints2, Time.deltaTime * 5);
        }
        yield return new WaitForSeconds(delay);    

        //TODO set if cond for final match
        MatchOverScreen.gameObject.SetActive(true);
        Pause();
    }

    //Sets HP and the timer for the red HP bar
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

    //Called by player on hit, sets green HP bar
    public void UpdateHits()
    {
        hpBarGreen1.fillAmount = Hitpoints1 / maxHitpoints1;
        hpBarGreen2.fillAmount = Hitpoints2 / maxHitpoints2;
    }

    //Red HP bar timer
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
    
    //For the slow decrease of the red health bar
    void LerpUI()
    {
        //print("Red1 new: "+hpBarRed1.fillAmount);
        //print("Red2 new: " + hpBarRed2.fillAmount);
        hpBarRed1.fillAmount = Mathf.Lerp(hpBarRed1.fillAmount, Hitpoints1 / maxHitpoints1, Time.deltaTime * 3);
        hpBarRed2.fillAmount = Mathf.Lerp(hpBarRed2.fillAmount, Hitpoints2 / maxHitpoints2, Time.deltaTime * 3);
    }

    //Pauses the game time
    public void Pause()
    {
        Time.timeScale = 0;
    }

    //Unpauses the game time and deactivates Pause Screen
    public void Unpause()
    {
        PauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    
    //Update to be run after the last match and show a result screen
    public void GameOver()
    {
        Pause();
    }

    private IEnumerator GameOverThing()
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
        SceneManager.LoadScene("Title");
        GameMode = eGameMode.Running;
    }
}
