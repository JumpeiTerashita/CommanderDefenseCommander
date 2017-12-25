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
    public class PlayerMover : MonoBehaviour
    {
#if WINDOWS_UWP
        public Gamepad controller;
        public GamepadReading reading;
        public GamepadReading oldButton;
#endif
        [SerializeField]
        public float maxSpeed = 0.01f;

        // ギア定速 by KTB
        // REVIEW maxSpeedから計算するべき…？
        [SerializeField]
        public float[] GearSpeed = new float[5] 
        {
            0,
            0.005f,
            0.01f,
            0.05f,
            0.1f
        };
        
        [SerializeField]
        public float accel = 0.001f;
        private float speed = 0;
        private const float NARROW_SPEED = 0.95f;
        // 自動操縦フラグ by KTB
        // オンなら操縦できないように
        static public bool IsAutoPilot;
        // 現在のギア
        private int NowGear;
        // コントローラー受付フラグ
        public bool isControll = false;

        private void Start()
        {
#if WINDOWS_UWP
        // Gamepadを探す
        if(Gamepad.Gamepads.Count > 0) {
            //Debug.Log("Gamepad found.");
            //controller = Gamepad.Gamepads.First();
        } else
        {
            Debug.Log("Gamepad not found.");
        }
        // ゲームパッド追加時イベント処理を追加
        Gamepad.GamepadAdded += Gamepad_GamepadAdded;
#endif

            NowGear = 1;
            IsAutoPilot = false;
        }

        void RotateAction()
        {
            float yStick = 0;
            float xStick = 0;
            float trigger = 0;
#if WINDOWS_UWP
        if(controller != null)
        {

            // ゲームパッドの現在の状態を取得する
            xStick = (float)reading.LeftThumbstickX;
            yStick = (float)reading.LeftThumbstickY * -1;
            // 死に値を設定
            if((xStick<=0.1f)&&(xStick>=-0.1f))xStick=0;
            if((yStick<=0.1f)&&(yStick>=-0.1f))yStick=0;
            trigger += (float)reading.LeftTrigger;
            trigger -= (float)reading.RightTrigger;
        }
#else
            // Stick、Triggerに入力があれば値を保持
            yStick = Input.GetAxis("Player_Pitch");
            xStick = Input.GetAxis("Player_Roll");
            trigger = Input.GetAxis("Player_Yaw");
#endif

            // YawPitchRollの入力によって
            // 現在の姿勢から値を変更していく
            if (yStick != 0)
            {
                // 後々調節
                // yStick *= 2 - (speed / maxSpeed);
                this.transform.rotation *=
                    Quaternion.AngleAxis(-yStick, new Vector3(1, 0, 0));
            }
            if (xStick != 0)
            {
                this.transform.rotation *=
                    Quaternion.AngleAxis(-xStick, new Vector3(0, 0, 1));
            }
            if (trigger != 0)
            {
                this.transform.rotation *=
                    Quaternion.AngleAxis(-trigger, new Vector3(0, 1, 0));
            }
        }
        void GearChangeAction()
        {
            // L1とR1でギアチェンジ
            bool gearUp = false;
            bool gearDown = false;

#if WINDOWS_UWP
            // ボタンが押されているかつ前回の入力と異なればFlagをtrueに
            if(controller != null)
            {
                if(reading.Buttons.HasFlag(GamepadButtons.RightShoulder)&&
                (reading.Buttons.HasFlag(GamepadButtons.RightShoulder)!=oldButton.Buttons.HasFlag(GamepadButtons.RightShoulder)))
                {
                    gearUp = true;
                }
                if(reading.Buttons.HasFlag(GamepadButtons.LeftShoulder)&&
                reading.Buttons.HasFlag(GamepadButtons.LeftShoulder)!=oldButton.Buttons.HasFlag(GamepadButtons.LeftShoulder))
                {
                    gearDown = true;
                }
            }
#else
            if (Input.GetButtonDown("GearUp")) gearUp = true;
            if (Input.GetButtonDown("GearDown")) gearDown = true;
#endif
            if (gearUp && (NowGear < 4)) NowGear++;
            if (gearDown && (NowGear > 0)) NowGear--;
            
        }
        void GearAutoAccelAction()
        {
            if (speed > GearSpeed[NowGear]) { speed *= NARROW_SPEED; return; }
            speed += accel;
        }

        void BrakeAction()
        {
            bool brake = false;
            // Aボタン！！！！！！
#if WINDOWS_UWP
            if(controller != null)
            {
                if(reading.Buttons.HasFlag(GamepadButtons.A))brake = true;
            }
#else
            if (Input.GetButton("Brake"))
            {
                brake = true;
            }
#endif
            if (brake)
            {
                if (speed <= 0)
                {
                    speed = 0;
                    return;
                }
                speed *= NARROW_SPEED;
            }
        }
        // 各アクションをコントローラーイベントに保持
        void ControllerEvent()
        {
            if (isControll == false) return;
            GearChangeAction();
            BrakeAction();
            RotateAction();
        }
        void Update()
        {
#if WINDOWS_UWP
            if(controller != null)
            {
                oldButton = reading;
                reading = controller.GetCurrentReading();
            }
#endif
            if (!IsAutoPilot)
            {
                ControllerEvent();
                GearAutoAccelAction();
                // 今現在向いている方向に進む
                this.transform.Translate(new Vector3(0, 0, speed));
            }
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
        // 外部からギアを変えられるようにします。
        public void SetGear(int _gear)
        {
            NowGear = _gear;
        }
        // 外部からコントローラー操作を変えられるように
        public void SetControllerFlag(bool _flag)
        {
            isControll = _flag;
        }
#if WINDOWS_UWP
        // ゲームパッド追加時のイベント処理
        private void Gamepad_GamepadAdded(object sender, Gamepad e)
        {
            controller = e;
        }
#endif
    }

}