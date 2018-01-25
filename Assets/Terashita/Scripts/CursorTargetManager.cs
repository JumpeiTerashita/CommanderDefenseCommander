using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KTB
{
    public class CursorTargetManager : MonoBehaviour {
        GameObject cursor;
        // Use this for initialization
        void Start() {
            cursor = GameObject.Find("arrowCursor");

            //cursor.GetComponent<gami.CursolFacePlayer>().lookAtTarget
    
    }

        // Update is called once per frame
        void Update() {

        }
    }
}