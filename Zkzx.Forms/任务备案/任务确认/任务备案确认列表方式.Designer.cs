namespace Zkzx.Forms
{
    partial class 任务备案确认列表方式
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
            this.lbl备案确认区 = new System.Windows.Forms.Label();
            this.pnl任务预备案信息区 = new System.Windows.Forms.Panel();
            this.btn网上委托任务 = new System.Windows.Forms.Button();
            this.btn批量确认 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lbl备案确认区);
            this.groupBox1.Controls.Add(this.pnl任务预备案信息区);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(984, 677);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "任务申请信息区";
            // 
            // lbl备案确认区
            // 
            this.lbl备案确认区.AutoSize = true;
            this.lbl备案确认区.Location = new System.Drawing.Point(829, 0);
            this.lbl备案确认区.Name = "lbl备案确认区";
            this.lbl备案确认区.Size = new System.Drawing.Size(65, 12);
            this.lbl备案确认区.TabIndex = 1;
            this.lbl备案确认区.Text = "备案确认区";
            // 
            // pnl任务预备案信息区
            // 
            this.pnl任务预备案信息区.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl任务预备案信息区.Location = new System.Drawing.Point(3, 17);
            this.pnl任务预备案信息区.Margin = new System.Windows.Forms.Padding(0);
            this.pnl任务预备案信息区.Name = "pnl任务预备案信息区";
            this.pnl任务预备案信息区.Size = new System.Drawing.Size(978, 657);
            this.pnl任务预备案信息区.TabIndex = 0;
            // 
            // btn网上委托任务
            // 
            this.btn网上委托任务.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn网上委托任务.ImeMode = System.Windows.Forms.ImeMode.On;
            this.btn网上委托任务.Location = new System.Drawing.Point(145, 695);
            this.btn网上委托任务.Name = "btn网上委托任务";
            this.btn网上委托任务.Size = new System.Drawing.Size(105, 23);
            this.btn网上委托任务.TabIndex = 3;
            this.btn网上委托任务.Text = "网上委托导入";
            this.btn网上委托任务.UseVisualStyleBackColor = true;
            this.btn网上委托任务.Click += new System.EventHandler(this.btn网上委托任务_Click);
            // 
            // btn批量确认
            // 
            this.btn批量确认.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn批量确认.ImeMode = System.Windows.Forms.ImeMode.On;
            this.btn批量确认.Location = new System.Drawing.Point(818, 695);
            this.btn批量确认.Name = "btn批量确认";
            this.btn批量确认.Size = new System.Drawing.Size(75, 23);
            this.btn批量确认.TabIndex = 1;
            this.btn批量确认.Text = "批量确认";
            this.btn批量确认.UseVisualStyleBackColor = true;
            this.btn批量确认.Click += new System.EventHandler(this.btn批量确认_Click);
            // 
            // 任务备案确认列表方式
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.btn网上委托任务);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn批量确认);
            this.Name = "任务备案确认列表方式";
            this.Text = "任务备案确认-列表方式";
            this.Load += new System.EventHandler(this.任务正式备案_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnl任务预备案信息区;
        private System.Windows.Forms.Button btn批量确认;
        private System.Windows.Forms.Label lbl备案确认区;
        private System.Windows.Forms.Button btn网上委托任务;
    }
}