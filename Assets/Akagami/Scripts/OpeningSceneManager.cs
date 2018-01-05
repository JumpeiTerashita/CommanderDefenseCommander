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
        private void Awake()
        {
            obj = openingObj;
            thisObj = this.gameObject;
        }
        public static void DestroyOpeningObjects()
        {
            foreach (GameObject e in obj)
            {
                Destroy(e);
            }
            
            Destroy(thisObj);
        }
        
        public static void CreateTimer()
        {
            GameObject obj = (GameObject)Resources.Load("TimeManager");
            obj = Instantiate(obj);
            obj.AddComponent<ResultSceneLoader>();
        }
    }
}