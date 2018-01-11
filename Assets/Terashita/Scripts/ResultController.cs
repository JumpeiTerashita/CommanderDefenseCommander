using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KTB
{
    public class ResultController : MonoBehaviour
    {
        [SerializeField]
        Text scoreText;

        // Use this for initialization
        void Start()
        {
            scoreText.text = InGameManager.Instance.Score.Value.ToString();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}