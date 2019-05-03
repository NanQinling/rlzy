using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ab;

namespace rlzy
{
    public partial class frmSalary : Form
    {
        public frmSalary()
        {
            InitializeComponent();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //先检查输入的数据
            bool b = false;
            string salary_range = "";
            string current_date = "";
            string current_year_month = "";
            if (textBox1.Text == string.Empty || (radioButton1.Checked == false && radioButton2.Checked == false))
            {
                MessageBox.Show("请输入所有必需的条目字段");
                textBox1.Focus();
                return;
            }
            else
            {
                salary_range = textBox1.Text.ToUpper();
                textBox1.Text = salary_range;
            }

            if (radioButton1.Checked == true)       //当前期间判断，要根据当前工资库获取最新的工资年月。
            {
                if (textBox3.Text == string.Empty && textBox2.Text == string.Empty)
                {
                    current_date = DateTime.Now.ToString("yyyyMMdd");
                    current_year_month = DateTime.Now.ToString("yyyyMMdd").Substring(0, 6);
                    textBox6.Text = current_year_month.Substring(0, 4);
                    textBox7.Text = current_year_month.Substring(4, 2);
                }
                else
                {
                    MessageBox.Show("请不要输入期间");
                    return;
                }
            }

            if (radioButton2.Checked == true)       //其他期间判断  textbox2：月  textbox3：年
            {
                textBox6.Visible = false;
                textBox7.Visible = false;

                if (textBox3.Text == string.Empty || textBox3.Text.Length<4)
                {
                    MessageBox.Show("请输入工资核算年份");
                    textBox3.Focus();
                    return;
                }
                else if (textBox2.Text == string.Empty || textBox2.Text.Length >2)
                {
                    MessageBox.Show("请输入工资核算月份");
                    textBox2.Focus();
                    return;
                }
                else
                {
                    if (textBox2.Text.Length == 1)
                    {
                        current_date = textBox3.Text + "0" + textBox2.Text + "01";
                    }
                    else
                    {
                        current_date = textBox3.Text + textBox2.Text + "01";
                    }
                    current_year_month = current_date.Substring(0, 6);

                    textBox3.Text = current_year_month.Substring(0, 4);
                    textBox2.Text = current_year_month.Substring(4, 2);

                    string[] str_GZFF = { "201905", "201906", "201907", "201908", "201909", "201910", "201911", "201912" };     //判断输入的工资年月是否正常，需要添加一个库
                    if (str_GZFF.Contains(current_year_month))
                    {

                    }
                    else
                    {
                        MessageBox.Show("工资发放期不正确");
                        return;
                    }
                }

            }

            b = true;





            //执行工资计算并显示工资表
            if (b == true)
            {
                groupBox1.Visible = false;
                dataGridView1.Visible = true;
                GetSalarySum(salary_range, current_date, current_year_month);
            }


        }




