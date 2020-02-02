using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance = null;
    public enum STATE
    {
        title,
        tutorial,
        Game,
        Result
    }

    public STATE state = STATE.title;

    public int score = 0;

    [SerializeField] TitleManager title;
    [SerializeField] TutorialManager tutorial;
    [SerializeField] CardManager card;
    [SerializeField] ResultManager result;
    [SerializeField] FadeImage fade;

    public float spd = 1f;

    private void Awake ()
    {
        if (instance == null) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        card.ScoreUpdate = (s) => {
            score += s;
            if (s > 0) {
                SoundManager.SEOK ();
            }
            else {
                SoundManager.SENG ();
            }
        };
        SetState (STATE.title);
    }

    public void SetState(STATE st)
    {
        StartCoroutine ("IEState", st);
    }
    IEnumerator IEState(STATE st)
    {
        fade.Range = 0;
        fade.gameObject.SetActive (true);
        yield return null;

        float delta = 0f;
        while(delta <= 1f) {
            delta += Mathf.Clamp01 (Time.deltaTime * spd);
            fade.Range = delta;
            yield return null;
        }

        switch ( state ) {
            case STATE.title: title.Close (); break;
            case STATE.tutorial: tutorial.Close (); break;
            case STATE.Game: card.Close (); break;
            case STATE.Result: result.Close (); break;
        }
        state = st;
        switch ( state ) {
            case STATE.title: title.Init (); score = 0; break;
            case STATE.tutorial: tutorial.Init (); break;
            case STATE.Game: card.Init (); break;
            case STATE.Result: result.Init (); break;
        }

        delta = 0f;
        while ( delta <= 1f ) {
            delta += Mathf.Clamp01 (Time.deltaTime * spd);
            fade.Range = 1 - delta;
            yield return null;
        }

        yield return null;
        fade.gameObject.SetActive (false);
        fade.Range = 0;
    }
}
