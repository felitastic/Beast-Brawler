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
    
    [Header("NOT FOR GDs! DO NOT TOUCH OR OUCH! ----------------------")]
    public int PlayerIndex;
    public float JumpForce;
    [Tooltip("Current hitpoints")]
    public float hitPoints;
    public float horizontal;
    public float vertical;
    public bool facingRight;
    public float move = 0f;
    [Tooltip("When Attack1 deals damage")]
    public float attack1Hit = 0.2f; 
    [Tooltip("When Attack2 deals damage")]
    public float attack2Hit = 0.2f;
    public float hitrange1 = 2f;    //hitrange of attack1
    public float hitrange2 = 2f;    //hitrange of attack2
    public float hitrangeBB = 2f;   //hitrange of the blockbreaker
    public float jumpHurt = 2.5f;
    public bool grounded;
    
    [Header("Components the script grabs itself")]
    public SpriteRenderer sprite;
    public Animator anim;
    public Rigidbody2D rigid;
    public CamShake camShake;
    public GameObject opponent;

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
        state = ePlayerState.Ready;
    }
    public void FixedUpdate()
    {
        //if (GameManager.instance.GameMode == eGameMode.Running)
        //{
        //    if (state == ePlayerState.Ready)
        //    {
        //        if (vertical == -1f)
        //        {
        //            print("jump");
        //            state = ePlayerState.InAir;
        //            //StartCoroutine(Jump());
        //            Jump();
        //        }
        //    }

        //    rigid.AddForce(new Vector3(0f, extraGravity, 0f));
        //}
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

                        Move();

                        Flip();

                        if (Input.GetButtonDown("Attack1_" + PlayerIndex))
                        {
                            print("Attack1");
                            StartCoroutine(Attack1());
                        }

                        if (Input.GetButtonDown("Attack2_" + PlayerIndex))
                        {
                            print("Attack2");
                            StartCoroutine(Attack2());
                        }

                        else if (vertical == 1f)
                        {
                            print("block");
                            //Jump if up
                            //Check if jumping, check if landed
                            //ground check via touching of the floor collider
                            //Block if down
                        }

                        if (vertical == -1f) //TODO physics in fixedupdate?
                        {
                            print("jump");
                            state = ePlayerState.InAir;
                            Jump();
                            //TODO trigger jump and fall animation
                        }

                        if (Input.GetButtonDown("Breaker_" + PlayerIndex))
                        {
                            print("Blockbreaker");
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
                    case ePlayerState.Stunned:
                        break;
                    case ePlayerState.Dead:
                        //call KO
                        //win counter
                        //next match or result
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
        print("checking for ground");
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
        StartCoroutine(camShake.Shake(0.15f, 0.4f));
        anim.Play("hurt");
        Debug.Log(gameObject.name + " got hit.");
        hitPoints -= dmgreceived;
        print(gameObject.name + "s HP: " + hitPoints);
        GameManager.instance.UpdateHits();
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
                print(gameObject.name + " läuft rückwärts");
                //rückwärts
            }
            else if(horizontal > 0.2f)
            {
                //anim.SetBool("walk", true);
                print(gameObject.name + " läuft vorwärts");
                //vorwärts
            }
        }
        else if (!facingRight)
            if (horizontal < -0.2f)
            {
                //anim.SetBool("walk", true);
                print(gameObject.name + " läuft vorwärts");
                //vorwärts
            }
            else if (horizontal > 0.2f)
            {
                print(gameObject.name + " läuft rückwärts");
                //rückwärts
            }
        else if (move == 0)
            {
                //anim.SetBool("walk", false);
                print(gameObject.name + " steht");
            }

        transform.Translate(new Vector2(move, 0) * MoveSpeed * Time.deltaTime);
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
        yield return new WaitForSeconds(attack1Hit);
        CheckForHit(attack1Dmg, hitrange1);
        state = ePlayerState.Ready;
    }

    IEnumerator Attack2()
    {
        if (state != ePlayerState.Ready)
            yield break;

        state = ePlayerState.Attacking;
        anim.SetTrigger("attack2");
        yield return new WaitForSeconds(attack2Hit);
        CheckForHit(attack2Dmg, hitrange2);
        state = ePlayerState.Ready;
    }

    public void CheckForHit(float dmg, float hitrange)
    {
        float opponentX = opponent.transform.position.x;
        float opponentY = opponent.transform.position.y;
        Vector3 player = transform.position;

        //Check if opponent is grounded)
        if (opponent.GetComponent<Player>().grounded == true)
        {
            if (facingRight)
            {
                if (player.x >= opponentX - hitrange && player.x <= opponentX) //&& opponent not blocking
                {
                    print(gameObject.name + " hits " + opponent.name);
                    opponent.GetComponent<Player>().ApplyDamage(dmg);
                }
            }
            else if (!facingRight)
            {
                if (player.x <= opponentX + hitrange && player.x >= opponentX) //&& opponent not blocking
                {
                    print(gameObject.name + " hits " + opponent.name);
                    opponent.GetComponent<Player>().ApplyDamage(dmg);
                }
            }
        }
        else if (opponent.GetComponent<Player>().grounded == false)
        {   //check if jumping player is in range
            if (opponentY == jumpHurt)
            {
                if (facingRight)
                {
                    if (player.x >= opponentX - hitrange && player.x <= opponentX) //&& opponent not blocking
                    {
                        print(gameObject.name + " hits " + opponent.name);
                        opponent.GetComponent<Player>().ApplyDamage(dmg);
                    }
                }
                else if (!facingRight)
                {
                    if (player.x <= opponentX + hitrange && player.x >= opponentX) //&& opponent not blocking
                    {
                        print(gameObject.name + " hits " + opponent.name);
                        opponent.GetComponent<Player>().ApplyDamage(dmg);
                    }
                }
            }
        }


        else
            return;

    }
}
