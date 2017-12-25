using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gami
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField]
        public AudioClip audio;

        float[] waveData = new float[1024];

        private AudioSource source;

        private void Start()
        {
            source = gameObject.GetComponent<AudioSource>();
            source.clip = audio;
            source.Play();
            source.loop = true;
        }

        private void Update()
        {
            source.GetOutputData(waveData, 1);
            DrawLine();
        }
        public float[] GetWaveData()
        {
            return waveData;
        }
        private void DrawLine()
        {

            var spectrum = waveData;
            for (int i = 1; i < spectrum.Length - 1; ++i)
            {
                Debug.DrawLine(
                        new Vector3(i - 1, spectrum[i] + 10, 100),
                        new Vector3(i, spectrum[i + 1] + 10, 100),
                        Color.red);
                Debug.DrawLine(
                        new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 100),
                        new Vector3(i, Mathf.Log(spectrum[i]) + 10, 100),
                        Color.cyan);
                Debug.DrawLine(
                        new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 100),
                        new Vector3(Mathf.Log(i), spectrum[i] - 10, 100),
                        Color.green);
                Debug.DrawLine(
                        new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 100),
                        new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 100),
                        Color.yellow);
            }
        }
    }
}