using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using System.Text;
namespace gami
{
    public class ExternalFileLoader
    {
        // 引数にはファイルの名前を入れる
        // 注意　使用の際は(1)を読んでください。
        public static void LoadExternalFile(string _fileName)
        {
           // StreamReader objReader = new StreamReader(_fileName);
           // objReader.Close();

            string dataText = ""; // ←　一時格納用
            // 現在の基準のパスを取得
            // ...していると思う。
            string dataPath = Application.dataPath;
            string persistentDataPath = Application.persistentDataPath;


            // 上で読み取ったパスの中のファイルネームのテキストを開く。
            //***********
            //* (1)
            //***********
            // 基準pathがAssetなのでAssetより下の階層,例えば
            // ShooterCommander / Asset / Akagami / test.txtを開きたい場合
            // 引数には Akagami/test.txtのように現在の階層のパスも含めてください
            FileInfo fInfo = new FileInfo(dataPath + "/" + _fileName);
            try
            {
                using (StreamReader sReader = new StreamReader(fInfo.OpenRead(), Encoding.UTF8))
                {
                    dataText = sReader.ReadToEnd();
                }
            }
            catch(Exception e)
            {
                dataText = "error"; 
            }
            Debug.Log(dataText);

            // この先未完成


            //// ファイルから1行ずつ読み込む
            //while (sLine != null)
            //{
            //    // ファイルから1行ずつ読み込むReadToEndでもいい
            //    sLine = objReader.ReadLine();
            //}
            //objReader.Close();
            //InData[a, b] = Convert.ToDouble(value);
        }
    }
}