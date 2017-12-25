using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace gami
{
    public class PillarParamSetter : MonoBehaviour
    {
        [SerializeField]
        GameObject audio;
        [SerializeField]
        float scaleMag = 1;
        // Update is called once per frame
        void Update()
        {
            if (audio == null) return;
            SetPillarParam();
        }
        public void SetPillarParam()
        {
            // 全体の子オブジェクトにアクセス
            int childCount = this.transform.childCount;
            float[] data = new float[childCount];
            //int dataSize = data.Length;
            //Debug.Log(dataSize);
            for (int i = 0; i < childCount; i++)
            {
                //Debug.Log(audio.GetComponent<AudioPlayer>().GetWaveData()[i] + 1);
                // 子オブジェクトのparameter取得関数に音楽ファイルのデータを代入
                float scaleParam = (audio.GetComponent<AudioPlayer>().GetWaveData()[i] + 1) * scaleMag;
                //(Mathf.Log(audio.GetComponent<AudioPlayer>().GetWaveData()[i]))*scaleMag;
                //Debug.Log(scaleParam);
                this.transform.GetChild(i).GetComponent<Pillar>().
                    SetHeightParam(scaleParam);
            }
        }
    }
}