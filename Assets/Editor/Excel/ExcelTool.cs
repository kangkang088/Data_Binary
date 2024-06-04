using Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEditor;
using UnityEngine;

public class ExcelTool {
    /// <summary>
    /// Excel文件存放路径
    /// </summary>
    public static string EXCEL_PATH = Application.dataPath + "/ArtRes/Excel/";
    /// <summary>
    /// 数据结构类脚本存放路径
    /// </summary>
    public static string DATACLASS_PATH = Application.dataPath + "/Scripts/ExcelData/DataClass/";
    /// <summary>
    /// 容器类脚本存放路径
    /// </summary>
    public static string DATACONTAINER_PATH = Application.dataPath + "/Scripts/ExcelData/Container/";
    
    /// <summary>
    /// 真正内容的行号
    /// </summary>
    public static int BEGIN_INDEX = 4;

    [MenuItem("GameTool/GenerateExcelInfo")]
    private static void GenerateExcelInfo() {
        //加载指定路径下的所有Excel文件
        DirectoryInfo directoryInfo = Directory.CreateDirectory(EXCEL_PATH);
        //得到指定路径中的所有文件信息
        FileInfo[] fileInfos = directoryInfo.GetFiles();

        //数据表容器
        DataTableCollection result;
        for(int i = 0;i < fileInfos.Length;i++) {
            if(fileInfos[i].Extension != ".xlsx" && fileInfos[i].Extension != ".xls")
                continue;

            //打开Excel文件，得到表数据
            using(FileStream fs = fileInfos[i].Open(FileMode.Open,FileAccess.Read)) {
                IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                result = excelDataReader.AsDataSet().Tables;
                fs.Close();
            }

            //遍历所有表信息
            foreach(DataTable table in result) {
                Debug.Log(table.TableName);
                //生成数据结构类
                GenerateExcelDataClass(table);
                //生成容器类
                GenerateExcelDataContainer(table);
                //生成二进制数据
                GenerateExcelBinaryData(table);
            }
        }
    }
    /// <summary>
    /// 生成Excel表对应的数据结构类
    /// </summary>
    /// <param name="table"></param>
    private static void GenerateExcelDataClass(DataTable table) {
        //字段名行
        DataRow rowName = GetExcelVariableName(table);
        //字段类型行
        DataRow rowType = GetExcelVariableTypeName(table);

        //判断存放数据结构类路径是否存在，不存在就创建
        if(!Directory.Exists(DATACLASS_PATH))
            Directory.CreateDirectory(DATACLASS_PATH);

        //生成数据结构类脚本，其实就是通过代码进行字符串拼接，然后保存进入文件
        string str = "public class " + table.TableName + "\n{\n";

        //变量进行字符串拼接
        for(int i = 0;i < table.Columns.Count;i++) {
            str += "    public " + rowType[i].ToString() + " " + rowName[i].ToString() + ";\n";
        }

        str += "}";

        File.WriteAllText(DATACLASS_PATH + table.TableName + ".cs",str);

        //刷新Project窗口
        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 生成Excel表对应的数据容器类
    /// </summary>
    /// <param name="table"></param>
    private static void GenerateExcelDataContainer(DataTable table) {
        int keyIndex = GetMainKeyIndex(table);
        //得到字段类型行
        DataRow rowType = GetExcelVariableTypeName(table);
        if(!Directory.Exists(DATACONTAINER_PATH))
            Directory.CreateDirectory(DATACONTAINER_PATH);

        string str = "using System.Collections.Generic;\n";
        str += "public class " + table.TableName + "Container" + "\n{\n";

        str += "    public Dictionary<" + rowType[keyIndex].ToString() + "," + table.TableName + "> dataDic = new Dictionary<" + rowType[keyIndex].ToString() + "," + table.TableName + ">();\n";

        str += "}";

        File.WriteAllText(DATACONTAINER_PATH + table.TableName + "Container.cs",str);

        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 生成Excel表二进制数据
    /// </summary>
    /// <param name="table"></param>
    private static void GenerateExcelBinaryData(DataTable table) {
        if(!Directory.Exists(BinaryDataMgr.DATABINARYDATA_PATH))
            Directory.CreateDirectory(BinaryDataMgr.DATABINARYDATA_PATH);

        using(FileStream fs = new FileStream(BinaryDataMgr.DATABINARYDATA_PATH + table.TableName + ".kang",FileMode.OpenOrCreate,FileAccess.Write)) {
            //先要存储我们需要写入多少行的数据，方便我们读取
            fs.Write(BitConverter.GetBytes(table.Rows.Count - 4),0,4);
            //存储主键的变量名
            string keyName = GetExcelVariableName(table)[GetMainKeyIndex(table)].ToString();
            byte[] bytes = Encoding.UTF8.GetBytes(keyName);
            fs.Write(BitConverter.GetBytes(bytes.Length),0,4);
            fs.Write(bytes,0,bytes.Length);

            DataRow dataRow;
            //得到类型行，根据类型决定如何写入数据
            DataRow rowType = GetExcelVariableTypeName(table);
            for(int i = BEGIN_INDEX;i < table.Rows.Count;i++) {
                //得到一行的数据
                dataRow = table.Rows[i];
                for(int j = 0;j < table.Columns.Count;j++) {
                    switch(rowType[j].ToString()) {
                        case "int":
                            fs.Write(BitConverter.GetBytes(int.Parse(dataRow[j].ToString())),0,4);
                            break;
                        case "float":
                            fs.Write(BitConverter.GetBytes(float.Parse(dataRow[j].ToString())),0,4);
                            break;
                        case "bool":
                            fs.Write(BitConverter.GetBytes(bool.Parse(dataRow[j].ToString())),0,1);
                            break;
                        case "string":
                            bytes = Encoding.UTF8.GetBytes(dataRow[j].ToString());
                            fs.Write(BitConverter.GetBytes(bytes.Length),0,4);
                            fs.Write(bytes,0,bytes.Length);
                            break;
                    }
                }
            }
            fs.Close();

            AssetDatabase.Refresh();
        }
    }
    /// <summary>
    /// 得到主键所在的列
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    private static int GetMainKeyIndex(DataTable table) {
        DataRow dataRow = table.Rows[2];
        for(int i = 0;i < table.Columns.Count;i++) {
            if(dataRow[i].ToString() == "key")
                return i;
        }
        return 0;
    }
    /// <summary>
    /// 获取变量名所在行
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    private static DataRow GetExcelVariableName(DataTable table) {
        return table.Rows[0];
    }
    /// <summary>
    /// 获取变量类型所在行
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    private static DataRow GetExcelVariableTypeName(DataTable table) {
        return table.Rows[1];
    }
}
