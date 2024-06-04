using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Lesson7 : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Person person = new Person();
        byte key = 121;
        using(MemoryStream ms = new MemoryStream()) {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms,person);
            byte[] bytes = ms.GetBuffer();
            for(int i = 0;i < bytes.Length;i++) {
                bytes[i] ^= key;
            }
            File.WriteAllBytes(Application.dataPath + "/Lesson7.txt",bytes);
        }

        byte[] bytes2 = File.ReadAllBytes(Application.dataPath + "/Lesson7.txt");
        for(int i = 0;i < bytes2.Length;i++) {
            bytes2[i] ^= key;
        }
        using(MemoryStream ms = new MemoryStream(bytes2)) {
            BinaryFormatter bf = new BinaryFormatter();
            Person person1 = bf.Deserialize(ms) as Person;
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
