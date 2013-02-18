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
    public partial class 车辆报停启用登记 : MyTemplateForm, IRefreshDataForm
    {
        public 车辆报停启用登记()
        {
            InitializeComponent();
        }

        private ArchiveUnboundGrid m_信息区Grid;
        private void 车辆报停启用登记_Load(object sender, EventArgs e)
        {
            m_信息区Grid = base.AssociateArchiveGrid(pnl信息区, "车队级调度_车辆报停启用") as ArchiveUnboundGrid;
            Helper.SetGridDefault(this, m_信息区Grid);
        }

        public void RefreshData()
        {
            m_信息区Grid.DisplayManager.SearchManager.LoadData();
        }
    }
}
