using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    [SerializeField]
    GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        if (collision.transform.tag == "Enemy")
        {
            Debug.Log("Hiiiiit");
            Vector3 hitPos = new Vector3(0,0,0);
            foreach (ContactPoint point in collision.contacts)
            {
                hitPos = point.point;
            }
            SetScale(hitPos);
        }
    }

    public void SetScale(Vector3 _pos)
    {
        if (player == null) return;
        float playerLenge = Mathf.Sqrt(
            (_pos.x - player.transform.position.x)* (_pos.x - player.transform.position.x)+
            (_pos.y - player.transform.position.y)* (_pos.y - player.transform.position.y)+
            (_pos.z - player.transform.position.z)* (_pos.z - player.transform.position.z));
        this.transform.localScale = new Vector3(this.transform.localScale.x,playerLenge,this.transform.localScale.z);
    }
}
