using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace gami
{
    public class OpeningCameraMover : MonoBehaviour
    {

        [SerializeField]
        public GameObject mainCamera;
        [SerializeField]
        GameObject player;
#if WINDOWS_UWP
        public Gamepad controller;
        public GamepadReading reading;
#endif
        private bool autoFlag = false;

        // Update is called once per frame
        void Update()
        {
#if WINDOWS_UWP
            if(controller != null)
            {
                reading = controller.GetCurrentReading();
                if(reading.Buttons.HasFlag(GamepadButtons.X))autoFlag=true;
            }
#else
            if (Input.GetButtonDown("AutoPilot")) autoFlag = true;
#endif
            if (autoFlag)
            {
                Vector3 length = mainCamera.transform.position - this.transform.position;
                this.transform.position +=
                    length * 0.05f;
                this.transform.localEulerAngles +=
                    (mainCamera.transform.eulerAngles - this.transform.eulerAngles) * 0.05f;

                player.GetComponent<gami.PlayerMover>().SetControllerFlag(true);
                player.GetComponent<gami.PlayerMover>().SetGear(1);
                if (Mathf.Sqrt(length.x * length.x +
                    length.y * length.y +
                    length.z * length.z) <= 1)
                {
                    gami.OpeningObjectsManager.DestroyOpeningObjects();
                }
            }
            else
            {
                player.transform.rotation *= Quaternion.AngleAxis(.5f, new Vector3(0, 1, 0));
                player.GetComponent<gami.PlayerMover>().SetControllerFlag(false);
                player.GetComponent<gami.PlayerMover>().SetGear(3);
                SetPos();
            }
        }
        private void SetPos()
        {
            // プレイヤーに向きと位置をあわせる
            this.transform.position = player.transform.position;
            this.transform.eulerAngles =
                player.transform.eulerAngles;
            // 現在の向きから後方にメートル移動
            float angleDir = this.transform.eulerAngles.y * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Sin(angleDir),0,Mathf.Cos(angleDir));
            this.transform.position -= dir;

            // 左に傾けて後ろに下がる
            // 平たく言えば進行方向に対して右移動
            this.transform.rotation *=
                Quaternion.AngleAxis(-90, new Vector3(0, 1, 0));
            // 現在の向きから右方向にメートル移動
            angleDir = this.transform.eulerAngles.y * Mathf.Deg2Rad;
            dir = new Vector3(Mathf.Sin(angleDir),0,Mathf.Cos(angleDir));
            this.transform.position -= dir;
        }
    }
}
