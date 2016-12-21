// Type: XORM.Base.ModelBase
// Assembly: XORM.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Users\Administrator\Desktop\mysql代码生成器\XORM.Base.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using XORM.Db;

namespace XORM.Base
{
    [Serializable]
    public class ModelBase
    {
        private static Dictionary<string, Type> TabTypeDic = new Dictionary<string, Type>();
        private static Dictionary<string, Assembly> AsmDic = new Dictionary<string, Assembly>();
        private StringBuilder OutCols = new StringBuilder();
        private StringBuilder PageCols = new StringBuilder();
        private StringBuilder JoinTxt = new StringBuilder();
        private Dictionary<string, string> TabNameDic = new Dictionary<string, string>();
        private Dictionary<string, List<string>> TabColDic = new Dictionary<string, List<string>>();
        private Dictionary<string, string> TabClassDic = new Dictionary<string, string>();
        private List<string> OutTabList = new List<string>();
        private Dictionary<string, string> TabAssemblyDic = new Dictionary<string, string>();
        [ScriptIgnore]
        [NonSerialized]
        protected string ORM_ConnectionMark = string.Empty;
        [ScriptIgnore]
        [NonSerialized]
        protected TableDefinition ORM_tabinfo = new TableDefinition();
        private SqlCommand MyCmd = new SqlCommand();
        private string SingleSql = "";
        private string PageSort = "";
        private bool OutDistinct = false;

        [JsonIgnore]
        [ScriptIgnore]
        public string SQLTEXT
        {
            get
            {
                StringBuilder stringBuilder1 = new StringBuilder();
                if (!this.OutDistinct)
                {
                    StringBuilder stringBuilder2 = stringBuilder1.Append("SELECT ");
                    string str1 = ((object)this.OutCols).ToString();
                    char[] chArray = new char[1];
                    int index = 0;
                    int num = 44;
                    chArray[index] = (char)num;
                    string str2 = str1.TrimEnd(chArray);
                    stringBuilder2.Append(str2).Append(" FROM ").Append(((object)this.JoinTxt).ToString());
                }
                else
                {
                    StringBuilder stringBuilder2 = stringBuilder1.Append("SELECT DISTINCT ");
                    string str1 = ((object)this.OutCols).ToString();
                    char[] chArray = new char[1];
                    int index = 0;
                    int num = 44;
                    chArray[index] = (char)num;
                    string str2 = str1.TrimEnd(chArray);
                    stringBuilder2.Append(str2).Append(" FROM ").Append(((object)this.JoinTxt).ToString());
                }
                return ((object)stringBuilder1).ToString();
            }
        }

