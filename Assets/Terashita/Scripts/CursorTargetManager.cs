using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KTB
{
    public class CursorTargetManager : MonoBehaviour
    {
        InGameManager inGameInstance;
        GameObject cursor;
        bool isTutorial;
        int missileMinNum;

        // Use this for initialization
        void Start()
        {
            cursor = GameObject.Find("arrowCursor");
            isTutorial = true;
            //cursor.GetComponent<gami.CursolFacePlayer>().lookAtTarget = InGameManager.Instance.Missile[0];
            inGameInstance = InGameManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            if (isTutorial)
            {
                if (cursor.GetComponent<gami.CursolFacePlayer>().lookAtTarget == null)
                {
                    var missileNum = inGameInstance.GetMissileNum();
                    if (missileNum == 4)
                    {
                        isTutorial = false;
                        missileMinNum = 4;
                        return;
                    }
                    cursor.GetComponent<gami.CursolFacePlayer>().lookAtTarget = inGameInstance.Missile[missileNum];
                }
            }
            else
            {
                if (cursor.GetComponent<gami.CursolFacePlayer>().lookAtTarget == null)
                {
                    Debug.Log("I ' ll search missile");
                    int missileNumber = missileMinNum;
                    bool missileFound = false;

                    while (missileFound)
                    {
                        if (GameObject.Find("Missile" + missileNumber.ToString()) != null)
                        {
                            Debug.Log("Missile OK");
                            missileFound = true;
                        }
                        else
                        {
                            Debug.Log("Missile NG");
                            missileNumber++;
                        }
                    }

                    missileMinNum = missileNumber;
                    cursor.GetComponent<gami.CursolFacePlayer>().lookAtTarget = GameObject.Find("Missile"+missileNumber.ToString());
                    
                }
            }

        }

        
    }
}