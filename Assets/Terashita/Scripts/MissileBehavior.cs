using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace KTB
{
    [RequireComponent(typeof(DestinationHolder))]
    public class MissileBehavior : MonoBehaviour
    {
        [SerializeField]
        float ArriveLength = 0.25f;

        [SerializeField]
        float Speed = 0.01f;

        bool IsDead = false;

        public bool IsTutorial;

        private Subject<Collision> onCollision = new Subject<Collision>();

        [SerializeField]
        int TutorialID;

        bool hasArrived;

        // Use this for initialization
        void Start()
        {
            hasArrived = false;
            GameObject cameraObj = GameObject.Find("MixedRealityCameraParent");
            IsDead = false;

            GetComponent<DestinationHolder>().SetDestination(new Vector3( cameraObj.transform.position.x,transform.position.y,cameraObj.transform.position.z));

            OnCollision().Subscribe(col=>CollisionProcess(col));
            //Debug.Log(GetComponent<DestinationHolder>().GetDestination());
        }

        // Update is called once per frame
        void Update()
        {
            if (IsTutorial) return;

            // 向いて移動
            FlyToDestination();

            // 目的地に到着済みか確認
            //ArriveCheck();

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

        //FIX : なんかうまくとれない Colliderでやるべき
        void ArriveCheck()
        {
            var Destination = GetComponent<DestinationHolder>().GetDestination();
            Vector3 Distance = Destination - transform.position;
            if (MyMath.IsShortLength(Destination, ArriveLength))
            {
                hasArrived = true;
                Debug.Log("Has Arrived");
            }
        }

        /// <summary>
        /// ミサイルが物と当たったときの処理
        /// </summary>
        /// <param name="collision">当たるTargetのCollision</param>
        public void CollisionProcess(Collision collision)
        {
            Destroy(gameObject);
            Debug.Log("Missile Break!!");

            if (IsTutorial) { InGameManager.Instance.Missile[TutorialID] = null; return; }
            else if (collision != null && collision.transform.tag == "Camera") return;
            InGameManager.Instance.Score.Value++;
            //if(col.transform.tag == "Weapon")
            //{
            //    Vector3 hitPos = new Vector3(0, 0, 0);
            //    foreach (ContactPoint point in __.contacts)
            //    {
            //        hitPos = point.point;
            //    }
            //    __.gameObject.GetComponent<gami.WeaponController>().SetScale(hitPos);
            //}
        }

        private void OnCollisionEnter(Collision collision)
        {
            onCollision.OnNext(collision);
        }

        public IObservable<Collision> OnCollision()
        {
            return onCollision;
        }
    }
}