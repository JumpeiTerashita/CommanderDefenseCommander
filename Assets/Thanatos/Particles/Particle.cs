using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {


    void OnParticleCollision(GameObject other) {

        //
        if (other.gameObject) {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Enemy") {
            Destroy(gameObject);
        }
       
    }

    void OnCollisionEnter(Collision other) {
    
        
            
    }

    void Update () {

        // InspectorのOFF
        //gameObject.SetActive(false);

        // InspectorのON
        //gameObject.SetActive(true);

    }
}
