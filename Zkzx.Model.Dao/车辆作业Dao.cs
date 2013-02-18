using System;
using System.Collections.Generic;
using System.Text;
using Feng;
using Feng.Data;

namespace Zkzx.Model
{
    public class 车辆作业Dao : BaseDao<车辆作业>
    {
        public string 生成作业号(string 专家任务号)
        {
            //string zyh = "ZY" + PrimaryMaxIdGenerator.GetIdYearMonth();
            //zyh = PrimaryMaxIdGenerator.GetMaxId("业务作业_车辆作业", "作业号", 13, zyh);
            return "Z" + 专家任务号;
        }

        /// <summary>
        /// 保存并返回车辆作业
        /// </summary>
        /// <param name="cl"></param>
        /// <param name="zjrw"></param>
        /// <param name="bz"></param>
        /// <returns></returns>
        public 车辆作业 生成车辆作业(车辆 cl, 专家任务 zjrw, string bz)
        {
            if (cl == null || zjrw == null)
                return null;

            using (IRepository rep = this.GenerateRepository())
            {
                try
                {
                    rep.BeginTransaction();

                    车辆作业 clzy = new 车辆作业();
                    clzy.车辆 = cl;
                    clzy.专家任务 = zjrw;
                    clzy.IsActive = true;
                    clzy.作业号 = 生成作业号(zjrw.新任务号);
                    clzy.备注 = bz;
                    base.Save(rep, clzy);

                    zjrw.车辆作业 = clzy;
                    m_专家任务dao.Update(rep, zjrw);

                    作业监控状态 entity = new 作业监控状态();
                    entity.IsActive = true;
                    entity.车辆作业 = clzy;
                    entity.时间 = System.DateTime.Now;
                    作业监控Dao.更新作业监控状态(rep, entity, clzy.最新作业状态, null, null);
                    //var route = m_作业监控Dao.GetDefaultRoute(clzy);
                    //if (route != null && route.Time.HasValue)
                    //{
                    //    entity.预计到达时间 = System.DateTime.Now.AddMinutes(route.Time.Value);
                    //}
                    m_作业监控Dao.Save(rep, entity);

                    rep.CommitTransaction();

                    return clzy;
                }
                catch (Exception)
                {
                    rep.RollbackTransaction();
                    throw;
                }
            }
        }

        private static 专家任务Dao m_专家任务dao = new 专家任务Dao();
        //private 专家任务Dao 专家任务Dao
        //{
        //    get
        //    {
        //        if (m_专家任务dao == null)
        //        {
        //            m_专家任务dao = new 专家任务Dao();
        //        }
        //        return m_专家任务dao;
        //    }
        //}
        /// <summary>
        /// 专家任务.车辆作业 = null，Delete(车辆作业)
        /// </summary>
        /// <param name="clzy"></param>
        public void 撤销车辆作业(车辆作业 clzy)
        {
            if (clzy == null)
                return;

            using (IRepository rep = this.GenerateRepository())
            {
                try
                {
                    rep.BeginTransaction();

                    专家任务 zjrw = clzy.专家任务;
                    zjrw.车辆作业 = null;
                    m_专家任务dao.Update(rep, zjrw);
                    base.Delete(rep, clzy);

                    rep.CommitTransaction();
                }
                catch (Exception)
                {
                    rep.RollbackTransaction();
                    throw;
                }
            }
        }

        private 作业监控Dao m_作业监控Dao = new 作业监控Dao();
        private 作业异常Dao m_作业异常Dao = new 作业异常Dao();
        /// <summary>
        /// 车辆作业.开始时间 = null，Delete(作业监控状态)，Delete(作业异常情况)
        /// </summary>
        /// <param name="clzy"></param>
        public void 撤销监控(车辆作业 clzy)
        {
            if (clzy == null)
                return;

            using (IRepository rep = this.GenerateRepository())
            {
                try
                {
                    rep.BeginTransaction();
                    rep.Attach(clzy);

                    foreach (作业监控状态 i in clzy.作业监控状态)
                    {
                        m_作业监控Dao.Delete(rep, i);
                    }
                    foreach (作业异常情况 i in clzy.作业异常情况)
                    {
                        m_作业异常Dao.Delete(rep, i);
                    }
                    foreach (动作时间数据 i in clzy.动作时间数据)
                    {
                        rep.Delete(i);
                    }
                    clzy.开始时间 = null;
                    clzy.结束时间 = null;
                    rep.CommitTransaction();
                }
                catch (Exception)
                {
                    rep.RollbackTransaction();
                    throw;
                }
            }
        }

        public void 更换车辆作业(车辆作业 clzy, 车辆 cl)
        {
            if (clzy == null || cl == null)
                return;

            clzy.车辆 = cl;
            Update(clzy);
        }

        public void 更换车辆作业(车辆作业 clzy1, 车辆作业 clzy2)
        {
            if (clzy1 == null || clzy2 == null)
                return;

            车辆 cl = clzy1.车辆;
            clzy1.车辆 = clzy2.车辆;
            clzy2.车辆 = cl;
            Update(clzy1);
            Update(clzy2);
        }
    }
}
