using TMPro;
using UnityEngine;

namespace _02.Scripts.Lee_Sanghyuk
{
    public class FlowManager : MonoBehaviour
    {
        public TMP_Text clock;
    
        private bool _isPlayTime;
    
        private float _playTime;

        void Update()
        {
            if (_isPlayTime)
            {
                _playTime += Time.deltaTime;
                clock.text = ((int)(_playTime*3)).ToString();
                if (_playTime>=180)
                {
                    print("하루끝남");
                    MoneyCount.instance.Calculate();
                }
            }
        }

        public void TimeStart()//영업시작
        {
            _isPlayTime = true;
            _playTime = 0;
        }
    }
}
