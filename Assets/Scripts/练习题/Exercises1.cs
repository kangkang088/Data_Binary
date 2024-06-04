using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Exercises1 : MonoBehaviour {
    void Start() {
        Student s = new Student();
        s.age = 18;
        s.name = "kangkang";
        s.number = 1;
        s.sex = true;

        //s.Save("Kang");
        Student s2 = Student.Load("Kang");
    }
    void Update() {

    }
}
public class Student {
    public int age;
    public string name;
    public int number;
    public bool sex;
    public void Save(string fileName) {
        Debug.Log(Application.persistentDataPath);
        if(!Directory.Exists(Application.persistentDataPath + "/Student")) {
            Directory.CreateDirectory(Application.persistentDataPath + "/Student");
        }
        using(FileStream fs = new FileStream(Application.persistentDataPath + "/Student/" + fileName + ".tang",FileMode.OpenOrCreate,FileAccess.Write)) {
            byte[] bytes = BitConverter.GetBytes(age);
            fs.Write(bytes,0,bytes.Length);

            bytes = Encoding.UTF8.GetBytes(name);
            int length = bytes.Length;
            fs.Write(BitConverter.GetBytes(length),0,4);
            fs.Write(bytes,0,length);

            bytes = BitConverter.GetBytes(number);
            fs.Write(bytes,0,bytes.Length);

            bytes = BitConverter.GetBytes(sex);
            fs.Write(bytes,0,bytes.Length);

            fs.Flush();
            fs.Close();
            fs.Dispose();
        }
    }
    public static Student Load(string fileName) {
        if(!File.Exists(Application.persistentDataPath + "/Student/" + fileName + ".tang")) {
            Debug.LogWarning("Not Found");
            return null;
        }
        Student s = new Student();
        using(FileStream fs = new FileStream(Application.persistentDataPath + "/Student/" + fileName + ".tang",FileMode.Open,FileAccess.Read)) {
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes,0,bytes.Length);
            fs.Close();

            int index = 0;
            s.age = BitConverter.ToInt32(bytes,index);
            index += 4;
            int length = BitConverter.ToInt32(bytes,index);
            index += 4;
            s.name = Encoding.UTF8.GetString(bytes,index,length);
            index += length;
            s.number = BitConverter.ToInt32(bytes,index);
            index += 4;
            s.sex = BitConverter.ToBoolean(bytes,index);
            index += 1;
        }
        return s;
    }
}
