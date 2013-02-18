using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using NHibernate.Mapping.Attributes;
using NHibernate;
using Feng;

namespace Zkzx.Model
{
    public enum 专家任务性质
    {
        [Description("套箱")]
        静态优化套箱 = 1,
        [Description("进口箱带货")]
        静态优化进口箱带货 = 2,
        [Description("出口箱带货")]
        静态优化出口箱带货 = 3,
        [Description("进口箱对箱")]
        静态优化进口箱对箱 = 4,
        [Description("出口箱对箱")]
        静态优化出口箱对箱 = 5,
        [Description("进出口对箱")]
        静态优化进出口对箱 = 6,
        [Description("套箱")]
        动态优化套箱 = 21,
        [Description("进口箱带货")]
        动态优化进口箱带货 = 22,
        [Description("出口箱带货")]
        动态优化出口箱带货 = 23,
        [Description("进口箱对箱")]
        动态优化进口箱对箱 = 24,
        [Description("出口箱对箱")]
        动态优化出口箱对箱 = 25,
        [Description("进出口对箱")]
        动态优化进出口对箱 = 26,
        [Description("进口拆箱")]
        无优化进口拆箱 = 11,
        [Description("出口装箱")]
        无优化出口装箱 = 12,
        [Description("I带货")]
        无优化I带货 = 13,
        [Description("E带货")]
        无优化E带货 = 14
    }

    [Serializable]
    [Class(NameType = typeof(专家任务), Table = "业务备案_专家任务", OptimisticLock = OptimisticLockMode.Version)]
    public class 专家任务 : BaseBOEntity
    {
        [Property(Length = 13, Unique = true, NotNull = true)]
        public virtual string 新任务号
        {
            get;
            set;
        }

        [Property(NotNull = true)]
        public virtual 专家任务性质 任务性质
        {
            get;
            set;
        }

        [Bag(0, Cascade = "none", Inverse = true, OrderBy = "任务性质", Lazy = CollectionLazy.False)]
        [Key(1, Column = "专家任务")]
        [OneToMany(2, ClassType = typeof(任务), NotFound = NotFoundMode.Ignore)]
        public virtual IList<任务> 任务
        {
            get;
            set;
        }

        [Property(Column = "区域", Length = 6, NotNull = false)]
        public virtual string 区域编号
        {
            get;
            set;
        }

        [Property(Length = 500, NotNull = false)]
        public virtual string 备注
        {
            get;
            set;
        }

        [ManyToOne(NotNull = false, ForeignKey = "FK_专家任务_车辆作业")]
        public virtual 车辆作业 车辆作业
        {
            get;
            set;
        }


        public virtual string 起始途径终止地
        {
            get
            {
                //TryInitialize任务();

                int[] taskIdx = null;
                string[] importantAreas = null;
                string[] importantTaskStatus = null;
                string[] importantWorkStatus = null;
                ModelHelper.Get任务状态(this, out taskIdx, out importantAreas, out importantTaskStatus, out importantWorkStatus);

                StringBuilder sb = new StringBuilder();
                foreach (var s in importantAreas)
                {
                    sb.Append(s);
                    sb.Append(",");
                }
                return sb.ToString();
            }
        }

        [Property(Column = "时间要求始", NotNull = false)]
        protected virtual DateTime? 时间要求始Original
        {
            get;
            set;
        }

