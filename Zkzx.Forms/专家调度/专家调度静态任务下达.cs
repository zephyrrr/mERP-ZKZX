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
    public partial class 专家调度静态任务下达 : Feng.Windows.Forms.MyTemplateForm, IRefreshDataForm
    {
        public 专家调度静态任务下达()
        {
            InitializeComponent();
        }

        private DataUnboundGrid m_已优化过的任务集合Grid, m_未优化过的任务集合Grid;
        private void 专家调度静态任务下达_Load(object sender, EventArgs e)
        {
            m_已优化过的任务集合Grid = base.AssociateBoundGrid(pnl已优化过的任务集合, "专家级调度_静态任务下达_已优化过的任务集合") as DataUnboundGrid;
            m_未优化过的任务集合Grid = base.AssociateBoundGrid(pnl未优化过的任务集合, "专家级调度_静态任务下达_未优化过的任务集合") as DataUnboundGrid;

            //m_已优化过的任务集合Grid.Columns["选定"].ReadOnly = false;
            //m_已优化过的任务集合Grid.DataRowTemplate.Cells["选定"].ReadOnly = false;
            //m_未优化过的任务集合Grid.Columns["选定"].ReadOnly = false;
            //m_未优化过的任务集合Grid.DataRowTemplate.Cells["选定"].ReadOnly = false;
            //m_已优化过的任务集合Grid.ReadOnly = false;
            //m_未优化过的任务集合Grid.ReadOnly = false;


            //m_已优化过的任务集合Grid.DataRowTemplate.Cells["调整"].DoubleClick += new EventHandler(已优化任务_DoubleClick);
            m_已优化过的任务集合Grid.DataRowTemplate.Cells["确认"].DoubleClick += new EventHandler(已优化任务_DoubleClick);
            m_已优化过的任务集合Grid.DataRowTemplate.Cells["移出"].DoubleClick += new EventHandler(已优化任务_DoubleClick);
            //m_未优化过的任务集合Grid.DataRowTemplate.Cells["调整"].DoubleClick += new EventHandler(未优化任务_DoubleClick);
            m_未优化过的任务集合Grid.DataRowTemplate.Cells["确认"].DoubleClick += new EventHandler(未优化任务_DoubleClick);
            m_未优化过的任务集合Grid.DataRowTemplate.Cells["移出"].DoubleClick += new EventHandler(未优化任务_DoubleClick);

            
            Helper.SetGridDefault(this, m_已优化过的任务集合Grid);
            Helper.SetGridDefault(this, m_未优化过的任务集合Grid);
            Helper.SetCellButton(m_已优化过的任务集合Grid, "确认", btn批量确认);
            Helper.SetCellButton(m_未优化过的任务集合Grid, "确认", btn批量确认2);
            Helper.SetCellButton(m_已优化过的任务集合Grid, "区域名称", btn区域自动分拣);
            Helper.SetCellButton(m_未优化过的任务集合Grid, "区域名称", btn区域自动分拣2);
            m_已优化过的任务集合Grid.ChangeControlPositionAccordColumn(label1, "区域名称");
            m_未优化过的任务集合Grid.ChangeControlPositionAccordColumn(label2, "区域名称");
        }

        public void RefreshData()
        {
            m_已优化过的任务集合Grid.DisplayManager.SearchManager.LoadData();
            m_未优化过的任务集合Grid.DisplayManager.SearchManager.LoadData();
        }

        专家任务Dao m_dao = new 专家任务Dao();
        任务Dao m_任务dao = new 任务Dao();

        private void btn批量确认下达_Click(object sender, EventArgs e)
        {
            foreach (Xceed.Grid.DataRow row in m_已优化过的任务集合Grid.DataRows)
            {
                bool? b = Feng.Utils.ConvertHelper.ToBoolean(row.Cells["选定"].Value);
                if (b.HasValue && b.Value)
                {
                    已优化任务_DoubleClick(row.Cells["确认"], System.EventArgs.Empty);
                }
            }
        }
        private void btn批量确认下达2_Click(object sender, EventArgs e)
        {
            foreach (Xceed.Grid.DataRow row in m_未优化过的任务集合Grid.DataRows)
            {
                bool? b = Feng.Utils.ConvertHelper.ToBoolean(row.Cells["选定"].Value);
                if (b.HasValue && b.Value)
                {
                    未优化任务_DoubleClick(row.Cells["确认"], System.EventArgs.Empty);
                }
            }
        }
        private Color m_disableColor = System.Drawing.Color.Gray;
        void 已优化任务_DoubleClick(object sender, EventArgs e)
        {
            Xceed.Grid.Cell srcCell = sender as Xceed.Grid.Cell;
            if (srcCell == null)
                return;
            srcCell.ParentRow.EndEdit();

            if (srcCell.ParentColumn.FieldName == "移出")
            {
                if (srcCell.ParentRow.ForeColor == m_disableColor)
                {
                    srcCell.ParentGrid.DataRows.Remove(srcCell.ParentRow as Xceed.Grid.DataRow);
                }
            }
            else if (srcCell.ParentColumn.FieldName == "确认")
            {
                if (srcCell.ParentRow.ForeColor == m_disableColor)
                    return;

                if (专家任务下达Validation(srcCell.ParentRow))
                {
                    专家任务 entity = srcCell.ParentRow.Tag as 专家任务;
                    //entity.时间要求始 = (DateTime)srcCell.ParentRow.Cells["时间要求始"].Value;
                    //entity.时间要求止 = (DateTime)srcCell.ParentRow.Cells["时间要求止"].Value;
                    entity.时间要求始 = entity.时间要求始;
                    entity.时间要求止 = entity.时间要求止;
                    entity.区域编号 = (string)srcCell.ParentRow.Cells["区域名称"].Value;
                    entity.缓急程度 = (int)srcCell.ParentRow.Cells["缓急程度"].Value;
                    if (m_dao.下达专家任务(entity, System.DateTime.Now))
                    {
                        srcCell.ParentRow.ForeColor = m_disableColor;
                        srcCell.ParentRow.ReadOnly = true;
                    }
                }
            }
        }

        void 未优化任务_DoubleClick(object sender, EventArgs e)
        {
            Xceed.Grid.Cell srcCell = sender as Xceed.Grid.Cell;
            if (srcCell == null)
                return;
            srcCell.ParentRow.EndEdit();

            if (srcCell.ParentColumn.FieldName == "移出")
            {
                if (srcCell.ParentRow.ForeColor == m_disableColor)
                {
                    srcCell.ParentGrid.DataRows.Remove(srcCell.ParentRow as Xceed.Grid.DataRow);
                }
            }
            else if (srcCell.ParentColumn.FieldName == "确认")
            {
                if (srcCell.ParentRow.ForeColor == m_disableColor)
                    return;
                if (专家任务下达Validation(srcCell.ParentRow))
                {
                    任务 rw = srcCell.ParentRow.Tag as 任务;
                    System.Diagnostics.Debug.Assert(rw != null && rw.专家任务 == null, "未优化任务专家任务需为空");

                    //if (m_dao.下达专家任务(rw, (DateTime)srcCell.ParentRow.Cells["时间要求始"].Value,
                    //    (DateTime)srcCell.ParentRow.Cells["时间要求止"].Value, (string)srcCell.ParentRow.Cells["区域名称"].Value,
                    //    (int?)srcCell.ParentRow.Cells["缓急程度"].Value, System.DateTime.Now))
                    if (m_dao.下达专家任务(rw, null,
                        null, (string)srcCell.ParentRow.Cells["区域名称"].Value,
                        (int?)srcCell.ParentRow.Cells["缓急程度"].Value, System.DateTime.Now))
                    {
                        srcCell.ParentRow.ForeColor = m_disableColor;
                        srcCell.ParentRow.ReadOnly = true;
                    }
                }
            }
        }

        private bool 专家任务下达Validation(Xceed.Grid.CellRow row)
        {
            if (row.Cells["区域名称"].Value == null)
            {
                MessageForm.ShowWarning("请填写“区域名称”。");
                return false;
            }
            //if (row.Cells["时间要求始"].Value == null)
            //{
            //    MessageForm.ShowWarning("请填写“时间要求始”。");
            //    return false;
            //}
            //else if (row.Cells["时间要求止"].Value == null)
            //{
            //    MessageForm.ShowWarning("请填写“时间要求止”。");
            //    return false;
            //}
            //else if ((DateTime)row.Cells["时间要求始"].Value >= (DateTime)row.Cells["时间要求止"].Value)
            //{
            //    MessageForm.ShowWarning("时间要求始必须小于时间要求止！");
            //    return false;
            //}
            return true;
        }
    }
}
