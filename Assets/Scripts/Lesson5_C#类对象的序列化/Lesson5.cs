using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Lesson5 : MonoBehaviour
{
    void Start()
    {
        Person person = new Person();
        using(MemoryStream ms = new MemoryStream()) {
            BinaryFormatter bf = new BinaryFormatter();
            //将对象序列化到内存流中
            bf.Serialize(ms,person);
            //获取内存流中的对象的字节数据
            byte[] bytes = ms.GetBuffer();
            //存储字节数据
            File.WriteAllBytes(Application.dataPath + "/Lesson5.txt",bytes);
            ms.Flush();
            ms.Close();
            ms.Dispose();
        }

        using(FileStream fs = new FileStream(Application.dataPath + "/Lesson5_2.txt",FileMode.OpenOrCreate,FileAccess.Write)) {
            BinaryFormatter bf = new BinaryFormatter();
            //将对象序列化到文件流（直接写到文件中了）
            bf.Serialize(fs,person);
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }
    }
    void Update()
    {
        
    }
}
[System.Serializable]
public class Person {
    public int age = 1;
    public string name = "kangkang";
    public int[] ints = new int[] { 1,2,3,4,5,6 };
    public List<int> list = new List<int> { 1,2,3,4 };
    public Dictionary<int,string> dic = new Dictionary<int,string> { { 1,"123" },{ 2,"1223" },{ 3,"122313" } };
    public StructTest st = new StructTest(2,"123");
    public ClassTest ct = new ClassTest();
}
[System.Serializable]
public struct StructTest {
    public int i;
    public string s;
    public StructTest(int i,string s) {
        this.i = i;
        this.s = s;
    }
}
[System.Serializable]
public class ClassTest {
    public int i = 1;
}
