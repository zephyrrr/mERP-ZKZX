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
    public partial class 单个作业监控详情 : MyTemplateForm
    {
        public 单个作业监控详情(车辆作业 clzy)
        {
            System.Diagnostics.Debug.Assert(clzy != null, "单个作业监控详情车辆作业不能为空");

            InitializeComponent();
            m_clzy = clzy;

            m_dm = new DisplayManager<车辆作业>(null);
            AssociateDataControls(new Control[] { 
                pnl作业号, pnl车主, pnl车牌号, pnl驾驶员, pnl驾驶员联系方式, pnl车主联系方式 
                }, m_dm, "实时监控_车辆作业_单个作业监控详情");

            m_任务集合1 = base.AssociateBoundGrid(pnl任务集合1, "实时监控_车辆作业_单个作业监控详情_任务");
            m_任务集合2 = base.AssociateBoundGrid(pnl任务集合2, "实时监控_车辆作业_单个作业监控详情_任务2");
        }

        private 车辆作业 m_clzy;
        private IDisplayManager m_dm;
        private IBoundGrid m_任务集合1, m_任务集合2;

        private void 单个作业监控详情_Load(object sender, EventArgs e)
        {
            using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
            {
                var clzy = rep.Get<车辆作业>(m_clzy.ID);

                m_dm.SetDataBinding(new List<车辆作业> { clzy }, string.Empty);

                m_任务集合1.DisplayManager.SearchManager.LoadData(SearchExpression.Eq("专家任务:车辆作业", clzy),
                    new List<ISearchOrder> { SearchOrder.Asc("任务性质") });
                m_任务集合1.DisplayManager.SearchManager.WaitLoadData();
                m_任务集合2.DisplayManager.SearchManager.LoadData(SearchExpression.Eq("专家任务:车辆作业", clzy),
                    new List<ISearchOrder> { SearchOrder.Asc("任务性质") });
                m_任务集合2.DisplayManager.SearchManager.WaitLoadData();

                List<Xceed.Grid.DataRow> realTimeRows = new List<Xceed.Grid.DataRow>();
                foreach (Xceed.Grid.DataRow i in m_任务集合2.DataRows)
                {
                    realTimeRows.Add(i);
                }
                int currentRowIdx = 承运时间要求详情.FillRowsRealTimes(clzy, realTimeRows);

                if (clzy.最新作业状态 != null)
                {
                    Xceed.Grid.DataRow row = m_任务集合2.DataRows[currentRowIdx];
                    row.Cells["车辆位置"].Value = clzy.最新作业状态.车辆位置;
                    row.Cells["作业状态"].Value = clzy.最新作业状态.作业状态;
                    row.Cells["预计到达时间"].Value = clzy.最新作业状态.预计到达时间;
                    row.Cells["异常情况"].Value = clzy.最新作业状态.异常情况;
                }

                switch (clzy.专家任务.任务性质)
                {
                    case 专家任务性质.静态优化套箱:
                    case 专家任务性质.动态优化套箱:
                        SetCellValueNone(m_任务集合2.DataRows[0].Cells["装货时间"]);
                        SetCellValueNone(m_任务集合2.DataRows[0].Cells["还箱进港时间"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货地详细地址"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货时间要求"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货联系人"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货联系电话"]);

                        SetCellValueNone(m_任务集合2.DataRows[1].Cells["提箱时间"]);
                        SetCellValueNone(m_任务集合2.DataRows[1].Cells["卸货时间"]);
                        SetCellValueNone(m_任务集合1.DataRows[1].Cells["卸货地详细地址"]);
                        SetCellValueNone(m_任务集合1.DataRows[1].Cells["卸货时间要求"]);
                        SetCellValueNone(m_任务集合1.DataRows[1].Cells["卸货联系人"]);
                        SetCellValueNone(m_任务集合1.DataRows[1].Cells["卸货联系电话"]);
                        break;
                    case 专家任务性质.静态优化进口箱带货:
                    case 专家任务性质.动态优化进口箱带货:
                        SetCellValueNone(m_任务集合2.DataRows[0].Cells["装货时间"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货地详细地址"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货时间要求"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货联系人"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货联系电话"]);

                        SetCellValueNone(m_任务集合2.DataRows[1].Cells["提箱时间"]);
                        SetCellValueNone(m_任务集合2.DataRows[1].Cells["还箱进港时间"]);
                        break;
                    case 专家任务性质.静态优化出口箱带货:
                    case 专家任务性质.动态优化出口箱带货:
                        SetCellValueNone(m_任务集合2.DataRows[0].Cells["卸货时间"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["卸货地详细地址"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["卸货时间要求"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["卸货联系人"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["卸货联系电话"]);

                        SetCellValueNone(m_任务集合2.DataRows[1].Cells["提箱时间"]);
                        SetCellValueNone(m_任务集合2.DataRows[1].Cells["还箱进港时间"]);
                        break;
                    case 专家任务性质.静态优化进口箱对箱:
                    case 专家任务性质.动态优化进口箱对箱:
                        SetCellValueNone(m_任务集合2.DataRows[0].Cells["装货时间"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货地详细地址"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货时间要求"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货联系人"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货联系电话"]);

                        SetCellValueNone(m_任务集合2.DataRows[1].Cells["装货时间"]);
                        SetCellValueNone(m_任务集合1.DataRows[1].Cells["装货地详细地址"]);
                        SetCellValueNone(m_任务集合1.DataRows[1].Cells["装货时间要求"]);
                        SetCellValueNone(m_任务集合1.DataRows[1].Cells["装货联系人"]);
                        SetCellValueNone(m_任务集合1.DataRows[1].Cells["装货联系电话"]);
                        break;
                    case 专家任务性质.静态优化出口箱对箱:
                    case 专家任务性质.动态优化出口箱对箱:
                        SetCellValueNone(m_任务集合2.DataRows[0].Cells["卸货时间"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["卸货地详细地址"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["卸货时间要求"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["卸货联系人"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["卸货联系电话"]);

                        SetCellValueNone(m_任务集合2.DataRows[1].Cells["卸货时间"]);
                        SetCellValueNone(m_任务集合1.DataRows[1].Cells["卸货地详细地址"]);
                        SetCellValueNone(m_任务集合1.DataRows[1].Cells["卸货时间要求"]);
                        SetCellValueNone(m_任务集合1.DataRows[1].Cells["卸货联系人"]);
                        SetCellValueNone(m_任务集合1.DataRows[1].Cells["卸货联系电话"]);
                        break;
                    case 专家任务性质.静态优化进出口对箱:
                    case 专家任务性质.动态优化进出口对箱:
                        SetCellValueNone(m_任务集合2.DataRows[0].Cells["装货时间"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货地详细地址"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货时间要求"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货联系人"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货联系电话"]);

                        SetCellValueNone(m_任务集合2.DataRows[1].Cells["卸货时间"]);
                        SetCellValueNone(m_任务集合1.DataRows[1].Cells["卸货地详细地址"]);
                        SetCellValueNone(m_任务集合1.DataRows[1].Cells["卸货时间要求"]);
                        SetCellValueNone(m_任务集合1.DataRows[1].Cells["卸货联系人"]);
                        SetCellValueNone(m_任务集合1.DataRows[1].Cells["卸货联系电话"]);
                        break;
                    case 专家任务性质.无优化进口拆箱:
                        SetCellValueNone(m_任务集合2.DataRows[0].Cells["装货时间"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货地详细地址"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货时间要求"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货联系人"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["装货联系电话"]);
                        break;
                    case 专家任务性质.无优化出口装箱:
                        SetCellValueNone(m_任务集合2.DataRows[0].Cells["卸货时间"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["卸货地详细地址"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["卸货时间要求"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["卸货联系人"]);
                        SetCellValueNone(m_任务集合1.DataRows[0].Cells["卸货联系电话"]);
                        break;
                    case 专家任务性质.无优化I带货:
                        break;
                    case 专家任务性质.无优化E带货:
                        break;
                }
            }
        }

        internal static void SetCellValueNone(Xceed.Grid.Cell cell)
        {
            if (cell.ParentColumn.DataType == typeof(string))
            {
                if (cell.Value != null && string.IsNullOrEmpty((string)cell.Value))
                {
                    cell.Value = null;
                }
            }
            if (cell != null)
            {
                cell.NullText = "-";
            }
        }
    }
}
