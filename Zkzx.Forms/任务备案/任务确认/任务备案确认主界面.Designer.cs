namespace Zkzx.Forms
{
    partial class 任务备案确认主界面
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl任务号 = new System.Windows.Forms.Panel();
            this.lbl任务号 = new System.Windows.Forms.Label();
            this.btn备案确认 = new System.Windows.Forms.Button();
            this.btn修改 = new System.Windows.Forms.Button();
            this.btn拒绝 = new System.Windows.Forms.Button();
            this.btn放弃 = new System.Windows.Forms.Button();
            this.btn网上委托导入 = new System.Windows.Forms.Button();
            this.pnl请求区 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pnl备用区 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.openFileDialog文件导入 = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new Feng.Windows.Forms.MySplitContainer();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pnl备案明细窗体);
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Controls.Add(this.btn备案确认);
            this.groupBox1.Controls.Add(this.btn修改);
            this.groupBox1.Controls.Add(this.btn拒绝);
            this.groupBox1.Controls.Add(this.btn放弃);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(838, 399);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "单个任务全部内容显示、确认区";
            // 
            // pnl备案明细窗体
            // 
            this.pnl备案明细窗体.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl备案明细窗体.Location = new System.Drawing.Point(6, 20);
            this.pnl备案明细窗体.Name = "pnl备案明细窗体";
            this.pnl备案明细窗体.Size = new System.Drawing.Size(829, 340);
            this.pnl备案明细窗体.TabIndex = 58;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.pnl任务号, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl任务号, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(616, 364);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(219, 25);
            this.tableLayoutPanel2.TabIndex = 57;
            // 
            // pnl任务号
            // 
            this.pnl任务号.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl任务号.ForeColor = System.Drawing.Color.Red;
            this.pnl任务号.Location = new System.Drawing.Point(109, 0);
            this.pnl任务号.Margin = new System.Windows.Forms.Padding(0);
            this.pnl任务号.Name = "pnl任务号";
            this.pnl任务号.Size = new System.Drawing.Size(110, 25);
            this.pnl任务号.TabIndex = 53;
            // 
            // lbl任务号
            // 
            this.lbl任务号.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl任务号.AutoSize = true;
            this.lbl任务号.ForeColor = System.Drawing.Color.Red;
            this.lbl任务号.Location = new System.Drawing.Point(3, 0);
            this.lbl任务号.Name = "lbl任务号";
            this.lbl任务号.Size = new System.Drawing.Size(103, 25);
            this.lbl任务号.TabIndex = 52;
            this.lbl任务号.Text = "任务号";
            this.lbl任务号.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn备案确认
            // 
            this.btn备案确认.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn备案确认.Location = new System.Drawing.Point(435, 364);
            this.btn备案确认.Margin = new System.Windows.Forms.Padding(0);
            this.btn备案确认.Name = "btn备案确认";
            this.btn备案确认.Size = new System.Drawing.Size(72, 23);
            this.btn备案确认.TabIndex = 56;
            this.btn备案确认.Text = "备案确认";
            this.btn备案确认.UseVisualStyleBackColor = true;
            this.btn备案确认.Click += new System.EventHandler(this.btn备案确认_Click);
            // 
            // btn修改
            // 
            this.btn修改.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn修改.Location = new System.Drawing.Point(148, 364);
            this.btn修改.Margin = new System.Windows.Forms.Padding(0);
            this.btn修改.Name = "btn修改";
            this.btn修改.Size = new System.Drawing.Size(72, 23);
            this.btn修改.TabIndex = 51;
            this.btn修改.Text = "修  改";
            this.btn修改.UseVisualStyleBackColor = true;
            this.btn修改.Click += new System.EventHandler(this.btn修改_Click);
            // 
            // btn拒绝
            // 
            this.btn拒绝.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn拒绝.Location = new System.Drawing.Point(244, 364);
            this.btn拒绝.Margin = new System.Windows.Forms.Padding(0);
            this.btn拒绝.Name = "btn拒绝";
            this.btn拒绝.Size = new System.Drawing.Size(72, 23);
            this.btn拒绝.TabIndex = 54;
            this.btn拒绝.Text = "拒  绝";
            this.btn拒绝.UseVisualStyleBackColor = true;
            this.btn拒绝.Click += new System.EventHandler(this.btn拒绝_Click);
            // 
            // btn放弃
            // 
            this.btn放弃.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn放弃.Location = new System.Drawing.Point(340, 364);
            this.btn放弃.Margin = new System.Windows.Forms.Padding(0);
            this.btn放弃.Name = "btn放弃";
            this.btn放弃.Size = new System.Drawing.Size(71, 23);
            this.btn放弃.TabIndex = 55;
            this.btn放弃.Text = "放  弃";
            this.btn放弃.UseVisualStyleBackColor = true;
            this.btn放弃.Click += new System.EventHandler(this.btn放弃_Click);
            // 
            // btn网上委托导入
            // 
            this.btn网上委托导入.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn网上委托导入.Location = new System.Drawing.Point(124, 620);
            this.btn网上委托导入.Name = "btn网上委托导入";
            this.btn网上委托导入.Size = new System.Drawing.Size(100, 23);
            this.btn网上委托导入.TabIndex = 6;
            this.btn网上委托导入.Text = "网上委托导入";
            this.btn网上委托导入.UseVisualStyleBackColor = true;
            this.btn网上委托导入.Click += new System.EventHandler(this.btn网上委托导入_Click);
            // 
            // pnl请求区
            // 
            this.pnl请求区.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl请求区.Location = new System.Drawing.Point(3, 17);
            this.pnl请求区.Margin = new System.Windows.Forms.Padding(0);
            this.pnl请求区.Name = "pnl请求区";
            this.pnl请求区.Size = new System.Drawing.Size(277, 593);
            this.pnl请求区.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pnl请求区);
            this.groupBox3.Controls.Add(this.btn网上委托导入);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(283, 649);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "任务备案确认请求区";
            // 
            // pnl备用区
            // 
            this.pnl备用区.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl备用区.Location = new System.Drawing.Point(3, 17);
            this.pnl备用区.Margin = new System.Windows.Forms.Padding(0);
            this.pnl备用区.Name = "pnl备用区";
            this.pnl备用区.Size = new System.Drawing.Size(833, 222);
            this.pnl备用区.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.pnl备用区);
            this.groupBox2.Location = new System.Drawing.Point(2, 401);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(839, 242);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "备用区";
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
            this.splitContainer1.Size = new System.Drawing.Size(1131, 649);
            this.splitContainer1.SplitterDistance = 844;
            this.splitContainer1.TabIndex = 7;
            // 
            // 任务备案确认主界面
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 649);
            this.Controls.Add(this.splitContainer1);
            this.Name = "任务备案确认主界面";
            this.Text = "任务备案确认主界面";
            this.Load += new System.EventHandler(this.任务正式备案二_Load);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn网上委托导入;
        private System.Windows.Forms.Panel pnl请求区;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel pnl备用区;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog文件导入;
        private System.Windows.Forms.Button btn备案确认;
        private System.Windows.Forms.Button btn放弃;
        private System.Windows.Forms.Button btn拒绝;
        private System.Windows.Forms.Button btn修改;
        private System.Windows.Forms.Panel pnl任务号;
        private System.Windows.Forms.Label lbl任务号;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel pnl备案明细窗体;
        private Feng.Windows.Forms.MySplitContainer splitContainer1;

    }
}