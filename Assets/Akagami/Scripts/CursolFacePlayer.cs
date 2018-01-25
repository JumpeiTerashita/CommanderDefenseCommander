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
        // 表示させるための基準オブジェクト情報を保持
        // 現在はカメラを入れています。
        [SerializeField]
        public GameObject baseObj;
        // 非アクティブの際に外部からアクセスするための変数
        private static GameObject arrow;
        [SerializeField]
        public const float CURSOR_LENGTH = 2;
        private void Start()
        {
            // 自信を保持
            arrow = this.gameObject;
        }
        private void Update()
        {
            SetCursorPos();
            SetCursorRotation();
        }
        // カーソルの位置調整
        private void SetCursorPos()
        {
            if(lookAtTarget == null||
                baseObj == null)
            {
                return;
            }
            this.transform.position = baseObj.transform.position;
            this.transform.eulerAngles = baseObj.transform.eulerAngles;
            float angleDir = this.transform.eulerAngles.y * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(
                Mathf.Sin(angleDir), 
                Mathf.Sin(this.transform.eulerAngles.x * Mathf.Deg2Rad), 
                Mathf.Cos(angleDir));
            this.transform.position += dir * CURSOR_LENGTH;
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