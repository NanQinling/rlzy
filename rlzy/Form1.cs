using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rlzy
{
    public partial class Form1 : Form
    {
        //public string user_name = "guest";
        //public int power_level = 0;
        //Form1 f1;
        public frmSalary f3;
        public frmImport f4;


        public Form1()
        {
            InitializeComponent();
        }

        private void 资金管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 维护员工工资数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (Form x in MdiChildren)
            {
                x.Close();
            }
            f3 = new frmSalary();
            f3.MdiParent = this;
            f3.WindowState = FormWindowState.Maximized;
            //f3.Parent = panel2;
            f3.Show();
        }

        private void 维护考勤数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 工资管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 批量录入考勤数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form x in MdiChildren)
            {
                x.Close();
            }
            f4 = new frmImport();
            f4.MdiParent = this;
            f4.WindowState = FormWindowState.Maximized;
            //f3.Parent = panel2;
            f4.Show();

        }
    }
}

