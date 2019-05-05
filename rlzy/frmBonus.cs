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
    public partial class frmBonus : Form
    {
        public frmBonus()
        {
            InitializeComponent();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {



        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Salary.GetDate(out string last_year_month, out string current_date_gz, out string current_date_jj, out string current_year_month);
            Salary.GetAttendanceYJK(last_year_month, current_date_jj);
        }
    }
}
