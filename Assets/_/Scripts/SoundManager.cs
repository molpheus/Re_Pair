using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;

    [SerializeField] AudioClip tutorial;
    [SerializeField] AudioClip ok;
    [SerializeField] AudioClip ng;
    [SerializeField] AudioClip finish;
    [SerializeField] AudioClip scene;

    [SerializeField] AudioSource source;

    private void Awake ()
    {
        if (instance == null ) {
            instance = this;
        }
    }

    static public void SETutorial()
    {
        instance.source.PlayOneShot (instance.tutorial);
    }

    static public void SEOK()
    {
        instance.source.PlayOneShot (instance.ok);
    }

    static public void SENG()
    {
        instance.source.PlayOneShot (instance.ng);
    }

    static public void SEFINISH ()
    {
        instance.source.PlayOneShot (instance.finish);
    }

    static public void SESCENE ()
    {
        instance.source.PlayOneShot (instance.scene);
    }

    static public void SetDrumRoll(bool isPlay)
    {
        if ( isPlay ) instance.source.Play ();
        else instance.source.Stop ();
    }
}
