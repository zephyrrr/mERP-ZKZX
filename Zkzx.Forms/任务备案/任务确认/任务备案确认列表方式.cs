using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using Feng;
using Feng.Grid;
using Feng.Windows.Forms;
using Feng.Utils;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 任务备案确认列表方式 : Feng.Windows.Forms.MyTemplateForm, IControlManagerContainer, IGridNamesContainer, IRefreshDataForm
    {
        IControlManager IControlManagerContainer.ControlManager
        {
            get { return m_cm; }
        }
        public string[] GridName
        {
            get { return new string[] { m_gridName }; }
        }

        public 任务备案确认列表方式()
        {
            InitializeComponent();
        }

        private IWindowControlManager m_cm;
        private string m_gridName = "任务备案_任务正式备案";
        private ArchiveUnboundGrid m_rightGrid;
        private void 任务正式备案_Load(object sender, EventArgs e)
        {
            m_rightGrid = base.AssociateArchiveGrid(pnl任务预备案信息区, m_gridName) as ArchiveUnboundGrid;

            m_cm = m_rightGrid.ControlManager as IWindowControlManager;
            m_rightGrid.DataRowTemplate.Cells["预录入号"].DoubleClick += new EventHandler(任务正式备案_DoubleClick);
            m_rightGrid.DataRowTemplate.Cells["确认"].DoubleClick += new EventHandler(任务正式备案_DoubleClick);
            m_rightGrid.DataRowTemplate.Cells["拒绝"].DoubleClick += new EventHandler(任务正式备案_DoubleClick);

            m_rightGrid.ChangeControlPositionAccordColumn(lbl备案确认区, "确认");
            m_rightGrid.ChangeControlPositionAccordColumn(btn批量确认, "确认");

            Helper.SetGridDefault(this, m_rightGrid);

            m_cm.DisplayManager.SearchManager.LoadData();
        }

        void 任务正式备案_DoubleClick(object sender, EventArgs e)
        {
            Xceed.Grid.Cell srcCell = sender as Xceed.Grid.Cell;
            var row = srcCell.ParentRow as Xceed.Grid.DataRow;
            if (row == null)
                return;

            m_cm.DisplayManager.Position = row.Index;

            if (srcCell.ParentColumn.FieldName == "预录入号")
            {
                任务确认详细信息  form = new 任务确认详细信息(m_cm);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_rightGrid.ResetRowData(srcCell.ParentRow as Xceed.Grid.DataRow);
                }
            }
            else if (srcCell.ParentColumn.FieldName == "确认")
            {
                if (row.Cells["任务号"].Value != null)
                    return;
                bool ret = 任务备案确认主界面.Save(m_cm, SaveType.正式备案确认);
                if (ret)
                {
                    row.Cells["任务号"].Value = (m_cm.DisplayManager.CurrentItem as 任务).任务号;
                }
            }
            else if (srcCell.ParentColumn.FieldName == "拒绝")
            {
                if (row.Cells["任务号"].Value != null)
                    return;

                拒绝原因 jjyy = new 拒绝原因(m_cm.DisplayManager.CurrentItem as 任务);
                if (jjyy.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    bool ret = 任务备案确认主界面.Save(m_cm, SaveType.拒绝确认);
                    if (ret)
                    {
                        row.Cells["任务号"].Value = "NA";
                    }
                }
            }
        }


        private 任务Dao m_dao = new 任务Dao();
        private void btn批量确认_Click(object sender, EventArgs e)
        {
            Helper.btn批量确认_Click(m_rightGrid, (sender1, e1) =>
            {
                任务正式备案_DoubleClick(sender1, e1);
            });
        }

        public void RefreshData()
        {
            m_rightGrid.ReloadData();
        }
        private void btn网上委托任务_Click(object sender, EventArgs e)
        {
            using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<网上委托任务>())
            {
                IList<网上委托任务> wswtrwList = rep.List<网上委托任务>("from 网上委托任务 where 是否受理 = 'false'", null);

                if (wswtrwList != null && wswtrwList.Count > 0)
                {
                    int successCount = 0;//成功导入的任务数量                    
                    foreach (网上委托任务 wswtrw in wswtrwList)
                    {
                        using (IRepository rep1 = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                        {
                            try
                            {
                                rep1.BeginTransaction();
                                任务 rw = new 任务();
                                rw.任务来源 = 任务来源.网上;
                                rw.IsActive = true;
                                rw.备注 = wswtrw.备注;
                                rw.船名 = wswtrw.船名;
                                rw.航次 = wswtrw.航次;
                                rw.封志号 = wswtrw.封志号;
                                rw.还箱进港点编号 = wswtrw.还箱进港点编号;
                                rw.还箱进港时间要求 = wswtrw.还箱进港时间要求;
                                rw.货名 = wswtrw.货名;
                                rw.货物特征 = wswtrw.货物特征;
                                rw.价值 = wswtrw.价值;
                                rw.提单号 = wswtrw.提单号;
                                rw.任务性质 = wswtrw.任务性质;
                                rw.是否小箱 = wswtrw.是否小箱;
                                rw.提示性箱号 = wswtrw.提示性箱号;
                                rw.提箱点编号 = wswtrw.提箱点编号;
                                rw.提箱时间要求 = wswtrw.提箱时间要求;
                                rw.委托联系人 = wswtrw.委托联系人;
                                rw.委托人编号 = wswtrw.委托人编号;
                                rw.委托时间 = wswtrw.委托时间;
                                rw.箱号 = wswtrw.箱号;
                                rw.箱属船公司编号 = wswtrw.箱属船公司编号;
                                rw.箱型编号 = wswtrw.箱型编号;
                                rw.卸货地编号 = wswtrw.卸货地编号;
                                rw.卸货联系手机 = wswtrw.卸货联系手机;
                                rw.卸货联系座机 = wswtrw.卸货联系座机;
                                rw.卸货联系人 = wswtrw.卸货联系人;
                                rw.卸货时间要求始 = wswtrw.卸货时间要求始;
                                rw.卸货时间要求止 = wswtrw.卸货时间要求止;
                                rw.重量 = wswtrw.重量;
                                rw.转关箱标志 = wswtrw.转关箱标志;
                                rw.装货地编号 = wswtrw.装货地编号;
                                rw.装货联系手机 = wswtrw.装货联系手机;
                                rw.装货联系座机 = wswtrw.装货联系座机;
                                rw.装货联系人 = wswtrw.装货联系人;
                                rw.装货时间要求始 = wswtrw.装货时间要求始;
                                rw.装货时间要求止 = wswtrw.装货时间要求止;
                                m_dao.Save(rep1, rw);
                                rep1.CommitTransaction();

                                rep.BeginTransaction();
                                wswtrw.是否受理 = true;
                                rep.Update(wswtrw);
                                rep.CommitTransaction();
                                successCount++;
                            }
                            catch (Exception ex)
                            {
                                rep1.RollbackTransaction();
                                rep.RollbackTransaction();
                                if (MessageForm.ShowYesNo("网上委托任务\"" + wswtrw.ID + "\"有备案错误。" + Environment.NewLine + "\"是\"跳过任务，\"否\"显示错误"))
                                {
                                    continue;
                                }
                                else
                                {
                                    ServiceProvider.GetService<IExceptionProcess>().ProcessWithNotify(ex);
                                }
                            }
                        }
                    }
                    m_rightGrid.ReloadData();
                    MessageForm.ShowInfo("成功导入 " + successCount + " 条任务。");
                }
            }
        }

        private void btn以票计的进口箱任务批量确认_Click(object sender, EventArgs e)
        {
            ServiceProvider.GetService<IApplication>().ExecuteAction("任务备案_进口箱批量任务确认");
        }
    }
}
