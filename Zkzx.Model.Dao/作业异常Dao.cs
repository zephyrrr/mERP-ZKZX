using System;
using System.Collections.Generic;
using System.Text;
using Feng;

namespace Zkzx.Model
{
    public class 作业异常Dao : BaseDao<作业异常情况>
    {
        internal void 新作业异常(IRepository rep, 车辆作业 车辆作业, string 异常情况, string 异常参数, DateTime 时间)
        {
            作业异常情况 entity = new 作业异常情况();
            entity.车辆作业 = 车辆作业;
            entity.异常情况 = 异常情况;
            entity.异常参数 = 异常参数;
            entity.处理状态 = "待处理";
            entity.时间 = 时间;
            Update最新状态(车辆作业, entity);

            base.Save(rep, entity);
        }

        public void 新作业异常(车辆作业 车辆作业, string 异常情况, string 异常参数, DateTime 时间)
        {
            using (IRepository rep = this.GenerateRepository())
            {
                try
                {
                    rep.BeginTransaction();

                    新作业异常(rep, 车辆作业, 异常情况, 异常参数, 时间);

                    rep.CommitTransaction();
                }
                catch (Exception)
                {
                    rep.RollbackTransaction();
                    throw;
                }
            }
        }

        public void 处理作业异常(车辆作业 车辆作业, string 处理结果, DateTime? 处理时间, bool 准备处理)
        {
            using (IRepository rep = this.GenerateRepository())
            {
                try
                {
                    作业异常情况 entity = rep.Get<作业异常情况>(车辆作业.最新作业状态.作业异常Id);
                    if (!string.IsNullOrEmpty(处理结果))
                    {
                        entity.处理结果 = 处理结果;
                        entity.处理状态 = "已处理";
                        entity.处理时间 = 处理时间.HasValue ? 处理时间.Value : System.DateTime.Now;
                    }
                    else if (准备处理)
                    {
                        entity.处理状态 = "处理中";
                    }
                    else
                    {
                        entity.处理状态 = "待处理";
                    }

                    rep.BeginTransaction();
                    base.Update(rep, entity);
                    rep.CommitTransaction();

                    Update最新状态(车辆作业, entity);
                }
                catch (Exception)
                {
                    rep.RollbackTransaction();
                    throw;
                }
            }
        }

        private void Update最新状态(车辆作业 clzy, 作业异常情况 entity)
        {
            clzy.最新作业状态.异常情况 = entity.异常情况;
            clzy.最新作业状态.异常参数 = entity.异常参数;
            clzy.最新作业状态.处理状态 = entity.处理状态;
            clzy.最新作业状态.作业异常Id = entity.ID;
        }
    }
}
