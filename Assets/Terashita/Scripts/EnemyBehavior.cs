using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTB
{
    public enum AI_PATTERN
    {
        STOP,
        CHASE,
        HITAWAY
    }

    enum HITAWAY_STATE
    {
        AWAY,
        TURN,
        SHOOT
    }


    /// <summary>
    /// 敵機のスクリプト
    /// </summary>
    public class EnemyBehavior : MonoBehaviour
    {
        GameObject Player;

        public AI_PATTERN BehaviorPattern = AI_PATTERN.STOP;
        HITAWAY_STATE nowState = HITAWAY_STATE.AWAY;

        float Timer;

        [SerializeField]
        float Speed = 0.01f;

        [SerializeField]
        GameObject Bullet;

        [SerializeField]
        const float ShootBulletSpeed = 0.1f;

        // Use this for initialization
        void Start()
        {
            Player = GameObject.Find("Player");
            Timer = Random.Range(-1,3);
        }


        // Update is called once per frame
        void Update()
        {
            switch (BehaviorPattern)
            {
                case AI_PATTERN.CHASE:
                    // プレイヤーの位置を目的地に設定
                    GetComponent<DestinationHolder>().SetDestination(Player.transform.position);
                    FlyToDestination();

                    Timer += Time.deltaTime;

                    if (Timer >= 5)
                    {
                        Shoot();
                        Timer = Random.Range(-1, 1);
                    }

                    break;

                case AI_PATTERN.HITAWAY:
                    {

                        switch (nowState)
                        {
                            case HITAWAY_STATE.AWAY:
                                // プレイヤーの反対の位置を目的地に設定
                                GetComponent<DestinationHolder>().SetDestination(2*transform.position - Player.transform.position);
                                FlyToDestination(2.0f);
                                var Distance = (transform.position - Player.transform.position).magnitude;
                                if (Distance >= 4) nowState = HITAWAY_STATE.TURN;
                                Timer = Random.Range(-1,1);
                                break;

                            case HITAWAY_STATE.TURN:
                                GetComponent<DestinationHolder>().SetDestination(Player.transform.position);
                                FlyToDestination(4.0f, 3.0f);

                                Timer += Time.deltaTime;

                                if (Timer >= 2)
                                {
                                    Timer = 0;
                                    Shoot(ShootBulletSpeed);
                                    nowState = HITAWAY_STATE.AWAY;
                                }

                                break;
                            default:
                                break;
                        }
                    }
                    break;

                case AI_PATTERN.STOP:
                default:
                    break;

            }
        }


        void FlyToDestination(float _speedMagnitude = 1.0f, float _turnMagnitude = 1.0f)
        {
            // 設定した目的地にだんだん向く
            Vector3 TargetPos = GetComponent<DestinationHolder>().GetDestination();
            Quaternion targetRotation = Quaternion.LookRotation(TargetPos - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _turnMagnitude);

            // 向いている方向に飛ぶ
            transform.Translate(new Vector3(0, 0, Speed * _speedMagnitude));
            return;
        }

        void Shoot(float BulletSpeed = ShootBulletSpeed, float _BulletInstLength = 0.25f)
        {
            GameObject InstBullet = Instantiate(Bullet);
            InstBullet.transform.position = this.transform.position + this.transform.forward * _BulletInstLength;
            //  そいつの向いている方向に飛ぶ
            InstBullet.GetComponent<BulletBehavior>().Destination = this.transform.forward;
            InstBullet.GetComponent<BulletBehavior>().speed = BulletSpeed;
        }

        /// <summary>
        /// 破壊されたとき
        /// </summary>
        void Destroy()
        {
            Debug.Log("Destroy -- Enemy");
            Destroy(gameObject);
        }
    }
}

