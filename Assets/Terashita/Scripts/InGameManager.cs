using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace KTB
{
    public class InGameManager : SingleTon<InGameManager>
    {
        [System.NonSerialized]
        public GameObject inGameManager;

        [System.NonSerialized]
        public ReactiveProperty<int> Score = new ReactiveProperty<int>();

        [System.NonSerialized]
        public GameObject[] Missile = new GameObject[4];

        [System.NonSerialized]
        public bool IsTutorial = true;

        [SerializeField]
        GameObject enemySpawner;

        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            Debug.Log("InGameManager Initialized");
            Score.Subscribe(score=> {
                Debug.Log("Now Score = "+score);
            });
            inGameManager = this.gameObject;
            IsTutorial = true;
            SearchMissile();
        }

        private void Update()
        {
            if (IsTutorial)
            {
                //  TutorialMissileが全破壊されてればチュートリアル終了
                for (int i = 0; i < 4; i++)
                {
                    if (Missile[i] != null) return;
                }
                Debug.Log("Tutorial Finished");
                IsTutorial = false;
                Instantiate(enemySpawner);
            }
            

            
        }

        void SearchMissile()
        {
            for (int i = 0; i < 4; i++)
            {
                Missile[i] = GameObject.Find("Missile"+i);
                Debug.Log("Missile " + i + " Found");
            }
        }

        

        //  TODO : TutorialMissileが死んだとき
        //         InGameManagerのMissile[]をヌルにしよう
        //         その為にMissleにIdを持たせよう！

        void Refresh()
        {
            Score.Value = 0;
        }
    }
}