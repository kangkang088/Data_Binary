using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Lesson3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //FileStream fs = new FileStream(Application.dataPath + "/Lesson3.txt",FileMode.OpenOrCreate,FileAccess.ReadWrite,FileShare.ReadWrite);

        //FileStream fs2 = File.Create(Application.dataPath + "/Lesson3_1.txt",2048,FileOptions.Asynchronous);

        //FileStream fs3 = File.Open(Application.dataPath + "/Lesson3_2.txt",FileMode.OpenOrCreate);

        //FileStream fs = File.Open(Application.dataPath + "/Lesson3.txt",FileMode.OpenOrCreate);
        //print(fs.Length);
        //if(fs.CanRead) {
        //    print("Can Read");
        //}
        //if(fs.CanWrite) {
        //    print("Can Write");
        //}
        //fs.Flush();
        //fs.Close();
        //fs.Dispose();

        #region MyRegion1
        FileStream fs = new FileStream(Application.persistentDataPath + "/Lesson3.txt",FileMode.OpenOrCreate,FileAccess.Write);
        byte[] bytes = BitConverter.GetBytes(999);
        fs.Write(bytes,0,bytes.Length);

        byte[] byte2 = Encoding.UTF8.GetBytes("kangkang,hahaha");
        int length = byte2.Length;
        fs.Write(BitConverter.GetBytes(length),0,4);
        fs.Write(byte2,0,length);
        fs.Flush();
        fs.Dispose();
        print(Application.persistentDataPath);

        FileStream fs2 = File.Open(Application.persistentDataPath + "/Lesson3.txt",FileMode.Open,FileAccess.Read);
        byte[] b1 = new byte[4];

        int index = fs2.Read(b1,0,4);
        int i = BitConverter.ToInt32(b1,0);
        print(index);
        print(i);
        index = fs2.Read(b1,0,4);
        int length1 = BitConverter.ToInt32(b1,0);
        b1 = new byte[length1];
        index = fs2.Read(b1,0,length1);
        print(Encoding.UTF8.GetString(b1));
        fs2.Dispose();
        #endregion

        #region MyRegion2
        FileStream fs3 = File.Open(Application.persistentDataPath + "/Lesson3.txt",FileMode.Open,FileAccess.Read);
        byte[] b2 = new byte[fs3.Length];
        fs3.Read(b2,0,(int)fs3.Length);
        fs3.Dispose();
        int num = BitConverter.ToInt32(b2,0);
        print("::" + num);
        int length2 = BitConverter.ToInt32(b2,4);
        print("::" + length2);
        string str = Encoding.UTF8.GetString(b2,8,length2);
        print("::" + str);
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
