using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Feng;
using Feng.Windows.Forms;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 紧急下达 : MyTemplateForm
    {
        public 紧急下达(专家任务 zjrw)
        {
            m_zjrw = zjrw;
            InitializeComponent();

            m_cm = AssociateDataControlsInControlManager(new Control[] { 
                pnl区域编号, pnl时间要求始, pnl时间要求止, pnl缓急程度}, 
                m_gridName);
        }

        // 动态调度-下达
        public 紧急下达(专家任务 zjrw, 任务 rw, 专家任务性质? xz)
            : this(zjrw)
        {
            m_rw = rw;
            m_xz = xz;
        }

        private 专家任务 m_zjrw;
        private 任务 m_rw;
        private 专家任务性质? m_xz;
        private 专家任务Dao m_专家任务Dao = new 专家任务Dao();
        private string m_gridName = "专家级调度_一级静态优化_紧急任务下达";
        private IControlManager m_cm;
        private void 紧急下达_Load(object sender, EventArgs e)
        {
            m_cm.AllowInsert = true;
            m_cm.AddNew();

            if (m_rw != null)
            {
                m_cm.DisplayManager.DataControls["区域编号"].SelectedDataValue = m_zjrw.区域编号;
                m_cm.DisplayManager.DataControls["区域编号"].ReadOnly = true;

                m_cm.DisplayManager.DataControls["时间要求始"].SelectedDataValue = m_zjrw.时间要求始;
                m_cm.DisplayManager.DataControls["时间要求止"].SelectedDataValue = m_zjrw.时间要求止;
                m_cm.DisplayManager.DataControls["缓急程度"].SelectedDataValue = m_zjrw.缓急程度;
            }
        }

        private void btn确定_Click(object sender, EventArgs e)
        {
            if (m_cm.SaveCurrent())
            {
                m_zjrw.时间要求始 = (DateTime)m_cm.DisplayManager.DataControls["时间要求始"].SelectedDataValue;
                m_zjrw.时间要求止 = (DateTime)m_cm.DisplayManager.DataControls["时间要求止"].SelectedDataValue;
                m_zjrw.区域编号 = (string)m_cm.DisplayManager.DataControls["区域编号"].SelectedDataValue;
                m_zjrw.缓急程度 = (int)m_cm.DisplayManager.DataControls["缓急程度"].SelectedDataValue;

                // 静态紧急下达
                if (m_rw == null)
                {
                    m_专家任务Dao.下达专家任务(m_zjrw, System.DateTime.Now);
                }
                // 动态调度
                else
                {
                    m_zjrw = m_专家任务Dao.生成专家任务(m_zjrw, m_rw, m_xz);
                }
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        public 专家任务 专家任务
        {
            get { return m_zjrw; }
        }
    }
}
