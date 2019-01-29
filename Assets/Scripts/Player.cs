using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main Player script, all important values are handled here
/// </summary>

public class Player : MonoBehaviour
{
    [Header("Atomic Parameters for the GD to meddle with")]
    [Tooltip("Max HP this character starts with")]
    public float maxHitPoints;
    public float MoveSpeed;
    [Tooltip("Damage Attack1 deals unblocked")]
    public float attack1Dmg = 1f;   //dmg attack1 does
    [Tooltip("Damage Attack2 deals unblocked")]
    public float attack2Dmg = 2f;   //dmg attack2 does
    [Tooltip("How much damage ")]
    public float blockPct = 50;

    [Header("CODERS ONLY! DO NOT TOUCH OR OUCH! ----------------------")]
    public int PlayerIndex;
    public float JumpForce;
    [Tooltip("Current hitpoints")]
    public float hitPoints;
    public float horizontal;
    public float vertical;
    public bool facingRight;
    public float move = 0f;
    public int wins;
    //[Tooltip("When Attack1 deals damage")]
    //public float attack1Hit = 0.2f; 
    //[Tooltip("When Attack2 deals damage")]
    //public float attack2Hit = 0.2f;
    [Tooltip("Everything about the attacks")]
    public float attackDelay;
    public float hitrange1 = 2f;    //hitrange of attack1
    public float hitrange2 = 2f;    //hitrange of attack2
    public float hitrangeBB = 2f;   //hitrange of the blockbreaker
    public float jumpHurt = 2.5f;
    public bool grounded;
    public bool hitCheck = false;
    public bool blockbreak = false;
    [Tooltip("Offset for the Hit VXF for Claudia")]
    public float HitOffsetC = 4.5f;
    [Tooltip("Offset for the Hit VXF for Bruno")]
    public float HitOffsetB = 4.5f;

    [Header("Placeholder Shit to delete")]
    public GameObject shield;

    [Header("Components the script grabs itself")]
    public SpriteRenderer sprite;
    public Animator anim;
    public Rigidbody2D rigid;
    public CamShake camShake;
    public GameObject opponent;
    [Tooltip("Reference to script with Claudias specials")]
    public Claudia claudia;

    [Header("Possibly now useless Animation times")]
    //public float clipAttack1;
    //public float clipAttack2;
    //public float clipHurt;



    public ePlayerState state;
    //public eCharacter character;

    public void Start()
    {
        if (PlayerIndex == 0)
        {
            facingRight = true;
        }
        else if (PlayerIndex == 1)
        {
            facingRight = false;
        }

        hitPoints = maxHitPoints;

        GetComponents();
        Flip();
        FindOpponent();
        GameManager.instance.UpdateHP();
        state = ePlayerState.Ready;
        //GetAnimClipTimes();
    }

    public void FixedUpdate()
    {
        //TODO put all physics in fixedupdate?
    }


    public void Update()
    {
        switch (GameManager.instance.GameMode)
        {
            case eGameMode.Running:

                horizontal = Input.GetAxis("Horizontal_" + PlayerIndex);
                vertical = Input.GetAxis("Vertical_" + PlayerIndex);

                if (transform.position.x < opponent.transform.position.x)
                    facingRight = true;
                else if (transform.position.x > opponent.transform.position.x)
                    facingRight = false;

                if (hitPoints <= 0)
                {
                    state = ePlayerState.Dead;
                }

                switch (state)
                {
                    case ePlayerState.Ready:
                        
                        shield.gameObject.SetActive(false);
                        Move();
                        Flip();

                        if (Input.GetButtonDown("Attack1_" + PlayerIndex))
                        {
                            //print("Attack1");
                            state = ePlayerState.Attacking;
                            anim.SetTrigger("attack1");
                            hitCheck = true;
                            claudia.PlayAttackSound();
                            //StartCoroutine(Attack1());
                        }

                        if (Input.GetButtonDown("Attack2_" + PlayerIndex))
                        {
                            //print("Attack2");
                            state = ePlayerState.Attacking;
                            anim.SetTrigger("attack2");
                            hitCheck = true;
                            claudia.PlayAttackSound();
                            //StartCoroutine(Attack2());
                        }

                        while (vertical == 1f)
                        {
                            shield.gameObject.SetActive(true);
                            state = ePlayerState.Blocking;
                            break;
                        }

                        if (vertical < 0.95f && vertical >= 0f)
                        {
                            state = ePlayerState.Ready;
                            shield.gameObject.SetActive(false);
                            //while (vertical == 1f)
                            //{
                            //    print("block");
                            //    state = ePlayerState.Blocking;
                            //    break;
                            //}
                        }
                        else if (vertical == -1f) //TODO physics in fixedupdate?
                        {
                            print("jump");
                            state = ePlayerState.InAir;
                            Jump();
                            //TODO trigger jump and fall animation
                        }

                        if (Input.GetButtonDown("Breaker_" + PlayerIndex))
                        {
                            print("Blockbreaker");
                            state = ePlayerState.Attacking;
                            anim.SetTrigger("attack2");
                            blockbreak = true;
                            hitCheck = true;
                            claudia.PlayAttackSound();
                            //Blockbreaking Move
                        }                        

                        break;
                    case ePlayerState.Attacking:
                        break;
                    case ePlayerState.InAir:
                        Move();
                        if (grounded)
                            state = ePlayerState.Ready;
                        break;

                    case ePlayerState.Blocking:
                        Move();
                        if (vertical < 0.95f && vertical >= 0f)
                        {
                            print("not blocking");
                            state = ePlayerState.Ready;
                            shield.gameObject.SetActive(false);
                        }
                        break;
                    case ePlayerState.Knockdown:
                        break;
                    case ePlayerState.Dead:
                        //play dying animation
                        //when finished, set bool for gamemanager matchover
                        break;

                    default:
                        break;
                }
                break;
        }
    }

