using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CVoices Sounds
/// </summary>

public class CSounds : MonoBehaviour
{


    public void PlayAttackSound()
    {
        //Getting a value to choose a sound from (last is excluded)
        int ran = Random.Range(1, 6);

        switch (ran)
        {
            case 1:
                SVFXManager.instance.PlaySFX_Hit1(transform.position);
                break;
            case 2:
                SVFXManager.instance.PlaySFX_Hit6(transform.position);
                break;
            case 3:
                SVFXManager.instance.PlaySFX_Hit3(transform.position);
                break;
            case 4:
                SVFXManager.instance.PlaySFX_Hit4(transform.position);
                break;
            case 5:
                SVFXManager.instance.PlayHit7(transform.position);
                break;
            default:
                break;
        }
    }

    public void PlayVoiceSound()
    {
        //Getting a value to choose a sound from (last is excluded)
        int ran = Random.Range(1, 5);

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

    public void PlayImpactSound()
    {
        //Getting a value to choose a sound from (last is excluded)
        int ran = Random.Range(1, 3);

        switch (ran)
        {
            case 1:
                SVFXManager.instance.PlayBang1(transform.position);
                break;
            case 2:
                SVFXManager.instance.PlayBang2(transform.position);
                break;
            default:
                break;
        }
    }
}
