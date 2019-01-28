using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animation events to check for damage and finished animations
/// </summary>

public class AnimationEvents : MonoBehaviour
{
    private Player player;
    private float dmg1;
    private float dmg2;
    private float hr1;
    private float hr2;
    private float offsetC;
    private float offsetB;

    void Start ()
    {
        player = GetComponentInParent<Player>();
        dmg1 = player.attack1Dmg;
        dmg2 = player.attack2Dmg;
        hr1 = player.hitrange1;
        hr2 = player.hitrange2;
        offsetC = player.HitOffsetC;
        offsetB = player.HitOffsetB;


    }
	
    public void HitCheck1()
    {
        print("AE Hit1");
        player.CheckForHit(dmg1, hr1, offsetC);
    }

    public void HitCheck2()
    {
        print("AE Hit2");
        player.CheckForHit(dmg2, hr2, offsetC);
    }

    public void Attack1Finished()
    {
        print("AE Attack1 finished");
        player.state = ePlayerState.Ready;
    }

    public void Attack2Finished()
    {
        player.state = ePlayerState.Ready;
    }

}
