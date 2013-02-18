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
using Feng.Utils;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 进口箱批量任务确认 : Feng.Windows.Forms.MyTemplateForm, IControlManagerContainer, IGridNamesContainer, IRefreshDataForm
    {
        IControlManager IControlManagerContainer.ControlManager
        {
            get { return m_cm; }
        }
        public string[] GridName
        {
            get { return new string[] { m_gridName }; }
        }

        public 进口箱批量任务确认()
        {
            InitializeComponent();
        }

        public 进口箱批量任务确认(string 提单号)
        {
            InitializeComponent();
            m_提单号 = 提单号;
        }

        private string m_提单号;
        private IWindowControlManager<进口票> m_cm;
        private IWindowControlManager<任务> m_cm2;
        private string m_gridName = "任务备案_进口箱批量任务录入_暂存区";

        private Feng.Grid.ArchiveUnboundGrid m_显示区Grid;
        private Feng.Grid.ArchiveUnboundGrid m_暂存区Grid;
        //private 任务Dao m_rwDao = new 任务Dao();
        
        private void 进口箱批量任务录入_Load(object sender, EventArgs e)
        {
            m_暂存区Grid = base.AssociateArchiveGrid(pnl暂存区, "任务备案_进口箱批量任务确认_暂存区") as ArchiveUnboundGrid;
            m_cm = m_暂存区Grid.ControlManager as IWindowControlManager<进口票>;

            m_显示区Grid = MyTemplateForm.AssociateArchiveDetailGrid(pnl显示区, "任务备案_进口箱批量任务确认_显示区", m_cm, m_cm.Dao as IRelationalDao) as ArchiveUnboundGrid;
            m_cm2 = m_显示区Grid.ControlManager as IWindowControlManager<任务>;

            base.AssociateDataControls(new Control[] { 
                pnl任务性质, pnl转关箱标志, pnl委托人, pnl委托时间, pnl委托联系人, pnl提单号,
                pnl船名,pnl航次, pnl提箱时间要求, pnl还箱进港时间要求,
                pnl备注, pnl总箱量, pnl提单号, pnl箱属船公司 }, m_cm.DisplayManager, m_gridName);

            m_cm.StateControls.Add(new StateControl(btn修改, StateType.View));
            m_cm.StateControls.Add(new StateControl(btn拒绝, StateType.View));
            m_cm.StateControls.Add(new StateControl(btn放弃, false));
            m_cm.StateControls.Add(new StateControl(btn备案确认, StateType.View | StateType.Add | StateType.Edit));

            m_cm.DisplayManager.DataControls["委托人编号"].SelectedDataValueChanged += new EventHandler(任务预备案_委托人编号_SelectedDataValueChanged);

            m_cm.DisplayManager.PositionChanging += new Feng.CancelEventHandler(DisplayManager_PositionChanging);
            m_cm.DisplayManager.PositionChanged += new EventHandler(DisplayManager_PositionChanged);

            Helper.SetGridDefault(this, m_暂存区Grid);

            if (m_提单号 == null)
            {
                m_cm.DisplayManager.SearchManager.LoadData();
            }
            else
            {
                btn修改.Enabled = false;
                m_cm.DisplayManager.SearchManager.AdditionalSearchExpression = null;
                m_cm.DisplayManager.SearchManager.LoadData(SearchExpression.Eq("提单号", m_提单号), null);
            }
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

            bool ret = true;
            if (entity.任务 != null)
            {
                foreach (var i in entity.任务)
                {
                    if (!string.IsNullOrEmpty(i.任务号) || i.是否拒绝)
                    {
                        ret = false;
                    }
                }
            }
            else
            {
                ret = false;
            }

            btn修改.Enabled = ret;
            btn拒绝.Enabled = ret;
            btn备案确认.Enabled = ret;
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

        public void RefreshData()
        {
            m_暂存区Grid.ReloadData();
        }

        internal static bool Check票(进口票 entity)
        {
            if (entity.任务 != null)
            {
                foreach (var i in entity.任务)
                {
                    if (!string.IsNullOrEmpty(i.任务号))
                    {
                        MessageForm.ShowWarning("此票中已经有任务备案确认过！");
                        return false;
                    }
                    if (i.是否拒绝)
                    {
                        MessageForm.ShowWarning("此票中已经有任务拒绝过！");
                        return false;
                    }
                }
            }
            return true;
        }

        private void btn修改_Click(object sender, EventArgs e)
        {
            if (m_cm.DisplayManager.CurrentItem != null)
            {
                进口票 entity = m_cm.DisplayManagerT.CurrentEntity;
                if (!Check票(entity))
                    return;

                ArchiveOperationForm.DoEditS(m_cm, m_gridName);
                UpdateContent(m_cm, m_gridName);
            }
        }

        private void btn拒绝_Click(object sender, EventArgs e)
        {
            进口票 entity = m_cm.DisplayManagerT.CurrentEntity;
            if (entity != null)
            {
                if (!Check票(entity))
                    return;

                拒绝原因 jjyy = new 拒绝原因(entity);
                if (jjyy.ShowDialog() == DialogResult.OK)
                {
                    bool ret = Save(SaveType.拒绝确认);
                }
            }
        }

        private void btn放弃_Click(object sender, EventArgs e)
        {
            m_cm.TryCancelEdit();
        }
        
        private void btn备案确认_Click(object sender, EventArgs e)
        {
            进口票 entity = m_cm.DisplayManagerT.CurrentEntity;
            if (entity != null)
            {
                if (!Check票(entity))
                    return;

                ArchiveDetailForm.AddValidations(m_cm, m_gridName);
                bool ret = Save(SaveType.正式备案确认);
            }
        }

        private bool Save(SaveType saveType)
        {
            if (m_cm.State == StateType.Edit)
            {
                this.ValidateChildren();

                if (!m_cm.SaveCurrent())
                {
                    return false;
                }
            }

            进口票 entity = m_cm.DisplayManagerT.CurrentEntity;
            switch (saveType)
            {
                case SaveType.正式备案确认:
                    if (entity.任务 != null)
                    {
                        for (int i = 0; i < entity.任务.Count; ++i )
                        {
                            任务Dao.生成任务号(entity.任务[i], i);
                            m_cm2.Dao.Update(entity.任务[i]);
                        }
                    }
                    break;
                case SaveType.拒绝确认:
                    entity.IsActive = false;
                    if (entity.任务 != null)
                    {
                        foreach (任务 i in entity.任务)
                        {
                            i.是否拒绝 = true;
                            i.IsActive = false;
                            i.任务号 = null;
                            m_cm2.Dao.Update(i);
                        }
                    }
                    break;
                default:
                    break;
            }

            bool ret;
            if (m_cm.State == StateType.Edit)
            {
                ret = m_cm.EndEdit(true);
            }
            else
            {
                try
                {
                    m_cm.Dao.Update(entity);
                    ret = true;
                }
                catch (Exception ex)
                {
                    ExceptionProcess.ProcessWithNotify(ex);
                    ret = false;
                }
            }

            if (m_cm.State == StateType.View)
            {
                ArchiveDetailForm.UpdateStatusDataControl(m_cm, m_gridName);

                m_cm.DisplayManager.OnPositionChanged(System.EventArgs.Empty);
            }

            return ret;
        }
    }
}
