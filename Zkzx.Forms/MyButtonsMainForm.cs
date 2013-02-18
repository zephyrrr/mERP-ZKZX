using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using Feng;
using Feng.Windows.Forms;

namespace Zkzx.Forms
{
    public class MyButtonsMainForm :  MyChildForm,  IRefreshDataForm, 
        IGridNamesContainer, IWindowNamesContainer, IDisplayManagerContainer, IControlManagerContainer

    {
        IDisplayManager IDisplayManagerContainer.DisplayManager
        {
            get
            {
                IDisplayManagerContainer v = m_nowForm as IDisplayManagerContainer;
                if (v != null)
                    return v.DisplayManager;
                else
                    return null;
            }
        }
        IControlManager IControlManagerContainer.ControlManager
        {
            get
            {
                IControlManagerContainer v = m_nowForm as IControlManagerContainer;
                if (v != null)
                    return v.ControlManager;
                else
                    return null;
            }
        }

        public string[] GridNames 
        {
            get
            {
                Feng.Windows.Forms.IGridNamesContainer v = m_nowForm as Feng.Windows.Forms.IGridNamesContainer;
                if (v != null)
                    return v.GridNames;
                else
                    return null;
            }
        }
        public string[] WindowNames
        {
            get
            {
                Feng.Windows.Forms.IWindowNamesContainer v = m_nowForm as Feng.Windows.Forms.IWindowNamesContainer;
                if (v != null)
                    return v.WindowNames;
                else
                    return null;
            }
        }

        private Dictionary<Button, MyForm> m_forms = new Dictionary<Button, MyForm>();
        private Dictionary<Button, Type> m_formTypes = new Dictionary<Button, Type>();

        protected override void Form_Load(object sender, EventArgs e)
        {
            base.Form_Load(sender, e);

            if (m_firstButton != null)
            {
                m_firstButton.PerformClick();
            }
        }
        protected override void Form_Closing(object sender, FormClosingEventArgs e)
        {
            foreach (var kvp in m_forms)
            {
                kvp.Value.Close();
            }
            base.Form_Closing(sender, e);
        }

        private Panel m_formPanel;
        private Button m_firstButton;
        private MyForm m_nowForm;

        protected void AssociateButtonMethod(Button btn, string methodName)
        {
            btn.Click += new EventHandler((sender, e) =>
                {
                    if (m_nowForm == null)
                        return;
                    Feng.Utils.ReflectionHelper.RunInstanceMethod(m_nowForm.GetType().AssemblyQualifiedName, methodName, m_nowForm, new object[] { sender, e });
                });
        }
        protected void AssociateForm(Panel panel, Button btnRefresh = null)
        {
            m_formPanel = panel;

            if (btnRefresh != null)
            {
                btnRefresh.Click += new EventHandler((sender, e) =>
                {
                    RefreshData();
                });
            }
        }

        public void RefreshData()
        {
            Feng.Windows.Forms.IRefreshDataForm f = m_nowForm as Feng.Windows.Forms.IRefreshDataForm;
            if (f != null)
            {
                f.RefreshData();
            }
            else
            {
                Feng.Utils.ReflectionHelper.RunInstanceMethod(m_nowForm.GetType().AssemblyQualifiedName, "RefreshData", m_nowForm, new object[] { });
            }
        }

        protected void AssociateButtonToForm(Button btn, Type formType)
        {
            if (m_formPanel == null)
            {
                throw new ArgumentException("please AssociateForm first", "btn"); 
            }
            if (m_formTypes.Count == 0)
            {
                m_firstButton = btn;
            }
            m_formTypes[btn] = formType;
            btn.Click += new EventHandler((sender, e) =>
                {
                    if (m_nowForm != null)
                    {
                        m_nowForm.SaveLayout();
                    }
                    if (!m_forms.ContainsKey(btn))
                    {
                        var form = Feng.Utils.ReflectionHelper.CreateInstanceFromType(m_formTypes[btn]) as MyForm;
                        form.FormBorderStyle = FormBorderStyle.None;
                        form.TopLevel = false;
                        form.Dock = DockStyle.Fill;
                        m_forms[btn] = form;
                    }
                    m_nowForm = m_forms[btn];
                    m_formPanel.Controls.Clear();
                    m_formPanel.Controls.Add(m_nowForm);
                    m_nowForm.Show();
                    this.Text = m_nowForm.Text;
                    foreach (var key in m_formTypes.Keys)
                    {
                        if (key == (Button)sender)
                            key.ForeColor = System.Drawing.Color.Red;
                        else
                            key.ForeColor = System.Drawing.Color.Black;
                    }
                    RefreshData();
                });
        }
    }
}
