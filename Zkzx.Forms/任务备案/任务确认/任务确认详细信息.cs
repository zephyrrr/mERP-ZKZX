using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Feng;
using Feng.Windows.Forms;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 任务确认详细信息 : MyTemplateForm, IControlManagerContainer, IGridNamesContainer
    {
        IControlManager IControlManagerContainer.ControlManager
        {
            get { return m_cm; }
        }
        public string[] GridName
        {
            get { return new string[] { m_gridName }; }
        }

        public 任务确认详细信息(IWindowControlManager cm)
        {
            InitializeComponent();
            m_cm = cm;
            m_cmT = m_cm as IWindowControlManager<任务>; 
        }

        private IWindowControlManager m_cm;
        private IWindowControlManager<任务> m_cmT;
        private string m_gridName = "任务备案_任务正式备案二";

        protected override void Form_Load(object sender, EventArgs e)
        {
            base.Form_Load(sender, e);

            MyTemplateForm.AddControl(pnl备案明细窗体, new 备案明细窗体(m_cm, m_gridName));
            base.AssociateDataControls(new Control[] { pnl任务号 }, m_cm.DisplayManager, m_gridName);

            m_cm.StateControls.Add(new StateControl(btn修改, StateType.View));
            m_cm.StateControls.Add(new StateControl(btn拒绝, StateType.View));
            m_cm.StateControls.Add(new StateControl(btn放弃, false));
            m_cm.StateControls.Add(new StateControl(btn备案确认, StateType.View | StateType.Add | StateType.Edit));

            m_cm.DisplayManager.DisplayCurrent();
        }

        protected override void Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (!m_cm.TryCancelEdit())
                e.Cancel = true;

            base.Form_Closing(sender, e);
        }

        private void btn修改_Click(object sender, EventArgs e)
        {
            if (m_cm.DisplayManager.CurrentItem != null)
            {
                任务 entity = m_cmT.DisplayManagerT.CurrentEntity;
                if (!任务备案确认主界面.Check任务(entity))
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
                if (!任务备案确认主界面.Check任务(entity))
                    return;

                拒绝原因 jjyy = new 拒绝原因(entity);
                if (jjyy.ShowDialog() == DialogResult.OK)
                {
                    任务预备案.RemoveAllValidation(m_cm);
                    Save(SaveType.拒绝确认);
                }
            }
        }

        private void btn放弃_Click(object sender, EventArgs e)
        {
            if (m_cm.TryCancelEdit())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        private void btn备案确认_Click(object sender, EventArgs e)
        {
            任务 entity = m_cmT.DisplayManagerT.CurrentEntity;
            if (entity != null)
            {
                if (!任务备案确认主界面.Check任务(entity))
                    return;

                //为了测试方便，删除验证
                //RemoveAllValidation();
                ArchiveDetailForm.AddValidations(m_cm, m_gridName);

                Save(SaveType.正式备案确认);
            }
        }

        private void Save(SaveType saveType)
        {
            // 验证控件有效值（有些控件需移出焦点后对其他控件产生影响，如果这里不Validate，会先执行Save）
            this.ValidateChildren();

            if (m_cm.SaveCurrent())
            {
                任务 entity = m_cm.DisplayManager.CurrentItem as 任务;

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
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    //ArchiveDetailForm.UpdateStatusDataControl(m_cm, m_gridName);
                }
            }
        }
    }
}
