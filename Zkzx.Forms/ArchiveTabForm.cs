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
    public partial class ArchiveTabForm : Feng.Windows.Forms.MyTemplateForm, Feng.Windows.Forms.IArchiveDetailForm
    {
        public ArchiveTabForm()
        {
            InitializeComponent();
        }

        public void UpdateContent()
        {
        }
        private void ArchiveTabForm_Load(object sender, EventArgs e)
        {
            m_ParentArchiveForm.DisplayManager.SearchManager.LoadData();

            MyTemplateForm.AssociateSeeForm(panel1, "实时监控_车辆作业_作业监控区");
            MyTemplateForm.AssociateSeeForm(panel2, "任务备案_任务正式备案");
        }

        #region IArchiveDetailForm 成员

        private Feng.Windows.Forms.ArchiveSeeForm m_ParentArchiveForm = null;
        public Feng.Windows.Forms.ArchiveSeeForm ParentArchiveForm
        {
            get { return m_ParentArchiveForm; }
            set { m_ParentArchiveForm = value; }
        }

        #endregion
    }
}
