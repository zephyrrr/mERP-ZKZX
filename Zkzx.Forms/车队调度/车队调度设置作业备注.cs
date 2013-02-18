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
    public partial class 车队调度设置作业备注 : Form
    {
        public 车队调度设置作业备注(string bz)
        {
            InitializeComponent();
            txt备注.Text = bz;
        }

        public string 备注
        {
            get { return txt备注.Text; }
        }
    }
}
