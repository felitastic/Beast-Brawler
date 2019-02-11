using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handling all SFX and VFX intantiations
/// </summary>

public class SVFXManager : MonoBehaviour
{
    public static SVFXManager instance = null;

    [Header("SOUND EFFECTS")]
    [Header("Attack Sounds")]
    public GameObject BB1;
    public GameObject BB2;
    public GameObject BB3;
    public GameObject Heavy1;
    public GameObject Heavy2;
    public GameObject Heavy3;
    public GameObject Light1;
    public GameObject Light2;
    public GameObject Light3;
    [Header("Hit Sounds")]
    public GameObject LightHit1;
    public GameObject LightHit2;
    public GameObject LightHit3;
    public GameObject HeavyHit1;
    public GameObject HeavyHit2;
    public GameObject HeavyHit3;
    [Header("Jump landing")]
    public GameObject Land1;
    public GameObject Land2;
    public GameObject Land3;
    [Header("Short Pain Screams")]
    public GameObject CVoice2;
    public GameObject CVoice3;
    public GameObject CVoice6;
    public GameObject CVoice7;
    [Header("Longer Pain Screams")]
    public GameObject CVoice1;
    public GameObject CVoice4;
    public GameObject CVoice5;
    public GameObject CVoice8;
    public GameObject CVoice9;
    [Header("Other claudia sounds")]
    public GameObject HeavyOpenFingers;
    public GameObject KnockdownImpact;
   
    [Header("CVoice Meelee Attack WHOOSH")]
    public GameObject Hit1;
    public GameObject Hit3;
    public GameObject Hit4;
    public GameObject Hit6;
    public GameObject Hit7;

    [Header("Hitting the opponent")]
    public GameObject Bang1;
    public GameObject Bang2;
    public GameObject Bang3;
    public GameObject Bang4;

    [Header("Announcer")]
    public GameObject Matchstart;
    public GameObject Matchfinish;
    public GameObject Close;
    public GameObject Affirmative;
    public GameObject Confirmed;
    public GameObject Roger;
    public GameObject Fight;
    public GameObject Start;

    [Header("Erstmal aussortiert")]
    public GameObject Hit2;
    public GameObject Hit5;    


    [Header("VISUAL EFFECTS")]
    public GameObject HitWhite;
    public GameObject HitMarker;
    public GameObject ComicPow;

    [Header("Animations")]
    public GameObject DustJump;
    public GameObject BreakShield;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    #region Attacks
    public void PlayLight1(Vector3 position)
    {
        GameObject.Instantiate(Light1, position, new Quaternion());
    }
    public void PlayLight2(Vector3 position)
    {
        GameObject.Instantiate(Light2, position, new Quaternion());
    }
    public void PlayLight3(Vector3 position)
    {
        GameObject.Instantiate(Light3, position, new Quaternion());
    }

    public void PlayHeavy1(Vector3 position)
    {
        GameObject.Instantiate(Heavy1, position, new Quaternion());
    }
    public void PlayHeavy2(Vector3 position)
    {
        GameObject.Instantiate(Heavy2, position, new Quaternion());
    }
    public void PlayHeavy3(Vector3 position)
    {
        GameObject.Instantiate(Heavy3, position, new Quaternion());
    }

    public void PlayBB1(Vector3 position)
    {
        GameObject.Instantiate(BB1, position, new Quaternion());
    }
    public void PlayBB2(Vector3 position)
    {
        GameObject.Instantiate(BB2, position, new Quaternion());
    }
    public void PlayBB3(Vector3 position)
    {
        GameObject.Instantiate(BB3, position, new Quaternion());
    }
    
    public void PlayLightHit1(Vector3 position)
    {
        GameObject.Instantiate(LightHit1, position, new Quaternion());
    }
    public void PlayLightHit2(Vector3 position)
    {
        GameObject.Instantiate(LightHit2, position, new Quaternion());
    }
    public void PlayLightHit3(Vector3 position)
    {
        GameObject.Instantiate(LightHit3, position, new Quaternion());
    }
    
    public void PlayHeavyHit1(Vector3 position)
    {
        GameObject.Instantiate(HeavyHit1, position, new Quaternion());
    }
    public void PlayHeavyHit2(Vector3 position)
    {
        GameObject.Instantiate(HeavyHit2, position, new Quaternion());
    }
    public void PlayHeavyHit3(Vector3 position)
    {
        GameObject.Instantiate(HeavyHit3, position, new Quaternion());
    }
    #endregion

