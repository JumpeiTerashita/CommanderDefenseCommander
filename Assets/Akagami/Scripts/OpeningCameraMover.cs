using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if WINDOWS_UWP
using Windows.Gaming.Input;
#endif
namespace gami
{
    public class OpeningCameraMover : MonoBehaviour
    {
        [SerializeField]
        public GameObject mainCamera;
        [SerializeField]
        GameObject player;
        //[SerializeField]
        //float LENGTH = 2;
        //[SerializeField]
        //float PLAYER_ROTATE_ANGLE = .5f;
        [SerializeField]
        GameObject controller;
        /// <summary>
        /// 生成する群れの数 by KTB
        /// </summary>
        //[SerializeField]
        //int BoidsNumber = 3;
#if WINDOWS_UWP
        public GamepadReading reading;
#endif
        private bool autoFlag = false;
        private void Start()
        {
            mainCamera.transform.parent = player.transform;
            DontDestroyOnLoad(controller);
        }
        // Update is called once per frame
        void Update()
        {
#if WINDOWS_UWP
            reading = 
            ControlEventGetter.Instance.reading;
            
            if(reading.Buttons.HasFlag(GamepadButtons.X)){autoFlag = true;}
#else
            if (Input.GetButtonDown("AutoPilot")) autoFlag = true;
#endif
            // ボタンが押されたら
            if (autoFlag == true)
            {
               mainCamera.transform.parent = null;
          
                // オブジェクト削除
                gami.OpeningSceneManager.DestroyOpeningObjects();
                    // タイマー作動
                //gami.OpeningSceneManager.CreateGameSceneObj();
                //}
                
                // コントローラーの受付開始
                player.GetComponent<gami.PlayerMoveController>().SetControllerFlag(true);
                SceneManager.LoadScene("Tutorial");
            }
            else
            {
                //mainCamera.SetActive(false);
                //player.transform.rotation *= Quaternion.AngleAxis(PLAYER_ROTATE_ANGLE, new Vector3(0, 1, 0));
                player.GetComponent<gami.PlayerMoveController>().SetControllerFlag(false);

            }
        }
        //private void SetPos()
        //{
        //    // プレイヤーに向きと位置をあわせる
        //    mainCamera.transform.position = player.transform.position;
        //    mainCamera.transform.eulerAngles =
        //        player.transform.eulerAngles;
        //    // 現在の向きから後方にメートル移動
        //    float angleDir = mainCamera.transform.eulerAngles.y * Mathf.Deg2Rad;
        //    Vector3 dir = new Vector3(Mathf.Sin(angleDir),0,Mathf.Cos(angleDir));
        //    mainCamera.transform.position -= dir * LENGTH;

        //    // 左に傾けて後ろに下がる
        //    // 平たく言えば進行方向に対して右移動
        //    mainCamera.transform.rotation *=
        //        Quaternion.AngleAxis(-90, new Vector3(0, 1, 0));
        //    // 現在の向きから右方向にメートル移動
        //    angleDir = mainCamera.transform.eulerAngles.y * Mathf.Deg2Rad;
        //    dir = new Vector3(Mathf.Sin(angleDir),0,Mathf.Cos(angleDir));
        //    mainCamera.transform.position -= dir * LENGTH;
        //}
    }
}
