using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All sounds regarding Claudia
/// </summary>

public class CSounds : MonoBehaviour
{
    #region Attack1
    public void PlayAttack1Sound()
    {
        //Getting a value to choose a sound from (last is excluded)
        int ran = Random.Range(1, 4);

        switch (ran)
        {
            case 1:
                SVFXManager.instance.PlayLight1(transform.position);
                break;
            case 2:
                SVFXManager.instance.PlayLight2(transform.position);
                break;
            case 3:
                SVFXManager.instance.PlayLight3(transform.position);
                break;
            default:
                break;
        }
    }
    #endregion
    #region Attack2
    public void PlayAttack2Sound()
    {
        //Getting a value to choose a sound from (last is excluded)
        int ran = Random.Range(1, 4);

        switch (ran)
        {
            case 1:
                SVFXManager.instance.PlayHeavy1(transform.position);
                break;
            case 2:
                SVFXManager.instance.PlayHeavy2(transform.position);
                break;
            case 3:
                SVFXManager.instance.PlayHeavy3(transform.position);
                break;
            default:
                break;
        }
    }
    #endregion
    #region BlockBreak 
    public void PlayBlockBreakSound()
    {
        //Getting a value to choose a sound from (last is excluded)
        int ran = Random.Range(1, 4);

        switch (ran)
        {
            case 1:
                SVFXManager.instance.PlayBB1(transform.position);
                break;
            case 2:
                SVFXManager.instance.PlayBB2(transform.position);
                break;
            case 3:
                SVFXManager.instance.PlayBB3(transform.position);
                break;
            default:
                break;
        }
    }
    #endregion
    #region Short Hurt Cry
    public void PlayVoiceSound()
    {
        //Getting a value to choose a sound from (last is excluded)
        int ran = Random.Range(1, 4);

        switch (ran)
        {
            case 1:
                SVFXManager.instance.PlayCVoice6(transform.position);
                break;
            case 2:
                SVFXManager.instance.PlayCVoice2(transform.position);
                break;
            case 3:
                SVFXManager.instance.PlayCVoice3(transform.position);
                break;
            case 4:
                SVFXManager.instance.PlayCVoice7(transform.position);
                break;
            default:
                break;
        }
    }
    #endregion
    #region Long Hurt Cry
    public void PlayLongCrySound()
    {
        //Getting a value to choose a sound from (last is excluded)
        int ran = Random.Range(1, 4);

        switch (ran)
        {
            case 1:
                SVFXManager.instance.PlayCVoice1(transform.position);
                break;
            case 2:
                SVFXManager.instance.PlayCVoice4(transform.position);
                break;
            case 3:
                SVFXManager.instance.PlayCVoice5(transform.position);
                break;
            default:
                break;
        }
    }
    #endregion
    #region Jump Landing SFX
    public void PlayLandSound()
    {
        //Getting a value to choose a sound from (last is excluded)
        int ran = Random.Range(1, 4);

        switch (ran)
        {
            case 1:
                SVFXManager.instance.PlayLand1(transform.position);
                break;
            case 2:
                SVFXManager.instance.PlayLand2(transform.position);
                break;
            case 3:
                SVFXManager.instance.PlayLand3(transform.position);
                break;
            default:
                break;
        }
    }
    #endregion

    #region Jump
    public void PlayTakeOffSound()
    {
        //Getting a value to choose a sound from (last is excluded)
        int ran = Random.Range(1, 4);

        switch (ran)
        {
            case 1:
                SVFXManager.instance.PlayLight1(transform.position);
                break;
            case 2:
                SVFXManager.instance.PlayLight2(transform.position);
                break;
            case 3:
                SVFXManager.instance.PlayLight3(transform.position);
                break;
            default:
                break;
        }
    }
    #endregion

    #region Impact Knockdown
    public void PlayImpactSounds()
    {
        //Getting a value to choose a sound from (last is excluded)
        int ran = Random.Range(1, 3);

        switch (ran)
        {
            case 1:
                SVFXManager.instance.PlayImpact2(transform.position);
                break;
            case 2:
                SVFXManager.instance.PlayImpact3(transform.position);
                break;
            default:
                break;
        }
    }
    #endregion


    public void PlayImpactSound()
    {
        //Getting a value to choose a sound from (last is excluded)
        int ran = Random.Range(1, 2);

        switch (ran)
        {
            case 1:
                SVFXManager.instance.PlayKnockdownImpact(transform.position);
                break;
            default:
                break;
        }
    }
}
