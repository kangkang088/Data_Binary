using Excel;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Lesson3ExcelRead
{
    [MenuItem("GameTool/OpenExcel")]
    private static void OpenExcel() {
        using(FileStream fs = File.Open(Application.dataPath + "/ArtRes/Excel/PlayerInfo.xlsx",FileMode.Open,FileAccess.Read)) {
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
            DataSet result = excelDataReader.AsDataSet();
            for(int i = 0;i < result.Tables.Count;i++) {
                Debug.Log("Table Name : " + result.Tables[i].TableName);
                Debug.Log("Rows : " + result.Tables[i].Rows.Count);
                Debug.Log("Columns : " + result.Tables[i].Columns.Count);
            }
            fs.Close();
        }
    }
    [MenuItem("GameTool/ReadExcel")]
    private static void ReadExcel() {
        using(FileStream fs = File.Open(Application.dataPath + "/ArtRes/Excel/PlayerInfo.xlsx",FileMode.Open,FileAccess.Read)) {
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
            DataSet result = excelDataReader.AsDataSet();
            //这里我们已经知道只有一张表了，就不用for了
            DataTable table = result.Tables[0];
            //第一行
            //DataRow dataRow = table.Rows[0];
            //第二列
            //Debug.Log(dataRow[1].ToString());//name

            DataRow dataRow;
            for(int i = 0;i < table.Rows.Count;i++) {
                dataRow = table.Rows[i];
                Debug.Log("****" + i + ".th row");
                for(int j = 0;j < table.Columns.Count;j++) {
                    Debug.Log(dataRow[j].ToString());
                }
            }
        }
    }
}