    #region Voice
    public void PlayCVoice1(Vector3 position)
    {
        GameObject.Instantiate(CVoice1, position, new Quaternion());
    }
    public void PlayCVoice2(Vector3 position)
    {
        GameObject.Instantiate(CVoice2, position, new Quaternion());
    }
    public void PlayCVoice3(Vector3 position)
    {
        GameObject.Instantiate(CVoice3, position, new Quaternion());
    }
    public void PlayCVoice4(Vector3 position)
    {
        GameObject.Instantiate(CVoice4, position, new Quaternion());
    }
    public void PlayCVoice5(Vector3 position)
    {
        GameObject.Instantiate(CVoice5, position, new Quaternion());
    }
    public void PlayCVoice6(Vector3 position)
    {
        GameObject.Instantiate(CVoice6, position, new Quaternion());
    }
    public void PlayCVoice7(Vector3 position)
    {
        GameObject.Instantiate(CVoice7, position, new Quaternion());
    }
    public void PlayCVoice8(Vector3 position)
    {
        GameObject.Instantiate(CVoice8, position, new Quaternion());
    }
    public void PlayCVoice9(Vector3 position)
    {
        GameObject.Instantiate(CVoice9, position, new Quaternion());
    }
    #endregion

    #region LandSounds
    public void PlayLand1(Vector3 position)
    {
        GameObject.Instantiate(Land1, position, new Quaternion());
    }
    public void PlayLand2(Vector3 position)
    {
        GameObject.Instantiate(Land2, position, new Quaternion());
    }
    public void PlayLand3(Vector3 position)
    {
        GameObject.Instantiate(Land3, position, new Quaternion());
    }
    #endregion

    #region Other
    public void PlayHeavyOpenFingers(Vector3 position)
    {
        GameObject.Instantiate(HeavyOpenFingers, position, new Quaternion());
    }
    public void PlayKnockdownImpact(Vector3 position)
    {
        GameObject.Instantiate(KnockdownImpact, position, new Quaternion());
    }
    #endregion

    #region Announcer
    public void PlayMatchstart(Vector3 position)
    {
        GameObject.Instantiate(Matchstart, position, new Quaternion());
    }
    public void PlayMatchfinish(Vector3 position)
    {
        GameObject.Instantiate(Matchfinish, position, new Quaternion());
    }
    public void PlayClose(Vector3 position)
    {
        GameObject.Instantiate(Close, position, new Quaternion());
    }
    public void PlayAffirmative(Vector3 position)
    {
        GameObject.Instantiate(Affirmative, position, new Quaternion());
    }
    public void PlayConfirmed(Vector3 position)
    {
        GameObject.Instantiate(Confirmed, position, new Quaternion());
    }
    public void PlayRoger(Vector3 position)
    {
        GameObject.Instantiate(Roger, position, new Quaternion());
    }
    public void PlayFight(Vector3 position)
    {
        GameObject.Instantiate(Fight, position, new Quaternion());
    }
    public void PlayStart(Vector3 position)
    {
        GameObject.Instantiate(Start, position, new Quaternion());
    }
    #endregion    

    #region VFX
    public void PlayVFX_HitWhite(float offsetY, GameObject player)
    {
        GameObject hitWhite = Instantiate(HitWhite, new Vector2(player.transform.position.x, player.transform.position.y + offsetY), new Quaternion());
        hitWhite.transform.parent = player.transform;
    }

    public void PlayVFX_ComicPow(float offsetY, GameObject player)
    {
        GameObject comicPow = Instantiate(ComicPow, new Vector2(player.transform.position.x, player.transform.position.y + offsetY), new Quaternion());
        //comicPow.transform.parent = player.transform;
    }

    public void PlayVFX_HitMarker(float offsetY, GameObject player)
    {
        GameObject hitMarker = Instantiate(HitMarker, new Vector2(player.transform.position.x, player.transform.position.y + offsetY), new Quaternion());
        //hitMarker.transform.parent = player.transform;
    }
    #endregion

    #region Animations
    public void InstantiateDustJump(float offsetY, GameObject player)
    {
        GameObject dustJump = Instantiate(DustJump, new Vector2(player.transform.position.x, player.transform.position.y + offsetY), new Quaternion());
    }

    public void InstantiateBreakShield(float offsetY, float scale, GameObject player)
    {
        print("breaking shield");
        GameObject breakShield = Instantiate(BreakShield, new Vector2(player.transform.position.x, player.transform.position.y + offsetY), new Quaternion());
        breakShield.transform.localScale *= scale;
    }
    #endregion

    #region Old stuff
    public void PlaySFX_Hit1(Vector3 position)
    {
        GameObject.Instantiate(Hit1, position, new Quaternion());
    }
    public void PlaySFX_Hit2(Vector3 position)
    {
        GameObject.Instantiate(Hit2, position, new Quaternion());
    }
    public void PlaySFX_Hit3(Vector3 position)
    {
        GameObject.Instantiate(Hit3, position, new Quaternion());
    }
    public void PlaySFX_Hit4(Vector3 position)
    {
        GameObject.Instantiate(Hit4, position, new Quaternion());
    }
    public void PlaySFX_Hit5(Vector3 position)
    {
        GameObject.Instantiate(Hit5, position, new Quaternion());
    }
    public void PlaySFX_Hit6(Vector3 position)
    {
        GameObject.Instantiate(Hit6, position, new Quaternion());
    }
    public void PlayHit7(Vector3 position)
    {
        GameObject.Instantiate(Hit7, position, new Quaternion());
    }
    #endregion            
    
}
