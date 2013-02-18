using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Feng;
using Feng.Data;

namespace Zkzx.Model
{
    public class 专家任务Dao : BaseDao<专家任务>
    {
        private 任务Dao m_任务Dao = new 任务Dao();

        public bool 是否能组合(任务 x, 任务 y, 专家任务性质 zjrwxz)
        {
            switch (zjrwxz)
            {
                case 专家任务性质.静态优化套箱:
                case 专家任务性质.动态优化套箱:
                    return x.箱属船公司编号 == y.箱属船公司编号 && x.箱型编号 == y.箱型编号
                        && ((x.任务性质 == 任务性质.出口装箱 && y.任务性质 == 任务性质.进口拆箱)
                        || (x.任务性质 == 任务性质.进口拆箱 && y.任务性质 == 任务性质.出口装箱));
                case 专家任务性质.静态优化出口箱带货:
                case 专家任务性质.动态优化出口箱带货:
                    return (x.任务性质 == 任务性质.出口装箱 && y.任务性质 == 任务性质.E带货)
                        || (x.任务性质 == 任务性质.E带货 && y.任务性质 == 任务性质.出口装箱);
                case 专家任务性质.静态优化进口箱带货:
                case 专家任务性质.动态优化进口箱带货:
                    return (x.任务性质 == 任务性质.进口拆箱 && y.任务性质 == 任务性质.I带货)
                        || (x.任务性质 == 任务性质.I带货 && y.任务性质 == 任务性质.进口拆箱);
                case 专家任务性质.静态优化进口箱对箱:
                case 专家任务性质.动态优化进口箱对箱:
                    return x.是否小箱 && y.是否小箱 
                        && ((x.任务性质 == 任务性质.进口拆箱 && y.任务性质 == 任务性质.出口装箱)
                            || (y.任务性质 == 任务性质.进口拆箱 && x.任务性质 == 任务性质.出口装箱));
                case 专家任务性质.静态优化出口箱对箱:
                case 专家任务性质.动态优化出口箱对箱:
                    return x.是否小箱 && y.是否小箱
                        && (x.任务性质 == 任务性质.出口装箱 && y.任务性质 == 任务性质.出口装箱);
                case 专家任务性质.静态优化进出口对箱:
                case 专家任务性质.动态优化进出口对箱:
                    return x.是否小箱 && y.是否小箱
                        && (x.任务性质 == 任务性质.进口拆箱 && y.任务性质 == 任务性质.进口拆箱);
            }
            return false;
        }

        public bool 撤销专家任务(专家任务 zjrw)
        {
            if (zjrw == null || zjrw.任务 == null || zjrw.任务.Count == 0)
                return true;

            if (zjrw.任务.Count == 1)
            {
                return 撤销专家任务(zjrw.任务[0]);
            }
            else if (zjrw.任务.Count == 2)
            {
                return 撤销专家任务(zjrw.任务[0], zjrw.任务[1]);
            }
            else
            {
                throw new InvalidUserOperationException("任务数量过多，无法撤销");
            }
        }

        public bool 撤销专家任务(string 任务号)
        {
            IList<任务> rwList = null;
            using (IRepository rep = m_任务Dao.GenerateRepository())
            {
                rwList = rep.List<任务>("from 任务 where 任务号 = '" + 任务号 + "'", null);            
            }
            if (rwList == null || rwList.Count != 1)
                return false;
            else
                return 撤销专家任务(rwList[0]);
        }

        public bool 撤销专家任务(任务 rw)
        {
            return 撤销专家任务(rw, null);
        }

