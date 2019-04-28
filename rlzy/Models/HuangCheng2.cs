#define WIN
//#define NET
//********ACCESS要开类型和版本两个******
//#define ACCESS
//#define ACCESS_2003
//#define ACCESS_2010
//********EXCEL开一个具体版本就行了******
//(#define EXCEL)
//#define EXCEL_2010
//********************************
#define SQLSERVER
//(sql2000测试通过)
//*******************************
//#define MYSQL

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
//using MySql.Data.MySqlClient;
#if NET
    using System.Web;
#endif

namespace HuangCheng
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
        protected static OleDbConnection createConn()
#endif
        {
#if SQLSERVER
                string s1 = "server=192.168.91.90;initial catalog=d1;user ID=sa;password=123456;";
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
            string s2 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\data\example1.mdb";
#endif
#endif

#if EXCEL_2010
                string s1 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
#if NET
                    string s2 =@HttpContext.Current.Server.MapPath(@"~/data/e1.xlsx")+";Extended Properties='Excel 12.0;HDR=YES'";//HDR=YES表示有标题
#endif
#if WIN
                    string s2 =System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+@"\data\e1.xlsx"+";Extended Properties='Excel 12.0;HDR=YES'";//HDR=YES表示有标题
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
        public static DataTable get_datatable(DbCommand input_comm)//带参查询，例子见后面
        {
#if SQLSERVER
            SqlCommand mycomm = (SqlCommand)input_comm;
            mycomm.Connection = Hc_db.createConn();
            SqlDataAdapter myda = new SqlDataAdapter(mycomm);
#elif MYSQL
            MySqlCommand mycomm = (MySqlCommand)input_comm;
            mycomm.Connection = Hc_db.createConn();               
            MySqlDataAdapter myda = new MySqlDataAdapter(mycomm);
#else
            OleDbCommand mycomm = (OleDbCommand)input_comm;
            mycomm.Connection = Hc_db.createConn();
            OleDbDataAdapter myda = new OleDbDataAdapter(mycomm);
#endif
            DataSet myds = new DataSet();
            try
            {
                myda.Fill(myds, "No1");
                return myds.Tables["No1"];
            }
            catch (Exception e1)
            {
                throw (e1);
            }
        }

        public static int do_nonquery(string s1)
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
                int c;
                myconn.Open();
                c=mycomm.ExecuteNonQuery();
                myconn.Close();
                //myconn.Dispose();
                return c;
            }
            catch (Exception e1)
            {
                //HttpContext.Current.Response.Write("<script language='javascript' defer>alert('" + e1.ToString() + "');</script>");
                throw (e1);
                //return false;
            }
        }

        public static int do_nonquery(DbCommand input_comm)//带参增删改，例子见方法后面
        {
#if SQLSERVER
                SqlConnection myconn = Hc_db.createConn();
                SqlCommand mycomm =(SqlCommand)input_comm;
#elif MYSQL
                MySqlConnection myconn = Hc_db.createConn();
                MySqlCommand mycomm =(MySqlCommand)input_comm;
#else
            OleDbConnection myconn = Hc_db.createConn();
            OleDbCommand mycomm = (OleDbCommand)input_comm;
#endif
            mycomm.Connection = myconn;
            try
            {
                int c;
                myconn.Open();
                c=mycomm.ExecuteNonQuery();
                myconn.Close();
                //myconn.Dispose();
                return c;
            }
            catch (Exception e1)
            {
                //HttpContext.Current.Response.Write("<script language='javascript' defer>alert('" + e1.ToString() + "');</script>");
                throw (e1);
                //return false;
            }
        }

    }
    class Hc_db1 : Hc_db
    {
        //本类自带命令，设置它并且执行它。
        //注意！！！！！！
        //access里执行带参的update时，参数赋值顺序必须与语句中参数出现顺序一致。

        //mysql未验证
#if SQLSERVER
        static SqlCommand this_command;
        
        public static void set_Para(string para_name, int para_value)
        {
            if (this_command == null)
            {
                this_command = new SqlCommand();
            }
            SqlParameter para1 = this_command.Parameters.Add(para_name, OleDbType.Integer);
            para1.Value = para_value;
        }
        public static void set_Para(string para_name, string para_value)
        {
            if (this_command == null)
            {
                this_command = new SqlCommand();
            }
            SqlParameter para1 = this_command.Parameters.Add(para_name, OleDbType.Char);
            para1.Value = para_value;
        }
        public static void set_Para(string para_name, DateTime para_value)
        {
            if (this_command == null)
            {
                this_command = new SqlCommand();
            }
            SqlParameter para1 = this_command.Parameters.Add(para_name, OleDbType.DBDate);
            para1.Value = para_value.ToString("yyyy-MM-dd");
        }
        public static void set_Para(string para_name, double para_value)
        {
            if (this_command == null)
            {
                this_command = new SqlCommand();
            }
            SqlParameter para1 = this_command.Parameters.Add(para_name, OleDbType.Numeric);
            para1.Value = para_value;
        }
#elif MYSQL
                    //暂未添加
#else
        static OleDbCommand this_command;
        public static void set_Para(string para_name, int para_value)
        {
            if (this_command == null)
            {
                this_command = new OleDbCommand();
            }
            OleDbParameter para1 = this_command.Parameters.Add(para_name, OleDbType.Integer);
            para1.Value = para_value;
        }
        public static void set_Para(string para_name, string para_value)
        {
            if (this_command == null)
            {
                this_command = new OleDbCommand();
            }
            OleDbParameter para1 = this_command.Parameters.Add(para_name, OleDbType.Char);
            para1.Value = para_value;
        }
        public static void set_Para(string para_name, DateTime para_value)
        {
            if (this_command == null)
            {
                this_command = new OleDbCommand();
            }
            OleDbParameter para1 = this_command.Parameters.Add(para_name, OleDbType.DBDate);
            para1.Value = para_value.ToString("yyyy-MM-dd");//该日期格式access2000测试通过
        }
        public static void set_Para(string para_name, double para_value)
        {
            if (this_command == null)
            {
                this_command = new OleDbCommand();
            }
            OleDbParameter para1 = this_command.Parameters.Add(para_name, OleDbType.Numeric);
            para1.Value = para_value;
        }
#endif
        public static void set_comm(string sql)
        {
            this_command.CommandText = sql;
        }
        public static DataTable get_datatable()//重载，执行默认命令对象
        {
            DataTable thisdt=get_datatable(this_command);
            this_command.Parameters.Clear();
            return thisdt;
        }
        public static int do_nonquery()
        {
            int thisint = do_nonquery(this_command);
            this_command.Parameters.Clear();
            return thisint;
        }
        public static string get_para()//调试用
        {
            string s = "";
            for (int i = 0; i < this_command.Parameters.Count; i++)
            {
                s += this_command.Parameters[i].ParameterName +" "+ this_command.Parameters[i].Value.ToString() + "\n";
            }
            return s;
        }
    }
}
#region///////////////////////////////////////////////get_datatable1的用法/////////////////////////////////////////////
/*带参查询
    string sql = "select xs.id as 学号,xm as 姓名,nl as 年龄,zy.zym as 专业 from xs,zy where xs.zy=zy.id and nl>@nl";//1

    OleDbCommand mycomm = new OleDbCommand(sql);
    OleDbParameter paras;

    paras= mycomm.Parameters.Add("@nl", OleDbType.Integer);//2
    paras.Value = 19;

    DataTable dt1;
    dt1 = Hc_db.get_datatable1(mycomm);
     */
