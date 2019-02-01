using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CVoices Animation events to check for damage and finished animations
/// </summary>

public class CAnimationEvents : MonoBehaviour
{
    private Player player;
    //private float dmg1;
    //private float dmg2;
    //private float hr1;
    //private float hr2;

    void Start()
    {
        player = GetComponentInParent<Player>();
        //dmg1 = player.attack1Dmg;
        //dmg2 = player.attack2Dmg;
        //hr1 = player.hitrange1;
        //hr2 = player.hitrange2;
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

    public void JumpStartupFinished()
    {
        if (player.grounded)
        {
            player.grounded = false;
            player.GetComponent<Player>().Jump();
        }
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
}
