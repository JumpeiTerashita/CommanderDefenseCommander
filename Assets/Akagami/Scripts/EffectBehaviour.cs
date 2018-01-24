using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBehaviour : MonoBehaviour {

    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log(other.transform.tag);
        if (other.transform.tag == "Enemy")
        {
            Collision col = null;
            
        }
    }
}
