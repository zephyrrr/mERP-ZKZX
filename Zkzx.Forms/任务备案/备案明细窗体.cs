using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Feng;
using Feng.Windows.Forms;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 备案明细窗体 : UserControl
    {
        private IControlManager m_cm;
        private string m_gridName;
        public 备案明细窗体(IControlManager cm, string gridName)
        {
            InitializeComponent();

            m_cm = cm;
            m_gridName = gridName;

            MyTemplateForm.AssociateDataControls(new Control[] { 
                pnl预录入号, pnl任务性质, pnl转关箱标志, pnl委托人, pnl委托时间, pnl委托联系人, pnl提单号,
                pnl箱号, pnl箱型, pnl箱属船公司, pnl船名, pnl航次,pnl货名, pnl货物特征, pnl价值, pnl重量, 
                pnl提箱点, pnl提箱时间要求, pnl还箱进港点, pnl还箱进港时间要求, 
                pnl装货地, pnl装货地详细地址, pnl装货时间要求始, pnl装货时间要求止, pnl装货联系人, pnl装货联系手机, pnl装货联系座机,
                pnl卸货地, pnl卸货地详细地址, pnl卸货时间要求始, pnl卸货时间要求止, pnl卸货联系人, pnl卸货联系手机, pnl卸货联系座机,
                pnl备注,pnl卸货地单位,pnl装货地单位}, cm.DisplayManager, gridName, false, this, m_labels);

            m_cm.DisplayManager.SelectedDataValueChanged += new EventHandler<SelectedDataValueChangedEventArgs>(DisplayManager_SelectedDataValueChanged);
            //m_cm.DisplayManager.DataControls["任务性质"].SelectedDataValueChanged += new EventHandler(任务预备案_任务性质_SelectedDataValueChanged);
            m_cm.DisplayManager.DataControls["委托人编号"].SelectedDataValueChanged += new EventHandler(任务预备案_委托人编号_SelectedDataValueChanged);
        }

        private Dictionary<string, Label> m_labels = null;

        // DataControl.SelectedDataValueChanged会触发DisplayManager.SelectedDataValueChanged会出发
        void DisplayManager_SelectedDataValueChanged(object sender, SelectedDataValueChangedEventArgs e)
        {
            if (e.DataControlName == "任务性质")
            {
                任务预备案_任务性质_SelectedDataValueChanged(e.Container, System.EventArgs.Empty);
            }
        }

        void 任务预备案_委托人编号_SelectedDataValueChanged(object sender, EventArgs e)
        {
            if (m_cm.State == StateType.View || m_cm.State == StateType.None)
                return;
            ((m_cm.DisplayManager.DataControls["委托联系人"] as IWindowControl).Control as MyComboBox).SelectFirstVisibleItem();

        }

        void 任务预备案_任务性质_SelectedDataValueChanged(object sender, EventArgs e)
        {
            if (m_cm.State == StateType.View || m_cm.State == StateType.None)
                return;

            string[] jkReadOnlyControls = new string[] { "装货地编号", "装货地详细地址", "装货地单位编号", "装货时间要求", "装货时间要求始", "装货时间要求止", "装货联系人", "装货联系电话", "装货联系手机", "装货联系座机" };
            string[] ckReadOnlyControls = new string[] { "提箱时间要求", "卸货地编号", "卸货地详细地址", "卸货地单位编号", "卸货时间要求", "卸货时间要求始", "卸货时间要求止", "卸货联系人", "卸货联系电话", "卸货联系手机", "卸货联系座机" };
            string[] dhReadOnlyControls = new string[] { "转关箱标志", "箱号", "箱属船公司编号", "船名", "航次", "提箱点编号", "提箱时间要求", "还箱进港点编号", "还箱进港时间要求" };

            if (m_cm.DisplayManager.DataControls["任务性质"].SelectedDataValue != null)
            {
                任务性质 rwxz = (任务性质)m_cm.DisplayManager.DataControls["任务性质"].SelectedDataValue;
                if (rwxz == 任务性质.进口拆箱)
                {
                    lbl还箱进港点编号.Text = "还箱点";
                    lbl还箱进港时间要求.Text = "还箱时间要求";

                    MyTemplateForm.ReadOnlyControls(m_cm.DisplayManager, dhReadOnlyControls, false, this, m_labels);
                    MyTemplateForm.ReadOnlyControls(m_cm.DisplayManager, ckReadOnlyControls, false, this, m_labels);
                    MyTemplateForm.ReadOnlyControls(m_cm.DisplayManager, jkReadOnlyControls, true, this, m_labels);
                }
                else if (rwxz == 任务性质.出口装箱)
                {
                    lbl还箱进港点编号.Text = "进港点";
                    lbl还箱进港时间要求.Text = "进港时间要求";

                    MyTemplateForm.ReadOnlyControls(m_cm.DisplayManager, dhReadOnlyControls, false, this, m_labels);
                    MyTemplateForm.ReadOnlyControls(m_cm.DisplayManager, jkReadOnlyControls, false, this, m_labels);
                    MyTemplateForm.ReadOnlyControls(m_cm.DisplayManager, ckReadOnlyControls, true, this, m_labels);
                }
                else if (rwxz == 任务性质.I带货 || rwxz == 任务性质.E带货)
                {
                    MyTemplateForm.ReadOnlyControls(m_cm.DisplayManager, jkReadOnlyControls, false, this, m_labels);
                    MyTemplateForm.ReadOnlyControls(m_cm.DisplayManager, ckReadOnlyControls, false, this, m_labels);
                    MyTemplateForm.ReadOnlyControls(m_cm.DisplayManager, dhReadOnlyControls, true, this, m_labels);
                }
            }
        }

    }
}
