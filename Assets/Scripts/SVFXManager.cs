using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handling all SFX and VFX intantiations
/// </summary>

public class SVFXManager : MonoBehaviour
{
    public static SVFXManager instance = null;

    [Header ("SOUND EFFECTS")]
    [Header("Claudia Meelee Attack WHOOSH")]
    public GameObject Hit1;
    public GameObject Hit3;
    public GameObject Hit4;
    public GameObject Hit6;
    public GameObject Hit7;

    [Header("Claudia Voice")]
    public GameObject CVoice2;
    public GameObject CVoice3;
    public GameObject CVoice6;
    public GameObject CVoice7;

    [Header("Hitting the opponent")]
    public GameObject Bang1;
    public GameObject Bang2;
    public GameObject Bang3;
    public GameObject Bang4;

    [Header("Erstmal aussortiert")]
    public GameObject Hit2;
    public GameObject Hit5;
    public GameObject CVoice1;
    public GameObject CVoice4;
    public GameObject CVoice5;
    public GameObject CVoice8;
    public GameObject CVoice9;
       
    [Header("Jump landing")]
    public GameObject Land1;
    public GameObject Land2;
    public GameObject Land3;

    [Header("Menus and Buttons")]
    public GameObject Button1;
    public GameObject Button2;

    [Header("VISUAL EFFECTS")]
    public GameObject HitWhite;
    public GameObject ComicPow;

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

    //SFX
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

    public void PlayBang1(Vector3 position)
    {
        GameObject.Instantiate(Bang1, position, new Quaternion());
    }
    public void PlayBang2(Vector3 position)
    {
        GameObject.Instantiate(Bang2, position, new Quaternion());
    }

    // VFX
    public void PlayVFX_HitWhite(float offsetY, GameObject player)
    {
        GameObject hitWhite = Instantiate(HitWhite, new Vector2(player.transform.position.x, player.transform.position.y + offsetY), new Quaternion());
        hitWhite.transform.parent = player.transform;
    }

    public void PlayVFX_ComicPow(float offsetY, GameObject player)
    {
        GameObject comicPow = Instantiate(ComicPow, new Vector2(player.transform.position.x, player.transform.position.y + offsetY), new Quaternion());
        comicPow.transform.parent = player.transform;
    }



    //public void B1PainSound(Vector3 position)
    //{
    //    GameObject.Instantiate(b1PainSound, position, new Quaternion());
    //}
    //public void B1DieSound(Vector3 position)
    //{
    //    GameObject.Instantiate(b1DieSound, position, new Quaternion());
    //}
    //public void B1CurseSound(Vector3 position)
    //{
    //    GameObject.Instantiate(b1CurseSound, position, new Quaternion());
    //}
    //public void E1Hitsound(Vector3 position)
    //{
    //    GameObject.Instantiate(e1Hitsound, position, new Quaternion());
    //}
    //public void E1PainSound(Vector3 position)
    //{
    //    GameObject.Instantiate(e1PainSound, position, new Quaternion());
    //}
    //public void E1DieSound(Vector3 position)
    //{
    //    GameObject.Instantiate(e1DieSound, position, new Quaternion());
    //}
    //public void StageClearSound(Vector3 position)
    //{
    //    GameObject.Instantiate(stageClearSound, position, new Quaternion());
    //}
    //public void WipeOutSound(Vector3 position)
    //{
    //    GameObject.Instantiate(wipeOutSound, position, new Quaternion());
    //}
    //public void SireneSound(Vector3 position)
    //{
    //    GameObject.Instantiate(sireneSound, position, new Quaternion());
    //}
    //... etc

}
