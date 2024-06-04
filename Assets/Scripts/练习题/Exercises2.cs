using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercises2 : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        //Test t = new Test();
        //t.i = 94;
        //t.str = "kangkang";
        //print(Application.persistentDataPath);
        //BinaryDataMgr.Instance.Save(t,"kangkang");

        Test t = BinaryDataMgr.Instance.Load<Test>("kangkang");
    }

    // Update is called once per frame
    void Update() {

    }
}
[System.Serializable]
public class Test {
    public int i;
    public string str;
}
