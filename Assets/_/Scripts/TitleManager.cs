using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : BaseManager
{
    public override void Init ()
    {
        base.Init ();
    }

    public void OnStartBtn ()
    {
        GameManager.instance.SetState (GameManager.STATE.tutorial);
    }
}
