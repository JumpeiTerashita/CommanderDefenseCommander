using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace gami
{
    public class ResultSceneLoader : MonoBehaviour
    {


        // Update is called once per frame
        void Update()
        {
            if (this.GetComponent<gami.Timer>().GetTime() <= 0)
            {
                //SceneManager.LoadScene("");
                Debug.Log(1);
            }
        }
    }

}