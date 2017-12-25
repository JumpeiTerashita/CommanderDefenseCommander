using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace gami
{
    public class PopupButtonSelect : MonoBehaviour
    {
        float originScale;
        int select;
        float inputVertical;
        float beforeVertical;
        [SerializeField]
        int maxCount = 1;
        [SerializeField]
        Button button1;
        [SerializeField]
        Button button2;
        [SerializeField]
        Button button3;

        void Start()
        {
            select = 0;
            // 最初のスケールを保持
            originScale = button1.transform.localScale.x;
        }

        // Update is called once per frame
        void Update()
        {
            inputVertical = Input.GetAxis("Select_Vertical");
            // 入力があり、Stickを倒しっぱなしでなかったら
            if ((inputVertical != beforeVertical) &&
               (Mathf.Sqrt(inputVertical * inputVertical) >= 1))
            {
                // 選択を変更
                ChangeSelect(inputVertical);
            }
            if (Input.GetButtonDown("Select_Button_A"))
            {
                CauseAction();
            }
            // 現在の選択に変化を与える
            SelectButton();
            beforeVertical = inputVertical;
        }
        // 選択シーン切り替え関数
        void ChangeSelect(float _inputVertical)
        {
            // Stick入力が上なら
            if (_inputVertical <= 0)
            {
                select -= 1;
            }
            else
            {
                select += 1;
            }
            // シーンの数を超えてしまったら戻す
            if (select > maxCount)
            {
                select = 0;
            }
            if (select < 0)
            {
                select = maxCount;
            }
        }
        // セレクトの数によって起こすアクションを分岐
        void CauseAction()
        {
            // スクリプトのオンクリックを取る
            if (select == 0)
            {
                this.GetComponent<Popup>().Onclick1();
            }
            else if (select == 1)
            {
                this.GetComponent<Popup>().Onclick2();
            }
            else if (select == 2)
            {
                this.GetComponent<Popup>().Onclick3();
            }
        }

        // 選択しているボタンに変化を与えたい
        void SelectButton()
        {
            const float selectButtonMag = 1.2f;
            // 一旦スケール値を元に戻す。
            button1.transform.localScale =
                 new Vector3(originScale, originScale, originScale);
            button2.transform.localScale =
                new Vector3(originScale, originScale, originScale);
            if (maxCount > 1)
            {
                button3.transform.localScale =
                    new Vector3(originScale, originScale, originScale);
            }

            // セレクトされてるボタンを拡大する。
            if (select == 0)
            {
                button1.transform.localScale =
                    new Vector3(originScale * selectButtonMag, originScale * selectButtonMag, originScale * selectButtonMag);
                return;
            }
            else if (select == 1)
            {
                button2.transform.localScale =
                   new Vector3(originScale * selectButtonMag, originScale * selectButtonMag, originScale * selectButtonMag);
                return;
            }
            else if (select == 2)
            {
                button3.transform.localScale =
                   new Vector3(originScale * selectButtonMag, originScale * selectButtonMag, originScale * selectButtonMag);
                return;
            }
        }
    }
}