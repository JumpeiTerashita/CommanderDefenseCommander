using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTB
{
    /// <summary>
    /// 数学的なやつらをまとめたライブラリ
    /// </summary>
    public class MyMath
    {
        /// <summary>
        /// 第一引数 ＜＝　第二引数　ならtrue
        /// </summary>
        public static bool IsShortLength(float _shorter, float _target)
        {
            if (_shorter * _shorter <= _target * _target) return true;
            else return false;
        }
        /// <summary>
        /// Vector3の長さ ＜＝　float ならtrue
        /// </summary>
        public static bool IsShortLength(Vector3 _shorterVec, float _target)
        {
            if (_shorterVec.sqrMagnitude <= _target * _target) return true;
            else return false;
        }

    }
}