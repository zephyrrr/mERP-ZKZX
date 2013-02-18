namespace Zkzx.Forms
{
    partial class 预录入任务详细信息
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
            this.pnl任务号 = new System.Windows.Forms.Panel();
            this.lbl任务号 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl备案明细窗体 = new System.Windows.Forms.Panel();
            this.btn修改 = new System.Windows.Forms.Button();
            this.btn预录入发送 = new System.Windows.Forms.Button();
            this.btn删除 = new System.Windows.Forms.Button();
            this.btn暂存待确认 = new System.Windows.Forms.Button();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl任务号
            // 
            this.pnl任务号.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl任务号.ForeColor = System.Drawing.Color.Red;
            this.pnl任务号.Location = new System.Drawing.Point(100, 0);
            this.pnl任务号.Margin = new System.Windows.Forms.Padding(0);
            this.pnl任务号.Name = "pnl任务号";
            this.pnl任务号.Size = new System.Drawing.Size(100, 37);
            this.pnl任务号.TabIndex = 53;
            // 
            // lbl任务号
            // 
            this.lbl任务号.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl任务号.AutoSize = true;
            this.lbl任务号.ForeColor = System.Drawing.Color.Red;
            this.lbl任务号.ImeMode = System.Windows.Forms.ImeMode.On;
            this.lbl任务号.Location = new System.Drawing.Point(3, 0);
            this.lbl任务号.Name = "lbl任务号";
            this.lbl任务号.Size = new System.Drawing.Size(94, 37);
            this.lbl任务号.TabIndex = 52;
            this.lbl任务号.Text = "任务号";
            this.lbl任务号.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lbl任务号, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pnl任务号, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(672, 358);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 37);
            this.tableLayoutPanel2.TabIndex = 57;
            // 
            // pnl备案明细窗体
            // 
            this.pnl备案明细窗体.Location = new System.Drawing.Point(12, 12);
            this.pnl备案明细窗体.Name = "pnl备案明细窗体";
            this.pnl备案明细窗体.Size = new System.Drawing.Size(860, 340);
            this.pnl备案明细窗体.TabIndex = 58;
            // 
            // btn修改
            // 
            this.btn修改.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn修改.ImeMode = System.Windows.Forms.ImeMode.On;
            this.btn修改.Location = new System.Drawing.Point(405, 365);
            this.btn修改.Name = "btn修改";
            this.btn修改.Size = new System.Drawing.Size(75, 23);
            this.btn修改.TabIndex = 62;
            this.btn修改.Text = "修改任务";
            this.btn修改.UseVisualStyleBackColor = true;
            this.btn修改.Click += new System.EventHandler(this.btn修改_Click);
            // 
            // btn预录入发送
            // 
            this.btn预录入发送.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn预录入发送.ImeMode = System.Windows.Forms.ImeMode.On;
            this.btn预录入发送.Location = new System.Drawing.Point(524, 365);
            this.btn预录入发送.Name = "btn预录入发送";
            this.btn预录入发送.Size = new System.Drawing.Size(75, 23);
            this.btn预录入发送.TabIndex = 61;
            this.btn预录入发送.Text = "预录入发送";
            this.btn预录入发送.UseVisualStyleBackColor = true;
            this.btn预录入发送.Click += new System.EventHandler(this.btn预录入发送_Click);
            // 
            // btn删除
            // 
            this.btn删除.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn删除.ImeMode = System.Windows.Forms.ImeMode.On;
            this.btn删除.Location = new System.Drawing.Point(324, 365);
            this.btn删除.Name = "btn删除";
            this.btn删除.Size = new System.Drawing.Size(75, 23);
            this.btn删除.TabIndex = 60;
            this.btn删除.Text = "删除";
            this.btn删除.UseVisualStyleBackColor = true;
            this.btn删除.Click += new System.EventHandler(this.btn删除_Click);
            // 
            // btn暂存待确认
            // 
            this.btn暂存待确认.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn暂存待确认.ImeMode = System.Windows.Forms.ImeMode.On;
            this.btn暂存待确认.Location = new System.Drawing.Point(243, 365);
            this.btn暂存待确认.Name = "btn暂存待确认";
            this.btn暂存待确认.Size = new System.Drawing.Size(75, 23);
            this.btn暂存待确认.TabIndex = 59;
            this.btn暂存待确认.Text = "暂存待确认";
            this.btn暂存待确认.UseVisualStyleBackColor = true;
            this.btn暂存待确认.Click += new System.EventHandler(this.btn暂存待确认_Click);
            // 
            // 预录入任务详细信息
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 412);
            this.Controls.Add(this.btn修改);
            this.Controls.Add(this.btn预录入发送);
            this.Controls.Add(this.btn删除);
            this.Controls.Add(this.btn暂存待确认);
            this.Controls.Add(this.pnl备案明细窗体);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "预录入任务详细信息";
            this.Text = "任务详细信息";
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl任务号;
        private System.Windows.Forms.Label lbl任务号;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel pnl备案明细窗体;
        private System.Windows.Forms.Button btn修改;
        private System.Windows.Forms.Button btn预录入发送;
        private System.Windows.Forms.Button btn删除;
        private System.Windows.Forms.Button btn暂存待确认;


    }
}