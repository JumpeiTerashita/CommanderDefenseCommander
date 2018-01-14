using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTB
{
    /// <summary>
    /// 弾の持つ機能　今のところ
    /// 1.移動
    /// </summary>
    public class BulletBehavior : MonoBehaviour
    {
        public Vector3 Destination;

        [SerializeField]
        public float speed = 0.1f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(speed*Destination);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("HIT -- Player");
                other.gameObject.SendMessage("Destroy");
                Destroy();
            }

            if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("HIT -- Enemy");
                other.gameObject.SendMessage("Destroy");
                Destroy();
            }
        }

        void Destroy()
        {
            Destroy(gameObject);
        }
    }
}