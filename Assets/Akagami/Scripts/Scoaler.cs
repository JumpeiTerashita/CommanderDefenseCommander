using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace gami
{
    public class Scoaler : MonoBehaviour
    {
        static Scoaler instance;
        private float score = 0;
        public static Scoaler Instance
        {
            get
            {
                if(instance == null)
                {
                    GameObject obj = new GameObject("Scoaler");
                    instance = obj.AddComponent<Scoaler>();
                }
                return instance;
            }
        }
        Scoaler(){}
        ~Scoaler(){}
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        public void PlusScore(float _plusCount)
        {
            score += _plusCount;
        }
        public float GetScore()
        {
            return score;
        }
        public void InitScore()
        {
            score = 0;
        }
    }
}