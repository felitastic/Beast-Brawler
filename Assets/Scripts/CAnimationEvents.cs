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

    //A1
    public void HitCheck1()
    {
        print("AE Hit1");
        player.GetAttackValues();
    }
    //A2
    public void HitCheck2()
    {
        print("AE Hit2");
        player.GetAttackValues();
    }

    //BB
    public void HitCheck3()
    {
        print("AE Hit3");
        player.GetAttackValues();
    }
    //HACK JumpAttack Dmg Check?
    public void HitCheck4()
    {
        print("AE Hit4");
        player.GetAttackValues();
    }

    public void Attack1Finished()
    {
        player.state = ePlayerState.Ready;
    }

    public void Attack2Finished()
    {
        player.state = ePlayerState.Ready;
    }

    public void BlockbreakerFinished()
    {
        player.state = ePlayerState.Ready;
    }

    public void JumpStartupFinished()
    {
        player.GetComponent<Player>().Jump();
        player.anim.SetBool("jumping", true);
    }

    public void LandingFinished()
    {
        player.anim.SetTrigger("landed");
    }

    public void StartAirAttack()
    {
        player.state = ePlayerState.InAirAttack;
    }

    public void AirAttackFinished()
    {
       
    }
}
