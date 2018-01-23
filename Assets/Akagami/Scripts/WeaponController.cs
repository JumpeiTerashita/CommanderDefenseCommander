using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace gami
{
    public class WeaponController : MonoBehaviour
    {

        [SerializeField]
        GameObject player;

        private float baseLength = 0;
        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Enemy")
            {
                Debug.Log("Hiiiiit");
                Vector3 hitPos = new Vector3(0, 0, 0);
                foreach (ContactPoint point in collision.contacts)
                {
                    hitPos = point.point;
                }
                SetScale(hitPos);
            }
        }

        private void BaseSetter()
        {
            if (player == null) return;
            // 現在のプレイヤーから武器の先端までの距離を基準値として保持
            Debug.Log(this.transform.position.z);
            baseLength = Mathf.Sqrt(
                (this.transform.position.x - player.transform.position.x) *
                (this.transform.position.x - player.transform.position.x) +
                (this.transform.position.y - player.transform.position.y) *
                (this.transform.position.y - player.transform.position.y) +
                (this.transform.position.z - player.transform.position.z) *
                (this.transform.position.z - player.transform.position.z)
                ) * 2;
        }
        private void SetPos(float _pos)
        {
            if (this.transform.localPosition.y > 0) _pos *= -1;
            this.transform.localPosition =
                new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + _pos, this.transform.localPosition.z);
        }
        public void SetScale(Vector3 _pos)
        {
            if(baseLength == 0)
            {
                BaseSetter();
            }
            if (player == null) return;
            float scaleMag = (Mathf.Sqrt(
                (_pos.x - player.transform.position.x) * (_pos.x - player.transform.position.x) +
                (_pos.y - player.transform.position.y) * (_pos.y - player.transform.position.y) +
                (_pos.z - player.transform.position.z) * (_pos.z - player.transform.position.z))) / baseLength;
            Debug.Log("legnth = "+Mathf.Sqrt(
                (_pos.x - player.transform.position.x) * (_pos.x - player.transform.position.x) +
                (_pos.y - player.transform.position.y) * (_pos.y - player.transform.position.y) +
                (_pos.z - player.transform.position.z) * (_pos.z - player.transform.position.z)));
            //Debug.Log("base = "+baseLength);
            this.transform.localScale = new Vector3(this.transform.localScale.x, scaleMag, this.transform.localScale.z);
            SetPos(scaleMag / 2);
        }
    }
}