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
    public partial class 预录入任务详细信息 : MyTemplateForm, IControlManagerContainer, IGridNamesContainer
    {
        IControlManager IControlManagerContainer.ControlManager
        {
            get { return m_cm; }
        }
        public string[] GridName
        {
            get { return new string[] { m_gridName }; }
        }

        public 预录入任务详细信息(IWindowControlManager<任务> cm)
        {
            InitializeComponent();
            m_cm = cm;
        }

        private IWindowControlManager<任务> m_cm;
        private string m_gridName = "实体类_任务";

        protected override void Form_Load(object sender, EventArgs e)
        {
            base.Form_Load(sender, e);

            btn暂存待确认.Visible = false;

            MyTemplateForm.AddControl(pnl备案明细窗体, new 备案明细窗体(m_cm, m_gridName));
            base.AssociateDataControls(new Control[] { pnl任务号 }, m_cm.DisplayManager, m_gridName);

            m_cm.StateControls.Add(new StateControl(btn预录入发送, false));
            m_cm.StateControls.Add(new StateControl(btn暂存待确认, false));
            m_cm.StateControls.Add(new StateControl(btn删除, StateType.View));
            m_cm.StateControls.Add(new StateControl(btn修改, StateType.View));
            m_cm.DisplayManager.DisplayCurrent();
        }

        protected override void Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (!m_cm.TryCancelEdit())
                e.Cancel = true;

            base.Form_Closing(sender, e);
        }

        private void btn暂存待确认_Click(object sender, EventArgs e)
        {
            if (m_cm.DisplayManager.CurrentItem != null)
            {
                任务预备案.RemoveAllValidation(m_cm);
                Save(SaveType.暂存待确认);
            }
        }

        private void btn删除_Click(object sender, EventArgs e)
        {
            if (m_cm.DisplayManager.CurrentItem != null)
            {
                ArchiveOperationForm.DoDeleteS(m_cm, m_gridName);
            }
        }

        private void btn修改_Click(object sender, EventArgs e)
        {
            if (m_cm.DisplayManager.CurrentItem != null)
            {
                ArchiveOperationForm.DoEditS(m_cm, m_gridName);
                UpdateContent(m_cm, m_gridName);

                m_cm.DisplayManager.OnSelectedDataValueChanged(new SelectedDataValueChangedEventArgs("任务性质", m_cm.DisplayManager.DataControls["任务性质"]));
            }
        }

        private void btn预录入发送_Click(object sender, EventArgs e)
        {
            任务 entity = m_cm.DisplayManagerT.CurrentEntity;
            if (entity != null)
            {
                //为了测试方便，删除验证
                //RemoveAllValidation();
                ArchiveDetailForm.AddValidations(m_cm, m_gridName);

                //if (m_cm.DisplayManager.DataControls["任务性质"].SelectedDataValue != null)
                //{
                //    任务性质 rwxz = (任务性质)m_cm.DisplayManager.DataControls["任务性质"].SelectedDataValue;
                //    if (rwxz != 任务性质.进口拆箱 && rwxz != 任务性质.出口装箱)
                //    {
                //        m_cm.RemoveValidation("箱型");
                //    }
                //}
                Save(SaveType.预录入发送);
            }
        }

        private void Save(SaveType saveType)
        {
            // 验证控件有效值（有些控件需移出焦点后对其他控件产生影响，如果这里不Validate，会先执行Save）
            this.ValidateChildren();

            if (m_cm.SaveCurrent())
            {
                任务 entity = m_cm.DisplayManagerT.CurrentEntity;

                switch (saveType)
                {
                    case SaveType.暂存待确认:
                        任务Dao.生成预录入号(entity);
                        break;
                    case SaveType.预录入发送:
                        entity.IsActive = true;
                        entity.是否拒绝 = false;
                        任务Dao.生成预录入号(entity);
                        break;
                    default:
                        break;
                }
                m_cm.EndEdit(true);

                if (m_cm.State == StateType.View)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    //ArchiveDetailForm.UpdateStatusDataControl(m_cm, m_gridName);
                    //m_cm.DisplayManager.OnPositionChanged(System.EventArgs.Empty);
                }
            }
        }

    }
}
