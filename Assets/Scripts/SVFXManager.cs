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
    [Header("CVoice Meelee Attack WHOOSH")]
    public GameObject Hit1;
    public GameObject Hit3;
    public GameObject Hit4;
    public GameObject Hit6;
    public GameObject Hit7;

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
    
    [Header("")]
    public GameObject Attack1;
    public GameObject Attack2;
    public GameObject Blockbreak;
    public GameObject BlockHit;
    public GameObject KickInAir;
    public GameObject KnockdownLand;
    public GameObject JumpLand;
    public GameObject JumpStart;

    [Header("Erstmal aussortiert")]
    public GameObject Hit2;
    public GameObject Hit5;
       
    [Header("Jump landing")]
    public GameObject Land1;
    public GameObject Land2;
    public GameObject Land3;

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

    #region SFX Hit
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

    public void PlayBang1(Vector3 position)
    {
        GameObject.Instantiate(Bang1, position, new Quaternion());
    }
    public void PlayBang2(Vector3 position)
    {
        GameObject.Instantiate(Bang2, position, new Quaternion());
    }

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

    
}
