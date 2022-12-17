using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int myLevel;                   //����
    [SerializeField] Sprite spriteData;         
    [SerializeField] string foodName;       //�����̸�

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
    /// ���� �ʱ�ȭ �Ҷ� ȣ�� �Ǵ� �Լ�
    /// </summary>
    private void InitFood()
    {
        spriteRenderer.sprite = spriteData;
    }

    /// <summary>
    /// ������ �������� ��
    /// </summary>
    /// <param name="mergeFood">������ ����</param>
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
    /// ���� �� ��Ȳ
    /// </summary>
    private void Merge()
    {

    }
}