        public virtual DateTime 时间要求始
        {
            get
            {
                if (!时间要求始Original.HasValue)
                {
                    //if (this.任务 != null && this.任务.Count > 0)
                    //{
                    //    switch (任务性质)
                    //    {
                    //        case 专家任务性质.静态优化进口箱带货:
                    //        case 专家任务性质.动态优化进口箱带货:
                    //        case 专家任务性质.无优化进口拆箱:
                    //            foreach (var i in this.任务)
                    //            {
                    //                if (i.任务性质 == Model.任务性质.进口拆箱)
                    //                    return i.提箱时间要求;
                    //            }
                    //            break;

                    //        case 专家任务性质.静态优化出口箱带货:
                    //        case 专家任务性质.动态优化出口箱带货:
                    //            foreach (var i in this.任务)
                    //            {
                    //                if (i.任务性质 == Model.任务性质.E带货)
                    //                    return i.装货时间要求始;
                    //            }
                    //            break;
                    //        case 专家任务性质.无优化出口装箱:
                    //            foreach (var i in this.任务)
                    //            {
                    //                if (i.任务性质 == Model.任务性质.出口装箱)
                    //                    return i.装货时间要求始;
                    //            }
                    //            break;

                    //        case 专家任务性质.静态优化套箱:
                    //        case 专家任务性质.动态优化套箱:
                    //            foreach (var i in this.任务)
                    //            {
                    //                if (i.任务性质 == Model.任务性质.进口拆箱)
                    //                    return i.提箱时间要求;
                    //            }
                    //            break;
                    //        case 专家任务性质.静态优化进口箱对箱:
                    //            {
                    //                DateTime min = DateTime.MaxValue;
                    //                foreach (var i in this.任务)
                    //                {
                    //                    if (i.任务性质 == Model.任务性质.进口拆箱)
                    //                        min = i.提箱时间要求.HasValue && i.提箱时间要求.Value < min ? i.提箱时间要求.Value : min;
                    //                }
                    //                return min;
                    //            }
                    //        case 专家任务性质.静态优化出口箱对箱:
                    //            {
                    //                DateTime min = DateTime.MaxValue;
                    //                foreach (var i in this.任务)
                    //                {
                    //                    if (i.任务性质 == Model.任务性质.出口装箱)
                    //                        min = i.装货时间要求始.HasValue && i.装货时间要求始.Value < min ? i.装货时间要求始.Value : min;
                    //                }
                    //                return min;
                    //            }
                    //        case 专家任务性质.静态优化进出口对箱:
                    //            {
                    //                foreach (var i in this.任务)
                    //                {
                    //                    if (i.任务性质 == Model.任务性质.进口拆箱)
                    //                        return i.提箱时间要求;
                    //                }
                    //            }
                    //            break;
                    //        case 专家任务性质.无优化I带货:
                    //            return this.任务[0].装货时间要求始;
                    //        case 专家任务性质.无优化E带货:
                    //            return this.任务[0].装货时间要求始;
                    //    }
                    //}
                    //return null;
                    return System.DateTime.Now;
                }
                else
                {
                    return 时间要求始Original.Value;
                }
            }
            set
            {
                时间要求始Original = value;
            }
        }

        [Property(Column = "时间要求止", NotNull = false)]
        protected virtual DateTime? 时间要求止Original
        {
            get;
            set;
        }


        public virtual DateTime 时间要求止
        {
            get
            {
                if (!时间要求止Original.HasValue)
                {
                    //if (this.任务 != null && this.任务.Count > 0)
                    //{
                    //    switch (任务性质)
                    //    {
                    //        case 专家任务性质.静态优化进口箱带货:
                    //        case 专家任务性质.动态优化进口箱带货:
                    //        case 专家任务性质.无优化进口拆箱:
                    //            foreach (var i in this.任务)
                    //            {
                    //                if (i.任务性质 == Model.任务性质.进口拆箱)
                    //                    return i.还箱进港时间要求;
                    //            }
                    //            break;
                    //        case 专家任务性质.静态优化出口箱带货:
                    //        case 专家任务性质.动态优化出口箱带货:
                    //        case 专家任务性质.无优化出口装箱:
                    //            foreach (var i in this.任务)
                    //            {
                    //                if (i.任务性质 == Model.任务性质.出口装箱)
                    //                    return i.还箱进港时间要求;
                    //            }
                    //            break;


                    //        case 专家任务性质.静态优化套箱:
                    //        case 专家任务性质.动态优化套箱:
                    //            foreach (var i in this.任务)
                    //            {
                    //                if (i.任务性质 == Model.任务性质.出口装箱)
                    //                    return i.还箱进港时间要求;
                    //            }
                    //            break;

                    //        case 专家任务性质.静态优化进口箱对箱:
                    //        case 专家任务性质.静态优化出口箱对箱:
                    //        case 专家任务性质.静态优化进出口对箱:
                    //            {
                    //                DateTime max = DateTime.MinValue;
                    //                foreach (var i in this.任务)
                    //                {
                    //                    max = i.还箱进港时间要求.HasValue && i.还箱进港时间要求.Value > max ? i.还箱进港时间要求.Value : max;
                    //                }
                    //                return max;
                    //            }

                    //        case 专家任务性质.无优化I带货:
                    //            return this.任务[0].卸货时间要求止;
                    //        case 专家任务性质.无优化E带货:
                    //            return this.任务[0].卸货时间要求止;
                    //    }
                    //}
                    //return null;
                    return System.DateTime.Now.AddHours(1);
                }
                else
                {
                    return 时间要求止Original.Value;
                }
            }
            set
            {
                时间要求止Original = value;
            }
        }

        [Property(NotNull = true)]
        public virtual int 缓急程度
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual DateTime? 下达时间
        {
            get;
            set;
        }

        public virtual DateTime 委托时间
        {
            get
            {
                //TryInitialize任务();
                DateTime dt = DateTime.MaxValue;
                foreach (var i in 任务)
                {
                    if (i.委托时间.HasValue && dt > i.委托时间.Value)
                    {
                        dt = i.委托时间.Value;
                    }
                }
                return dt;
            }
        }

