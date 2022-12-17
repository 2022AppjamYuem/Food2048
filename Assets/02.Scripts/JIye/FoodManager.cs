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
        TryMerge(tempFood1, tempFood2);
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

        if (food1.myLevel >= 4)
        {
            

            if (food1.myLevel == 4)       //�⺻ ��࿡�� �߰� ���ᰡ ���� ���¶��
            {
                Food mergeFood = food1.isRamen == true ? food1 : food2;
                Food deFood = food1.isRamen == false ? food1 : food2;
                Merge(mergeFood);
                Destroy(deFood);
            }
            else
            {
                Merge(food1);
                Destroy(food2.gameObject);
            }


        }
        else
        {
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

    private void CheckReceip(Food food)
    {
        if (food.foodName == receip.foodName)
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
