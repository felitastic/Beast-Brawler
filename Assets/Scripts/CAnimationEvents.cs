using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CVoices Animation events to check for damage and finished animations
/// </summary>

public class CAnimationEvents : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }
    
    public void HitCheck()
    {
        print("AE Hit1");
        player.GetAttackValues();
    }

    public void AttackFinished()
    {
        player.state = ePlayerState.Ready;
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
        player.state = ePlayerState.Ready;
    }

    public void ShieldBroken()
    {
        player.shield.ResetTrigger("break");
        //
    }
}
