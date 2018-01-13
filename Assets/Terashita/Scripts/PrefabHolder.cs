using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTB
{
    public class PrefabHolder : SingleTon<PrefabHolder>
    {
        public GameObject Inst;
        public GameObject[] Explode = new GameObject[6];

        public GameObject Enemy;
        public GameObject BoidsController;


        // Use this for initialization
        void Start()
        {
            Inst = (GameObject)Resources.Load("Conv");
            Enemy = (GameObject)Resources.Load("TestEnemy");



            for (int i = 0; i < 6; i++)
            {
                Explode[i] = (GameObject)Resources.Load("Exp" + i);

            }

            BoidsController = (GameObject)Resources.Load("BoidsController");
            BoidsController.GetComponent<BoidsController>().BoidsChild = Enemy;

            
        }

        
    }
}