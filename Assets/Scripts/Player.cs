﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main Player script, all important values are handled here
/// </summary>

public class Player : MonoBehaviour
{
    [Header("Atomic Parameters for the GD to meddle with")]
    [Tooltip("Max HP this character starts with")]
    public float maxHitPoints = 4000f;
    [Tooltip("Speed forwards movement")]
    public float MoveSpeedF = 12f;
    [Tooltip("Speed backwards movement")]
    public float MoveSpeedB = 12f;
    [Tooltip("Damage Attack1 deals unblocked")]
    public float attack1Dmg = 185f;
    [Tooltip("Damage Attack2 deals unblocked")]
    public float attack2Dmg = 340f;
    [Tooltip("Damage jumpattack deals unblocked")]
    public float jumpattackDmg = 200f;
    [Tooltip("Damage Blockbreaker deals")]
    public float blockbreakDmg = 250f;
    [Tooltip("% damage avoided by blocking")]
    public float blockPct = 80;
    [Tooltip("How much the opponent is knocked back after being hit")]
    public float KBstrength = 5f;
    [Tooltip("How much the opponent is knocked up after being hit")]
    public float knockUp = 1f;  //upward knock when knocked back

    [Header("CODERS ONLY! DO NOT TOUCH OR OUCH! ----------------------")]
    public int PlayerIndex;
    public float JumpForce;
    [Tooltip("Speed movement when in Air")]
    public float MoveSpeedJ;
    [Tooltip("Current hitpoints")]
    public float hitPoints;
    public float horizontal;
    public float vertical;
    public bool facingRight;
    private float dmg;
    private float rangeY;
    private float rangeX;
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

    [Header("Attack stuff")]
    public float hitrange1 = 9.5f;    //hitrange of attack1
    public float hitrange2 = 9f;    //hitrange of attack2
    public float hitrangeBB = 3f;   //hitrange of the blockbreaker
    public float hitrangeJX = 6.5f;    //range of jumpattack horizontal
    public float hitrangeJY = 6.25f;    //range of jumpattack vertical
    public float jumpHurt = 2.5f;
    public bool hitCheck = false;   //to make sure it is only once checked for hit and not continuously
    public bool blockbreak = false;
    public float curMoveSpeed;      //current Movement Speed
    [Tooltip("Offset for the Hit VXF")]
    public float HitOffset = 4.5f;

    [Header("Jump stuff")]
    public float extraGravity = 3f; //for better falling
    public float airAttackGravity = 6f; //for the dive attack
    public float jumpAttack = 1.5f; //jumpattacks are allowed from this hight on
    public bool attackpossible = false; //jumpattack allowed
    public float dustJumpY; //Jump dust animation offset


    [Header("Components the script grabs itself")]
    public SpriteRenderer sprite;
    public Animator anim;
    public Animator shield;
    public Rigidbody2D rigid;
    public CamShake camShake;
    public GameObject opponent;
    [Tooltip("Reference to script with CVoices specials")]
    public CSounds CVoice;

    [Header("Enum stuff")]
    public ePlayerState state;
    public eStun stun;
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

    public void FixedUpdate()
    {
        anim.SetFloat("velocityY", rigid.velocity.y);
    }