        /// <summary>
        /// 生成工资表
        /// </summary>
        void GetSalarySum(string salary_range, string current_date, string current_year_month)
        {
            string s1 = $"select emp_bas.所属二级机构,emp_bas.所属三级机构,emp_bas.人员编号,emp_bas.姓名,emp_bas.身份证号," + $"Sum(IIf(dt_res.工资项='9010',dt_res.金额,0))+Sum(IIf(dt_res.工资项='9050',dt_res.金额,0))+" + $"Sum(IIf(dt_res.工资项='1000',dt_res.金额,0))+Sum(IIf(dt_res.工资项='1099',dt_res.金额,0)) AS 岗位工资," + $"Sum(IIf(dt_res.工资项='3000',dt_res.金额,0))+Sum(IIf(dt_res.工资项='3099',dt_res.金额,0)) AS 年功工资," + $"Sum(IIf(dt_res.工资项='2090',dt_res.金额,0)) AS 书报费," + $"Sum(IIf(dt_res.工资项='2080',dt_res.金额,0)) AS 洗理费," + $"Sum(IIf(dt_res.工资项='2120',dt_res.金额,0))+Sum(IIf(dt_res.工资项='2999',dt_res.金额,0)) AS 其它津补贴," + $"Sum(IIf(dt_res.工资项='2328',dt_res.金额,0)) AS 副食补贴, " + $"Sum(IIf(dt_res.工资项='2314',dt_res.金额,0)) AS 知识分子补助," + $"Sum(IIf(dt_res.工资项='2316',dt_res.金额,0)) AS 保健津贴," + $"Sum(IIf(dt_res.工资项='2030',dt_res.金额,0)) AS 运龄津贴," + $"Sum(IIf(dt_res.工资项='2010',dt_res.金额,0)) AS 夜班津贴," + $"Sum(IIf(dt_res.工资项='2304',dt_res.金额,0)) AS 通讯补贴," + $"Sum(IIf(dt_res.工资项='3500',dt_res.金额,0)) AS 加班工资," + $"Sum(IIf(dt_res.工资项='1040',dt_res.金额,0)) AS 缺勤扣减," + $"岗位工资+年功工资+书报费+洗理费+其它津补贴+副食补贴+知识分子补助+保健津贴+运龄津贴+夜班津贴+通讯补贴+加班工资-缺勤扣减 AS 应发合计," + $"Sum(IIf(dt_res.工资项='7180',dt_res.金额,0)) AS 基本养老," + $"Sum(IIf(dt_res.工资项='7290',dt_res.金额,0)) AS 企业年金," + $"Sum(IIf(dt_res.工资项='7220',dt_res.金额,0)) AS 医疗," + $"Sum(IIf(dt_res.工资项='7200',dt_res.金额,0)) AS 失业," + $"Sum(IIf(dt_res.工资项='7160',dt_res.金额,0)) AS 公积金," + $"Sum(IIf(dt_res.工资项='7580',dt_res.金额,0))+Sum(IIf(dt_res.工资项='7570',dt_res.金额,0)) AS 个税," + $"Sum(IIf(dt_res.工资项='7997',dt_res.金额,0))+Sum(IIf(dt_res.工资项='7998',dt_res.金额,0))+Sum(IIf(dt_res.工资项='7999',dt_res.金额,0)) AS 其它扣减," + $"基本养老+企业年金+医疗+失业+公积金+个税+其它扣减 AS 扣减合计," + $"应发合计-扣减合计 AS 实发" + $" from (select dt_sum.人员编号,dt_sum.工资项,sum(dt_sum.金额) as 金额 from (select emp_pay_bas.人员编号,emp_pay_bas.工资项,emp_pay_bas.金额 from emp_pay_bas where '{current_date}' between emp_pay_bas.开始日期 and emp_pay_bas.结束日期 union all select emp_pay_recu.人员编号,emp_pay_recu.工资项,emp_pay_recu.金额 from emp_pay_recu where '{current_date}' between emp_pay_recu.开始日期 and emp_pay_recu.结束日期 union all select emp_pay_addi.人员编号,emp_pay_addi.工资项,emp_pay_addi.金额 from emp_pay_addi where 发放日期 like '{current_year_month}%') as dt_sum group by dt_sum.人员编号,dt_sum.工资项) as dt_res,emp_bas where '{current_date}' between emp_bas.开始日期 and emp_bas.结束日期 and emp_bas.工资范围 = '{salary_range}' and emp_bas.人员编号 = dt_res.人员编号 group by emp_bas.所属二级机构,emp_bas.所属三级机构,emp_bas.人员编号,emp_bas.姓名,emp_bas.身份证号";
            //先将三个表汇总，再按照工资项进行分组求和，再通过工资项进行行转列呈现。
            DataTable dt = Hc_db.get_datatable(s1);
            dataGridView1.DataSource = dt;
        }





        #region 合并三个datatable
        /// <summary>
        /// 对三个表结构相同的表进行数据合并
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="dt3"></param>
        /// <returns>返回的新表格</returns>
        //public DataTable GetAllDataTable(DataTable dt1, DataTable dt2, DataTable dt3)
        //{

        //    DataTable newTable = dt1.Copy();
        //    for (int i = 0; i < dt2.Rows.Count; i++)
        //    {
        //        newTable.Rows.Add(dt2.Rows[i].ItemArray);
        //    }
        //    for (int i = 0; i < dt3.Rows.Count; i++)
        //    {
        //        newTable.Rows.Add(dt3.Rows[i].ItemArray);
        //    }
        //    return newTable;
        //}
        #endregion





        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RadioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void FrmSalary_Load(object sender, EventArgs e)
        {
            //textBox1.Text = "VN";
            //radioButton1.Checked = true;
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
