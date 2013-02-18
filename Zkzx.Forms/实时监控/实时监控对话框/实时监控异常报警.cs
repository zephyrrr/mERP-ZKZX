using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 实时监控异常报警 : Form
    {
        public 实时监控异常报警(车辆作业 clzy)
        {
            InitializeComponent();
            m_clzy = clzy;
        }

        private 车辆作业 m_clzy;
        private 作业异常Dao m_dao = new 作业异常Dao();

        private void btn确定_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbo异常类型.Text))
            {
                Feng.MessageForm.ShowWarning("请选择异常类型");
                return;
            }

            m_dao.新作业异常(m_clzy, cbo异常类型.Text, null, System.DateTime.Now);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btn取消_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
