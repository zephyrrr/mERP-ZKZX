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
    public partial class 承运时间要求详情 : MyTemplateForm
    {
        public 承运时间要求详情(车辆作业 clzy)
        {
            System.Diagnostics.Debug.Assert(clzy != null, "承运时间要求详情车辆作业不能为空");

            InitializeComponent();
            m_clzy = clzy;

            m_dm = new DisplayManager<车辆作业>(null);
            AssociateDataControls(new Control[] { 
                pnl作业号, pnl车主, pnl车牌号, pnl驾驶员, pnl驾驶员联系方式, pnl车主联系方式 
                }, m_dm, "实时监控_车辆作业_承运时间要求详情");

            m_任务集合区 = base.AssociateBoundGrid(pnl任务集合, "实时监控_车辆作业_承运时间要求详情_任务") as DataUnboundGrid;
        }

        private 车辆作业 m_clzy;
        private IDisplayManager m_dm;
        private DataUnboundGrid m_任务集合区;

        private void 承运时间要求详情_Load(object sender, EventArgs e)
        {
            using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
            {
                var clzy = rep.Get<车辆作业>(m_clzy.ID);

                m_dm.SetDataBinding(new List<车辆作业> { clzy }, string.Empty);

                var dm = m_dm;
                // dm.DataControls["箱型编号"].SelectedDataValue = clzy.专家任务.任务[0].箱型编号;

                var list = m_任务集合区.DisplayManager.SearchManager.GetData(SearchExpression.Eq("专家任务:车辆作业", clzy),
                    new List<ISearchOrder> { SearchOrder.Asc("任务性质") });

                int idx = 0;
                List<Xceed.Grid.DataRow> realTimeRows = new List<Xceed.Grid.DataRow>();
                Xceed.Grid.DataRow row;
                foreach (var i in list)
                {
                    if (idx != 0)
                    {
                        m_任务集合区.AddSpaceDataRow();
                    }

                    row = m_任务集合区.DataRows.AddNew();
                    m_任务集合区.SetDataRowsIListData(i, row);
                    row.EndEdit();
                    row.Cells["序号"].Value = idx + 1;

                    foreach (Xceed.Grid.Cell cell in row.Cells)
                    {
                        单个作业监控详情.SetCellValueNone(cell);
                    }

                    row = m_任务集合区.DataRows.AddNew();
                    row.EndEdit();
                    realTimeRows.Add(row);
                    row.ForeColor = System.Drawing.Color.Red;

                    单个作业监控详情.SetCellValueNone(row.Cells["疏港期限"]);
                    单个作业监控详情.SetCellValueNone(row.Cells["还箱进港时间"]);

                    idx++;
                }

                FillRowsRealTimes(clzy, realTimeRows);

                switch (clzy.专家任务.任务性质)
                {
                    case 专家任务性质.静态优化套箱:
                    case 专家任务性质.动态优化套箱:
                        单个作业监控详情.SetCellValueNone(realTimeRows[0].Cells["装货时间"]);
                        单个作业监控详情.SetCellValueNone(realTimeRows[0].Cells["还箱进港时间"]);

                        单个作业监控详情.SetCellValueNone(realTimeRows[1].Cells["疏港期限"]);
                        单个作业监控详情.SetCellValueNone(realTimeRows[1].Cells["卸货时间"]);
                        break;
                    case 专家任务性质.静态优化进口箱带货:
                    case 专家任务性质.动态优化进口箱带货:
                        单个作业监控详情.SetCellValueNone(realTimeRows[0].Cells["装货时间"]);

                        单个作业监控详情.SetCellValueNone(realTimeRows[1].Cells["提箱时间"]);
                        单个作业监控详情.SetCellValueNone(realTimeRows[1].Cells["还箱进港时间"]);
                        break;
                    case 专家任务性质.静态优化出口箱带货:
                    case 专家任务性质.动态优化出口箱带货:
                        单个作业监控详情.SetCellValueNone(realTimeRows[0].Cells["卸货时间"]);

                        单个作业监控详情.SetCellValueNone(realTimeRows[1].Cells["疏港期限"]);
                        单个作业监控详情.SetCellValueNone(realTimeRows[1].Cells["还箱进港时间"]);
                        break;
                    case 专家任务性质.静态优化进口箱对箱:
                    case 专家任务性质.动态优化进口箱对箱:
                        单个作业监控详情.SetCellValueNone(realTimeRows[0].Cells["装货时间"]);

                        单个作业监控详情.SetCellValueNone(realTimeRows[1].Cells["装货时间"]);
                        break;
                    case 专家任务性质.静态优化出口箱对箱:
                    case 专家任务性质.动态优化出口箱对箱:
                        单个作业监控详情.SetCellValueNone(realTimeRows[0].Cells["卸货时间"]);

                        单个作业监控详情.SetCellValueNone(realTimeRows[1].Cells["卸货时间"]);
                        break;
                    case 专家任务性质.静态优化进出口对箱:
                    case 专家任务性质.动态优化进出口对箱:
                        单个作业监控详情.SetCellValueNone(realTimeRows[0].Cells["装货时间"]);

                        单个作业监控详情.SetCellValueNone(realTimeRows[1].Cells["卸货时间"]);
                        break;
                    case 专家任务性质.无优化进口拆箱:
                        单个作业监控详情.SetCellValueNone(realTimeRows[0].Cells["装货时间"]);
                        break;
                    case 专家任务性质.无优化出口装箱:
                        单个作业监控详情.SetCellValueNone(realTimeRows[0].Cells["卸货时间"]);
                        break;
                    case 专家任务性质.无优化I带货:
                        break;
                    case 专家任务性质.无优化E带货:
                        break;
                }
            }
        }

        internal static int FillRowsRealTimes(车辆作业 clzy, List<Xceed.Grid.DataRow> rows)
        {
            int[] taskIdx = null;
            string[] importantAreas = null;
            string[] importantTaskStatus = null;
            string[] importantWorkStatus = null;
            ModelHelper.Get任务状态(clzy.专家任务, out taskIdx, out importantAreas, out importantTaskStatus, out importantWorkStatus);
            //int preIndex = 0;
            //if (preEntity != null && !string.IsNullOrEmpty(preEntity.作业地点))
            //{
            //    string[] ss = preEntity.作业地点.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //    preIndex = ss.Length;
            //}

            int idx = 0;
            Xceed.Grid.DataRow row;
            using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<作业监控状态>())
            {
                IList<作业监控状态> list = rep.List<作业监控状态>("from 作业监控状态 where 车辆作业 = :车辆作业 AND ((作业状态 LIKE '%中%') AND 作业状态 NOT LIKE '%途中%')",
                                new Dictionary<string, object> { { "车辆作业", clzy } });

                foreach (var i in list)
                {
                    idx = Math.Min(idx, taskIdx.Length - 1);
                    row = rows[taskIdx[idx]]; //m_任务集合区.DataRows[taskIdx[idx]];

                    //if (i.作业状态 == importantWorkStatus[idx] + "中")
                    {
                        if (importantWorkStatus[idx] == "进港" || importantWorkStatus[idx] == "还箱")
                        {
                            TryFillCellDateTimeValue(row.Cells["还箱进港时间"], i.时间);
                        }
                        else if (importantWorkStatus[idx].EndsWith("提箱"))
                        {
                            TryFillCellDateTimeValue(row.Cells["提箱时间"], i.时间);
                        }
                        else if (importantWorkStatus[idx].EndsWith("装货"))
                        {
                            TryFillCellDateTimeValue(row.Cells["装货时间"], i.时间);
                        }
                        else if (importantWorkStatus[idx].EndsWith("卸货"))
                        {
                            TryFillCellDateTimeValue(row.Cells["卸货时间"], i.时间);
                        }
                        //else
                        //{
                        //    TryFillCellDateTimeValue(row.Cells[importantWorkStatus[idx] + "时间"], i.时间);
                        //}
                        idx++;
                    }
                }
            }

            idx = Math.Min(idx, taskIdx.Length - 1);
            return taskIdx[idx];
        }

        private static void TryFillCellDateTimeValue(Xceed.Grid.Cell cell, DateTime time)
        {
            if (cell.ParentColumn.DataType == typeof(DateTime))
            {
                cell.Value = time;
            }
            else if (cell.ParentColumn.DataType == typeof(string))
            {
                cell.Value = Helper.DateTime2String(time);
            }
        }
    }
}
