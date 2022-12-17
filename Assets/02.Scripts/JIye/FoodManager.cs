using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class FoodManager : MonoBehaviour
{
    //Temp
    [SerializeField] Food tempFood1;
    [SerializeField] Food tempFood2;

    public static FoodManager instance = null;

    public Food receip;
    [SerializeField] GameObject trashPrefab;

    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
    }

    /// <summary>
    /// 스테이지 레시피 설정
    /// </summary>
    /// <param name="food"></param>
    private void SetReceip(Food food)
    {
        receip = food;
    }

    /// <summary>
    /// merge 가능한지 체크
    /// </summary>
    /// <param name="food1"></param>
    /// <param name="food2"></param>
    public GameObject TryMerge(Food food1, Food food2, Vector2 Pos)
    {
        if(food1.foodName == food2.foodName)        //실패한 경우
        {
            Destroy(food1.gameObject);
            Destroy(food2.gameObject);

            //쓰레기
            return Instantiate(trashPrefab, Pos, Quaternion.identity);
        }

        if (food1.myLevel == food2.myLevel)
        {
            if (food1.myLevel == 4)       //기본 라멘에서 추가 제료가 들어가는 상태라면
            {
                Food mergeFood = food1.isRamen == true ? food1 : food2;
                Food deFood = food1.isRamen == false ? food1 : food2;
                return Merge(mergeFood, deFood);
            }
            else
            {
                Destroy(food2.gameObject);
                return Merge(food1, Pos);
            }
        }

        return food2.gameObject;
    }

    private GameObject Merge(Food food, Vector2 Pos)
    {
        Food mergeFood = null;

        mergeFood = Instantiate(food.nextFood[0], Pos, Quaternion.identity).GetComponent<Food>();


        if (mergeFood.isRamen)      //마지막 음식일때 체크
        {
            CheckReceip(mergeFood);
        }

        Destroy(food.gameObject);

        return mergeFood.gameObject;
    }

    private GameObject Merge(Food food1, Food food2)
    {
        Food mergeFood = null;
        Transform trans = food1.gameObject.transform;

        switch (food2.foodName)
        {
            case "고춧기름":
                mergeFood = Instantiate(food1.nextFood[0], trans.position, Quaternion.identity).GetComponent<Food>();

                break;

            case "와사비":
                mergeFood = Instantiate(food1.nextFood[1], trans.position, Quaternion.identity).GetComponent<Food>();

                break;
            default:
                break;
        }

        if (mergeFood.isRamen)      //마지막 음식일때 체크
        {
            //CheckReceip(mergeFood);
        }


        Destroy(food1.gameObject);
        Destroy(food2.gameObject);

        return mergeFood.gameObject;
    }

    private void CheckReceip(Food food)
    {
        if (food.foodName == receip.foodName && food.myLevel > 4)
        {
            //성공
        }
        else
        {
            //실패 => 쓰레기
            Instantiate(trashPrefab, food.gameObject.transform.position, Quaternion.identity);
        }
    }

}