        /// <summary>
        /// 已下达的专家任务，将无法删除和移出任务
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool 撤销专家任务(任务 x, 任务 y)
        {
            专家任务 zjrw = null;
            if (x != null && y != null)
            {
                if (x.专家任务 == null || y.专家任务 == null)
                    return true;

                using (IRepository rep = this.GenerateRepository())
                {
                    rep.Attach(x);
                    rep.Attach(y);

                    zjrw = x.专家任务;
                    if (zjrw == null)   // 到这zjrw = null，可能因为多人操作，别人删除了专家任务
                        return true;
                    if (zjrw.下达时间.HasValue)
                        return false;
                    try
                    {
                        rep.BeginTransaction();                        
                        x.专家任务 = null;
                        m_任务Dao.Update(rep, x);
                        y.专家任务 = null;
                        m_任务Dao.Update(rep, y);
                        // 动态调度时，只撤销rw                    
                        if (!zjrw.下达时间.HasValue)
                        {
                            base.Delete(rep, zjrw);
                        }
                        rep.CommitTransaction();                        
                    }
                    catch (Exception ex)
                    {
                        rep.RollbackTransaction();
                        x.专家任务 = zjrw;
                        y.专家任务 = zjrw;
                        ExceptionProcess.ProcessWithNotify(ex);
                        return false;
                    }
                }
                return true;
            }
            else
            {
                任务 rw = x != null ? x : y;
                if (rw != null)
                {
                    if (rw.专家任务 == null)
                        return true;

                    using (IRepository rep = this.GenerateRepository())
                    {
                        try
                        {
                            rep.Attach(rw);
                            rep.BeginTransaction();
                            zjrw = rw.专家任务;
                            if (zjrw == null)   // 到这zjrw = null，可能因为多人操作，别人删除了专家任务
                                return true;
                            if (zjrw.下达时间.HasValue)
                                return false;
                            rw.专家任务 = null;
                            m_任务Dao.Update(rep, rw);
                            bool deleteZjrw = true;
                            foreach (任务 rwItem in zjrw.任务)
                            {
                                if (rwItem.ID != rw.ID)
                                {
                                    zjrw.任务性质 = Get专家任务性质(rwItem);
                                    base.Update(rep, zjrw);
                                    deleteZjrw = false;
                                }
                            }
                            // 动态调度时，只撤销rw
                            // 如果原有2个任务，会报约束异常
                            if (!zjrw.下达时间.HasValue && deleteZjrw)
                            {
                                base.Delete(rep, zjrw);
                            }
                            rep.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            rep.RollbackTransaction();
                            rw.专家任务 = zjrw;
                            ExceptionProcess.ProcessWithNotify(ex);
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public 专家任务 生成专家任务(任务 x, 任务 y, 专家任务性质? xz, string bz)
        {
            using (IRepository rep = this.GenerateRepository())
            {
                return 生成专家任务(x, y, false, xz, bz, rep);
            }
        }

        private 车辆作业Dao m_车辆作业Dao = new 车辆作业Dao();
        private 作业异常Dao m_作业异常Dao = new 作业异常Dao();
        private 作业监控Dao m_作业监控Dao = new 作业监控Dao();

        public 专家任务 撤销专家任务(专家任务 x, 任务 y)
        {
            using (IRepository rep = this.GenerateRepository())
            {
                try
                {
                    rep.BeginTransaction();
                    rep.Attach(x);
                    foreach (var i in x.任务)
                    {
                        if (i.任务号 != y.任务号)
                        {
                            x.新任务号 = "O" + i.任务号;
                            x.任务性质 = Get专家任务性质(i);
                            rep.Update(x);

                            if (x.车辆作业 != null)
                            {
                                x.车辆作业.作业号 = m_车辆作业Dao.生成作业号(x.新任务号);
                                rep.Update(x.车辆作业);
                            }
                        }
                        else
                        {
                            i.专家任务 = null;
                            rep.Update(i);
                        }
                    }

                    rep.CommitTransaction();
                    x.任务.Remove(y);
                }
                catch (Exception)
                {
                    rep.RollbackTransaction();
                    throw;
                }
            }
            return x;
        }
        public 专家任务 生成专家任务(专家任务 x, 任务 y, 专家任务性质? xz)
        {
            using (IRepository rep = this.GenerateRepository())
            {
                System.Diagnostics.Debug.Assert(x.任务.Count == 1, "动态调度的原始专家任务任务数必须为1");

                try
                {
                    rep.Attach(x);

                    专家任务 zjrw = 生成专家任务(x.任务[0], y, true, xz, x.备注, rep);
                    if (zjrw != null)
                    {
                        if (x.车辆作业 != null)
                        {
                            x.车辆作业.作业号 = m_车辆作业Dao.生成作业号(zjrw.新任务号);

                            rep.BeginTransaction();
                            m_车辆作业Dao.Update(rep, x.车辆作业);

                            m_作业异常Dao.新作业异常(x.车辆作业, "动态任务追加", y.任务号, System.DateTime.Now);

                            m_作业监控Dao.更新作业监控状态2(rep, x.车辆作业, System.DateTime.Now, null, null);

                            rep.CommitTransaction();
                        }
                    }
                    return zjrw;
                }
                catch (Exception)
                {
                    rep.RollbackTransaction();
                    throw;
                }
            }
        }

        /// <summary>
        /// 返回新任务号
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="是否动态">是否动态优化</param>
        /// <returns></returns>
        public 专家任务 生成专家任务(任务 x, 任务 y, bool 是否动态, 专家任务性质? xz, string bz, IRepository rep)
        {
            if ((x == null && y == null) || (x != null && y != null && x.专家任务 != null && y.专家任务 != null))
            {
                throw new NullReferenceException("不能生成专家任务！");
            }

            bool bx = false, by = false;

            try
            {
                rep.BeginTransaction();

                专家任务 zjrw = null;
                if (x != null && y != null)
                {
                    // 动态
                    if (x.专家任务 != null || y.专家任务 != null)
                    {
                        zjrw = x.专家任务 != null ? x.专家任务 : y.专家任务;
                    }
                    else
                    {
                        zjrw = new 专家任务();
                        zjrw.任务 = new List<任务>();
                        zjrw.IsActive = true;
                        zjrw.备注 = bz;
                        zjrw.缓急程度 = Math.Max(x.缓急程度, y.缓急程度);
                    }

                    zjrw.任务性质 = Get专家任务性质(x, y, xz, 是否动态);

                    if (zjrw.任务性质 == 专家任务性质.静态优化套箱
                        || zjrw.任务性质 == 专家任务性质.动态优化套箱)
                    {
                        if (x.任务性质 == 任务性质.出口装箱)
                        {
                            if (string.IsNullOrEmpty(x.箱号))
                            {
                                x.箱号 = y.箱号;
                            }
                        }
                        else if (y.任务性质 == 任务性质.出口装箱)
                        {
                            if (string.IsNullOrEmpty(y.箱号))
                            {
                                y.箱号 = x.箱号;
                            }
                        }
                    }
                    switch (zjrw.任务性质)
                    {
                        case 专家任务性质.静态优化套箱:
                            zjrw.新任务号 = "NT";
                            break;
                        case 专家任务性质.动态优化套箱:
                            zjrw.新任务号 = "MT";
                            break;
                        case 专家任务性质.静态优化进口箱带货:
                            zjrw.新任务号 = "NI";
                            break;
                        case 专家任务性质.动态优化进口箱带货:
                            zjrw.新任务号 = "MI";
                            break;
                        case 专家任务性质.静态优化出口箱带货:
                            zjrw.新任务号 = "NE";
                            break;
                        case 专家任务性质.动态优化出口箱带货:
                            zjrw.新任务号 = "ME";
                            break;
                        case 专家任务性质.静态优化进出口对箱:
                            zjrw.新任务号 = "NB";
                            break;
                        case 专家任务性质.动态优化进出口对箱:
                            zjrw.新任务号 = "MB";
                            break;
                        case 专家任务性质.静态优化进口箱对箱:
                            zjrw.新任务号 = "NS";
                            break;
                        case 专家任务性质.动态优化进口箱对箱:
                            zjrw.新任务号 = "MS";
                            break;
                        case 专家任务性质.静态优化出口箱对箱:
                            zjrw.新任务号 = "NA";
                            break;
                        case 专家任务性质.动态优化出口箱对箱:
                            zjrw.新任务号 = "MA";
                            break;
                    }
                    //zjrw.新任务号 += PrimaryMaxIdGenerator.GetIdYearMonth();
                    //zjrw.新任务号 = PrimaryMaxIdGenerator.GetMaxId("业务备案_专家任务", "新任务号", 12, zjrw.新任务号);

                    string s = null;
                    switch (zjrw.任务性质)
                    {
                        case 专家任务性质.静态优化套箱:
                        case 专家任务性质.静态优化进口箱带货:
                        case 专家任务性质.静态优化进口箱对箱:
                        case 专家任务性质.静态优化进出口对箱:
                        case 专家任务性质.动态优化套箱:
                        case 专家任务性质.动态优化进口箱带货:
                        case 专家任务性质.动态优化进口箱对箱:
                        case 专家任务性质.动态优化进出口对箱:
                            s = x.任务性质 == 任务性质.进口拆箱 ? x.任务号 : y.任务号;
                            break;
                        case 专家任务性质.静态优化出口箱带货:
                        case 专家任务性质.静态优化出口箱对箱:
                        case 专家任务性质.动态优化出口箱带货:
                        case 专家任务性质.动态优化出口箱对箱:
                            s = x.任务性质 == 任务性质.出口装箱 ? x.任务号 : y.任务号;
                            break;
                        default:
                            throw new ArgumentException("不合理的专家任务性质！");

                    }
                    zjrw.新任务号 += s.Substring(1);
                }
                else // 无优化专家任务
                {
                    任务 rw = x != null ? x : y;
                    if (rw.专家任务 == null)
                    {
                        zjrw = new 专家任务();
                        zjrw.任务 = new List<任务>();
                        zjrw.IsActive = true;
                        zjrw.备注 = bz;
                        zjrw.缓急程度 = rw.缓急程度;
                    }
                    else
                    {
                        zjrw = rw.专家任务;
                    }
                    zjrw.新任务号 = "O" + rw.任务号;
                    zjrw.任务性质 = Get专家任务性质(rw);
                }

                base.SaveOrUpdate(rep, zjrw);

                if (x != null && x.专家任务 == null)
                {
                    bx = true;
                    x.专家任务 = zjrw;
                    m_任务Dao.Update(rep, x);
                }
                if (y != null && y.专家任务 == null)
                {
                    by = true;
                    y.专家任务 = zjrw;
                    m_任务Dao.Update(rep, y);
                }

                rep.CommitTransaction();

                if (bx)
                {
                    zjrw.任务.Add(x);
                }
                if (by)
                {
                    zjrw.任务.Add(y);
                }

                return zjrw;
            }
            catch (Exception ex)
            {
                rep.RollbackTransaction();
                if (bx)
                {
                    x.专家任务 = null;
                }
                if (by)
                {
                    y.专家任务 = null;
                }
                ExceptionProcess.ProcessWithNotify(ex);
                return null;
            }
        }

        public 专家任务性质 Get专家任务性质(任务 rw)
        {
            return Get专家任务性质(rw, null, null, false);
        }

        public 专家任务性质 Get专家任务性质(任务 x, 任务 y, 专家任务性质? xz, bool 是否动态)
        {
            if (x == null && y == null)
                throw new NullReferenceException("两个任务参数都为空");

            if (xz.HasValue)
                return xz.Value;

            if (x != null && y != null)
            {
                //if (x.是否小箱 && y.是否小箱)
                //{
                //    if (x.任务性质 == 任务性质.出口装箱 && y.任务性质 == 任务性质.进口拆箱
                //        || x.任务性质 == 任务性质.进口拆箱 && y.任务性质 == 任务性质.出口装箱)
                //        return 专家任务性质.静态优化进出口对箱;
                //    else if (x.任务性质 == 任务性质.进口拆箱 && y.任务性质 == 任务性质.进口拆箱)
                //        return 专家任务性质.静态优化进口箱对箱;
                //    else if (x.任务性质 == 任务性质.出口装箱 && y.任务性质 == 任务性质.出口装箱)
                //        return 专家任务性质.静态优化出口箱对箱;
                //    else
                //        throw new ArgumentException("小箱配对任务性质不符合要求！");
                //}
                if (x.任务性质 == 任务性质.出口装箱 && y.任务性质 == 任务性质.进口拆箱
                    || x.任务性质 == 任务性质.进口拆箱 && y.任务性质 == 任务性质.出口装箱)
                {
                    if (x.是否小箱 && y.是否小箱)
                        return 是否动态 ? 专家任务性质.动态优化进出口对箱 : 专家任务性质.静态优化进出口对箱;
                    else
                        return 是否动态 ? 专家任务性质.动态优化套箱 : 专家任务性质.静态优化套箱;
                }
                else if (x.任务性质 == 任务性质.进口拆箱 && y.任务性质 == 任务性质.I带货
                    || x.任务性质 == 任务性质.I带货 && y.任务性质 == 任务性质.进口拆箱)
                {
                    return 是否动态 ? 专家任务性质.动态优化进口箱带货 : 专家任务性质.静态优化进口箱带货;
                }
                else if (x.任务性质 == 任务性质.出口装箱 && y.任务性质 == 任务性质.E带货
                    || x.任务性质 == 任务性质.E带货 && y.任务性质 == 任务性质.出口装箱)
                {
                    return 是否动态 ? 专家任务性质.动态优化出口箱带货 : 专家任务性质.静态优化出口箱带货;
                }
                else if (x.任务性质 == 任务性质.进口拆箱 && y.任务性质 == 任务性质.进口拆箱)
                {
                    if (x.是否小箱 && y.是否小箱)
                    {
                        return 是否动态 ? 专家任务性质.动态优化进口箱对箱 : 专家任务性质.静态优化进口箱对箱;
                    }
                }
                else if (x.任务性质 == 任务性质.出口装箱 && y.任务性质 == 任务性质.出口装箱)
                {
                    if (x.是否小箱 && y.是否小箱)
                    {
                        return 是否动态 ? 专家任务性质.动态优化出口箱对箱 : 专家任务性质.静态优化出口箱对箱;
                    }
                }
            }
            else
            {
                任务 rw = x != null ? x : y;
                switch (rw.任务性质)
                {
                    case 任务性质.进口拆箱:
                        return 专家任务性质.无优化进口拆箱;
                    case 任务性质.出口装箱:
                        return 专家任务性质.无优化出口装箱;
                    case 任务性质.I带货:
                        return 专家任务性质.无优化I带货;
                    case 任务性质.E带货:
                        return 专家任务性质.无优化E带货;
                }
            }

            throw new ArgumentException("两个任务不符合专家任务配对规则");
        }

        public bool 下达专家任务(专家任务 zjrw, DateTime 下达时间)
        {
            try
            {
                zjrw.下达时间 = 下达时间;
                base.Update(zjrw);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionProcess.ProcessWithNotify(ex);
                return false;
            }
        }

        private 任务Dao m_任务dao = new 任务Dao();
        public bool 下达专家任务(任务 rw, DateTime? 时间要求始, DateTime? 时间要求止, string 区域编号, int? 缓急程度, DateTime 下达时间)
        {
            using (IRepository rep = base.GenerateRepository())
            {
                try
                {
                    rep.BeginTransaction();
                    专家任务 zjrw = new 专家任务();
                    zjrw.新任务号 = "O" + rw.任务号;
                    zjrw.IsActive = true;
                    zjrw.时间要求始 = 时间要求始.HasValue ? 时间要求始.Value : zjrw.时间要求始;
                    zjrw.时间要求止 = 时间要求止.HasValue ? 时间要求止.Value : zjrw.时间要求止;
                    zjrw.区域编号 = 区域编号;
                    zjrw.缓急程度 = 缓急程度.HasValue ? 缓急程度.Value : 0;
                    zjrw.下达时间 = 下达时间;
                    zjrw.任务性质 = Get专家任务性质(rw);
                    base.Save(rep, zjrw);

                    rw.专家任务 = zjrw;
                    m_任务dao.Update(rep, rw);
                    rep.CommitTransaction();

                    return true;
                }
                catch (Exception ex)
                {
                    rep.RollbackTransaction();
                    rw.专家任务 = null;
                    ExceptionProcess.ProcessWithNotify(ex);

                    return false;
                }
            }
        }
    }
}
