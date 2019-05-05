#define WIN
//#define NET
//********ACCESS要开类型和版本两个******
#define ACCESS
//#define ACCESS_2003
#define ACCESS_2010
//********EXCEL开一个具体版本就行了******
//#define EXCEL
//#define EXCEL_2010
//********************************
//#define SQLSERVER
//(sql2000测试通过)
//*******************************
//#define MYSQL

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
#if NET
using System.Web;
#endif

namespace ab
{
    //操作access等数据库的类
    class Hc_db
    {
        public Hc_db()
        {
        }
#if SQLSERVER
            static SqlConnection createConn()
#elif MYSQL
            static MySqlConnection createConn()
#else
        static OleDbConnection createConn()
#endif
        {
#if SQLSERVER
                string s1 = "server=10.22.30.240;initial catalog=d1;user ID=sa;password=123456;";
#endif
#if ACCESS_2003
                string s1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
#endif
#if ACCESS_2010
            string s1 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
#endif
#if ACCESS
#if NET
                    string s2 =@HttpContext.Current.Server.MapPath(@"~/data/d1.accdb"); 
#endif
#if WIN
            string s2 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\data\rlzy.accdb";
#endif
#endif

#if EXCEL_2010
            string s1 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
#if NET
            string s2 = @HttpContext.Current.Server.MapPath(@"~/App_Data/月度汇总.xlsx") + ";Extended Properties='Excel 12.0;HDR=NO'";//HDR=YES表示有标题
#endif
#if WIN
                    string s2 =System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+@"\data\月度汇总.xlsx"+";Extended Properties='Excel 12.0;HDR=YES'";//HDR=YES表示有标题
#endif
            //说明：
            //若要引用完全使用的工作表的范围，请指定后面跟有美元符号的工作表名称。例如： 
            //select * from [Sheet1$] 
            //若要引用工作表上的特定地址范围，请指定后面跟有美元符号和该范围的工作表名称。例如： 
            //select * from [Sheet1$A1:B10]
            //关于IMEX:
            //若为 0，则为输出模式，此情况下只能用作写入 Excel； 
            //若为 1，则为输入模式，此情况下只能用作读取 Excel，并且始终将 Excel 数据作为文本类型读取； 
            //若为 2，则为连接模式，此情况下既可用作写入、也可用作读取。
            //所以若要读取混合数据类型，应该将 IMEX 设置为 1；若误设置为 0，则读取不到任何行；若误设置为 2 或省略，则有些数据读取出来是空白。
            //注意：输出模式对应写入、输入模式对应读取。
#endif
#if MYSQL
            string s1 = "Database=ipt_authd;Data Source=10.10.XXX.XXX;User Id=XXXX;allow zero datetime=true";
            //标准写法：myConnectionString = "Database=Test;Data Source=localhost;User Id=username;Password=pass";
#endif
#if SQLSERVER
                SqlConnection conn = new SqlConnection(s1);
#elif MYSQL
                MySqlConnection conn = new MySqlConnection(s1);
#else
            OleDbConnection conn = new OleDbConnection(s1 + s2);
#endif
            return conn;
        }

        public static DataTable get_datatable(string s1)
        {
#if SQLSERVER
                SqlConnection myconn = Hc_db.createConn();
                SqlDataAdapter myda = new SqlDataAdapter(s1, myconn);
#elif MYSQL
                    MySqlConnection myconn = Hc_db.createConn();                
                    MySqlDataAdapter myda = new MySqlDataAdapter(s1, myconn);
#else
            OleDbConnection myconn = Hc_db.createConn();
            OleDbDataAdapter myda = new OleDbDataAdapter(s1, myconn);
#endif
            DataSet myds = new DataSet();
            try
            {
                myconn.Open();
                myda.Fill(myds, "No1");
                myconn.Close();
                //myconn.Dispose();
                return myds.Tables["No1"];
            }
            catch (Exception e1)
            {
                throw (e1);
            }
        }
        public static bool do_nonquery(string s1)
        {
#if SQLSERVER
                SqlConnection myconn = Hc_db.createConn();
                SqlCommand mycomm = new SqlCommand(s1, myconn);
#elif MYSQL
                MySqlConnection myconn = Hc_db.createConn();
                MySqlCommand mycomm = new MySqlCommand(s1, myconn);
#else
            OleDbConnection myconn = Hc_db.createConn();
            OleDbCommand mycomm = new OleDbCommand(s1, myconn);
#endif
            try
            {
                myconn.Open();
                mycomm.ExecuteNonQuery();
                myconn.Close();
                //myconn.Dispose();
                return true;
            }
            catch (Exception e1)
            {
                //HttpContext.Current.Response.Write("<script language='javascript' defer>alert('" + e1.ToString() + "');</script>");
                throw (e1);
                //return false;
            }
        }









        static OleDbConnection createConn_ex()

        {
            string s1 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";

            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Excel文件(*.xls,xlsx)|*.xls;*.xlsx|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
            }
            string s2 = filePath + ";Extended Properties='Excel 12.0;HDR=YES'";//HDR=YES表示有标题
            OleDbConnection conn = new OleDbConnection(s1 + s2);
            return conn;

            //说明：
            //若要引用完全使用的工作表的范围，请指定后面跟有美元符号的工作表名称。例如： 
            //select * from [Sheet1$] 
            //若要引用工作表上的特定地址范围，请指定后面跟有美元符号和该范围的工作表名称。例如： 
            //select * from [Sheet1$A1:B10]
            //关于IMEX:
            //若为 0，则为输出模式，此情况下只能用作写入 Excel； 
            //若为 1，则为输入模式，此情况下只能用作读取 Excel，并且始终将 Excel 数据作为文本类型读取； 
            //若为 2，则为连接模式，此情况下既可用作写入、也可用作读取。
            //所以若要读取混合数据类型，应该将 IMEX 设置为 1；若误设置为 0，则读取不到任何行；若误设置为 2 或省略，则有些数据读取出来是空白。
            //注意：输出模式对应写入、输入模式对应读取。


        }

        public static DataTable get_datatable_ex(string s1)
        {
            OleDbConnection myconn = Hc_db.createConn_ex();
            OleDbDataAdapter myda = new OleDbDataAdapter(s1, myconn);
            DataSet myds = new DataSet();
            try
            {
                myconn.Open();
                myda.Fill(myds, "No1");
                myconn.Close();
                //myconn.Dispose();
                return myds.Tables["No1"];
            }
            catch (Exception e1)
            {
                throw (e1);
            }
        }









    }
}
//INSERT INTO 表 [(字段1[,字段2[, ...]])]
////VALUES (值1[,值2[, ...])
//            string str_sql = "insert into myt1(姓名,性别,年龄) values('"+textBox2.Text+"','"+textBox3.Text+"',"+textBox4.Text+")";