using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTB
{
    public class EnemyListManager : SingleTon<EnemyListManager>
    {
        List<GameObject> missileList = new List<GameObject>();
        
        public void AddList(GameObject _gameObject)
        {
            missileList.Add(_gameObject);
        }

        public void DelList(int _id)
        {
            for (int i = 0; i < missileList.Count; i++)
            {
                if (missileList[i].GetComponent<MissileBehavior>().id == _id)
                {
                    missileList.RemoveAt(i);
                    //Destroy(missileList[i]);
                    break;
                }
            }
        }

        public GameObject GetList(int _index)
        {
            return missileList[_index];
        }
        
    }
}