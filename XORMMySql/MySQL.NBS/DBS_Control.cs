using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Text;

namespace MySQL.NetDBS
{
    public class DBS_Control
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string _AdminConnectionString = string.Empty;
        /// <summary>
        /// 数据访问类命名空间头
        /// </summary>
        private string _DBONameSpace = string.Empty;
        /// <summary>
        /// 数据实体类命名空间头
        /// </summary>
        private string _ModelNameSpace = string.Empty;
        /// <summary>
        /// 数据访问类输出目录
        /// </summary>
        private string _DBOFolder = string.Empty;
        /// <summary>
        /// 数据实体类输出目录
        /// </summary>
        private string _ModelFolder = string.Empty;
        /// <summary>
        /// 数据库表名
        /// </summary>
        private string _TableName = string.Empty;
        /// <summary>
        /// 数据访问类命名空间
        /// </summary>
        private string _DBUNameSpace = string.Empty;
        /// <summary>
        /// 链接字符串前缀
        /// </summary>
        private string _ConnectionMark = string.Empty;
        /// <summary>
        /// 是否默认使用只读源
        /// </summary>
        private bool _UseReadOnlyForSelect = true;

        public DBS_Control(string ConnStr, string NameSpaceHead_DBO, string FolderHead_DBO, string NameSpaceHead_DAT, string FolderHead_DAT, string TableName, string ConnectionMark, bool UseReadOnlyForSelect = true)
        {
            this._AdminConnectionString = ConnStr;
            this._DBONameSpace = NameSpaceHead_DBO;
            this._DBOFolder = FolderHead_DBO;
            this._ModelNameSpace = NameSpaceHead_DAT;
            this._ModelFolder = FolderHead_DAT;
            this._TableName = TableName;
            this._ConnectionMark = ConnectionMark;
            this._UseReadOnlyForSelect = UseReadOnlyForSelect;
        }

        public void CreateAll()
        {
            string cmdText = "SHOW FULL COLUMNS FROM " + this._TableName;
            MySqlConnection connection = new MySqlConnection(this._AdminConnectionString);
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(new MySqlCommand(cmdText, connection));
            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);
            connection.Close();
            connection.Dispose();
            if (dataTable == null || dataTable.Rows.Count <= 0)
                return;
            string Dir1 = this._DBOFolder;
            string Dir2 = this._ModelFolder;
            //创建数据实体类目录
            this.CheckAndCreateDir(Dir1);
            //创建数据访问类目录
            this.CheckAndCreateDir(Dir2);
            //创建数据实体类
            this.Create_Class(dataTable, this._TableName, Dir2 + "\\" + this._TableName + ".cs", this._ModelNameSpace, this._ModelFolder);
            this.Create_DBOper(dataTable, this._TableName, Dir1 + "\\" + this._TableName + ".cs", this._DBONameSpace, this._DBOFolder, this._ModelNameSpace, this._ConnectionMark, this._UseReadOnlyForSelect);
        }

        /// <summary>
        /// 创建实体文件
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tableName"></param>
        /// <param name="aimFile"></param>
        /// <param name="namespaceHead"></param>
        /// <param name="classfolderHead"></param>
        private void Create_Class(DataTable dt, string tableName, string aimFile, string namespaceHead, string classfolderHead)
        {
            string fileContent = new DBS_CCClass(dt, namespaceHead, classfolderHead, tableName, this._ConnectionMark).CreateFileContent();
            this.CheckAndWriteContent(aimFile, fileContent);
        }


        /// <summary>
        /// 创建代码访问文件
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tableName"></param>
        /// <param name="aimFile"></param>
        /// <param name="namespaceHead_dbo"></param>
        /// <param name="dbofolderHead"></param>
        /// <param name="namespaceHead_class"></param>
        /// <param name="namespaceHead_dbu"></param>
        /// <param name="UseReadOnlyForSelect"></param>
        private void Create_DBOper(DataTable dt, string tableName, string aimFile, string namespaceHead_dbo, string dbofolderHead, string namespaceHead_class, string connectionConfigMark, bool UseReadOnlyForSelect)
        {
            string fileContent = new DBS_CCDBOper(dt, namespaceHead_dbo, dbofolderHead, tableName, namespaceHead_class, connectionConfigMark, UseReadOnlyForSelect).CreateFileContent();
            this.CheckAndWriteContent(aimFile, fileContent);
        }

        /// <summary>
        /// 检测目录，如不存在则创建
        /// </summary>
        /// <param name="Dir"></param>
        private void CheckAndCreateDir(string Dir)
        {
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
            }
        }
        /// <summary>
        /// 检查并创建文件内容
        /// </summary>
        /// <param name="AimFile"></param>
        /// <param name="Content"></param>
        private void CheckAndWriteContent(string AimFile, string Content)
        {
            FileStream fileStream = null;
            if (!File.Exists(AimFile))
            {
                fileStream = File.Create(AimFile);
            }
            else
            {
                File.Delete(AimFile);
                fileStream = new FileStream(AimFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
            }
            StreamWriter streamWriter = new StreamWriter((Stream)fileStream, Encoding.Default);
            streamWriter.Write(Content);
            streamWriter.Close();
            streamWriter.Dispose();
            fileStream.Close();
            fileStream.Dispose();
        }
    }
}