        public virtual string 箱型编号
        {
            get 
            {
                //TryInitialize任务();

                switch (任务性质)
                {
                    case 专家任务性质.静态优化套箱:
                    case 专家任务性质.动态优化套箱:
                        //foreach (var i in this.任务)
                        //{
                        //    if (i.任务性质 == Model.任务性质.进口拆箱)
                        //        return i.箱型编号.HasValue ? i.箱型编号.Value.ToString() : null;
                        //}
                        {
                            if (!this.任务[0].箱型编号.HasValue && this.任务[1].箱型编号.HasValue)
                                return null;
                            if (!this.任务[0].箱型编号.HasValue)
                                return this.任务[1].箱型编号.Value.ToString();
                            if (!this.任务[1].箱型编号.HasValue)
                                return this.任务[0].箱型编号.Value.ToString();
                            return this.任务[0].箱型编号.Value.ToString() + "," + this.任务[1].箱型编号.Value;
                        }
                    case 专家任务性质.静态优化进口箱带货:
                    case 专家任务性质.动态优化进口箱带货:
                        foreach (var i in this.任务)
                        {
                            if (i.任务性质 == Model.任务性质.进口拆箱)
                                return i.箱型编号.HasValue ? i.箱型编号.Value.ToString() : null;
                        }
                        break;
                    case 专家任务性质.静态优化出口箱带货:
                    case 专家任务性质.动态优化出口箱带货:
                        foreach (var i in this.任务)
                        {
                            if (i.任务性质 == Model.任务性质.出口装箱)
                                return i.箱型编号.HasValue ? i.箱型编号.Value.ToString() : null;
                        }
                        break;
                    case 专家任务性质.静态优化进口箱对箱:
                    case 专家任务性质.静态优化出口箱对箱:
                    case 专家任务性质.静态优化进出口对箱:
                    case 专家任务性质.动态优化进口箱对箱:
                    case 专家任务性质.动态优化出口箱对箱:
                    case 专家任务性质.动态优化进出口对箱:
                        {
                            if (!this.任务[0].箱型编号.HasValue && this.任务[1].箱型编号.HasValue)
                                return null;
                            if (!this.任务[0].箱型编号.HasValue)
                                return this.任务[1].箱型编号.Value.ToString();
                            if (!this.任务[1].箱型编号.HasValue)
                                return this.任务[0].箱型编号.Value.ToString();
                            //if (this.任务[0].箱型编号.Value == this.任务[1].箱型编号.Value)
                            //    return this.任务[0].箱型编号.Value.ToString();
                            return this.任务[0].箱型编号.Value.ToString() + "," + this.任务[1].箱型编号.Value;
                        }
                    case 专家任务性质.无优化进口拆箱:
                    case 专家任务性质.无优化出口装箱:
                    case 专家任务性质.无优化I带货:
                    case 专家任务性质.无优化E带货:
                        return this.任务[0].箱型编号.HasValue ? this.任务[0].箱型编号.Value.ToString() : null;
                }
                return null;
                //List<int> list = new List<int>();
                //if (this.任务 != null && this.任务.Count > 0)
                //{
                //    foreach (任务 rw in this.任务)
                //    {
                //        if (rw.箱型编号.HasValue)
                //        {
                //            if (!list.Contains(rw.箱型编号.Value))
                //            {
                //                list.Add(rw.箱型编号.Value);
                //            }
                //        }
                //    }
                //    StringBuilder sb = new StringBuilder();
                //    for (int i = 0; i < list.Count; ++i)
                //    {
                //        sb.Append(list[i].ToString());
                //        sb.Append(",");
                //    }
                //    return sb.ToString();
                //}
                //return null;
            }
        }

        public virtual 转关箱标志 转关箱标志
        {
            get 
            {
                //TryInitialize任务();
                转关箱标志 zgx = 转关箱标志.非转关;
                if (任务 != null && 任务.Count > 0)
                {
                    foreach (任务 rw in 任务)
                    {
                        if (rw.转关箱标志 == 转关箱标志.转关)
                        {
                            zgx = 转关箱标志.转关;
                            break;
                        }
                    }
                }
                return zgx;
            }
        }

        //private void TryInitialize任务()
        //{
        //    try
        //    {
        //        foreach (var i in this.任务)
        //        {
        //            var s = i.ToString();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        if (!NHibernateUtil.IsInitialized(this.任务))
        //        {
        //            using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<专家任务>())
        //            {
        //                rep.Attach(this);
        //                foreach (var i in this.任务)
        //                {
        //                    var s = i.ToString();
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