    public void Update()
    {
        anim.SetBool("grounded", grounded);

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

                        attack = eAttacks.None;
                        anim.SetBool("blocking", false);

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

                        if (Input.GetButton("Block_" + PlayerIndex) && grounded)
                        {
                            print("block");
                            state = ePlayerState.Blocking;
                            //break; //for while loop
                        }

                        if (vertical == -1f && grounded)
                        {
                            state = ePlayerState.JumpTakeOff;
                            anim.SetBool("walkbackwards", false);
                            anim.SetBool("walkforwards", false);
                            anim.SetBool("startup", true);
                        }

                        break;
                    case ePlayerState.Attacking:
                        anim.SetBool("walkbackwards", false);
                        anim.SetBool("walkforwards", false);
                        break;
                    case ePlayerState.JumpTakeOff:
                        break;
                    case ePlayerState.InAir:
                        Move();

                        if (rigid.velocity.y > 0f && !grounded && !anim.GetBool("jumping"))
                        {
                            attackpossible = true;
                            anim.SetBool("jumping", true);
                        }
                        else if (rigid.velocity.y <= 0f && !grounded)
                        {
                            attackpossible = true;
                            anim.SetBool("jumping", false);
                            anim.SetBool("falling", true);
                            rigid.velocity += Vector2.up * Physics.gravity * extraGravity * Time.deltaTime;

                        }
                        else if (grounded && anim.GetBool("falling"))
                        {
                            attackpossible = false;
                            anim.SetBool("falling", false);
                            anim.SetTrigger("land");
                            //anim.ResetTrigger("land");
                        }

                        if (Input.GetButtonDown("Attack1_" + PlayerIndex) && attackpossible)
                        {
                            state = ePlayerState.InAirAttack;
                            attack = eAttacks.Jump;
                            anim.SetTrigger("jumpattack");
                            CVoice.PlayAttackSound();
                            hitCheck = true;
                        }

                        break;
                    case ePlayerState.InAirAttack:
                        Move();

                        break;
                    case ePlayerState.DiveAttack:

                        if (rigid.velocity.y <= 0f && !grounded)
                        {
                            anim.SetBool("jumping", false);
                            anim.SetBool("jumpattack", true);
                            rigid.velocity += Vector2.up * Physics.gravity * airAttackGravity * Time.deltaTime;

                            //45° angle in face direction downwards
                            if (!facingRight)
                                rigid.AddForce(new Vector2(-1, -1) * Time.deltaTime);
                            else if (facingRight)
                                rigid.AddForce(new Vector2(1, -1) * Time.deltaTime);
                        }
                        else if (grounded && anim.GetBool("jumpattack"))
                        {
                            GetAttackValues();
                            anim.SetTrigger("land");
                            anim.SetBool("jumpattack", false);
                            state = ePlayerState.Ready;
                        }

                        break;
                    case ePlayerState.Blocking:

                        anim.SetBool("walkbackwards", false);
                        anim.SetBool("walkforwards", false);
                        anim.SetBool("blocking", true);
                        stun = eStun.blocking;

                        if (Input.GetButtonUp("Block_" + PlayerIndex))
                        {
                            state = ePlayerState.Ready;
                            stun = eStun.normal;
                        }
                        //if (vertical < 0.95f && vertical >= 0f)
                        //{
                        //    print("not blocking");
                        //    anim.SetBool("blocking", false);
                        //    state = ePlayerState.Ready;
                        //}
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
        //anim = this.GetComponentInChildren<Animator>();
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

       public void Jump()
    {
        state = ePlayerState.InAir;
        grounded = false;
        rigid.AddForce(new Vector2(0f, JumpForce / 10));
        SVFXManager.instance.InstantiateDustJump(dustJumpY, this.gameObject);
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
                    anim.SetBool("walkforwards", false);
                    anim.SetBool("walkbackwards", true);
                    //print(gameObject.name + " läuft rückwärts");
                }
                else if ((facingRight && horizontal > 0.2f) || (!facingRight && horizontal < -0.2f))
                {
                    curMoveSpeed = MoveSpeedF;
                    anim.SetBool("walkbackwards", false);
                    anim.SetBool("walkforwards", true);
                    //print(gameObject.name + " läuft vorwärts");
                }
                else if (move == 0)
                {
                    curMoveSpeed = 0f;
                    anim.SetBool("walkbackwards", false);
                    anim.SetBool("walkforwards", false);
                    //print(gameObject.name + " steht");
                }
                break;

            case ePlayerState.InAir:
                curMoveSpeed = MoveSpeedJ;
                break;

            default:
                break;
        }
    }

    //Checks which attack has been used to get range and dmg
    public void GetAttackValues()
    {
        if (hitCheck)
        {
            hitCheck = false;

            switch (attack)
            {
                case eAttacks.None:
                    print("No attack mode set");
                    break;
                case eAttacks.Light:
                    dmg = attack1Dmg;
                    rangeX = hitrange1;
                    opponent.GetComponent<Player>().stun = eStun.normal;
                    break;
                case eAttacks.Heavy:
                    dmg = attack2Dmg;
                    rangeX = hitrange2;
                    opponent.GetComponent<Player>().stun = eStun.normal;
                    break;
                case eAttacks.Blockbreak:
                    dmg = blockbreakDmg;
                    rangeX = hitrangeBB;
                    if (opponent.GetComponent<Player>().state == ePlayerState.Blocking)
                        opponent.GetComponent<Player>().stun = eStun.blockbroken;
                    else
                        opponent.GetComponent<Player>().stun = eStun.normal;
                    break;
                case eAttacks.Jump:
                    dmg = jumpattackDmg;
                    rangeY = hitrangeJY;
                    rangeX = hitrangeJX;
                    opponent.GetComponent<Player>().stun = eStun.jumped;
                    break;
                case eAttacks.Dive:
                    //dmg = jumpattackDmg;
                    //range = hitrangeJA;
                    break;
                default:
                    break;
            }
            CheckForHit(dmg, rangeX, rangeY);
        }
    }

    //Checks if attack has gone through
    public void CheckForHit(float dmg, float rangeX, float rangeY)
    {
        float opponentX = opponent.transform.position.x;
        float opponentY = opponent.transform.position.y;
        Vector3 player = transform.position;

        if (attack == eAttacks.Jump)
        {
            if (facingRight)
            {
                if (player.y <= opponentY + rangeY && player.y >= opponentY)
                {
                    if (player.x >= opponentX - rangeX && player.x <= opponentX)
                    {
                        DealDmg(dmg);
                    }
                }
            }
            else if (!facingRight)
            {
                if (player.y >= opponentY - rangeY && player.y <= opponentY)
                {
                    if (player.x <= opponentX + rangeX && player.x >= opponentX)
                    {
                        DealDmg(dmg);
                    }
                }
            }
        }
        else if (opponent.GetComponent<Player>().grounded)
        {
            if (facingRight)
            {
                if (player.x >= opponentX - rangeX && player.x <= opponentX)
                {
                    DealDmg(dmg);
                }
            }
            else if (!facingRight)
            {
                if (player.x <= opponentX + rangeX && player.x >= opponentX)
                {
                    DealDmg(dmg);
                }
            }
        }
    }

    //Deal damage and special effects after successful hit
    public void DealDmg(float dmg)
    {
        float offset = opponent.GetComponent<Player>().HitOffset;

        if (opponent.GetComponent<Player>().state == ePlayerState.Blocking)
        {
            if (opponent.GetComponent<Player>().stun == eStun.blockbroken)
            {
                //    float scale;
                //    if (!opponent.GetComponent<Player>().facingRight)
                //        scale = -1;
                //    else
                //        scale = 1;

                print(gameObject.name + " deals " + dmg + " to " + opponent.name + " by breaking their shield");
                GameManager.instance.startSlowMo = true; 
                SVFXManager.instance.PlayVFX_HitMarker(offset, opponent.gameObject);
                SVFXManager.instance.PlayVFX_ComicPow(offset, opponent.gameObject);
                //SVFXManager.instance.InstantiateBreakShield(offset, scale, opponent.gameObject);                
                opponent.GetComponent<Player>().ApplyDamage(dmg);
                opponent.GetComponent<Player>().Knockdown(KBstrength);
        }
        else
            {
                float newdmg = dmg - (dmg * (blockPct / 100));
                print(gameObject.name + " deals " + dmg + " to the blocking " + opponent.name);
                GameManager.instance.startSlowMo = true;
                SVFXManager.instance.PlayVFX_HitMarker(offset, opponent.gameObject);
                SVFXManager.instance.PlayVFX_ComicPow(offset, opponent.gameObject);
                //opponent.GetComponent<Player>().shield.SetTrigger("show");
                opponent.GetComponent<Player>().ApplyDamage(newdmg);
            }
        }
        else
        {
            print(gameObject.name + " deals " + dmg + " to " + opponent.name);
            GameManager.instance.startSlowMo = true;
            SVFXManager.instance.PlayVFX_HitMarker(offset, opponent.gameObject);
            SVFXManager.instance.PlayVFX_ComicPow(offset, opponent.gameObject);
            opponent.GetComponent<Player>().ApplyDamage(dmg);
            opponent.GetComponent<Player>().Knockback(KBstrength);
        }
    }

    //Called by opponent on hit
    public void ApplyDamage(float dmgreceived)
    {
        print(gameObject.name + " got hit");

        if (stun == eStun.blocking)
        {
            CVoice.PlayVoiceSound();
            StartCoroutine(camShake.Shake(0.15f, 0.4f));
            hitPoints -= dmgreceived;
            GameManager.instance.UpdateHP();

        }
        else if (stun == eStun.blockbroken)
        {
            state = ePlayerState.Hurt;
            //anim.SetBool("knockdown", true);        //TODO implement knockdown anim   
            CVoice.PlayLongCrySound();
            StartCoroutine(camShake.Shake(0.15f, 0.4f));
            hitPoints -= dmgreceived;
            GameManager.instance.UpdateHP();
        }
        else if (stun == eStun.jumped)
        {
            state = ePlayerState.Hurt;
            anim.SetTrigger("longhurt");
            CVoice.PlayVoiceSound();
            StartCoroutine(camShake.Shake(0.15f, 0.4f));
            hitPoints -= dmgreceived;
            GameManager.instance.UpdateHP();
        }
        else if (stun == eStun.normal)
        {
            state = ePlayerState.Hurt;
            anim.SetTrigger("hurt");
            CVoice.PlayVoiceSound();
            StartCoroutine(camShake.Shake(0.15f, 0.4f));
            hitPoints -= dmgreceived;
            GameManager.instance.UpdateHP();
        }

        print(gameObject.name + "s HP: " + hitPoints);
        stun = eStun.normal;
    }

    //Knocks the player backwards
    public void Knockback(float strength)
    {
        print(gameObject.name + " is knocked back by " + strength);

        if (transform.position.x >= border && move > 0 || transform.position.x <= -border && move < 0)
            rigid.AddForce(new Vector2(-0.1f / 10, knockUp));
        else
        {
            if (facingRight)
            {
                rigid.AddForce(new Vector2(-strength / 10, knockUp));
            }
            else if (!facingRight)
            {
                rigid.AddForce(new Vector2(+strength / 10, knockUp));
            }
        }
    }

    //Knocks the player down
    public void Knockdown(float strength)
    {
        state = ePlayerState.Knockeddown;
        print(gameObject.name + " is knocked down");
        anim.SetBool("knockdown", true);

        if (transform.position.x >= border && move > 0 || transform.position.x <= -border && move < 0)
            rigid.AddForce(new Vector2(-0.5f / 10, 0f));
        else
        {
            if (facingRight)
            {
                rigid.AddForce(new Vector2(-strength / 10, knockUp));
            }
            else if (!facingRight)
            {
                rigid.AddForce(new Vector2(+strength / 10, knockUp));
            }
        }
    }
}
