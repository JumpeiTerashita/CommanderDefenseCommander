using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace KTB
{
    public class ResultController : MonoBehaviour
    {
        [SerializeField]
        Text scoreText;

        [SerializeField]
        float WaitTime = 20.0f;

        // Use this for initialization
        void Start()
        {
            scoreText.text = InGameManager.Instance.Score.Value.ToString();
            Destroy(InGameManager.Instance.inGameManager);
            StartCoroutine(LoadNextScene());
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator LoadNextScene()
        {
            yield return new WaitForSeconds(WaitTime);
            Debug.Log("Scene Changed => InGame");
            SceneManager.LoadScene("InGame");
        }
    }
}