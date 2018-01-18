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
        }
    }

}