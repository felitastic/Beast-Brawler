using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    //Claudia
    public GameObject c1Hitsound;
    public GameObject c1PainSound;
    public GameObject c1DieSound;
    public GameObject c1CurseSound;

    //Bruno
    public GameObject b1Hitsound;
    public GameObject b1PainSound;
    public GameObject b1DieSound;
    public GameObject b1CurseSound;

    //Ingame SFX
    public GameObject stageClearSound;
    public GameObject wipeOutSound;
    public GameObject sireneSound;

    //Menu SFX
    public GameObject buttonTouchSound;
    public GameObject buttonClickSound;
    public GameObject sliderMoveSound;

   

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

    //public void B1Hitsound(Vector3 position)
    //{
    //    GameObject.Instantiate(b1Hitsound, position, new Quaternion());
    //}

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
