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
        public float transformYValue = 30;
        [SerializeField]
        Camera mainCamera;
        [SerializeField]
        public float circleRadius = 1;

        private float speed = 0.0001f;
        private const float NARROW_SPEED = 0.95f;
        // プレイヤーを動かす際の角度保持
        private float angle = 0;
        // 演習の長さを保持
        private float circleLength;
        // 右に動くかのフラグとして利用
        private bool rightMove = true;

        // コントローラー受付フラグ
        bool isControll = true;

        private void Start()
        {
            if (mainCamera != null)
            {
                this.transform.position = mainCamera.transform.position;
            }
            this.transform.position += new Vector3(0, 0, circleRadius);
            this.transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y + 90,0);
            circleLength = ( circleRadius * 2) * Mathf.PI;
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
        void TransformYPos()
        {
            float yStick = 0;
#if WINDOWS_UWP
            // ゲームパッドの現在の状態を取得する
            yStick = (float)reading.LeftThumbstickY;
            // 死に値を設定
            if((yStick<=0.1f)&&(yStick>=-0.1f))yStick=0;
#else
            yStick = Input.GetAxis("Player_Pitch") * -1;
#endif
            // Stickの入力によって上下に移動
            this.transform.position +=
                new Vector3(0, yStick * speed, 0);
        }

        private void TurnAction()
        {
            // Aボタンでフラグ処理
            bool pushAButton = false;
#if WINDOWS_UWP
              if(reading.Buttons.HasFlag(GamepadButtons.A)&&!oldButton.Buttons.HasFlag(GamepadButtons.A))pushAButton = true;
#else
            if (Input.GetButtonDown("Select_Button_A"))
            {
                pushAButton= true;
            }
#endif
            // ボタンが押されたら　反転＋フラグ処理
            if (pushAButton)
            {
                this.transform.eulerAngles =
                    new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y + 180, this.transform.eulerAngles.z);
                rightMove = !rightMove;
            }
        }

        // 各アクションをコントローラーイベントに保持
        void ControllerEvent()
        {
            if (isControll == false) return;
            TransformYPos();
            TurnAction();
        }
        void Update()
        {
#if WINDOWS_UWP
       
            oldButton = reading;
            reading = ControlEventGetter.Instance.reading;
        
#endif
            //Debug.Log("circleRadius : " + Mathf.Sqrt((this.transform.position.x-mainCamera.transform.position.x)* (this.transform.position.x - mainCamera.transform.position.x)+ (this.transform.position.z - mainCamera.transform.position.z)* (this.transform.position.z - mainCamera.transform.position.z)));
            ControllerEvent();
            AccelAction();
            // 移動
            TransformPos();
            //mainCamera.transform.LookAt(this.transform);
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
            // 角度は現在のスピードの移動距離を円の１°あたりの移動距離で割ったものを使用
            angle = speed / (circleLength / 360);
            if (rightMove)
            {
                this.transform.rotation *= Quaternion.AngleAxis(angle, new Vector3(0, 1, 0));
        }
            else
            {
                this.transform.rotation *= Quaternion.AngleAxis(-angle, new Vector3(0, 1, 0));
            }
}
        private void TransformPos()
        {
            // 角度調整
            SetAngle();
            // 向いている方向＊スピード値にポジションを移動

            this.transform.position +=
                new Vector3(
                    Mathf.Sin(this.transform.eulerAngles.y * Mathf.Deg2Rad),
                    0,
                    Mathf.Cos(this.transform.eulerAngles.y * Mathf.Deg2Rad)) * speed;
         
        }
    }

}