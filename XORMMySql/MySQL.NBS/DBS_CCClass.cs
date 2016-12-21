using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace MySQL.NetDBS
{
    /// <summary>
    /// Model实体生成类
    /// </summary>
    public class DBS_CCClass
    {
        private string _namespaceHead = string.Empty;
        private string _folderHead = string.Empty;
        private string _tableName = string.Empty;
        private DataTable DT = null;
        private string _ConnectionMark = string.Empty;

        public DBS_CCClass(DataTable dt, string NameSpaceHead, string FolderHead, string TableName, string ConnectionMark)
        {
            this.DT = dt;
            this._namespaceHead = NameSpaceHead;
            this._folderHead = FolderHead;
            this._tableName = TableName;
            this._ConnectionMark = ConnectionMark;
        }
        public string CreateFileContent()
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.AppendLine("using System;");
            stringBuilder1.AppendLine("using System.Data;");
            stringBuilder1.AppendLine("using System.Collections.Generic;");
            stringBuilder1.AppendLine("using Newtonsoft.Json;");
            stringBuilder1.AppendLine("using System.ComponentModel;");
            stringBuilder1.AppendLine("using System.Web.Script.Serialization;");
            stringBuilder1.AppendLine("");
            StringBuilder stringBuilder2 = stringBuilder1;
            string str1 = "namespace ";
            string str2 = this._namespaceHead;
            char[] chArray = new char[1];
            int index1 = 0;
            int num = 46;
            chArray[index1] = (char)num;
            string str3 = str2.TrimEnd(chArray);
            string str4 = str1 + str3;
            stringBuilder2.AppendLine(str4);
            stringBuilder1.AppendLine("{");
            stringBuilder1.AppendLine("\t/// <summary>");
            stringBuilder1.AppendLine("\t/// 数据实体类:" + this._tableName);
            stringBuilder1.AppendLine("\t/// </summary>");
            stringBuilder1.AppendLine("\t[Serializable]");
            stringBuilder1.AppendLine("\tpublic class " + this._tableName + " : XORM.Base.ModelBase");
            stringBuilder1.AppendLine("\t{");
            stringBuilder1.AppendLine("\t\t#region 表名");
            stringBuilder1.AppendLine("\t\tprivate static string @ORM_tablename = \"" + this._tableName + "\";");
            stringBuilder1.AppendLine("\t\t/// <summary>");
            stringBuilder1.AppendLine("\t\t/// 表名");
            stringBuilder1.AppendLine("\t\t/// </summary>");
            stringBuilder1.AppendLine("\t\t[JsonIgnore]");
            stringBuilder1.AppendLine("\t\t[ScriptIgnore]");
            stringBuilder1.AppendLine("\t\tpublic string @ORMTableName { get { return @ORM_tablename; } }");
            stringBuilder1.AppendLine("\t\t#endregion");
            stringBuilder1.AppendLine("");
            stringBuilder1.AppendLine("\t\t#region 实体类型");
            stringBuilder1.AppendLine("\t\tprivate static string @ORM_typeName = \"" + this._namespaceHead + this._tableName + "\";");
            stringBuilder1.AppendLine("\t\t#endregion");
            stringBuilder1.AppendLine("");
            DBS_TypeMap dbsTypeMap = new DBS_TypeMap();
            stringBuilder1.AppendLine("\t\t#region 列名列表");
            StringBuilder stringBuilder3 = new StringBuilder();
            foreach (DataRow dataRow in (InternalDataCollectionBase)this.DT.Rows)
            {
                if (!(dbsTypeMap[dataRow["Type"].ToString()].DBType == "uniqueidentifier") && !(dbsTypeMap[dataRow["Type"].ToString()].DBType == "timestamp"))
                {
                    if (stringBuilder3.Length == 0)
                        stringBuilder3.Append("\"" + dataRow["Field"].ToString().ToUpper() + "\"");
                    else
                        stringBuilder3.Append(", \"" + dataRow["Field"].ToString().ToUpper() + "\"");
                }
            }
            stringBuilder1.AppendLine("\t\tprivate static List<string> @ORM_cols = new List<string>() { " + ((object)stringBuilder3).ToString() + " };");
            stringBuilder1.AppendLine("\t\t/// <summary>");
            stringBuilder1.AppendLine("\t\t/// 列名列表");
            stringBuilder1.AppendLine("\t\t/// </summary>");
            stringBuilder1.AppendLine("\t\t[JsonIgnore]");
            stringBuilder1.AppendLine("\t\t[ScriptIgnore]");
            stringBuilder1.AppendLine("\t\tpublic List<string> @ORMColList { get { return @ORM_cols; } }");
            stringBuilder1.AppendLine("\t\t#endregion");
            stringBuilder1.AppendLine("");
            stringBuilder1.AppendLine("\t\t#region 字段、属性");
            foreach (DataRow dataRow in (InternalDataCollectionBase)this.DT.Rows)
            {
                if (!(dbsTypeMap[dataRow["Type"].ToString()].DBType == "uniqueidentifier") && !(dbsTypeMap[dataRow["Type"].ToString()].DBType == "timestamp"))
                {
                    stringBuilder1.AppendLine("\t\t/// <summary>");
                    stringBuilder1.AppendLine("\t\t/// " + dataRow["Comment"].ToString().Replace("\r", "").Replace("\n", ""));
                    stringBuilder1.AppendLine("\t\t/// </summary>");
                    stringBuilder1.AppendLine("\t\t[Description(\"" + dataRow["Comment"].ToString().Replace("\r", "").Replace("\n", "").Replace("\"", "\\\"") + "\")]");
                    if (dataRow["Field"].ToString().ToUpper() == "ISDEL")
                    {
                        stringBuilder1.AppendLine("\t\t[JsonIgnore]");
                        stringBuilder1.AppendLine("\t\t[ScriptIgnore]");
                    }
                    if (dbsTypeMap.IsKeyWord(dataRow["Field"].ToString()))
                        stringBuilder1.AppendLine("\t\tpublic @" + dbsTypeMap[dataRow["Type"].ToString()].CodeType + " " + dataRow["Field"].ToString());
                    else
                        stringBuilder1.AppendLine("\t\tpublic " + dbsTypeMap[dataRow["Type"].ToString()].CodeType + " " + dataRow["Field"].ToString());
                    stringBuilder1.AppendLine("\t\t{");
                    stringBuilder1.AppendLine("\t\t\tget{ return this._" + dataRow["Field"].ToString() + ";}");
                    StringBuilder stringBuilder4 = stringBuilder1;
                    string[] strArray1 = new string[5];
                    int index2 = 0;
                    string str5 = "\t\t\tset{ this._";
                    strArray1[index2] = str5;
                    int index3 = 1;
                    string str6 = dataRow["Field"].ToString();
                    strArray1[index3] = str6;
                    int index4 = 2;
                    string str7 = " = value; ModifiedColumns.Add(\"[";
                    strArray1[index4] = str7;
                    int index5 = 3;
                    string str8 = dataRow["Field"].ToString();
                    strArray1[index5] = str8;
                    int index6 = 4;
                    string str9 = "]\"); }";
                    strArray1[index6] = str9;
                    string str10 = string.Concat(strArray1);
                    stringBuilder4.AppendLine(str10);
                    stringBuilder1.AppendLine("\t\t}");
                    if (dbsTypeMap[dataRow["Type"].ToString()].CodeType == "string")
                    {
                        if (string.IsNullOrEmpty(dataRow["Default"].ToString()))
                        {
                            StringBuilder stringBuilder5 = stringBuilder1;
                            string[] strArray2 = new string[5];
                            int index7 = 0;
                            string str11 = "\t\tprivate ";
                            strArray2[index7] = str11;
                            int index8 = 1;
                            string codeType = dbsTypeMap[dataRow["Type"].ToString()].CodeType;
                            strArray2[index8] = codeType;
                            int index9 = 2;
                            string str12 = " _";
                            strArray2[index9] = str12;
                            int index10 = 3;
                            string str13 = dataRow["Field"].ToString();
                            strArray2[index10] = str13;
                            int index11 = 4;
                            string str14 = " = \"\";";
                            strArray2[index11] = str14;
                            string str15 = string.Concat(strArray2);
                            stringBuilder5.AppendLine(str15);
                        }
                        else
                        {
                            StringBuilder stringBuilder5 = stringBuilder1;
                            string[] strArray2 = new string[7];
                            int index7 = 0;
                            string str11 = "\t\tprivate ";
                            strArray2[index7] = str11;
                            int index8 = 1;
                            string codeType = dbsTypeMap[dataRow["Type"].ToString()].CodeType;
                            strArray2[index8] = codeType;
                            int index9 = 2;
                            string str12 = " _";
                            strArray2[index9] = str12;
                            int index10 = 3;
                            string str13 = dataRow["Field"].ToString();
                            strArray2[index10] = str13;
                            int index11 = 4;
                            string str14 = " = \"";
                            strArray2[index11] = str14;
                            int index12 = 5;
                            string str15 = dataRow["Default"].ToString().Replace("(", "").Replace(")", "").Replace("'", "");
                            strArray2[index12] = str15;
                            int index13 = 6;
                            string str16 = "\";";
                            strArray2[index13] = str16;
                            string str17 = string.Concat(strArray2);
                            stringBuilder5.AppendLine(str17);
                        }
                    }
                    else if (dbsTypeMap[dataRow["Type"].ToString()].CodeType == "decimal")
                    {
                        if (string.IsNullOrEmpty(dataRow["Default"].ToString()))
                        {
                            StringBuilder stringBuilder5 = stringBuilder1;
                            string[] strArray2 = new string[5];
                            int index7 = 0;
                            string str11 = "\t\tprivate ";
                            strArray2[index7] = str11;
                            int index8 = 1;
                            string codeType = dbsTypeMap[dataRow["Type"].ToString()].CodeType;
                            strArray2[index8] = codeType;
                            int index9 = 2;
                            string str12 = " _";
                            strArray2[index9] = str12;
                            int index10 = 3;
                            string str13 = dataRow["Field"].ToString();
                            strArray2[index10] = str13;
                            int index11 = 4;
                            string str14 = " = 0M;";
                            strArray2[index11] = str14;
                            string str15 = string.Concat(strArray2);
                            stringBuilder5.AppendLine(str15);
                        }
                        else
                        {
                            StringBuilder stringBuilder5 = stringBuilder1;
                            string[] strArray2 = new string[7];
                            int index7 = 0;
                            string str11 = "\t\tprivate ";
                            strArray2[index7] = str11;
                            int index8 = 1;
                            string codeType = dbsTypeMap[dataRow["Type"].ToString()].CodeType;
                            strArray2[index8] = codeType;
                            int index9 = 2;
                            string str12 = " _";
                            strArray2[index9] = str12;
                            int index10 = 3;
                            string str13 = dataRow["Field"].ToString();
                            strArray2[index10] = str13;
                            int index11 = 4;
                            string str14 = " = ";
                            strArray2[index11] = str14;
                            int index12 = 5;
                            string str15 = dataRow["Default"].ToString().Replace("(", "").Replace(")", "").Replace("'", "");
                            strArray2[index12] = str15;
                            int index13 = 6;
                            string str16 = "M;";
                            strArray2[index13] = str16;
                            string str17 = string.Concat(strArray2);
                            stringBuilder5.AppendLine(str17);
                        }
                    }
                    else if (dbsTypeMap[dataRow["Type"].ToString()].CodeType == "double")
                    {
                        if (string.IsNullOrEmpty(dataRow["Default"].ToString()))
                        {
                            StringBuilder stringBuilder5 = stringBuilder1;
                            string[] strArray2 = new string[5];
                            int index7 = 0;
                            string str11 = "\t\tprivate ";
                            strArray2[index7] = str11;
                            int index8 = 1;
                            string codeType = dbsTypeMap[dataRow["Type"].ToString()].CodeType;
                            strArray2[index8] = codeType;
                            int index9 = 2;
                            string str12 = " _";
                            strArray2[index9] = str12;
                            int index10 = 3;
                            string str13 = dataRow["Field"].ToString();
                            strArray2[index10] = str13;
                            int index11 = 4;
                            string str14 = " = 0;";
                            strArray2[index11] = str14;
                            string str15 = string.Concat(strArray2);
                            stringBuilder5.AppendLine(str15);
                        }
                        else
                        {
                            StringBuilder stringBuilder5 = stringBuilder1;
                            string[] strArray2 = new string[7];
                            int index7 = 0;
                            string str11 = "\t\tprivate ";
                            strArray2[index7] = str11;
                            int index8 = 1;
                            string codeType = dbsTypeMap[dataRow["Type"].ToString()].CodeType;
                            strArray2[index8] = codeType;
                            int index9 = 2;
                            string str12 = " _";
                            strArray2[index9] = str12;
                            int index10 = 3;
                            string str13 = dataRow["Field"].ToString();
                            strArray2[index10] = str13;
                            int index11 = 4;
                            string str14 = " = ";
                            strArray2[index11] = str14;
                            int index12 = 5;
                            string str15 = dataRow["Default"].ToString().Replace("(", "").Replace(")", "").Replace("'", "");
                            strArray2[index12] = str15;
                            int index13 = 6;
                            string str16 = ";";
                            strArray2[index13] = str16;
                            string str17 = string.Concat(strArray2);
                            stringBuilder5.AppendLine(str17);
                        }
                    }
                    else if (dbsTypeMap[dataRow["Type"].ToString()].CodeType == "int" || dbsTypeMap[dataRow["Type"].ToString()].CodeType == "Int32")
                    {
                        if (string.IsNullOrEmpty(dataRow["Default"].ToString()))
                        {
                            StringBuilder stringBuilder5 = stringBuilder1;
                            string[] strArray2 = new string[5];
                            int index7 = 0;
                            string str11 = "\t\tprivate ";
                            strArray2[index7] = str11;
                            int index8 = 1;
                            string codeType = dbsTypeMap[dataRow["Type"].ToString()].CodeType;
                            strArray2[index8] = codeType;
                            int index9 = 2;
                            string str12 = " _";
                            strArray2[index9] = str12;
                            int index10 = 3;
                            string str13 = dataRow["Field"].ToString();
                            strArray2[index10] = str13;
                            int index11 = 4;
                            string str14 = " = 0;";
                            strArray2[index11] = str14;
                            string str15 = string.Concat(strArray2);
                            stringBuilder5.AppendLine(str15);
                        }
                        else
                        {
                            StringBuilder stringBuilder5 = stringBuilder1;
                            string[] strArray2 = new string[7];
                            int index7 = 0;
                            string str11 = "\t\tprivate ";
                            strArray2[index7] = str11;
                            int index8 = 1;
                            string codeType = dbsTypeMap[dataRow["Type"].ToString()].CodeType;
                            strArray2[index8] = codeType;
                            int index9 = 2;
                            string str12 = " _";
                            strArray2[index9] = str12;
                            int index10 = 3;
                            string str13 = dataRow["Field"].ToString();
                            strArray2[index10] = str13;
                            int index11 = 4;
                            string str14 = " = ";
                            strArray2[index11] = str14;
                            int index12 = 5;
                            string str15 = dataRow["Default"].ToString().Replace("(", "").Replace(")", "").Replace("'", "");
                            strArray2[index12] = str15;
                            int index13 = 6;
                            string str16 = ";";
                            strArray2[index13] = str16;
                            string str17 = string.Concat(strArray2);
                            stringBuilder5.AppendLine(str17);
                        }
                    }
                    else if (dbsTypeMap[dataRow["Type"].ToString()].CodeType == "Int64")
                    {
                        if (string.IsNullOrEmpty(dataRow["Default"].ToString()))
                        {
                            StringBuilder stringBuilder5 = stringBuilder1;
                            string[] strArray2 = new string[5];
                            int index7 = 0;
                            string str11 = "\t\tprivate ";
                            strArray2[index7] = str11;
                            int index8 = 1;
                            string codeType = dbsTypeMap[dataRow["Type"].ToString()].CodeType;
                            strArray2[index8] = codeType;
                            int index9 = 2;
                            string str12 = " _";
                            strArray2[index9] = str12;
                            int index10 = 3;
                            string str13 = dataRow["Field"].ToString();
                            strArray2[index10] = str13;
                            int index11 = 4;
                            string str14 = " = 0L;";
                            strArray2[index11] = str14;
                            string str15 = string.Concat(strArray2);
                            stringBuilder5.AppendLine(str15);
                        }
                        else
                        {
                            StringBuilder stringBuilder5 = stringBuilder1;
                            string[] strArray2 = new string[7];
                            int index7 = 0;
                            string str11 = "\t\tprivate ";
                            strArray2[index7] = str11;
                            int index8 = 1;
                            string codeType = dbsTypeMap[dataRow["Type"].ToString()].CodeType;
                            strArray2[index8] = codeType;
                            int index9 = 2;
                            string str12 = " _";
                            strArray2[index9] = str12;
                            int index10 = 3;
                            string str13 = dataRow["Field"].ToString();
                            strArray2[index10] = str13;
                            int index11 = 4;
                            string str14 = " = ";
                            strArray2[index11] = str14;
                            int index12 = 5;
                            string str15 = dataRow["Default"].ToString().Replace("(", "").Replace(")", "").Replace("'", "");
                            strArray2[index12] = str15;
                            int index13 = 6;
                            string str16 = "L;";
                            strArray2[index13] = str16;
                            string str17 = string.Concat(strArray2);
                            stringBuilder5.AppendLine(str17);
                        }
                    }
                    else if (dbsTypeMap[dataRow["Type"].ToString()].CodeType == "DateTime")
                    {
                        if (string.IsNullOrEmpty(dataRow["Default"].ToString()))
                        {
                            StringBuilder stringBuilder5 = stringBuilder1;
                            string[] strArray2 = new string[5];
                            int index7 = 0;
                            string str11 = "\t\tprivate ";
                            strArray2[index7] = str11;
                            int index8 = 1;
                            string codeType = dbsTypeMap[dataRow["Type"].ToString()].CodeType;
                            strArray2[index8] = codeType;
                            int index9 = 2;
                            string str12 = " _";
                            strArray2[index9] = str12;
                            int index10 = 3;
                            string str13 = dataRow["Field"].ToString();
                            strArray2[index10] = str13;
                            int index11 = 4;
                            string str14 = ";";
                            strArray2[index11] = str14;
                            string str15 = string.Concat(strArray2);
                            stringBuilder5.AppendLine(str15);
                        }
                        else if (dataRow["Default"].ToString().ToUpper().Contains("CURRENT_TIMESTAMP"))
                        {
                            StringBuilder stringBuilder5 = stringBuilder1;
                            string[] strArray2 = new string[5];
                            int index7 = 0;
                            string str11 = "\t\tprivate ";
                            strArray2[index7] = str11;
                            int index8 = 1;
                            string codeType = dbsTypeMap[dataRow["Type"].ToString()].CodeType;
                            strArray2[index8] = codeType;
                            int index9 = 2;
                            string str12 = " _";
                            strArray2[index9] = str12;
                            int index10 = 3;
                            string str13 = dataRow["Field"].ToString();
                            strArray2[index10] = str13;
                            int index11 = 4;
                            string str14 = " = DateTime.Now;";
                            strArray2[index11] = str14;
                            string str15 = string.Concat(strArray2);
                            stringBuilder5.AppendLine(str15);
                        }
                        else
                        {
                            StringBuilder stringBuilder5 = stringBuilder1;
                            string[] strArray2 = new string[7];
                            int index7 = 0;
                            string str11 = "\t\tprivate ";
                            strArray2[index7] = str11;
                            int index8 = 1;
                            string codeType = dbsTypeMap[dataRow["Type"].ToString()].CodeType;
                            strArray2[index8] = codeType;
                            int index9 = 2;
                            string str12 = " _";
                            strArray2[index9] = str12;
                            int index10 = 3;
                            string str13 = dataRow["Field"].ToString();
                            strArray2[index10] = str13;
                            int index11 = 4;
                            string str14 = " = DateTime.Parse(\"";
                            strArray2[index11] = str14;
                            int index12 = 5;
                            string str15 = dataRow["Default"].ToString().Replace("(", "").Replace(")", "").Replace("'", "");
                            strArray2[index12] = str15;
                            int index13 = 6;
                            string str16 = "\");";
                            strArray2[index13] = str16;
                            string str17 = string.Concat(strArray2);
                            stringBuilder5.AppendLine(str17);
                        }
                    }
                    else
                    {
                        StringBuilder stringBuilder5 = stringBuilder1;
                        string[] strArray2 = new string[5];
                        int index7 = 0;
                        string str11 = "\t\tprivate ";
                        strArray2[index7] = str11;
                        int index8 = 1;
                        string codeType = dbsTypeMap[dataRow["Type"].ToString()].CodeType;
                        strArray2[index8] = codeType;
                        int index9 = 2;
                        string str12 = " _";
                        strArray2[index9] = str12;
                        int index10 = 3;
                        string str13 = dataRow["Field"].ToString();
                        strArray2[index10] = str13;
                        int index11 = 4;
                        string str14 = ";";
                        strArray2[index11] = str14;
                        string str15 = string.Concat(strArray2);
                        stringBuilder5.AppendLine(str15);
                    }
                }
            }
            stringBuilder1.AppendLine("\t\t#endregion");
            stringBuilder1.AppendLine("");
            stringBuilder1.AppendLine("\t\t#region 构造函数");
            stringBuilder1.AppendLine("\t\t/// <summary>");
            stringBuilder1.AppendLine("\t\t/// 构造函数");
            stringBuilder1.AppendLine("\t\t/// </summary>");
            stringBuilder1.AppendLine("\t\tpublic " + this._tableName + "()");
            stringBuilder1.AppendLine("\t\t{");
            stringBuilder1.AppendLine("\t\t\t@ORM_ConnectionMark = \"" + this._ConnectionMark + "\";");
            stringBuilder1.AppendLine("\t\t\t@ORM_tabinfo = @ORM_TableDefinition;");
            stringBuilder1.AppendLine("\t\t\tbase.Init();");
            stringBuilder1.AppendLine("\t\t}");
            stringBuilder1.AppendLine("\t\t[JsonIgnore]");
            stringBuilder1.AppendLine("\t\t[ScriptIgnore]");
            stringBuilder1.AppendLine("\t\tpublic static XORM.Base.TableDefinition @ORM_TableDefinition = new XORM.Base.TableDefinition()");
            stringBuilder1.AppendLine("\t\t{");
            stringBuilder1.AppendLine("\t\t\t@ORMTableName = @ORM_tablename,");
            stringBuilder1.AppendLine("\t\t\t@ORMColList = @ORM_cols,");
            stringBuilder1.AppendLine("\t\t\t@ORMTypeName = @ORM_typeName,");
            stringBuilder1.AppendLine("\t\t\t@ORMAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().Location");
            stringBuilder1.AppendLine("\t\t};");
            stringBuilder1.AppendLine("\t\t/// <summary>");
            stringBuilder1.AppendLine("\t\t/// 构造函数,初始化一行数据");
            stringBuilder1.AppendLine("\t\t/// </summary>");
            stringBuilder1.AppendLine("\t\tpublic " + this._tableName + "(DataRow dr) : this()");
            stringBuilder1.AppendLine("\t\t{");
            foreach (DataRow dataRow in (InternalDataCollectionBase)this.DT.Rows)
            {
                if (!(dbsTypeMap[dataRow["Type"].ToString()].DBType == "uniqueidentifier") && !(dbsTypeMap[dataRow["Type"].ToString()].DBType == "timestamp"))
                {
                    StringBuilder stringBuilder4 = stringBuilder1;
                    string[] strArray1 = new string[5];
                    int index2 = 0;
                    string str5 = "\t\t\tif (dr != null && dr.Table.Columns.Contains(\"";
                    strArray1[index2] = str5;
                    int index3 = 1;
                    string str6 = dataRow["Field"].ToString();
                    strArray1[index3] = str6;
                    int index4 = 2;
                    string str7 = "\") && !(dr[\"";
                    strArray1[index4] = str7;
                    int index5 = 3;
                    string str8 = dataRow["Field"].ToString();
                    strArray1[index5] = str8;
                    int index6 = 4;
                    string str9 = "\"] is DBNull))";
                    strArray1[index6] = str9;
                    string str10 = string.Concat(strArray1);
                    stringBuilder4.AppendLine(str10);
                    stringBuilder1.AppendLine("\t\t\t{");
                    StringBuilder stringBuilder5 = stringBuilder1;
                    string[] strArray2 = new string[7];
                    int index7 = 0;
                    string str11 = "\t\t\t\tthis._";
                    strArray2[index7] = str11;
                    int index8 = 1;
                    string str12 = dataRow["Field"].ToString();
                    strArray2[index8] = str12;
                    int index9 = 2;
                    string str13 = " = ";
                    strArray2[index9] = str13;
                    int index10 = 3;
                    string convertType = dbsTypeMap[dataRow["Type"].ToString()].ConvertType;
                    strArray2[index10] = convertType;
                    int index11 = 4;
                    string str14 = "(dr[\"";
                    strArray2[index11] = str14;
                    int index12 = 5;
                    string str15 = dataRow["Field"].ToString();
                    strArray2[index12] = str15;
                    int index13 = 6;
                    string str16 = "\"]);";
                    strArray2[index13] = str16;
                    string str17 = string.Concat(strArray2);
                    stringBuilder5.AppendLine(str17);
                    stringBuilder1.AppendLine("\t\t\t}");
                }
            }
            stringBuilder1.AppendLine("\t\t}");
            stringBuilder1.AppendLine("\t\t#endregion");
            stringBuilder1.AppendLine("");
            stringBuilder1.AppendLine("\t\t#region 被修改字段序列");
            stringBuilder1.AppendLine("\t\t[JsonIgnore]");
            stringBuilder1.AppendLine("\t\t[ScriptIgnore]");
            stringBuilder1.AppendLine("\t\tpublic HashSet<string> ModifiedColumns = new HashSet<string>();");
            stringBuilder1.AppendLine("\t\t#endregion");
            stringBuilder1.AppendLine("");
            stringBuilder1.AppendLine("\t}");
            stringBuilder1.Append("}");
            return ((object)stringBuilder1).ToString();
        }
    }
}