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

    [Tooltip("Countdown time for the first stage - for 30s put 30.5")]
    public float TimerStage1 = 30f;
    public float TimerStage2 = 30f;

    [Header("Enums")]
    public eGameMode GameMode = eGameMode.MatchStart;
    public eStage Stage;

    [Header("Debug")]
    public Text debug;

    [Header("Menu Drag n Drop")]
    public GameObject mainCam;
    public GameObject GameOverScreen;
    public GameObject MatchOverScreen;
    public GameObject RestartButton;
    public GameObject PauseScreen;
    public GameObject MatchOverButtons;
    public SpriteRenderer MatchOverBlackScreen;
    public Transform WinnerPose;
    public Image Pulse1;
    public Image Pulse2;
    public GameObject UI;
    public Image hpBarGreen1;
    public Image hpBarRed1;
    public Image hpBarGreen2;
    public Image hpBarRed2;
    //public Slider potatoSlider;
    public Image clock;
    public Text timerText;
    public Text timerTextShade;
    public Text MatchWinText;
    public Text MatchWinTextShade;
    public Text StageWinText;
    public Text IntroCDText;
    [Header("Firework Transforms")]
    public Transform FW1;
    public Transform FW2;
    public Transform FW3;
    public Transform FW4;
    public Transform FW5;
    public Transform FW6;
    public Transform FW7;
    public Transform FW8;
    public Transform FW9;
    public Transform FW10;
    public Transform FW11;
    public Transform FW12;
    public Transform FW13;

    [Header("Other values")]
    public float lerpTimer;
    public float lerpCooldown = 0.75f;
    public bool lerpUI = false;
    public float delay;    //temporary delay for timers
    public float deathTime;    //for match end so death anim can play
    //for low health warning fadein/out
    public bool warn1 = false;
    public bool warn2 = false;
    //for timer text zoomin/out
    public bool text = false;
    //has time run out before anyone wins
    public bool timeout = false;
    bool end = false;       //for end of match

    //[Header("Potato values")]
    //public float potatoTime = 6f;   //how many seconds without hit before getting potato dmg
    //public float potatoTimer;
    //public float PotatoDmg = 1f;
    //public GameObject Potato;

    [Header("For the match countdown")]
    public Transform P1Respawn;
    public Transform P2Respawn;
    public float countdown;
    public float maxTime;   //Stage maxTime for Lerp
    //public int matchCounter;

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

    private bool slowed;    //for the slowmo coroutine
    public bool startSlowMo;

    void Awake()
    {
        instance = this;
        //if (instance == null)
        //{
        //}
        //else if (instance != this)
        //{
        //    //Destroy this - singleton pattern, there can only ever be one instance of GameManager
        //    Destroy(gameObject);
        //}

        GameMode = eGameMode.MatchStart;
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

        //potatoTimer = potatoTime;
        //Potato = Player1;

        maxHitpoints1 = Player1.GetComponent<Player>().maxHitPoints;
        maxHitpoints2 = Player2.GetComponent<Player>().maxHitPoints;

        Player1.transform.position = P1Respawn.position;
        Player2.transform.position = P2Respawn.position;

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
            case eGameMode.MatchStart:
                StartCoroutine(MatchStartText());
                break;

            #region GameMode Running
            case eGameMode.Running:

                #region Debug Log
                //HACK DebugLog
                if (Input.GetButtonDown("Debug") && debug.gameObject.activeSelf)
                    debug.gameObject.SetActive(false);
                else if (Input.GetButtonDown("Debug"))
                    debug.gameObject.SetActive(true);

                debug.text =
                    (Player1.name + " used: " + Player1.GetComponent<Player>().attack + "\n" +
                    Player1.name + " is in state: " + Player1.GetComponent<Player>().state + "\n" +
                    Player2.name + " used: " + Player2.GetComponent<Player>().attack + "\n" +
                    Player2.name + " is in state: " + Player2.GetComponent<Player>().state + "\n");
                #endregion

                UI.gameObject.SetActive(true);
                Unpause();
                LerpTiming();
                StageCountdown();

                if (Player1.GetComponent<Player>().hitPoints <= (Player1.GetComponent<Player>().maxHitPoints / 4) && !warn1)
                {
                    print("starting pulse1");
                    StartCoroutine(LowHealth1(0.75f, Pulse1));
                }

                if (Player2.GetComponent<Player>().hitPoints <= (Player2.GetComponent<Player>().maxHitPoints / 4) && !warn2)
                {
                    print("starting pulse2");
                    StartCoroutine(LowHealth2(0.75f, Pulse2));
                }

                //if (startSlowMo)
                //    StartCoroutine(SlowMo());

                if (Input.GetButtonDown("Start"))
                {
                    GameMode = eGameMode.Pause;
                    Pause();
                }
                break;
            #endregion

            case eGameMode.Pause:
                UI.gameObject.SetActive(false);
                PauseScreen.gameObject.SetActive(true);
                if (Input.GetButtonDown("Start"))
                    GameMode = eGameMode.Running;
                break;

            case eGameMode.MatchOver:
                LerpUI();
                StageCountdown();
                break;

            case eGameMode.GameOver:
                winner.transform.position = WinnerPose.position;
                UI.gameObject.SetActive(false);
                //show victory screen
                //rematch / back to title
                break;
            default:
                break;
        }
    }


    void TestInput() //HACK testshit
    {
        if (Input.GetKeyDown("space"))
        {
            winner = Player1;
            loser = Player2;
            //StartCoroutine(GameOver());
            StartCoroutine(TextStuff());
            //SVFXManager.instance.PlayVFX_Steam();
            //Player1.GetComponent<Player>().Death();

            //countdown = 5f;

            //Player1.GetComponent<Player>().hitPoints = maxHitpoints1 / 4;
            //UpdateHP();
        }

        //Player1.GetComponent<Player>().Knockdown(1f, 5f);
    }

    //Match intro text
    IEnumerator MatchStartText()
    {
        if (GameMode == eGameMode.Countdown)
            yield break;

        GameMode = eGameMode.Countdown;
        Vector3 inZoom = new Vector3(0, 0, 0);
        Vector3 outZoom = new Vector3(1, 1, 1);
        IntroCDText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(FadeTextIn(1f, IntroCDText, ("Initiating research")));
        StartCoroutine(ZoomTextIn(1f, IntroCDText, ("Initiating research"), inZoom, 1f));
        SVFXManager.instance.PlayMatchstart(mainCam.transform.position);
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeTextOut(0.5f, IntroCDText));
        StartCoroutine(ZoomTextOut(0.5f, IntroCDText, outZoom, 0));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeTextIn(0.5f, IntroCDText, ("Fight!")));
        StartCoroutine(ZoomTextIn(0.6f, IntroCDText, ("Fight!"), inZoom, 4f));
        yield return new WaitForSeconds(0.2f);
        SVFXManager.instance.PlayFight(mainCam.transform.position);
        //TODO play stupid research start sound
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(FadeTextOut(0.2f, IntroCDText));
        yield return new WaitForSeconds(0.3f);
        IntroCDText.gameObject.SetActive(false);
        GameMode = eGameMode.Running;
    }

    IEnumerator LowHealth1(float speed, Image pulse)
    {
        if (warn1)
            yield break;

        warn1 = true;
        while (Hitpoints1 <= maxHitpoints1 / 4 && warn1)
        {
            StartCoroutine(FadeImageIn(speed, pulse));
            yield return new WaitForSeconds(speed);
            StartCoroutine(FadeImageOut(speed, pulse));
            yield return new WaitForSeconds(speed);
        }
    }

    IEnumerator LowHealth2(float speed, Image pulse)
    {
        if (warn2)
            yield break;

        warn2 = true;
        while (Hitpoints2 <= maxHitpoints2 / 4 && warn2)
        {
            StartCoroutine(FadeImageIn(speed, pulse));
            yield return new WaitForSeconds(speed);
            StartCoroutine(FadeImageOut(speed, pulse));
            yield return new WaitForSeconds(speed);
        }
    }

    IEnumerator FadeImageIn(float t, Image pulse)
    {
        pulse.color = new Color(pulse.color.r, pulse.color.g, pulse.color.b, 0);
        while (pulse.color.a < 1.0f)
        {
            pulse.color = new Color(pulse.color.r, pulse.color.g, pulse.color.b, pulse.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    IEnumerator FadeImageOut(float t, Image pulse)
    {
        pulse.color = new Color(pulse.color.r, pulse.color.g, pulse.color.b, 1);
        while (pulse.color.a > 0.0f)
        {
            pulse.color = new Color(pulse.color.r, pulse.color.g, pulse.color.b, pulse.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    IEnumerator FadeTextIn(float t, Text textfield, string textline)
    {
        textfield.text = (textline);

        textfield.color = new Color(textfield.color.r, textfield.color.g, textfield.color.b, 0);
        while (textfield.color.a < 1.0f)
        {
            textfield.color = new Color(textfield.color.r, textfield.color.g, textfield.color.b, textfield.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    IEnumerator FadeTextOut(float t, Text textfield)
    {
        textfield.color = new Color(textfield.color.r, textfield.color.g, textfield.color.b, 1);
        while (textfield.color.a > 0.0f)
        {
            textfield.color = new Color(textfield.color.r, textfield.color.g, textfield.color.b, textfield.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    IEnumerator ZoomTextIn(float t, Text textfield, string textline, Vector3 newScale, float Endsize)
    {
        textfield.text = (textline);

        textfield.transform.localScale = newScale;
        while (textfield.transform.localScale.x < Endsize)
        {
            textfield.transform.localScale = new Vector3(
                 textfield.transform.localScale.x + (Time.deltaTime / t),
                 textfield.transform.localScale.y + (Time.deltaTime / t),
                 textfield.transform.localScale.z + (Time.deltaTime / t));
            yield return null;
        }
    }

    IEnumerator ZoomTextOut(float t, Text textfield, Vector3 newScale, float Endsize)
    {
        textfield.transform.localScale = newScale;
        while (textfield.transform.localScale.x > Endsize)
        {
            textfield.transform.localScale = new Vector3(
                 textfield.transform.localScale.x - (Time.deltaTime / t),
                 textfield.transform.localScale.y - (Time.deltaTime / t),
                 textfield.transform.localScale.z - (Time.deltaTime / t));
            yield return null;
        }
    }

    IEnumerator FadeSpriteIn(float t, SpriteRenderer sprite)
    {
        print("starting sprite fadein");
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        while (sprite.color.a < 255f)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    //Countdown for time til match ends and also match win
    void StageCountdown()
    {
        int curtime = Mathf.RoundToInt(countdown);

        //For when time has run out
        if (countdown <= 0)
        {
            timeout = true;
            if (Hitpoints1 < Hitpoints2)
            {
                print("p2 wins");
                winner = Player2;
                loser = Player1;
                GameMode = eGameMode.MatchOver;
                MatchOver(winner, loser);
            }
            else if (Hitpoints2 < Hitpoints1)
            {
                print("p1 wins");
                winner = Player1;
                loser = Player2;
                GameMode = eGameMode.MatchOver;
                MatchOver(winner, loser);
            }
            else if (Hitpoints1 == Hitpoints2)
            {
                print("Draw, Hot Potato!");
            }
        }
        else if (Player1.GetComponent<Player>().hitPoints <= 0f || Player2.GetComponent<Player>().hitPoints <= 0f)
        {
            //TODO counter for how many matches have been played
            if (Hitpoints1 < Hitpoints2)
            {
                print("p2 wins");
                winner = Player2;
                loser = Player1;
                GameMode = eGameMode.MatchOver;
                MatchOver(winner, loser);
            }
            else if (Hitpoints2 < Hitpoints1)
            {
                print("p1 wins");
                winner = Player1;
                loser = Player2;
                GameMode = eGameMode.MatchOver;
                MatchOver(winner, loser);
            }
        }
        else
        {
            countdown -= Time.deltaTime;

            //For the timer image and text countdown
            if (curtime >= 0)
            {
                clock.fillAmount = Mathf.Lerp(clock.fillAmount, curtime / maxTime, Time.deltaTime * 3);
                timerText.text = (curtime).ToString("0");
                timerTextShade.text = (curtime).ToString("0");
            }
            else if (countdown < 0.0f)
            {
                curtime = 0;
                //timerText.text = ("0");
                //timerTextShade.text = ("0");
            }

            switch (curtime)
            {
                case (10):
                    timerText.color = new Color(255, 0, 0, 255);
                    StartCoroutine(ZoomSeconds(timerTextShade, timerTextShade.text));
                    break;
                case (5):
                    StartCoroutine(ZoomSeconds(timerTextShade, timerTextShade.text));
                    break;
                case (4):
                    StartCoroutine(ZoomSeconds(timerTextShade, timerTextShade.text));
                    break;
                case (3):
                    StartCoroutine(ZoomSeconds(timerTextShade, timerTextShade.text));
                    break;
                case (2):
                    StartCoroutine(ZoomSeconds(timerTextShade, timerTextShade.text));
                    break;
                case (1):
                    StartCoroutine(ZoomSeconds(timerTextShade, timerTextShade.text));
                    break;
                default:
                    break;
            }
        }
    }

    public IEnumerator ZoomSeconds(Text textfield, string textline)
    {
        if (text || countdown <= 0f)
            yield break;

        text = true;
        float wait = 0.49f;
        Vector3 zoomIn = new Vector3(2, 2, 2);
        Vector3 zoomOut = new Vector3(1, 1, 1);
        StartCoroutine(ZoomTextIn(wait, textfield, textline, zoomIn, 2));
        yield return new WaitForSeconds(wait);
        StartCoroutine(ZoomTextOut(wait, textfield, zoomOut, 1));
        yield return new WaitForSeconds(wait);
        text = false;
    }

    //public IEnumerator TimeIsRunningOut(Text textfield, string textline)
    //{
    //    if (ending)
    //        yield break;

    //    ending = true;
    //    int curtime = Mathf.RoundToInt(countdown);
    //    Vector3 zoomIn = new Vector3(2, 2, 2);
    //    Vector3 zoomOut = new Vector3(1, 1, 1);
    //    float wait = 0.49f;
    //    while (curtime <= 5 && curtime > 0)
    //    {
    //        if (curtime <= 0)
    //            yield break;

    //        print("zoom in and out, curtime = "+curtime);
    //        StartCoroutine(ZoomTextIn(wait, textfield, textline, zoomIn, 2));
    //        yield return new WaitForSeconds(wait);
    //        StartCoroutine(ZoomTextOut(wait, textfield, zoomOut, 1));
    //        yield return new WaitForSeconds(wait);
    //    }
    //}

    //Runs after a match has finished
    public void MatchOver(GameObject winner, GameObject loser)
    {
        if (timeout)
        {
            print("time has run out");
            loser.GetComponent<Player>().hitPoints = 0f;
            UpdateHP();
            loser.GetComponent<Player>().Death();
        }
        else
        {
            print("Match over");
            loser.GetComponent<Player>().hitPoints = 0f;
            UpdateHP();
            loser.GetComponent<Player>().Death();
        }
    }

    public IEnumerator TextStuff()
    {
        if (end)
            yield break;

        end = true;
        name = winner.name;

        yield return new WaitForSeconds(2f);

        Vector3 inZoom = new Vector3(0, 0, 0);
        MatchWinText.gameObject.SetActive(true);
        MatchWinText.text = ("Research complete");

        StartCoroutine(FadeTextIn(1f, MatchWinText, MatchWinText.text));
        StartCoroutine(ZoomTextIn(1f, MatchWinText, MatchWinText.text, inZoom, 1f));
        SVFXManager.instance.PlayMatchfinish(mainCam.transform.position);
        yield return new WaitForSeconds(2f);

        StartCoroutine(FadeTextOut(0.5f, MatchWinText));
        yield return new WaitForSeconds(0.4f);
        MatchWinText.text = (name + " lives");
        StartCoroutine(FadeTextIn(1f, MatchWinText, MatchWinText.text));
        SVFXManager.instance.PlaySuccess(mainCam.transform.position);
        yield return new WaitForSeconds(2f);

        StartCoroutine(FadeTextOut(0.5f, MatchWinText));
        yield return new WaitForSeconds(0.4f);

        float x = WinnerPose.position.x;
        UI.gameObject.SetActive(false);
        mainCam.transform.position = new Vector3(x, mainCam.transform.position.y, mainCam.transform.position.z);
        winner.GetComponent<Player>().sprite.color = new Color(winner.GetComponent<Player>().sprite.color.r, winner.GetComponent<Player>().sprite.color.g, winner.GetComponent<Player>().sprite.color.b, 0);
        MatchWinText.gameObject.SetActive(false);
        StartCoroutine(GameOver());
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.3f);
        GameMode = eGameMode.GameOver;
        winner.GetComponent<Player>().sprite.color = new Color(winner.GetComponent<Player>().sprite.color.r, winner.GetComponent<Player>().sprite.color.g, winner.GetComponent<Player>().sprite.color.b, 255);
        winner.GetComponent<Player>().anim.Play("victory");

        //Fireworks
        SVFXManager.instance.PlayVFX_Firework(FW1.transform.position);
        SVFXManager.instance.PlayVFX_Firework(FW7.transform.position);
        yield return new WaitForSeconds(0.1f);
        SVFXManager.instance.PlayVFX_Firework(FW2.transform.position);
        SVFXManager.instance.PlayVFX_Firework(FW8.transform.position);
        yield return new WaitForSeconds(0.2f);
        SVFXManager.instance.PlayVFX_Firework(FW3.transform.position);
        SVFXManager.instance.PlayVFX_Firework(FW9.transform.position);
        yield return new WaitForSeconds(0.1f);
        SVFXManager.instance.PlayVFX_Firework(FW4.transform.position);
        SVFXManager.instance.PlayVFX_Firework(FW11.transform.position);
        yield return new WaitForSeconds(0.15f);
        SVFXManager.instance.PlayVFX_Firework(FW5.transform.position);
        SVFXManager.instance.PlayVFX_Firework(FW10.transform.position);
        yield return new WaitForSeconds(0.1f);
        SVFXManager.instance.PlayVFX_Firework(FW6.transform.position);
        SVFXManager.instance.PlayVFX_Firework(FW12.transform.position);

        yield return new WaitForSeconds(4f);
        MatchOverButtons.SetActive(true);
    }

    //HACK old unfinished hot potato code
    //Give potato to the player who has been hit or lost the last match
    //public void HotPotato(GameObject screwedPlayer)
    //{
    //Potato = screwedPlayer;
    //if (screwedPlayer.GetComponent<Player>().PlayerIndex == 0)
    //{
    //    potatoSlider.value = 0;
    //}
    //else if (screwedPlayer.GetComponent<Player>().PlayerIndex == 1)
    //{
    //    potatoSlider.value = 1;
    //}
    //}

    //Called to deal dmg to the potato holder
    //public void HotPotatoDmg()
    //{
    //potatoTimer = potatoTime;
    //Potato.GetComponent<Player>().ApplyDamage(PotatoDmg);
    //}

    //Timer for the Hot Potato mechanic
    //void PotatoTiming()
    //{        
    //    if (potatoTimer > 0.0f)
    //    {
    //        potatoTimer -= Time.deltaTime;
    //    }
    //    if (potatoTimer == 0.0f)
    //    {
    //        HotPotatoDmg();
    //    }
    //    if (potatoTimer < 0.0f)
    //    {
    //        potatoTimer = 0.0f;
    //    }
    //}

    //Sets HP and the timer for the red HP bar

    //called by player when dmg received
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
        hpBarRed1.fillAmount = Mathf.Lerp(hpBarRed1.fillAmount, Hitpoints1 / maxHitpoints1, Time.deltaTime * 3);
        hpBarRed2.fillAmount = Mathf.Lerp(hpBarRed2.fillAmount, Hitpoints2 / maxHitpoints2, Time.deltaTime * 3);
    }

    public IEnumerator SlowMo()
    {
        if (slowed)
            yield break;

        slowed = true;
        startSlowMo = false;
        print("slowing down");
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1f;
        print("time back to normal");
        slowed = false;
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
        GameMode = eGameMode.Running;
        Time.timeScale = 1;
    }

    //Update to be run after the last match and show a result screen
    //public void GameOver()
    //{
    //    Pause();
    //}

    public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("Title");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NextMatch()
    {
        Player1.GetComponent<Player>().hitPoints = maxHitpoints1;
        Player2.GetComponent<Player>().hitPoints = maxHitpoints2;
        Player1.transform.position = P1Respawn.position;
        Player2.transform.position = P2Respawn.position;
        GameMode = eGameMode.Running;
    }

}
