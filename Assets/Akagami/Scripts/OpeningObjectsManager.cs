using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace gami
{
    public class OpeningObjectsManager : MonoBehaviour
    {
        [SerializeField]
        public GameObject[] openingObj = new GameObject[]
        {
            null
        };
        static GameObject[] obj;
        private void Awake()
        {
            obj = openingObj;
        }
        public static void DestroyOpeningObjects()
        {
            foreach (GameObject e in obj)
            {
                Destroy(e);
            }
            
        }
    }
}