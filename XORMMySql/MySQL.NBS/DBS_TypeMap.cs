using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MySQL.NetDBS
{
    public class DBS_TypeMap
    {
        private Dictionary<string, DBS_AtomType> DIC = new Dictionary<string, DBS_AtomType>();
        private List<string> KeyWordList = new List<string>();
        public const string CodeKeyWord = "abstract\r\n|as\r\n|base\r\n|bool\r\n|break\r\n|byte\r\n|case\r\n|catch\r\n|char\r\n|checked\r\n|class\r\n|const\r\n|continue\r\n|decimal\r\n|default\r\n|delegate\r\n|do\r\n|double\r\n|else\r\n|enum\r\n|event\r\n|explicit\r\n|extern\r\n|false\r\n|finally\r\n|fixed\r\n|float\r\n|for\r\n|foreach\r\n|goto\r\n|if\r\n|implicit\r\n|in\r\n|int\r\n|interface\r\n|internal\r\n|is\r\n|lock\r\n|long\r\n|namespace\r\n|new\r\n|null\r\n|operator\r\n|out\r\n|override\r\n|params\r\n|private\r\n|protected\r\n|public\r\n|readonly\r\n|ref\r\n|return\r\n|sbyte\r\n|sealed\r\n|short\r\n|sizeof\r\n|stackalloc\r\n|static\r\n|string\r\n|struct\r\n|switch\r\n|this\r\n|throw\r\n|true\r\n|try\r\n|typeof\r\n|uint\r\n|ulong\r\n|unchecked\r\n|unsafe\r\n|ushort\r\n|using\r\n|virtual\r\n|void\r\n|volatile\r\n|while";

        public DBS_AtomType this[string typeName]
        {
            get
            {
                Dictionary<string, DBS_AtomType> dictionary = this.DIC;
                string str = typeName;
                char[] chArray = new char[1];
                int index1 = 0;
                int num = 40;
                chArray[index1] = (char)num;
                string index2 = str.Split(chArray)[0];
                return dictionary[index2];
            }
        }

        public DBS_TypeMap()
        {
            this.DIC.Add("bit", new DBS_AtomType("bit", "bool", "Convert.ToBoolean", "MySql.Data.MySqlClient.MySqlDbType.Bit"));
            this.DIC.Add("tinyint", new DBS_AtomType("tinyint", "int", "Convert.ToInt32", "System.Data.SqlDbType.TinyInt"));
            this.DIC.Add("varbinary", new DBS_AtomType("varbinary", "Byte[]", "(Byte[])", "System.Data.SqlDbType.VarBinary"));
            this.DIC.Add("timestamp", new DBS_AtomType("timestamp", "Byte[]", "(Byte[])", "System.Data.SqlDbType.Timestamp"));
            this.DIC.Add("image", new DBS_AtomType("image", "Byte[]", "(Byte[])", "System.Data.SqlDbType.Image"));
            this.DIC.Add("binary", new DBS_AtomType("binary", "Byte[]", "(Byte[])", "System.Data.SqlDbType.Binary"));
            this.DIC.Add("smallint", new DBS_AtomType("smallint", "Int16", "Convert.ToInt16", "MySql.Data.MySqlClient.MySqlDbType.Int16"));
            this.DIC.Add("int", new DBS_AtomType("int", "Int32", "Convert.ToInt32", "MySql.Data.MySqlClient.MySqlDbType.Int32"));
            this.DIC.Add("bigint", new DBS_AtomType("bigint", "Int64", "Convert.ToInt64", "MySql.Data.MySqlClient.MySqlDbType.Int64"));
            this.DIC.Add("date", new DBS_AtomType("datetime", "DateTime", "Convert.ToDateTime", "MySql.Data.MySqlClient.MySqlDbType.Date"));
            this.DIC.Add("datetime", new DBS_AtomType("datetime", "DateTime", "Convert.ToDateTime", "MySql.Data.MySqlClient.MySqlDbType.Datetime"));
            this.DIC.Add("datetime2", new DBS_AtomType("datetime", "DateTime", "Convert.ToDateTime", "System.Data.SqlDbType.DateTime2"));
            this.DIC.Add("smalldatetime", new DBS_AtomType("smalldatetime", "int", "Convert.ToDateTime", "System.Data.SqlDbType.SmallDateTime"));
            this.DIC.Add("decimal", new DBS_AtomType("decimal", "decimal", "Convert.ToDecimal", "MySql.Data.MySqlClient.MySqlDbType.Decimal"));
            this.DIC.Add("numeric", new DBS_AtomType("numeric", "decimal", "Convert.ToDecimal", "MySql.Data.MySqlClient.MySqlDbType.Decimal"));
            this.DIC.Add("float", new DBS_AtomType("float", "double", "Convert.ToDouble", "MySql.Data.MySqlClient.MySqlDbType.Decimal"));
            this.DIC.Add("money", new DBS_AtomType("money", "decimal", "Convert.ToDecimal", "System.Data.SqlDbType.Money"));
            this.DIC.Add("smallmoney", new DBS_AtomType("smallmoney", "decimal", "Convert.ToDecimal", "System.Data.SqlDbType.Decimal"));
            this.DIC.Add("real", new DBS_AtomType("real", "decimal", "Convert.ToDecimal", "System.Data.SqlDbType.Real"));
            this.DIC.Add("char", new DBS_AtomType("char", "string", "Convert.ToString", "MySql.Data.MySqlClient.MySqlDbType.VarChar"));
            this.DIC.Add("nchar", new DBS_AtomType("nchar", "string", "Convert.ToString", "MySql.Data.MySqlClient.MySqlDbType.VarChar"));
            this.DIC.Add("text", new DBS_AtomType("text", "string", "Convert.ToString", "System.Data.SqlDbType.Text"));
            this.DIC.Add("ntext", new DBS_AtomType("ntext", "string", "Convert.ToString", "System.Data.SqlDbType.NText"));
            this.DIC.Add("varchar", new DBS_AtomType("varchar", "string", "Convert.ToString", "MySql.Data.MySqlClient.MySqlDbType.VarChar"));
            this.DIC.Add("nvarchar", new DBS_AtomType("nvarchar", "string", "Convert.ToString", "MySql.Data.MySqlClient.MySqlDbType.VarChar"));
            this.DIC.Add("uniqueidentifier", new DBS_AtomType("uniqueidentifier", "System.Guid", "Convert.ToString", "System.Data.SqlDbType.UniqueIdentifier"));
            this.DIC.Add("xml", new DBS_AtomType("xml", "string", "Convert.ToString", "System.Data.SqlDbType.Xml"));
            string str = "abstract\r\n|as\r\n|base\r\n|bool\r\n|break\r\n|byte\r\n|case\r\n|catch\r\n|char\r\n|checked\r\n|class\r\n|const\r\n|continue\r\n|decimal\r\n|default\r\n|delegate\r\n|do\r\n|double\r\n|else\r\n|enum\r\n|event\r\n|explicit\r\n|extern\r\n|false\r\n|finally\r\n|fixed\r\n|float\r\n|for\r\n|foreach\r\n|goto\r\n|if\r\n|implicit\r\n|in\r\n|int\r\n|interface\r\n|internal\r\n|is\r\n|lock\r\n|long\r\n|namespace\r\n|new\r\n|null\r\n|operator\r\n|out\r\n|override\r\n|params\r\n|private\r\n|protected\r\n|public\r\n|readonly\r\n|ref\r\n|return\r\n|sbyte\r\n|sealed\r\n|short\r\n|sizeof\r\n|stackalloc\r\n|static\r\n|string\r\n|struct\r\n|switch\r\n|this\r\n|throw\r\n|true\r\n|try\r\n|typeof\r\n|uint\r\n|ulong\r\n|unchecked\r\n|unsafe\r\n|ushort\r\n|using\r\n|virtual\r\n|void\r\n|volatile\r\n|while";
            char[] chArray = new char[1];
            int index = 0;
            int num = 124;
            chArray[index] = (char)num;
            string[] strArray = str.Split(chArray);
            this.KeyWordList.Clear();
            this.KeyWordList.AddRange((IEnumerable<string>)strArray);
        }

        public bool IsKeyWord(string colName)
        {
            return this.KeyWordList.Contains(colName);
        }
    }
}