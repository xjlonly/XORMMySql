using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Text;

namespace XORM.Db
{
    internal class Loger : IDisposable
    {
        private Saver MySaver = (Saver)null;

        public Loger()
        {
            if (string.IsNullOrEmpty(LogConfig.Path))
                return;
            this.MySaver = new Saver(LogConfig.Path);
        }

        public void Save(Exception e, string CmdText)
        {
            try
            {
                if (!LogConfig.Enable || string.IsNullOrEmpty(LogConfig.Path) || this.MySaver == null)
                    return;
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                stringBuilder.Append("Message").AppendLine(e.Message).Append("StackTrace:").AppendLine(e.StackTrace);
                stringBuilder.Append("CommandText:").AppendLine(CmdText);
                stringBuilder.AppendLine("SqlParameters: NO");
                stringBuilder.AppendLine("-----------------------------------------------------------------------------").Append("\r\n");
                this.MySaver.Info(((object)stringBuilder).ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Exception e, string CmdText, object[] Parameters)
        {
            try
            {
                if (!LogConfig.Enable || string.IsNullOrEmpty(LogConfig.Path) || this.MySaver == null)
                    return;
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                stringBuilder.Append("Message").AppendLine(e.Message).Append("StackTrace:").AppendLine(e.StackTrace);
                stringBuilder.Append("CommandText:").AppendLine(CmdText);
                stringBuilder.AppendLine("Parameters:");
                foreach (object obj in Parameters)
                {
                    DbParameter dbParameter = obj as DbParameter;
                    stringBuilder.Append("\tNAME:").Append(dbParameter.ParameterName).Append(",").Append("VALUE:").AppendLine(dbParameter.Value.ToString());
                }
                stringBuilder.AppendLine("-----------------------------------------------------------------------------").Append("\r\n");
                this.MySaver.Info(((object)stringBuilder).ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Exception e, DbCommand ExCmd)
        {
            try
            {
                if (!LogConfig.Enable || string.IsNullOrEmpty(LogConfig.Path) || this.MySaver == null)
                    return;
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                stringBuilder.Append("Message").AppendLine(e.Message).Append("StackTrace:").AppendLine(e.StackTrace);
                stringBuilder.Append("CommandText:").AppendLine(ExCmd.CommandText);
                stringBuilder.AppendLine("SqlParameters:");
                foreach (MySqlParameter mySqlParameter in ExCmd.Parameters)
                    stringBuilder.Append("\tNAME:").Append(mySqlParameter.ParameterName).Append(",").Append("VALUE:").AppendLine(mySqlParameter.Value.ToString());
                stringBuilder.AppendLine("-----------------------------------------------------------------------------").Append("\r\n");
                this.MySaver.Info(((object)stringBuilder).ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            this.MySaver.FinalSave();
        }
    }
}
