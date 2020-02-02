using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : BaseManager
{
    public override void Init ()
    {
        base.Init ();
        SoundManager.SETutorial ();
    }

    public override void Close ()
    {
        base.Close ();
    }

    public void OnClickBtn()
    {
        GameManager.instance.SetState (GameManager.STATE.Game);
    }
}
