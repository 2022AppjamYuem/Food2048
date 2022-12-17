using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    //Temp
    [SerializeField] Food tempFood1;
    [SerializeField] Food tempFood2;

    public static FoodManager instance = null;


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
        Merge(tempFood1, tempFood2);
    }

    // Update is called once per frame

    public void Merge(Food food1, Food food2)
    {
        Transform trans = food1.gameObject.transform;

        if (food1.nextFood != null)
        {
            Instantiate(food1.nextFood, trans.position, Quaternion.identity);
        }
        else
        {

        }

        Destroy(food1.gameObject);
        Destroy(food2.gameObject);
    }
}
