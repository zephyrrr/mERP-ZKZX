using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Feng;
using Feng.Grid;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 监控级调度主界面 :MyButtonsMainForm
    {
        public 监控级调度主界面()
        {
            InitializeComponent();
            AssociateForm(panel1, btn刷新);
            AssociateButtonToForm(btn实时监控主界面, typeof(实时监控主界面));
            AssociateButtonToForm(btn作业开始结束, typeof(作业开始结束));
            AssociateButtonToForm(btn作业过程监控, typeof(作业过程监控));
            AssociateButtonToForm(btn任务承运状况, typeof(任务承运状况));
            AssociateButtonToForm(btn虚拟手机终端, typeof(虚拟手机终端));
        }

        private static Feng.Map.MapForm m_mapForm = null;
        public static void OnCellDoubleClick(object sender, EventArgs e)
        {
            Xceed.Grid.Cell srcCell = sender as Xceed.Grid.Cell;
            if (srcCell == null)
                return;

            System.Windows.Forms.DialogResult ret = System.Windows.Forms.DialogResult.None;

            var row = srcCell.ParentRow as Xceed.Grid.DataRow;
            (srcCell.GridControl as IBoundGrid).DisplayManager.SearchManager.ReloadItem(row.Index);
            (srcCell.GridControl as IBoundGrid).ResetRowData(row);

            车辆作业 entity = srcCell.ParentRow.Tag as 车辆作业;
            if (entity == null)
                return;

            if (srcCell.ParentColumn.FieldName == "作业号")
            {
                using (var form = new 单个作业监控详情(entity))
                {
                    form.ShowDialog();
                }
            }
            else if (srcCell.ParentColumn.FieldName == "箱型编号")
            {
                using (var form = new 电子作业单(entity))
                {
                    form.ShowDialog();
                }
            }
            else if (srcCell.ParentColumn.FieldName == "车辆位置")
            {
                if (m_mapForm == null)
                {
                    m_mapForm = new Feng.Map.MapForm();
                    m_mapForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler((sender2, e2) =>
                    {
                        if (e2.CloseReason == System.Windows.Forms.CloseReason.UserClosing)
                        {
                            e2.Cancel = true;
                            m_mapForm.Visible = false;
                        }
                    });
                }
                m_mapForm.ClearMap();
                if (!m_mapForm.Visible)
                {
                    m_mapForm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    m_mapForm.Show();
                }
                if (entity.Track.HasValue)
                {
                    m_mapForm.LoadTrack(entity.Track.Value);
                }

                //if (entity.Track.HasValue)
                //{
                //    using (WebForm form = new WebForm("车辆轨迹", "http://17haha8.oicp.net/CarTrackService/TrackAnimation.aspx?TrackId=" + entity.Track.Value.ToString()))
                //    {
                //        form.ShowDialog();
                //    }
                //}
            }
            else if (srcCell.ParentColumn.FieldName == "车牌号")
            {
                using (var form = new 车辆详细信息(entity.车辆))
                {
                    form.ShowDialog();
                }
                //using (var form = Feng.ServiceProvider.GetService<IWindowFactory>().CreateWindow(ADInfoBll.Instance.GetWindowInfo("实时监控_车辆作业_车辆详细信息")) as System.Windows.Forms.Form)
                //{
                //    form.ShowDialog();
                //}
            }
            else if (srcCell.ParentColumn.FieldName == "承运时间要求")
            {
                using (var form = new 承运时间要求详情(entity))
                {
                    form.ShowDialog();
                }
            }
            else if (srcCell.ParentColumn.FieldName == "开始")
            {
                if (entity.开始时间.HasValue)
                    return;

                using (var form = new 电子作业单(entity))
                {
                    ret = form.ShowDialog();
                }
            }
            else if (srcCell.ParentColumn.FieldName == "结束")
            {
                if (entity.结束时间.HasValue || !entity.开始时间.HasValue)
                    return;

                //if (entity.最新作业状态 != null && entity.最新作业状态.处理状态 == "待处理")
                //{
                //    MessageForm.ShowWarning("请先处理异常情况！");
                //    return;
                //}

                if ((string)srcCell.ParentRow.Cells["作业状态"].Value != "已完成")
                {
                    MessageForm.ShowWarning("还有未完成任务！");
                    return;
                }
                using (var form = new 实时监控结束(entity))
                {
                    ret = form.ShowDialog();
                }
            }
            else if (srcCell.ParentColumn.FieldName == "退回")
            {
                if (entity.开始时间.HasValue)
                    return;

                using (var form = new 实时监控作业退回申请(entity))
                {
                    form.ShowDialog();
                }
            }
            else if (srcCell.ParentColumn.FieldName == "处理状态")
            {
                if (entity.最新作业状态 != null && entity.最新作业状态.处理状态 == "待处理")
                {
                    if (entity.最新作业状态.异常情况 == "动态任务追加")
                    {
                        using (var form = new 实时监控异常处理动态任务追加(entity))
                        {
                            ret = form.ShowDialog();
                        }
                    }
                    else
                    {
                        using (var form = new 实时监控异常处理(entity))
                        {
                            ret = form.ShowDialog();
                        }
                    }
                }
            }
            else if (srcCell.ParentColumn.FieldName == "异常报警")
            {
                using (var form = new 实时监控异常报警(entity))
                {
                    ret = form.ShowDialog();
                }
            }

            if (ret == System.Windows.Forms.DialogResult.OK)
            {
                //(srcCell.GridControl as IBoundGrid).DisplayManager.SearchManager.ReloadItem(row.Index);
                (srcCell.GridControl as Feng.Grid.IBoundGrid).ResetRowData(row);
            }
        }
    }
}