    //Get all Components and values needed at start
    void GetComponents()
    {
        sprite = this.GetComponentInChildren<SpriteRenderer>();
        anim = this.GetComponentInChildren<Animator>();
        rigid = this.GetComponent<Rigidbody2D>();
        camShake = FindObjectOfType<CamShake>();
        claudia = FindObjectOfType<Claudia>();
    }

    //Find other player
    void FindOpponent()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            int Index = player.GetComponentInChildren<Player>().PlayerIndex;
            if (Index != this.PlayerIndex)
                opponent = player;
        }
    }

    //Check if the player is touching the ground
    private void OnCollisionEnter2D(Collision2D col)
    {
        //print("checking for ground");
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    //Turn character around
    public void Flip()
    { //TODO Coroutine for delay before turning around
        if (facingRight)
        {
            Vector3 scale = transform.localScale;
            scale.x = +1f;
            transform.localScale = scale;
        }
        else if (!facingRight)
        {
            Vector3 scale = transform.localScale;
            scale.x = -1f;
            transform.localScale = scale;
        }
    }

    //Called by opponent on hit
    public void ApplyDamage(float dmgreceived)
    {
        //TODO Animator
        print(gameObject.name + " got hurt");
        state = ePlayerState.Knockdown;
        hitPoints -= dmgreceived;
        anim.SetTrigger("hurt");
        claudia.PlayVoiceSound();
        StartCoroutine(camShake.Shake(0.15f, 0.4f));
        GameManager.instance.UpdateHP();
        Debug.Log(gameObject.name + " got hit.");
        print(gameObject.name + "s HP: " + hitPoints);
        //GameManager.instance.lerpTimer = GameManager.instance.lerpCooldown;
        state = ePlayerState.Ready;
    }

    //Moves the character via axis input
    public void Move()
    {
        //work around cause all joysticks are broken af
        if (horizontal > 0.2f)
            move = 1f;
        else if (horizontal < -0.2f)
            move = -1f;
        else
            move = 0f;        

        //TODO Animator Laufrichtung
        if (facingRight)
        {
            if (horizontal < -0.2f)
            {
                //print(gameObject.name + " läuft rückwärts");
            }
            else if (horizontal > 0.2f)
            {
                //anim.SetBool("walk", true);
                //print(gameObject.name + " läuft vorwärts");
            }
        }
        else if (!facingRight)
        {
            if (horizontal < -0.2f)
            {
                //anim.SetBool("walk", true);
                //print(gameObject.name + " läuft vorwärts");
            }
            else if (horizontal > 0.2f)
            {
                //print(gameObject.name + " läuft rückwärts");
            }
        }
            else if (move == 0)
            {
                //anim.SetBool("walk", false);
                //print(gameObject.name + " steht");
            }

        transform.Translate(new Vector2(move, 0) * MoveSpeed * Time.deltaTime);

        //Vector2 velocity = new Vector2(move, transform.position.y);
        //rigid.MovePosition(rigid.position + velocity * MoveSpeed * Time.fixedDeltaTime);
        //rigid.MovePosition(new Vector2(move, 0) * MoveSpeed * Time.deltaTime);
        //rigid.MovePosition(new Vector2((transform.position.x + move * MoveSpeed * Time.deltaTime),transform.position.y));
    }

    //public IEnumerator Jump()
    //{
    //  Vector2 jumpVector = ???
    //  float jumpTime = 2f;
    //    rigid.velocity = Vector2.zero;
    //    float timer = 0f;

    //    while (timer < jumpTime)
    //    {
    //        float completed = timer / jumpTime;
    //        Vector2 frameJumpV = Vector2.Lerp(jumpVector, Vector2.zero, completed);
    //        rigid.AddForce(frameJumpV);
    //        timer += Time.deltaTime;
    //        yield return null;
    //    }
    //    state = ePlayerState.Ready;
    //}

    public void Jump()
    {
        grounded = false;
        rigid.AddForce(new Vector2(0f, JumpForce / 10));

        //rigid.AddForce(Vector2.up * JumpForce * Time.deltaTime);
        //rigid.AddForce(Vector2.up * JumpForce * Time.deltaTime);
        //rigid.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        //rigid.AddForce(Vector2.up * JumpForce, ForceMode2D.Force);

    }

    IEnumerator Attack1()
    {
        if (state != ePlayerState.Ready)
            yield break;

        state = ePlayerState.Attacking;
        anim.SetTrigger("attack1");
        //yield return new WaitForSeconds(attack1Hit);
        //CheckForHit(attack1Dmg, hitrange1);
        //wait for animation to finish
        //yield return new WaitForSeconds(clipAttack1 - attack1Hit);
        state = ePlayerState.Ready;
    }

    IEnumerator Attack2()
    {
        if (state != ePlayerState.Ready)
            yield break;

        //state = ePlayerState.Attacking;
        //anim.SetTrigger("attack2");
        //yield return new WaitForSeconds(attack2Hit);
        //CheckForHit(attack2Dmg, hitrange2);
        //state = ePlayerState.Ready;
    }

    public void CheckForHit(float dmg, float hitrange, float hitoffset)
    {
        if (hitCheck)
        {
            hitCheck = false;
            float opponentX = opponent.transform.position.x;
            float opponentY = opponent.transform.position.y;
            Vector3 player = transform.position;

            //Check if opponent is grounded)
            if (opponent.GetComponent<Player>().grounded == true)
            {
                if (facingRight)
                {
                    if (player.x >= opponentX - hitrange && player.x <= opponentX)
                    {
                        Hit(dmg, hitoffset);
                    }
                }
                else if (!facingRight)
                {
                    if (player.x <= opponentX + hitrange && player.x >= opponentX)
                    {
                        Hit(dmg, hitoffset);
                    }
                }
            }
            else if (opponent.GetComponent<Player>().grounded == false)
            {   //check if jumping player is in range
                if (opponentY == jumpHurt)
                {
                    if (facingRight)
                    {
                        if (player.x >= opponentX - hitrange && player.x <= opponentX)
                        {
                            Hit(dmg, hitoffset);
                        }
                    }
                    else if (!facingRight)
                    {
                        if (player.x <= opponentX + hitrange && player.x >= opponentX) //&& opponent not blocking
                        {
                            Hit(dmg, hitoffset);
                        }
                    }
                }
            }
            else
                blockbreak = false;
                return;
        }
    }

    //Deal damage after successful hit
    public void Hit(float dmg, float offsetY)
    {
        if (opponent.GetComponent<Player>().state != ePlayerState.Blocking)
        {
            print(gameObject.name + " hits " + opponent.name);
            //claudia.PlayImpactSound();
            SVFXManager.instance.PlayVFX_HitWhite(offsetY, opponent.gameObject);
            opponent.GetComponent<Player>().ApplyDamage(dmg);
        }
        else if (opponent.GetComponent<Player>().state == ePlayerState.Blocking)
        {
            if (blockbreak)
            {
                //knockdown
                print(gameObject.name + " deals " + dmg + " to " + opponent.name);
                //claudia.PlayImpactSound();
                SVFXManager.instance.PlayVFX_HitWhite(offsetY, opponent.gameObject);
                opponent.GetComponent<Player>().ApplyDamage(dmg);
            }
            else
            {
                float realdmg = dmg - (dmg * (blockPct / 100));
                print(gameObject.name + " deals " + realdmg+ " to " + opponent.name);
                //claudia.PlayImpactSound();
                SVFXManager.instance.PlayVFX_HitWhite(offsetY, opponent.gameObject);
                opponent.GetComponent<Player>().ApplyDamage(realdmg);
            }

        }
        blockbreak = false;
    }
}
