using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTB
{
    [RequireComponent(typeof(SphereCollider))]

    /// <summary>
    /// 敵生成オブジェクト　指定座標
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        GameObject Player;
        [SerializeField]
        GameObject Enemy;

        [SerializeField]
        float SpawnSpan = 5.0f;

        [SerializeField]
        AI_PATTERN SetBehaviorPattern = AI_PATTERN.STOP;

        Vector3 SpawnPoint;

        bool isStarted = false;
        bool isRunning = false;
        bool isAlreadySpawn = false;

        // Use this for initialization
        void Start()
        {
            SpawnPoint = transform.position;
            Player = GameObject.Find("Player");
            if (!Player) Destroy(gameObject);
        }

        void Update()
        {
            if (!isStarted)
            {
                isStarted = true;
                StartCoroutine(SpawnLoop());
            }
        }

        IEnumerator SpawnLoop()
        {
            if (isRunning) yield break;
            isRunning = true;
            //  Debug.Log("I'm Running");
            if (isAlreadySpawn)
            {
                isAlreadySpawn = false;
            }
            else
            {
                GameObject SpawnedEnemy = Instantiate(Enemy, SpawnPoint, Quaternion.identity);
                SpawnedEnemy.transform.LookAt(Player.transform.position);
                SpawnedEnemy.GetComponent<EnemyBehavior>().BehaviorPattern = SetBehaviorPattern;
                Debug.Log(Player.transform.position);
                Debug.Log(SpawnedEnemy.transform.forward);
            }

            yield return new WaitForSeconds(SpawnSpan);


            isRunning = false;
            StartCoroutine(SpawnLoop());
        }

        void OnTriggerStay(Collider col)
        {
            if (col.tag == "Enemy") isAlreadySpawn = true;
        }

        public void SetSpawnSpan(float _sec)
        {
            SpawnSpan = _sec;
        }

    }
}
