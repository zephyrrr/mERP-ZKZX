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
    public partial class 车队调度主界面 : Feng.Windows.Forms.MyTemplateForm, IRefreshDataForm
    {
        public 车队调度主界面()
        {
            InitializeComponent();
        }

        private IBoundGrid m_新任务集合Grid,m_作业未结束Grid;
        private void 车队调度主界面_Load(object sender, EventArgs e)
        {
            m_新任务集合Grid = base.AssociateBoundGrid(pnl新任务集合, "车队级调度_主界面_新任务集合");
            m_作业未结束Grid = base.AssociateBoundGrid(pnl未结束的作业, "车队级调度_主界面_未结束");
        }

        public void RefreshData()
        {
            m_新任务集合Grid.DisplayManager.SearchManager.LoadData(
                    SearchExpression.Parse("车辆作业 ISNULL"), null);
            m_作业未结束Grid.DisplayManager.SearchManager.LoadData();
            LoadLabelData();
        }
        private void LoadLabelData()
        {
            if (!ServiceProvider.GetService<IRepositoryFactory>().GetType().ToString().Contains("Feng.NH.RepositoryFactory"))
                return;

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<专家任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 专家任务 WHERE 下达时间 IS NOT NULL AND Created >= :Created")
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
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 车辆作业 WHERE IsActive = 1 AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl2.Text = result.ToString();
            });
            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<专家任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 专家任务 WHERE 下达时间 IS NOT NULL AND 车辆作业 IS NULL")
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl3.Text = result.ToString();
            });
            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 车辆作业 WHERE 结束时间 IS NOT NULL AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl4.Text = result.ToString();
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
            Feng.Async.AsyncHelper.Start(() =>
            {
                return (Feng.Data.DbHelper.Instance.ExecuteScalar("SELECT  COUNT(DISTINCT A.作业号) FROM 业务作业_车辆作业 AS A INNER JOIN 业务作业_作业异常情况 AS B ON A.Id = B.车辆作业 WHERE  (B.异常情况 <> '动态任务追加') AND A.结束时间 IS NULL")).ToString();
            }, (result) =>
            {
                lbl14.Text = result.ToString();
            });
        }
    }
}
