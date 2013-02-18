namespace Zkzx.Forms
{
    partial class 任务预备案
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
            this.pnl备案明细窗体 = new System.Windows.Forms.Panel();
            this.btn修改 = new System.Windows.Forms.Button();
            this.btn预录入发送 = new System.Windows.Forms.Button();
            this.btn删除 = new System.Windows.Forms.Button();
            this.btn新增任务 = new System.Windows.Forms.Button();
            this.btn暂存待确认 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnl备用区 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pnl待确认区 = new System.Windows.Forms.Panel();
            this.btn文件导入 = new System.Windows.Forms.Button();
            this.openFileDialog文件导入 = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new Feng.Windows.Forms.MySplitContainer();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pnl备案明细窗体);
            this.groupBox1.Controls.Add(this.btn修改);
            this.groupBox1.Controls.Add(this.btn预录入发送);
            this.groupBox1.Controls.Add(this.btn删除);
            this.groupBox1.Controls.Add(this.btn新增任务);
            this.groupBox1.Controls.Add(this.btn暂存待确认);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(790, 401);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "单个任务全部内容预录入、显示区";
            // 
            // pnl备案明细窗体
            // 
            this.pnl备案明细窗体.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl备案明细窗体.Location = new System.Drawing.Point(7, 20);
            this.pnl备案明细窗体.Name = "pnl备案明细窗体";
            this.pnl备案明细窗体.Size = new System.Drawing.Size(777, 346);
            this.pnl备案明细窗体.TabIndex = 9;
            // 
            // btn修改
            // 
            this.btn修改.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn修改.Location = new System.Drawing.Point(420, 369);
            this.btn修改.Name = "btn修改";
            this.btn修改.Size = new System.Drawing.Size(75, 23);
            this.btn修改.TabIndex = 8;
            this.btn修改.Text = "修改任务";
            this.btn修改.UseVisualStyleBackColor = true;
            this.btn修改.Click += new System.EventHandler(this.btn修改任务_Click);
            // 
            // btn预录入发送
            // 
            this.btn预录入发送.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn预录入发送.Location = new System.Drawing.Point(539, 369);
            this.btn预录入发送.Name = "btn预录入发送";
            this.btn预录入发送.Size = new System.Drawing.Size(75, 23);
            this.btn预录入发送.TabIndex = 4;
            this.btn预录入发送.Text = "预录入发送";
            this.btn预录入发送.UseVisualStyleBackColor = true;
            this.btn预录入发送.Click += new System.EventHandler(this.btn预录入发送_Click);
            // 
            // btn删除
            // 
            this.btn删除.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn删除.Location = new System.Drawing.Point(339, 369);
            this.btn删除.Name = "btn删除";
            this.btn删除.Size = new System.Drawing.Size(75, 23);
            this.btn删除.TabIndex = 3;
            this.btn删除.Text = "删除";
            this.btn删除.UseVisualStyleBackColor = true;
            this.btn删除.Click += new System.EventHandler(this.btn删除_Click);
            // 
            // btn新增任务
            // 
            this.btn新增任务.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn新增任务.Location = new System.Drawing.Point(177, 369);
            this.btn新增任务.Name = "btn新增任务";
            this.btn新增任务.Size = new System.Drawing.Size(75, 23);
            this.btn新增任务.TabIndex = 1;
            this.btn新增任务.Text = "新增任务";
            this.btn新增任务.UseVisualStyleBackColor = true;
            this.btn新增任务.Click += new System.EventHandler(this.btn新增任务_Click);
            // 
            // btn暂存待确认
            // 
            this.btn暂存待确认.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn暂存待确认.Location = new System.Drawing.Point(258, 369);
            this.btn暂存待确认.Name = "btn暂存待确认";
            this.btn暂存待确认.Size = new System.Drawing.Size(75, 23);
            this.btn暂存待确认.TabIndex = 2;
            this.btn暂存待确认.Text = "暂存待确认";
            this.btn暂存待确认.UseVisualStyleBackColor = true;
            this.btn暂存待确认.Click += new System.EventHandler(this.btn暂存待确认_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.pnl备用区);
            this.groupBox2.Location = new System.Drawing.Point(0, 410);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(790, 177);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "备用区";
            // 
            // pnl备用区
            // 
            this.pnl备用区.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl备用区.Location = new System.Drawing.Point(3, 17);
            this.pnl备用区.Margin = new System.Windows.Forms.Padding(0);
            this.pnl备用区.Name = "pnl备用区";
            this.pnl备用区.Size = new System.Drawing.Size(784, 157);
            this.pnl备用区.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.pnl待确认区);
            this.groupBox3.Controls.Add(this.btn文件导入);
            this.groupBox3.Location = new System.Drawing.Point(3, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(292, 579);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "暂存的预录入待确认、发送区";
            // 
            // pnl待确认区
            // 
            this.pnl待确认区.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl待确认区.Location = new System.Drawing.Point(3, 17);
            this.pnl待确认区.Margin = new System.Windows.Forms.Padding(0);
            this.pnl待确认区.Name = "pnl待确认区";
            this.pnl待确认区.Size = new System.Drawing.Size(286, 532);
            this.pnl待确认区.TabIndex = 0;
            // 
            // btn文件导入
            // 
            this.btn文件导入.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn文件导入.Location = new System.Drawing.Point(103, 553);
            this.btn文件导入.Name = "btn文件导入";
            this.btn文件导入.Size = new System.Drawing.Size(108, 23);
            this.btn文件导入.TabIndex = 6;
            this.btn文件导入.Text = "预录入文件导入";
            this.btn文件导入.UseVisualStyleBackColor = true;
            this.btn文件导入.Click += new System.EventHandler(this.btn文件导入_Click);
            // 
            // openFileDialog文件导入
            // 
            this.openFileDialog文件导入.Filter = "Excel文件(*.xl*;*.xlsx;*.xlsm;*.xlsb;*.xlam;*.xltx;*.xltm;*.xls;*.xla;*.xlt;*.xlm;*" +
    ".xlw)|*.xl*;*.xlsx;*.xlsm;*.xlsb;*.xlam;*.xltx;*.xltm;*.xls;*.xla;*.xlt;*.xlm;*." +
    "xlw";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(1098, 590);
            this.splitContainer1.SplitterDistance = 796;
            this.splitContainer1.TabIndex = 3;
            // 
            // 任务预备案
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 590);
            this.Controls.Add(this.splitContainer1);
            this.Name = "任务预备案";
            this.Text = "任务预录入";
            this.Load += new System.EventHandler(this.任务预备案_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel pnl待确认区;
        private System.Windows.Forms.Panel pnl备用区;
        private System.Windows.Forms.Button btn删除;
        private System.Windows.Forms.Button btn暂存待确认;
        private System.Windows.Forms.Button btn新增任务;
        private System.Windows.Forms.Button btn预录入发送;
        private System.Windows.Forms.Button btn文件导入;
        private System.Windows.Forms.Button btn修改;
        private System.Windows.Forms.OpenFileDialog openFileDialog文件导入;
        private System.Windows.Forms.Panel pnl备案明细窗体;
        private Feng.Windows.Forms.MySplitContainer splitContainer1;
    }
}