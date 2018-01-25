using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KTB
{
    public class CursorTargetManager : MonoBehaviour
    {
        InGameManager inGameInstance;
        GameObject cursor;
        // Use this for initialization
        void Start()
        {
            cursor = GameObject.Find("arrowCursor");

            //cursor.GetComponent<gami.CursolFacePlayer>().lookAtTarget = InGameManager.Instance.Missile[0];
            inGameInstance = InGameManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            if (cursor.GetComponent<gami.CursolFacePlayer>().lookAtTarget == null)
            {
                var missileNum = inGameInstance.GetMissileNum();
                cursor.GetComponent<gami.CursolFacePlayer>().lookAtTarget = inGameInstance.Missile[missileNum];
            }
        }

        
    }
}