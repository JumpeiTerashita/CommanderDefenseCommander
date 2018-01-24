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

        GameObject tutorialTube;

        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            Debug.Log("InGameManager Initialized");
            Score.Subscribe(score=> {
                Debug.Log("Now Score = "+score);
            });
            inGameManager = this.gameObject;
            Refresh();
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
                IsTutorial = false;
                Instantiate(enemySpawner);

                Destroy(tutorialTube);

                // TODO : ここでタイマー作動
                
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

        public void Refresh()
        {
            tutorialTube = GameObject.Find("Tube");
            IsTutorial = true;
            Score.Value = 0;
            SearchMissile();
        }
    }
}