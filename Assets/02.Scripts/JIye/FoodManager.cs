using _02.Scripts.Lee_Sanghyuk;
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

    public FoodEnum receip;
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

    // Start is called before the first frame update
    void Start()
    {
        //��� Ʈ����?
        //TryMerge(tempFood1, tempFood2);
    }

    /// <summary>
    /// �������� ������ ����
    /// </summary>
    /// <param name="food"></param>
    public void SetReceip(FoodEnum food)
    {
        Debug.Log(food);
        receip = food;
    }

    /// <summary>
    /// merge �������� üũ
    /// </summary>
    /// <param name="food1"></param>
    /// <param name="food2"></param>
    public void TryMerge(Food food1, Food food2)
    {

        if(food1.foodName == food2.foodName)        //������ ���
        {
            //������
            Instantiate(trashPrefab, food1.gameObject.transform.position, Quaternion.identity);

            Destroy(food1.gameObject);
            Destroy(food2.gameObject);

            return;
        }

        if (food1.myLevel == food2.myLevel)
        {
            

            if (food1.myLevel == 4)       //�⺻ ��࿡�� �߰� ���ᰡ ���� ���¶��
            {
                Food mergeFood = food1.isRamen == true ? food1 : food2;
                Food deFood = food1.isRamen == false ? food1 : food2;
                Merge(mergeFood,deFood);
            }
            else
            {
                Merge(food1);
                Destroy(food2.gameObject);
            }


        }

    }

    private void Merge(Food food)
    {
        Food mergeFood = null;
        Transform trans = food.gameObject.transform;

        mergeFood = Instantiate(food.nextFood[0], trans.position, Quaternion.identity).GetComponent<Food>();


        if (mergeFood.isRamen)      //������ �����϶� üũ
        {
            CheckReceip(mergeFood);
        }


        Destroy(food.gameObject);
    }

    private void Merge(Food food1, Food food2)
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
    }

    private void CheckReceip(Food food)
    {
        if (food.myFood == receip && food.myLevel > 4)
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
