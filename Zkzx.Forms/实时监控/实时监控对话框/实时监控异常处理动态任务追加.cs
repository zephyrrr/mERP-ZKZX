using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Zkzx.Model;
using Feng;
using Feng.Windows.Forms;

namespace Zkzx.Forms
{
    public partial class 实时监控异常处理动态任务追加 : MyTemplateForm
    {
        public 实时监控异常处理动态任务追加(车辆作业 clzy)
        {
            InitializeComponent();

            m_clzy = clzy;
        }

        private 车辆作业 m_clzy;
        private IDisplayManager m_dm;
        private 作业异常Dao m_dao = new 作业异常Dao();
        private int m_处理 = -1;
        private 专家任务Dao m_dao2 = new 专家任务Dao();
        private void 实时监控异常处理_Load(object sender, EventArgs e)
        {
            m_dm = AssociateDataControlsInDisplayManager(new Control[] { pnl异常情况, pnl追加后任务性质, pnl作业号, pnl承运车辆,
                pnl任务号1, pnl任务性质1, pnl箱型1,pnl监管箱1, pnl货物特征1, pnl时间要求1,  pnl起始地途经地终止地1, pnl当前位置1, pnl备注1, 
                pnl任务号2, pnl任务性质2, pnl箱型2, pnl货物特征2, pnl时间要求2,  pnl起始地途经地终止地2, pnl备注2,
                pnl通知驾驶员时间, pnl理由 }, "实时监控_车辆作业_动态任务追加");

            任务 rw2 = null, rw1 = null;
            using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
            {
                var clzy = rep.Get<车辆作业>(m_clzy.ID);

                var list = rep.List<任务>("from 任务 where 任务号 = :任务号", new Dictionary<string, object> { { "任务号", clzy.最新作业状态.异常参数 } });
                if (list.Count > 0)
                {
                    rw2 = list[0];
                }

                foreach (var i in clzy.专家任务.任务)
                {
                    if (i != rw2)
                    {
                        rw1 = i;
                        break;
                    }
                }

                m_dm.SetDataBinding(new List<车辆作业> { clzy }, string.Empty);

                m_dm.DataControls["作业号"].SelectedDataValue = clzy.作业号;
                m_dm.DataControls["承运车辆"].SelectedDataValue = clzy.车辆.车牌号;
                m_dm.DataControls["异常情况"].SelectedDataValue = clzy.最新作业状态.异常情况; // "动态任务追加";
                m_dm.DataControls["追加后任务性质"].SelectedDataValue = clzy.专家任务.任务性质;

                if (rw1 != null)
                {
                    m_dm.DataControls["任务号1"].SelectedDataValue = rw1.任务号;
                    m_dm.DataControls["任务性质1"].SelectedDataValue = rw1.任务性质;
                    m_dm.DataControls["箱型1"].SelectedDataValue = rw1.箱型编号;
                    m_dm.DataControls["货物特征1"].SelectedDataValue = rw1.货物特征;
                    m_dm.DataControls["时间要求1"].SelectedDataValue = Helper.DateTime2String(rw1.时间要求始, rw1.时间要求止);
                    m_dm.DataControls["起始地途经地终止地1"].SelectedDataValue = Feng.Utils.NameValueControlHelper.GetMultiString("人员单位_装卸货地_全部", rw1.起始地编号 + "," + rw1.途径地编号 + "," + rw1.终止地编号);
                    m_dm.DataControls["备注1"].SelectedDataValue = rw1.备注;

                    m_dm.DataControls["监管箱1"].SelectedDataValue = rw1.备注;
                    m_dm.DataControls["当前位置1"].SelectedDataValue = clzy.最新作业状态.车辆位置;
                }
                if (rw2 != null)
                {
                    m_dm.DataControls["任务号2"].SelectedDataValue = rw2.任务号;
                    m_dm.DataControls["任务性质2"].SelectedDataValue = rw2.任务性质;
                    m_dm.DataControls["箱型2"].SelectedDataValue = rw2.箱型编号;
                    m_dm.DataControls["货物特征2"].SelectedDataValue = rw2.货物特征;
                    m_dm.DataControls["时间要求2"].SelectedDataValue = Helper.DateTime2String(rw2.时间要求始, rw2.时间要求止);
                    m_dm.DataControls["起始地途经地终止地2"].SelectedDataValue = Feng.Utils.NameValueControlHelper.GetMultiString("人员单位_装卸货地_全部", rw2.起始地编号 + "," + rw2.途径地编号 + "," + rw2.终止地编号);
                    m_dm.DataControls["备注2"].SelectedDataValue = rw2.备注;
                }

                m_dm.DataControls["通知驾驶员时间"].SelectedDataValue = DateTime.Now;
                m_dm.DataControls["通知驾驶员时间"].ReadOnly = false;
                m_dm.DataControls["理由"].ReadOnly = false;
            }

            m_dao.处理作业异常(m_clzy, null, null, true);
        }

        private void btn确定_Click(object sender, EventArgs e)
        {
            m_处理 = 1;
            DateTime? 通知时间 = (DateTime?)m_dm.DataControls["通知驾驶员时间"].SelectedDataValue;
            m_dao.处理作业异常(m_clzy, "已通知", 通知时间, true);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btn退回_Click(object sender, EventArgs e)
        {
            车辆作业 clzy = m_clzy;
            using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
            {
                rep.Attach(clzy);

                foreach (var i in clzy.专家任务.任务)
                {
                    if (i.任务号 == clzy.最新作业状态.异常参数)
                    {
                        m_dao2.撤销专家任务(clzy.专家任务, i);
                        break;
                    }
                }
            }

            m_处理 = 1;
            DateTime? 通知时间 = System.DateTime.Now;
            string msg = "动态任务追加失败";
            if (m_dm.DataControls["理由"].SelectedDataValue != null)
            {
                msg += ":" + m_dm.DataControls["理由"].SelectedDataValue.ToString();
            }
            m_dao.处理作业异常(m_clzy, msg, 通知时间, true);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void 实时监控异常处理动态任务追加_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_处理 < 0)
            {
                m_dao.处理作业异常(m_clzy, null, null, false);
            }
        }     
    }
}
