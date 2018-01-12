using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    public GameObject Camera;

    float Timer = 0;

    // gami追加
    bool isSideUnder = false;

    private void Update()
    {
        //Timer++;
        //if (Timer >= 200)
        //{
        //    Timer = 0;
        //    transform.LookAt(new Vector3 (Camera.transform.position.x,transform.position.y,Camera.transform.position.z));
        //}
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
        
            //Debug.Log("HIT");
            other.gameObject.SendMessage("Destroy");
            //Destroy();
        

    }
}
