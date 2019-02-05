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
        //set velocity to downward?
        //fall anim
        player.anim.SetBool("falling", true);
        player.state = ePlayerState.InAir;
    }

    public void JumpStartupFinished()
    {
        player.grounded = false;
        player.GetComponent<Player>().Jump();
        player.anim.SetBool("jumping", true);
        player.anim.SetBool("startup", false);
    }

    public void LandingFinished()
    {
        player.state = ePlayerState.Ready;
    }

    public void StartAirAttack()
    {
        player.state = ePlayerState.InAirAttack;
    }

    public void AirAttackFinished()
    {
       
    }

    public void HurtFinished()
    {
        player.state = ePlayerState.Ready;
    }
}
