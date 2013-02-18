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
    public partial class 专家调度一级静态优化 :MyButtonsMainForm
    {
        public 专家调度一级静态优化()
        {
            InitializeComponent();

            AssociateForm(panel1);

            AssociateButtonToForm(btn静态套箱, typeof(专家调度一级静态优化套箱));
            AssociateButtonToForm(btn静态进口箱带货, typeof(专家调度一级静态优化进口箱带货));
            AssociateButtonToForm(btn静态出口箱带货, typeof(专家调度一级静态优化出口箱带货));
            AssociateButtonToForm(btn静态小箱配对, typeof(专家调度一级静态优化小箱配对));

            AssociateButtonMethod(btn计算机辅助优化, "btn计算机辅助优化_Click");
            AssociateButtonMethod(btn全自动优化, "btn全自动优化_Click");
        }

        internal static Color 优化DisableColor = Color.Gray;
        internal static void Clear组合(DataUnboundGrid gridX, DataUnboundGrid gridY)
        {
            foreach (Xceed.Grid.DataRow row in gridX.DataRows)
            {
                if (row.Font.Bold)
                {
                    row.ResetFont();
                }
            }
            foreach (Xceed.Grid.DataRow row in gridY.DataRows)
            {
                if (row.Font.Bold)
                {
                    row.ResetFont();
                }
            }
        }
        internal static void 批量确认_Click(Xceed.Grid.GridControl nowGrid, 专家任务性质? zjrwxz)
        {
            foreach (Xceed.Grid.DataRow row in nowGrid.DataRows)
            {
                bool? b = Feng.Utils.ConvertHelper.ToBoolean(row.Cells["选定2"].Value);
                if (b.HasValue && b.Value)
                {
                    专家调度一级静态优化_DoubleClick(row.Cells["确认"], zjrwxz);
                }
            }
        }

        internal static void 选择组合(DataUnboundGrid gridX, DataUnboundGrid gridY, 专家任务性质 zjrwxz)
        {
            选择组合(gridX, gridY, new List<专家任务性质> { zjrwxz });
        }
        internal static void 选择组合(DataUnboundGrid gridX, DataUnboundGrid gridY, List<专家任务性质> zjrwxz)
        {
            Clear组合(gridX, gridY);

            if (gridX.CurrentRow == null)
                return;
            任务 x = gridX.CurrentRow.Tag as 任务;
            foreach (Xceed.Grid.DataRow row in gridY.DataRows)
            {
                if (row.ForeColor == 专家调度一级静态优化.优化DisableColor)
                    continue;

                任务 y = row.Tag as 任务;

                foreach (var i in zjrwxz)
                {
                    if (m_dao.是否能组合(x, y, i))
                    {
                        row.Font = new Font(row.Font, FontStyle.Bold);
                        break;
                    }
                }
            }
        }
        internal static void 自动优化(DataUnboundGrid gridX, DataUnboundGrid gridY, 专家任务性质 zjrwxz)
        {
            自动优化(gridX, gridY, new List<专家任务性质> { zjrwxz });
        }
        internal static void 自动优化(DataUnboundGrid gridX, DataUnboundGrid gridY, List<专家任务性质> zjrwxz)
        {
            foreach (Xceed.Grid.DataRow rowX in gridY.DataRows)
            {
                foreach (Xceed.Grid.DataRow rowY in gridY.DataRows)
                {
                    任务 x = rowX.Tag as 任务;
                    任务 y = rowY.Tag as 任务;

                    foreach (var i in zjrwxz)
                    {
                        if (m_dao.是否能组合(x, y, i))
                        {
                        }
                    }
                }
            }
        }

        private static string m_topGridDragFildeName = "配对任务号";
        private static 专家任务Dao m_dao = new 专家任务Dao();
        internal static void 专家调度一级静态优化_DoubleClick(Xceed.Grid.Cell srcCell, 专家任务性质? zjrwxz)
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
            else if (srcCell.ParentColumn.FieldName == "取消")
            {
                任务 x = null, y = null;
                if (srcCell.ParentRow.Cells["新任务号"].Value != null)
                {
                    //x = srcCell.ParentRow.Tag as 任务;
                    return;
                }
                if (srcCell.ParentRow.Cells[m_topGridDragFildeName].Value != null)
                {
                    if (srcCell.ParentRow.Cells[m_topGridDragFildeName].Tag != null)
                        y = (srcCell.ParentRow.Cells[m_topGridDragFildeName].Tag as Xceed.Grid.Cell).ParentRow.Tag as 任务;
                }
                if (m_dao.撤销专家任务(x, y))
                {
                    if (srcCell.ParentRow.Cells[m_topGridDragFildeName].Tag != null)
                        (srcCell.ParentRow.Cells[m_topGridDragFildeName].Tag as Xceed.Grid.Cell).ParentRow.ResetForeColor();
                    srcCell.ParentRow.ResetForeColor();
                    srcCell.ParentRow.Cells[m_topGridDragFildeName].Value = null;
                    srcCell.ParentRow.Cells[m_topGridDragFildeName].Tag = null;
                    srcCell.ParentRow.Cells["新任务号"].Value = null;
                }
                else
                {
                    Feng.MessageForm.ShowWarning("无法移出任务！", "操作失败");
                }
            }
            else if (srcCell.ParentColumn.FieldName == "确认")
            {
                if (srcCell.ParentRow.Cells["新任务号"].Value != null)
                    return;

                Xceed.Grid.Cell destCell = srcCell.ParentRow.Cells[m_topGridDragFildeName].Tag as Xceed.Grid.Cell;
                if (destCell == null)
                    return;

                任务 x = srcCell.ParentRow.Tag as 任务;
                任务 y = destCell.ParentRow.Tag as 任务;
                专家任务 zjrw = null;

                string bz = null;
                if (srcCell.ParentRow.Cells["备注"].Value != null)
                    bz = (string)srcCell.ParentRow.Cells["备注"].Value;

                zjrw = m_dao.生成专家任务(x, y, zjrwxz, bz);

                if (zjrw != null)
                {
                    srcCell.ParentRow.Cells["新任务号"].Value = zjrw.新任务号;
                    srcCell.ParentRow.Cells["新任务号"].Tag = zjrw;
                }
            }
            else if (srcCell.ParentColumn.FieldName == "紧急下达")
            {
                if (srcCell.ParentRow.Cells["新任务号"].Tag != null)
                {
                    var zjrw = srcCell.ParentRow.Cells["新任务号"].Tag as 专家任务;
                    if (!zjrw.下达时间.HasValue)
                    {
                        using (var form = new 紧急下达(zjrw))
                        {
                            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                srcCell.ParentRow.Cells["新任务号"].Tag = form.专家任务;
                            }
                        }
                    }
                }
            }
        }

        #region "DragDrop"
        private const string m_dragDataFormatDelete = "专家调度一级静态优化_删除";
        private const string m_dragDataFormatAdd = "专家调度一级静态优化_新建";
        internal static void 专家调度一级静态优化_GridDragStart(object sender, GridDataGragEventArgs e)
        {
            e.Data = null;
            e.AllowedEffect = DragDropEffects.None;

            Xceed.Grid.Cell srcCell = sender as Xceed.Grid.Cell;
            if (srcCell == null)
                return;

            //Xceed.Grid.CellRow row = srcCell.ParentRow;
            if (srcCell.GridControl.Name.Contains("top") || srcCell.GridControl.Name.Contains("小箱配对"))
            {
                if (srcCell.ParentColumn.FieldName == m_topGridDragFildeName
                    && srcCell.Value != null
                    && srcCell.ParentRow.Cells["新任务号"].Value == null)
                {
                    e.Data = new DataObject(m_dragDataFormatDelete, srcCell);
                    e.AllowedEffect = DragDropEffects.Move;
                    return;
                }
            }
            if (srcCell.GridControl.Name.Contains("bottom") || srcCell.GridControl.Name.Contains("小箱配对"))
            {
                if (srcCell.ParentRow.ForeColor != 专家调度一级静态优化.优化DisableColor)
                {
                    e.Data = new DataObject(m_dragDataFormatAdd, srcCell);
                    e.AllowedEffect = DragDropEffects.Link;
                    return;
                }
            }
        }

        internal static void 专家调度一级静态优化_GridDragOver(object sender, DragEventArgs e)
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
                if (srcCell.ParentRow != destCell.ParentRow
                    && (srcCell.GridControl != destCell.GridControl
                        || srcCell.GridControl.Name.Contains("小箱配对"))
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

            //System.Diagnostics.Debug.WriteLine(e.Effect.ToString());
        }
        internal static void 专家调度一级静态优化_GridDragDrop(object sender, DragEventArgs e)
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
                if (srcCell.ParentRow != destCell.ParentRow
                    && (srcCell.GridControl != destCell.GridControl
                        || srcCell.GridControl.Name.Contains("小箱配对"))
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
