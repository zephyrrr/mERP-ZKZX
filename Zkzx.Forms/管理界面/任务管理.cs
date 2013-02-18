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
    public partial class 任务管理 : MyTemplateForm
    {
        public 任务管理()
        {
            InitializeComponent();
        }

        private DataUnboundGrid m_任务集合;
        private static 任务Dao m_任务dao = new 任务Dao();
        private static 专家任务Dao m_专家任务dao = new 专家任务Dao();

        private void 任务管理_Load(object sender, EventArgs e)
        {
            m_任务集合 = base.AssociateBoundGrid(pnl任务集合, "管理_任务管理") as DataUnboundGrid;

            m_任务集合.DataRowTemplate.Cells["撤销预发送"].DoubleClick += new EventHandler(任务管理_DoubleClick);
            m_任务集合.DataRowTemplate.Cells["撤销备案确认"].DoubleClick += new EventHandler(任务管理_DoubleClick);
            m_任务集合.DataRowTemplate.Cells["撤销专家任务"].DoubleClick += new EventHandler(任务管理_DoubleClick);

            btn刷新_Click(btn刷新, EventArgs.Empty);
        }

        public static void ResetRowData(ArchiveOperationForm masterForm)
        {
            DataUnboundGrid m_grid = masterForm.MasterGrid as DataUnboundGrid;
            if (m_grid != null)
            {
                m_grid.ResetRowData(m_grid.CurrentDataRow);
            }
        }

        public static void 撤销预发送(ArchiveOperationForm masterForm)
        {
            if (masterForm.DisplayManager.CurrentItem != null)
            {
                撤销任务预发送(masterForm.DisplayManager.CurrentItem as 任务);
                ResetRowData(masterForm);
            }
        }

        public static void 撤销备案确认(ArchiveOperationForm masterForm)
        {
            if (masterForm.DisplayManager.CurrentItem != null)
            {
                撤销任务备案确认(masterForm.DisplayManager.CurrentItem as 任务);
                ResetRowData(masterForm);
            }
        }

        public static void 撤销专家任务(ArchiveOperationForm masterForm)
        {
            if (masterForm.DisplayManager.CurrentItem != null)
            {
                撤销任务的专家任务(masterForm.DisplayManager.CurrentItem as 任务);
                ResetRowData(masterForm);
            }
        }

        private static void 撤销任务预发送(任务 entity)
        {
            if (!entity.IsActive) 
                return;

            if (撤销Validation(entity, 撤销任务.撤销预发送))
            {
                entity.IsActive = false;
                m_任务dao.Update(entity);
            }
        }

        private static void 撤销任务备案确认(任务 entity)
        {
            if (entity.任务号 == null) 
                return;

            if (撤销Validation(entity, 撤销任务.撤销备案确认))
            {
                entity.任务号 = null;
                m_任务dao.Update(entity);
            }
        }

        private static void 撤销任务的专家任务(任务 entity)
        {
            if (entity.专家任务 == null) return;

            if (撤销Validation(entity, 撤销任务.撤销专家任务))
            {
                m_专家任务dao.撤销专家任务(entity);
            }
        }

        void 任务管理_DoubleClick(object sender, EventArgs e)
        {
            Xceed.Grid.Cell srcCell = sender as Xceed.Grid.Cell;
            if (srcCell == null)
                return;

            任务 rw = srcCell.ParentRow.Tag as 任务;
            if (rw != null)
            {
                if (srcCell.ParentColumn.FieldName == "撤销预发送")
                {
                    撤销任务预发送(rw);
                }
                else if (srcCell.ParentColumn.FieldName == "撤销备案确认")
                {
                    撤销任务备案确认(rw);
                }
                else if (srcCell.ParentColumn.FieldName == "撤销专家任务")
                {
                    撤销任务的专家任务(rw);
                }
            }

            btn刷新_Click(btn刷新, EventArgs.Empty);
        }

        enum 撤销任务
        {
            撤销预发送, 撤销备案确认, 撤销专家任务
        }

        private static bool 撤销Validation(任务 rw, 撤销任务 cx)
        {
            switch (cx)
            {
                case 撤销任务.撤销预发送:
                    if (rw.任务号 != null)
                    {
                        MessageForm.ShowWarning("任务已生成任务号，无法撤销");
                        return false;
                    }
                    break;
                case 撤销任务.撤销备案确认:
                    if (rw.专家任务 != null)
                    {
                        MessageForm.ShowWarning("任务已安排专家任务，无法撤销");
                        return false;
                    }
                    break;
                case 撤销任务.撤销专家任务:
                    if (rw.专家任务.车辆作业 != null)
                    {
                        MessageForm.ShowWarning("任务的专家任务已安排车辆作业，无法撤销");
                        return false;
                    }
                    if (rw.专家任务.下达时间.HasValue)
                    {
                        MessageForm.ShowWarning("任务的专家任务已下达，无法撤销");
                        return false;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("撤销任务 out of range");
            }
            return true;
        }

        private void btn刷新_Click(object sender, EventArgs e)
        {
            m_任务集合.DisplayManager.SearchManager.LoadData();
        }
    }
}
