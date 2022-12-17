using System;
using TMPro;
using UnityEngine;

namespace _02.Scripts.Lee_Sanghyuk
{
    public class MoneyCount : MonoBehaviour
    {
        public GameObject calculateWindow;//결과창
        public TMP_Text income;//영업중 돈 표시
        public TMP_Text totalMoney;//결과창 돈 표시
        public TMP_Text cost;
        public TMP_Text trashCost;
        public TMP_Text revival;
        public TMP_Text ramenSalesCost;
        public int revivalCount;//부활 횟수

        private int _money;//영업중 총소득
        private int _makeRamenCount;//판매량
        private int _makeTrashCount;//쓰레기양

        public static MoneyCount instance;

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
        
        public void Calculate()
        {
            Time.timeScale = 0;
            calculateWindow.SetActive(true);
            totalMoney.text = _money + "₩";
            cost.text = "-"+(_makeRamenCount * 3) + "₩";
            trashCost.text ="-"+ _makeTrashCount + "₩";
            revival.text = "-" + (revivalCount * -1) + "₩";
            ramenSalesCost.text = "+"+(_makeRamenCount * 7) + "₩";
            GameManager.Instance.CalculateMoney(_money);
        }

        private void Start()
        {
            _money = 30;
            income.text = _money.ToString();
        }

        public void SalesRamen()//라멘판매
        {
            _makeRamenCount++;
            _money += 4;
            income.text = _money.ToString();
        }

        public void MakeTrash()//쓰레기처리
        {
            _makeTrashCount++;
            _money--;
            if (_money<=0)
            {
                revivalCount++;
                _money -= 3;
                if (_money<=0)
                {
                    Calculate();
                }
            }
            income.text = _money.ToString();
        }
    }
}