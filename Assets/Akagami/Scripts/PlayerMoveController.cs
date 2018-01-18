using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if WINDOWS_UWP
using Windows.Gaming.Input;
#endif

namespace gami
{
    // コピペ用消さないで
    //#if WINDOWS_UWP
    //#else
    //#endif
    public class PlayerMoveController : MonoBehaviour
    {
#if WINDOWS_UWP
        public static GamepadReading reading;
        public static GamepadReading oldButton;
#endif
        [SerializeField]
        public float maxSpeed = 0.01f;
        [SerializeField]
        public float accel = 0.001f;
        [SerializeField]
        public float maxRotDeg = 60;
        [SerializeField]
        Camera mainCamera;
        [SerializeField]
        public float circleRadius = 1;

        private float speed = 0.0001f;
        private const float NARROW_SPEED = 0.95f;
        private float angle = 0;
        float yStick;
       // private float 

        // コントローラー受付フラグ
        bool isControll = true;
        private void Start()
        {
            if (mainCamera != null)
            {
                this.transform.position = mainCamera.transform.position;
            }
            //this.transform.position += new Vector3(0, 0, circleRadius);
            //this.transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y+90,0);
            SetAngle();
        }
        void AccelAction()
        {
            speed *= NARROW_SPEED;
            if (speed <= maxSpeed)
            {
                speed += accel;
            }
        }
        void RotateAction()
        {
            yStick = 0;
            float xStick = 0;
#if WINDOWS_UWP
       
            // ゲームパッドの現在の状態を取得する
            xStick = (float)reading.LeftThumbstickX;
            yStick = (float)reading.LeftThumbstickY;
            // 死に値を設定
            if((xStick<=0.1f)&&(xStick>=-0.1f))xStick=0;
            if((yStick<=0.1f)&&(yStick>=-0.1f))yStick=0;
#else
            // Stick、Triggerに入力があれば値を保持
            yStick = Input.GetAxis("Player_Pitch") * -1;
            xStick = Input.GetAxis("Player_Roll");
#endif
            // YawPitchRollの入力によって
            // 現在の姿勢から値を変更していく

            // 後々調節
            this.transform.position +=
                new Vector3(0, yStick * speed, 0);
            //this.transform.rotation *= 
            //    Quaternion.AngleAxis(yStick * maxRotDeg, new Vector3(1, 0, 0));
            //this.transform.eulerAngles = new Vector3(
            //    yStick * maxRotDeg,
            //    this.transform.eulerAngles.y,
            //    this.transform.eulerAngles.z);
                  
            if (xStick != 0)
            {

            }
        
        }
        
  
        // 各アクションをコントローラーイベントに保持
        void ControllerEvent()
        {
            if (isControll == false) return;
            RotateAction();
        }
        void Update()
        {
#if WINDOWS_UWP
       
            oldButton = reading;
            reading = ControlEventGetter.Instance.reading;
        
#endif

            Debug.Log("circleRadius : " + Mathf.Sqrt((this.transform.position.x-mainCamera.transform.position.x)* (this.transform.position.x - mainCamera.transform.position.x)+ (this.transform.position.z - mainCamera.transform.position.z)* (this.transform.position.z - mainCamera.transform.position.z)));
            ControllerEvent();
            AccelAction();
            this.transform.rotation *= Quaternion.AngleAxis(angle/speed, new Vector3(0, 1, 0));
            //Debug.Log(angle / speed);
            TransformPos();
            mainCamera.transform.LookAt(this.transform);
        }

        // AutoPilot時の速度取得 by KTB
        public float GetMaxSpeed()
        {
            return maxSpeed;
        }

        // AutoPilot終了時の速度変更 by KTB
        public void SetSpeed(float _speed)
        {
            speed = _speed;
        }
   
        // 外部からコントローラー操作を変えられるように
        public void SetControllerFlag(bool _flag)
        {
            isControll = _flag;
        }

        private void SetAngle()
        {
            Vector2 pos1 = new Vector2((Mathf.Sin(0) * circleRadius), (Mathf.Cos(0) * circleRadius)) * speed;
            Vector2 pos2 = new Vector2((Mathf.Sin(1 * Mathf.Deg2Rad) * circleRadius ), (Mathf.Cos(1 * Mathf.Deg2Rad) * circleRadius)) * speed;
            angle = Vector2.Angle(pos1, pos2);
        }
        private void TransformPos()
        {
            SetAngle();
            this.transform.localEulerAngles =
                new Vector3(0, this.transform.eulerAngles.y, 0);
            this.transform.position +=
                new Vector3(
                    Mathf.Sin(this.transform.eulerAngles.y * Mathf.Deg2Rad),
                    0,
                    Mathf.Cos(this.transform.eulerAngles.y * Mathf.Deg2Rad))*speed;
            //this.transform.rotation *= Quaternion.AngleAxis(yStick * maxRotDeg,
            //    new Vector3(1,0,0));
        }
    }

}