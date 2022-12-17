using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SystemManager : MonoBehaviour
{
    public GameObject[] n;
    public GameObject Quit;
    public Text Score, BestScore, Plus;

    bool wait, move, stop;
    int x, y, i, j, k, l, score;
    Vector3 firstPos, gap;
    GameObject[,] Square = new GameObject[4, 4];

    float xOffset = 1.62f;
    float yOffset = 1.62f;
    float interval = 1.078f;

    // x 1.62
    // y 1.62

    // 1.62 - 0.542 = 1.078


    void Start()
    {
        Spawn(0);
        Spawn(1);
        //BestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
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
                    //Spawn(); // �ӽ� �ּ�
                    k = 0;
                    l = 0;

                    // ����
                    //if (score > 0)
                    //{
                    //    Plus.text = "+" + score.ToString() + "    ";
                    //    Plus.GetComponent<Animator>().SetTrigger("PlusBack");
                    //    Plus.GetComponent<Animator>().SetTrigger("Plus");
                    //    Score.text = (int.Parse(Score.text) + score).ToString();
                    //    if (PlayerPrefs.GetInt("BestScore", 0) < int.Parse(Score.text)) PlayerPrefs.SetInt("BestScore", int.Parse(Score.text));
                    //    BestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
                    //    score = 0;
                    //}

                    for (x = 0; x <= 3; x++) for (y = 0; y <= 3; y++)
                        {
                            // ��� Ÿ���� ���� ���� k�� 0�� ��
                            if (Square[x, y] == null) { k++; continue; }
                            if (Square[x, y].tag == "Combine") Square[x, y].tag = "Untagged";
                        }
                    if (k == 0)
                    {
                        //����, ���� ���� ���� ������ l�� 0�� �Ǿ ���ӿ���
                        for (y = 0; y <= 3; y++) for (x = 0; x <= 2; x++) if (Square[x, y].name == Square[x + 1, y].name) l++;
                        for (x = 0; x <= 3; x++) for (y = 0; y <= 2; y++) if (Square[x, y].name == Square[x, y + 1].name) l++;
                        if (l == 0) { stop = true; Quit.SetActive(true); return; }
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
        if (Square[x1, y1] != null && Square[x2, y2] != null && Square[x1, y1].tag != "Combine" && Square[x2, y2].tag != "Combine")
        {
            move = true;
            Food food1 = Square[x1, y1].GetComponent<Food>();
            Food food2 = Square[x2, y2].GetComponent<Food>();
            FoodManager.instance.TryMerge(food1, food2, new Vector3(interval * x2 - xOffset, interval * y2 - yOffset, 0));
            //for (j = 0; j <= 16; j++) if (Square[x2, y2].name == n[j].name + "(Clone)") break;
            //Square[x1, y1].GetComponent<Moving>().Move(x2, y2, true);
            //Destroy(Square[x2, y2]);
            //Square[x1, y1] = null;
            //Square[x2, y2] = Instantiate(n[j + 1], new Vector3(interval * x2 - xOffset, interval * y2 - yOffset, 0), Quaternion.identity);
            Square[x2, y2].tag = "Combine";
            Square[x2, y2].GetComponent<Animator>().SetTrigger("Combine");
        }
    }

    // ����
    void Spawn(int index)
    {
        while (true) { x = Random.Range(0, 4); y = Random.Range(0, 4); if (Square[x, y] == null) break; }
        Square[x, y] = Instantiate(n[index], new Vector3(interval * x - xOffset, interval * y - yOffset, 0), Quaternion.identity);
        
        //Square[x, y] = Instantiate(Random.Range(0, int.Parse(Score.text) > 800 ? 4 : 8) > 0 ? n[0] : n[1], new Vector3(interval * x - xOffset, interval * y - yOffset, 0), Quaternion.identity);
        Square[x, y].GetComponent<Animator>().SetTrigger("Spawn");
    }
}
