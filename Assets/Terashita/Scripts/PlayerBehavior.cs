using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTB
{
   
    public class PlayerBehavior : MonoBehaviour
    {
        
        // Use this for initialization
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
           
        }

        void Destroy()
        {
            GetComponent<SkinnedMeshRenderer>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            //gameObject.SetActive(false);
        }

    }
}