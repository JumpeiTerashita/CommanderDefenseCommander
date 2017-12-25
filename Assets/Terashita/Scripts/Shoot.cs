using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTB
{
    /// <summary>
    /// 自機の射撃機能 ... Playerにアタッチ
    /// </summary>
    public class Shoot : MonoBehaviour
    {
        //  弾を自機からどんくらい前に出すか
        //  0.25fくらいがちょうどよい
        [SerializeField]
        float BulletInstLength = 0.25f;

        [SerializeField]
        GameObject Bullet;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                //Debug.Log("Fire");
                GameObject InstBullet = Instantiate(Bullet);
                InstBullet.transform.position = this.transform.position + this.transform.forward * BulletInstLength;
                //  プレイヤーの向いている方向に飛ぶ
                InstBullet.GetComponent<BulletBehavior>().Destination = this.transform.forward;
            }
            
        }
    }
}