using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Goods : MonoBehaviour
{
    public string goodsName;
    public Sprite goodsSprite;
    public Sprite soldOutSprite;
    public int price;
    [SerializeField] int index;

    bool bought;

    SpriteRenderer spriteRenderer;
    Button button;
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        button = GetComponent<Button>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        spriteRenderer.sprite = goodsSprite;

        if (bought)     //이미 구매 된 상태
        {
            button.interactable = false;
        }
    }


    /// <summary>
    /// 버튼에 연결될 event 함수
    /// </summary>
    public void BuyGoodsBtnClick()
    {
        if(GameManager.Instance.saveData.money >= price)        //돈이 있을 때
        {
            GameManager.Instance.CalculateMoney(-1 * price);
            GameManager.Instance.boughtGoods[index] = true;
            spriteRenderer.sprite = soldOutSprite;
            button.interactable = false;
        }

    }
}
