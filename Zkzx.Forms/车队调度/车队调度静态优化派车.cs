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
using Feng.Utils;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 车队调度静态优化派车 : Feng.Windows.Forms.MyTemplateForm, IRefreshDataForm
    {
        private DataUnboundGrid m_待命车辆_单车多任务Grid;
        private DataUnboundGrid m_待排任务Grid;

        #region "Constructor"
        public 车队调度静态优化派车()
        {
            InitializeComponent();
        }

        //void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    if (m_待命车辆_单车多任务Grid.GridHelper.ContextCell != null)
        //    {
        //        string fieldName = m_待命车辆_单车多任务Grid.GridHelper.ContextCell.ParentColumn.FieldName;
        //        if (fieldName.StartsWith("中心新任务号"))
        //        {
        //            if (m_待命车辆_单车多任务Grid.GridHelper.ContextCell.Value != null)
        //            {
        //                tsmSetBz.Visible = true;
        //                return;
        //            }
        //        }
        //    }
        //    tsmSetBz.Visible = false;
        //}

        //void tsmSetBz_Click(object sender, EventArgs e)
        //{
        //    if (m_待命车辆_单车多任务Grid.GridHelper.ContextCell != null)
        //    {
        //        string fieldName = m_待命车辆_单车多任务Grid.GridHelper.ContextCell.ParentColumn.FieldName;
        //        if (fieldName.StartsWith("中心新任务号"))
        //        {
        //            if (m_待命车辆_单车多任务Grid.GridHelper.ContextCell.Value != null)
        //            {
        //                string si = fieldName.Substring(fieldName.Length - 1);
        //                string bz = (string)m_待命车辆_单车多任务Grid.GridHelper.ContextCell.ParentRow.Cells["作业备注" + si].Value;
        //                using (var form = new 车队调度设置作业备注(bz))
        //                {
        //                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //                    {
        //                        m_待命车辆_单车多任务Grid.GridHelper.ContextCell.ParentRow.Cells["作业备注" + si].Value = form.备注;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        
        private void 车队调度静态任务下达_Load(object sender, EventArgs e)
        {
            m_待排任务Grid = base.AssociateBoundGrid(pnl待排任务, "车队级调度_静态任务下达_待排任务") as DataUnboundGrid;
            m_待命车辆_单车多任务Grid = base.AssociateBoundGrid(pnl待命车辆_单车多任务, "车队级调度_静态任务下达_待命车辆_单车多任务") as DataUnboundGrid;

            //m_待命车辆_单车多任务Grid.GridHelper.MergeContenxtMenuStripForCell(this.contextMenuStrip1);
            //m_待命车辆_单车多任务Grid.GridHelper.ContextMenuStripForCell.Opening += new System.ComponentModel.CancelEventHandler(contextMenuStrip1_Opening);
            //tsmSetBz.Click += new EventHandler(tsmSetBz_Click);

            m_待命车辆_单车多任务Grid.DataRowTemplate.Cells["撤销"].DoubleClick += new EventHandler(车队调度静态任务下达_DoubleClick);
            m_待命车辆_单车多任务Grid.DataRowTemplate.Cells["确认"].DoubleClick += new EventHandler(车队调度静态任务下达_DoubleClick);
            m_待命车辆_单车多任务Grid.DataRowTemplate.Cells["后续作业计划"].DoubleClick += new EventHandler(车队调度静态任务下达_DoubleClick);
            m_待命车辆_单车多任务Grid.DataRowTemplate.Cells["车牌号"].DoubleClick += new EventHandler(车队调度静态任务下达_DoubleClick);


            m_待命车辆_单车多任务Grid.EnableDragDrop = true;
            m_待排任务Grid.EnableDragDrop = true;

            m_待命车辆_单车多任务Grid.GridDragStart += new EventHandler<GridDataGragEventArgs>(车队调度静态任务下达_GridDragStart);
            m_待排任务Grid.GridDragStart += new EventHandler<GridDataGragEventArgs>(车队调度静态任务下达_GridDragStart);

            m_待命车辆_单车多任务Grid.GridDragDrop += new DragEventHandler(车队调度静态任务下达_GridDragDrop);
            m_待排任务Grid.GridDragDrop += new DragEventHandler(车队调度静态任务下达_GridDragDrop);

            m_待命车辆_单车多任务Grid.GridDragOver += new DragEventHandler(车队调度静态任务下达_GridDragOver);
            m_待排任务Grid.GridDragOver += new DragEventHandler(车队调度静态任务下达_GridDragOver);

            m_待命车辆_单车多任务Grid.GotFocus += new EventHandler(m_待命车辆_单车单任务Grid_GotFocus);
            m_待排任务Grid.GotFocus += new EventHandler(m_待命车辆_单车单任务Grid_GotFocus);

            Helper.SetGridDefault(this, m_待命车辆_单车多任务Grid);
            Helper.SetGridDefault(this, m_待排任务Grid);
            Helper.SetCellButton(m_待命车辆_单车多任务Grid, "确认", btn批量确认);
            m_待命车辆_单车多任务Grid.ChangeControlPositionAccordColumn(label1, "中心新任务号1");
        }

        #endregion

        #region "Auto"
        private object m_lastGrid;
        void m_待命车辆_单车单任务Grid_GotFocus(object sender, EventArgs e)
        {
            m_lastGrid = sender;
        }

        private void btn计算机辅助优化_Click(object sender, EventArgs e)
        {
            if (m_lastGrid == m_待排任务Grid)
            {
                选择组合(m_待排任务Grid, m_待命车辆_单车多任务Grid);
            }
            else
            {
                选择组合(m_待命车辆_单车多任务Grid, m_待排任务Grid);
            }
        }

        private void Clear组合()
        {
            专家调度一级静态优化.Clear组合(m_待命车辆_单车多任务Grid, m_待排任务Grid);
        }

        private void 选择组合(DataUnboundGrid gridX, DataUnboundGrid gridY)
        {
            专家调度一级静态优化.Clear组合(gridX, gridY);

            if (gridX.CurrentRow == null)
                return;
            
            if (gridX == m_待排任务Grid)
            {
                专家任务 x = gridX.CurrentRow.Tag as 专家任务; ;
                foreach (Xceed.Grid.DataRow row in gridY.DataRows)
                {
                    string s = (string)row.Cells["后续作业计划"].Value;   // 1/2
                    int v1 = 0, v2 = 0;
                    int idx = s.IndexOf('/');
                    if (idx != -1)
                    {
                        int? v = ConvertHelper.ToInt(s.Substring(0, idx));
                        if (v.HasValue)
                            v1 = v.Value;
                        v = ConvertHelper.ToInt(s.Substring(idx + 1));
                        if (v.HasValue)
                            v2 = v.Value;
                    }
                    if (是否能组合(x, row.Tag as 车辆, v1 + v2))
                    {
                        row.Font = new Font(row.Font, FontStyle.Bold);
                    }
                }
            }
            else if (gridX == m_待命车辆_单车多任务Grid)
            {
                string s = (string)gridX.CurrentDataRow.Cells["后续作业计划"].Value;   // 1/2
                int v1 = 0, v2 = 0;
                int idx = s.IndexOf('/');
                if (idx != -1)
                {
                    int? v = ConvertHelper.ToInt(s.Substring(0, idx));
                    if (v.HasValue)
                        v1 = v.Value;
                    v = ConvertHelper.ToInt(s.Substring(idx + 1));
                    if (v.HasValue)
                        v2 = v.Value;
                }
                foreach (Xceed.Grid.DataRow row in gridY.DataRows)
                {
                    专家任务 x = row.Tag as 专家任务;
                    if (是否能组合(x, row.Tag as 车辆, v1 + v2))
                    {
                        row.Font = new Font(row.Font, FontStyle.Bold);
                    }
                }
            }
        }
        private bool 是否能组合(专家任务 x, 车辆 y, int 后续作业计划)
        {
            if (后续作业计划 < 1)
            {
                return true;
            }
            return false;
        }
            
        private void btn全自动优化_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region "Button"
        private int m_multiTaskCnt = 3;
        void 车队调度静态任务下达_DoubleClick(object sender, EventArgs e)
        {
            Xceed.Grid.Cell srcCell = sender as Xceed.Grid.Cell;
            if (srcCell == null)
                return;
            srcCell.ParentRow.EndEdit();

            if (srcCell.GridControl == m_待命车辆_单车多任务Grid)
            {
                if (srcCell.ParentColumn.FieldName == "确认")
                {
                    for (int i = 1; i <= m_multiTaskCnt; ++i)
                    {
                        string si = i.ToString();
                        if (srcCell.ParentRow.Cells["中心新任务号" + si].Value != null)
                        {
                            Xceed.Grid.Cell destCell = srcCell.ParentRow.Cells["中心新任务号" + si].Tag as Xceed.Grid.Cell;
                            if (destCell == null)
                                continue;

                            if (srcCell.ParentRow.Cells["作业号" + si].Value != null)
                                continue;

                            车辆 cl = srcCell.ParentRow.Tag as 车辆;
                            专家任务 zjrw = destCell.ParentRow.Tag as 专家任务;
                            车辆作业 clzy = m_clzyDao.生成车辆作业(cl, zjrw, (string)srcCell.ParentRow.Cells["作业备注"].Value);
                            srcCell.ParentRow.Cells["作业号" + si].Value = clzy.作业号;
                            srcCell.ParentRow.Cells["作业号" + si].Tag = clzy;

                            srcCell.ParentRow.Cells["作业号Any"].Value = clzy.作业号;
                            srcCell.ParentRow.Cells["作业号Any"].Tag = clzy;
                            srcCell.ParentRow.Cells["中心新任务号" + si].ForeColor = 专家调度一级静态优化.优化DisableColor;
                        }
                    }
                    m_待命车辆_单车多任务Grid.ResetRowData(srcCell.ParentRow as Xceed.Grid.DataRow);
                }
                else if (srcCell.ParentColumn.FieldName == "撤销")
                {
                    if (srcCell.ParentRow.Cells["作业号Any"].Value != null)
                    {
                        return;
                    }
                    for (int i = 1; i <= m_multiTaskCnt; ++i)
                    {
                        string si = i.ToString();
                        if (srcCell.ParentRow.Cells["中心新任务号" + si].Value != null)
                        {
                            if (srcCell.ParentRow.Cells["中心新任务号" + si].Tag == null)
                                continue;

                            (srcCell.ParentRow.Cells["中心新任务号" + si].Tag as Xceed.Grid.Cell).ParentRow.ResetForeColor();
                            srcCell.ParentRow.Cells["中心新任务号" + si].Value = null;
                            srcCell.ParentRow.Cells["中心新任务号" + si].Tag = null;
                        }
                    }
                }
            }

            if (srcCell.ParentColumn.FieldName == "后续作业计划")
            {
                车辆 cl = srcCell.ParentRow.Tag as 车辆;
                if (cl != null)
                {
                    new 单车后续作业计划(cl).ShowDialog();
                }
            }
            else if (srcCell.ParentColumn.FieldName == "车牌号")
            {
                new 车辆详细信息(srcCell.ParentRow.Tag as 车辆).ShowDialog();
            }
        }
        #endregion

        #region "Drag"
        private 车辆作业Dao m_clzyDao = new 车辆作业Dao();
        private const string m_dragDataFormatAdd = "车队调度静态任务下达_新建";
        private const string m_dragDataFormatDelete = "车队调度静态任务下达_删除";
        private string m_topGridDragFildeName = "中心新任务号";
        void 车队调度静态任务下达_GridDragStart(object sender, GridDataGragEventArgs e)
        {
            e.Data = null;
            e.AllowedEffect = DragDropEffects.None;

            Xceed.Grid.Cell srcCell = sender as Xceed.Grid.Cell;
            if (srcCell == null)
                return;

            if (srcCell.GridControl == m_待命车辆_单车多任务Grid)
            {
                // Todo
                if (srcCell.ParentColumn.FieldName.StartsWith(m_topGridDragFildeName)
                    && srcCell.Value != null
                    && srcCell.ParentRow.Cells["作业号Any"].Value == null)
                {
                    e.Data = new DataObject(m_dragDataFormatDelete, srcCell);
                    e.AllowedEffect = DragDropEffects.Move;
                }
            }
            else if (srcCell.GridControl == m_待排任务Grid)
            {
                if (srcCell.ParentRow.ForeColor != 专家调度一级静态优化.优化DisableColor)
                {
                    e.Data = new DataObject(m_dragDataFormatAdd, srcCell);
                    e.AllowedEffect = DragDropEffects.Link;
                }
            }
        }

        void 车队调度静态任务下达_GridDragOver(object sender, DragEventArgs e)
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
                if (destCell.GridControl == m_待命车辆_单车多任务Grid)
                {
                    if (srcCell.GridControl != destCell.GridControl
                        && destCell.Value == null
                        && destCell.ParentColumn.FieldName.StartsWith(m_topGridDragFildeName)
                        && destCell.ParentRow.Cells["作业号Any"].Value == null)
                    {
                        e.Effect = DragDropEffects.Link;
                    }
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
                        if (destCell.GridControl == m_待命车辆_单车多任务Grid)
                        {
                            if (destCell.Value == null
                                 && destCell.ParentColumn.FieldName.StartsWith(m_topGridDragFildeName)
                                 && destCell != srcCell
                                 && destCell.ParentRow.Cells["作业号Any"].Value == null)
                            {
                                e.Effect = DragDropEffects.Move;
                            }
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

        void 车队调度静态任务下达_GridDragDrop(object sender, DragEventArgs e)
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
                if (destCell.GridControl == m_待命车辆_单车多任务Grid)
                {
                    if (srcCell.GridControl != destCell.GridControl
                        && destCell.Value == null
                        && destCell.ParentColumn.FieldName.StartsWith(m_topGridDragFildeName)
                        && destCell.ParentRow.Cells["作业号Any"].Value == null)
                    {
                        destCell.Tag = srcCell;
                        destCell.Value = (srcCell.ParentRow.Tag as 专家任务).新任务号;
                        //destCell.ParentRow.ForeColor = 专家调度一级静态优化.优化DisableColor;
                        srcCell.ParentRow.ForeColor = 专家调度一级静态优化.优化DisableColor;
                    }
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
                        if (destCell.GridControl == m_待命车辆_单车多任务Grid)
                        {
                            if (destCell.Value == null
                                 && destCell.ParentColumn.FieldName.StartsWith(m_topGridDragFildeName)
                                 && destCell != srcCell
                                 && destCell.ParentRow.Cells["作业号Any"].Value == null
                                 && destCell.ParentRow.ForeColor != 专家调度一级静态优化.优化DisableColor)
                            {
                                destCell.Tag = srcCell.Tag;
                                srcCell.Tag = null;
                                destCell.Value = srcCell.Value;
                                srcCell.Value = null;
                                //destCell.ParentRow.ForeColor = 专家调度一级静态优化.优化DisableColor;
                                //srcCell.ParentRow.ResetForeColor();
                            }
                        }
                    }
                    // 删除
                    else
                    {
                        Xceed.Grid.Cell srcCell2 = srcCell.Tag as Xceed.Grid.Cell;
                        srcCell2.ParentRow.ResetForeColor();

                        srcCell.Tag = null;
                        srcCell.Value = null;
                        //srcCell.ParentRow.ResetForeColor();
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
                        //srcCell.ParentRow.ResetForeColor();
                    }
                }
            }

            //else if (srcRow.Tag is 车辆)
            //{
            //    object tag = destCell.Tag;
            //    destCell.Tag = srcRow.Cells["中心新任务号"].Tag;
            //    srcRow.Cells["中心新任务号"].Tag = tag;

            //    object tag1 = destCell.ParentRow.Cells["作业号"].Tag;
            //    destCell.ParentRow.Cells["作业号"].Tag = srcRow.Cells["作业号"].Tag;
            //    车辆作业 srcClzy = destCell.ParentRow.Cells["作业号"].Tag as 车辆作业;
            //    destCell.ParentRow.Cells["作业号"].Value = srcClzy.作业号;
            //    srcRow.Cells["作业号"].Tag = tag1;

            //    if (destCell.Value == null)
            //    {
            //        destCell.Value = ((destCell.Tag as Xceed.Grid.CellRow).Tag as 专家任务).新任务号;
            //        srcRow.Cells["中心新任务号"].Value = null;

            //        srcRow.Cells["作业号"].Value = null;

            //        destCell.ParentRow.BackColor = 专家调度一级静态优化.优化DisableColor;
            //        srcRow.ResetBackColor();

            //        m_clzyDao.更换车辆作业(srcClzy, destCell.ParentRow.Tag as 车辆);
            //    }
            //    else
            //    {
            //        destCell.Value = ((destCell.Tag as Xceed.Grid.CellRow).Tag as 专家任务).新任务号;
            //        srcRow.Cells["中心新任务号"].Tag = tag;
            //        srcRow.Cells["中心新任务号"].Value = ((srcRow.Cells["中心新任务号"].Tag as Xceed.Grid.CellRow).Tag as 专家任务).新任务号;

            //        srcRow.Cells["作业号"].Tag = tag1;
            //        车辆作业 destClzy = srcRow.Cells["作业号"].Tag as 车辆作业;
            //        srcRow.Cells["作业号"].Value = destClzy.作业号;

            //        destCell.ParentRow.BackColor = 专家调度一级静态优化.优化DisableColor;
            //        srcRow.BackColor = 专家调度一级静态优化.优化DisableColor;

            //        m_clzyDao.更换车辆作业(srcClzy, destClzy);
            //    }
            //}
        }

        #endregion

        #region "Refresh"
        

        public void RefreshData()
        {
            m_待命车辆_单车多任务Grid.DisplayManager.SearchManager.LoadData();
            m_待排任务Grid.DisplayManager.SearchManager.LoadData();
        }
        #endregion

        private void btn批量确认_Click(object sender, EventArgs e)
        {
            Helper.btn批量确认_Click(m_待命车辆_单车多任务Grid, (sender1, e1) =>
            {
                车队调度静态任务下达_DoubleClick(sender1, e1);
            });
        }
    }
}
