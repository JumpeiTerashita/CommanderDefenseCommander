using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gami
{
    public class ContactedObject : MonoBehaviour
    {
        [SerializeField]
        gami.Timer timer;
        // thisオブジェクトがほかのオブジェクトを通過したとき
        private void OnTriggerEnter(Collider other)
        {
            // checkPointを通過したとき
            if (other.gameObject.tag == "checkPoint")
            {
                // 制限時間内なら加点
                if (timer.InLimitTime())
                {
                    this.GetComponent<gami.Scoaler>().plusScore(1);
                    return;
                }
            }
        }
        // 他のオブジェクトと接触したとき
        private void OnCollisionEnter(Collision collision)
        {
            // 破壊されるようにする予定
            Debug.Log(1);
        }

    }
}