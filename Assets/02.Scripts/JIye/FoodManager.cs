using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        //어디서 트리거?
        TryMerge(tempFood1, tempFood2);
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
    public void TryMerge(Food food1, Food food2)
    {

        if(food1.foodName == food2.foodName)
        {
            //쓰레기
            Instantiate(trashPrefab, food1.gameObject.transform.position, Quaternion.identity);

            Destroy(food1.gameObject);
            Destroy(food2.gameObject);

            return;
        }

        if (food1.nextFood != null)
        {
            Merge(food1);
            Destroy(food2.gameObject);
        }
        else
        {
        }

    }

    private void Merge(Food food)
    {
        Food mergeFood = null;
        Transform trans = food.gameObject.transform;


        mergeFood = Instantiate(food.nextFood, trans.position, Quaternion.identity).GetComponent<Food>();

        if (mergeFood.isFinal)      //마지막 음식일때 체크
        {
            CheckReceip(mergeFood);
        }


        Destroy(food.gameObject);
    }

    private void CheckReceip(Food food)
    {
        if (food.name == receip.name)
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
