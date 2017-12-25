using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTB
{
    [RequireComponent(typeof(Rigidbody))]
    /// <summary>
    /// 自機なりなんなりが何かとぶつかったとき
    /// 慣性力が働き続けるので　解消する
    /// </summary>
    public class StrikeForceReset : MonoBehaviour
    {
        [SerializeField]
        float ResetSec = 1.0f;
        // Use this for initialization
        void Start()
        {
            StartCoroutine(Reset());
        }

        IEnumerator Reset()
        {
            yield return new WaitForSeconds(ResetSec);
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            StartCoroutine(Reset());
        }
    }
}