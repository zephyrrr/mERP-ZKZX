namespace Zkzx.Forms
{
    partial class 预录入发送后情况查询
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
            this.lbl反馈区 = new System.Windows.Forms.Label();
            this.pnl信息区 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lbl反馈区);
            this.groupBox1.Controls.Add(this.pnl信息区);
            this.groupBox1.Location = new System.Drawing.Point(3, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1054, 517);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "任务预备案信息区";
            // 
            // lbl反馈区
            // 
            this.lbl反馈区.AutoSize = true;
            this.lbl反馈区.Location = new System.Drawing.Point(602, 2);
            this.lbl反馈区.Name = "lbl反馈区";
            this.lbl反馈区.Size = new System.Drawing.Size(89, 12);
            this.lbl反馈区.TabIndex = 1;
            this.lbl反馈区.Text = "备案结果反馈区";
            // 
            // pnl信息区
            // 
            this.pnl信息区.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl信息区.Location = new System.Drawing.Point(3, 17);
            this.pnl信息区.Name = "pnl信息区";
            this.pnl信息区.Size = new System.Drawing.Size(1048, 497);
            this.pnl信息区.TabIndex = 0;
            // 
            // 预录入发送后情况查询
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 527);
            this.Controls.Add(this.groupBox1);
            this.Name = "预录入发送后情况查询";
            this.Text = "任务预录入发送后情况查询";
            this.Load += new System.EventHandler(this.预录入发送后情况查询_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnl信息区;
        private System.Windows.Forms.Label lbl反馈区;

    }
}