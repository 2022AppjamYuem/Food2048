using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace _02.Scripts.Lee_Sanghyuk
{
    public enum FoodEnum
    {
        돈코츠라멘,
        와사비라멘,
        매운라멘,
        ETC,
    }
    public class DialogueData : MonoBehaviour
    {
        public TMP_Text orderText;
        public GameObject NPC;
        public GameObject tspeechbubble;
        public FoodEnum receip;

        public static DialogueData instance;

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


        private readonly string[] _menuName = new []{
            "요즘 살이 너무쪄서 다시 뺄려구요 그래서 와사비라멘 주세요 못먹게",
            "빨간색소스가 캡사이신아니였네요 그럼 와사비 라멘주세요",
            "요즘 초록초록 한세상이 좋더라구요 그래서 전 파괴하기로했어요 빨갛게",
            "simple is best",
        };

        private void Start()
        {
            //주문 받을 때 FlowManaer에다가 생성하기
            Order();
        }

        /// <summary>
        /// 주문 할때 호출
        /// </summary>
        public void Order()
        {
            NPC.SetActive(true);
            NPC.transform.DOMove(new Vector3(-1.36f,1,0), 1).OnComplete(() =>
            {
                int orderDetails = 0;
                var selectMenu = Random.Range(0, 3);
                switch (selectMenu)
                {
                    case 0:     //돈코츠라멘
                        orderDetails = 3;
                        break;
                    case 1:     //와사비라멘
                        orderDetails = Random.Range(0,2);
                        break;
                    case 2:     //매운라멘
                        orderDetails = 2;
                        break;
                }
                tspeechbubble.gameObject.SetActive(true);
                orderText.text = _menuName[orderDetails];
                receip = (FoodEnum)(selectMenu);
                FoodManager.instance.SetReceip(receip);

            });

        }

        public void OrderEnd()
        {
            NPC.transform.DOMove(new Vector3(-3.8f,1,1), 1).OnComplete(() =>
            {
                tspeechbubble.SetActive(false);
                NPC.SetActive(false);
                Order();
            });
        }
    }
}
