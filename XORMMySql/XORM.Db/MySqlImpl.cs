using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace XORM.Db
{
    public class MySqlImpl : IDbExecute
    {
        private MySqlConnection SQLConn = (MySqlConnection)null;
        private Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        public DbDataReader ExecDataReader(string cmdText)
        {
            return this.ExecDataReader(cmdText, (object[])null);
        }

        public DbDataReader ExecDataReader(string cmdText, params object[] cmdParams)
        {
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                this.CommonPreCmd(cmdText, cmd, (MySqlTransaction)null, CommandType.Text, (MySqlParameter[])cmdParams);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return (DbDataReader)mySqlDataReader;
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
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter((MySqlCommand)cmd);
                DataSet dataSet = new DataSet();
                ((DataAdapter)mySqlDataAdapter).Fill(dataSet);
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
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter((MySqlCommand)cmd);
                DataTable dataTable = new DataTable();
                mySqlDataAdapter.Fill(dataTable);
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
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                this.CommonPreCmd(ProcName, mySqlCommand, (MySqlTransaction)null, CommandType.StoredProcedure, (MySqlParameter[])cmdParams);
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataSet dataSet = new DataSet();
                ((DataAdapter)mySqlDataAdapter).Fill(dataSet);
                mySqlCommand.Parameters.Clear();
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
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                this.CommonPreCmd(ProcName, mySqlCommand, (MySqlTransaction)null, CommandType.StoredProcedure, (MySqlParameter[])cmdParams);
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataTable dataTable = new DataTable();
                mySqlDataAdapter.Fill(dataTable);
                mySqlCommand.Parameters.Clear();
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
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                this.CommonPreCmd(ProcName, cmd, (MySqlTransaction)null, CommandType.StoredProcedure, (MySqlParameter[])cmdParams);
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
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                this.CommonPreCmd(ProcName, cmd, (MySqlTransaction)null, CommandType.StoredProcedure, (MySqlParameter[])cmdParams);
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
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                this.CommonPreCmd(SQLText, mySqlCommand, (MySqlTransaction)null, CommandType.Text, (MySqlParameter[])cmdParams);
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataSet dataSet = new DataSet();
                ((DataAdapter)mySqlDataAdapter).Fill(dataSet);
                mySqlCommand.Parameters.Clear();
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
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                this.CommonPreCmd(SQLText, mySqlCommand, (MySqlTransaction)null, CommandType.Text, (MySqlParameter[])cmdParams);
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataTable dataTable = new DataTable();
                mySqlDataAdapter.Fill(dataTable);
                mySqlCommand.Parameters.Clear();
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
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                this.CommonPreCmd(SQLText, cmd, (MySqlTransaction)null, CommandType.Text, (MySqlParameter[])cmdParams);
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
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                this.CommonPreCmd(SQLText, cmd, (MySqlTransaction)null, CommandType.Text, (MySqlParameter[])cmdParams);
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
                this.SQLConn = new MySqlConnection(ConnectionString);
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

        private void CacheParameters(string cacheKey, params MySqlParameter[] commandParameters)
        {
            this.parmCache[(object)cacheKey] = (object)commandParameters;
        }

        private MySqlParameter[] GetCachedParameters(string cacheKey)
        {
            MySqlParameter[] mySqlParameterArray1 = (MySqlParameter[])this.parmCache[(object)cacheKey];
            if (mySqlParameterArray1 == null)
                return (MySqlParameter[])null;
            MySqlParameter[] mySqlParameterArray2 = new MySqlParameter[mySqlParameterArray1.Length];
            int index = 0;
            for (int length = mySqlParameterArray1.Length; index < length; ++index)
                mySqlParameterArray2[index] = (MySqlParameter)mySqlParameterArray1[index].Clone();
            return mySqlParameterArray2;
        }

        private void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
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
            foreach (MySqlParameter mySqlParameter in cmdParms)
                cmd.Parameters.Add(mySqlParameter);
        }

        private void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
        }

        private void CommonPreCmd(string cmdText, MySqlCommand cmd, MySqlTransaction trans, CommandType cmdType, params MySqlParameter[] commandParameters)
        {
            if (commandParameters != null)
                this.PrepareCommand(cmd, this.SQLConn, trans, cmdType, cmdText, commandParameters);
            else
                this.PrepareCommand(cmd, this.SQLConn, trans, cmdType, cmdText);
        }
    }
}
