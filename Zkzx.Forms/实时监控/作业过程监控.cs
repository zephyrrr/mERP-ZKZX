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
    public partial class 作业过程监控 : MyTemplateForm, IRefreshDataForm
    {

        public 作业过程监控()
        {
            InitializeComponent();
        }

        private DataUnboundGrid m_作业监控区Grid;
      private void 作业过程监控_Load(object sender, EventArgs e)
        {
            m_作业监控区Grid = base.AssociateBoundGrid(pnl监控区, "实时监控_车辆作业_作业监控区") as DataUnboundGrid;
            //m_作业监控区Grid.SetSearchRowVisible(true);


            m_作业监控区Grid.DataRowTemplate.Cells["车牌号"].DoubleClick += new EventHandler(作业监控区_DoubleClick);
            m_作业监控区Grid.DataRowTemplate.Cells["起始途径终止地"].DoubleClick += new EventHandler(作业监控区_DoubleClick);
            m_作业监控区Grid.DataRowTemplate.Cells["处理状态"].DoubleClick += new EventHandler(作业监控区_DoubleClick);
            m_作业监控区Grid.DataRowTemplate.Cells["车辆位置"].DoubleClick += new EventHandler(作业监控区_DoubleClick);
            m_作业监控区Grid.DataRowTemplate.Cells["作业号"].DoubleClick += new EventHandler(作业监控区_DoubleClick);
            m_作业监控区Grid.DataRowTemplate.Cells["承运时间要求"].DoubleClick += new EventHandler(作业监控区_DoubleClick);

            m_作业监控区Grid.DataRowTemplate.Cells["缓急程度"].MouseEnter += new EventHandler(作业实时监控_MouseEnter);
            m_作业监控区Grid.DataRowTemplate.Cells["异常报警"].DoubleClick += new EventHandler(作业监控区_DoubleClick);

            Helper.SetGridDefault(this, m_作业监控区Grid);
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

        

        public void RefreshData()
        {
            m_作业监控区Grid.DisplayManager.SearchManager.LoadData();      
        }
      
        void 作业监控区_DoubleClick(object sender, EventArgs e)
        {
            监控级调度主界面.OnCellDoubleClick(sender, e);
        }
    }
}
