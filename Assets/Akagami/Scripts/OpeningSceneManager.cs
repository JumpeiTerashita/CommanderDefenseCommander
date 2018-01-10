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
            Instantiate(boidsCtr);
        }
    }
}