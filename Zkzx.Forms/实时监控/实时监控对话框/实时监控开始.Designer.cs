namespace Zkzx.Forms
{
    partial class 实时监控开始
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
            this.lbl车载ID号 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn确定 = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl车载ID号 = new System.Windows.Forms.Panel();
            this.pnl备注 = new System.Windows.Forms.Panel();
            this.pnl驾驶员 = new System.Windows.Forms.Panel();
            this.pnl开始时间 = new System.Windows.Forms.Panel();
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
            this.label1.Size = new System.Drawing.Size(82, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "驾驶员";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl车载ID号
            // 
            this.lbl车载ID号.AutoSize = true;
            this.lbl车载ID号.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl车载ID号.Location = new System.Drawing.Point(0, 46);
            this.lbl车载ID号.Margin = new System.Windows.Forms.Padding(0);
            this.lbl车载ID号.Name = "lbl车载ID号";
            this.lbl车载ID号.Size = new System.Drawing.Size(82, 46);
            this.lbl车载ID号.TabIndex = 1;
            this.lbl车载ID号.Text = "车载ID号";
            this.lbl车载ID号.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(0, 92);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 47);
            this.label3.TabIndex = 2;
            this.label3.Text = "开始时间";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(205, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 46);
            this.label4.TabIndex = 3;
            this.label4.Text = "备注";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn确定
            // 
            this.btn确定.ImeMode = System.Windows.Forms.ImeMode.On;
            this.btn确定.Location = new System.Drawing.Point(112, 157);
            this.btn确定.Name = "btn确定";
            this.btn确定.Size = new System.Drawing.Size(75, 23);
            this.btn确定.TabIndex = 8;
            this.btn确定.Text = "确  定";
            this.btn确定.UseVisualStyleBackColor = true;
            this.btn确定.Click += new System.EventHandler(this.btn确定_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.On;
            this.btnCancel.Location = new System.Drawing.Point(226, 157);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.Controls.Add(this.pnl车载ID号, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl车载ID号, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnl备注, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnl驾驶员, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnl开始时间, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(410, 139);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // pnl车载ID号
            // 
            this.pnl车载ID号.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl车载ID号.Location = new System.Drawing.Point(85, 49);
            this.pnl车载ID号.Name = "pnl车载ID号";
            this.pnl车载ID号.Size = new System.Drawing.Size(117, 40);
            this.pnl车载ID号.TabIndex = 6;
            // 
            // pnl备注
            // 
            this.pnl备注.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl备注.Location = new System.Drawing.Point(269, 3);
            this.pnl备注.Name = "pnl备注";
            this.tableLayoutPanel1.SetRowSpan(this.pnl备注, 3);
            this.pnl备注.Size = new System.Drawing.Size(138, 133);
            this.pnl备注.TabIndex = 4;
            // 
            // pnl驾驶员
            // 
            this.pnl驾驶员.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl驾驶员.Location = new System.Drawing.Point(85, 3);
            this.pnl驾驶员.Name = "pnl驾驶员";
            this.pnl驾驶员.Size = new System.Drawing.Size(117, 40);
            this.pnl驾驶员.TabIndex = 5;
            // 
            // pnl开始时间
            // 
            this.pnl开始时间.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl开始时间.Location = new System.Drawing.Point(85, 95);
            this.pnl开始时间.Name = "pnl开始时间";
            this.pnl开始时间.Size = new System.Drawing.Size(117, 41);
            this.pnl开始时间.TabIndex = 7;
            // 
            // 实时监控开始
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(434, 192);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btn确定);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "实时监控开始";
            this.Text = "开始";
            this.Load += new System.EventHandler(this.实时监控开始_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl车载ID号;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn确定;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnl备注;
        private System.Windows.Forms.Panel pnl车载ID号;
        private System.Windows.Forms.Panel pnl驾驶员;
        private System.Windows.Forms.Panel pnl开始时间;
    }
}