﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Reflection;
using System.Data.OleDb;


namespace Sjustmytestsql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sqlCnt.StateChange += new StateChangeEventHandler(sqlCnt_StateChange);
        }

        void sqlCnt_StateChange(object sender, StateChangeEventArgs e)
        {
            switch (sqlCnt.State)
            {
                case ConnectionState.Open:
                    button1.Enabled = true;
                    button1.Text = ConnectionState.Open.ToString();
                    break;
                case ConnectionState.Broken:
                    button1.Enabled = false;
                    button1.Text = ConnectionState.Broken.ToString();
                    break;
                case ConnectionState.Closed:
                    button1.Enabled = true;
                    button1.Text = ConnectionState.Closed.ToString();
                    break;
                default:
                    break;
            }
            //throw new NotImplementedException();
        }
        //string sql = "CREATE DATABASE mydb ON PRIMARY" + "(name=test_data,filename = ‘C:\\mysql\\mydb_data.mdf’, size=3," + "maxsize=5,filegrowth=10%)log on" + "(name=mydbb_log,filename=‘C:\\mysql\\mydb_log.ldf’,size=3," + "maxsize=20,filegrowth=1)";
        //Data Source=.;
        //Initial Catalog= ;//数据库名称
        //Integrated Security=True";//安全域 SSPI 这个表示以当前WINDOWS系统用户身去登录SQL SERVER服务器(需要在安装sqlserver时候设置)，如果SQL SERVER服务器不支持这种方式登录时，就会出错。
        private static string connectString = "Data Source=qds114325507.my3w.com;Initial Catalog=qds114325507_db;Integrated Security=False;User ID=qds114325507;Password=sqs20160127;Pooling=true;Max Pool Size=40000;Min Pool Size=0";
        private SqlConnection sqlCnt = new SqlConnection(connectString);
        private void button1_Click(object sender, EventArgs e)
        {
            if (sqlCnt.State == ConnectionState.Closed)
                sqlCnt.Open();
            else
                sqlCnt.Close();

            //try
            //{
            //    string sql = "CREATE TABLE myTable" + "(Id INTEGER CONSTRAINT PId PRIMARY KEY," + "myName CHAR(50),myAddress CHAR(255), myBalance FLOAT)";
            //    SqlCommand cmd = new SqlCommand(sql, sqlCnt);
            //    cmd.ExecuteNonQuery();
            //    MessageBox.Show("创建成功");
            //}
            //catch(Exception ew)
            //{
            //    MessageBox.Show("创建失败");
            //}
            //SELECT COLUMN_NAME FROM ALL_TAB_COLUMNS WHERE TABLE_NAME = '表名' AND COLUMN_NAME='列名'

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ClearEvent(button1, "Click");
            try
            {
                //string sql = "CREATE TABLE TagInfoTable" + "(Id INTEGER IDENTITY PRIMARY KEY," +
                //    "Active_ID CHAR(50),b_VoltLow CHAR(50), bConnect CHAR(50),Reader_ID CHAR(50),Readtime CHAR(50)" +
                //    ",str_ReaderIP CHAR(50),Tag_ID CHAR(50), Value1 CHAR(50),Value2 CHAR(50),Value3 CHAR(50)" +
                //    ",Uid CHAR(50),MotherName CHAR(50), status CHAR(50),b_left CHAR(50),Left_time CHAR(50)" +
                //    ",Left_hour CHAR(50),Left_min CHAR(50), sec_overtime CHAR(50),Last_time CHAR(50),event_time CHAR(50)" +
                //    ",up_time CHAR(50),sec_upalt CHAR(50), b_in_load CHAR(50),b_in_unload CHAR(50),b_in_warning CHAR(50)" +
                //    ",Record_time CHAR(50),RFID_remark CHAR(50), s_tel CHAR(50),Tag_RoomId CHAR(50),Tag_BedId CHAR(50),SqlSendTime DateTime,HospitalName CHAR(50)" +
                //    ")";
                string sql = "CREATE TABLE " + "T"+Convert.ToBase64String(Encoding.UTF8.GetBytes("中文名".ToCharArray())) + "(Id INTEGER IDENTITY PRIMARY KEY," +
                   "Active_ID CHAR(50),b_VoltLow CHAR(50), bConnect CHAR(50),Reader_ID CHAR(50),Readtime CHAR(50)" +
                   ",str_ReaderIP CHAR(50),Tag_ID CHAR(50), Value1 CHAR(50),Value2 CHAR(50),Value3 CHAR(50)" +
                   ",Uid CHAR(50),MotherName CHAR(50), status CHAR(50),b_left CHAR(50),Left_time CHAR(50)" +
                   ",Left_hour CHAR(50),Left_min CHAR(50), sec_overtime CHAR(50),Last_time CHAR(50),event_time CHAR(50)" +
                   ",up_time CHAR(50),sec_upalt CHAR(50), b_in_load CHAR(50),b_in_unload CHAR(50),b_in_warning CHAR(50)" +
                   ",Record_time CHAR(50),RFID_remark CHAR(50), s_tel CHAR(50),Tag_RoomId CHAR(50),Tag_BedId CHAR(50),SqlSendTime DateTime,HospitalName CHAR(50)" +
                   ")";
                SqlCommand cmd = new SqlCommand(sql, sqlCnt);
                cmd.ExecuteNonQuery();
                MessageBox.Show("创建成功");
            }
            catch   
            {
                MessageBox.Show("创建失败");
            }
            //SELECT COLUMN_NAME FROM ALL_TAB_COLUMNS WHERE TABLE_NAME = '表名' AND COLUMN_NAME='列名'
        }
        private static void ClearEvent(Control control, string eventname)
        {
            if (control == null) return;
            if (string.IsNullOrEmpty(eventname)) return;
            BindingFlags mPropertyFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic;
            BindingFlags mFieldFlags = BindingFlags.Static | BindingFlags.NonPublic;
            Type controlType = typeof(System.Windows.Forms.Control);
            PropertyInfo propertyInfo = controlType.GetProperty("Events", mPropertyFlags);
            EventHandlerList eventHandlerList = (EventHandlerList)propertyInfo.GetValue(control, null);
            FieldInfo fieldInfo = (typeof(Control)).GetField("Event" + eventname, mFieldFlags);
            Delegate d = eventHandlerList[fieldInfo.GetValue(control)];
            if (d == null) return;
            EventInfo eventInfo = controlType.GetEvent(eventname);
            foreach (Delegate dx in d.GetInvocationList())
                eventInfo.RemoveEventHandler(control, dx);
        }
        /// <summary>
        /// 清除一个对象的某个事件所挂钩的delegate
        /// </summary>
        /// <param name="ctrl">控件对象</param>
        /// <param name="eventName">事件名称，默认的</param>
        private static void ClearEvents(object ctrl, string eventName = "_EventAll")
        {
            if (ctrl == null) return;
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Static;
            EventInfo[] events = ctrl.GetType().GetEvents(bindingFlags);
            if (events == null || events.Length < 1) return;

            for (int i = 0; i < events.Length; i++)
            {
                try
                {
                    EventInfo ei = events[i];
                    //只删除指定的方法，默认是_EventAll，前面加_是为了和系统的区分，防以后雷同
                    if (eventName != "_EventAll" && ei.Name != eventName) continue;

                    /********************************************************
                     * class的每个event都对应了一个同名(变了，前面加了Event前缀)的private的delegate类
                     * 型成员变量（这点可以用Reflector证实）。因为private成
                     * 员变量无法在基类中进行修改，所以为了能够拿到base 
                     * class中声明的事件，要从EventInfo的DeclaringType来获取
                     * event对应的成员变量的FieldInfo并进行修改
                     ********************************************************/
                    FieldInfo fi = ei.DeclaringType.GetField("Event" + ei.Name, bindingFlags);
                    if (fi != null)
                    {
                        // 将event对应的字段设置成null即可清除所有挂钩在该event上的delegate
                        fi.SetValue(ctrl, null);
                    }
                }
                catch { }
            }
        }

        private BindingSource bindingSource1 = new BindingSource();
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        private void button3_Click(object sender, EventArgs e)
        {
           
            try
            {
                switch(int.Parse((string)tabControl1.SelectedTab.Tag) ) 
                {
                    case 1:
                        // TODO: 这行代码将数据加载到表“qds114325507_dbDataSet.THos1”中。您可以根据需要移动或删除它。
                        this.tHos1TableAdapter.Fill(this.qds114325507_dbDataSet.THos1);
                        break;
                    case 2:
                        // TODO: 这行代码将数据加载到表“qds114325507_dbDataSet1.THosLog2”中。您可以根据需要移动或删除它。
                        this.tHosLog2TableAdapter.Fill(this.qds114325507_dbDataSet1.THosLog2);
                        break;
                    case 3:
                        // TODO: 这行代码将数据加载到表“qds114325507_dbDataSet2.THosUserRec1”中。您可以根据需要移动或删除它。
                        this.tHosUserRec1TableAdapter.Fill(this.qds114325507_dbDataSet2.THosUserRec1);
                        break;
                }

               
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>  
        /// 获取局域网内的所有数据库服务器名称  
        /// </summary>  
        /// <returns>服务器名称数组</returns>  
        public List<string> GetSqlServerNames()
        {
            DataTable dataSources = SqlClientFactory.Instance.CreateDataSourceEnumerator().GetDataSources();

            DataColumn column = dataSources.Columns["InstanceName"];
            DataColumn column2 = dataSources.Columns["ServerName"];

            DataRowCollection rows = dataSources.Rows;
            List<string> Serverlist = new List<string>();
            string array = string.Empty;
            for (int i = 0; i < rows.Count; i++)
            {
                string str2 = rows[i][column2] as string;
                string str = rows[i][column] as string;
                if (((str == null) || (str.Length == 0)) || ("MSSQLSERVER" == str))
                {
                    array = str2;
                }
                else
                {
                    array = str2 + @"/" + str;
                }

                Serverlist.Add(array);
            }

            Serverlist.Sort();

            return Serverlist;
        }

        /// <summary>  
        /// 查询sql中的非系统库  
        /// </summary>  
        /// <param name="connection"></param>  
        /// <returns></returns>  
        public List<string> databaseList(string connection)
        {
            List<string> getCataList = new List<string>();
            string cmdStirng = "select name from sys.databases where database_id > 4";
            SqlConnection connect = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand(cmdStirng, connect);
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                    IDataReader dr = cmd.ExecuteReader();
                    getCataList.Clear();
                    while (dr.Read())
                    {
                        getCataList.Add(dr["name"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (SqlException e)
            {
                //MessageBox.Show(e.Message);  
            }
            finally
            {
                if (connect != null && connect.State == ConnectionState.Open)
                {
                    connect.Dispose();
                }
            }
            return getCataList;
        }

        /// <summary>  
        /// 获取列名  
        /// </summary>  
        /// <param name="connection"></param>  
        /// <returns></returns>  
        public List<string> GetTables(string connection)
        {
            List<string> tablelist = new List<string>();
            SqlConnection objConnetion = new SqlConnection(connection);
            try
            {
                if (objConnetion.State == ConnectionState.Closed)
                {
                    objConnetion.Open();
                    DataTable objTable = objConnetion.GetSchema("Tables");
                    foreach (DataRow row in objTable.Rows)
                    {
                        tablelist.Add(row[2].ToString());
                    }
                }
            }
            catch
            {

            }
            finally
            {
                if (objConnetion != null && objConnetion.State == ConnectionState.Closed)
                {
                    objConnetion.Dispose();
                }

            }
            return tablelist;
        }

        /// <summary>  
        /// 获取字段  
        /// </summary>  
        /// <param name="connection"></param>  
        /// <param name="TableName"></param>  
        /// <returns></returns>  
        public List<string> GetColumnField(string connection, string TableName)
        {
            List<string> Columnlist = new List<string>();
            SqlConnection objConnetion = new SqlConnection(connection);
            try
            {
                if (objConnetion.State == ConnectionState.Closed)
                {
                    objConnetion.Open();
                }

                SqlCommand cmd = new SqlCommand("Select Name FROM SysColumns Where id=Object_Id('" + TableName + "')", objConnetion);
                SqlDataReader objReader = cmd.ExecuteReader();

                while (objReader.Read())
                {
                    Columnlist.Add(objReader[0].ToString());

                }
            }
            catch
            {

            }
            objConnetion.Close();
            return Columnlist;
        }

        /// <summary>  
        /// 返回Mdb<a href="http://lib.csdn.net/base/14" class='replace_word' title="MySQL知识库" target='_blank' style='color:#df3434; font-weight:bold;'>数据库</a>中所有表表名  
        /// </summary>  
        public string[] GetShemaTableName(string database_path, string database_password)
        {
            OleDbConnection conn = new OleDbConnection();
            try
            {
                //获取数据表  
              
                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:DataBase Password='" + database_password + "Data Source=" + database_path;
                conn.Open();
                DataTable shemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                int n = shemaTable.Rows.Count;
                string[] strTable = new string[n];
                int m = shemaTable.Columns.IndexOf("TABLE_NAME");
                for (int i = 0; i < n; i++)
                {
                    DataRow m_DataRow = shemaTable.Rows[i];
                    strTable[i] = m_DataRow.ItemArray.GetValue(m).ToString();
                }
                return strTable;
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("指定的限制集无效:/n" + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        /// <summary>  
        /// 返回某一表的所有字段名  
        /// </summary>  
        public string[] GetTableColumn(string database_path, string varTableName)
        {
            OleDbConnection conn = new OleDbConnection();
            DataTable dt = new DataTable();
            try
            {
                conn = new OleDbConnection();
                conn.ConnectionString = "Provider = Microsoft.Jet.OleDb.4.0;Data Source=" + database_path;
                conn.Open();
                dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, varTableName, null });
                int n = dt.Rows.Count;
                string[] strTable = new string[n];
                int m = dt.Columns.IndexOf("COLUMN_NAME");
                for (int i = 0; i < n; i++)
                {
                    DataRow m_DataRow = dt.Rows[i];
                    strTable[i] = m_DataRow.ItemArray.GetValue(m).ToString();
                }
                return strTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }  


        private void comboBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sqlCnt.State != ConnectionState.Open)
                return;
            if (string.IsNullOrEmpty(comboBox1.Text))
                return;
            string[] shosindex=comboBox1.Text.Split(',');
            //int ihosindex=int.Parse(shosindex[0]);
            //string ssqlbefore = this.tHos1TableAdapter.Adapter.SelectCommand.CommandText;
            string sss = "SELECT Id, Active_ID, b_VoltLow, bConnect, Reader_ID, Readtime, str_ReaderIP, Tag_ID, Value1, Value2, Value3, Uid,"
                +"MotherName, status, b_left, Left_time, Left_hour, Left_min, sec_overtime, Last_time, event_time, up_time, sec_upalt,"
                +"b_in_load, b_in_unload, b_in_warning, Record_time, RFID_remark, s_tel, Tag_RoomId, Tag_BedId, SqlSendTime,"
                + "HospitalName FROM THos" + shosindex[0];
            if (this.tHos1TableAdapter.CommandCollection.Length > 0)
            {
                this.tHos1TableAdapter.Adapter.SelectCommand = new SqlCommand(sss);
                this.tHos1TableAdapter.CommandCollection[0].CommandText = sss;
            }
            string sss1 = "SELECT Id, log_type, Info_num, Info_value, Controler, log_logtime, log_infoclass, log_discribe, SqlSendTime,"
                +"HospitalName FROM dbo.THosLog"+ shosindex[0];
            if (this.tHosLog2TableAdapter.CommandCollection.Length > 0)
            {
                this.tHosLog2TableAdapter.Adapter.SelectCommand = new SqlCommand(sss1);
                this.tHosLog2TableAdapter.CommandCollection[0].CommandText = sss1;
            }
            string sss2 = " SELECT Id, Tag_ID, Record_time, LogOff_time, SqlSendTime, HospitalName FROM dbo.THosUserRec" + shosindex[0];
            if (this.tHosUserRec1TableAdapter.CommandCollection.Length > 0)
            {
                this.tHosUserRec1TableAdapter.Adapter.SelectCommand = new SqlCommand(sss2);
                this.tHosUserRec1TableAdapter.CommandCollection[0].CommandText = sss2;
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (sqlCnt.State != ConnectionState.Open)
                return;
            //this.tagInfoTableTableAdapter.GetData();
            //this.tagInfoTableTableAdapter.Fill(this.qds167524424_dbDataSet.TagInfoTable);
            comboBox1.Items.Clear();
            try
            {
                string sql = "select * from HosReg";
                SqlCommand cmd = new SqlCommand(sql, sqlCnt);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string ss = reader["ID"].ToString().Trim() + "," + reader["HOSPITALNAME"].ToString().Trim();
                    comboBox1.Items.Add(ss);
                }
                reader.Close();
            }
            catch
            {
            }
            comboBox1.SelectedIndex = comboBox1.Items.Count > 0 ? 0 : -1;
        }

        private void dataGridView1_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is string)
                e.Value = e.Value.ToString().Trim();
        }
    }
}
