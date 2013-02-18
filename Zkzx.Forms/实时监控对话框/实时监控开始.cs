using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Zkzx.Model;
using Feng;
using Feng.Windows.Forms;

namespace Zkzx.Forms
{
    public partial class 实时监控开始 : MyTemplateForm
    {
        public 实时监控开始()
        {
        }
        public 实时监控开始(车辆作业 clzy)
        {
            InitializeComponent();
            m_clzy = clzy;
        }

        private 车辆作业 m_clzy;
        private 作业监控Dao m_dao = new 作业监控Dao();
        private IControlManager m_cm;
        private void 实时监控开始_Load(object sender, EventArgs e)
        {
            m_cm = AssociateDataControlsInControlManager(new Control[] { pnl驾驶员, pnl车载ID号, pnl开始时间, pnl备注}, 
                "实时监控_车辆作业_监控开始");
            m_cm.AllowInsert = true;
            m_cm.AddNew();

            if (m_clzy != null)
            {
                m_cm.DisplayManager.DataControls["驾驶员编号"].SelectedDataValue = m_clzy.驾驶员编号;
                m_cm.DisplayManager.DataControls["车载Id号"].SelectedDataValue = m_clzy.车载Id号;
                m_cm.DisplayManager.DataControls["开始时间"].SelectedDataValue = m_clzy.开始时间;
                m_cm.DisplayManager.DataControls["备注"].SelectedDataValue = m_clzy.备注;
            }

            if (m_cm.DisplayManager.DataControls["开始时间"].SelectedDataValue == null)
            {
                m_cm.DisplayManager.DataControls["开始时间"].SelectedDataValue = System.DateTime.Now;
            }

            if (m_cm.DisplayManager.DataControls["车载Id号"].SelectedDataValue == null)
            {
                m_cm.DisplayManager.DataControls["车载Id号"].SelectedDataValue = m_clzy.车辆.车载Id号;
            }

            if (m_cm.DisplayManager.DataControls["驾驶员编号"].SelectedDataValue == null)
            {
                m_cm.DisplayManager.DataControls["驾驶员编号"].SelectedDataValue = m_clzy.车辆.主驾驶员编号;
            }
        }

        private void btn确定_Click(object sender, EventArgs e)
        {
            //if (m_clzy.专家任务.任务性质 == 专家任务性质.无优化I带货)
            //{
            //    foreach (任务 rw in m_clzy.专家任务.任务)
            //    {
            //        if (rw.任务性质 == 任务性质.出口装箱 || rw.任务性质 == 任务性质.进口拆箱)
            //        {
            //            rw.封志号 = txt封志号.Text.Trim();
            //        }
            //    }
            //}

            if (m_cm.SaveCurrent())
            {
                string jsybh = (string)m_cm.DisplayManager.DataControls["驾驶员编号"].SelectedDataValue;
                string czidh = (string)m_cm.DisplayManager.DataControls["车载Id号"].SelectedDataValue;
                string bz = (string)m_cm.DisplayManager.DataControls["备注"].SelectedDataValue;
                DateTime kssj = (DateTime)m_cm.DisplayManager.DataControls["开始时间"].SelectedDataValue;

                if (string.IsNullOrEmpty(jsybh))
                {
                    MessageForm.ShowWarning("请先指定驾驶员！");
                    return;
                }

                m_dao.开始监控(m_clzy, kssj, jsybh, czidh);

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
