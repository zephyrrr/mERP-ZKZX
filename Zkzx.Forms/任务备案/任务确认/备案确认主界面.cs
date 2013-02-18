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
    public partial class 备案确认主界面 : MyButtonsMainForm
    {
        public 备案确认主界面()
        {
            InitializeComponent();

            AssociateForm(panel1, btn刷新);
            AssociateButtonToForm(btn任务备案确认主界面, typeof(任务备案确认主界面));
            AssociateButtonToForm(btn列表方式确认, typeof(任务备案确认列表方式));
            AssociateButtonToForm(btn以票计的进口箱批量任务确认, typeof(进口箱批量任务确认));
            AssociateButtonToForm(btn备案确认情况查询, typeof(备案确认情况查询));
        }
    }
}
