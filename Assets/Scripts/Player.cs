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
    [Tooltip("Speed forwards movement")]
    public float MoveSpeedF;
    [Tooltip("Speed backwards movement")]
    public float MoveSpeedB;
    [Tooltip("Speed movement when in Air")]
    public float MoveSpeedJ;
    [Tooltip("Damage Attack1 deals unblocked")]
    public float attack1Dmg = 1f;   //dmg attack1 does
    [Tooltip("Damage Attack2 deals unblocked")]
    public float attack2Dmg = 2f;   //dmg attack2 does    
    [Tooltip("Damage jumpattack deals unblocked")]
    public float jumpattackDmg = 2f;   //dmg jumattack does    
    [Tooltip("Damage Blockbreaker deals")]
    public float blockbreakDmg = 2f;   //dmg BB does
    [Tooltip("% damage avoided by blocking")]
    public float blockPct = 50;

    [Header("CODERS ONLY! DO NOT TOUCH OR OUCH! ----------------------")]
    public int PlayerIndex;
    public float JumpForce;
    public float KBstrength = 7f;       //How much enemy is knocked back
    [Tooltip("Current hitpoints")]
    public float hitPoints;
    public float horizontal;
    public float vertical;
    public bool facingRight;
    private float dmg;
    private float range;
    public float move = 0f;
    public float border = 17f;      //stage borders, center is 0
    public bool grounded;
    public bool landCheck;
    public float origScale; //for flipping, get how big the char is scaled
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

    public float hitrangeJA;    //range of jumpattack horizontal
    public float jumpHurt = 2.5f;   //TODO change when jump attack comes

    public float extraGravity = 3f; //for better falling
    public float airAttackGravity = 6f;
    public float jumpAttack = 1.5f; //jumpattacks are allowed from this hight on
    public bool hitCheck = false;   //to make sure it is only once checked for hit and not continuously
    public bool blockbreak = false;
    public float curMoveSpeed;      //current Movement Speed
    [Tooltip("Offset for the Hit VXF")]
    public float HitOffset = 4.5f;

    [Header("Placeholder Shit to delete")]
    public GameObject shield;

    [Header("Components the script grabs itself")]
    public SpriteRenderer sprite;
    public Animator anim;
    public Rigidbody2D rigid;
    public CamShake camShake;
    public GameObject opponent;

    [Tooltip("Reference to script with CVoices specials")]
    public CSounds CVoice;

    [Header("Possibly now useless Animation times")]
    //public float clipAttack1;
    //public float clipAttack2;
    //public float clipHurt;

    public ePlayerState state;
    public eAttacks attack = eAttacks.None;
    //public eCharacter character;

    public void Start()
    {
        if (PlayerIndex == 0)
        {
            gameObject.name = "Player 1";
            facingRight = true;
        }
        else if (PlayerIndex == 1)
        {
            gameObject.name = "Player 2";
            facingRight = false;
        }

        origScale = transform.localScale.x;

        hitPoints = maxHitPoints;

        GetComponents();
        FindOpponent();
        Flip();
        GameManager.instance.UpdateHP();
        state = ePlayerState.Ready;
        //GetAnimClipTimes();
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
                            attack = eAttacks.Light;
                            anim.SetTrigger("attack1");
                            hitCheck = true;
                            CVoice.PlayAttackSound();
                            //StartCoroutine(Attack1());
                        }

                        if (Input.GetButtonDown("Attack2_" + PlayerIndex))
                        {
                            //print("Attack2");
                            state = ePlayerState.Attacking;
                            attack = eAttacks.Heavy;
                            anim.SetTrigger("attack2");
                            hitCheck = true;
                            CVoice.PlayAttackSound();
                            //StartCoroutine(Attack2());
                        }

                        if (Input.GetButtonDown("Breaker_" + PlayerIndex))
                        {
                            print("Blockbreaker");
                            state = ePlayerState.Attacking;
                            attack = eAttacks.Blockbreak;
                            anim.SetTrigger("blockbreak");
                            blockbreak = true;
                            hitCheck = true;
                            CVoice.PlayAttackSound();
                            //Blockbreaking Move
                        }

                        while (vertical == 1f)
                        {
                            state = ePlayerState.Blocking;
                            anim.SetBool("blocking", true);
                            shield.gameObject.SetActive(true);
                            break;
                        }

                        if (vertical < 0.95f && vertical >= 0f)
                        {
                            anim.SetBool("blocking", false);
                            shield.gameObject.SetActive(false);
                            state = ePlayerState.Ready;
                            //while (vertical == 1f)
                            //{
                            //    print("block");
                            //    state = ePlayerState.Blocking;
                            //    break;
                            //}
                        }
                        else if (vertical == -1f)
                        {
                            print("jump");
                            anim.SetTrigger("startup");
                            state = ePlayerState.InAir;
                        }

                        break;
                    case ePlayerState.Attacking:
                        break;
                    case ePlayerState.InAir:
                        Move();

                        //if (rigid.velocity.y > 0 && !grounded)
                        //{
                        //    anim.SetBool("jumping", true);
                        //}
                        //else 
                        if (rigid.velocity.y < 0f && !grounded)
                        {
                            if (attack == eAttacks.Jump)
                            {
                                anim.SetTrigger("jumpattack");
                                state = ePlayerState.InAirAttack;
                            }
                            else
                            {
                                anim.SetBool("jumping", false);
                                anim.SetBool("falling", true);
                                rigid.velocity += Vector2.up * Physics.gravity * extraGravity * Time.deltaTime;
                            }

                        }
                        else if (landCheck && anim.GetBool("falling"))
                        {
                            anim.SetBool("falling", false);
                            anim.SetTrigger("land");
                            //anim.ResetTrigger("land");
                            state = ePlayerState.Ready;
                        }

                        if (Input.GetButtonDown("Attack1_" + PlayerIndex) && !anim.GetBool("falling"))
                        {
                            attack = eAttacks.Jump;


                            //if (transform.position.y >= jumpAttack)
                            //{
                            //    print("JumpAttack");
                            //    state = ePlayerState.InAirAttack;
                            //    anim.SetBool("jumpattack", true);
                            //    attack = eAttacks.Jump;

                            //    hitCheck = true;
                            //    CVoice.PlayAttackSound();
                            //}
                            //else
                            //{
                            //    print("not high enough for jumpattack");
                            //}
                        }

                        break;

                    case ePlayerState.InAirAttack:

                        rigid.velocity += Vector2.up * Physics.gravity * airAttackGravity * Time.deltaTime;

                        if (landCheck && anim.GetBool("jumpattack"))
                        {
                            anim.SetTrigger("land");
                        }
                        else if (grounded)
                        {
                            state = ePlayerState.Ready;
                        }

                        break;
                    case ePlayerState.Blocking:
                        //Move();
                        if (vertical < 0.95f && vertical >= 0f)
                        {
                            print("not blocking");
                            anim.SetBool("blocking", false);
                            shield.gameObject.SetActive(false);
                            state = ePlayerState.Ready;
                        }
                        break;
                    case ePlayerState.Hurt:
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
        CVoice = FindObjectOfType<CSounds>();
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

    //Turn character around
    public void Flip()
    {
        //TODO Prio5: langsamer umdrehen wenn blockend
        Vector3 scale = transform.localScale;

        if (facingRight)
        {
            scale.x = origScale;
            transform.localScale = scale;
        }
        else if (!facingRight)
        {
            scale.x = -origScale;
            transform.localScale = scale;
        }
    }

    //Called by opponent on hit
    public void ApplyDamage(float dmgreceived)
    {
        state = ePlayerState.Hurt;
        anim.SetTrigger("hurt");
        hitPoints -= dmgreceived;
        print(gameObject.name + " got hurt");
        CVoice.PlayVoiceSound();
        StartCoroutine(camShake.Shake(0.15f, 0.4f));
        GameManager.instance.UpdateHP();
        Debug.Log(gameObject.name + " got hit.");
        print(gameObject.name + "s HP: " + hitPoints);
        //GameManager.instance.lerpTimer = GameManager.instance.lerpCooldown;
        state = ePlayerState.Ready;
    }

    //Knocks the player backwards
    public void Knockback(float strength)
    {
        print(gameObject.name + " is knocked back by " + strength);

        if (transform.position.x >= border && move > 0 || transform.position.x <= -border && move < 0)
            rigid.AddForce(new Vector2(-0.1f / 10, 0f));
        else
        {
            if (facingRight)
            {
                rigid.AddForce(new Vector2(-strength / 10, 0f));
            }
            else if (!facingRight)
            {
                rigid.AddForce(new Vector2(+strength / 10, 0f));
            }
        }
    }

    //Moves the character via axis input
    public void Move()
    {
        WalkDirection();

        //do move if
        if (transform.position.x <= border && transform.position.x >= -border)
            transform.Translate(new Vector2(move, 0) * curMoveSpeed * Time.deltaTime);
        else if (transform.position.x >= border && move < 0)
            transform.Translate(new Vector2(move, 0) * curMoveSpeed * Time.deltaTime);
        else if (transform.position.x <= -border && move > 0)
            transform.Translate(new Vector2(move, 0) * curMoveSpeed * Time.deltaTime);
        //do not move if
        else if (transform.position.x >= border && move > 0)
            horizontal = 0;
        else if (transform.position.x <= -border && move < 0)
            horizontal = 0;
    }

    //Checks walk direction or if in air to adjust speed and animation
    public void WalkDirection()
    {
        //work around cause all joysticks are broken af
        if (horizontal > 0.2f)
            move = 1f;
        else if (horizontal < -0.2f)
            move = -1f;
        else
            move = 0f;

        switch (state)
        {
            case ePlayerState.Ready:

                if ((facingRight && horizontal < -0.2f) || (!facingRight && horizontal > 0.2f))
                {
                    curMoveSpeed = MoveSpeedB;
                    anim.SetBool("walkbackwards", true);
                    print(gameObject.name + " läuft rückwärts");
                }
                else if ((facingRight && horizontal > 0.2f) || (!facingRight && horizontal < -0.2f))
                {
                    curMoveSpeed = MoveSpeedF;
                    anim.SetBool("walkforwards", true);
                    print(gameObject.name + " läuft vorwärts");
                }
                else if (move == 0)
                {
                    curMoveSpeed = 0f;
                    anim.SetBool("walkbackwards", false);
                    anim.SetBool("walkforwards", false);
                    print(gameObject.name + " steht");
                }
                break;

            case ePlayerState.InAir:
                curMoveSpeed = MoveSpeedJ;
                break;

            default:
                break;
        }
    }

    public void Jump()
    {
        grounded = false;
        rigid.AddForce(new Vector2(0f, JumpForce / 10));
    }

    //Checks which attack has been used to get range and dmg
    public void GetAttackValues()
    {
        if (hitCheck)
        {
            hitCheck = false;

            if (opponent.GetComponent<Player>().grounded)
            {
                switch (attack)
                {
                    case eAttacks.None:
                        print("No attack mode set");
                        break;
                    case eAttacks.Light:
                        dmg = attack1Dmg;
                        range = hitrange1;
                        break;
                    case eAttacks.Heavy:
                        dmg = attack2Dmg;
                        range = hitrange2;
                        break;
                    case eAttacks.Blockbreak:
                        dmg = blockbreakDmg;
                        range = hitrangeBB;
                        break;
                    case eAttacks.Jump:
                        dmg = jumpattackDmg;
                        range = hitrangeJA;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //TODO if opponent you are trying to hit is not grounded
            }

            CheckForHit(dmg, range);
            attack = eAttacks.None;
        }
    }

    //Checks if attack has gone through
    public void CheckForHit(float dmg, float range)
    {
        float opponentX = opponent.transform.position.x;
        //float opponentY = opponent.transform.position.y;
        Vector3 player = transform.position;

        if (opponent.GetComponent<Player>().grounded)
        {
            if (facingRight)
            {
                if (player.x >= opponentX - range && player.x <= opponentX)
                {
                    DealDmg(dmg);
                }
            }
            else if (!facingRight)
            {
                if (player.x <= opponentX + range && player.x >= opponentX)
                {
                    DealDmg(dmg);
                }
            }
        }
    }

    //Deal damage after successful hit
    public void DealDmg(float dmg)
    {
        float offset = opponent.GetComponent<Player>().HitOffset;

        if (opponent.GetComponent<Player>().state == ePlayerState.Blocking)
        {
            if (attack == eAttacks.Blockbreak)
            {
                print(gameObject.name + " deals " + dmg + " to " + opponent.name);
                SVFXManager.instance.PlayVFX_ComicPow(offset, opponent.gameObject);
                opponent.GetComponent<Player>().ApplyDamage(dmg);
                opponent.GetComponent<Player>().Knockback(KBstrength);
            }
            else
            {
                float realdmg = dmg - (dmg * (blockPct / 100));
                print(gameObject.name + " deals " + realdmg + " to " + opponent.name);
                SVFXManager.instance.PlayVFX_ComicPow(offset, opponent.gameObject);
                opponent.GetComponent<Player>().ApplyDamage(realdmg);
            }
        }
        else
        {
            print(gameObject.name + " deals " + dmg + " to " + opponent.name);
            SVFXManager.instance.PlayVFX_ComicPow(offset, opponent.gameObject);
            opponent.GetComponent<Player>().ApplyDamage(dmg);
            opponent.GetComponent<Player>().Knockback(KBstrength);
        }
    }
}
