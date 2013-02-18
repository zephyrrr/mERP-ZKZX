using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Feng.Grid;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 专家调度一级动态优化 :MyButtonsMainForm
    {
        public 专家调度一级动态优化()
        {
            InitializeComponent();

            AssociateForm(panel1);

            AssociateButtonToForm(btn静态套箱, typeof(专家调度一级动态优化套箱));
            AssociateButtonToForm(btn静态进口箱带货, typeof(专家调度一级动态优化进口箱带货));
            AssociateButtonToForm(btn静态出口箱带货, typeof(专家调度一级动态优化出口箱带货));
            AssociateButtonToForm(btn静态小箱配对, typeof(专家调度一级动态优化小箱配对));

            AssociateButtonMethod(btn计算机辅助优化, "btn计算机辅助优化_Click");
            AssociateButtonMethod(btn全自动优化, "btn全自动优化_Click");
        }

        internal static void 批量确认_Click(Xceed.Grid.GridControl nowGrid, 专家任务性质? zjrwxz)
        {
            foreach (Xceed.Grid.DataRow row in nowGrid.DataRows)
            {
                bool? b = Feng.Utils.ConvertHelper.ToBoolean(row.Cells["选定2"].Value);
                if (b.HasValue && b.Value)
                {
                    专家调度一级动态优化_DoubleClick(row.Cells["确认"], zjrwxz);
                }
            }
        }

        internal static void 专家调度一级动态优化_DoubleClick(Xceed.Grid.Cell srcCell, 专家任务性质? zjrwxz)
        {
            if (srcCell == null)
                return;

            if (srcCell.ParentColumn.FieldName == "移出")
            {
                if (srcCell.ParentRow.Cells["新任务号"].Value != null)
                {
                    Xceed.Grid.Cell srcCell2 = srcCell.ParentRow.Cells[m_topGridDragFildeName].Tag as Xceed.Grid.Cell;
                    srcCell.ParentGrid.DataRows.Remove(srcCell.ParentRow as Xceed.Grid.DataRow);
                    srcCell2.ParentGrid.DataRows.Remove(srcCell2.ParentRow as Xceed.Grid.DataRow);
                }
            }
            else if (srcCell.ParentColumn.FieldName == "确认")
            {
                // Todo
                if (srcCell.ParentRow.Cells["新任务号"].Value != null)
                    return;

                Xceed.Grid.Cell destCell = srcCell.ParentRow.Cells[m_topGridDragFildeName].Tag as Xceed.Grid.Cell;
                if (destCell == null)
                    return;

                任务 x = srcCell.ParentRow.Tag as 任务;
                任务 y = destCell.ParentRow.Tag as 任务;

                if (srcCell.ParentRow.Cells["备注"].Value != null)
                    x.专家任务.备注 = (string)srcCell.ParentRow.Cells["备注"].Value;

                //紧急下达 form = new 紧急下达(x.专家任务, y, zjrwxz);
                //using (form)
                //{
                //    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //    {
                //        //专家任务 zjrw = m_dao.生成专家任务(x.专家任务, y);
                //        srcCell.ParentRow.Cells["新任务号"].Value = form.专家任务.新任务号;
                //        srcCell.ParentRow.Cells["新任务号"].Tag = form.专家任务;
                //    }
                //}
                var zjrw = x.专家任务;
                zjrw = m_专家任务Dao.生成专家任务(zjrw, y, zjrwxz);
                srcCell.ParentRow.Cells["新任务号"].Value = zjrw.新任务号;
                srcCell.ParentRow.Cells["新任务号"].Tag = zjrw;
            }
            else if (srcCell.ParentColumn.FieldName == "紧急下达")
            {
                // do nothing now
            }
            else if (srcCell.ParentColumn.FieldName == "专家任务号")
            {
                任务 x = srcCell.ParentRow.Tag as 任务;
                var zjrw = x.专家任务;
                if (zjrw.车辆作业 == null)
                    return;

                using (var form = new 单个作业监控详情(zjrw.车辆作业))
                {
                    form.ShowDialog();
                }

            }
            else if (srcCell.ParentColumn.FieldName == "车辆位置")
            {
                任务 x = srcCell.ParentRow.Tag as 任务;
                var zjrw = x.专家任务;
                if (zjrw.车辆作业 == null)
                    return;
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
                if (zjrw.车辆作业.Track.HasValue)
                {
                    m_mapForm.LoadTrack(zjrw.车辆作业.Track.Value);
                }
            }
        }
        private static Feng.Map.MapForm m_mapForm = null;
        #region "Dragdrop"
        private static string m_topGridDragFildeName = "配对任务号";
        //private Color m_enableColor = Color.Yellow;
        private static 专家任务Dao m_专家任务Dao = new 专家任务Dao();
        private const string m_dragDataFormatDelete = "专家调度一级动态优化_删除";
        private const string m_dragDataFormatAdd = "专家调度一级动态优化_新建";

        internal static void 专家调度一级动态优化_GridDragStart(object sender, GridDataGragEventArgs e)
        {
            e.Data = null;
            e.AllowedEffect = DragDropEffects.None;

            Xceed.Grid.Cell srcCell = sender as Xceed.Grid.Cell;
            if (srcCell == null)
                return;

            if (srcCell.GridControl.Name.Contains("top"))
            {
                if (srcCell.ParentColumn.FieldName == m_topGridDragFildeName
                    && srcCell.Value != null
                    && srcCell.ParentRow.Cells["新任务号"].Value == null)
                {
                    e.Data = new DataObject(m_dragDataFormatDelete, srcCell);
                    e.AllowedEffect = DragDropEffects.Move;
                }
            }
            if (srcCell.GridControl.Name.Contains("bottom"))
            {
                if (srcCell.ParentRow.ForeColor != 专家调度一级静态优化.优化DisableColor)
                {
                    e.Data = new DataObject(m_dragDataFormatAdd, srcCell);
                    e.AllowedEffect = DragDropEffects.Link;
                }
            }
        }

        internal static void 专家调度一级动态优化_GridDragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            if (e.Data == null)
                return;
            if (e.Data.GetDataPresent(m_dragDataFormatAdd))
            {
                Xceed.Grid.Cell destCell = sender as Xceed.Grid.Cell;
                if (destCell == null)
                    return;
                Xceed.Grid.Cell srcCell = e.Data.GetData(m_dragDataFormatAdd) as Xceed.Grid.Cell;

                // 增加
                if (srcCell.GridControl != destCell.GridControl
                    && destCell.Value == null
                    && destCell.ParentColumn.FieldName == m_topGridDragFildeName
                    && destCell.ParentRow.ForeColor != 专家调度一级静态优化.优化DisableColor)
                {
                    e.Effect = DragDropEffects.Link;
                }
            }
            else if (e.Data.GetDataPresent(m_dragDataFormatDelete))
            {
                Xceed.Grid.Cell destCell = sender as Xceed.Grid.Cell;
                Xceed.Grid.Cell srcCell = e.Data.GetData(m_dragDataFormatDelete) as Xceed.Grid.Cell;
                if (destCell != null)
                {
                    // 更改
                    if (destCell.GridControl == srcCell.GridControl)
                    {
                        if (destCell.Value == null
                             && destCell.ParentColumn.FieldName == m_topGridDragFildeName
                             && destCell.ParentRow != srcCell.ParentRow
                             && destCell.ParentRow.ForeColor != 专家调度一级静态优化.优化DisableColor)
                        {
                            e.Effect = DragDropEffects.Move;
                        }
                    }
                    // 删除
                    else
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                }
                else
                {
                    // 删除
                    Xceed.Grid.GridControl destGrid = sender as Xceed.Grid.GridControl;
                    if (destGrid != null && destGrid != srcCell.GridControl)
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                }
            }
        }

        internal static void 专家调度一级动态优化_GridDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data == null)
                return;
            if (e.Data.GetDataPresent(m_dragDataFormatAdd))
            {
                Xceed.Grid.Cell destCell = sender as Xceed.Grid.Cell;
                if (destCell == null)
                    return;
                Xceed.Grid.Cell srcCell = e.Data.GetData(m_dragDataFormatAdd) as Xceed.Grid.Cell;

                // 增加
                if (srcCell.GridControl != destCell.GridControl
                    && destCell.Value == null
                    && destCell.ParentColumn.FieldName == m_topGridDragFildeName
                    && destCell.ParentRow.ForeColor != 专家调度一级静态优化.优化DisableColor)
                {
                    destCell.Tag = srcCell;
                    destCell.Value = (srcCell.ParentRow.Tag as 任务).任务号;
                    destCell.ParentRow.ForeColor = 专家调度一级静态优化.优化DisableColor;
                    srcCell.ParentRow.ForeColor = 专家调度一级静态优化.优化DisableColor;
                }
            }
            else if (e.Data.GetDataPresent(m_dragDataFormatDelete))
            {
                Xceed.Grid.Cell destCell = sender as Xceed.Grid.Cell;
                Xceed.Grid.Cell srcCell = e.Data.GetData(m_dragDataFormatDelete) as Xceed.Grid.Cell;
                if (destCell != null)
                {
                    // 更改
                    if (destCell.GridControl == srcCell.GridControl)
                    {
                        if (destCell.Value == null
                             && destCell.ParentColumn.FieldName == m_topGridDragFildeName
                             && destCell.ParentRow != srcCell.ParentRow
                             && destCell.ParentRow.ForeColor != 专家调度一级静态优化.优化DisableColor)
                        {
                            destCell.Tag = srcCell.Tag;
                            srcCell.Tag = null;
                            destCell.Value = srcCell.Value;
                            srcCell.Value = null;
                            destCell.ParentRow.ForeColor = 专家调度一级静态优化.优化DisableColor;
                            srcCell.ParentRow.ResetForeColor();
                        }
                    }
                    // 删除
                    else
                    {
                        Xceed.Grid.Cell srcCell2 = srcCell.Tag as Xceed.Grid.Cell;
                        srcCell2.ParentRow.ResetForeColor();

                        srcCell.Tag = null;
                        srcCell.Value = null;
                        srcCell.ParentRow.ResetForeColor();
                    }
                }
                else
                {
                    // 删除
                    Xceed.Grid.GridControl destGrid = sender as Xceed.Grid.GridControl;
                    if (destGrid != null)
                    {
                        Xceed.Grid.Cell srcCell2 = srcCell.Tag as Xceed.Grid.Cell;
                        srcCell2.ParentRow.ResetForeColor();

                        srcCell.Tag = null;
                        srcCell.Value = null;
                        srcCell.ParentRow.ResetForeColor();
                    }
                }
            }
        }
        #endregion
    }
}
