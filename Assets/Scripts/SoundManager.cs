using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    [Header("Meelee Attack WHOOSH")]
    public GameObject Hit1;
    public GameObject Hit2;
    public GameObject Hit3;
    public GameObject Hit4;
    public GameObject Hit5;
    public GameObject Hit6;
    public GameObject Hit7;

    [Header("Hitting something")]
    public GameObject Bang1;
    public GameObject Bang2;
    public GameObject Bang3;
    public GameObject Bang4;

    [Header("Jump landing")]
    public GameObject Land1;
    public GameObject Land2;
    public GameObject Land3;

    [Header("Hurt")]
    public GameObject Ouch1;
    public GameObject Ouch2;
    public GameObject Ouch3;

    [Header("Menus and Buttons")]
    public GameObject Button1;
    public GameObject Button2;


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

    public void PlayHit1(Vector3 position)
    {
        GameObject.Instantiate(Hit1, position, new Quaternion());
    }
    public void PlayHit2(Vector3 position)
    {
        GameObject.Instantiate(Hit2, position, new Quaternion());
    }
    public void PlayHit3(Vector3 position)
    {
        GameObject.Instantiate(Hit3, position, new Quaternion());
    }
    public void PlayHit4(Vector3 position)
    {
        GameObject.Instantiate(Hit4, position, new Quaternion());
    }
    public void PlayHit5(Vector3 position)
    {
        GameObject.Instantiate(Hit5, position, new Quaternion());
    }
    public void PlayHit6(Vector3 position)
    {
        GameObject.Instantiate(Hit6, position, new Quaternion());
    }
    public void PlayHit7(Vector3 position)
    {
        GameObject.Instantiate(Hit7, position, new Quaternion());
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
