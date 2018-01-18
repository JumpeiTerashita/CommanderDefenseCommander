using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {

    [SerializeField]
    Light dirLight;

	// Use this for initialization
	void Start () {
        GameObject MRCamera = GameObject.Find("MixedRealityCamera");
        var Light = Instantiate(dirLight);
        Light.transform.SetParent(MRCamera.transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
