using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace gami
{
    public class SceneOpening : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            Popup.Open2ButtonPop("シューターコマンダー", "シューティング", "レース",
                () => SceneManager.LoadScene("Terashita/Scenes/Shooting"),
                () => SceneManager.LoadScene("raceScene"));
        }
    }
}