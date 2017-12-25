using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour {

    // gami追加
    bool isSideUnder = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetSideUnderFlag(bool _flag)
    {
        isSideUnder = _flag;
    }
    // gami
    // 高さを受け取った分だけ高くして位置を調整
    public void SetHeightParam(float _param)
    {
        //Debug.Log(this.transform.localScale.y + _param);
        // 愚直にスケールにplus
        if (float.IsNaN(_param)) _param = 0;
        this.transform.localScale = 
            new Vector3(this.transform.localScale.x, this.transform.localScale.y + _param, this.transform.localScale.z);
        float yPos = _param / 2;
        
        // フラグによってポジションから加減
        if (isSideUnder)
        {
            this.transform.position =
                new Vector3(this.transform.position.x, this.transform.position.y - yPos, this.transform.position.z);
        }
        else
        {
            this.transform.position =
                new Vector3(this.transform.position.x, this.transform.position.y + yPos, this.transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
            Debug.Log("HIT");
            other.gameObject.SendMessage("Destroy");
            //Destroy();
        

    }
}
