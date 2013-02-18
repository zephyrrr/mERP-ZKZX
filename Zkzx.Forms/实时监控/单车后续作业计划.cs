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
    public partial class 单车后续作业计划 : MyTemplateForm
    {
        public 单车后续作业计划(车辆 cl)
        {
            InitializeComponent();
            m_cl = cl;
        }

        private 车辆 m_cl = null;

        private void 单车后续作业计划_Load(object sender, EventArgs e)
        {
            if (m_cl != null)
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
                {
                    var cl = rep.Get<车辆>(m_cl.ID);

                    txt车型.Text = cl.车型;
                    txt车主.Text = cl.车主.简称;
                    txt承运车辆.Text = cl.车牌号;
                    txt核定载重.Text = cl.核定载重;
                    txt监管车.Text = cl.监管车 ? "是" : "否";
                    txt联系电话.Text = cl.主驾驶员.联系方式;
                }

                var rightGrid = base.AssociateBoundGrid(pnl车辆作业集合, "车队级调度_静态任务下达_单车后续作业计划");
                rightGrid.DisplayManager.SearchManager.LoadData(SearchExpression.And(
                    SearchExpression.Eq("车辆作业:车辆", m_cl), SearchExpression.IsNull("车辆作业:结束时间")),
                    new List<ISearchOrder> { SearchOrder.Asc("车辆作业:Created") });
            }
        }
    }
}
