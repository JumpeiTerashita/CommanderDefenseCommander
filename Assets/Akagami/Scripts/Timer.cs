using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace gami
{
    public class Timer : MonoBehaviour
    {

        [SerializeField]
        public float limitTime = 60;
        public float timeCount = 0;

        private void Start()
        {
            // 生成時にカウンターを0に
            timeCount = 0;
        }

        private void Update()
        {
            // マイフレーム秒数を加算
            timeCount += Time.deltaTime;
        }

        // 制限時間内であればtrueをかえす
        public bool InLimitTime()
        {
            if (timeCount >= limitTime)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // 現在の残り時間を返す
        public float GetTime()
        {
            return limitTime - timeCount;
        }
        // カウンターを引数の値に変更
        public void SetTime(float _time)
        {
            timeCount = _time;
        }
        // 制限時間を引数の値に変更 
        public void SetLimit(float _limit)
        {
            limitTime = _limit;
        }
    }
}