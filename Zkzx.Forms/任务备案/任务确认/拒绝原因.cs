using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using Feng;
using Feng.Windows.Forms;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 拒绝原因 : System.Windows.Forms.Form
    {
        public 拒绝原因(任务 rw)
        {
            InitializeComponent();
            m_rw = rw;
        }

        public 拒绝原因(进口票 piao)
        {
            InitializeComponent();
            m_piao = piao;
        }

        private 任务 m_rw = null;
        private 进口票 m_piao = null;

        private string Get拒绝原因()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (checkBox1.Checked)
                sb.Append(checkBox1.Text + ";");
            if (checkBox2.Checked)
                sb.Append(checkBox2.Text + ";");
            if (checkBox3.Checked)
                sb.Append(checkBox3.Text + ";");
            if (checkBox4.Checked)
                sb.Append(txt拒绝原因.Text + ";");

            string s = sb.ToString();
            return s.Substring(0, s.Length - 1);
        }
        private void btn确定_Click(object sender, EventArgs e)
        {
            string s = Get拒绝原因();
            if (string.IsNullOrEmpty(s))
            {
                MessageForm.ShowWarning("请填写拒绝原因！");
                return;
            }
            s = s.Trim();

            string jjyy = "拒绝原因:" + s;
            if (m_rw != null)
            {
                m_rw.拒绝原因 = s;
                m_rw.备注 = string.IsNullOrEmpty(m_rw.备注) ? jjyy : m_rw.备注 + System.Environment.NewLine + jjyy;
            }
            if (m_piao != null)
            {
                m_piao.备注 = string.IsNullOrEmpty(m_piao.备注) ? jjyy : m_piao.备注 + System.Environment.NewLine + jjyy;
                //foreach (任务 rw in m_piao.任务)
                //{
                //    rw.拒绝原因 = txt拒绝原因.Text.Trim();
                //    rw.备注 = string.IsNullOrEmpty(rw.备注) ? "拒绝原因:" + rw.拒绝原因 : rw.备注 + System.Environment.NewLine + "拒绝原因:" + rw.拒绝原因;
                //}
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
