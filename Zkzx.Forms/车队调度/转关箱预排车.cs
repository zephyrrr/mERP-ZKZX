using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using Feng;
using Feng.Grid;
using Feng.Utils;
using Feng.Windows.Forms;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 转关箱预排车 : MyTemplateForm, IRefreshDataForm
    {
        public 转关箱预排车()
        {
            InitializeComponent();
        }

        private DataUnboundGrid m_全部监管车辆及作业现状Grid, m_待排转关箱任务区Grid;
        private ArchiveUnboundGrid m_暂存计划区Grid;
        private void 转关箱预排车_Load(object sender, EventArgs e)
        {
            m_全部监管车辆及作业现状Grid = base.AssociateBoundGrid(pnl全部监管车辆及作业现状, "车队级调度_转关箱预排车_监管车辆及作业状况") as DataUnboundGrid;
            m_待排转关箱任务区Grid = base.AssociateBoundGrid(pnl待排转关箱任务区, "车队级调度_转关箱预排车_待排转关箱任务") as DataUnboundGrid;
            m_暂存计划区Grid = base.AssociateArchiveGrid(pnl暂存计划区, "车队级调度_转关箱预排车_暂存计划区") as ArchiveUnboundGrid;

            m_全部监管车辆及作业现状Grid.ChangeControlPositionAccordColumn(label1, "第1天");

            m_全部监管车辆及作业现状Grid.DataRowTemplate.Cells["后续作业计划"].DoubleClick += new EventHandler(转关箱预排车_DoubleClick);
            m_待排转关箱任务区Grid.DataRowTemplate.Cells["提单号"].DoubleClick += new EventHandler(转关箱预排车_DoubleClick);

            m_全部监管车辆及作业现状Grid.EnableDragDrop = true;
            m_待排转关箱任务区Grid.EnableDragDrop = true;
            m_全部监管车辆及作业现状Grid.GridDragOver += new System.Windows.Forms.DragEventHandler(m_全部监管车辆及作业现状Grid_GridDragOver);
            m_全部监管车辆及作业现状Grid.GridDragDrop += new System.Windows.Forms.DragEventHandler(m_全部监管车辆及作业现状Grid_GridDragDrop);
            m_全部监管车辆及作业现状Grid.GridDragStart += new EventHandler<GridDataGragEventArgs>(m_待排转关箱任务区Grid_GridDragStart);
            m_待排转关箱任务区Grid.GridDragOver += new System.Windows.Forms.DragEventHandler(m_全部监管车辆及作业现状Grid_GridDragOver);
            m_待排转关箱任务区Grid.GridDragDrop += new System.Windows.Forms.DragEventHandler(m_全部监管车辆及作业现状Grid_GridDragDrop);
            m_待排转关箱任务区Grid.GridDragStart += new EventHandler<GridDataGragEventArgs>(m_待排转关箱任务区Grid_GridDragStart);

            m_暂存计划区Grid.DisplayManager.PositionChanged += new EventHandler(DisplayManager_PositionChanged);

            Helper.SetGridDefault(this, m_暂存计划区Grid);
            Helper.SetGridDefault(this, m_全部监管车辆及作业现状Grid);
        }

        void 转关箱预排车_DoubleClick(object sender, EventArgs e)
        {
            Xceed.Grid.Cell srcCell = sender as Xceed.Grid.Cell;
            if (srcCell == null)
                return;

            if (srcCell.ParentColumn.FieldName == "后续作业计划")
            {
                车辆 cl = srcCell.ParentRow.Tag as 车辆;
                if (cl != null)
                {
                    new 单车后续作业计划(cl).ShowDialog();
                }
            }
            else if (srcCell.ParentColumn.FieldName == "提单号")
            {
                new 进口箱批量任务确认((string)srcCell.Value).ShowDialog();
            }  
        }

        void DisplayManager_PositionChanged(object sender, EventArgs e)
        {
            Clear(false);

            转关箱排车暂存组 entity = m_暂存计划区Grid.DisplayManager.CurrentItem as 转关箱排车暂存组;

            if (entity == null)
            {
                btn计算机辅助优化.Enabled = false;
                btn全自动化.Enabled = false;
                btn确认.Enabled = false;
                btn暂存.Enabled = false;
                btn重排.Enabled = false;
                btn打印.Enabled = false;
                return;
            }

            if (entity.IsActive)
            {
                btn计算机辅助优化.Enabled = false;
                btn全自动化.Enabled = false;
                btn确认.Enabled = false;
                btn暂存.Enabled = false;
                btn重排.Enabled = false;
            }
            else
            {
                btn计算机辅助优化.Enabled = true;
                btn全自动化.Enabled = true;
                btn确认.Enabled = true;
                btn暂存.Enabled = true;
                btn重排.Enabled = true;
            }

            m_待排转关箱任务区Grid.DisplayManager.SearchManager.LoadData(SearchExpression.Eq("暂存组", entity.ID), null);
            m_待排转关箱任务区Grid.DisplayManager.SearchManager.WaitLoadData();

            Load排车任务(entity);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            m_暂存计划区Grid.ControlManager.AddNew();
            (m_暂存计划区Grid.DisplayManager.CurrentItem as 转关箱排车暂存组).预排时间 = System.DateTime.Now;
            m_暂存计划区Grid.ControlManager.EndEdit();
        }

        private void btn暂存_Click(object sender, EventArgs e)
        {
            转关箱排车暂存组 entity = m_暂存计划区Grid.DisplayManager.CurrentItem as 转关箱排车暂存组;
            if (entity != null)
            {
                if (entity.IsActive)
                {
                    MessageForm.ShowWarning("此暂存已经确认！");
                    return;
                }
                Confirm(entity, false);
            }
        }

        private void btn确认_Click(object sender, EventArgs e)
        {
            转关箱排车暂存组 entity = m_暂存计划区Grid.DisplayManager.CurrentItem as 转关箱排车暂存组;
            if (entity != null)
            {
                if (entity.IsActive)
                {
                    MessageForm.ShowWarning("此暂存已经确认！");
                    return;
                }
                Confirm(entity, true);
            }
        }

        private void btn打印_Click(object sender, EventArgs e)
        {
        }

        private void btn重排_Click(object sender, EventArgs e)
        {
            转关箱排车暂存组 entity = m_暂存计划区Grid.DisplayManager.CurrentItem as 转关箱排车暂存组;
            if (entity != null)
            {
                if (entity.IsActive)
                {
                    MessageForm.ShowWarning("此暂存已经确认！");
                    return;
                }
                Clear(true);
            }
        }
        private const int Days = 6;
        private void Clear(bool modifySrcCell)
        {
            foreach (Xceed.Grid.DataRow row in m_全部监管车辆及作业现状Grid.DataRows)
            {
                for (int i = 1; i <= Days; ++i)
                {
                    string fieldName = string.Format("第{0}天", i);
                    if (row.Cells[fieldName].Value != null)
                    {
                        if (modifySrcCell)
                        {
                            ModifySrcCell(row.Cells[fieldName].Tag as Xceed.Grid.DataCell, 1);
                        }
                        row.Cells[fieldName].Value = null;
                        row.Cells[fieldName].Tag = null;
                    }
                }
            }
        }

        public void RefreshData()
        {
            m_全部监管车辆及作业现状Grid.DisplayManager.SearchManager.LoadData();
            m_全部监管车辆及作业现状Grid.DisplayManager.SearchManager.WaitLoadData();

            m_暂存计划区Grid.DisplayManager.SearchManager.LoadData();
        }

        #region "Drag"
        private const string m_dragDataFormatDelete = "转关箱预排车_删除";
        private const string m_dragDataFormatAdd = "转关箱预排车_新建";
        private string m_topGridDragFildeName = "任务色调";
        void m_待排转关箱任务区Grid_GridDragStart(object sender, GridDataGragEventArgs e)
        {
            e.Data = null;
            e.AllowedEffect = System.Windows.Forms.DragDropEffects.None;

            转关箱排车暂存组 entity = m_暂存计划区Grid.DisplayManager.CurrentItem as 转关箱排车暂存组;
            if (entity == null)
            {
                return;
            }

            Xceed.Grid.Cell srcCell = sender as Xceed.Grid.Cell;
            if (srcCell == null)
                return;

            if (srcCell.GridControl == m_待排转关箱任务区Grid
                && srcCell.ParentColumn.FieldName == m_topGridDragFildeName
                && srcCell.ParentRow.Cells[m_topGridDragFildeName].Value != null)
            {
                string s = srcCell.ParentRow.Cells[m_topGridDragFildeName].Value.ToString();
                string[] ss = s.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                int? cnt = ConvertHelper.ToInt(ss[1]);
                if (cnt.HasValue && cnt.Value > 0)
                {
                    e.Data = new System.Windows.Forms.DataObject(m_dragDataFormatAdd, srcCell);
                    e.AllowedEffect = System.Windows.Forms.DragDropEffects.Link;
                }
                return;
            }
            if (srcCell.GridControl == m_全部监管车辆及作业现状Grid
                && srcCell.Value != null)
            {
                e.Data = new System.Windows.Forms.DataObject(m_dragDataFormatDelete, srcCell);
                e.AllowedEffect = System.Windows.Forms.DragDropEffects.Move;
                return;
            }
        }

        void m_全部监管车辆及作业现状Grid_GridDragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            e.Effect = System.Windows.Forms.DragDropEffects.None;
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
                    && destCell.ParentColumn.FieldName.StartsWith("第") && destCell.ParentColumn.FieldName.EndsWith("天"))
                {
                    e.Effect = System.Windows.Forms.DragDropEffects.Link;
                }
            }
            else if (e.Data.GetDataPresent(m_dragDataFormatDelete))
            {
                Xceed.Grid.Cell destCell = sender as Xceed.Grid.Cell;
                if (destCell == null)
                {
                    Xceed.Grid.GridControl grid = sender as Xceed.Grid.GridControl;
                    if (grid != null)
                    {
                        e.Effect = System.Windows.Forms.DragDropEffects.Move;
                    }
                }
                else
                {
                    Xceed.Grid.Cell srcCell = e.Data.GetData(m_dragDataFormatDelete) as Xceed.Grid.Cell;
                    if (srcCell != null)
                    {
                        if (srcCell.GridControl != destCell.GridControl)
                        {
                            e.Effect = System.Windows.Forms.DragDropEffects.Move;
                        }
                        else
                        {
                            if (destCell.Value == null
                                && destCell.ParentColumn.FieldName.StartsWith("第") && destCell.ParentColumn.FieldName.EndsWith("天"))
                            {
                                e.Effect = System.Windows.Forms.DragDropEffects.Move;
                            }
                        }
                    }
                }
            }
        }

        void m_全部监管车辆及作业现状Grid_GridDragDrop(object sender, System.Windows.Forms.DragEventArgs e)
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
                     && destCell.ParentColumn.FieldName.StartsWith("第") && destCell.ParentColumn.FieldName.EndsWith("天"))
                {
                    string s = srcCell.ParentRow.Cells[m_topGridDragFildeName].Value.ToString();
                    string[] ss = s.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    int? idx = ConvertHelper.ToInt(ss[0]);
                    if (idx.HasValue)
                    {
                        destCell.Tag = srcCell;
                        destCell.Value = idx.Value.ToString();
                    }

                    ModifySrcCell(srcCell, -1);
                }
            }
            else if (e.Data.GetDataPresent(m_dragDataFormatDelete))
            {
                Xceed.Grid.Cell destCell = sender as Xceed.Grid.Cell;
                Xceed.Grid.Cell srcCell = e.Data.GetData(m_dragDataFormatDelete) as Xceed.Grid.Cell;

                if (destCell == null)
                {
                    Xceed.Grid.GridControl grid = sender as Xceed.Grid.GridControl;
                    if (grid != null)
                    {
                        ModifySrcCell(srcCell.Tag as Xceed.Grid.Cell, 1);

                        srcCell.Value = null;
                        srcCell.Tag = null;
                    }
                }
                else
                {
                    if (srcCell != null)
                    {
                        if (srcCell.GridControl != destCell.GridControl)
                        {
                            ModifySrcCell(srcCell.Tag as Xceed.Grid.Cell, 1);

                            srcCell.Value = null;
                            srcCell.Tag = null;
                        }
                        else
                        {
                            if (destCell.Value == null
                                && destCell.ParentColumn.FieldName.StartsWith("第") && destCell.ParentColumn.FieldName.EndsWith("天"))
                            {
                                destCell.Value = srcCell.Value;
                                destCell.Tag = srcCell.Tag;
                                srcCell.Value = null;
                                srcCell.Tag = null;
                            }
                        }
                    }
                }
            }
        }

        private void ModifySrcCell(Xceed.Grid.Cell srcCell, int delta)
        {
            string s = srcCell.ParentRow.Cells[m_topGridDragFildeName].Value.ToString();
            string[] ss = s.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            int? idx = ConvertHelper.ToInt(ss[0]);
            if (idx.HasValue)
            {
                int? cnt = ConvertHelper.ToInt(ss[1]);
                srcCell.ParentRow.Cells[m_topGridDragFildeName].Value = idx.Value.ToString() + "/" + (cnt.Value + delta).ToString();
            }
        }

        #endregion

        private void Load排车任务(转关箱排车暂存组 暂存组)
        {
            if (暂存组.转关箱排车 == null)
                return;

            using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<转关箱排车暂存组>())
            {
                rep.Initialize(暂存组.转关箱排车, 暂存组);

                Dictionary<string, int> dict = new Dictionary<string, int>();
                Dictionary<string, Xceed.Grid.Cell> dictCell = new Dictionary<string, Xceed.Grid.Cell>();
                foreach (Xceed.Grid.DataRow row in m_待排转关箱任务区Grid.DataRows)
                {
                    System.Data.DataRowView rowView = row.Tag as System.Data.DataRowView;
                    string s = rowView["序号"].ToString();
                    string[] ss = s.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    int idx = ConvertHelper.ToInt(ss[0]).Value;

                    s = rowView["任务号"].ToString();
                    ss = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var i in ss)
                    {
                        dict[i] = idx;
                        dictCell[i] = row.Cells["任务色调"];
                    }
                }

                Dictionary<车辆, Xceed.Grid.DataRow> cls = new Dictionary<车辆, Xceed.Grid.DataRow>();
                foreach (Xceed.Grid.DataRow row in m_全部监管车辆及作业现状Grid.DataRows)
                {
                    cls[row.Tag as 车辆] = row;
                }
                foreach (var i in 暂存组.转关箱排车)
                {
                    if (dict.ContainsKey(i.任务号) && cls.ContainsKey(i.车辆))
                    {
                        string fieldName = string.Format("第{0}天", i.天数序号);
                        cls[i.车辆].Cells[fieldName].Value = dict[i.任务号].ToString();
                        cls[i.车辆].Cells[fieldName].Tag = dictCell[i.任务号];

                        ModifySrcCell(dictCell[i.任务号], -1);
                    }
                }
            }
        }

        private Model.BaseDao<转关箱排车计划> m_dao = new Model.BaseDao<转关箱排车计划>();
        private Model.BaseDao<转关箱排车暂存组> m_dao暂存组 = new Model.BaseDao<转关箱排车暂存组>();
        private void Confirm(转关箱排车暂存组 暂存组, bool 是否确认)
        {
            Dictionary<int, string[]> dict = new Dictionary<int, string[]>();
            foreach (Xceed.Grid.DataRow row in m_待排转关箱任务区Grid.DataRows)
            {
                System.Data.DataRowView rowView = row.Tag as System.Data.DataRowView;
                string s = rowView["序号"].ToString();
                string[] ss = s.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                int idx = ConvertHelper.ToInt(ss[0]).Value;

                s = rowView["任务号"].ToString();
                ss = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                dict[idx] = ss;
            }
            Dictionary<int, int> dictIdx = new Dictionary<int, int>();
            foreach (var i in dict.Keys)
            {
                dictIdx[i] = 0;
            }

            using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<转关箱排车暂存组>())
            {
                try
                {
                    rep.BeginTransaction();

                    rep.Attach(暂存组);
                    暂存组.IsActive = 是否确认 ? true : false;
                    暂存组.预排时间 = System.DateTime.Now;
                    m_dao暂存组.Update(rep, 暂存组);

                    if (暂存组.转关箱排车 != null)
                    {
                        foreach (var i in 暂存组.转关箱排车)
                        {
                            m_dao.Delete(rep, i);
                        }
                        暂存组.转关箱排车.Clear();
                    }
                    else
                    {
                        暂存组.转关箱排车 = new List<转关箱排车计划>();
                    }

                    foreach (Xceed.Grid.DataRow row in m_全部监管车辆及作业现状Grid.DataRows)
                    {
                        for (int i = 1; i <= Days; ++i)
                        {
                            string fieldName = string.Format("第{0}天", i);
                            if (row.Cells[fieldName].Value != null)
                            {
                                int idx = ConvertHelper.ToInt(row.Cells[fieldName].Value).Value;
                                System.Diagnostics.Debug.Assert(dict.ContainsKey(idx), "必须在现有待排任务中！");
                                string rwh = dict[idx][dictIdx[idx]];
                                dictIdx[idx] = dictIdx[idx] + 1;

                                转关箱排车计划 entity = new 转关箱排车计划();
                                entity.车辆 = row.Tag as 车辆;
                                entity.任务号 = rwh;
                                entity.日期 = 是否确认 ? (DateTime?)System.DateTime.Today.AddDays(i) : null;
                                entity.天数序号 = i;
                                entity.暂存组 = 暂存组;

                                m_dao.Save(rep, entity);

                                暂存组.转关箱排车.Add(entity);
                            }
                        }
                    }
                    rep.CommitTransaction();
                }
                catch (Exception ex)
                {
                    rep.RollbackTransaction();
                    ExceptionProcess.ProcessWithNotify(ex);
                }
            }
        }
    }
}
