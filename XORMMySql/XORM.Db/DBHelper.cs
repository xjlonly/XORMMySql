using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace XORM.Db
{
    public class DBHelper
    {
        private int DBTypeMARK = 1;
        private string _ConnectionMark = "";
        private Loger log = new Loger();

        IDbExecute DbExecutor
        {
            get
            {
                ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[this._ConnectionMark];
                this.DBTypeMARK = Convert.ToInt32(connectionStringSettings.ProviderName ?? "1");
                switch (this.DBTypeMARK)
                {
                    case 1:
                        IDbExecute dbExecute1 = (IDbExecute)new SQLServerImpl();
                        dbExecute1.Initialize(connectionStringSettings.ConnectionString);
                        return dbExecute1;
                    case 3:
                        IDbExecute dbExecute2 = (IDbExecute)new MySqlImpl();
                        dbExecute2.Initialize(connectionStringSettings.ConnectionString);
                        return dbExecute2;
                    default:
                        return (IDbExecute)null;
                }
            }
        }

        public DBHelper(string ConnectionMark, bool IsReadOnly = true)
        {
            this._ConnectionMark = ConnectionMark;
            if (IsReadOnly)
                this._ConnectionMark = this._ConnectionMark + "_READ";
            else
                this._ConnectionMark = this._ConnectionMark + "_WRITE";
        }

        public object ExecTextScalar(string SQLText)
        {
            try
            {
                return this.DbExecutor.ExecTextScalar(SQLText);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, SQLText);
                throw ex;
            }
        }

        public object ExecTextScalar(string SQLText, params object[] cmdParams)
        {
            try
            {
                return this.DbExecutor.ExecTextScalar(SQLText, cmdParams);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, SQLText, cmdParams);
                throw ex;
            }
        }

        public object ExecProcScalar(string ProcName)
        {
            try
            {
                return this.DbExecutor.ExecProcScalar(ProcName);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, ProcName);
                throw ex;
            }
        }

        public object ExecProcScalar(string ProcName, params object[] cmdParams)
        {
            try
            {
                return this.DbExecutor.ExecProcScalar(ProcName, cmdParams);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, ProcName, cmdParams);
                throw ex;
            }
        }

        public int ExecTextNonQuery(string SQLText)
        {
            try
            {
                return this.DbExecutor.ExecTextNonQuery(SQLText);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, SQLText);
                throw ex;
            }
        }

        public int ExecTextNonQuery(string SQLText, params object[] cmdParams)
        {
            try
            {
                return this.DbExecutor.ExecTextNonQuery(SQLText, cmdParams);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, SQLText, cmdParams);
                throw ex;
            }
        }

        public int ExecProcNonQuery(string ProcName)
        {
            try
            {
                return this.DbExecutor.ExecProcNonQuery(ProcName);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, ProcName);
                throw ex;
            }
        }

        public int ExecProcNonQuery(string ProcName, params object[] cmdParams)
        {
            try
            {
                return this.DbExecutor.ExecProcNonQuery(ProcName, cmdParams);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, ProcName, cmdParams);
                throw ex;
            }
        }

        public DataSet ExecTextDataSet(string SQLText)
        {
            try
            {
                return this.DbExecutor.ExecTextDataSet(SQLText);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, SQLText);
                throw ex;
            }
        }

        public DataSet ExecTextDataSet(string SQLText, params object[] cmdParams)
        {
            try
            {
                return this.DbExecutor.ExecTextDataSet(SQLText, cmdParams);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, SQLText, cmdParams);
                throw ex;
            }
        }

        public DataSet ExecProcDataSet(string ProcName)
        {
            try
            {
                return this.DbExecutor.ExecProcDataSet(ProcName);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, ProcName);
                throw ex;
            }
        }

        public DataSet ExecProcDataSet(string ProcName, params object[] cmdParams)
        {
            try
            {
                return this.DbExecutor.ExecProcDataSet(ProcName, cmdParams);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, ProcName, cmdParams);
                throw ex;
            }
        }

        public DataTable ExecTextDataTable(string SQLText)
        {
            try
            {
                return this.DbExecutor.ExecTextDataTable(SQLText);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, SQLText);
                throw ex;
            }
        }

        public DataTable ExecTextDataTable(string SQLText, params object[] cmdParams)
        {
            try
            {
                return this.DbExecutor.ExecTextDataTable(SQLText, cmdParams);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, SQLText, cmdParams);
                throw ex;
            }
        }

        public DataTable ExecProcDataTable(string ProcName)
        {
            try
            {
                return this.DbExecutor.ExecProcDataTable(ProcName);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, ProcName);
                throw ex;
            }
        }

        public DataTable ExecProcDataTable(string ProcName, params object[] cmdParams)
        {
            try
            {
                return this.DbExecutor.ExecProcDataTable(ProcName, cmdParams);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, ProcName, cmdParams);
                throw ex;
            }
        }

        public object ExecScalar(DbCommand cmd)
        {
            try
            {
                return this.DbExecutor.ExecScalar(cmd);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, cmd);
                throw ex;
            }
        }

        public object ExecScalar(string cmdText)
        {
            try
            {
                return this.DbExecutor.ExecScalar(cmdText);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, cmdText);
                throw ex;
            }
        }

        public int ExecNonQuery(DbCommand cmd)
        {
            try
            {
                return this.DbExecutor.ExecNonQuery(cmd);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, cmd);
                throw ex;
            }
        }

        public DataTable ExecDataTable(DbCommand cmd)
        {
            try
            {
                return this.DbExecutor.ExecDataTable(cmd);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, cmd);
                throw ex;
            }
        }

        public DataSet ExecDataSet(DbCommand cmd)
        {
            try
            {
                return this.DbExecutor.ExecDataSet(cmd);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, cmd);
                throw ex;
            }
        }

        public DbDataReader ExecDataReader(string cmdText)
        {
            try
            {
                return this.DbExecutor.ExecDataReader(cmdText);
            }
            catch (Exception ex)
            {
                this.log.Save(ex, cmdText);
                throw ex;
            }
        }
    }
}
