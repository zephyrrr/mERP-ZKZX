using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Feng;
using Feng.Grid;
using Feng.Windows.Forms;
using Feng.Utils;
using Zkzx.Model;

namespace Zkzx.Forms
{
    // Bug：
    // 点刷新，再点移出。原因刷新后出口箱任务号Cell.Tag失去了任务对象。(暂不考虑动态任务刷新后能移出)
    // 同样的错误出现在本身就包含出口箱任务的记录中，Cell.Value由.py获取，Cell.Tag = null
    public partial class 专家调度一级动态优化套箱 : Feng.Windows.Forms.MyTemplateForm, IRefreshDataForm
    {
        #region "Constructor"
        public 专家调度一级动态优化套箱()
        {
            InitializeComponent();
        }

        private DataUnboundGrid m_topGrid, m_bottomGrid;
        private void 专家调度一级动态优化_Load(object sender, EventArgs e)
        {
            m_topGrid = base.AssociateBoundGrid(pnl任务集合top, "专家级调度_一级动态优化_动态套箱_任务集合top") as DataUnboundGrid;
            m_bottomGrid = base.AssociateBoundGrid(pnl任务集合bottom, "专家级调度_一级静态优化_静态套箱_任务集合bottom") as DataUnboundGrid;

            m_topGrid.DataRowTemplate.Cells["确认"].DoubleClick += new EventHandler(专家调度一级动态优化_DoubleClick);
            m_topGrid.DataRowTemplate.Cells["专家任务号"].DoubleClick += new EventHandler(专家调度一级动态优化_DoubleClick);
            m_topGrid.DataRowTemplate.Cells["车辆位置"].DoubleClick += new EventHandler(专家调度一级动态优化_DoubleClick);

            m_topGrid.EnableDragDrop = true;
            m_bottomGrid.EnableDragDrop = true;

            m_topGrid.GridDragStart += new EventHandler<GridDataGragEventArgs>(专家调度一级动态优化.专家调度一级动态优化_GridDragStart);
            m_bottomGrid.GridDragStart += new EventHandler<GridDataGragEventArgs>(专家调度一级动态优化.专家调度一级动态优化_GridDragStart);

            m_topGrid.GridDragDrop += new DragEventHandler(专家调度一级动态优化.专家调度一级动态优化_GridDragDrop);
            m_bottomGrid.GridDragDrop += new DragEventHandler(专家调度一级动态优化.专家调度一级动态优化_GridDragDrop);

            m_topGrid.GridDragOver += new DragEventHandler(专家调度一级动态优化.专家调度一级动态优化_GridDragOver);
            m_bottomGrid.GridDragOver += new DragEventHandler(专家调度一级动态优化.专家调度一级动态优化_GridDragOver);

            m_topGrid.GotFocus += new EventHandler(m_动态套箱_任务集合Grid_GotFocus);
            m_bottomGrid.GotFocus += new EventHandler(m_动态套箱_任务集合Grid_GotFocus);

            Helper.SetGridDefault(this, m_topGrid);
            Helper.SetGridDefault(this, m_bottomGrid);
            Helper.SetCellButton(m_topGrid, "确认", btn批量确认);
            m_topGrid.ChangeControlPositionAccordColumn(lbl预配集合, "配对任务号");
            m_topGrid.ChangeControlPositionAccordColumn(lbl新任务, "新任务号");
        }
        #endregion

        #region "Auto"
        private 专家任务性质 m_hereZjrwxz = 专家任务性质.动态优化套箱;

        private void 专家调度一级动态优化_DoubleClick(object sender, EventArgs e)
        {
            专家调度一级动态优化.专家调度一级动态优化_DoubleClick(sender as Xceed.Grid.Cell, m_hereZjrwxz);
        }
        private void btn批量确认_Click(object sender, EventArgs e)
        {
            专家调度一级静态优化.批量确认_Click(m_topGrid, m_hereZjrwxz);
        }

        private object m_lastGrid;
        private void m_动态套箱_任务集合Grid_GotFocus(object sender, EventArgs e)
        {
            m_lastGrid = sender;
        }

        private void btn计算机辅助优化_Click(object sender, EventArgs e)
        {
            if (m_lastGrid == m_topGrid)
                专家调度一级静态优化.选择组合(m_topGrid, m_bottomGrid, m_hereZjrwxz);
            else
                专家调度一级静态优化.选择组合(m_bottomGrid, m_topGrid, m_hereZjrwxz);
        }

        private void Clear组合()
        {
            专家调度一级静态优化.Clear组合(m_topGrid, m_bottomGrid);
        }

        private void btn全自动优化_Click(object sender, EventArgs e)
        {
            专家调度一级静态优化.自动优化(m_topGrid, m_bottomGrid, m_hereZjrwxz);
        }
        #endregion

        public void RefreshData()
        {
            m_topGrid.DisplayManager.SearchManager.LoadData();
            m_bottomGrid.DisplayManager.SearchManager.LoadData();
        }
    }
}
