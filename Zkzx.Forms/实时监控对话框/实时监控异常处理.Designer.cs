namespace Zkzx.Forms
{
    partial class 实时监控异常处理
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn确定 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl处理结果 = new System.Windows.Forms.Panel();
            this.pnl处理时间 = new System.Windows.Forms.Panel();
            this.pnl异常情况 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "异常情况";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(0, 33);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 33);
            this.label3.TabIndex = 2;
            this.label3.Text = "处理时间";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(0, 66);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 34);
            this.label4.TabIndex = 3;
            this.label4.Text = "处理结果";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn确定
            // 
            this.btn确定.ImeMode = System.Windows.Forms.ImeMode.On;
            this.btn确定.Location = new System.Drawing.Point(185, 127);
            this.btn确定.Name = "btn确定";
            this.btn确定.Size = new System.Drawing.Size(75, 23);
            this.btn确定.TabIndex = 6;
            this.btn确定.Text = "确  定";
            this.btn确定.UseVisualStyleBackColor = true;
            this.btn确定.Click += new System.EventHandler(this.btn确定_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.pnl处理结果, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.pnl处理时间, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pnl异常情况, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(260, 100);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // pnl处理结果
            // 
            this.pnl处理结果.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl处理结果.Location = new System.Drawing.Point(81, 69);
            this.pnl处理结果.Name = "pnl处理结果";
            this.pnl处理结果.Size = new System.Drawing.Size(176, 28);
            this.pnl处理结果.TabIndex = 6;
            // 
            // pnl处理时间
            // 
            this.pnl处理时间.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl处理时间.Location = new System.Drawing.Point(81, 36);
            this.pnl处理时间.Name = "pnl处理时间";
            this.pnl处理时间.Size = new System.Drawing.Size(176, 27);
            this.pnl处理时间.TabIndex = 5;
            // 
            // pnl异常情况
            // 
            this.pnl异常情况.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl异常情况.Location = new System.Drawing.Point(81, 3);
            this.pnl异常情况.Name = "pnl异常情况";
            this.pnl异常情况.Size = new System.Drawing.Size(176, 27);
            this.pnl异常情况.TabIndex = 4;
            // 
            // 实时监控异常处理
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(284, 162);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btn确定);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "实时监控异常处理";
            this.Text = "实时监控异常情况";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.实时监控异常处理_FormClosing);
            this.Load += new System.EventHandler(this.实时监控异常处理_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn确定;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnl处理结果;
        private System.Windows.Forms.Panel pnl处理时间;
        private System.Windows.Forms.Panel pnl异常情况;
    }
}