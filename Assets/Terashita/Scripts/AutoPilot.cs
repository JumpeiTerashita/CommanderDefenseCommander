using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if WINDOWS_UWP
using Windows.Gaming.Input;
#endif
namespace KTB
{
    public class AutoPilot : MonoBehaviour
    {
#if WINDOWS_UWP
        public Gamepad controller;
        public GamepadReading reading;
#endif
        
        [SerializeField]
        float ArriveLength = 0.25f;

        [SerializeField]
        float SpeedMagnitude = 4.0f;


        float Distance;

        Vector3 CursorPos;

        // Use this for initialization
        void Start()
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
        //Gamepad.GamepadAdded += Gamepad_GamepadAdded;
#endif
        }

        // Update is called once per frame
        void Update()
        {
            bool autoPilot = false;
#if WINDOWS_UWP
            if(controller != null)
            {
                reading = controller.GetCurrentReading();
                if(reading.Buttons.HasFlag(GamepadButtons.X))autoPilot = true;
            }
#else
            if (Input.GetButtonDown("AutoPilot")) autoPilot = true;
#endif
            if (autoPilot&&!gami.PlayerMover.IsAutoPilot)
            {
                Debug.Log("AutoPilot enabled");
                gami.PlayerMover.IsAutoPilot = true;

                CursorPos = CursorInfo.GetCursorPos();
                GetComponent<DestinationHolder>().SetDestination(CursorPos);
                transform.LookAt(CursorPos);
              
            }

            if (gami.PlayerMover.IsAutoPilot)
            {
                FlyToDestination(SpeedMagnitude);
                float DistanceTwice = (CursorPos - transform.position).sqrMagnitude;

                if (DistanceTwice <= (ArriveLength*ArriveLength))
                {
                    Debug.Log("AutoPilot disabled");
                    gami.PlayerMover.IsAutoPilot = false;
                    GetComponent<gami.PlayerMover>().SetSpeed(0.005f);

                    
                    
                    transform.LookAt(new Vector3(0,0,10));
                }
            }

        }

        void FlyToDestination(float _speedMagnitude = 1.0f, float _turnMagnitude = 1.0f)
        {
            // 設定した目的地にだんだん向く
            Vector3 TargetPos = GetComponent<DestinationHolder>().GetDestination();
            Quaternion targetRotation = Quaternion.LookRotation(TargetPos - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _turnMagnitude);

            float Speed = GetComponent<gami.PlayerMover>().GetMaxSpeed();

            // 向いている方向に飛ぶ
            transform.Translate(new Vector3(0, 0, Speed * _speedMagnitude));
            return;
        }
    }
}