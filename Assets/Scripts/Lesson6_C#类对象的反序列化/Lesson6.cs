using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Lesson6 : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        using(FileStream fs = new FileStream(Application.dataPath + "/Lesson5_2.txt",FileMode.Open,FileAccess.Read)) {
            BinaryFormatter bf = new BinaryFormatter();
            Person p = bf.Deserialize(fs) as Person;
            fs.Close();
            fs.Dispose();
        }

        byte[] bytes = File.ReadAllBytes(Application.dataPath + "/Lesson5.txt");
        using(MemoryStream ms = new MemoryStream(bytes)) {
            BinaryFormatter bf = new BinaryFormatter();
            Person p = bf.Deserialize(ms) as Person;
            ms.Close();
            ms.Dispose();
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
