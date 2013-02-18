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
using Feng.Data;
using Feng.Windows.Forms;
using Feng.Utils;
using Hd.NetRead;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 进口箱批量任务录入 : Feng.Windows.Forms.MyTemplateForm, IControlManagerContainer, IGridNamesContainer, IRefreshDataForm
    {
        IControlManager IControlManagerContainer.ControlManager
        {
            get { return m_cm; }
        }
        public string[] GridName
        {
            get { return new string[] { m_gridName }; }
        }

        public 进口箱批量任务录入()
        {
            InitializeComponent();
        }

        private IWindowControlManager<进口票> m_cm;
        private IWindowControlManager<任务> m_cm2;
        private string m_gridName = "任务备案_进口箱批量任务录入_暂存区";

        private Feng.Grid.ArchiveUnboundGrid m_显示区Grid;
        private Feng.Grid.ArchiveUnboundGrid m_暂存区Grid;
        //private 任务Dao m_rwDao = new 任务Dao();

        private void 进口箱批量任务录入_Load(object sender, EventArgs e)
        {
            m_暂存区Grid = base.AssociateArchiveGrid(pnl暂存区, "任务备案_进口箱批量任务录入_暂存区") as ArchiveUnboundGrid;
            m_cm = m_暂存区Grid.ControlManager as IWindowControlManager<进口票>;

            m_显示区Grid = MyTemplateForm.AssociateArchiveDetailGrid(pnl显示区, "任务备案_进口箱批量任务录入_显示区", m_cm, m_cm.Dao as IRelationalDao) as ArchiveUnboundGrid;
            m_cm2 = m_显示区Grid.ControlManager as IWindowControlManager<任务>;

            base.AssociateDataControls(new Control[] { 
                pnl任务性质, pnl转关箱标志, pnl委托人, pnl委托时间, pnl委托联系人, 
                pnl船名, pnl航次,pnl提箱时间要求, pnl还箱进港时间要求, 
                pnl备注, pnl总箱量, pnl提单号, pnl提示性箱号, pnl箱属船公司 }, m_cm.DisplayManager, m_gridName);

            m_cm.StateControls.Add(new StateControl(btn新增任务, true));
            m_cm.StateControls.Add(new StateControl(btn暂存待确认, false));
            m_cm.StateControls.Add(new StateControl(btn删除, StateType.View));
            m_cm.StateControls.Add(new StateControl(btn修改, StateType.View));
            m_cm.StateControls.Add(new StateControl(btn预录入发送, false));
            m_cm.StateControls.Add(new StateControl(btn网上导入, false));

            m_cm.DisplayManager.DataControls["委托人编号"].SelectedDataValueChanged += new EventHandler(任务预备案_委托人编号_SelectedDataValueChanged);

            //MyTemplateForm.RestrictToUserAccess(m_cm.DisplayManager.SearchManager, "备案主管");

            RemoveGridValidations();

            m_cm.DisplayManager.PositionChanging += new Feng.CancelEventHandler(DisplayManager_PositionChanging);
            m_cm.DisplayManager.PositionChanged += new EventHandler(DisplayManager_PositionChanged);

            Helper.SetGridDefault(this, m_暂存区Grid);

            m_cm.DisplayManager.SearchManager.LoadData();
        }

        void DisplayManager_PositionChanging(object sender, Feng.CancelEventArgs e)
        {
            e.Cancel = !m_cm.TryCancelEdit();
        }

        void DisplayManager_PositionChanged(object sender, EventArgs e)
        {
            if (m_cm.DisplayManager.Position == -1)
            {
                MyGrid.CancelEditCurrentDataRow(m_显示区Grid);
                m_显示区Grid.DataRows.Clear();
            }
            else
            {
                m_显示区Grid.ReloadData(false);
            }

            var entity = m_cm.DisplayManagerT.CurrentEntity;
            if (entity == null)
                return;

            if (entity.IsActive)
            {
                btn删除.Enabled = false;
                btn修改.Enabled = false;
            }
            else
            {
                btn删除.Enabled = true;
                btn修改.Enabled = true;
            }
        }

        void 任务预备案_委托人编号_SelectedDataValueChanged(object sender, EventArgs e)
        {
            if (m_cm.State == StateType.View || m_cm.State == StateType.None)
                return;
            if (m_cm.DisplayManager.DataControls["委托人编号"].SelectedDataValue != null)
            {
                string s = m_cm.DisplayManager.DataControls["委托人编号"].SelectedDataValue.ToString();
                object s2 = NameValueMappingCollection.Instance.FindColumn2FromColumn1("人员单位_委托人", "编号", "第一联系人", s);
                if (s2 != null)
                {
                    m_cm.DisplayManager.DataControls["委托联系人"].SelectedDataValue = s2;
                }
            }
            else
            {
                m_cm.DisplayManager.DataControls["委托联系人"].SelectedDataValue = null;
            }
        }

        private void btn网上导入_Click(object sender, EventArgs e)
        {
            string tdh = (string)(pnl提单号.Controls[0] as IWindowDataControl).SelectedDataValue;
            string hc = (string)(pnl船名.Controls[0] as IWindowDataControl).SelectedDataValue;

            if (string.IsNullOrEmpty(tdh))
            {
                MessageForm.ShowWarning("请填写提单号！");
                return;
            }

            if (!string.IsNullOrEmpty(hc) && hc.Contains('/'))
            {
                hc = hc.Substring(hc.LastIndexOf('/') + 1).Trim();
            }

            IList<Hd.NetRead.集装箱数据> boxList = null;
            int piao_successCount = 0;//成功导入的票数量 
            int rw_successCount = 0;//成功导入的任务数量 

            ProgressForm progressForm = new ProgressForm();
            progressForm.Start(this, "网上导入");

            Feng.Async.AsyncHelper asyncHelper = new Feng.Async.AsyncHelper(
                new Feng.Async.AsyncHelper.DoWork(delegate()
                {
                    nbeportRead m_nbeportGrab = new nbeportRead();
                    m_nbeportGrab.SetLoginInfo(Feng.DBDef.Instance.TryGetValue("NetReadUserName"),
                        Feng.DBDef.Instance.TryGetValue("NetReadPassword"));

                    if (string.IsNullOrEmpty(hc))
                    {
                        boxList = m_nbeportGrab.查询集装箱数据(ImportExportType.进口集装箱, tdh);
                    }
                    else
                    {
                        boxList = m_nbeportGrab.查询集装箱数据(ImportExportType.进口集装箱, tdh, hc);
                    }

                    if (boxList != null && boxList.Count > 0)
                    {
                        AskToReplace(m_cm, "任务性质", 任务性质.进口拆箱);
                        AskToReplace(m_cm, "提单号", boxList[0].提单号);
                        AskToReplace(m_cm, "船名航次", boxList[0].船名 + "/" + boxList[0].航次);
                        piao_successCount++;

                        foreach (集装箱数据 jzx in boxList)
                        {
                            bool have = false;
                            foreach (Xceed.Grid.DataRow row in m_显示区Grid.DataRows)
                            {
                                if (row.Cells["箱号"].Value != null && row.Cells["箱号"].Value.ToString().Trim() == jzx.集装箱号.Trim())
                                {
                                    have = true;
                                    break;
                                }
                            }
                            if (!have)
                            {
                                任务 rw = new 任务();
                                rw.任务来源 = 任务来源.网上;
                                rw.任务性质 = 任务性质.进口拆箱;
                                rw.提箱点编号 = NameValueMappingCollection.Instance.FindIdFromName("人员单位_全部", jzx.堆场区).ToString();
                                rw.箱号 = jzx.集装箱号;
                                rw.船名 = jzx.船名;
                                rw.航次 =  jzx.航次;
                                m_cm2.AddNew();
                                m_cm2.DisplayManager.Items[m_cm2.DisplayManager.Position] = rw;
                                m_cm2.EndEdit();
                                rw_successCount++;
                            }
                        }
                    }
                    return null;
                }),
               new Feng.Async.AsyncHelper.WorkDone(delegate(object result)
               {
                   MessageForm.ShowInfo("成功导入 " + piao_successCount + " 票，" + rw_successCount + " 条任务。");
                   progressForm.Stop();
               }));
        }

        private static void AskToReplace(IControlManager cm, string propertyName, object destValue)
        {
            if (propertyName == null || destValue == null)
            {
                return;
            }

            bool replace = true;

            if (//cm.DisplayManager.DataControls[propertyName].SelectedDataValue == null && destValue != null || 
                cm.DisplayManager.DataControls[propertyName].SelectedDataValue != null && destValue == null
                || (cm.DisplayManager.DataControls[propertyName].SelectedDataValue != null && destValue != null &&
                    cm.DisplayManager.DataControls[propertyName].SelectedDataValue.ToString() != destValue.ToString()) && propertyName != "报关单快照")
            {
                if (!MessageForm.ShowYesNo(propertyName + "是否要替换成" + (destValue == null ? "空" : destValue.ToString()), "网上导入"))
                {
                    replace = false;
                }
            }
            if (replace)
            {
                cm.DisplayManager.DataControls[propertyName].SelectedDataValue = destValue;
            }
        }

        public void RefreshData()
        {
            m_暂存区Grid.ReloadData();
        }
        private void btn新增任务_Click(object sender, EventArgs e)
        {
            m_cm.AddNew();
            UpdateContent(m_cm, m_gridName);

            进口票 entity = m_cm.DisplayManagerT.CurrentEntity;
            //entity.任务来源 = 任务来源.手工;
            entity.IsActive = false;

            m_cm.DisplayManagerT.CurrentEntity.任务性质 = 任务性质.进口拆箱;
            m_cm.DisplayManager.DataControls["任务性质"].SelectedDataValue = 任务性质.进口拆箱;
            m_cm.DisplayManager.DataControls["委托时间"].SelectedDataValue = System.DateTime.Now;
        }

        private void btn修改_Click(object sender, EventArgs e)
        {
            if (m_cm.DisplayManager.CurrentItem != null)
            {
                进口票 entity = m_cm.DisplayManagerT.CurrentEntity;
                if (entity.IsActive)
                {
                    MessageForm.ShowWarning("此进口票已经预录入发送！");
                    return;
                }
                ArchiveOperationForm.DoEditS(m_cm, m_gridName);
                UpdateContent(m_cm, m_gridName);
            }
        }

        private void btn删除_Click(object sender, EventArgs e)
        {
            if (m_cm.DisplayManager.CurrentItem != null)
            {
                进口票 entity = m_cm.DisplayManagerT.CurrentEntity;
                if (entity.IsActive)
                {
                    MessageForm.ShowWarning("此进口票已经预录入发送！");
                    return;
                }

                ArchiveOperationForm.DoDeleteS(m_cm, m_gridName);
            }
        }

        private void btn暂存待确认_Click(object sender, EventArgs e)
        {
            if (m_cm.DisplayManager.CurrentItem != null)
            {
                RemoveAllValidation();
                Save(SaveType.暂存待确认);
            }
        }

        private void btn预录入发送_Click(object sender, EventArgs e)
        {
            if (m_cm.DisplayManager.CurrentItem != null)
            {
                //为了测试方便，删除验证
                //RemoveAllValidation();
                ArchiveDetailForm.AddValidations(m_cm, m_gridName);
                m_显示区Grid.ArchiveGridHelper.AddValidations();

                Save(SaveType.预录入发送);

                RemoveGridValidations();
            }
        }

        private void Save(SaveType saveType)
        {
            // 验证控件有效值（有些控件需移出焦点后对其他控件产生影响，如果这里不Validate，会先执行Save）
            this.ValidateChildren();

            进口票 jkp = m_cm.DisplayManager.CurrentItem as 进口票;
            if (jkp == null)
                return;

            if (m_cm.SaveCurrent())
            {
                switch (saveType)
                {
                    case SaveType.暂存待确认:
                        jkp.IsActive = false;

                        for (int i = 0; i < m_显示区Grid.DataRows.Count; ++i)
                        {
                            任务 entity = m_显示区Grid.DataRows[i].Tag as 任务;
                            entity.任务来源 = 任务来源.手工;
                            entity.任务性质 = 任务性质.进口拆箱;
                            任务Dao.生成预录入号(entity, i);

                            m_cm2.Dao.Update(entity);
                        }
                        break;
                    case SaveType.预录入发送:
                        if (!Check总箱量(jkp))
                            return;

                        jkp.IsActive = true;

                        for (int i = 0; i < m_显示区Grid.DataRows.Count; ++i)
                        {
                            任务 entity = m_显示区Grid.DataRows[i].Tag as 任务;

                            if (string.IsNullOrEmpty(entity.预录入号))
                            {
                                entity.任务来源 = 任务来源.手工;
                                entity.任务性质 = 任务性质.进口拆箱;
                                任务Dao.生成预录入号(entity, i);
                            }
                            entity.IsActive = true;
                            entity.是否拒绝 = false;

                            m_cm2.Dao.Update(entity);
                        }

                        break;
                    default:
                        break;
                }
                m_cm.EndEdit(true);

                if (m_cm.State == StateType.View)
                {
                    ArchiveDetailForm.UpdateStatusDataControl(m_cm, m_gridName);
                    m_cm.DisplayManager.OnPositionChanged(System.EventArgs.Empty);
                }
            }
        }

        private bool Check总箱量(进口票 jkp)
        {
            int real = m_显示区Grid.DataRows.Count;
            int input = 0;
            if (int.TryParse(jkp.总箱量, out input))
            {
            }
            else
            {
                if (jkp.总箱量 != null && jkp.总箱量.Contains('/'))
                {
                    string zxl = jkp.总箱量.Trim();
                    input = Convert.ToInt32(zxl.Substring(0, zxl.IndexOf('/')));
                }
            }
            if (input != real)
            {
                MessageForm.ShowWarning("箱量不一致，无法发送");
                return false;
            }
            return true;
        }

        private void RemoveAllValidation()
        {
            m_cm.RemoveValidation("委托人编号");
            m_cm.RemoveValidation("转关箱标志");
            m_cm.RemoveValidation("委托时间");
            m_cm.RemoveValidation("提箱时间要求");
            m_cm.RemoveValidation("还箱进港时间要求");
            m_cm.RemoveValidation("提单号");
            m_cm.RemoveValidation("委托联系人");
            m_cm.RemoveValidation("箱属船公司编号");
            m_cm.RemoveValidation("总箱量");
        }

        private void RemoveGridValidations()
        {
            //m_显示区Grid.ArchiveGridHelper.RemoveValidation("箱号");
            m_显示区Grid.ArchiveGridHelper.RemoveValidation("箱型编号");
            m_显示区Grid.ArchiveGridHelper.RemoveValidation("提箱点编号");
            m_显示区Grid.ArchiveGridHelper.RemoveValidation("卸货地编号");
            m_显示区Grid.ArchiveGridHelper.RemoveValidation("卸货地详细地址");
            m_显示区Grid.ArchiveGridHelper.RemoveValidation("卸货时间要求始");
            m_显示区Grid.ArchiveGridHelper.RemoveValidation("卸货时间要求止");
            m_显示区Grid.ArchiveGridHelper.RemoveValidation("卸货联系人");
            m_显示区Grid.ArchiveGridHelper.RemoveValidation("卸货联系手机");
            m_显示区Grid.ArchiveGridHelper.RemoveValidation("卸货联系座机");
            m_显示区Grid.ArchiveGridHelper.RemoveValidation("还箱进港点编号");
            m_显示区Grid.ArchiveGridHelper.RemoveValidation("货名");
            m_显示区Grid.ArchiveGridHelper.RemoveValidation("货物特征");
            m_显示区Grid.ArchiveGridHelper.RemoveValidation("价值");
            m_显示区Grid.ArchiveGridHelper.RemoveValidation("重量");
        }
    }
}
