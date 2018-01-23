using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace gami
{
    public class WeaponParentMover : MonoBehaviour
    {
        [SerializeField]
        GameObject player;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            this.transform.position = player.transform.position;
            this.transform.localEulerAngles =
                new Vector3(this.transform.eulerAngles.x, player.transform.eulerAngles.y + 270, this.transform.eulerAngles.z);
        }
    }

}