using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _02.Scripts.Lee_Sanghyuk
{
    public class FlowManager : MonoBehaviour
    {        
        public TMP_Text clock;
    
        private bool _isPlayTime;
    
        private float _playTime;

        private int clockTime = 720;

        private void Start()
        {
            MoneyCount.instance.money = GameManager.Instance.money;
            _playTime = GameManager.Instance.time;
            
            ClockTime();
            StartCoroutine(TimerCor());
        }

        private IEnumerator TimerCor()
        {
            for (int i = 0; i < 36; i++)
            {
                yield return new WaitForSeconds(5f);

                clockTime += 15;
                ClockTime();
                _playTime += 3;
            }

            print("하루끝남");
            MoneyCount.instance.Calculate();
            GameManager.Instance.time = 0;
        }

        public void ClockTime()
        {
            int hour = clockTime / 60;
            int minute = clockTime % 60;

            clock.text = hour + ":" + minute;
        }

        public void GoToMenu()
        {
            SceneManager.LoadScene("StartScene");
        }

        public void Open()
        {
            GameManager.Instance.saveData.days++;
            Time.timeScale = 1;
            SceneManager.LoadScene("GameScene");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void MenuSuccess()
        {
            GameManager.Instance.money = MoneyCount.instance.money;
            GameManager.Instance.time = _playTime;
            MoneyCount.instance.SalesRamen();
            DialogueData.instance.OrderEnd();
            SceneManager.LoadScene("GameScene");
        }
    }
}
