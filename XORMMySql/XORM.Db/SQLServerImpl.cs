using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace XORM.Db
{
    public class SQLServerImpl : IDbExecute
    {
        private SqlConnection SQLConn = (SqlConnection)null;
        private Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        public DbDataReader ExecDataReader(string cmdText)
        {
            return this.ExecDataReader(cmdText, (object[])null);
        }

        public DbDataReader ExecDataReader(string cmdText, params object[] cmdParams)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                this.CommonPreCmd(cmdText, cmd, (SqlTransaction)null, CommandType.Text, (SqlParameter[])cmdParams);
                SqlDataReader sqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return (DbDataReader)sqlDataReader;
            }
            catch (Exception ex)
            {
                this.Close();
                throw ex;
            }
        }

        public DataSet ExecDataSet(DbCommand cmd)
        {
            try
            {
                cmd.Connection = (DbConnection)this.SQLConn;
                this.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter((SqlCommand)cmd);
                DataSet dataSet = new DataSet();
                ((DataAdapter)sqlDataAdapter).Fill(dataSet);
                this.Close();
                return dataSet;
            }
            catch (Exception ex)
            {
                this.Close();
                throw ex;
            }
        }

        public DataTable ExecDataTable(DbCommand cmd)
        {
            try
            {
                cmd.Connection = (DbConnection)this.SQLConn;
                this.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter((SqlCommand)cmd);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                this.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                this.Close();
                throw ex;
            }
        }

        public int ExecNonQuery(DbCommand cmd)
        {
            try
            {
                cmd.Connection = (DbConnection)this.SQLConn;
                this.Open();
                int num = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                this.Close();
                return num;
            }
            catch (Exception ex)
            {
                this.Close();
                throw ex;
            }
        }

        public DataSet ExecProcDataSet(string ProcName)
        {
            return this.ExecProcDataSet(ProcName, (object[])null);
        }

        public DataSet ExecProcDataSet(string ProcName, params object[] cmdParams)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                this.CommonPreCmd(ProcName, sqlCommand, (SqlTransaction)null, CommandType.StoredProcedure, (SqlParameter[])cmdParams);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                ((DataAdapter)sqlDataAdapter).Fill(dataSet);
                sqlCommand.Parameters.Clear();
                this.Close();
                return dataSet;
            }
            catch (Exception ex)
            {
                this.Close();
                throw ex;
            }
        }

        public DataTable ExecProcDataTable(string ProcName)
        {
            return this.ExecProcDataTable(ProcName);
        }

        public DataTable ExecProcDataTable(string ProcName, params object[] cmdParams)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                this.CommonPreCmd(ProcName, sqlCommand, (SqlTransaction)null, CommandType.StoredProcedure, (SqlParameter[])cmdParams);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlCommand.Parameters.Clear();
                this.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                this.Close();
                throw ex;
            }
        }

        public int ExecProcNonQuery(string ProcName)
        {
            return this.ExecProcNonQuery(ProcName, (object[])null);
        }

        public int ExecProcNonQuery(string ProcName, params object[] cmdParams)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                this.CommonPreCmd(ProcName, cmd, (SqlTransaction)null, CommandType.StoredProcedure, (SqlParameter[])cmdParams);
                int num = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                this.Close();
                return num;
            }
            catch (Exception ex)
            {
                this.Close();
                throw ex;
            }
        }

        public object ExecProcScalar(string ProcName)
        {
            return this.ExecProcScalar(ProcName, (object[])null);
        }

        public object ExecProcScalar(string ProcName, params object[] cmdParams)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                this.CommonPreCmd(ProcName, cmd, (SqlTransaction)null, CommandType.StoredProcedure, (SqlParameter[])cmdParams);
                object obj = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                this.Close();
                return obj;
            }
            catch (Exception ex)
            {
                this.Close();
                throw ex;
            }
        }

        public object ExecScalar(string cmdText)
        {
            return this.ExecTextScalar(cmdText);
        }

        public object ExecScalar(DbCommand cmd)
        {
            try
            {
                cmd.Connection = (DbConnection)this.SQLConn;
                this.Open();
                object obj = cmd.ExecuteScalar();
                this.Close();
                return obj;
            }
            catch (Exception ex)
            {
                this.Close();
                throw ex;
            }
        }

        public DataSet ExecTextDataSet(string SQLText)
        {
            return this.ExecTextDataSet(SQLText, (object[])null);
        }

        public DataSet ExecTextDataSet(string SQLText, params object[] cmdParams)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                this.CommonPreCmd(SQLText, sqlCommand, (SqlTransaction)null, CommandType.Text, (SqlParameter[])cmdParams);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                ((DataAdapter)sqlDataAdapter).Fill(dataSet);
                sqlCommand.Parameters.Clear();
                this.Close();
                return dataSet;
            }
            catch (Exception ex)
            {
                this.Close();
                throw ex;
            }
        }

        public DataTable ExecTextDataTable(string SQLText)
        {
            return this.ExecTextDataTable(SQLText, (object[])null);
        }

        public DataTable ExecTextDataTable(string SQLText, params object[] cmdParams)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                this.CommonPreCmd(SQLText, sqlCommand, (SqlTransaction)null, CommandType.Text, (SqlParameter[])cmdParams);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlCommand.Parameters.Clear();
                this.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                this.Close();
                throw ex;
            }
        }

        public int ExecTextNonQuery(string SQLText)
        {
            return this.ExecTextNonQuery(SQLText, (object[])null);
        }

        public int ExecTextNonQuery(string SQLText, params object[] cmdParams)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                this.CommonPreCmd(SQLText, cmd, (SqlTransaction)null, CommandType.Text, (SqlParameter[])cmdParams);
                int num = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                this.Close();
                return num;
            }
            catch (Exception ex)
            {
                this.Close();
                throw ex;
            }
        }

        public object ExecTextScalar(string SQLText)
        {
            return this.ExecTextScalar(SQLText, (object[])null);
        }

        public object ExecTextScalar(string SQLText, params object[] cmdParams)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                this.CommonPreCmd(SQLText, cmd, (SqlTransaction)null, CommandType.Text, (SqlParameter[])cmdParams);
                object obj = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                this.Close();
                return obj;
            }
            catch (Exception ex)
            {
                this.Close();
                throw ex;
            }
        }

        public bool Initialize(string ConnectionString)
        {
            try
            {
                this.SQLConn = new SqlConnection(ConnectionString);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Open()
        {
            if (this.SQLConn == null)
                throw new Exception("链接尚未初始化!");
            if (this.SQLConn == null || this.SQLConn.State != ConnectionState.Closed)
                return;
            this.SQLConn.Open();
        }

        public void Close()
        {
            if (this.SQLConn == null || this.SQLConn.State != ConnectionState.Open)
                return;
            this.SQLConn.Close();
            this.SQLConn.Dispose();
        }

        private void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            this.parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// 操作时把放入缓存中的参数取出来
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        private SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] sqlParameterArray1 = (SqlParameter[])this.parmCache[(object)cacheKey];
            if (sqlParameterArray1 == null)
                return null;
            SqlParameter[] sqlParameterArray2 = new SqlParameter[sqlParameterArray1.Length];
            int index = 0;
            for (int length = sqlParameterArray1.Length; index < length; ++index)
                sqlParameterArray2[index] = (SqlParameter)((ICloneable)sqlParameterArray1[index]).Clone();
            return sqlParameterArray2;
        }

        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms == null)
                return;
            foreach (SqlParameter sqlParameter in cmdParms)
                cmd.Parameters.Add(sqlParameter);
        }

        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
        }

        private void CommonPreCmd(string cmdText, SqlCommand cmd, SqlTransaction trans, CommandType cmdType, params SqlParameter[] commandParameters)
        {
            if (commandParameters != null)
                this.PrepareCommand(cmd, this.SQLConn, trans, cmdType, cmdText, commandParameters);
            else
                this.PrepareCommand(cmd, this.SQLConn, trans, cmdType, cmdText);
        }
    }
}
