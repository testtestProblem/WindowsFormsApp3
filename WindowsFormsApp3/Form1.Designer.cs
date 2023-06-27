
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button_openSerial = new System.Windows.Forms.Button();
            this.label_biosName = new System.Windows.Forms.Label();
            this.comboBox_comPortSelecter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
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
            this.checkBox_text = new System.Windows.Forms.CheckBox();
            this.checkBox_excel = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_openSerial
            // 
            this.button_openSerial.Font = new System.Drawing.Font("PMingLiU", 20F);
            this.button_openSerial.Location = new System.Drawing.Point(12, 94);
            this.button_openSerial.Name = "button_openSerial";
            this.button_openSerial.Size = new System.Drawing.Size(222, 49);
            this.button_openSerial.TabIndex = 0;
            this.button_openSerial.Text = "open serial port";
            this.button_openSerial.UseVisualStyleBackColor = true;
            this.button_openSerial.Click += new System.EventHandler(this.button_openSerial_Click);
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
            dataGridViewCellStyle7.Font = new System.Drawing.Font("PMingLiU", 15F);
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("PMingLiU", 15F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.indexGrid,
            this.powerGrid,
            this.voltageGrid,
            this.ampereGrid});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("PMingLiU", 15F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle9;
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
            // 
            // checkBox_text
            // 
            this.checkBox_text.AutoSize = true;
            this.checkBox_text.Location = new System.Drawing.Point(419, 97);
            this.checkBox_text.Name = "checkBox_text";
            this.checkBox_text.Size = new System.Drawing.Size(41, 16);
            this.checkBox_text.TabIndex = 17;
            this.checkBox_text.Text = "text";
            this.checkBox_text.UseVisualStyleBackColor = true;
            // 
            // checkBox_excel
            // 
            this.checkBox_excel.AutoSize = true;
            this.checkBox_excel.Location = new System.Drawing.Point(419, 116);
            this.checkBox_excel.Name = "checkBox_excel";
            this.checkBox_excel.Size = new System.Drawing.Size(48, 16);
            this.checkBox_excel.TabIndex = 18;
            this.checkBox_excel.Text = "excel";
            this.checkBox_excel.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 306);
            this.Controls.Add(this.checkBox_excel);
            this.Controls.Add(this.checkBox_text);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_reflashRate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lable_biosBom);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label_time);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox_comPortSelecter);
            this.Controls.Add(this.label_biosName);
            this.Controls.Add(this.button_openSerial);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_openSerial;
        private System.Windows.Forms.Label label_biosName;
        private System.Windows.Forms.ComboBox comboBox_comPortSelecter;
        private System.Windows.Forms.Label label3;
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
        private System.Windows.Forms.CheckBox checkBox_text;
        private System.Windows.Forms.CheckBox checkBox_excel;
    }
}

