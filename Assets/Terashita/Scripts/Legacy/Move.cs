using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTB
{
    /// <summary>
    /// キー入力を受け取って移動する　自機用
    /// </summary>
    public class Move : MonoBehaviour
    {
        [SerializeField]
        public float maxSpeed = 0.01f;
        [SerializeField]
        public float accel = 0.001f;
        private float speed = 0;
        void RotateAction()
        {
            float yStick = Input.GetAxis("Player_Pitch");
            float xStick = Input.GetAxis("Player_Roll");
            if (yStick != 0)
            {
                this.transform.rotation *=
                    Quaternion.AngleAxis(-yStick, new Vector3(1, 0, 0));
            }
            if (xStick != 0)
            {
                this.transform.rotation *=
                    Quaternion.AngleAxis(-xStick, new Vector3(0, 0, 1));
            }
        }
        void AccelAction()
        {
            if (Input.GetButton("Player_Accel"))
            {
                if (speed >= maxSpeed) return;
                speed += accel;
                Debug.Log(speed);
            }
        }
        void BrakeAction()
        {
            if (Input.GetButton("Player_Brake"))
            {
                if (speed <= 0)
                {
                    speed = 0;
                    return;
                }
                speed *= 0.95f;
            }
        }
        void ControllerEvent()
        {
            AccelAction();
            BrakeAction();
            RotateAction();
        }
        void Update()
        {
            ControllerEvent();
            float x = Mathf.Cos(Mathf.Deg2Rad * (this.transform.localEulerAngles.y + 270) * -1) * speed;
            float y = Mathf.Sin(Mathf.Deg2Rad * this.transform.localEulerAngles.x * -1) * speed;
            float z = Mathf.Sin(Mathf.Deg2Rad * (this.transform.localEulerAngles.y + 270) * -1) * speed;

            this.transform.position += new Vector3(x, y, z);
        }
    }
}