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
using rlzy;

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
            string last_year_month = "";
            string current_date_gz = "";
            string current_date_jj = "";
            string current_year_month = "";

            if (textSalaryRange.Text == string.Empty || (radioButton_Current.Checked == false && radioButton_Other.Checked == false))
            {
                MessageBox.Show("请输入所有必需的条目字段");
                textSalaryRange.Focus();
                return;
            }

            salary_range = textSalaryRange.Text.ToUpper();
            textSalaryRange.Text = salary_range;

            if (radioButton_Current.Checked == true)
            {
                if (textOtherYear.Text == string.Empty && textOtherMonth.Text == string.Empty)
                {
                    Salary.GetDate(out last_year_month, out current_date_gz, out current_date_jj, out current_year_month);

                    textCurrentYear.Text = current_year_month.Substring(0, 4);
                    textCurrentMonth.Text = current_year_month.Substring(4, 2);
                }
                else
                {
                    MessageBox.Show("请不要输入期间");
                    return;
                }
            }

            if (radioButton_Other.Checked == true)
            {
                textCurrentYear.Visible = false;
                textCurrentMonth.Visible = false;

                if (textOtherYear.Text == string.Empty || textOtherYear.Text.Length < 4)
                {
                    MessageBox.Show("请输入工资核算年份");
                    textOtherYear.Focus();
                    return;
                }
                else if (textOtherMonth.Text == string.Empty || textOtherMonth.Text.Length > 2)
                {
                    MessageBox.Show("请输入工资核算月份");
                    textOtherMonth.Focus();
                    return;
                }
                else
                {
                    if (textOtherMonth.Text.Length == 1)
                    {
                        current_date_gz = textOtherYear.Text + "0" + textOtherMonth.Text + "01";
                    }
                    else
                    {
                        current_date_gz = textOtherYear.Text + textOtherMonth.Text + "01";
                    }
                    current_year_month = current_date_gz.Substring(0, 6);

                    textOtherYear.Text = current_year_month.Substring(0, 4);
                    textOtherMonth.Text = current_year_month.Substring(4, 2);

                    string s2 = "SELECT 发放日期 FROM ffb WHERE ffb.工资范围='VN' AND ffb.发放类型='工资'";
                    Hc_db.do_nonquery(s2);
                    DataTable dt_ffrq = Hc_db.get_datatable(s2);
                    string[] str_GZFF = new string[dt_ffrq.Rows.Count];     //判断输入的工资年月是否正常，需要添加一个库
                    for (int i = 0; i < dt_ffrq.Rows.Count; i++)
                    {
                        str_GZFF[i] = dt_ffrq.Rows[i]["发放日期"].ToString().Substring(0, 6);
                    }

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

            if (b == true)
            {
                dataGridView1.Visible = true;
                DataTable dt_sum_salary = Salary.GetSalarySum(salary_range, current_date_gz, current_year_month);
                dataGridView1.DataSource = dt_sum_salary;
            }


        }
















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
            textSalaryRange.Text = "VN";
            radioButton_Current.Checked = true;
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {

            Salary.GetDate(out string last_year_month, out string current_date_gz, out string current_date_jj, out string current_year_month);
            Salary.GetAttendanceGZK(last_year_month);
        }

        private void Button3_Click(object sender, EventArgs e)
        {

            Salary.GetDate(out string last_year_month, out string current_date_gz, out string current_date_jj, out string current_year_month);
            MessageBox.Show(last_year_month);
            MessageBox.Show(current_date_gz);
            MessageBox.Show(current_date_jj);
            MessageBox.Show(current_year_month);
        }
    }
}