#endregion
#region//////////////////////////////////////////////do_nonquery1的用法/////////////////////////////////////////
/*带参增删改
        调用方式：
        sql = "delete from xs where id=@id";//1

        OleDbCommand mycomm = new OleDbCommand(sql);
        OleDbParameter paras;

        paras= my_comm.Parameters.Add("@id", OleDbType.VarChar);//2
        paras.Value = id;

        Hc_db.do_nonquery1(my_comm);
            */
#endregion
#region///////////////////////////////////////////////额外用法//////////////////////////////////
/*其他带参参数（存储过程）（未验证）
SqlCommand comm=new SqlCommand()
 string sql = "proc_out";
 comm.CommandText = sql;

 //把Command执行类型改为存储过程方式，默认为Text。
 comm.CommandType = CommandType.StoredProcedure;
 //----------------------只用这里的东西，用exec proc的方式，也应该可以常规执行存储过程-------------------------------------
 //传递一个输入参数，需赋值
 SqlParameter sp = comm.Parameters.Add("@uid", SqlDbType.Int);
 sp.Value = 4;

 //定义一个输出参数，不需赋值。Direction用来描述参数的类型
 //Direction默认为输入参数，还有输出参数和返回值型。
 sp = comm.Parameters.Add("@output", SqlDbType.VarChar, 50);
 sp.Direction = ParameterDirection.Output;

 //定义过程的返回值参数，过程执行完之后，将把过程的返回值赋值给名为myreturn的Paremeters赋值。
 sp = comm.Parameters.Add("myreturn", SqlDbType.Int);
 sp.Direction = ParameterDirection.ReturnValue;
 dt1 = Hc_db.get_datatable1(mycomm);或Hc_db.do_nonquery1(my_comm);二选一
 //---------------------------------------------------------
 */
#endregion
#region/////////////////////////////////////////////其他///////////////////////////////////////
//INSERT INTO 表 [(字段1[,字段2[, ...]])]
////VALUES (值1[,值2[, ...])
//            string str_sql = "insert into myt1(姓名,性别,年龄) values('"+textBox2.Text+"','"+textBox3.Text+"',"+textBox4.Text+")";
#endregion

