using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gami
{
    public　class CursolFacePlayer : MonoBehaviour
    {
        // 向かせる先にいるターゲット情報を保持
        [SerializeField]
        public GameObject lookAtTarget;
        // 非アクティブの際に外部からアクセスするための変数
        private static GameObject arrow;

        private const float LENGTH = 2;
        private void Start()
        {
            // 自信を保持
            arrow = this.gameObject;
        }
        private void Update()
        {
            SetCursorRotation();
           // SetCursorPos();
        }
        // カーソルの位置調整
        private void SetCursorPos()
        {
            if(lookAtTarget == null)
            {
                return;
            }
            
        }
        // カーソルの回転値を調整
        private void SetCursorRotation()
        {
            if(lookAtTarget == null)
            {
                return;
            }
            // Playerの位置へ向く
            this.transform.LookAt(lookAtTarget.transform);
        }

        public static void ChangeActiveIsTrue()
        {
            if (arrow == null) return;
            arrow.gameObject.SetActive(true);
        }
        public static void ChangeActiveIsFalse()
        {
            if (arrow == null) return;
            arrow.gameObject.SetActive(false);
        }
    }
}