        [JsonIgnore]
        [ScriptIgnore]
        public string SQLTEXT_COUNT
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("SELECT COUNT(1) FROM ").Append(((object)this.JoinTxt).ToString());
                return ((object)stringBuilder).ToString();
            }
        }

        [JsonIgnore]
        [ScriptIgnore]
        public string SQLTEXT_SINGLE
        {
            get
            {
                StringBuilder stringBuilder1 = new StringBuilder();
                StringBuilder stringBuilder2 = stringBuilder1.Append("SELECT ");
                string str1 = this.SingleSql;
                char[] chArray = new char[1];
                int index = 0;
                int num = 44;
                chArray[index] = (char)num;
                string str2 = str1.TrimEnd(chArray);
                stringBuilder2.Append(str2).Append(" FROM ").Append(((object)this.JoinTxt).ToString());
                return ((object)stringBuilder1).ToString();
            }
        }

        [JsonIgnore]
        [ScriptIgnore]
        public string SQLTEXT_PAGE
        {
            get
            {
                StringBuilder stringBuilder1 = new StringBuilder();
                StringBuilder stringBuilder2 = stringBuilder1.Append("WITH PST(RN,");
                string str1 = ((object)this.PageCols).ToString();
                char[] chArray1 = new char[1];
                int index1 = 0;
                int num1 = 44;
                chArray1[index1] = (char)num1;
                string str2 = str1.TrimEnd(chArray1);
                stringBuilder2.Append(str2).Append(")AS(");
                StringBuilder stringBuilder3 = stringBuilder1.Append("SELECT ROW_NUMBER() OVER(ORDER BY ").Append(this.PageSort).Append(") RN,");
                string str3 = ((object)this.OutCols).ToString();
                char[] chArray2 = new char[1];
                int index2 = 0;
                int num2 = 44;
                chArray2[index2] = (char)num2;
                string str4 = str3.TrimEnd(chArray2);
                stringBuilder3.Append(str4).Append(" FROM ").Append(((object)this.JoinTxt).ToString()).Append(")");
                StringBuilder stringBuilder4 = stringBuilder1.Append("SELECT RN,");
                string str5 = ((object)this.PageCols).ToString();
                char[] chArray3 = new char[1];
                int index3 = 0;
                int num3 = 44;
                chArray3[index3] = (char)num3;
                string str6 = str5.TrimEnd(chArray3);
                stringBuilder4.Append(str6).Append(" FROM PST WHERE RN BETWEEN @SI AND @EI;");
                stringBuilder1.Append("SELECT COUNT(1) FROM ").Append(((object)this.JoinTxt).ToString());
                return ((object)stringBuilder1).ToString();
            }
        }

        static ModelBase()
        {
        }

        protected void Init()
        {
            this.JoinTxt.Append("[").Append(this.ORM_tabinfo.ORMTableName).Append("] AS SRCTAB ");
            this.TabNameDic.Add("SRCTAB", this.ORM_tabinfo.ORMTableName);
            this.TabColDic.Add("SRCTAB", this.ORM_tabinfo.ORMColList);
            this.TabClassDic.Add("SRCTAB", this.ORM_tabinfo.ORMTypeName);
            this.TabAssemblyDic.Add("SRCTAB", this.ORM_tabinfo.ORMAssemblyName);
            foreach (string str in this.ORM_tabinfo.ORMColList)
            {
                if (this.OutCols.Length == 0)
                    this.OutCols.Append("SRCTAB.").Append("[").Append(str.ToUpper()).Append("] AS SRCTAB_").Append(str);
                else
                    this.OutCols.Append(",").Append("SRCTAB.").Append("[").Append(str.ToUpper()).Append("] AS SRCTAB_").Append(str);
                if (this.PageCols.Length == 0)
                    this.PageCols.Append("SRCTAB_").Append(str);
                else
                    this.PageCols.Append(",SRCTAB_").Append(str);
            }
        }

        private void AddOutCols(List<string> JoinTableColList, string SName)
        {
            foreach (string str in JoinTableColList)
            {
                this.OutCols.Append(",").Append(SName).Append(".[").Append(str).Append("]").Append(" AS ").Append(SName).Append("_").Append(str);
                this.PageCols.Append(",").Append(SName).Append("_").Append(str);
            }
        }

        private void CheckJoinCols(string JoinCondition, out string SafeCondition)
        {
            StringBuilder stringBuilder = new StringBuilder(JoinCondition);
            stringBuilder.Replace("[", "").Replace("]", "");
            MatchCollection matchCollection = new Regex("([a-zA-Z_]{1,30}[0-9a-zA-Z_]{0,30})\\.[\\[]?([a-zA-Z_]{1,30}[0-9a-zA-Z_]{0,30})[\\]]?", RegexOptions.IgnoreCase).Matches(JoinCondition);
            if (matchCollection != null && matchCollection.Count > 0)
            {
                foreach (Match match in matchCollection)
                {
                    string key = match.Groups[1].Value;
                    string str1 = match.Groups[2].Value;
                    if (!this.TabNameDic.ContainsKey(key))
                        throw new Exception(key + "表不存在");
                    if (!this.TabColDic[key].Contains(str1.ToUpper()))
                    {
                        string[] strArray = new string[6];
                        int index1 = 0;
                        string str2 = key;
                        strArray[index1] = str2;
                        int index2 = 1;
                        string str3 = ":[";
                        strArray[index2] = str3;
                        int index3 = 2;
                        string str4 = this.TabNameDic[key];
                        strArray[index3] = str4;
                        int index4 = 3;
                        string str5 = "]";
                        strArray[index4] = str5;
                        int index5 = 4;
                        string str6 = "表不存在字段";
                        strArray[index5] = str6;
                        int index6 = 5;
                        string str7 = str1;
                        strArray[index6] = str7;
                        throw new Exception(string.Concat(strArray));
                    }
                    else
                    {
                        stringBuilder = stringBuilder.Replace(key + "." + str1, key + ".[" + str1 + "]");
                        if (!this.OutTabList.Contains(key))
                            this.OutTabList.Add(key);
                    }
                }
                SafeCondition = ((object)stringBuilder).ToString();
            }
            else
                SafeCondition = JoinCondition;
        }

        private void CheckOutCols(string OutColumns)
        {
            if (OutColumns.TrimStart(new char[0]).ToUpper().StartsWith("DISTINCT "))
            {
                this.OutDistinct = true;
                OutColumns = OutColumns.Substring("DISTINCT ".Length);
            }
            MatchCollection matchCollection = new Regex("([a-zA-Z_]{1,30}[0-9a-zA-Z_]{0,30})\\.[\\[]?([a-zA-Z_]{1,30}[0-9a-zA-Z_]{0,30}|[\\*]?)[\\]]?", RegexOptions.IgnoreCase).Matches(OutColumns);
            StringBuilder stringBuilder1 = new StringBuilder();
            if (matchCollection == null || matchCollection.Count <= 0)
                return;
            foreach (Match match in matchCollection)
            {
                string key = match.Groups[1].Value;
                string str1 = match.Groups[2].Value;
                if (!this.TabNameDic.ContainsKey(key))
                    throw new Exception(key + "表不存在");
                if (str1 == "*")
                {
                    foreach (string str2 in this.TabColDic[key])
                    {
                        if (stringBuilder1.Length == 0)
                        {
                            StringBuilder stringBuilder2 = stringBuilder1;
                            string[] strArray = new string[7];
                            int index1 = 0;
                            string str3 = key;
                            strArray[index1] = str3;
                            int index2 = 1;
                            string str4 = ".";
                            strArray[index2] = str4;
                            int index3 = 2;
                            string str5 = str2;
                            strArray[index3] = str5;
                            int index4 = 3;
                            string str6 = " AS ";
                            strArray[index4] = str6;
                            int index5 = 4;
                            string str7 = key;
                            strArray[index5] = str7;
                            int index6 = 5;
                            string str8 = "_";
                            strArray[index6] = str8;
                            int index7 = 6;
                            string str9 = str2;
                            strArray[index7] = str9;
                            string str10 = string.Concat(strArray);
                            stringBuilder2.Append(str10);
                        }
                        else
                        {
                            StringBuilder stringBuilder2 = stringBuilder1.Append(",");
                            string[] strArray = new string[7];
                            int index1 = 0;
                            string str3 = key;
                            strArray[index1] = str3;
                            int index2 = 1;
                            string str4 = ".";
                            strArray[index2] = str4;
                            int index3 = 2;
                            string str5 = str2;
                            strArray[index3] = str5;
                            int index4 = 3;
                            string str6 = " AS ";
                            strArray[index4] = str6;
                            int index5 = 4;
                            string str7 = key;
                            strArray[index5] = str7;
                            int index6 = 5;
                            string str8 = "_";
                            strArray[index6] = str8;
                            int index7 = 6;
                            string str9 = str2;
                            strArray[index7] = str9;
                            string str10 = string.Concat(strArray);
                            stringBuilder2.Append(str10);
                        }
                    }
                }
                else if (!this.TabColDic[key].Contains(str1))
                {
                    string[] strArray = new string[6];
                    int index1 = 0;
                    string str2 = key;
                    strArray[index1] = str2;
                    int index2 = 1;
                    string str3 = ":[";
                    strArray[index2] = str3;
                    int index3 = 2;
                    string str4 = this.TabNameDic[key];
                    strArray[index3] = str4;
                    int index4 = 3;
                    string str5 = "]";
                    strArray[index4] = str5;
                    int index5 = 4;
                    string str6 = "表不存在字段";
                    strArray[index5] = str6;
                    int index6 = 5;
                    string str7 = str1;
                    strArray[index6] = str7;
                    throw new Exception(string.Concat(strArray));
                }
                else if (stringBuilder1.Length == 0)
                {
                    StringBuilder stringBuilder2 = stringBuilder1;
                    string[] strArray = new string[7];
                    int index1 = 0;
                    string str2 = key;
                    strArray[index1] = str2;
                    int index2 = 1;
                    string str3 = ".";
                    strArray[index2] = str3;
                    int index3 = 2;
                    string str4 = str1;
                    strArray[index3] = str4;
                    int index4 = 3;
                    string str5 = " AS ";
                    strArray[index4] = str5;
                    int index5 = 4;
                    string str6 = key;
                    strArray[index5] = str6;
                    int index6 = 5;
                    string str7 = "_";
                    strArray[index6] = str7;
                    int index7 = 6;
                    string str8 = str1;
                    strArray[index7] = str8;
                    string str9 = string.Concat(strArray);
                    stringBuilder2.Append(str9);
                }
                else
                {
                    StringBuilder stringBuilder2 = stringBuilder1.Append(",");
                    string[] strArray = new string[7];
                    int index1 = 0;
                    string str2 = key;
                    strArray[index1] = str2;
                    int index2 = 1;
                    string str3 = ".";
                    strArray[index2] = str3;
                    int index3 = 2;
                    string str4 = str1;
                    strArray[index3] = str4;
                    int index4 = 3;
                    string str5 = " AS ";
                    strArray[index4] = str5;
                    int index5 = 4;
                    string str6 = key;
                    strArray[index5] = str6;
                    int index6 = 5;
                    string str7 = "_";
                    strArray[index6] = str7;
                    int index7 = 6;
                    string str8 = str1;
                    strArray[index7] = str8;
                    string str9 = string.Concat(strArray);
                    stringBuilder2.Append(str9);
                }
                if (!this.OutTabList.Contains(key))
                    this.OutTabList.Add(key);
            }
            this.OutCols.Clear();
            this.OutCols.Append(((object)stringBuilder1).ToString());
        }

        public ModelBase INNER_JOIN(TableDefinition tab, string SName, string JoinCondition, object[] ParamsList = null)
        {
            this.TabNameDic.Add(SName, tab.ORMTableName);
            this.TabColDic.Add(SName, tab.ORMColList);
            this.TabClassDic.Add(SName, tab.ORMTypeName);
            this.TabAssemblyDic.Add(SName, tab.ORMAssemblyName);
            this.JoinTxt.Append(" INNER JOIN ").Append("[").Append(tab.ORMTableName).Append("]").Append(" AS ").Append(SName);
            this.AddOutCols(tab.ORMColList, SName);
            string SafeCondition = "";
            this.CheckJoinCols(JoinCondition, out SafeCondition);
            this.JoinTxt.Append(" ON ").Append(SafeCondition);
            this.BuildCommand(" ON " + SafeCondition, ParamsList);
            return this;
        }

        public ModelBase LEFT_JOIN(TableDefinition tab, string SName, string JoinCondition, object[] ParamsList = null)
        {
            this.TabNameDic.Add(SName, tab.ORMTableName);
            this.TabColDic.Add(SName, tab.ORMColList);
            this.TabClassDic.Add(SName, tab.ORMTypeName);
            this.TabAssemblyDic.Add(SName, tab.ORMAssemblyName);
            this.JoinTxt.Append(" LEFT OUTER JOIN ").Append("[").Append(tab.ORMTableName).Append("]").Append(" AS ").Append(SName);
            this.AddOutCols(tab.ORMColList, SName);
            string SafeCondition = "";
            this.CheckJoinCols(JoinCondition, out SafeCondition);
            this.JoinTxt.Append(" ON ").Append(SafeCondition);
            this.BuildCommand(" ON " + SafeCondition, ParamsList);
            return this;
        }

        public ModelBase RIGHT_JOIN(TableDefinition tab, string SName, string JoinCondition, object[] ParamsList = null)
        {
            this.TabNameDic.Add(SName, tab.ORMTableName);
            this.TabColDic.Add(SName, tab.ORMColList);
            this.TabClassDic.Add(SName, tab.ORMTypeName);
            this.TabAssemblyDic.Add(SName, tab.ORMAssemblyName);
            this.JoinTxt.Append(" RIGHT OUTER JOIN ").Append("[").Append(tab.ORMTableName).Append("]").Append(" AS ").Append(SName);
            this.AddOutCols(tab.ORMColList, SName);
            string SafeCondition = "";
            this.CheckJoinCols(JoinCondition, out SafeCondition);
            this.JoinTxt.Append(" ON ").Append(SafeCondition);
            this.BuildCommand(" ON " + SafeCondition, ParamsList);
            return this;
        }

        public ModelBase Where(string Condition, object[] ParamsList = null)
        {
            string SafeCondition = "";
            this.CheckJoinCols(Condition, out SafeCondition);
            this.JoinTxt.Append(" WHERE ").Append(SafeCondition);
            this.BuildCommand(SafeCondition, ParamsList);
            return this;
        }

        public ModelBase OUT(string OutColumns)
        {
            this.CheckOutCols(OutColumns);
            return this;
        }

        public List<QueryResult> List(bool UseReadOnlyDataSource = true)
        {
            this.MyCmd.CommandText = this.SQLTEXT;
            DataTable dataTable1 = new DBHelper(this.ORM_ConnectionMark, UseReadOnlyDataSource).ExecDataTable((DbCommand)this.MyCmd);
            List<QueryResult> list = new List<QueryResult>();
            if (dataTable1 != null && dataTable1.Rows.Count > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                stopwatch.Stop();
                foreach (DataRow dataRow1 in (InternalDataCollectionBase)dataTable1.Rows)
                {
                    QueryResult queryResult = new QueryResult();
                    foreach (string index1 in this.OutTabList)
                    {
                        stopwatch.Restart();
                        Type tabType = this.GetTabType(this.TabClassDic[index1], this.TabAssemblyDic[index1]);
                        stopwatch.Stop();
                        stringBuilder.AppendLine("Asm.GetType\t" + stopwatch.ElapsedTicks.ToString());
                        DataTable dataTable2 = new DataTable();
                        foreach (DataColumn dataColumn in (InternalDataCollectionBase)dataTable1.Columns)
                        {
                            if (dataColumn.ColumnName.StartsWith(index1 + "_"))
                                dataTable2.Columns.Add(dataColumn.ColumnName.Substring((index1 + "_").Length));
                        }
                        DataRow row = dataTable2.NewRow();
                        foreach (DataColumn dataColumn in (InternalDataCollectionBase)dataTable2.Columns)
                            row[dataColumn.ColumnName] = dataRow1[index1 + "_" + dataColumn.ColumnName];
                        dataTable2.Rows.Add(row);
                        stopwatch.Restart();
                        Type type = tabType;
                        object[] objArray = new object[1];
                        int index2 = 0;
                        DataRow dataRow2 = row;
                        objArray[index2] = (object)dataRow2;
                        object instance = Activator.CreateInstance(type, objArray);
                        stopwatch.Stop();
                        stringBuilder.AppendLine("Activator.CreateInstance\t" + stopwatch.ElapsedTicks.ToString());
                        queryResult.RowResultList.Add(this.TabNameDic[index1] + "." + index1, instance);
                        queryResult.Tables.Add(this.TabNameDic[index1] + "." + index1);
                    }
                    list.Add(queryResult);
                }
                using (StreamWriter text = File.CreateText("C:/" + Guid.NewGuid().ToString() + ".txt"))
                    text.WriteLine(((object)stringBuilder).ToString());
                Console.WriteLine(((object)stringBuilder).ToString());
            }
            return list;
        }

        public List<QueryResult> List(string sqlSort, bool UseReadOnlyDataSource = true)
        {
            this.MyCmd.CommandText = this.SQLTEXT + " ORDER BY " + sqlSort;
            DataTable dataTable1 = new DBHelper(this.ORM_ConnectionMark, UseReadOnlyDataSource).ExecDataTable((DbCommand)this.MyCmd);
            List<QueryResult> list = new List<QueryResult>();
            if (dataTable1 != null && dataTable1.Rows.Count > 0)
            {
                foreach (DataRow dataRow1 in (InternalDataCollectionBase)dataTable1.Rows)
                {
                    QueryResult queryResult = new QueryResult();
                    foreach (string index1 in this.OutTabList)
                    {
                        Type tabType = this.GetTabType(this.TabClassDic[index1], this.TabAssemblyDic[index1]);
                        DataTable dataTable2 = new DataTable();
                        foreach (DataColumn dataColumn in (InternalDataCollectionBase)dataTable1.Columns)
                        {
                            if (dataColumn.ColumnName.StartsWith(index1 + "_"))
                                dataTable2.Columns.Add(dataColumn.ColumnName.Substring((index1 + "_").Length));
                        }
                        DataRow row = dataTable2.NewRow();
                        foreach (DataColumn dataColumn in (InternalDataCollectionBase)dataTable2.Columns)
                            row[dataColumn.ColumnName] = dataRow1[index1 + "_" + dataColumn.ColumnName];
                        dataTable2.Rows.Add(row);
                        Type type = tabType;
                        object[] objArray = new object[1];
                        int index2 = 0;
                        DataRow dataRow2 = row;
                        objArray[index2] = (object)dataRow2;
                        object instance = Activator.CreateInstance(type, objArray);
                        queryResult.RowResultList.Add(this.TabNameDic[index1] + "." + index1, instance);
                        queryResult.Tables.Add(this.TabNameDic[index1] + "." + index1);
                    }
                    list.Add(queryResult);
                }
            }
            return list;
        }

        public long Count(bool UseReadOnlyDataSource = true)
        {
            this.MyCmd.CommandText = this.SQLTEXT_COUNT;
            object obj = new DBHelper(this.ORM_ConnectionMark, UseReadOnlyDataSource).ExecScalar((DbCommand)this.MyCmd);
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                return -1L;
            long result = 0L;
            long.TryParse(obj.ToString(), out result);
            return result;
        }

        public DataTable Function(string FunctionList, bool UseReadOnlyDataSource = true)
        {
            this.SingleSql = FunctionList;
            this.MyCmd.CommandText = this.SQLTEXT_SINGLE;
            return new DBHelper(this.ORM_ConnectionMark, UseReadOnlyDataSource).ExecDataTable((DbCommand)this.MyCmd);
        }

        public List<QueryResult> List(string sqlSort, out long RecordCount, int PageIndex = 1, int PageSize = 20, bool UseReadOnlyDataSource = true)
        {
            RecordCount = 0L;
            this.PageSort = sqlSort;
            this.MyCmd.CommandText = this.SQLTEXT_PAGE;
            this.MyCmd.Parameters.Add("@SI", SqlDbType.Int).Value = (object)(PageIndex * PageSize - PageSize + 1);
            this.MyCmd.Parameters.Add("@EI", SqlDbType.Int).Value = (object)(PageIndex * PageSize);
            DataSet dataSet = new DBHelper(this.ORM_ConnectionMark, UseReadOnlyDataSource).ExecDataSet((DbCommand)this.MyCmd);
            List<QueryResult> list = new List<QueryResult>();
            if (dataSet != null && dataSet.Tables.Count == 2)
            {
                DataTable dataTable1 = dataSet.Tables[0];
                try
                {
                    long.TryParse(dataSet.Tables[1].Rows[0][0].ToString(), out RecordCount);
                }
                catch
                {
                }
                if (dataTable1 != null && dataTable1.Rows.Count > 0)
                {
                    foreach (DataRow dataRow1 in (InternalDataCollectionBase)dataTable1.Rows)
                    {
                        QueryResult queryResult = new QueryResult();
                        foreach (string index1 in this.OutTabList)
                        {
                            Type tabType = this.GetTabType(this.TabClassDic[index1], this.TabAssemblyDic[index1]);
                            DataTable dataTable2 = new DataTable();
                            foreach (DataColumn dataColumn in (InternalDataCollectionBase)dataTable1.Columns)
                            {
                                if (dataColumn.ColumnName.StartsWith(index1 + "_"))
                                    dataTable2.Columns.Add(dataColumn.ColumnName.Substring((index1 + "_").Length));
                            }
                            DataRow row = dataTable2.NewRow();
                            foreach (DataColumn dataColumn in (InternalDataCollectionBase)dataTable2.Columns)
                                row[dataColumn.ColumnName] = dataRow1[index1 + "_" + dataColumn.ColumnName];
                            dataTable2.Rows.Add(row);
                            Type type = tabType;
                            object[] objArray = new object[1];
                            int index2 = 0;
                            DataRow dataRow2 = row;
                            objArray[index2] = (object)dataRow2;
                            object instance = Activator.CreateInstance(type, objArray);
                            queryResult.RowResultList.Add(this.TabNameDic[index1] + "." + index1, instance);
                            queryResult.Tables.Add(this.TabNameDic[index1] + "." + index1);
                        }
                        list.Add(queryResult);
                    }
                }
            }
            return list;
        }

        private Assembly LoadAssemblyFromFile(string fileFullName)
        {
            if (ModelBase.AsmDic.ContainsKey(fileFullName))
                return ModelBase.AsmDic[fileFullName];
            Assembly assembly = Assembly.LoadFile(fileFullName);
            ModelBase.AsmDic.Add(fileFullName, assembly);
            return assembly;
        }

        private Type GetTabType(string TypeName, string AssemblyName)
        {
            if (ModelBase.TabTypeDic.ContainsKey(TypeName))
                return ModelBase.TabTypeDic[TypeName];
            Type type = this.LoadAssemblyFromFile(AssemblyName).GetType(TypeName);
            ModelBase.TabTypeDic.Add(TypeName, type);
            return type;
        }

        private void BuildCommand(string sqlPart, object[] ParamsList = null)
        {
            if (string.IsNullOrEmpty(sqlPart))
                return;
            List<string> list = new List<string>();
            MatchCollection matchCollection = new Regex("(@[0-9a-zA-Z_]{1,30})", RegexOptions.IgnoreCase).Matches(sqlPart);
            if (matchCollection != null && matchCollection.Count > 0)
            {
                foreach (Match match in matchCollection)
                {
                    if (!list.Contains(match.Groups[1].Value))
                        list.Add(match.Groups[1].Value);
                }
            }
            if (list.Count > 0)
            {
                int index = 0;
                foreach (string parameterName in list)
                {
                    if (!((DbParameterCollection)this.MyCmd.Parameters).Contains(parameterName))
                        this.MyCmd.Parameters.AddWithValue(parameterName, ParamsList[index]);
                    ++index;
                }
            }
        }
    }
}
