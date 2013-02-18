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
    public partial class 专家任务管理 : MyTemplateForm
    {
        public 专家任务管理()
        {
            InitializeComponent();
        }

        private DataUnboundGrid m_专家任务集合;
        private static 专家任务Dao m_专家任务dao = new 专家任务Dao();
        private static 车辆作业Dao m_车辆作业dao = new 车辆作业Dao();

        private void 专家任务管理_Load(object sender, EventArgs e)
        {
            m_专家任务集合 = base.AssociateBoundGrid(pnl专家任务集合, "管理_专家任务管理") as DataUnboundGrid;

            m_专家任务集合.DataRowTemplate.Cells["撤销监控"].DoubleClick += new EventHandler(专家任务管理_DoubleClick);
            m_专家任务集合.DataRowTemplate.Cells["撤销车辆作业"].DoubleClick += new EventHandler(专家任务管理_DoubleClick);
            m_专家任务集合.DataRowTemplate.Cells["撤销任务下达"].DoubleClick += new EventHandler(专家任务管理_DoubleClick);

            btn刷新_Click(btn刷新, EventArgs.Empty);
        }

        public static void 撤销监控(ArchiveOperationForm masterForm)
        {
            if (masterForm.DisplayManager.CurrentItem != null)
            {
                撤销专家任务的车辆作业监控(masterForm.DisplayManager.CurrentItem as 专家任务);
                任务管理.ResetRowData(masterForm);
            }
        }

        public static void 撤销车辆作业(ArchiveOperationForm masterForm)
        {
            if (masterForm.DisplayManager.CurrentItem != null)
            {
                撤销专家任务的车辆作业(masterForm.DisplayManager.CurrentItem as 专家任务);
                任务管理.ResetRowData(masterForm);
            }
        }

        public static void 撤销任务下达(ArchiveOperationForm masterForm)
        {
            if (masterForm.DisplayManager.CurrentItem != null)
            {
                撤销专家任务下达(masterForm.DisplayManager.CurrentItem as 专家任务);
                任务管理.ResetRowData(masterForm);
            }
        }

        private static void 撤销专家任务的车辆作业监控(专家任务 entity)
        {
            if (entity.车辆作业 == null || entity.车辆作业.开始时间 == null) return;

            if (撤销Validation(entity, 撤销专家任务.撤销监控))
            {
                m_车辆作业dao.撤销监控(entity.车辆作业);
            }
        }

        private static void 撤销专家任务的车辆作业(专家任务 entity)
        {
            if (entity.车辆作业 == null) return;

            if (撤销Validation(entity, 撤销专家任务.撤销车辆作业))
            {
                m_车辆作业dao.撤销车辆作业(entity.车辆作业);
            }
        }

        private static void 撤销专家任务下达(专家任务 entity)
        {
            if (!entity.下达时间.HasValue) return;

            if (撤销Validation(entity, 撤销专家任务.撤销任务下达))
            {
                entity.下达时间 = null;
                m_专家任务dao.Update(entity);
            }
        }

        void 专家任务管理_DoubleClick(object sender, EventArgs e)
        {
            Xceed.Grid.Cell srcCell = sender as Xceed.Grid.Cell;
            if (srcCell == null)
                return;

            专家任务 zjrw = srcCell.ParentRow.Tag as 专家任务;
            if (zjrw != null)
            {
                if (srcCell.ParentColumn.FieldName == "撤销监控")
                {
                    撤销专家任务的车辆作业监控(zjrw);
                }
                else if (srcCell.ParentColumn.FieldName == "撤销车辆作业")
                {
                    撤销专家任务的车辆作业(zjrw);
                }
                else if (srcCell.ParentColumn.FieldName == "撤销任务下达")
                {
                    撤销专家任务下达(zjrw);
                }
            }

            btn刷新_Click(btn刷新, EventArgs.Empty);
        }

        enum 撤销专家任务
        {
            撤销监控, 撤销车辆作业, 撤销任务下达
        }

        static bool 撤销Validation(专家任务 zjrw, 撤销专家任务 cx)
        {
            switch (cx)
            {
                case 撤销专家任务.撤销监控:
                    if (zjrw.车辆作业.结束时间 != null)
                    {
                        MessageForm.ShowWarning("专家任务的车辆作业已结束，无法撤销");
                        return false;
                    }
                    break;
                case 撤销专家任务.撤销车辆作业:
                    if (zjrw.车辆作业.开始时间 != null)
                    {
                        MessageForm.ShowWarning("专家任务的车辆作业已开始监控，无法撤销");
                        return false;
                    }
                    break;
                case 撤销专家任务.撤销任务下达:
                    if (zjrw.车辆作业 != null)
                    {
                        MessageForm.ShowWarning("专家任务已安排车辆作业，无法撤销");
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
            m_专家任务集合.DisplayManager.SearchManager.LoadData();
        }
    }
}
