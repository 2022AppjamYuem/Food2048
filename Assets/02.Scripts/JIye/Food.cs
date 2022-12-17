using _02.Scripts.Lee_Sanghyuk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Food : MonoBehaviour
{
    public int myLevel;                   //레벨
    public string foodName;       //음식이름
    public GameObject[] nextFood;   //다음 음식


    public FoodEnum myFood;     //최종 라면 상태

    public bool isRamen;        //라면상태 인가?
    public bool isTrash;


}
