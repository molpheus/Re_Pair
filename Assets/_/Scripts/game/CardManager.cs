using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardManager : BaseManager
{
    /// <summary> </summary>
    CardObject.CARD_TYPE startPair_left = CardObject.CARD_TYPE.spade;
    /// <summary> </summary>
    CardObject.CARD_TYPE startPair_right = CardObject.CARD_TYPE.club;
    /// <summary> </summary>
    int startPair_id = 1;

    /// <summary> </summary>
    public CardObject.CARD_TYPE left_type = CardObject.CARD_TYPE.spade;
    /// <summary> </summary>
    public int left_id = 1;
    /// <summary> </summary>
    public CardObject.CARD_TYPE right_type = CardObject.CARD_TYPE.club;
    /// <summary> </summary>
    public int right_id = 1;

    /// <summary> </summary>
    public CardObject leftObj;
    /// <summary> </summary>
    public CardObject rightObj;

    /// <summary> </summary>
    public UnityAction<int> ScoreUpdate;

    /// <summary> </summary>
    public List<CardObject> cardList = new List<CardObject>();

    /// <summary> </summary>
    float wait = 0;
    /// <summary> </summary>
    bool isEnd = true;
    /// <summary> </summary>
    [SerializeField] Slider waitSlider;
    /// <summary> </summary>
    [SerializeField] Image sliderColor;
    /// <summary> </summary>
    [SerializeField] Gradient gradient;

    /// <summary> </summary>
    readonly float waitTimer = 3;
    /// <summary>
    /// 
    /// </summary>
    private void Update ()
    {
        if ( isEnd ) return;

        wait += Mathf.Clamp01 (Time.deltaTime);
        if ( wait > waitTimer ) {
            wait = waitTimer;
            isEnd = true;
            GameManager.instance.SetState (GameManager.STATE.Result);
        }

        waitSlider.value = 1f - (wait / waitTimer);
        sliderColor.color = gradient.Evaluate (waitSlider.value);
    }

    /// <summary>
    /// 
    /// </summary>
    private void Start ()
    {
        leftObj.selectCard = UpdateCardSelect;
        rightObj.selectCard = UpdateCardSelect;

        for(int i = 0; i < cardList.Count; i++ ) {
            cardList[i].selectCard = UpdateCardSelect;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    override public void Init ()
    {
        base.Init ();
        wait = 0;
        isEnd = false;
        left_type = startPair_left;
        left_id = startPair_id;
        right_type = startPair_right;
        right_id = startPair_id;

        leftObj.SetCard (left_type, left_id);
        rightObj.SetCard (right_type, right_id);

        CreateNewList ();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    public void UpdateCardSelect(GameObject target)
    {
        int isSelectID = -1;
        // 選択可能なのは片方のみ
        if (leftObj.gameObject == target) {
            if (leftObj.SetWakuActive && rightObj.SetWakuActive) {
                rightObj.SetWakuActive = false;
            }
            for ( int i = 0; i < cardList.Count; i++ ) {
                {
                    if ( cardList[i].SetWakuActive ) {
                        isSelectID = i;
                    }
                }
            }
        }
        // 選択可能なのは片方のみ
        else if ( rightObj.gameObject == target ) {
            if ( leftObj.SetWakuActive && rightObj.SetWakuActive ) {
                leftObj.SetWakuActive = false;
            }
            for ( int i = 0; i < cardList.Count; i++ ) {
                {
                    if ( cardList[i].SetWakuActive ) {
                        isSelectID = i;
                    }
                }
            }
        }
        else {
            // 上部２箇所のカード以外が選択されている
            for(int i = 0; i < cardList.Count; i++ ) {
                if (cardList[i].gameObject != target) {
                    cardList[i].SetWakuActive = false;
                } 
                else {
                    if (cardList[i].SetWakuActive) {
                        isSelectID = i;
                    }
                }
            }
        }

        // 両方選択されているか確認
        if ((leftObj.SetWakuActive || rightObj.SetWakuActive) 
            && isSelectID != -1) {
            CardObject selectObj = leftObj.SetWakuActive ? leftObj : rightObj;
            CardObject noSelectObj = !leftObj.SetWakuActive ? leftObj : rightObj;
            
            // 残った側と新しくペアになるもので判定を行う
            if (( noSelectObj.card_id == cardList[isSelectID].card_id ) &&
                ( noSelectObj.card_type == cardList[isSelectID].card_type ) ) {
                ScoreUpdate (200);
            }
            else if ( noSelectObj.card_id == cardList[isSelectID].card_id) {
                ScoreUpdate (100);
            }
            else if ( noSelectObj.card_type == cardList[isSelectID].card_type) {
                ScoreUpdate (50);
            }
            else {
                //ScoreUpdate (0);
                StartCoroutine ("IEEnd");
                return;
            }
            
            // 選択されている側のカードを新しいカードで更新する
            selectObj.SetCard (cardList[isSelectID].card_type, cardList[isSelectID].card_id);
            CreateNewList ();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator IEEnd()
    {
        isEnd = true;
        ScoreUpdate (0);
        yield return new WaitForSeconds(0.2f);
        GameManager.instance.SetState (GameManager.STATE.Result);
    }

    /// <summary> </summary>
    int createListCnt = 0;
    /// <summary> </summary>
    int max = 10;
    /// <summary>
    /// 
    /// </summary>
    void CreateNewList()
    {
        //if (max <= createListCnt) {
        //    GameManager.instance.SetState (GameManager.STATE.Result);
        //}
        //createListCnt++;

        for(int i = 0; i < cardList.Count; i++ ) {
            int type = Random.Range(0, 4);
            int id = Random.Range (1, 13);
            cardList[i].SetCard ((CardObject.CARD_TYPE)type, id);
        }

        wait = 0;
    }
}
