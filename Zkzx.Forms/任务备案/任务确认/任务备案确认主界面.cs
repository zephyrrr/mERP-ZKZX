using System;
using System.Collections.Generic;
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
    public partial class 任务备案确认主界面 : MyTemplateForm, IControlManagerContainer, IGridNamesContainer, IRefreshDataForm
    {
        IControlManager IControlManagerContainer.ControlManager
        {
            get { return m_cm; }
        }
        public string[] GridName
        {
            get { return new string[] { m_gridName }; }
        }

        public 任务备案确认主界面()
        {
            InitializeComponent();
        }

        private IWindowControlManager m_cm;
        private IWindowControlManager<任务> m_cmT;
        private ArchiveUnboundGrid m_rightGrid;
        private string m_gridName = "任务备案_任务正式备案二";

        private void 任务正式备案二_Load(object sender, EventArgs e)
        {
            m_rightGrid = base.AssociateArchiveGrid(pnl请求区, "任务备案_任务正式备案二") as ArchiveUnboundGrid;
            m_cm = m_rightGrid.ControlManager as IWindowControlManager;
            m_cmT = m_cm as IWindowControlManager<任务>;

            MyTemplateForm.AddControl(pnl备案明细窗体, new 备案明细窗体(m_cm, m_gridName));
            base.AssociateDataControls(new System.Windows.Forms.Control[] { pnl任务号 }, m_cm.DisplayManager, m_gridName);

            m_cm.StateControls.Add(new StateControl(btn修改, StateType.View));
            m_cm.StateControls.Add(new StateControl(btn拒绝, StateType.View));
            m_cm.StateControls.Add(new StateControl(btn放弃, false));
            m_cm.StateControls.Add(new StateControl(btn备案确认, StateType.View | StateType.Add | StateType.Edit));

            m_cm.StateControls.Add(new StateControl(btn网上委托导入, true));

            m_cm.DisplayManager.PositionChanged += new EventHandler(DisplayManager_PositionChanged);
            m_cm.DisplayManager.PositionChanging += new CancelEventHandler(DisplayManager_PositionChanging);

            Helper.SetGridDefault(this, m_rightGrid);

            m_cm.DisplayManager.SearchManager.LoadData();
        }

        void DisplayManager_PositionChanged(object sender, EventArgs e)
        {
            任务 entity = m_cmT.DisplayManagerT.CurrentEntity;
            if (entity == null)
                return;

            if (!string.IsNullOrEmpty(entity.任务号) || entity.是否拒绝)
            {
                btn修改.Enabled = false;
                btn拒绝.Enabled = false;
                btn备案确认.Enabled = false;
            }
            else
            {
                btn修改.Enabled = true;
                btn拒绝.Enabled = true;
                btn备案确认.Enabled = true;
            }
        }

        void DisplayManager_PositionChanging(object sender, CancelEventArgs e)
        {
            e.Cancel = !m_cm.TryCancelEdit();
        }


        public void RefreshData()
        {
            m_rightGrid.ReloadData();
        }

        internal static bool Check任务(任务 entity)
        {
            if (entity.是否拒绝)
            {
                MessageForm.ShowWarning("此任务已经拒绝！");
                return false;
            }
            return true;
        }

        private void btn修改_Click(object sender, EventArgs e)
        {
            if (m_cm.DisplayManager.CurrentItem != null)
            {
                任务 entity = m_cmT.DisplayManagerT.CurrentEntity;
                if (!Check任务(entity))
                    return;

                ArchiveOperationForm.DoEditS(m_cm, m_gridName);
                UpdateContent(m_cm, m_gridName);

                m_cm.DisplayManager.OnSelectedDataValueChanged(new SelectedDataValueChangedEventArgs("任务性质", m_cm.DisplayManager.DataControls["任务性质"]));
            }
        }

        private void btn拒绝_Click(object sender, EventArgs e)
        {
            任务 entity = m_cmT.DisplayManagerT.CurrentEntity;
            if (entity != null)
            {
                if (!Check任务(entity))
                    return;

                拒绝原因 jjyy = new 拒绝原因(entity);
                if (jjyy.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    任务预备案.RemoveAllValidation(m_cm);
                    bool ret = Save(SaveType.拒绝确认);
                    if (ret)
                    {
                        m_rightGrid.CurrentDataRow.Cells["预录入号"].ForeColor = Color.Red;
                        m_rightGrid.MoveCurrentRow(Xceed.Grid.VerticalDirection.Down, Xceed.Grid.GridSection.Current);
                    }
                }
            }
        }

        private void btn放弃_Click(object sender, EventArgs e)
        {
            m_cm.TryCancelEdit();
        }

        private void btn备案确认_Click(object sender, EventArgs e)
        {
            任务 entity = m_cmT.DisplayManagerT.CurrentEntity;
            if (entity != null)
            {
                if (!Check任务(entity))
                    return;

                //为了测试方便，删除验证
                //RemoveAllValidation();
                ArchiveDetailForm.AddValidations(m_cm, m_gridName);

                bool ret = Save(SaveType.正式备案确认);
                if (ret)
                {
                    m_rightGrid.CurrentDataRow.Cells["预录入号"].ForeColor = Color.Green;
                    m_rightGrid.MoveCurrentRow(Xceed.Grid.VerticalDirection.Down, Xceed.Grid.GridSection.Current);
                }
            }
        }
        private bool Save(SaveType saveType)
        {
            if (m_cm.State == StateType.Edit)
            {
                this.ValidateChildren();
            }

            bool ret = Save(m_cm, saveType);

            if (m_cm.State == StateType.View)
            {
                ArchiveDetailForm.UpdateStatusDataControl(m_cm, m_gridName);

                m_cm.DisplayManager.OnPositionChanged(System.EventArgs.Empty);
            }
            return ret;
        }

        internal static bool Save(IControlManager cm, SaveType saveType)
        {
            if (cm.State == StateType.Edit)
            {
                if (!cm.SaveCurrent())
                {
                    return false;
                }
            }

            任务 entity = cm.DisplayManager.CurrentItem as 任务;
            switch (saveType)
            {
                case SaveType.正式备案确认:
                    任务Dao.生成任务号(entity);
                    break;
                case SaveType.拒绝确认:
                    entity.是否拒绝 = true;
                    entity.IsActive = false;
                    entity.任务号 = null;
                    break;
                default:
                    break;
            }

            bool ret;
            if (cm.State == StateType.Edit)
            {
                ret = cm.EndEdit(true);
            }
            else
            {
                try
                {
                    cm.Dao.Update(entity);
                    ret = true;
                }
                catch (Exception ex)
                {
                    ExceptionProcess.ProcessWithNotify(ex);
                    ret = false;
                }
            }

            return ret;
        }

        private void btn网上委托导入_Click(object sender, EventArgs e)
        {
            任务Dao dao = m_cm.Dao as 任务Dao;
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
                                rw.卸货联系手机 = wswtrw.卸货联系手机;
                                rw.卸货联系座机 = wswtrw.卸货联系座机;
                                rw.装货联系人 = wswtrw.装货联系人;
                                rw.装货时间要求始 = wswtrw.装货时间要求始;
                                rw.装货时间要求止 = wswtrw.装货时间要求止;
                                dao.Save(rep1, rw);
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

        private void btn批量确认_Click(object sender, EventArgs e)
        {
            ServiceProvider.GetService<IApplication>().ExecuteAction("任务备案_任务正式备案");
        }
    }
}
