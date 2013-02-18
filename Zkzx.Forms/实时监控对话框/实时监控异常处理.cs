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
    public partial class 实时监控异常处理 : MyTemplateForm
    {
        public 实时监控异常处理(车辆作业 clzy)
        {
            InitializeComponent();

            m_clzy = clzy;
        }

        private 车辆作业 m_clzy;
        private ControlManager<车辆作业> m_cm = new ControlManager<车辆作业>(null);
        private 作业异常Dao m_dao = new 作业异常Dao();
        private int m_处理 = -1;

        private void 实时监控异常处理_Load(object sender, EventArgs e)
        {
            AssociateDataControls(new Control[] { pnl异常情况, pnl处理时间, pnl处理结果 }, 
                m_cm.DisplayManager, "实时监控_车辆作业_异常处理");
            m_cm.AllowInsert = true;
            m_cm.AddNew();

            车辆作业 clzy = m_clzy;
            using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
            {
                rep.Initialize(clzy.最新作业状态, clzy);
            }

            m_cm.DisplayManager.DataControls["异常情况"].ReadOnly = true;
            m_cm.DisplayManager.DataControls["异常情况"].SelectedDataValue = clzy.最新作业状态.异常情况;

            m_cm.DisplayManager.DataControls["处理时间"].SelectedDataValue = DateTime.Now;

            m_dao.处理作业异常(m_clzy, null, null, true);
        }

        private void btn确定_Click(object sender, EventArgs e)
        {
            if (m_cm.SaveCurrent())
            {
                string 处理结果 = (string)m_cm.DisplayManager.DataControls["处理结果"].SelectedDataValue;

                m_处理 = 1;
                DateTime? 处理时间 = (DateTime?)m_cm.DisplayManager.DataControls["处理时间"].SelectedDataValue;

                m_dao.处理作业异常(m_clzy, 处理结果, 处理时间, true);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void 实时监控异常处理_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_处理 < 0)
            {
                m_dao.处理作业异常(m_clzy, null, null, false);
            }
        }
    }
}
