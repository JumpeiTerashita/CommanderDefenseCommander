using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject bullet;
    int key;

    void PlayerShoot() {

        key = 0;
        if (Input.GetKeyUp(KeyCode.Space)) key = 1;
        if (key != 0) {
            GameObject go = Instantiate(bullet) as GameObject;
            go.transform.position = transform.position;
        }
        if (bullet.transform.position.z > 30.0f) Destroy(bullet);// Partcleだと消えない！ デストロイだと再度Instantiateした時のpositionがデストロイしたところからに書き換えられてしまう為ダメ

    }

    void Update() {

        PlayerShoot();

    }
}
