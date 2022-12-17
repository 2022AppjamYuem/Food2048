using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueData : MonoBehaviour
{
    public TMP_Text orderText;
    private string[] _menuName = new []{"moneyflower", 
        "돈크츠 라멘 주세요 ···아니 역시 매운 라멘 주세요 그리고 와사비 추가해주시고요. ···아니다 그냥 뺴주세요",
        "곱빼기인듯곱빼기아닌곱빼기같지않지만 곱빼기로 주는척해주세요",
        "고추냉이랑 와사비랑 다른거아시나요 그러니까 매운라면주세요",
        "치즈넣어주세요 없으면 와사비넣어주세요 아니다 와사비말고 그냥 주세요 매운라면주지마세요",
        "많이줘요 2배로다가알아서",
        "전 자연의 초록이 이쁘더라구요 그래서 와사비를 넣어주세요",
        "초록 묻고따블로 가~",
        "아따 배고프구마이 매운거 두개주소이"
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Order()
    {
        var menuNum = Random.Range(0, _menuName.Length);
        orderText.text = _menuName[menuNum];
    }
}
