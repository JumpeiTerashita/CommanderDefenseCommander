using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTB
{
    /// <summary>
    /// 目的地保持クラス
    /// 目的地に向かって移動とかさせたいオブジェクトに持たせてつかう
    /// </summary>
    public class DestinationHolder : MonoBehaviour
    {
        Vector3 Destination;

        [SerializeField]
        Vector3 TargetPos;

        // Use this for initialization
        void Start()
        {
            SetDestination();
        }


        public void SetDestination()
        {
            Destination = TargetPos;
        }

        public void SetDestination(Vector3 _TargetPos)
        {
            TargetPos = _TargetPos;
            Destination = TargetPos;
        }


        public Vector3 GetDestination()
        {
            return Destination;
        }
    }
}