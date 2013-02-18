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
using Feng.Data;

namespace Zkzx.Forms
{
    public partial class 实时监控主界面 : MyTemplateForm, IRefreshDataForm
    {
        public 实时监控主界面()
        {
            InitializeComponent();
        }

        private DataUnboundGrid m_未开始作业集合, m_运行中作业集合;

        private void 监控级调度主界面_Load(object sender, EventArgs e)
        {
            m_未开始作业集合 = base.AssociateBoundGrid(pnl未开始, "实时监控_车辆作业_监控级调度主界面_未开始") as DataUnboundGrid;
            m_运行中作业集合 = base.AssociateBoundGrid(pnl运行中, "实时监控_车辆作业_监控级调度主界面_运行中") as DataUnboundGrid;

            Helper.SetGridDefault(this, m_未开始作业集合);
            Helper.SetGridDefault(this, m_运行中作业集合);
        }
        public void RefreshData()
        {
            m_未开始作业集合.DisplayManager.SearchManager.LoadData();
            m_运行中作业集合.DisplayManager.SearchManager.LoadData();

            LoadLabelData();
        }

        private void LoadLabelData()
        {
            if (!ServiceProvider.GetService<IRepositoryFactory>().GetType().ToString().Contains("Feng.NH.RepositoryFactory"))
                return;

            Feng.Async.AsyncHelper.Start(() =>
                {
                    using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
                    {
                        return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 车辆作业 WHERE Created >= :Created")
                        .SetDateTime("Created", System.DateTime.Today)
                        .UniqueResult<long>();
                    }
                }, (result) =>
                {
                    lbl1.Text = result.ToString();
                });
            Feng.Async.AsyncHelper.Start(() =>
                {
                    using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
                    {
                        return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 车辆作业 WHERE 开始时间 IS NOT NULL AND 结束时间 IS NULL")
                        .UniqueResult<long>();
                    }
                }, (result) =>
                {
                    lbl2.Text = result.ToString();
                });
            Feng.Async.AsyncHelper.Start(() =>
                {
                    using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
                    {
                        return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 车辆作业 WHERE 开始时间 IS NULL AND IsActive = 1")
                        .UniqueResult<long>();
                    }
                }, (result) =>
                {
                    lbl3.Text = result.ToString();
                });
            Feng.Async.AsyncHelper.Start(() =>
                {
                    return (Feng.Data.DbHelper.Instance.ExecuteScalar("SELECT  COUNT(DISTINCT A.作业号) FROM 业务作业_车辆作业 AS A INNER JOIN 业务作业_作业异常情况 AS B ON A.Id = B.车辆作业 WHERE  (B.异常情况 = '动态任务追加') AND A.结束时间 IS NULL")).ToString();
                }, (result) =>
                {
                    lbl4.Text = result.ToString();
                });

            Feng.Async.AsyncHelper.Start(() =>
            {
                return (Feng.Data.DbHelper.Instance.ExecuteScalar("SELECT  COUNT(DISTINCT A.作业号) FROM 业务作业_车辆作业 AS A INNER JOIN 业务作业_作业异常情况 AS B ON A.Id = B.车辆作业 WHERE  (B.异常情况 <> '动态任务追加') AND A.结束时间 IS NULL")).ToString();
            }, (result) =>
            {
                lbl5.Text = result.ToString();
            });
            Feng.Async.AsyncHelper.Start(() =>
            {
                return (Feng.Data.DbHelper.Instance.ExecuteScalar("SELECT  COUNT(ID) FROM 参数备案_车辆 WHERE  IsActive = 1")).ToString();
            }, (result) =>
            {
                lbl6.Text = result.ToString();
            });
            Feng.Async.AsyncHelper.Start(() =>
            {
                return (Feng.Data.DbHelper.Instance.ExecuteScalar("SELECT  COUNT(ID) FROM 参数备案_车辆 WHERE  IsActive = 1 AND 当前状态 <> 3")).ToString();
            }, (result) =>
            {
                lbl7.Text = result.ToString();
            });
            Feng.Async.AsyncHelper.Start(() =>
            {
                return (Feng.Data.DbHelper.Instance.ExecuteScalar("SELECT  COUNT(ID) FROM 参数备案_车辆 WHERE  IsActive = 1 AND 当前状态 = 2")).ToString();
            }, (result) =>
            {
                lbl8.Text = result.ToString();
            });
            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 作业异常情况 WHERE Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl9.Text = result.ToString();
            });
            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 作业异常情况 WHERE 处理时间 >= :处理时间")
                   .SetDateTime("处理时间", System.DateTime.Today)
                   .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl10.Text = result.ToString();
            });
            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 车辆作业 WHERE IsActive = false AND 结束时间 IS NULL")
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl11.Text = result.ToString();
            });
            Feng.Async.AsyncHelper.Start(() =>
            {
                return (Feng.Data.DbHelper.Instance.ExecuteScalar("SELECT  COUNT(DISTINCT A.作业号) FROM 业务作业_车辆作业 AS A INNER JOIN 业务作业_作业异常情况 AS B ON A.Id = B.车辆作业 WHERE  (SUBSTRING(B.处理结果,1,8) = '动态任务追加失败') AND B.处理结果 <> '已处理' AND A.结束时间 IS NULL")).ToString();
            }, (result) =>
            {
                lbl12.Text = result.ToString();
            });
            Feng.Async.AsyncHelper.Start(() =>
            {
                return (Feng.Data.DbHelper.Instance.ExecuteScalar("SELECT  COUNT(DISTINCT A.作业号) FROM 业务作业_车辆作业 AS A INNER JOIN 业务作业_作业异常情况 AS B ON A.Id = B.车辆作业 WHERE  (SUBSTRING(B.处理结果,1,8) <> '动态任务追加失败') AND B.处理结果 <> '已处理' AND A.结束时间 IS NULL")).ToString();
            }, (result) =>
            {
                lbl13.Text = result.ToString();
            });
        }
    }
}
