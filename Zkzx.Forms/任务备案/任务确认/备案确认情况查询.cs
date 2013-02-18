using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Feng;
using Feng.Grid;
using Feng.Windows.Forms;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 备案确认情况查询 : MyTemplateForm, IRefreshDataForm
    {
        public 备案确认情况查询()
        {
            InitializeComponent();
        }

        private ArchiveUnboundGrid m_信息区Grid;
        private 任务Dao m_dao = new 任务Dao();

        private void 备案确认情况查询_Load(object sender, EventArgs e)
        {
            m_信息区Grid = base.AssociateArchiveGrid(pnl信息区, "查询统计_备案确认情况查询") as ArchiveUnboundGrid;
            m_信息区Grid.DataRowTemplate.Cells["预录入号"].DoubleClick += new EventHandler(备案确认情况查询_DoubleClick);

            m_信息区Grid.ChangeControlPositionAccordColumn(lbl反馈区, "任务号");

            Helper.SetGridDefault(this, m_信息区Grid);
        }

        public void RefreshData()
        {
            m_信息区Grid.DisplayManager.SearchManager.LoadData();
        }

        void 备案确认情况查询_DoubleClick(object sender, EventArgs e)
        {
            Xceed.Grid.Cell srcCell = sender as Xceed.Grid.Cell;
            if (srcCell.ParentColumn.FieldName == "预录入号")
            {
                任务确认详细信息 form = new 任务确认详细信息(m_信息区Grid.ControlManager as IWindowControlManager<任务>);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_信息区Grid.ResetRowData(srcCell.ParentRow as Xceed.Grid.DataRow);
                }
            }
        }
    }
}
