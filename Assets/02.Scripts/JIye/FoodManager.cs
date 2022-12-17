using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{

    [SerializeField] GameObject[] foodPrefabs;

    public static FoodManager instance = null;

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
        
    }

    // Update is called once per frame

    public void Merge(Food food)
    {
        Transform trans = food.transform;
        Instantiate(foodPrefabs[food.myLevel + 1], trans.position, Quaternion.identity);
    }
}
