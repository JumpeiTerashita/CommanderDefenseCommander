﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTB
{
    // TODO : 敵が目的地到着したら目的地決め直すのはどう？
    [RequireComponent(typeof(SphereCollider))]

    /// <summary>
    /// 敵生成オブジェクト　指定座標
    /// </summary>
    public class RandomChaseTarget : MonoBehaviour
    {
        GameObject Player;

        [SerializeField]
        float ChangeSpan = 5.0f;

        [SerializeField]
        float RandomLimit = 3.0f;

        Vector3 TargetPosition;

        bool isStarted = false;
        bool isRunning = false;

        // Use this for initialization
        void Start()
        {
            Player = GameObject.Find("Player");
            TargetPosition = transform.position;
        }

        void Update()
        {
            if (!isStarted)
            {
                isStarted = true;
                StartCoroutine(ChangePosLoop());
            }
        }

        IEnumerator ChangePosLoop()
        {
            if (isRunning) yield break;
            isRunning = true;
            //  Debug.Log("I'm Running");

            TargetPosition = new Vector3(
                Player.transform.position.x + Random.Range(-RandomLimit, RandomLimit),
                Player.transform.position.y + Random.Range(-RandomLimit, RandomLimit),
                Player.transform.position.z + Random.Range(-RandomLimit, RandomLimit)
                );
            transform.position = TargetPosition;

            yield return new WaitForSeconds(ChangeSpan);


            isRunning = false;
            StartCoroutine(ChangePosLoop());
        }

        public void ResetPos()
        {
            StopCoroutine(ChangePosLoop());
            isRunning = false;
            StartCoroutine(ChangePosLoop());
        }
    }
}
