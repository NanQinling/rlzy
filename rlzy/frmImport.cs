using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServerTools;
using rlzy.Models;
using ab;

namespace rlzy
{
    public partial class frmImport : Form
    {
        public frmImport()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Title = "请选择要导入的文件";
            ////设置对话框可以多选
            //ofd.Multiselect = true;
            ////设置对话框的初始目录
            //ofd.InitialDirectory = @"";
            //ofd.Filter = "Excel文件(*.xls,xlsx)|*.xls;*.xlsx";
            //ofd.ShowDialog();

            ExcelHelper excelHelper = new ExcelHelper(AppDomain.CurrentDomain.BaseDirectory + "考勤汇总表.xlsx");

            DataTable dt2 = excelHelper.ExcelToDataTable("Sheet1", true);

            //man[] men;
            //var dt = new System.Data.DataTable();
            //var dt1 = new System.Data.DataTable();
            ////dt获取所有员工工号和姓名后
            //string s1 = "select 人员编号,姓名 from emp_bas where 结束日期='99991231'";
            //dt = Hc_db.get_datatable(s1);
            //men = new man[dt.Rows.Count];
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    men[i] = new man(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
            //    //dt1获取指定ID员工的工资类目名称和对应金额，比如ID=123的员工，书报费，25；洗礼费，50
            //    dt1 = Hc_db.get_datatable("select 工资名目,金额 from 查询1 where 员工ID='" + dt.Rows[i][0].ToString() + "'");
            //       //即：dt1=hc_db.get_table("select 工资名目,金额 from 查询1 where 员工ID='"+dt.Rows[i][0].ToString()+"'"
            //       men[i].pay_name = new string[dt1.Rows.Count];
            //    men[i].pay = new float[dt1.Rows.Count];
            //    for (int j = 0; j < dt1.Rows.Count; j++)
            //    {
            //        men[i].pay_name[j] = dt1.Rows[j][0].ToString();
            //        men[i].pay[j] = float.Parse(dt1.Rows[j][1].ToString());
            //    }
            //    men[i].set_kqb();
            //}



            //string s1 = "select 人员编号,姓名 from emp_bas where 结束日期='99991231'";
            //var dt = Hc_db.get_datatable(s1);
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                string id = dt2.Rows[i][0].ToString();
                string name = dt2.Rows[i][1].ToString();

                Hc_db.do_nonquery("insert into emp_remuneration (人员编号,考勤项,金额) value ");
            }










        }

    }
}
