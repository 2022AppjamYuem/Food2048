using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace _02.Scripts.Lee_Sanghyuk
{
    public class DayCounter : MonoBehaviour
    {
        public SpriteRenderer dayCountBackground;
        public TMP_Text dayCount;

        private void Start()
        {
            dayCount.text = GameManager.Instance.saveData.days + " 일차";
            dayCountBackground.DOFade(0, 1);
            dayCount.DOFade(0, 1);
            dayCount.gameObject.SetActive(false);
            dayCountBackground.gameObject.SetActive(false);
        }
    }
}