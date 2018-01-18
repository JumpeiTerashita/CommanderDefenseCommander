using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveManager : MonoBehaviour {
    [SerializeField]
    GameObject obj;
    int count = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        count++;
        if (count > 60 ) { obj.SetActive(false); }
        else obj.SetActive(true);
        if (count > 120) { count = 0; }
	}
}
