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
    public partial class 任务预录入主界面 : MyButtonsMainForm
    {
        public 任务预录入主界面()
        {
            InitializeComponent();

            AssociateForm(panel1, btn刷新);
            AssociateButtonToForm(btn任务预备案, typeof(任务预备案));
            AssociateButtonToForm(btn进口箱批量任务录入, typeof(进口箱批量任务录入));
            AssociateButtonToForm(btn预录入发送后情况查询, typeof(预录入发送后情况查询));
        }
    }
}




