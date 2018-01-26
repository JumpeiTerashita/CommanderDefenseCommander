using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if WINDOWS_UWP
using Windows.Gaming.Input;
#endif

namespace gami
{
    public class GazePlayer : MonoBehaviour
    {

#if WINDOWS_UWP
        public static GamepadReading reading;
        public static GamepadReading oldButton;
#endif
        // 表示させるための基準オブジェクト情報を保持
        // 現在はカメラを入れています。
        [SerializeField]
        public GameObject baseObj;
        // 非アクティブの際に外部からアクセスするための変数
        private static GameObject arrow;
        [SerializeField]
        public const float POS_LENGTH = 2;

        [SerializeField]
        GameObject playerMissile;

        [SerializeField]
        GameObject bulletObj;
        private void Start()
        {
            // 自信を保持
            arrow = this.gameObject;
        }
        private void Update()
        {
            SetCursorPos();
            ControllerEvent();
            //SetCursorRotation();
        }
        private void AttackAction()
        {
            bool pushAButton = false;
#if WINDOWS_UWP
              if(reading.Buttons.HasFlag(GamepadButtons.A)&&!oldButton.Buttons.HasFlag(GamepadButtons.A))pushAButton = true;
#else
            if (Input.GetButtonDown("Select_Button_A"))
            {
                pushAButton = true;
            }
#endif
            if (pushAButton)
            {

                //Debug.Log("Fire");
                //var bullet = Instantiate(playerMissile);
                //bullet.transform.position = transform.position;
                //Vector3 targetPos = (CursorInfo.GetCursorPos() - baseObj.transform.position) * 10;
                //Debug.Log(CursorInfo.GetCursorPos().z);
                //    //Vector3.Angle(baseObj.transform.position,CursorInfo.GetCursorPos()) * POS_LENGTH;
                //bullet.GetComponent<KTB.DestinationHolder>().SetDestination(targetPos) ;

                //GameObject obj = Instantiate(bulletObj);
                //obj.transform.position = this.transform.position;
                GameObject obj = bulletObj.GetComponent<gami.BulletController>().CreateBullet(this.transform.position);
                obj.GetComponent<gami.BulletController>().SetSpeed(targetPos.normalized);
            }
        }
        private void ControllerEvent()
        {
            AttackAction();
        }
        // カーソルの位置調整
        private void SetCursorPos()
        {
            if (baseObj == null)
            {
                return;
            }
            this.transform.position = baseObj.transform.position;
            this.transform.eulerAngles = baseObj.transform.eulerAngles;
            float angleDir = this.transform.eulerAngles.y * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(
                Mathf.Sin(angleDir),
                Mathf.Sin(-this.transform.eulerAngles.x * Mathf.Deg2Rad)+0.05f,
                Mathf.Cos(angleDir));
            this.transform.position += dir * POS_LENGTH;
        }


    }
}