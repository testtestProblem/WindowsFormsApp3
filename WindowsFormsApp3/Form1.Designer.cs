﻿
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.testCommand1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.indexGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.powerGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voltageGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ampereGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("PMingLiU", 20F);
            this.button1.Location = new System.Drawing.Point(12, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 49);
            this.button1.TabIndex = 0;
            this.button1.Text = "open serial port";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("PMingLiU", 20F);
            this.label1.Location = new System.Drawing.Point(7, 266);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "unknow";
            this.label1.Visible = false;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("PMingLiU", 16F);
            this.button2.Location = new System.Drawing.Point(12, 96);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(159, 49);
            this.button2.TabIndex = 2;
            this.button2.Text = "check battery";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("PMingLiU", 10F);
            this.label2.Location = new System.Drawing.Point(9, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "receive data";
            this.label2.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownHeight = 150;
            this.comboBox1.DropDownWidth = 50;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.IntegralHeight = false;
            this.comboBox1.Items.AddRange(new object[] {
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
            this.comboBox1.Location = new System.Drawing.Point(210, 61);
            this.comboBox1.MaxDropDownItems = 12;
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(63, 20);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.Text = "COM0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("PMingLiU", 10F);
            this.label3.Location = new System.Drawing.Point(207, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "com port";
            // 
            // testCommand1
            // 
            this.testCommand1.Font = new System.Drawing.Font("PMingLiU", 12F);
            this.testCommand1.Location = new System.Drawing.Point(233, 96);
            this.testCommand1.Name = "testCommand1";
            this.testCommand1.Size = new System.Drawing.Size(50, 23);
            this.testCommand1.TabIndex = 6;
            this.testCommand1.Text = "test";
            this.testCommand1.UseVisualStyleBackColor = true;
            this.testCommand1.Visible = false;
            this.testCommand1.Click += new System.EventHandler(this.testCommand1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "battery index :??";
            this.label4.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(177, 96);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(50, 49);
            this.button3.TabIndex = 9;
            this.button3.Text = "clear";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("PMingLiU", 20F);
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 27);
            this.label5.TabIndex = 10;
            this.label5.Text = "unknow";
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("PMingLiU", 20F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.indexGrid,
            this.powerGrid,
            this.voltageGrid,
            this.ampereGrid});
            this.dataGridView1.Location = new System.Drawing.Point(12, 151);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 30;
            this.dataGridView1.RowTemplate.DividerHeight = 55;
            this.dataGridView1.RowTemplate.Height = 35;
            this.dataGridView1.RowTemplate.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(526, 280);
            this.dataGridView1.TabIndex = 11;
            // 
            // indexGrid
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.indexGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.indexGrid.Frozen = true;
            this.indexGrid.HeaderText = "index";
            this.indexGrid.Name = "indexGrid";
            this.indexGrid.ReadOnly = true;
            this.indexGrid.Width = 80;
            // 
            // powerGrid
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.powerGrid.DefaultCellStyle = dataGridViewCellStyle4;
            this.powerGrid.Frozen = true;
            this.powerGrid.HeaderText = "power";
            this.powerGrid.Name = "powerGrid";
            this.powerGrid.ReadOnly = true;
            this.powerGrid.Width = 130;
            // 
            // voltageGrid
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.voltageGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.voltageGrid.Frozen = true;
            this.voltageGrid.HeaderText = "voltage(mV)";
            this.voltageGrid.Name = "voltageGrid";
            this.voltageGrid.ReadOnly = true;
            this.voltageGrid.Width = 130;
            // 
            // ampereGrid
            // 
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.ampereGrid.DefaultCellStyle = dataGridViewCellStyle6;
            this.ampereGrid.Frozen = true;
            this.ampereGrid.HeaderText = "ampere(mA)";
            this.ampereGrid.Name = "ampereGrid";
            this.ampereGrid.ReadOnly = true;
            this.ampereGrid.Width = 130;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 696);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.testCommand1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button testCommand1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn powerGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn voltageGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ampereGrid;
    }
}

