using System.Data;
using System.Data.Common;

namespace XORM.Db
{
    internal interface IDbExecute
    {
        bool Initialize(string ConnectionString);

        object ExecTextScalar(string SQLText);

        object ExecTextScalar(string SQLText, params object[] cmdParams);

        object ExecProcScalar(string ProcName);

        object ExecProcScalar(string ProcName, params object[] cmdParams);

        int ExecTextNonQuery(string SQLText);

        int ExecTextNonQuery(string SQLText, params object[] cmdParams);

        int ExecProcNonQuery(string ProcName);

        int ExecProcNonQuery(string ProcName, params object[] cmdParams);

        DataSet ExecTextDataSet(string SQLText);

        DataSet ExecTextDataSet(string SQLText, params object[] cmdParams);

        DataSet ExecProcDataSet(string ProcName);

        DataSet ExecProcDataSet(string ProcName, params object[] cmdParams);

        DataTable ExecTextDataTable(string SQLText);

        DataTable ExecTextDataTable(string SQLText, params object[] cmdParams);

        DataTable ExecProcDataTable(string ProcName);

        DataTable ExecProcDataTable(string ProcName, params object[] cmdParams);

        object ExecScalar(DbCommand cmd);

        object ExecScalar(string cmdText);

        int ExecNonQuery(DbCommand cmd);

        DataTable ExecDataTable(DbCommand cmd);

        DataSet ExecDataSet(DbCommand cmd);

        DbDataReader ExecDataReader(string cmdText);

        DbDataReader ExecDataReader(string cmdText, params object[] cmdParams);
    }
}
