using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gami
{
    public class InCameraChecker : MonoBehaviour
    {

        [SerializeField]
        public Camera mainCamera;

        // メインカメラに付いているタグ名
        private const string MAIN_CAMERA_TAG_NAME = "MainCamera";

        // カメラに表示されているか
        private bool isRendered = false;

        private void Update()
        {
            // もしも画面内にこのオブジェクトがあったとき
            if (isRendered)
            {
                InScreenProcess();
            }
            else
            {
                OutScreenProcess();
            }
            // フラグは常にfalseに戻す
            isRendered = false;
        }
        //カメラに映ってる間に呼ばれる
        private void OnWillRenderObject()
        {
            //メインカメラに映った時だけ_isRenderedを有効に
            if (Camera.current.tag == MAIN_CAMERA_TAG_NAME)
            {
                // 画面内フラグを立てる
                isRendered = true;
            }
        }
        // このオブジェクトが画面内にある時にさせたい処理
        private void InScreenProcess()
        {
            gami.CursolFacePlayer.ChangeActiveIsFalse();
        }
        // このオブジェクトが画面外にある時にさせたい処理
        private void OutScreenProcess()
        {
            gami.CursolFacePlayer.ChangeActiveIsTrue();
        }
    }
}