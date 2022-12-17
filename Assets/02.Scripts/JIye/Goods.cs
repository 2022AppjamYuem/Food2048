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

    Image image;
    TMP_Text nameText;
    Button button;
    

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        nameText = GetComponentInChildren<TMP_Text>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        image.sprite = goodsSprite;
        nameText.text = goodsName;

        if (bought)     //�̹� ���� �� ����
        {
            button.interactable = false;
        }
    }


    /// <summary>
    /// ��ư�� ����� event �Լ�
    /// </summary>
    public void BuyGoodsBtnClick()
    {
        if(GameManager.Instance.money >= price)        //���� ���� ��
        {
            GameManager.Instance.money -= price;
            GameManager.Instance.boughtGoods[index] = true;
            image.sprite = soldOutSprite;
            button.interactable = false;
        }

    }
}
