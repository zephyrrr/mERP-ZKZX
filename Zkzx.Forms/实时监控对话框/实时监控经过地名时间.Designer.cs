namespace Zkzx.Forms
{
    partial class 实时监控经过地名时间
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
            this.btn确定 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pnl途径地时间 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl途径地 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "经过地名的时间";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn确定
            // 
            this.btn确定.Location = new System.Drawing.Point(178, 127);
            this.btn确定.Name = "btn确定";
            this.btn确定.Size = new System.Drawing.Size(70, 23);
            this.btn确定.TabIndex = 2;
            this.btn确定.Text = "确  定";
            this.btn确定.UseVisualStyleBackColor = true;
            this.btn确定.Click += new System.EventHandler(this.btn确定_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 54);
            this.label2.TabIndex = 3;
            this.label2.Text = "经过地名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl途径地时间
            // 
            this.pnl途径地时间.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl途径地时间.Location = new System.Drawing.Point(107, 57);
            this.pnl途径地时间.Name = "pnl途径地时间";
            this.pnl途径地时间.Size = new System.Drawing.Size(150, 49);
            this.pnl途径地时间.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.pnl途径地, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnl途径地时间, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(260, 109);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // pnl途径地
            // 
            this.pnl途径地.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl途径地.Location = new System.Drawing.Point(107, 3);
            this.pnl途径地.Name = "pnl途径地";
            this.pnl途径地.Size = new System.Drawing.Size(150, 48);
            this.pnl途径地.TabIndex = 0;
            // 
            // 实时监控经过地名时间
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(284, 162);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btn确定);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "实时监控经过地名时间";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "实时监控经过地名时间";
            this.Load += new System.EventHandler(this.实时监控经过地名时间_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn确定;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnl途径地时间;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnl途径地;
    }
}