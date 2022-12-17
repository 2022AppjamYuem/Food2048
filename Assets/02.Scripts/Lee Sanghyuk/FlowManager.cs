using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _02.Scripts.Lee_Sanghyuk
{
    public class FlowManager : MonoBehaviour
    {
        public static FlowManager Instance;

        
        public TMP_Text clock;
    
        private bool _isPlayTime;
    
        private float _playTime;
        
        public static FlowManager Instance1
        {
            get
            {
                if (Instance != null) return Instance;
                var obj = FindObjectOfType<FlowManager>();
                if (obj != null)
                {
                    Instance = obj;
                }
                else
                {
                    var newObj = new GameObject().AddComponent<FlowManager>();
                    Instance = newObj;
                }
                return Instance;
            }
        }
        
        private void Awake()
        {
            var objs = FindObjectsOfType<GameManager>();
            if (objs.Length != 1)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        private void Start()
        {
            MoneyCount.instance.money = GameManager.Instance.money;
            _playTime = GameManager.Instance.time;
        }

        void Update()
        {
            _playTime += Time.deltaTime;
            clock.text = ((int)(_playTime*3)).ToString();
            if (_playTime>=180)
            {
                print("하루끝남");
                MoneyCount.instance.Calculate();
                GameManager.Instance.time = 0;
            }
            
        }

      

        public void GoToMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public void Open()
        {
            GameManager.Instance.saveData.days++;
            Time.timeScale = 1;
            SceneManager.LoadScene("PlayScene");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void MenuSuccess()
        {
            GameManager.Instance.money = MoneyCount.instance.money;
            GameManager.Instance.time = _playTime;
            MoneyCount.instance.SalesRamen();
            DialogueData.instance.OrderEnd();
            SceneManager.LoadScene("playScene");
        }
    }
}
