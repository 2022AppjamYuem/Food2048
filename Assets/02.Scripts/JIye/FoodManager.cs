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
        if (instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������
        {
            instance = this; //���ڽ��� instance�� �־��ݴϴ�.
        }
        else
        {
            if (instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ����
        }
    }

    /// <summary>
    /// �������� ������ ����
    /// </summary>
    /// <param name="food"></param>
    private void SetReceip(Food food)
    {
        receip = food;
    }

    /// <summary>
    /// merge �������� üũ
    /// </summary>
    /// <param name="food1"></param>
    /// <param name="food2"></param>
    public GameObject TryMerge(Food food1, Food food2, Vector2 Pos)
    {
        if(food1.foodName == food2.foodName)        //������ ���
        {
            Destroy(food1.gameObject);
            Destroy(food2.gameObject);

            //������
            return Instantiate(trashPrefab, Pos, Quaternion.identity);
        }

        if (food1.myLevel == food2.myLevel)
        {
            if (food1.myLevel == 4)       //�⺻ ��࿡�� �߰� ���ᰡ ���� ���¶��
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


        if (mergeFood.isRamen)      //������ �����϶� üũ
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
            case "����⸧":
                mergeFood = Instantiate(food1.nextFood[0], trans.position, Quaternion.identity).GetComponent<Food>();

                break;

            case "�ͻ��":
                mergeFood = Instantiate(food1.nextFood[1], trans.position, Quaternion.identity).GetComponent<Food>();

                break;
            default:
                break;
        }

        if (mergeFood.isRamen)      //������ �����϶� üũ
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
            //����
        }
        else
        {
            //���� => ������
            Instantiate(trashPrefab, food.gameObject.transform.position, Quaternion.identity);
        }
    }

}
