using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRace : MonoBehaviour {
	// Use this for initialization
	void Start ()
    {
        gami.ExternalFileLoader.LoadExternalFile("Akagami/CheckPointCreatePos.txt");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
