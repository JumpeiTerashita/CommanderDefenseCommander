using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace gami
{
    // Popupを生成するクラス。
    // 実態には画面買いに出ないためにコライダーbillboard,tagalongが張り付いている
    public class Popup : MonoBehaviour
    {
        [SerializeField]
        Text buttonText1;
        [SerializeField]
        Text buttonText2;
        [SerializeField]
        Text buttonText3;

        [SerializeField]
        Text bodyText;

        Action click1;
        Action click2;
        Action click3;


        // Popupを動的に生成、表示する。
        // 本文とボタンの文章、ボタンが押された時の処理を引数に入れてください。
        public static void Open2ButtonPop(string _body, string _buttonStr1, string _buttonStr2, Action click1, Action click2)
        {
            // Popup生成
            GameObject popupObj = Instantiate<GameObject>(Resources.Load<GameObject>("Popup2Button"));
            Popup popup = popupObj.GetComponent<Popup>();
            // 生成したPopupに各引数を代入
            popup.bodyText.text = _body;
            popup.buttonText1.text = _buttonStr1;
            popup.buttonText2.text = _buttonStr2;
            popup.click1 = click1;
            popup.click2 = click2;
        }


        // Popupを動的に生成、表示する。
        // 本文とボタンの文章、ボタンが押された時の処理を引数に入れてください。
        public static void Open3ButtonPop(string _body, string _buttonStr1, string _buttonStr2, string _buttonStr3, Action click1, Action click2, Action click3)
        {
            // リソースの中のPopupCanvasを生成
            GameObject popupObj = Instantiate<GameObject>(Resources.Load<GameObject>("PopupCanvas"));
            Popup popup = popupObj.GetComponent<Popup>();
            // 各値を代入
            popup.bodyText.text = _body;
            popup.buttonText1.text = _buttonStr1;
            popup.buttonText2.text = _buttonStr2;
            popup.buttonText3.text = _buttonStr3;
            popup.click1 = click1;
            popup.click2 = click2;
            popup.click3 = click3;
        }
        public void Onclick1()
        {
            click1();
            Destroy(gameObject);
        }

        public void Onclick2()
        {
            click2();
            Destroy(gameObject);
        }
        public void Onclick3()
        {
            click3();
            Destroy(gameObject);
        }
    }
}