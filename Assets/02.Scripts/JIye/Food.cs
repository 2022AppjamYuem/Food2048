using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int myLevel;                   //레벨
    [SerializeField] Sprite spriteData;         
    [SerializeField] string foodName;       //음식이름

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InitFood();
    }

    /// <summary>
    /// 음식 초기화 할때 호출 되는 함수
    /// </summary>
    private void InitFood()
    {
        spriteRenderer.sprite = spriteData;
    }

    /// <summary>
    /// 음식이 합쳐졌을 때
    /// </summary>
    /// <param name="mergeFood">합쳐질 음식</param>
    protected void TryMerge(Food mergeFood)
    {
        if(mergeFood.myLevel == this.myLevel)
        {
            Merge();
        }
        else
        {

        }
    }

    /// <summary>
    /// 머지 된 상황
    /// </summary>
    private void Merge()
    {

    }
}
