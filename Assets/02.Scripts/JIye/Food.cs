using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int myLevel;                   //레벨
    [SerializeField] string foodName;       //음식이름


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
        FoodManager.instance.Merge(this);
    }
}
