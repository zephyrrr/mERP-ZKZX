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
    public partial class 专家级主界面 : MyTemplateForm, IRefreshDataForm
    {
        public 专家级主界面()
        {
            InitializeComponent();
        }

        private DataUnboundGrid m_原始, m_运行中, m_未分派, m_已下达;

        private void 专家级主界面_Load(object sender, EventArgs e)
        {
            m_原始 = base.AssociateBoundGrid(pnl原始, "专家级调度主界面_原始") as DataUnboundGrid;
            m_运行中 = base.AssociateBoundGrid(pnl运行中, "专家级调度主界面_运行中") as DataUnboundGrid;
            m_未分派 = base.AssociateBoundGrid(pnl未分派, "专家级调度主界面_未分派") as DataUnboundGrid;
            m_已下达 = base.AssociateBoundGrid(pnl已下达, "专家级调度主界面_已下达") as DataUnboundGrid;
            Helper.SetGridDefault(this, m_原始);
            Helper.SetGridDefault(this, m_运行中);
            Helper.SetGridDefault(this, m_未分派);
            Helper.SetGridDefault(this, m_已下达);
        }

        public void RefreshData()
        {
            m_原始.DisplayManager.SearchManager.LoadData();
            m_运行中.DisplayManager.SearchManager.LoadData();
            m_未分派.DisplayManager.SearchManager.LoadData();
            m_已下达.DisplayManager.SearchManager.LoadData();

            LoadLabelData();
        }

        private void LoadLabelData()
        {
            if (!ServiceProvider.GetService<IRepositoryFactory>().GetType().ToString().Contains("Feng.NH.RepositoryFactory"))
                return;

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 任务 WHERE Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl11.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 任务 WHERE 任务性质 = 1 AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl12.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 任务 WHERE 任务性质 = 2 AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl13.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 任务 WHERE 任务性质 = 3 AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl14.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 任务 WHERE 任务性质 = 4 AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl15.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 任务 WHERE 任务性质 = 1 AND 是否小箱 =1 AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl16.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 任务 WHERE 任务性质 = 2 AND 是否小箱 =1 AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl17.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 任务 WHERE NOT((任务性质 = 1 OR 任务性质 = 2) AND 是否小箱 =1) AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl18.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<专家任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 专家任务 WHERE Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl21.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<专家任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 专家任务 WHERE 任务性质 >=26 AND 任务性质 <=26 AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl22.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<专家任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 专家任务 WHERE 任务性质 <=6 AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl23.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<专家任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 专家任务 WHERE (任务性质 = 1 OR 任务性质 = 21) AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl24.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<专家任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 专家任务 WHERE (任务性质 = 2 OR 任务性质 = 22) AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl25.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<专家任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 专家任务 WHERE (任务性质 = 3 OR 任务性质 = 23) AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl26.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<专家任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 专家任务 WHERE (任务性质 = 4 OR 任务性质 = 24 OR 任务性质 = 5 OR 任务性质 = 25 OR 任务性质 = 6 OR 任务性质 = 26) AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl27.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 任务 WHERE 专家任务 IS NULL AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl31.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 任务 WHERE 专家任务 IS NULL AND 任务性质 = 1 AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl32.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 任务 WHERE 专家任务 IS NULL AND 任务性质 = 2 AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl33.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 任务 WHERE 专家任务 IS NULL AND 任务性质 = 3 AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl34.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 任务 WHERE 专家任务 IS NULL AND 任务性质 = 4 AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl35.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 任务 WHERE 专家任务 IS NULL AND 任务性质 = 1 AND 是否小箱 =1 AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl36.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 任务 WHERE 专家任务 IS NULL AND 任务性质 = 2 AND 是否小箱 =1 AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl37.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                {
                    return (rep as Feng.NH.INHibernateRepository).Session.CreateQuery("select count(*) from 任务 WHERE 专家任务 IS NULL AND NOT((任务性质 = 1 OR 任务性质 = 2) AND 是否小箱 =1) AND Created >= :Created")
                    .SetDateTime("Created", System.DateTime.Today)
                    .UniqueResult<long>();
                }
            }, (result) =>
            {
                lbl38.Text = result.ToString();
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
                lbl41.Text = result.ToString();
            });


            Feng.Async.AsyncHelper.Start(() =>
            {
                return (Feng.Data.DbHelper.Instance.ExecuteScalar("SELECT  COUNT(DISTINCT A.作业号) FROM 业务作业_车辆作业 AS A INNER JOIN 业务作业_作业异常情况 AS B ON A.Id = B.车辆作业 WHERE  (B.异常情况 <> '动态任务追加') AND A.结束时间 IS NULL")).ToString();
            }, (result) =>
            {
                lbl42.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                return (Feng.Data.DbHelper.Instance.ExecuteScalar("SELECT  COUNT(DISTINCT A.作业号) FROM 业务作业_车辆作业 AS A INNER JOIN 业务作业_作业异常情况 AS B ON A.Id = B.车辆作业 WHERE  (B.异常情况 <> '动态任务追加' AND B.处理状态 = '已处理') AND A.结束时间 IS NULL")).ToString();
            }, (result) =>
            {
                lbl43.Text = result.ToString();
            });

            Feng.Async.AsyncHelper.Start(() =>
            {
                return (Feng.Data.DbHelper.Instance.ExecuteScalar("SELECT  COUNT(DISTINCT A.作业号) FROM 业务作业_车辆作业 AS A INNER JOIN 业务作业_作业异常情况 AS B ON A.Id = B.车辆作业 WHERE  (B.异常情况 <> '动态任务追加' AND B.处理状态 = '待处理') AND A.结束时间 IS NULL")).ToString();
            }, (result) =>
            {
                lbl44.Text = result.ToString();
            });
        }
    }
}
