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
    public partial class 车队级调度主界面 : MyButtonsMainForm
    {
        public 车队级调度主界面()
        {
            InitializeComponent();
            AssociateForm(panel1, btn刷新);
            AssociateButtonToForm(btn车队级主界面, typeof(车队调度主界面));
            AssociateButtonToForm(btn二级静态优化派车, typeof(车队调度静态优化派车));
            AssociateButtonToForm(btn二级动态优化派车, typeof(车队调度动态优化派车));
            AssociateButtonToForm(btn车辆报停启用登记, typeof(车辆报停启用登记));
            AssociateButtonToForm(btn进口转关箱预排车, typeof(转关箱预排车));
        }
    }
}
