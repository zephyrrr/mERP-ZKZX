using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Feng;
using Feng.Grid;
using Feng.Windows.Forms;
using Feng.Utils;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 作业开始结束 : MyTemplateForm, IRefreshDataForm
    {
        public 作业开始结束()
        {
            InitializeComponent();
        }
        private DataUnboundGrid  m_作业开始结束Grid;
        private void 作业开始结束_Load(object sender, EventArgs e)
        {
            m_作业开始结束Grid = base.AssociateBoundGrid(pnl监控区, "实时监控_车辆作业_作业开始结束") as DataUnboundGrid;

            m_作业开始结束Grid.ChangeControlPositionAccordColumn(lbl作业开始结束操作区, "驾驶员编号");

            m_作业开始结束Grid.DataRowTemplate.Cells["箱型编号"].DoubleClick += new EventHandler(作业监控区_DoubleClick);
            m_作业开始结束Grid.DataRowTemplate.Cells["车牌号"].DoubleClick += new EventHandler(作业监控区_DoubleClick);
            m_作业开始结束Grid.DataRowTemplate.Cells["起始途径终止地"].DoubleClick += new EventHandler(作业监控区_DoubleClick);
            m_作业开始结束Grid.DataRowTemplate.Cells["开始"].DoubleClick += new EventHandler(作业监控区_DoubleClick);
            m_作业开始结束Grid.DataRowTemplate.Cells["退回"].DoubleClick += new EventHandler(作业监控区_DoubleClick);
            m_作业开始结束Grid.DataRowTemplate.Cells["结束"].DoubleClick += new EventHandler(作业监控区_DoubleClick);

            m_作业开始结束Grid.DataRowTemplate.Cells["作业状态"].DoubleClick += new EventHandler(作业监控区_DoubleClick);
            m_作业开始结束Grid.DataRowTemplate.Cells["作业号"].DoubleClick += new EventHandler(作业监控区_DoubleClick);
            m_作业开始结束Grid.DataRowTemplate.Cells["承运时间要求"].DoubleClick += new EventHandler(作业监控区_DoubleClick);

            m_作业开始结束Grid.DataRowTemplate.Cells["缓急程度"].MouseEnter += new EventHandler(作业实时监控_MouseEnter);

            Helper.SetGridDefault(this, m_作业开始结束Grid);
        }

        private void 作业实时监控_MouseEnter(object sender, EventArgs e)
        {
            Xceed.Grid.Cell src = sender as Xceed.Grid.Cell;
            string s = (src.ParentRow.Tag as 车辆作业).备注;
            if (!string.IsNullOrEmpty(s))
            {
                (src.GridControl as MyGrid).GridHelper.GridToolTip.RemoveAll();
                (src.GridControl as MyGrid).GridHelper.GridToolTip.SetToolTip((src.GridControl as MyGrid), s);
            }
        }

        private Feng.Map.MapForm m_mapForm = null;
        private void m_mapForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing)
            {
                e.Cancel = true;
                m_mapForm.Visible = false;
            }
        }

        public void RefreshData()
        {
            m_作业开始结束Grid.DisplayManager.SearchManager.LoadData();
        }
      

        void 作业监控区_DoubleClick(object sender, EventArgs e)
        {
            监控级调度主界面.OnCellDoubleClick(sender, e);
        }
        
    }
}
