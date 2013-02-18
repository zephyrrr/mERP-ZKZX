namespace Zkzx.Forms
{
    partial class 车队调度动态优化派车
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
            this.btn全自动匹配 = new System.Windows.Forms.Button();
            this.btn计算机辅助匹配 = new System.Windows.Forms.Button();
            this.splitContainer1 = new Feng.Windows.Forms.MySplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl待命车辆_单车单任务 = new System.Windows.Forms.Panel();
            this.splitContainer3 = new Feng.Windows.Forms.MySplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pnl待排任务 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnl备用区 = new System.Windows.Forms.Panel();
            this.btn批量确认 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnl备用区.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn全自动匹配
            // 
            this.btn全自动匹配.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn全自动匹配.Location = new System.Drawing.Point(163, 344);
            this.btn全自动匹配.Name = "btn全自动匹配";
            this.btn全自动匹配.Size = new System.Drawing.Size(87, 27);
            this.btn全自动匹配.TabIndex = 1;
            this.btn全自动匹配.Text = "全自动匹配";
            this.btn全自动匹配.UseVisualStyleBackColor = true;
            this.btn全自动匹配.Click += new System.EventHandler(this.btn全自动优化_Click);
            // 
            // btn计算机辅助匹配
            // 
            this.btn计算机辅助匹配.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn计算机辅助匹配.Location = new System.Drawing.Point(16, 344);
            this.btn计算机辅助匹配.Name = "btn计算机辅助匹配";
            this.btn计算机辅助匹配.Size = new System.Drawing.Size(118, 27);
            this.btn计算机辅助匹配.TabIndex = 0;
            this.btn计算机辅助匹配.Text = "计算机辅助匹配";
            this.btn计算机辅助匹配.UseVisualStyleBackColor = true;
            this.btn计算机辅助匹配.Click += new System.EventHandler(this.btn计算机辅助优化_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 730);
            this.splitContainer1.SplitterDistance = 348;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.pnl待命车辆_单车单任务);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1006, 346);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "可作业车辆";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(710, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "车辆、任务集成区";
            // 
            // pnl待命车辆_单车单任务
            // 
            this.pnl待命车辆_单车单任务.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl待命车辆_单车单任务.Location = new System.Drawing.Point(3, 17);
            this.pnl待命车辆_单车单任务.Margin = new System.Windows.Forms.Padding(0);
            this.pnl待命车辆_单车单任务.Name = "pnl待命车辆_单车单任务";
            this.pnl待命车辆_单车单任务.Size = new System.Drawing.Size(1000, 326);
            this.pnl待命车辆_单车单任务.TabIndex = 1;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer3.Panel2.Controls.Add(this.btn计算机辅助匹配);
            this.splitContainer3.Panel2.Controls.Add(this.btn全自动匹配);
            this.splitContainer3.Size = new System.Drawing.Size(1008, 378);
            this.splitContainer3.SplitterDistance = 725;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pnl待排任务);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(723, 376);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "待排任务";
            // 
            // pnl待排任务
            // 
            this.pnl待排任务.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl待排任务.Location = new System.Drawing.Point(3, 17);
            this.pnl待排任务.Margin = new System.Windows.Forms.Padding(0);
            this.pnl待排任务.Name = "pnl待排任务";
            this.pnl待排任务.Size = new System.Drawing.Size(717, 356);
            this.pnl待排任务.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pnl备用区);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(277, 338);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "优化条件设定区";
            // 
            // pnl备用区
            // 
            this.pnl备用区.Controls.Add(this.btn批量确认);
            this.pnl备用区.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl备用区.Location = new System.Drawing.Point(3, 17);
            this.pnl备用区.Margin = new System.Windows.Forms.Padding(0);
            this.pnl备用区.Name = "pnl备用区";
            this.pnl备用区.Size = new System.Drawing.Size(271, 318);
            this.pnl备用区.TabIndex = 0;
            // 
            // btn批量确认
            // 
            this.btn批量确认.Location = new System.Drawing.Point(93, 14);
            this.btn批量确认.Name = "btn批量确认";
            this.btn批量确认.Size = new System.Drawing.Size(65, 23);
            this.btn批量确认.TabIndex = 4;
            this.btn批量确认.Text = "批量确认";
            this.btn批量确认.UseVisualStyleBackColor = true;
            this.btn批量确认.Click += new System.EventHandler(this.btn批量确认_Click);
            // 
            // 车队调度动态优化派车
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.splitContainer1);
            this.Name = "车队调度动态优化派车";
            this.Text = "二级动态优化派车";
            this.Load += new System.EventHandler(this.车队调度静态任务下达_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.pnl备用区.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn计算机辅助匹配;
        private Feng.Windows.Forms.MySplitContainer splitContainer1;
        private System.Windows.Forms.Button btn全自动匹配;
        private Feng.Windows.Forms.MySplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel pnl备用区;
        private System.Windows.Forms.Panel pnl待排任务;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel pnl待命车辆_单车单任务;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn批量确认;
    }
}