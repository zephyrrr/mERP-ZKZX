using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Feng;
using Feng.Grid;
using Feng.Windows.Forms;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 电子作业单 : MyTemplateForm
    {
        public 电子作业单(车辆作业 clzy)
        {
            System.Diagnostics.Debug.Assert(clzy != null, "电子作业单车辆作业不能为空");

            InitializeComponent();
            m_clzy = clzy;

            m_cm = AssociateDataControlsInControlManager(new Control[] { 
                pnl作业号, pnl作业路线, pnl车牌号, pnl驾驶员, pnl任务性质, pnl开始时间, pnl疏港期限, pnl进港还箱期限, pnl备注,pnl车载Id号}, 
                "实时监控_车辆作业_电子作业单");

            m_作业流程 = base.AssociateBoundGrid(pnl作业流程, "实时监控_车辆作业_电子作业单_作业流程");
        }

        private 车辆作业 m_clzy;
        private IBoundGrid m_作业流程;
        private IControlManager m_cm ;
        private 作业监控Dao m_dao = new 作业监控Dao();
        private void 电子作业单_Load(object sender, EventArgs e)
        {
            int[] taskIdx = null;
            string[] importantAreas = null;
            string[] importantTaskStatus = null;
            string[] importantWorkStatus = null;
            using(IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
            {
                var clzy = rep.Get<车辆作业>(m_clzy.ID);

                m_cm.DisplayManager.SetDataBinding(new List<车辆作业> { clzy }, string.Empty);
                m_cm.EditCurrent();

                ModelHelper.Get任务状态(clzy.专家任务, out taskIdx, out importantAreas, out importantTaskStatus, out importantWorkStatus);

                StringBuilder strZylx = new StringBuilder();
                for (int i = 0; i < taskIdx.Length; ++i)
                {
                    Xceed.Grid.DataRow row = m_作业流程.DataRows.AddNew();
                    row.Cells["次序"].Value = i;
                    row.Cells["作业地点"].Value = importantAreas[i];
                    row.Cells["动作"].Value = importantWorkStatus[i];

                    var rw = clzy.专家任务.任务[taskIdx[i]];
                    row.Cells["箱号"].Value = rw.箱号;
                    row.Cells["箱型"].Value = rw.箱型编号;
                    if (importantWorkStatus[i].StartsWith("港区提箱"))
                    {
                        m_cm.DisplayManager.DataControls["疏港期限"].SelectedDataValue = rw.提箱时间要求.HasValue ? rw.提箱时间要求.Value : rw.提箱时间要求;
                    }
                    else if (importantWorkStatus[i].StartsWith("还箱") || importantWorkStatus[i].StartsWith("进港"))
                    {
                        m_cm.DisplayManager.DataControls["进港还箱期限"].SelectedDataValue = rw.还箱进港时间要求.HasValue ? rw.还箱进港时间要求.Value : rw.还箱进港时间要求;
                    }
                    else if (importantWorkStatus[i].StartsWith("卸货"))
                    {
                        row.Cells["详细地址"].Value = rw.卸货地详细地址;
                        row.Cells["联系人"].Value = rw.卸货联系人;
                        row.Cells["联系电话"].Value = rw.卸货联系手机 + "," + rw.卸货联系座机;
                    }
                    else if (importantWorkStatus[i].StartsWith("装货"))
                    {
                        row.Cells["详细地址"].Value = rw.装货地详细地址;
                        row.Cells["联系人"].Value = rw.装货联系人;
                        row.Cells["联系电话"].Value = rw.装货联系手机 + "," + rw.装货联系座机;
                    }

                    row.EndEdit();

                    if (i != 0)
                        strZylx.Append("、");
                    strZylx.Append(row.Cells["作业地点"].GetDisplayText() + "(" + row.Cells["动作"].GetDisplayText() + ")");
                }

                m_cm.DisplayManager.DataControls["作业路线"].SelectedDataValue = strZylx.ToString();

                if (m_cm.DisplayManager.DataControls["开始时间"].SelectedDataValue == null)
                {
                    m_cm.DisplayManager.DataControls["开始时间"].SelectedDataValue = System.DateTime.Now;
                }

                if (m_cm.DisplayManager.DataControls["车载Id号"].SelectedDataValue == null)
                {
                    m_cm.DisplayManager.DataControls["车载Id号"].SelectedDataValue = clzy.车辆.车载Id号;
                }

                if (m_cm.DisplayManager.DataControls["驾驶员编号"].SelectedDataValue == null)
                {
                    m_cm.DisplayManager.DataControls["驾驶员编号"].SelectedDataValue = clzy.车辆.主驾驶员编号;
                }
            }
        }

        private void btn确定_Click(object sender, EventArgs e)
        {
            if (m_cm.SaveCurrent())
            {
                string jsybh = (string)m_cm.DisplayManager.DataControls["驾驶员编号"].SelectedDataValue;
                string czidh = (string)m_cm.DisplayManager.DataControls["车载Id号"].SelectedDataValue;
                DateTime kssj = (DateTime)m_cm.DisplayManager.DataControls["开始时间"].SelectedDataValue;

                if (string.IsNullOrEmpty(jsybh))
                {
                    MessageForm.ShowWarning("请先指定驾驶员！");
                    return;
                }

                m_dao.开始监控(m_clzy, kssj, jsybh, czidh);

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

    }
}
