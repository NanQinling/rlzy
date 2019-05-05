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
    public partial class frmImport : Form
    {
        public frmImport()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            if (this.radioButton1.Checked == true)
            {
                Salary.ImportAttendance();
            }

        }
    }
}