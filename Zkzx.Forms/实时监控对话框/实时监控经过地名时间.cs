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
    public partial class 实时监控经过地名时间 : MyTemplateForm
    {
        public 实时监控经过地名时间(车辆作业 clzy, string areas)
        {
            InitializeComponent();
            m_clzy = clzy;
            m_areas = areas.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); ;
        }

        private string[] m_areas;
        private 车辆作业 m_clzy;
        private 作业监控Dao m_dao = new 作业监控Dao();
        private IControlManager m_cm;

        private void btn确定_Click(object sender, EventArgs e)
        {     
            //switch (this.Text)
            //{
            //    case "起始地":
            //        m_clzy.起始地时间 = (DateTime?)m_cm.DisplayManager.DataControls["途径地时间"].SelectedDataValue;
            //        break;
            //    case "途径地":
            //        m_clzy.途径地时间 = (DateTime?)m_cm.DisplayManager.DataControls["途径地时间"].SelectedDataValue;
            //        break;
            //    case "终止地":
            //        m_clzy.终止地时间 = (DateTime?)m_cm.DisplayManager.DataControls["途径地时间"].SelectedDataValue;
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException("实时监控经过地名时间 out of range");
            //}

            if (m_cm.SaveCurrent())
            {
                m_dao.更新作业监控状态2(m_clzy,
                    (DateTime)m_cm.DisplayManager.DataControls["途径地时间"].SelectedDataValue, 
                    (string)m_cm.DisplayManager.DataControls["途径地编号"].SelectedDataValue,
                    "结束");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        private void 实时监控经过地名时间_Load(object sender, EventArgs e)
        {
            m_cm = AssociateDataControlsInControlManager(new Control[] { pnl途径地, pnl途径地时间 },
                "实时监控_车辆作业_途径时间");
            m_cm.AllowInsert = true;
            m_cm.AddNew();

            //switch (this.Text)
            //{
            //    case "起始地":
            //        m_cm.DisplayManager.DataControls["途径地"].SelectedDataValue = m_clzy.专家任务.起始地编号;
            //        break;
            //    case "途径地":
            //        m_cm.DisplayManager.DataControls["途径地"].SelectedDataValue = m_clzy.专家任务.途径地编号;
            //        break;
            //    case "终止地":
            //        m_cm.DisplayManager.DataControls["途径地"].SelectedDataValue = m_clzy.专家任务.终止地编号;
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException("实时监控经过地名 out of range");
            //}

            if (m_cm.DisplayManager.DataControls["途径地时间"].SelectedDataValue == null)
            {
                m_cm.DisplayManager.DataControls["途径地时间"].SelectedDataValue = System.DateTime.Now;
            }

            m_cm.DisplayManager.DataControls["途径地编号"].ReadOnly = true;
            if (m_clzy.最新作业状态 == null || string.IsNullOrEmpty(m_clzy.最新作业状态.作业地点))
            {
                m_cm.DisplayManager.DataControls["途径地编号"].SelectedDataValue = m_areas[0];
            }
            else 
            {
                string[] ss = m_clzy.最新作业状态.作业地点.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);

                if (ss.Length < m_areas.Length)
                {
                    m_cm.DisplayManager.DataControls["途径地编号"].SelectedDataValue = m_areas[ss.Length];
                }
            }

            if (m_cm.DisplayManager.DataControls["途径地编号"].SelectedDataValue == null)
            {
                this.Close();
            }
        }
    }
}
