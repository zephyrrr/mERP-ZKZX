using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Zkzx.Forms
{
    public partial class 专家级调度主界面 :MyButtonsMainForm
    {
        public 专家级调度主界面()
        {
            InitializeComponent();
            AssociateForm(panel1, btn刷新);
            AssociateButtonToForm(btn专家级主界面, typeof(专家级主界面));
            AssociateButtonToForm(btn一级动态优化, typeof(专家调度一级动态优化));
            AssociateButtonToForm(btn一级静态优化, typeof(专家调度一级静态优化));
            AssociateButtonToForm(btn静态任务分派, typeof(专家调度静态任务下达));
        }
    }
}
