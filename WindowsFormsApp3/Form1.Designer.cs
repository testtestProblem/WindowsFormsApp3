
namespace WindowsFormsApp3
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button1 = new System.Windows.Forms.Button();
            this.label_biosName = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_comPortSelecter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.testCommand1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label_time = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.indexGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.powerGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voltageGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ampereGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.lable_biosBom = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_reflashRate = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("PMingLiU", 20F);
            this.button1.Location = new System.Drawing.Point(12, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(222, 49);
            this.button1.TabIndex = 0;
            this.button1.Text = "open serial port";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_biosName
            // 
            this.label_biosName.AutoSize = true;
            this.label_biosName.Font = new System.Drawing.Font("PMingLiU", 17F);
            this.label_biosName.Location = new System.Drawing.Point(8, 68);
            this.label_biosName.Name = "label_biosName";
            this.label_biosName.Size = new System.Drawing.Size(81, 23);
            this.label_biosName.TabIndex = 1;
            this.label_biosName.Text = "unknow";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("PMingLiU", 16F);
            this.button2.Location = new System.Drawing.Point(384, 324);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 61);
            this.button2.TabIndex = 2;
            this.button2.Text = "check battery";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("PMingLiU", 10F);
            this.label2.Location = new System.Drawing.Point(150, 371);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "receive data";
            this.label2.Visible = false;
            // 
            // comboBox_comPortSelecter
            // 
            this.comboBox_comPortSelecter.DropDownHeight = 150;
            this.comboBox_comPortSelecter.DropDownWidth = 50;
            this.comboBox_comPortSelecter.FormattingEnabled = true;
            this.comboBox_comPortSelecter.IntegralHeight = false;
            this.comboBox_comPortSelecter.Items.AddRange(new object[] {
            "COM0",
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11"});
            this.comboBox_comPortSelecter.Location = new System.Drawing.Point(240, 114);
            this.comboBox_comPortSelecter.MaxDropDownItems = 12;
            this.comboBox_comPortSelecter.Name = "comboBox_comPortSelecter";
            this.comboBox_comPortSelecter.Size = new System.Drawing.Size(63, 20);
            this.comboBox_comPortSelecter.TabIndex = 4;
            this.comboBox_comPortSelecter.Text = "COM0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("PMingLiU", 10F);
            this.label3.Location = new System.Drawing.Point(237, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "com port";
            // 
            // testCommand1
            // 
            this.testCommand1.Font = new System.Drawing.Font("PMingLiU", 12F);
            this.testCommand1.Location = new System.Drawing.Point(220, 397);
            this.testCommand1.Name = "testCommand1";
            this.testCommand1.Size = new System.Drawing.Size(54, 49);
            this.testCommand1.TabIndex = 6;
            this.testCommand1.Text = "Bios\r\nname";
            this.testCommand1.UseVisualStyleBackColor = true;
            this.testCommand1.Visible = false;
            this.testCommand1.Click += new System.EventHandler(this.testCommand1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(132, 389);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "battery index :??";
            this.label4.UseWaitCursor = true;
            this.label4.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(308, 397);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(50, 49);
            this.button3.TabIndex = 9;
            this.button3.Text = "clear";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label_time
            // 
            this.label_time.AutoSize = true;
            this.label_time.Font = new System.Drawing.Font("PMingLiU", 11F);
            this.label_time.Location = new System.Drawing.Point(361, 6);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(37, 15);
            this.label_time.TabIndex = 10;
            this.label_time.Text = "Time";
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("PMingLiU", 15F);
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("PMingLiU", 15F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.indexGrid,
            this.powerGrid,
            this.voltageGrid,
            this.ampereGrid});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("PMingLiU", 15F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.Location = new System.Drawing.Point(12, 149);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(508, 151);
            this.dataGridView1.TabIndex = 11;
            // 
            // indexGrid
            // 
            this.indexGrid.Frozen = true;
            this.indexGrid.HeaderText = "index";
            this.indexGrid.Name = "indexGrid";
            this.indexGrid.ReadOnly = true;
            this.indexGrid.Width = 75;
            // 
            // powerGrid
            // 
            this.powerGrid.Frozen = true;
            this.powerGrid.HeaderText = "power";
            this.powerGrid.Name = "powerGrid";
            this.powerGrid.ReadOnly = true;
            this.powerGrid.Width = 130;
            // 
            // voltageGrid
            // 
            this.voltageGrid.Frozen = true;
            this.voltageGrid.HeaderText = "voltage(mV)";
            this.voltageGrid.Name = "voltageGrid";
            this.voltageGrid.ReadOnly = true;
            this.voltageGrid.Width = 130;
            // 
            // ampereGrid
            // 
            this.ampereGrid.Frozen = true;
            this.ampereGrid.HeaderText = "ampere(mA)";
            this.ampereGrid.Name = "ampereGrid";
            this.ampereGrid.ReadOnly = true;
            this.ampereGrid.Width = 130;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "BIOS BOM:";
            // 
            // lable_biosBom
            // 
            this.lable_biosBom.AutoSize = true;
            this.lable_biosBom.Font = new System.Drawing.Font("PMingLiU", 17F);
            this.lable_biosBom.Location = new System.Drawing.Point(8, 24);
            this.lable_biosBom.Name = "lable_biosBom";
            this.lable_biosBom.Size = new System.Drawing.Size(81, 23);
            this.lable_biosBom.TabIndex = 13;
            this.lable_biosBom.Text = "unknow";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "BIOS name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("PMingLiU", 10F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(321, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 14);
            this.label1.TabIndex = 16;
            this.label1.Text = "Refresh speed";
            // 
            // comboBox_reflashRate
            // 
            this.comboBox_reflashRate.DropDownHeight = 150;
            this.comboBox_reflashRate.DropDownWidth = 50;
            this.comboBox_reflashRate.FormattingEnabled = true;
            this.comboBox_reflashRate.IntegralHeight = false;
            this.comboBox_reflashRate.Items.AddRange(new object[] {
            "Rapid",
            "Normal",
            "Slow",
            "Stop"});
            this.comboBox_reflashRate.Location = new System.Drawing.Point(324, 114);
            this.comboBox_reflashRate.MaxDropDownItems = 12;
            this.comboBox_reflashRate.Name = "comboBox_reflashRate";
            this.comboBox_reflashRate.Size = new System.Drawing.Size(63, 20);
            this.comboBox_reflashRate.TabIndex = 15;
            this.comboBox_reflashRate.Text = "Normal";
            this.comboBox_reflashRate.SelectedIndexChanged += new System.EventHandler(this.comboBox_reflashRate_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 302);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_reflashRate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lable_biosBom);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label_time);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.testCommand1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox_comPortSelecter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label_biosName);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_biosName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_comPortSelecter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button testCommand1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn powerGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn voltageGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ampereGrid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lable_biosBom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_reflashRate;
    }
}

