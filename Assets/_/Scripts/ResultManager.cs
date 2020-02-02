using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : BaseManager
{
    [SerializeField] Text scoreText;
    [SerializeField] GameObject skip;

    bool isSkip = false;

    public override void Init ()
    {
        base.Init ();
        skip.SetActive (true);
        isSkip = false;
        StartCoroutine ("IEScoreResult");
    }

    IEnumerator IEScoreResult()
    {
        SoundManager.SetDrumRoll (true);
        int score = GameManager.instance.score;
        int up = 0;
        int p_score = Mathf.Abs(score);
        int s = (p_score / 200) + 1;
        while( Mathf.Abs (up) < Mathf.Abs(score) ) {
            if (isSkip) { break; }
            if (score > 0) {
                up += s;
            } else {
                up -= s;
            }

            scoreText.text = up.ToString ("00000");
            yield return null;
        }
        scoreText.text = score.ToString ("00000");
        skip.SetActive (false);

        SoundManager.SetDrumRoll (false);
        yield return null;
        SoundManager.SEFINISH();
    }

    public override void Close ()
    {
        StopAllCoroutines ();
        base.Close ();
    }

    public void OnClickBtn()
    {
        GameManager.instance.SetState (GameManager.STATE.title);
    }

    public void Skip()
    {
        isSkip = true;
    }
}
