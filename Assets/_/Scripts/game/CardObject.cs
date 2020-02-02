using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.Events;
public class CardObject : MonoBehaviour
{
    /// <summary>  </summary>
    [SerializeField] Image card;
    /// <summary>  </summary>
    [SerializeField] GameObject waku;
    /// <summary>  </summary>
    [SerializeField] SpriteAtlas atlas;

    /// <summary>  </summary>
    private string imageName = "card_{0}_{1}";

    /// <summary>  </summary>
    public enum CARD_TYPE : int
    {
        spade = 0,
        club = 1,
        heart = 2,
        diamond = 3
    }

    /// <summary>  </summary>
    public CARD_TYPE card_type { get; private set; }
    /// <summary>  </summary>
    public int card_id { get; private set; }

    public UnityAction<GameObject> selectCard;

    /// <summary>  </summary>
    public bool SetWakuActive {
        get { return waku.activeSelf; }
        set {
            waku.SetActive (value);
        }
    }

    /// <summary>
    /// カードの設定を行う
    /// </summary>
    /// <param name="type"></param>
    /// <param name="id"></param>
    public void SetCard(CARD_TYPE type, int id)
    {
        id = Mathf.Clamp (id, 1, 13);
        if (atlas != null) {
            Sprite sp = atlas.GetSprite (
                string.Format (
                    imageName,
                    GetCardString(type),
                    id.ToString("00")
                )
            );
            card.sprite = sp;
        }
        card_type = type;
        card_id = id;

        SetWakuActive = false;
    }

    /// <summary>
    /// カードのTypeごとに取得したい文字列
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string GetCardString(CARD_TYPE type)
    {
        string str = "";

        switch(type) {
            case CARD_TYPE.spade: str = "spade"; break;
            case CARD_TYPE.club: str = "club"; break;
            case CARD_TYPE.heart: str = "heart"; break;
            case CARD_TYPE.diamond: str = "diamond"; break;
        }

        return str;
    }

    public void OnClickCard()
    {
        SetWakuActive = !SetWakuActive;
        selectCard (this.gameObject);
    }
}
