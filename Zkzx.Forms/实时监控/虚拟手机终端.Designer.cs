namespace Zkzx.Forms
{
    partial class 虚拟手机终端
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnReadGpsData = new System.Windows.Forms.Button();
            this.btnSendGPS = new System.Windows.Forms.Button();
            this.txtGPSLon = new System.Windows.Forms.TextBox();
            this.txtGPSLat = new System.Windows.Forms.TextBox();
            this.dtpGPSTime = new System.Windows.Forms.DateTimePicker();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn堵车 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn故障处理 = new System.Windows.Forms.Button();
            this.btn途中休息 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn后续作业 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btn当前作业 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pnl车载ID = new System.Windows.Forms.Panel();
            this.ckbUseLocal = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btn后续作业);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.btn当前作业);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(772, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 615);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "单个车载ID显示区";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.ckbUseLocal);
            this.groupBox4.Controls.Add(this.btnReadGpsData);
            this.groupBox4.Controls.Add(this.btnSendGPS);
            this.groupBox4.Controls.Add(this.txtGPSLon);
            this.groupBox4.Controls.Add(this.txtGPSLat);
            this.groupBox4.Controls.Add(this.dtpGPSTime);
            this.groupBox4.Location = new System.Drawing.Point(10, 537);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(280, 72);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "GPS模拟";
            // 
            // btnReadGpsData
            // 
            this.btnReadGpsData.Location = new System.Drawing.Point(222, 43);
            this.btnReadGpsData.Name = "btnReadGpsData";
            this.btnReadGpsData.Size = new System.Drawing.Size(52, 21);
            this.btnReadGpsData.TabIndex = 11;
            this.btnReadGpsData.Text = "读入";
            this.btnReadGpsData.UseVisualStyleBackColor = true;
            this.btnReadGpsData.Click += new System.EventHandler(this.btnReadGpsData_Click);
            // 
            // btnSendGPS
            // 
            this.btnSendGPS.Location = new System.Drawing.Point(222, 14);
            this.btnSendGPS.Name = "btnSendGPS";
            this.btnSendGPS.Size = new System.Drawing.Size(52, 21);
            this.btnSendGPS.TabIndex = 10;
            this.btnSendGPS.Text = "发送";
            this.btnSendGPS.UseVisualStyleBackColor = true;
            this.btnSendGPS.Click += new System.EventHandler(this.btnSendGPS_Click);
            // 
            // txtGPSLon
            // 
            this.txtGPSLon.Location = new System.Drawing.Point(115, 14);
            this.txtGPSLon.Name = "txtGPSLon";
            this.txtGPSLon.Size = new System.Drawing.Size(100, 21);
            this.txtGPSLon.TabIndex = 9;
            this.txtGPSLon.Text = "121";
            // 
            // txtGPSLat
            // 
            this.txtGPSLat.Location = new System.Drawing.Point(9, 14);
            this.txtGPSLat.Name = "txtGPSLat";
            this.txtGPSLat.Size = new System.Drawing.Size(100, 21);
            this.txtGPSLat.TabIndex = 7;
            this.txtGPSLat.Text = "29";
            // 
            // dtpGPSTime
            // 
            this.dtpGPSTime.CustomFormat = "yy-MM-dd HH:mm";
            this.dtpGPSTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpGPSTime.Location = new System.Drawing.Point(9, 41);
            this.dtpGPSTime.Name = "dtpGPSTime";
            this.dtpGPSTime.Size = new System.Drawing.Size(180, 21);
            this.dtpGPSTime.TabIndex = 8;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox5.Location = new System.Drawing.Point(10, 468);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(280, 63);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "中心发送的即时信息列表";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btn堵车);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblTitle);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btn故障处理);
            this.groupBox2.Controls.Add(this.btn途中休息);
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Location = new System.Drawing.Point(10, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(288, 362);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            // 
            // btn堵车
            // 
            this.btn堵车.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn堵车.Location = new System.Drawing.Point(125, 333);
            this.btn堵车.Name = "btn堵车";
            this.btn堵车.Size = new System.Drawing.Size(48, 21);
            this.btn堵车.TabIndex = 1;
            this.btn堵车.Text = "堵车";
            this.btn堵车.UseVisualStyleBackColor = true;
            this.btn堵车.Click += new System.EventHandler(this.btn堵车_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "作业进程：";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(79, 21);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 12);
            this.lblTitle.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "作业性质";
            // 
            // btn故障处理
            // 
            this.btn故障处理.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn故障处理.Location = new System.Drawing.Point(207, 333);
            this.btn故障处理.Name = "btn故障处理";
            this.btn故障处理.Size = new System.Drawing.Size(75, 23);
            this.btn故障处理.TabIndex = 2;
            this.btn故障处理.Text = "故障处理";
            this.btn故障处理.UseVisualStyleBackColor = true;
            this.btn故障处理.Click += new System.EventHandler(this.btn故障处理_Click);
            // 
            // btn途中休息
            // 
            this.btn途中休息.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn途中休息.Location = new System.Drawing.Point(6, 333);
            this.btn途中休息.Name = "btn途中休息";
            this.btn途中休息.Size = new System.Drawing.Size(75, 23);
            this.btn途中休息.TabIndex = 0;
            this.btn途中休息.Text = "途中休息";
            this.btn途中休息.UseVisualStyleBackColor = true;
            this.btn途中休息.Click += new System.EventHandler(this.btn途中休息_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.2963F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.85185F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.11111F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 58);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(270, 265);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // btn后续作业
            // 
            this.btn后续作业.Location = new System.Drawing.Point(159, 388);
            this.btn后续作业.Name = "btn后续作业";
            this.btn后续作业.Size = new System.Drawing.Size(120, 35);
            this.btn后续作业.TabIndex = 13;
            this.btn后续作业.Text = "后续作业";
            this.btn后续作业.UseVisualStyleBackColor = true;
            this.btn后续作业.Click += new System.EventHandler(this.btn后续作业_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(230, 428);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(60, 33);
            this.button6.TabIndex = 12;
            this.button6.Text = "返回";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(152, 429);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(60, 33);
            this.button5.TabIndex = 11;
            this.button5.Text = "通话";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(76, 428);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(60, 33);
            this.button4.TabIndex = 10;
            this.button4.Text = "导航";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(10, 429);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(60, 33);
            this.button3.TabIndex = 9;
            this.button3.Text = "作业";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btn当前作业
            // 
            this.btn当前作业.Location = new System.Drawing.Point(16, 388);
            this.btn当前作业.Name = "btn当前作业";
            this.btn当前作业.Size = new System.Drawing.Size(120, 35);
            this.btn当前作业.TabIndex = 8;
            this.btn当前作业.Text = "当前作业";
            this.btn当前作业.UseVisualStyleBackColor = true;
            this.btn当前作业.Click += new System.EventHandler(this.btn当前作业_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.pnl车载ID);
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(766, 615);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "车载ID当前使用情况集合区";
            // 
            // pnl车载ID
            // 
            this.pnl车载ID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl车载ID.Location = new System.Drawing.Point(3, 17);
            this.pnl车载ID.Name = "pnl车载ID";
            this.pnl车载ID.Size = new System.Drawing.Size(760, 595);
            this.pnl车载ID.TabIndex = 12;
            // 
            // ckbUseLocal
            // 
            this.ckbUseLocal.AutoSize = true;
            this.ckbUseLocal.Location = new System.Drawing.Point(196, 41);
            this.ckbUseLocal.Name = "ckbUseLocal";
            this.ckbUseLocal.Size = new System.Drawing.Size(15, 14);
            this.ckbUseLocal.TabIndex = 12;
            this.ckbUseLocal.UseVisualStyleBackColor = true;
            this.ckbUseLocal.CheckedChanged += new System.EventHandler(this.ckbUseLocal_CheckedChanged);
            // 
            // 虚拟手机终端
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 615);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "虚拟手机终端";
            this.Text = "虚拟手机终端";
            this.Load += new System.EventHandler(this.虚拟手机终端_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn堵车;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn故障处理;
        private System.Windows.Forms.Button btn途中休息;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn后续作业;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btn当前作业;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel pnl车载ID;
        private System.Windows.Forms.Button btnSendGPS;
        private System.Windows.Forms.TextBox txtGPSLon;
        private System.Windows.Forms.DateTimePicker dtpGPSTime;
        private System.Windows.Forms.TextBox txtGPSLat;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnReadGpsData;
        private System.Windows.Forms.CheckBox ckbUseLocal;
    }
}