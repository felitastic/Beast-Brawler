using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CVoices Animation events to check for damage and finished animations
/// </summary>

public class CAnimationEvents : MonoBehaviour
{
    public Player player;
    private CamShake camShake;
    public ShadowBehaviour shade;

    void Start()
    {
        player = GetComponentInParent<Player>();
        camShake = FindObjectOfType<CamShake>();
    }
    
    public void HitCheck()
    {
        print("AE Hit1");
        player.GetAttackValues();
    }   
    
    public void Attack1()
    {
        if (player.attack1)
            player.attack1 = false;
        else
            player.attack1 = true;
    }
    
    public void Attack2()
    {
        if (player.attack2)
            player.attack2 = false;
        else
            player.attack2 = true;
    }
    
    public void HeavyOpenFingers()
    {
        SVFXManager.instance.PlayHeavyOpenFingers(player.transform.position);
    }

    public void AttackFinished()
    {
        player.state = ePlayerState.Ready;
    }

    public void HeavyAScreenShake()
    {
        StartCoroutine(camShake.Shake(0.15f, 0.4f));
    }

    public void JumpAttackFinished()
    {
        player.anim.ResetTrigger("jumpattack");
        player.state = ePlayerState.InAir;
    }

    public void JumpStartupFinished()
    {
        player.anim.SetBool("startup", false);
        player.GetComponent<Player>().Jump();
    }

    public void LandingFinished()
    {
        player.state = ePlayerState.Ready;
    }

    public void StartAirAttack()
    {
        player.state = ePlayerState.InAirAttack;
    }    

    public void HurtFinished()
    {
        print("hurt done");
        player.state = ePlayerState.Ready;
    }

    public void ShieldBroken()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void ShieldHit()
    {
        //player.shield.ResetTrigger("show");
        player.state = ePlayerState.Ready;
    }

    public void DustFinish()
    {
        GameObject.Destroy(this.gameObject);
    }

    //After getting knocked down and getting back up
    public void GotUp()
    {
        player.state = ePlayerState.Ready;
    }    

    public void KnockDownFinished()
    {
        player.anim.SetBool("knockdown", false);
        player.anim.SetTrigger("getup");
    }

    public void KnockDownScale()
    {
        shade.NewScale(1.75f, 0.7f);
    }    

    public void VictoryDone()
    {
        player.anim.SetBool("victorydone", true);
    }    
}
