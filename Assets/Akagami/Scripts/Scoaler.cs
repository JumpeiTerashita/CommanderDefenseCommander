using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace gami
{
    public class Scoaler
    {
        private float score = 0;
        Scoaler()
        {
            score = 0;
        }
        ~Scoaler()
        {

        }
        public void plusScore(float _plusCount)
        {
            score += _plusCount;
        }
        public float getScore()
        {
            return score;
        }
    }
}