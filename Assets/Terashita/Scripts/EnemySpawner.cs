using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

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
        float SpawnLength = 10.0f;

        [SerializeField]
        float SpawnSpan = 5.0f;

        [SerializeField]
        float SpawnRandomTime = 0.5f;

        /// <summary>
        /// 上下のランダム範囲　- RandomRange から  RandomRange まで
        /// </summary>
        [SerializeField]
        public float RandomRange = 1.5f;

        bool isStarted = false;
        bool isRunning = false;
        bool isAlreadySpawn = false;

        int missileId;

        // Use this for initialization
        void Start()
        {
            Player = GameObject.Find("Player");
            if (!Player) Destroy(gameObject);
            missileId = 3;
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
                var RandomThita = Random.Range(0, 2 * Mathf.PI);
                Vector3 SpawnPoint = new Vector3(
                    SpawnLength * Mathf.Cos(RandomThita),
                    //transform.position.x + Random.Range(-RandomRange, RandomRange),
                    Random.Range(-RandomRange, RandomRange),
                    SpawnLength * Mathf.Sin(RandomThita)
                    );
                missileId++;
                GameObject SpawnedEnemy = Instantiate(Enemy, SpawnPoint, Quaternion.identity);
                SpawnedEnemy.name = ("Missile"+missileId);
                SpawnedEnemy.GetComponent<MissileBehavior>().id = missileId;
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
