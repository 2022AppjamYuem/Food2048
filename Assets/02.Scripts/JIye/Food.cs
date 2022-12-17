using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int myLevel;                   //����
    [SerializeField] string foodName;       //�����̸�


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
        FoodManager.instance.Merge(this);
    }
}
