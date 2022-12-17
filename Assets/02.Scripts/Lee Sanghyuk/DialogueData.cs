using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace _02.Scripts.Lee_Sanghyuk
{
    public class DialogueData : MonoBehaviour
    {
        public TMP_Text orderText;
        public GameObject NPC;
        public Food receip;

        public enum Food
        {
            돈코츠라멘,
            와사비라멘,
            매운라멘,
            돈코츠라멘곱배기,
            와사비라멘곱배기,
            매운라멘곱배기,
        }
        private readonly string[] _menuName = new []{
            "moneyflower",
            "요세주면라비사와",
            "돈크츠 라멘 주세요 ···아니 역시 매운 라멘 주세요 그리고 와사비 추가해주시고요. ···아니다 그냥 뺴주세요",
            "곱빼기인듯곱빼기아닌곱빼기같지않지만 곱빼기로 주는척해주세요",
            "고추냉이랑 와사비랑 다른거아시나요 그러니까 매운라면주세요",
            "치즈넣어주세요 없으면 와사비넣어주세요 아니다 와사비말고 그냥 주세요 매운라면주지마세요",
            "많이줘요 2배로다가알아서",
            "전 자연의 초록이 이쁘더라구요 그래서 와사비를 넣어주세요",
            "초록 묻고따블로 가~",
            "아따 배고프구마이 매운거 두개주소이"
        };

        public void Order()
        {
            NPC.SetActive(true);
            NPC.transform.DOMove(new Vector3(1,1,1), 1).OnComplete(() =>
            {
                var orderDetails = Random.Range(0, _menuName.Length);
                var selectMenu = Random.Range(0, 5);
                orderText.text = _menuName[orderDetails];
                receip = (Food)(orderDetails);
            });
        }

        private IEnumerator OrderTime()
        {
            yield return new WaitForSeconds(1);
            NPC.SetActive(false);
        }
    }
}
