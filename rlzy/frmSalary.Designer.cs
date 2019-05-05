namespace rlzy
{
    partial class frmSalary
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textSalaryRange = new System.Windows.Forms.TextBox();
            this.textOtherMonth = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textOtherYear = new System.Windows.Forms.TextBox();
            this.radioButton_Current = new System.Windows.Forms.RadioButton();
            this.radioButton_Other = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textCurrentYear = new System.Windows.Forms.TextBox();
            this.textCurrentMonth = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "工资核算范围";
            // 
            // textSalaryRange
            // 
            this.textSalaryRange.Location = new System.Drawing.Point(138, 16);
            this.textSalaryRange.Name = "textSalaryRange";
            this.textSalaryRange.Size = new System.Drawing.Size(26, 21);
            this.textSalaryRange.TabIndex = 1;
            this.textSalaryRange.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // textOtherMonth
            // 
            this.textOtherMonth.Location = new System.Drawing.Point(138, 70);
            this.textOtherMonth.Name = "textOtherMonth";
            this.textOtherMonth.Size = new System.Drawing.Size(26, 21);
            this.textOtherMonth.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(125, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "计算";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.Location = new System.Drawing.Point(12, 194);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1038, 275);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.Visible = false;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            // 
            // textOtherYear
            // 
            this.textOtherYear.Location = new System.Drawing.Point(165, 70);
            this.textOtherYear.Name = "textOtherYear";
            this.textOtherYear.Size = new System.Drawing.Size(49, 21);
            this.textOtherYear.TabIndex = 6;
            // 
            // radioButton_Current
            // 
            this.radioButton_Current.AutoSize = true;
            this.radioButton_Current.Location = new System.Drawing.Point(20, 45);
            this.radioButton_Current.Name = "radioButton_Current";
            this.radioButton_Current.Size = new System.Drawing.Size(71, 16);
            this.radioButton_Current.TabIndex = 7;
            this.radioButton_Current.TabStop = true;
            this.radioButton_Current.Text = "当前期间";
            this.radioButton_Current.UseVisualStyleBackColor = true;
            this.radioButton_Current.CheckedChanged += new System.EventHandler(this.RadioButton1_CheckedChanged_1);
            // 
            // radioButton_Other
            // 
            this.radioButton_Other.AutoSize = true;
            this.radioButton_Other.Location = new System.Drawing.Point(20, 72);
            this.radioButton_Other.Name = "radioButton_Other";
            this.radioButton_Other.Size = new System.Drawing.Size(71, 16);
            this.radioButton_Other.TabIndex = 8;
            this.radioButton_Other.TabStop = true;
            this.radioButton_Other.Text = "其他期间";
            this.radioButton_Other.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textCurrentYear);
            this.groupBox1.Controls.Add(this.textCurrentMonth);
            this.groupBox1.Controls.Add(this.radioButton_Other);
            this.groupBox1.Controls.Add(this.textOtherYear);
            this.groupBox1.Controls.Add(this.radioButton_Current);
            this.groupBox1.Controls.Add(this.textOtherMonth);
            this.groupBox1.Controls.Add(this.textSalaryRange);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1038, 130);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "工资核算期间";
            this.groupBox1.Enter += new System.EventHandler(this.GroupBox1_Enter);
            // 
            // textCurrentYear
            // 
            this.textCurrentYear.Location = new System.Drawing.Point(165, 43);
            this.textCurrentYear.Name = "textCurrentYear";
            this.textCurrentYear.ReadOnly = true;
            this.textCurrentYear.Size = new System.Drawing.Size(49, 21);
            this.textCurrentYear.TabIndex = 14;
            // 
            // textCurrentMonth
            // 
            this.textCurrentMonth.Location = new System.Drawing.Point(138, 43);
            this.textCurrentMonth.Name = "textCurrentMonth";
            this.textCurrentMonth.ReadOnly = true;
            this.textCurrentMonth.Size = new System.Drawing.Size(26, 21);
            this.textCurrentMonth.TabIndex = 13;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 40);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(20, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "考勤工资扣";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(256, 22);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "test";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // frmSalary
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1062, 498);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MaximizeBox = false;
            this.Name = "frmSalary";
            this.Text = "frmSalary";
            this.Load += new System.EventHandler(this.FrmSalary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textSalaryRange;
        private System.Windows.Forms.TextBox textOtherMonth;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textOtherYear;
        private System.Windows.Forms.RadioButton radioButton_Current;
        private System.Windows.Forms.RadioButton radioButton_Other;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textCurrentYear;
        private System.Windows.Forms.TextBox textCurrentMonth;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.DataGridView dataGridView1;
    }
}