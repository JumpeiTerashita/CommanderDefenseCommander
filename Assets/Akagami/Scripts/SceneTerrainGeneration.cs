using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VR.WSA.Input;
using UnityEngine.UI;
namespace gami
{
    public class SceneTerrainGeneration : MonoBehaviour
    {
        // 地形データをFieldにアタッチ
        [SerializeField]
        GameObject field;
        // 別シーンでのSpatialMappingのマテリアルを保持
        [SerializeField]
        Material fieldMaterial;
        // Use this for initialization
        void Start()
        {
            // 読み込んだ地形を別のシーンに持ち越すために破壊されないようにする
            DontDestroyOnLoad(field);
        }

        void Update()
        {
            //　XBoxコントローラーのAボタンが押されたら
            if (Input.GetKey(KeyCode.Joystick1Button0)||Input.GetButtonDown("Select_Button_A"))
            {
                // SpatialMappingのマテリアルをワイヤーフレームから
                // 保持してあるfieldMaterialへ変更
                field.GetComponent<HoloToolkit.Unity.SpatialMapping.SpatialMappingManager>().
                    SurfaceMaterial = fieldMaterial;
                // シーン切り替え
                SceneManager.LoadScene("Opening");
            }
        }
    }
}
