using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace gami
{
    public class OpeningSceneManager : MonoBehaviour
    {
        static GameObject thisObj;
        [SerializeField]
        public GameObject[] openingObj = new GameObject[]
        {
            null
        };
        static GameObject[] obj;

        [SerializeField]
        GameObject boidsController;
        static GameObject boidsCtr;

        /// <summary>
        /// 生成する群れの数 by KTB
        /// </summary>
        [SerializeField]
        static int BoidsNumber = 3;

        private void Awake()
        {
            obj = openingObj;
            thisObj = this.gameObject;
            boidsCtr = boidsController;
        }
        public static void DestroyOpeningObjects()
        {
            foreach (GameObject e in obj)
            {
                Destroy(e);
            }
            
            Destroy(thisObj);
        }
        
        public static void CreateGameSceneObj()
        {
            GameObject obj = (GameObject)Resources.Load("TimeManager");
            obj = Instantiate(obj);
            obj.AddComponent<ResultSceneLoader>();

            for (int i = 0; i < BoidsNumber; i++)
            {
                Instantiate(boidsCtr);
            }
        }
    }
}