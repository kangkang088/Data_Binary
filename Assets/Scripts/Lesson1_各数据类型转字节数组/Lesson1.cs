using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Lesson1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        byte[] bytes = BitConverter.GetBytes(256);
        int i = BitConverter.ToInt32(bytes,0);
        print(i);
        byte[] byte2 = Encoding.UTF8.GetBytes("123123");
        string str = Encoding.UTF8.GetString(byte2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
