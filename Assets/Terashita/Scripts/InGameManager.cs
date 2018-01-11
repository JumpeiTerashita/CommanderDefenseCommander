using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace KTB
{
    public class InGameManager : SingleTon<InGameManager>
    {
        public ReactiveProperty<int> Score = new ReactiveProperty<int>();

        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            Debug.Log("InGameManager Initialized");
            Score.Subscribe(score=> {
                Debug.Log("Now Score = "+score);
            });
        }

        void Refresh()
        {
            Score.Value = 0;
        }
    }
}