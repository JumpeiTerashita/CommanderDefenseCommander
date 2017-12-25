using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace gami
{
    public class PillarParamSetter : MonoBehaviour
    {
        [SerializeField]
        GameObject audio;
        // Update is called once per frame
        void Update()
        {

        }
        public void SetPillarParam()
        {
            int childCount = this.transform.childCount;
            float[] data = new float[childCount];
            for (int i = 0; i < childCount; i++)
            {
                this.transform.GetChild(i);
            }
        }
    }
}