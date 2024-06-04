using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class BinaryDataMgr {
    private static BinaryDataMgr instance = new BinaryDataMgr();
    public static BinaryDataMgr Instance => instance;
    /// <summary>
    /// 二进制数据存储路径
    /// </summary>
    public static string DATABINARYDATA_PATH = Application.streamingAssetsPath + "/Binary/";
    /// <summary>
    /// 数据存储的位置
    /// </summary>
    private static string SAVE_PATH = Application.persistentDataPath + "/Data/";
    /// <summary>
    /// 用于存储所有Excel表数据的容器
    /// </summary>
    private Dictionary<string,object> tabelDic = new Dictionary<string,object>();
    private BinaryDataMgr() {

    }
    public void InitData() {
        //LoadTable<TowerInfoContainer,TowerInfo>();
        //LoadTable<PlayerInfoContainer,PlayerInfo>();
        //LoadTable<TestInfoContainer,TestInfo>();
    }
    /// <summary>
    /// 加载Excel表的二进制数据到内存中
    /// </summary>
    /// <typeparam name="T">容器类名</typeparam>
    /// <typeparam name="K">数据结构类名</typeparam>
    public void LoadTable<T, K>() {
        //读取Excel表对应的二进制文件
        using(FileStream fs = File.Open(DATABINARYDATA_PATH + typeof(K).Name + ".kang",FileMode.Open,FileAccess.Read)) {
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes,0,bytes.Length);
            fs.Close();
            //用于记录当前读取了多少字节了
            int index = 0;

            //读取多少行数据
            int count = BitConverter.ToInt32(bytes,index);
            index += 4;
            //读取主键的名字
            int keyNameLength = BitConverter.ToInt32(bytes,index);
            index += 4;
            string keyName = Encoding.UTF8.GetString(bytes,index,keyNameLength);
            index += keyNameLength;

            //创建容器类对象
            Type containerType = typeof(T);
            object containerObj = Activator.CreateInstance(containerType);
            //得到数据结构类的Type
            Type classType = typeof(K);
            //得到数据结构类所有字段信息
            FieldInfo[] infos = classType.GetFields();
            //读取每一行的信息
            for(int i = 0;i < count;i++) {
                object dataObj = Activator.CreateInstance(classType);
                foreach(FieldInfo info in infos) {
                    if(info.FieldType == typeof(int)) {
                        info.SetValue(dataObj,BitConverter.ToInt32(bytes,index));
                        index += 4;
                    }
                    else if(info.FieldType == typeof(float)) {
                        info.SetValue(dataObj,BitConverter.ToSingle(bytes,index));
                        index += 4;
                    }
                    else if(info.FieldType == typeof(bool)) {
                        info.SetValue(dataObj,BitConverter.ToBoolean(bytes,index));
                        index += 1;
                    }
                    else if(info.FieldType == typeof(string)) {
                        int length = BitConverter.ToInt32(bytes,index);
                        index += 4;
                        info.SetValue(dataObj,Encoding.UTF8.GetString(bytes,index,length));
                        index += length;
                    }
                }
                //读取完一行数据了，应该把这一行数据添加到字典容器中

                //得到容器对象中的字典对象
                object dicObject = containerType.GetField("dataDic").GetValue(containerObj);
                //通过字典对象得到Add方法
                MethodInfo methodInfo = dicObject.GetType().GetMethod("Add");
                //得到数据结构类对象中，指定主键字段的值
                object keyValue = classType.GetField(keyName).GetValue(dataObj);
                methodInfo.Invoke(dicObject,new object[] { keyValue,dataObj });
            }

            //把读取完成的表记录下来
            tabelDic.Add(typeof(T).Name,containerObj);
        }
    }
    /// <summary>
    /// 得到一张表的信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetTable<T>() where T : class {
        string tableName = typeof(T).Name;
        if(tabelDic.ContainsKey(tableName)) {
            return tabelDic[tableName] as T;
        }
        return null;
    }
    /// <summary>
    /// 存储类对象数据
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="fileName"></param>
    public void Save(object obj,string fileName) {
        if(!Directory.Exists(SAVE_PATH))
            Directory.CreateDirectory(SAVE_PATH);
        using(FileStream fs = new FileStream(SAVE_PATH + fileName + ".txt",FileMode.OpenOrCreate,FileAccess.Write)) {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs,obj);
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }
    }
    /// <summary>
    /// 加载类对象数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public T Load<T>(string fileName) where T : class {
        if(!File.Exists(SAVE_PATH + fileName + ".txt"))
            return default(T);
        T obj;
        using(FileStream fs = new FileStream(SAVE_PATH + fileName + ".txt",FileMode.Open,FileAccess.Read)) {
            BinaryFormatter bf = new BinaryFormatter();
            obj = bf.Deserialize(fs) as T;
            fs.Close();
        }
        return obj;
    }
}
