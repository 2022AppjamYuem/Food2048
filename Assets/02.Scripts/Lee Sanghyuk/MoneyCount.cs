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

        public void Calculate()
        {
            calculateWindow.SetActive(true);
            totalMoney.text = _money + "₩";
            cost.text = "-"+(_makeRamenCount * 3) + "₩";
            trashCost.text ="-"+ _makeTrashCount + "₩";
            revival.text = "-" + (revivalCount * -1) + "₩";
            ramenSalesCost.text = "+"+(_makeRamenCount * 7) + "₩";
            GameManager.Instance.CalculateMoney(_money);
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
            income.text = _money.ToString();
        }
    }
}