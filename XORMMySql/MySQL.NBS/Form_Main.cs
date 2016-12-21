using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;
using MySQL.NetDBS;

namespace Lixx.DBS
{
    public partial class Form_Main : Form
    {
        private string DBConnString = string.Empty;
        public Form_Main()
        {
            this.InitializeComponent();
            if (ConfigurationManager.ConnectionStrings != null && ConfigurationManager.ConnectionStrings.Count > 0)
            {
                for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
                {
                    this.Combo_Conn.Items.Add(ConfigurationManager.ConnectionStrings[i].Name);
                }
            }

            ToolTip TT = new ToolTip();
            TT.SetToolTip(Server_Box, "服务器地址,如:192.168.0.1,1433(如数据库端口未修改,则不用写,1433)");
            TT.SetToolTip(DataBase_Box, "数据库用户名");
            TT.SetToolTip(DataBase_Box, "数据库名");
            TT.SetToolTip(NameSpaceDBO_Box, "数据访问组件命名空间,如:Vending.DatasInfo.");
            TT.SetToolTip(SelDBODir_Btn, "数据访问组件根目录,如D:\\Vending.DatasInfo");
        }
        /// <summary>
        /// 连接字符串,并获取表列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Conn_Btn_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(this.DBConnString);
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
            }
            catch (Exception ex)
            {
                this.ShowMsg("数据库连接失败" + ex.Message);
                return;
            }
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(new MySqlCommand("select table_name from information_schema.tables where table_schema = '" + connection.Database + "'", connection));
            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);
            if (connection.State == ConnectionState.Open)
                connection.Close();
            this.ShowMsg("成功获取数据表列表");
            this.DBTabList.Items.Clear();
            foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable.Rows)
                this.DBTabList.Items.Add((object)dataRow["table_name"].ToString(), false);
        }
        /// <summary>
        /// 选择访问类输出目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelDir_Btn_Click(object sender, EventArgs e)
        {
            if (Data_SelDirDlg.ShowDialog() == DialogResult.OK)
            {
                this.OutDBODir_Box.Text = Data_SelDirDlg.SelectedPath;
                ShowMsg("<数据访问类>输出目录：" + this.OutDBODir_Box.Text);
            }
        }
        /// <summary>
        /// 选择实体类输出目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelDATDir_Btn_Click(object sender, EventArgs e)
        {
            if (Model_SelDirDlg.ShowDialog() == DialogResult.OK)
            {
                this.OutDATDir_Box.Text = Model_SelDirDlg.SelectedPath;
                ShowMsg("<数据实体类>输出目录：" + this.OutDATDir_Box.Text);
            }
        }
        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="msg"></param>
        private void ShowMsg(string msg)
        {
            this.Msg_Box.AppendText(msg + "\r\n");
        }
        /// <summary>
        /// 创建数据库操作代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DBT_Btn_Click(object sender, EventArgs e)
        {
            
            //访问类命名控件头,如:Vending.DatasInfo.
            string NameSpaceHead_DBO = this.NameSpaceDBO_Box.Text + ".";
            //实体类命名空间头,如:Vending.Model.Base.
            string NameSpaceHead_DAT = this.NameSpaceDAT_Box.Text + ".";
            //数据访问类输出目录,如:D:\\Vending.DatasInfo
            string OutDirStr_DBO = this.OutDBODir_Box.Text;
            //数据实体类输出目录,如:D:\\Vending.Model.Base
            string OutDirStr_DAT = this.OutDATDir_Box.Text;
            string ConnectionMark = this.CONNMARK_Box.Text.Trim();
            this.DBT_Progress.Maximum = this.DBTabList.CheckedItems.Count;
            this.DBT_Progress.Minimum = 0;
            bool @checked = this.CHK_READONLY.Checked;
            for (int index = 0; index < this.DBTabList.CheckedItems.Count; ++index)
            {
                new DBS_Control(this.DBConnString, NameSpaceHead_DBO, OutDirStr_DBO, NameSpaceHead_DAT, OutDirStr_DAT, this.DBTabList.CheckedItems[index].ToString(), ConnectionMark, @checked).CreateAll();
                this.DBT_Progress.Value = index + 1;
            }
            if (this.DBT_Progress.Value != this.DBTabList.CheckedItems.Count)
                return;
            int num = (int)MessageBox.Show("操作成功", "消息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }

        private void SelAll_Btn_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < this.DBTabList.Items.Count; ++index)
                this.DBTabList.SetItemChecked(index, true);
        }

        private void Combo_Conn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Combo_Conn.SelectedIndex == -1 || string.IsNullOrEmpty(this.Combo_Conn.Text))
                return;
            MySqlConnection mySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings[this.Combo_Conn.Text].ConnectionString);
            this.Server_Box.Text = mySqlConnection.DataSource;
            this.DataBase_Box.Text = mySqlConnection.Database;
            this.DBConnString = ConfigurationManager.ConnectionStrings[this.Combo_Conn.Text].ConnectionString;
            mySqlConnection.Dispose();
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[this.Combo_Conn.Text + "_DIR_DBO"]))
                this.OutDBODir_Box.Text = ConfigurationManager.AppSettings[this.Combo_Conn.Text + "_DIR_DBO"];
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[this.Combo_Conn.Text + "_DIR_MOD"]))
                this.OutDATDir_Box.Text = ConfigurationManager.AppSettings[this.Combo_Conn.Text + "_DIR_MOD"];
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[this.Combo_Conn.Text + "_NAS_DAT"]))
                this.NameSpaceDAT_Box.Text = ConfigurationManager.AppSettings[this.Combo_Conn.Text + "_NAS_DAT"];
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[this.Combo_Conn.Text + "_NAS_DBO"]))
                this.NameSpaceDBO_Box.Text = ConfigurationManager.AppSettings[this.Combo_Conn.Text + "_NAS_DBO"];
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[this.Combo_Conn.Text + "_MARK_CON"]))
                this.CONNMARK_Box.Text = ConfigurationManager.AppSettings[this.Combo_Conn.Text + "_MARK_CON"];
        }


    }
}