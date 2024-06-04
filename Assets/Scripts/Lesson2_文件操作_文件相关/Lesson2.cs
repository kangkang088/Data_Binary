using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Lesson2 : MonoBehaviour {
    void Start() {
        if(File.Exists(Application.dataPath + "/UnityTeach.tang")) {
            print("文件存在");
        }
        else {
            print("文件不存在");
        }
        //FileStream fs = File.Create(Application.dataPath + "/UnityTeach.tang");


        //byte[] bytes = BitConverter.GetBytes(999);


        //string[] strs = new string[] { "123","tangtang","kangkang","weiwei" };
        //File.WriteAllLines(Application.dataPath + "/UnityTeach.tang",strs);

        //File.WriteAllText(Application.dataPath + "/UnityTeach.tang","康");

        //lock(bytes) {
        //    File.WriteAllBytes(Application.dataPath + "/UnityTeach.tang",bytes);
        //}
        //bytes = File.ReadAllBytes(Application.dataPath + "/UnityTeach.tang");
        //print(BitConverter.ToInt32(bytes,0));

        //string[] strs = File.ReadAllLines(Application.dataPath + "/UnityTeach.tang");
        //for(int i = 0;i < strs.Length;i++) {
        //    print(strs[i]);
        //}

        //print(File.ReadAllText(Application.dataPath + "/UnityTeach.tang"));

        //File.Copy(Application.dataPath + "/UnityTeach.tang",Application.dataPath + "/kangkang.kkang",true);

        //File.Replace(Application.dataPath + "/UnityTeach.tang",Application.dataPath + "/kangkang.kkang",Application.dataPath + "/kangkang.kkangbeifen");

        FileStream fs = File.Open(Application.dataPath + "/kangkang.kkang",FileMode.OpenOrCreate,FileAccess.ReadWrite);
    }
    void Update() {

    }
}
