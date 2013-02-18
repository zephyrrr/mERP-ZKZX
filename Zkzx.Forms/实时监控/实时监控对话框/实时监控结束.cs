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
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 实时监控结束 : MyTemplateForm
    {
        public 实时监控结束(车辆作业 clzy)
        {
            InitializeComponent();
            m_clzy = clzy;
        }
        private IArchiveGrid grid;
        private void 实时监控结束_Load(object sender, EventArgs e)
        {
            m_cm = AssociateDataControlsInControlManager(new Control[] { 
                pnl作业号,  pnl车牌号, pnl驾驶员编号, pnl联系电话,  pnl备注}, 
                "实时监控_车辆作业_监控结束");
            grid = base.AssociateArchiveGrid(pnl作业流程, "实时监控_车辆作业_监控结束_任务");

            var dm = m_cm.DisplayManager;
            dm.DataControls["备注"].ReadOnly = false;

            using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
            {
                var clzy = rep.Get<车辆作业>(m_clzy.ID);

                dm.DataControls["作业号"].SelectedDataValue = clzy.作业号;
                dm.DataControls["车牌号"].SelectedDataValue = clzy.车辆.车牌号;
                dm.DataControls["驾驶员编号"].SelectedDataValue = clzy.驾驶员编号;
                dm.DataControls["联系电话"].SelectedDataValue = clzy.驾驶员.联系方式;
                dm.DataControls["备注"].SelectedDataValue = clzy.备注;

                grid.DisplayManager.SetDataBinding(clzy.专家任务.任务, string.Empty);
                for(int i=0; i<grid.DataRows.Count; ++i)
                {
                    grid.DataRows[i].Cells["序号"].Value = i;
                }
            }
        }

        private 车辆作业 m_clzy;
        private 作业监控Dao m_dao = new 作业监控Dao();
        private IControlManager m_cm;

        private void btn确定_Click(object sender, EventArgs e)
        {
            m_dao.结束监控(m_clzy, System.DateTime.Now, (string)m_cm.DisplayManager.DataControls["备注"].SelectedDataValue);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
