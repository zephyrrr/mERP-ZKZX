namespace Zkzx.Forms
{
    partial class 实时监控异常报警
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
            this.cbo异常类型 = new System.Windows.Forms.ComboBox();
            this.btn确定 = new System.Windows.Forms.Button();
            this.btn取消 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "异常类型";
            // 
            // cbo异常类型
            // 
            this.cbo异常类型.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo异常类型.FormattingEnabled = true;
            this.cbo异常类型.Items.AddRange(new object[] {
            "车辆故障",
            "堵车",
            "交通事故",
            "其它"});
            this.cbo异常类型.Location = new System.Drawing.Point(118, 25);
            this.cbo异常类型.Name = "cbo异常类型";
            this.cbo异常类型.Size = new System.Drawing.Size(130, 20);
            this.cbo异常类型.TabIndex = 1;
            // 
            // btn确定
            // 
            this.btn确定.Location = new System.Drawing.Point(92, 69);
            this.btn确定.Name = "btn确定";
            this.btn确定.Size = new System.Drawing.Size(75, 23);
            this.btn确定.TabIndex = 2;
            this.btn确定.Text = "确  定";
            this.btn确定.UseVisualStyleBackColor = true;
            this.btn确定.Click += new System.EventHandler(this.btn确定_Click);
            // 
            // btn取消
            // 
            this.btn取消.Location = new System.Drawing.Point(173, 69);
            this.btn取消.Name = "btn取消";
            this.btn取消.Size = new System.Drawing.Size(75, 23);
            this.btn取消.TabIndex = 3;
            this.btn取消.Text = "取  消";
            this.btn取消.UseVisualStyleBackColor = true;
            this.btn取消.Click += new System.EventHandler(this.btn取消_Click);
            // 
            // 实时监控异常报警
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(284, 112);
            this.Controls.Add(this.btn取消);
            this.Controls.Add(this.btn确定);
            this.Controls.Add(this.cbo异常类型);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "实时监控异常报警";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "实时监控异常报警";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbo异常类型;
        private System.Windows.Forms.Button btn确定;
        private System.Windows.Forms.Button btn取消;
    }
}