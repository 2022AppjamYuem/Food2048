using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FoodLevel
{
    public GameObject[] foodPrefabs;
}

public class SystemManager : MonoBehaviour
{
    public List<FoodLevel> foodList = new List<FoodLevel>();
    public GameObject Quit;
    public Text Score, BestScore, Plus;

    bool wait, move, stop;
    int x, y, i, j, k, l, score;
    Vector3 firstPos, gap;
    GameObject[,] Square = new GameObject[4, 4];

    float xOffset = 1.356f;
    float yOffset = -3.224f;
    float interval = 0.905f;

    private bool firstCheck;
    private int maxLevel;

    // x 1.62
    // y 1.62

    // 1.356f - 0.451 = 0.905f


    void Start()
    {
        firstCheck = false;
        maxLevel = 0;

        Spawn();

        firstCheck = true;
    }

    void Update()
    {
        if (stop) return;

        // ������
        if (Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            wait = true;
            firstPos = Input.GetMouseButtonDown(0) ? Input.mousePosition : (Vector3)Input.GetTouch(0).position;
        }

        if (Input.GetMouseButton(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            gap = (Input.GetMouseButton(0) ? Input.mousePosition : (Vector3)Input.GetTouch(0).position) - firstPos;
            if (gap.magnitude < 100) return;
            gap.Normalize();

            if (wait)
            {
                wait = false;
                // ��
                if (gap.y > 0 && gap.x > -0.5f && gap.x < 0.5f) for (x = 0; x <= 3; x++) for (y = 0; y <= 2; y++) for (i = 3; i >= y + 1; i--) MoveOrCombine(x, i - 1, x, i);
                // �Ʒ�
                else if (gap.y < 0 && gap.x > -0.5f && gap.x < 0.5f) for (x = 0; x <= 3; x++) for (y = 3; y >= 1; y--) for (i = 0; i <= y - 1; i++) MoveOrCombine(x, i + 1, x, i);
                // ������
                else if (gap.x > 0 && gap.y > -0.5f && gap.y < 0.5f) for (y = 0; y <= 3; y++) for (x = 0; x <= 2; x++) for (i = 3; i >= x + 1; i--) MoveOrCombine(i - 1, y, i, y);
                // ����
                else if (gap.x < 0 && gap.y > -0.5f && gap.y < 0.5f) for (y = 0; y <= 3; y++) for (x = 3; x >= 1; x--) for (i = 0; i <= x - 1; i++) MoveOrCombine(i + 1, y, i, y);
                else return;

                if (move)
                {
                    move = false;
                    Spawn(); 
                    k = 0;
                    l = 0;

                    for (x = 0; x <= 3; x++) for (y = 0; y <= 3; y++)
                    {
                            // ��� Ÿ���� ���� ���� k�� 0�� ��
                            if (Square[x, y] == null) { k++; continue; }
                            if (Square[x, y].tag == "Combine") Square[x, y].tag = "Untagged";
                    }
                    if (k == 0)
                    {
                        //����, ���� ���� ���� ������ l�� 0�� �Ǿ ���ӿ���
                        for (y = 0; y <= 3; y++) for (x = 0; x <= 2; x++) if (Square[x, y].GetComponent<Food>().myLevel == Square[x + 1, y].GetComponent<Food>().myLevel) l++;
                        for (x = 0; x <= 3; x++) for (y = 0; y <= 2; y++) if (Square[x, y].GetComponent<Food>().myLevel == Square[x, y + 1].GetComponent<Food>().myLevel) l++;
                        if (l == 0) { stop = true;  return; }
                    }
                }
            }
        }
    }

    // [x1, y1] �̵� �� ��ǥ, [x2, y2] �̵� �� ��ǥ
    void MoveOrCombine(int x1, int y1, int x2, int y2)
    {
        // �̵� �� ��ǥ�� ����ְ�, �̵� �� ��ǥ�� �����ϸ� �̵�
        if (Square[x2, y2] == null && Square[x1, y1] != null)
        {
            move = true;
            Square[x1, y1].GetComponent<Moving>().Move(x2, y2, false);
            Square[x2, y2] = Square[x1, y1];
            Square[x1, y1] = null;
        }



        // �Ѵ� ���� ���϶� ����
        if (Square[x1, y1] != null && Square[x2, y2] != null &&
            Square[x1, y1].tag != "Combine" && Square[x2, y2].tag != "Combine")
        {
            Food food1 = Square[x1, y1].GetComponent<Food>();
            Food food2 = Square[x2, y2].GetComponent<Food>();

            if (food1.myLevel != food2.myLevel || food1.myLevel == -1 && food2.myLevel == -1)
            {
                return;
            }
            move = true;

            GameObject food = FoodManager.instance.TryMerge(food1, food2, new Vector3(interval * x2 - xOffset, interval * y2 + yOffset, 0));

            if (food.GetComponent<Food>().myLevel > maxLevel)
            {
                maxLevel = food.GetComponent<Food>().myLevel;
            }

            //for (j = 0; j <= 16; j++) if (Square[x2, y2].name == n[j].name + "(Clone)") break;
            Square[x1, y1].GetComponent<Moving>().Move(x2, y2, true);
            Destroy(Square[x2, y2]);
            Square[x1, y1] = null;
            Square[x2, y2] = Instantiate(food, new Vector3(interval * x2 - xOffset, interval * y2 + yOffset, 0), Quaternion.identity);
            Square[x2, y2].tag = "Combine";
            Square[x2, y2].GetComponent<Animator>().SetTrigger("Combine");
        }
    }

    // ����
    void Spawn()
    {
        while (true) { x = UnityEngine.Random.Range(0, 4); y = UnityEngine.Random.Range(0, 4); if (Square[x, y] == null) break; }

        // ó������ �� �ٸ� 0���� ��� 
        // 3 �ܰ� �̻� ������ �� ��� ��� ����
        // �ƴϸ� 0 ~ 1�ܰ� ��� ���� 

        if (firstCheck == false) {
            Square[x, y] = Instantiate(foodList[0].foodPrefabs[0], new Vector3(interval * x - xOffset, interval * y + yOffset, 0),
                Quaternion.identity);
            Square[x, y].GetComponent<Animator>().SetTrigger("Spawn");

            while (true) { x = UnityEngine.Random.Range(0, 4); y = UnityEngine.Random.Range(0, 4); if (Square[x, y] == null) break; }

            Square[x, y] = Instantiate(foodList[0].foodPrefabs[1], new Vector3(interval * x - xOffset, interval * y + yOffset, 0),
                Quaternion.identity);
            Square[x, y].GetComponent<Animator>().SetTrigger("Spawn");

            return;
        }
        else // 4�ܰ谡 ���ٸ� 
        {
            int randomFoodList = UnityEngine.Random.Range(0, maxLevel + 1);
            Square[x, y] = Instantiate(foodList[randomFoodList].foodPrefabs[UnityEngine.Random.Range(0, foodList[randomFoodList].foodPrefabs.Length)], 
                new Vector3(interval * x - xOffset, interval * y + yOffset, 0), Quaternion.identity);
        }


        Square[x, y].GetComponent<Animator>().SetTrigger("Spawn");
    }

}
