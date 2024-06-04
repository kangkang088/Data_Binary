using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Lesson4 : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        //if(Directory.Exists(Application.dataPath + "/Lesson4.txt")) {
        //    print("存在文件夹");
        //}
        //else {
        //    print("不存在文件夹");
        //}
        //DirectoryInfo dirInfo = Directory.CreateDirectory(Application.dataPath + "/Lesson4.txt");
        ////Directory.Delete(Application.dataPath + "/Lesson4.txt",true);

        //string[] strs = Directory.GetDirectories(Application.dataPath);
        //for(int i = 0;i < strs.Length;i++) {
        //    print(strs[i]);
        //}
        //strs = Directory.GetFiles(Application.dataPath);
        //for(int i = 0;i < strs.Length;i++) {
        //    print(strs[i]);
        //}

        //Directory.Move(Application.dataPath + "/Lesson4.txt",Application.dataPath + "/newDirectory");

        DirectoryInfo directoryInfo = Directory.CreateDirectory(Application.dataPath + "/Lesson4.txt");
        //print(directoryInfo.FullName);
        //print(directoryInfo.Name);
        //directoryInfo = Directory.GetParent(Application.dataPath + "/Lesson4.txt");
        //print(directoryInfo.FullName);
        //print(directoryInfo.Name);
        //DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();

        FileInfo[] fileInfos = directoryInfo.GetFiles();
        //for(int i = 0;i < fileInfos.Length;i++) {
        //    print(fileInfos[i].Name);
        //    print(fileInfos[i].FullName);
        //    print(fileInfos[i].Length);
        //    print(fileInfos[i].Extension);
        //}
    }

    // Update is called once per frame
    void Update() {

    }
}
