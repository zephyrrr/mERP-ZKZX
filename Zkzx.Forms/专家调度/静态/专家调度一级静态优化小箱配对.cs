using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Feng;
using Feng.Grid;
using Feng.Windows.Forms;
using Feng.Utils;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 专家调度一级静态优化小箱配对 : Feng.Windows.Forms.MyTemplateForm, IRefreshDataForm
    {
        #region "Constructor"
        public 专家调度一级静态优化小箱配对()
        {
            InitializeComponent();
        }

        private DataUnboundGrid m_topGrid, m_bottomGrid;
        
        private void 专家调度一级静态优化_Load(object sender, EventArgs e)
        {
            m_topGrid = base.AssociateBoundGrid(pnl任务集合top, "专家级调度_一级静态优化_小箱配对_任务集合top") as DataUnboundGrid;
            m_bottomGrid = base.AssociateBoundGrid(pnl任务集合bottom, "专家级调度_一级静态优化_小箱配对_任务集合bottom") as DataUnboundGrid;

            m_topGrid.DataRowTemplate.Cells["确认"].DoubleClick += new EventHandler(专家调度一级静态优化_DoubleClick);
            m_bottomGrid.DataRowTemplate.Cells["确认"].DoubleClick += new EventHandler(专家调度一级静态优化_DoubleClick);
            //m_小箱配对_任务集合topGrid.DataRowTemplate.Cells["取消"].DoubleClick += new EventHandler(专家调度一级静态优化_DoubleClick);
            //m_小箱配对_任务集合bottomGrid.DataRowTemplate.Cells["取消"].DoubleClick += new EventHandler(专家调度一级静态优化_DoubleClick);
            //m_小箱配对_任务集合topGrid.DataRowTemplate.Cells["移出"].DoubleClick += new EventHandler(专家调度一级静态优化_DoubleClick);
            //m_小箱配对_任务集合bottomGrid.DataRowTemplate.Cells["移出"].DoubleClick += new EventHandler(专家调度一级静态优化_DoubleClick);
            m_topGrid.DataRowTemplate.Cells["紧急下达"].DoubleClick += new EventHandler(专家调度一级静态优化_DoubleClick);
            m_bottomGrid.DataRowTemplate.Cells["紧急下达"].DoubleClick += new EventHandler(专家调度一级静态优化_DoubleClick);

            m_topGrid.EnableDragDrop = true;
            m_bottomGrid.EnableDragDrop = true;

            m_bottomGrid.GridDragStart += new EventHandler<GridDataGragEventArgs>(专家调度一级静态优化.专家调度一级静态优化_GridDragStart);
            m_topGrid.GridDragStart += new EventHandler<GridDataGragEventArgs>(专家调度一级静态优化.专家调度一级静态优化_GridDragStart);

            m_topGrid.GridDragDrop += new DragEventHandler(专家调度一级静态优化.专家调度一级静态优化_GridDragDrop);
            m_bottomGrid.GridDragDrop += new DragEventHandler(专家调度一级静态优化.专家调度一级静态优化_GridDragDrop);

            m_topGrid.GridDragOver += new DragEventHandler(专家调度一级静态优化.专家调度一级静态优化_GridDragOver);
            m_bottomGrid.GridDragOver += new DragEventHandler(专家调度一级静态优化.专家调度一级静态优化_GridDragOver);

            m_topGrid.GotFocus += new EventHandler(m_静态套箱_任务集合Grid_GotFocus);
            m_bottomGrid.GotFocus += new EventHandler(m_静态套箱_任务集合Grid_GotFocus);

            Helper.SetGridDefault(this, m_topGrid);
            Helper.SetGridDefault(this, m_bottomGrid);
            Helper.SetCellButton(m_topGrid, "确认", btn批量确认);
            Helper.SetCellButton(m_bottomGrid, "确认", btn批量确认2);
            m_topGrid.ChangeControlPositionAccordColumn(lbl预配集合, "配对任务号");
            m_topGrid.ChangeControlPositionAccordColumn(lbl新任务, "新任务号");
            m_bottomGrid.ChangeControlPositionAccordColumn(lbl预配集合bottom, "配对任务号");
            m_bottomGrid.ChangeControlPositionAccordColumn(lbl新任务bottom, "新任务号");
        }

        #endregion

        #region "Auto"
        private 专家任务性质? m_hereZjrwxz = null;

        void 专家调度一级静态优化_DoubleClick(object sender, EventArgs e)
        {
            专家调度一级静态优化.专家调度一级静态优化_DoubleClick(sender as Xceed.Grid.Cell, m_hereZjrwxz);
        }

        private void btn批量确认_Click(object sender, EventArgs e)
        {
            专家调度一级静态优化.批量确认_Click(m_topGrid, m_hereZjrwxz);
        }
        private void 批量确认2_Click(object sender, EventArgs e)
        {
            专家调度一级静态优化.批量确认_Click(m_bottomGrid, m_hereZjrwxz);
        }
        private object m_lastGrid;
        private void m_静态套箱_任务集合Grid_GotFocus(object sender, EventArgs e)
        {
            m_lastGrid = sender;
        }
        private void btn计算机辅助优化_Click(object sender, EventArgs e)
        {
            if (m_lastGrid == m_topGrid)
                专家调度一级静态优化.选择组合(m_topGrid, m_bottomGrid, new List<专家任务性质> { 专家任务性质.静态优化进口箱对箱, 专家任务性质.静态优化出口箱对箱, 专家任务性质.静态优化进出口对箱 });
            else
                专家调度一级静态优化.选择组合(m_bottomGrid, m_topGrid, new List<专家任务性质> { 专家任务性质.静态优化进口箱对箱, 专家任务性质.静态优化出口箱对箱, 专家任务性质.静态优化进出口对箱 });
        }
        private void Clear组合()
        {
            专家调度一级静态优化.Clear组合(m_topGrid, m_bottomGrid);
        }
        
        private void btn全自动优化_Click(object sender, EventArgs e)
        {
            专家调度一级静态优化.自动优化(m_topGrid, m_bottomGrid, 
                new List<专家任务性质> { 专家任务性质.静态优化进口箱对箱, 专家任务性质.静态优化出口箱对箱, 专家任务性质.静态优化进出口对箱 });
        }
        #endregion

        public void RefreshData()
        {
            m_topGrid.DisplayManager.SearchManager.LoadData();
            m_bottomGrid.DisplayManager.SearchManager.LoadData();
        }

        
    }
}
