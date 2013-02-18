namespace Zkzx.Forms
{
    partial class 专家调度一级静态优化套箱
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
            this.pnl任务集合top = new System.Windows.Forms.Panel();
            this.btn批量确认 = new System.Windows.Forms.Button();
            this.pnl任务集合bottom = new System.Windows.Forms.Panel();
            this.gbox任务集合bottom = new System.Windows.Forms.GroupBox();
            this.gbox任务集合top = new System.Windows.Forms.GroupBox();
            this.lbl新任务 = new System.Windows.Forms.Label();
            this.lbl预配集合 = new System.Windows.Forms.Label();
            this.gbox备用区 = new System.Windows.Forms.GroupBox();
            this.pnl备用区 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new Feng.Windows.Forms.MySplitContainer();
            this.splitContainer3 = new Feng.Windows.Forms.MySplitContainer();
            this.gbox任务集合bottom.SuspendLayout();
            this.gbox任务集合top.SuspendLayout();
            this.gbox备用区.SuspendLayout();
            this.pnl备用区.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl任务集合top
            // 
            this.pnl任务集合top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl任务集合top.Location = new System.Drawing.Point(3, 17);
            this.pnl任务集合top.Margin = new System.Windows.Forms.Padding(0);
            this.pnl任务集合top.Name = "pnl任务集合top";
            this.pnl任务集合top.Size = new System.Drawing.Size(1195, 341);
            this.pnl任务集合top.TabIndex = 0;
            // 
            // btn批量确认
            // 
            this.btn批量确认.Location = new System.Drawing.Point(115, 39);
            this.btn批量确认.Name = "btn批量确认";
            this.btn批量确认.Size = new System.Drawing.Size(70, 23);
            this.btn批量确认.TabIndex = 9;
            this.btn批量确认.Text = "批量确认";
            this.btn批量确认.UseVisualStyleBackColor = true;
            this.btn批量确认.Click += new System.EventHandler(this.btn批量确认_Click);
            // 
            // pnl任务集合bottom
            // 
            this.pnl任务集合bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl任务集合bottom.Location = new System.Drawing.Point(3, 17);
            this.pnl任务集合bottom.Margin = new System.Windows.Forms.Padding(0);
            this.pnl任务集合bottom.Name = "pnl任务集合bottom";
            this.pnl任务集合bottom.Size = new System.Drawing.Size(748, 341);
            this.pnl任务集合bottom.TabIndex = 1;
            // 
            // gbox任务集合bottom
            // 
            this.gbox任务集合bottom.Controls.Add(this.pnl任务集合bottom);
            this.gbox任务集合bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbox任务集合bottom.Location = new System.Drawing.Point(0, 0);
            this.gbox任务集合bottom.Name = "gbox任务集合bottom";
            this.gbox任务集合bottom.Size = new System.Drawing.Size(754, 361);
            this.gbox任务集合bottom.TabIndex = 9;
            this.gbox任务集合bottom.TabStop = false;
            this.gbox任务集合bottom.Text = "出口箱任务集合";
            // 
            // gbox任务集合top
            // 
            this.gbox任务集合top.Controls.Add(this.lbl新任务);
            this.gbox任务集合top.Controls.Add(this.lbl预配集合);
            this.gbox任务集合top.Controls.Add(this.pnl任务集合top);
            this.gbox任务集合top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbox任务集合top.Location = new System.Drawing.Point(0, 0);
            this.gbox任务集合top.Name = "gbox任务集合top";
            this.gbox任务集合top.Size = new System.Drawing.Size(1201, 361);
            this.gbox任务集合top.TabIndex = 6;
            this.gbox任务集合top.TabStop = false;
            this.gbox任务集合top.Text = "进口箱任务集合";
            // 
            // lbl新任务
            // 
            this.lbl新任务.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl新任务.AutoSize = true;
            this.lbl新任务.Location = new System.Drawing.Point(1057, 0);
            this.lbl新任务.Name = "lbl新任务";
            this.lbl新任务.Size = new System.Drawing.Size(101, 12);
            this.lbl新任务.TabIndex = 2;
            this.lbl新任务.Text = "套箱成功的新任务";
            // 
            // lbl预配集合
            // 
            this.lbl预配集合.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl预配集合.AutoSize = true;
            this.lbl预配集合.Location = new System.Drawing.Point(945, -1);
            this.lbl预配集合.Name = "lbl预配集合";
            this.lbl预配集合.Size = new System.Drawing.Size(101, 12);
            this.lbl预配集合.TabIndex = 1;
            this.lbl预配集合.Text = "静态套箱预配集合";
            // 
            // gbox备用区
            // 
            this.gbox备用区.Controls.Add(this.pnl备用区);
            this.gbox备用区.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbox备用区.Location = new System.Drawing.Point(0, 0);
            this.gbox备用区.Name = "gbox备用区";
            this.gbox备用区.Size = new System.Drawing.Size(441, 361);
            this.gbox备用区.TabIndex = 11;
            this.gbox备用区.TabStop = false;
            this.gbox备用区.Text = "备用区";
            // 
            // pnl备用区
            // 
            this.pnl备用区.Controls.Add(this.btn批量确认);
            this.pnl备用区.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl备用区.Location = new System.Drawing.Point(3, 17);
            this.pnl备用区.Margin = new System.Windows.Forms.Padding(0);
            this.pnl备用区.Name = "pnl备用区";
            this.pnl备用区.Size = new System.Drawing.Size(435, 341);
            this.pnl备用区.TabIndex = 2;
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
            this.splitContainer1.Panel1.Controls.Add(this.gbox任务集合top);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1203, 730);
            this.splitContainer1.SplitterDistance = 363;
            this.splitContainer1.TabIndex = 12;
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
            this.splitContainer3.Panel1.Controls.Add(this.gbox任务集合bottom);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.gbox备用区);
            this.splitContainer3.Size = new System.Drawing.Size(1203, 363);
            this.splitContainer3.SplitterDistance = 756;
            this.splitContainer3.TabIndex = 0;
            // 
            // 专家调度一级静态优化套箱
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 730);
            this.Controls.Add(this.splitContainer1);
            this.Name = "专家调度一级静态优化套箱";
            this.Text = "专家调度一级静态优化";
            this.Load += new System.EventHandler(this.专家调度一级静态优化_Load);
            this.gbox任务集合bottom.ResumeLayout(false);
            this.gbox任务集合top.ResumeLayout(false);
            this.gbox任务集合top.PerformLayout();
            this.gbox备用区.ResumeLayout(false);
            this.pnl备用区.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl任务集合top;
        private System.Windows.Forms.Panel pnl任务集合bottom;
        private System.Windows.Forms.GroupBox gbox任务集合bottom;
        private System.Windows.Forms.GroupBox gbox任务集合top;
        private System.Windows.Forms.GroupBox gbox备用区;
        private System.Windows.Forms.Panel pnl备用区;
        private Feng.Windows.Forms.MySplitContainer splitContainer1;
        private Feng.Windows.Forms.MySplitContainer splitContainer3;
        private System.Windows.Forms.Label lbl新任务;
        private System.Windows.Forms.Label lbl预配集合;
        private System.Windows.Forms.Button btn批量确认;
    }
}