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
    public partial class 车辆详细信息 : MyTemplateForm
    {
        public 车辆详细信息(车辆 cl)
        {
            System.Diagnostics.Debug.Assert(cl != null, "车辆详细信息车辆不能为空");

            InitializeComponent();
            m_cl = cl;

            m_dm = AssociateDataControlsInDisplayManager(new Control[] { 
                pnl车辆忠诚度, pnl车牌号, pnl车型, pnl车主联系方式, pnl核定载重, 
                pnl驾驶员联系方式, pnl是否监管车, pnl所属车队, pnl车主, pnl驾驶员 
                }, "实时监控_车辆作业_车辆详细信息");
        }

        private 车辆 m_cl;
        private IDisplayManager m_dm;

        private void 车辆详细信息_Load(object sender, EventArgs e)
        {
            using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
            {
                //rep.Initialize(m_cl.车主, m_cl);
                //rep.Initialize(m_cl.主驾驶员, m_cl);
                var cl = rep.Get<车辆>(m_cl.ID);
                m_dm.SetDataBinding(new List<车辆> { cl }, string.Empty);

                //m_dm.DataControls["车主联系方式"].SelectedDataValue = m_cl.车主.联系方式;
                //m_dm.DataControls["驾驶员联系方式"].SelectedDataValue = m_cl.主驾驶员.联系方式;
            }
        }
    }
}
