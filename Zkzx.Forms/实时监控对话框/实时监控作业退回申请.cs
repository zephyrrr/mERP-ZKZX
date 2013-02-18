using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using Zkzx.Model;
using Feng;
using Feng.Windows.Forms;

namespace Zkzx.Forms
{
    public partial class 实时监控作业退回申请 : MyTemplateForm
    {
        public 实时监控作业退回申请(车辆作业 clzy)
        {
            InitializeComponent();

            m_clzy = clzy;
        }

        private 车辆作业 m_clzy;
        private DisplayManager<车辆作业> m_dm = new DisplayManager<车辆作业>(null);

        private void 实时监控异常处理_Load(object sender, EventArgs e)
        {
            AssociateDataControls(new Control[] { 
                pnl作业号, pnl车牌号, pnl任务性质, pnl货物特征, pnl时间要求, pnl起始地途经地终止地, 
                 pnl备注,  pnl理由 }, m_dm, "实时监控_车辆作业_作业退回申请");

            车辆作业 clzy = m_clzy;

            m_dm.SetDataBinding(new List<车辆作业> { m_clzy }, string.Empty);
            m_dm.Position = 0;
            m_dm.DisplayCurrent();

            StringBuilder sb = new StringBuilder();
            using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<专家任务>())
            {
                rep.Initialize(clzy.专家任务.任务, clzy.专家任务);
                foreach (var rw in clzy.专家任务.任务)
                {
                    if (sb.Length > 0)
                        sb.Append(",");
                    sb.Append(rw.货物特征);
                }
            }
            m_dm.DataControls["货物特征"].SelectedDataValue = sb.ToString();

            m_dm.DataControls["理由"].ReadOnly = false;
        }

      
        private void btn退回_Click(object sender, EventArgs e)
        {
            车辆作业Dao dao1 = new 车辆作业Dao();
            专家任务Dao dao2 = new 专家任务Dao();
            DaoHelper.DoInRepository((rep) =>
                {
                    m_clzy.IsActive = false;
                    string ly = (string)m_dm.DataControls["理由"].SelectedDataValue;
                    m_clzy.备注 += "已退回," + (string.IsNullOrEmpty(ly) ? string.Empty : ("理由：" + ly));
                    dao1.Update(rep, m_clzy);

                    m_clzy.专家任务.车辆作业 = null;
                    //m_clzy.专家任务.是否已下达 = false;
                    dao2.Update(rep, m_clzy.专家任务);
                });
        }
    }
}
