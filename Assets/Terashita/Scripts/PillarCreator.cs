using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if WINDOWS_UWP
using Windows.Gaming.Input;
#endif
namespace KTB
{
    public class PillarCreator : MonoBehaviour
    {
#if WINDOWS_UWP
        public Gamepad controller;
        public GamepadReading reading;
#endif

        /// <summary>
        /// 柱の寿命 デフォルト1.0f
        /// </summary>
        [SerializeField]
        float EveryPillarLifeLimit = 1.0f;

        /// <summary>
        /// 柱の上端のy座標 デフォルト5
        /// </summary>
        [SerializeField]
        float PillarTopLimit = 5;

        /// <summary>
        /// 柱の下端のy座標 デフォルト-5
        /// </summary>
        [SerializeField]
        float PillarUnderLimit = -5;

        /// <summary>
        /// 柱の端座標 基本下端 切替で上端
        /// </summary>
        private float PillarLimit;

        [SerializeField]
        GameObject Pillar;

        /// <summary>
        /// 使うの下端か
        /// </summary>
        private bool IsSideUnder;

        private GameObject Player;

        private void Awake()
        {
            Player = GameObject.Find("Player");
        }

        // Use this for initialization
        void Start()
        {
            PillarLimit = PillarUnderLimit;
            IsSideUnder = true;

        }


        // Update is called once per frame
        void Update()
        {
            bool sideChange = false;
#if WINDOWS_UWP
            reading = controller.GetCurrentReading();
            if(reading.Buttons.HasFlag(GamepadButtons.Y))
            {
                sideChange = true;
            }
#else
            if (Input.GetButtonDown("Attack_SideChange")) sideChange = true;
#endif
            if (sideChange)
            {
                if (IsSideUnder)
                {
                    IsSideUnder = false;
                    PillarLimit = PillarTopLimit;
                }
                else
                {
                    IsSideUnder = true;
                    PillarLimit = PillarUnderLimit;
                }
            }

            Vector3 PlayerPos = Player.transform.position;
            GameObject InstPillar = Instantiate(Pillar);

            //  柱の長さセット
            // Player.y - PillarLimit
            // 例 Player.y = 3 , PillarLimit = -5 のとき
            //    3 - ( - 5 ) = 8
            InstPillar.transform.localScale = new Vector3(InstPillar.transform.localScale.x, PlayerPos.y - PillarLimit, InstPillar.transform.localScale.z);

            //  柱の位置セット
            // 長さ÷2 中点にy座標をおく
            InstPillar.transform.position = new Vector3(PlayerPos.x, PlayerPos.y - (PlayerPos.y - PillarLimit) / 2, PlayerPos.z);

            InstPillar.transform.SetParent(this.gameObject.transform);
            InstPillar.GetComponent<AutoDestroy>().SetDestroyLimit(EveryPillarLifeLimit);
        }
    }
}