namespace Zkzx.Forms
{
    partial class 专家调度静态任务下达
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn区域自动分拣2 = new System.Windows.Forms.Button();
            this.btn批量确认2 = new System.Windows.Forms.Button();
            this.pnl未优化过的任务集合 = new System.Windows.Forms.Panel();
            this.btn批量确认 = new System.Windows.Forms.Button();
            this.btn区域自动分拣 = new System.Windows.Forms.Button();
            this.splitContainer1 = new Feng.Windows.Forms.MySplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl已优化过的任务集合 = new System.Windows.Forms.Panel();
            this.groupBox2.SuspendLayout();
            this.pnl未优化过的任务集合.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnl已优化过的任务集合.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.pnl未优化过的任务集合);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1006, 356);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "未优化过的任务集合";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(729, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "区域分拣、任务分派区";
            // 
            // btn区域自动分拣2
            // 
            this.btn区域自动分拣2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn区域自动分拣2.Location = new System.Drawing.Point(679, 252);
            this.btn区域自动分拣2.Name = "btn区域自动分拣2";
            this.btn区域自动分拣2.Size = new System.Drawing.Size(100, 23);
            this.btn区域自动分拣2.TabIndex = 0;
            this.btn区域自动分拣2.Text = "区域自动分拣";
            this.btn区域自动分拣2.UseVisualStyleBackColor = true;
            // 
            // btn批量确认2
            // 
            this.btn批量确认2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn批量确认2.Location = new System.Drawing.Point(822, 252);
            this.btn批量确认2.Name = "btn批量确认2";
            this.btn批量确认2.Size = new System.Drawing.Size(100, 23);
            this.btn批量确认2.TabIndex = 1;
            this.btn批量确认2.Text = "批量确认";
            this.btn批量确认2.UseVisualStyleBackColor = true;
            this.btn批量确认2.Click += new System.EventHandler(this.btn批量确认下达2_Click);
            // 
            // pnl未优化过的任务集合
            // 
            this.pnl未优化过的任务集合.Controls.Add(this.btn区域自动分拣2);
            this.pnl未优化过的任务集合.Controls.Add(this.btn批量确认2);
            this.pnl未优化过的任务集合.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl未优化过的任务集合.Location = new System.Drawing.Point(3, 17);
            this.pnl未优化过的任务集合.Margin = new System.Windows.Forms.Padding(0);
            this.pnl未优化过的任务集合.Name = "pnl未优化过的任务集合";
            this.pnl未优化过的任务集合.Size = new System.Drawing.Size(1000, 336);
            this.pnl未优化过的任务集合.TabIndex = 1;
            // 
            // btn批量确认
            // 
            this.btn批量确认.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn批量确认.Location = new System.Drawing.Point(822, 267);
            this.btn批量确认.Name = "btn批量确认";
            this.btn批量确认.Size = new System.Drawing.Size(100, 23);
            this.btn批量确认.TabIndex = 1;
            this.btn批量确认.Text = "批量确认";
            this.btn批量确认.UseVisualStyleBackColor = true;
            this.btn批量确认.Click += new System.EventHandler(this.btn批量确认下达_Click);
            // 
            // btn区域自动分拣
            // 
            this.btn区域自动分拣.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn区域自动分拣.Location = new System.Drawing.Point(653, 267);
            this.btn区域自动分拣.Name = "btn区域自动分拣";
            this.btn区域自动分拣.Size = new System.Drawing.Size(100, 23);
            this.btn区域自动分拣.TabIndex = 0;
            this.btn区域自动分拣.Text = "区域自动分拣";
            this.btn区域自动分拣.UseVisualStyleBackColor = true;
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 730);
            this.splitContainer1.SplitterDistance = 368;
            this.splitContainer1.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pnl已优化过的任务集合);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1006, 366);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "已优化过的任务集合";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(729, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "区域分拣、任务分派区";
            // 
            // pnl已优化过的任务集合
            // 
            this.pnl已优化过的任务集合.Controls.Add(this.btn批量确认);
            this.pnl已优化过的任务集合.Controls.Add(this.btn区域自动分拣);
            this.pnl已优化过的任务集合.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl已优化过的任务集合.Location = new System.Drawing.Point(3, 17);
            this.pnl已优化过的任务集合.Margin = new System.Windows.Forms.Padding(0);
            this.pnl已优化过的任务集合.Name = "pnl已优化过的任务集合";
            this.pnl已优化过的任务集合.Size = new System.Drawing.Size(1000, 346);
            this.pnl已优化过的任务集合.TabIndex = 0;
            // 
            // 专家调度静态任务下达
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.splitContainer1);
            this.Name = "专家调度静态任务下达";
            this.Text = "专家调度静态任务下达";
            this.Load += new System.EventHandler(this.专家调度静态任务下达_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnl未优化过的任务集合.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnl已优化过的任务集合.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel pnl未优化过的任务集合;
        private System.Windows.Forms.Button btn批量确认;
        private System.Windows.Forms.Button btn区域自动分拣;
        private Feng.Windows.Forms.MySplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnl已优化过的任务集合;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn区域自动分拣2;
        private System.Windows.Forms.Button btn批量确认2;
    }
}