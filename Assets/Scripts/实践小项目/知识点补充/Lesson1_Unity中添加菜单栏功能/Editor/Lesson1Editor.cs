using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Lesson1Editor
{
    [MenuItem("GameTool/Test")]
    private static void Test() {
        Debug.Log("Test");
        Directory.CreateDirectory(Application.dataPath + "/TestDirectory");
        AssetDatabase.Refresh();
    }
}
