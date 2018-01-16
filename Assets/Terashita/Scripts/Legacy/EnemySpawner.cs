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
        float SpawnRandomTime = 0.5f;

        [SerializeField]
        float RandomRange = 1.5f;

        bool isStarted = false;
        bool isRunning = false;
        bool isAlreadySpawn = false;

        // Use this for initialization
        void Start()
        {
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
                Vector3 SpawnPoint = new Vector3(
                    transform.position.x + Random.Range(-RandomRange, RandomRange),
                    transform.position.y + Random.Range(-RandomRange, RandomRange),
                    transform.position.z + Random.Range(-RandomRange, RandomRange)
                    );
                GameObject SpawnedEnemy = Instantiate(Enemy, SpawnPoint, Quaternion.identity);
                SpawnedEnemy.transform.LookAt(Player.transform.position);
                //Debug.Log(Player.transform.position);
                //Debug.Log(SpawnedEnemy.transform.forward);
            }

            yield return new WaitForSeconds(SpawnSpan+ Random.Range(-SpawnRandomTime, SpawnRandomTime));


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
