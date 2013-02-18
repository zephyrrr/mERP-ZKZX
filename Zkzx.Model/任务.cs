using System;
using Feng;

#if SILVERLIGHT
#else
using NHibernate.Mapping.Attributes;
#endif

namespace Zkzx.Model
{
    public enum 任务性质
    {
        进口拆箱 = 1,
        出口装箱 = 2,
        I带货 = 3,
        E带货 = 4
    }

    public enum 转关箱标志
    {
        转关 = 1,
        非转关 = 2
    }

    public enum 任务来源
    {
        手工 = 1,
        网上 = 2,
        文件 = 3
    }

    /// <summary>
    /// 任务。
    /// 流程：空记录-预备案（IsActive）-备案（任务号）-静态优化（专家任务）-下达（是否已下达）-车辆作业（结束时间）
    /// </summary>
#if SILVERLIGHT
#else
    [Serializable]
    [Class(NameType = typeof(任务), Table = "业务备案_任务", OptimisticLock = OptimisticLockMode.Version)]
#endif
    public class 任务 : BaseBOEntity,
         IDetailEntity<进口票, 任务>
    {
        #region "Interface"
        进口票 IDetailEntity<进口票, 任务>.MasterEntity
        {
            get { return 票; }
            set { 票 = value; }
        }
        #endregion

#if SILVERLIGHT
#else
        [ManyToOne(NotNull = false, ForeignKey = "FK_任务_票", Column = "票", Cascade = "none")]
#endif
        public virtual 进口票 票
        {
            get;
            set;
        }


        //#if SILVERLIGHT
        //        [Key]
        //        [ReadOnly(true)]
        //#else
        //        [Id(0, Name = "Id", Column = "Id")]
        //        [Generator(1, Class = "guid.comb")]
        //#endif
        //        public virtual Guid Id
        //        {
        //            get;
        //            set;
        //        }

#if SILVERLIGHT
        [Required()]
        public virtual int 任务来源
        {
            get;
            set;
        }
#else
        [Property(NotNull = true)]
        public virtual 任务来源 任务来源
        {
            get;
            set;
        }
#endif

#if SILVERLIGHT
#else
        [Property(NotNull = false, Length = 12)]
#endif
        public virtual string 预录入号
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(Length = 12)]
#endif
        public virtual string 任务号
        {
            get;
            set;
        }

#if SILVERLIGHT
        [Required()]
        public virtual int 任务性质
        {
            get;
            set;
        }
#else
        [Property(NotNull = false)]
        public virtual 任务性质 任务性质
        {
            get;
            set;
        }
#endif


#if SILVERLIGHT
#else
        [Property(NotNull = false)]
#endif
        public virtual bool 是否小箱
        {
            get;
            set;
        }

#if SILVERLIGHT
        [Required()]
        public virtual int 转关箱标志
        {
            get;
            set;
        }
#else
        [Property(NotNull = false)]
        public virtual 转关箱标志? 转关箱标志
        {
            get;
            set;
        }
#endif


#if SILVERLIGHT
        [Required()]
#else
        [ManyToOne(NotNull = false, Insert = false, Update = false, ForeignKey = "FK_任务_委托人")]
        public virtual 人员 委托人
        {
            get;
            set;
        }

        [Property(Column = "委托人", Length = 6, NotNull = false)]
#endif
        public virtual string 委托人编号
        {
            get;
            set;
        }

#if SILVERLIGHT
        [Required()]
#else
        [Property(NotNull = false)]
#endif
        public virtual DateTime? 委托时间
        {
            get;
            set;
        }

#if SILVERLIGHT
        [Required()]
#else
        [Property(Length = 255)]
#endif
        public virtual string 委托联系人
        {
            get;
            set;
        }

#if SILVERLIGHT
        [Required()]
#else
        [Property(Length = 12, NotNull = false, Index = "Idx_任务_箱号")]
#endif
        public virtual string 箱号
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [ManyToOne(Insert = false, Update = false, NotNull = false, ForeignKey = "FK_任务_箱型")]
        public virtual 箱型 箱型
        {
            get;
            set;
        }

        [Property(Column = "箱型", NotNull = false)]
#endif
        public virtual int? 箱型编号
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [ManyToOne(NotNull = false, Insert = false, Update = false, ForeignKey = "FK_任务_箱属船公司")]
        public virtual 人员 箱属船公司
        {
            get;
            set;
        }

        [Property(Column = "箱属船公司", Length = 6, NotNull = false)]
#endif
        public virtual string 箱属船公司编号
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false, Length = 50)]
#endif
        public virtual string 封志号
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false, Length = 50)]
#endif
        public virtual string 货名
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false, Length = 50)]
#endif
        public virtual string 货物特征
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false)]
#endif
        public virtual decimal? 价值
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false)]
#endif
        public virtual double? 重量
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [ManyToOne(NotNull = false, Insert = false, Update = false, ForeignKey = "FK_任务_提箱点")]
        public virtual 人员 提箱点
        {
            get;
            set;
        }

        [Property(Column = "提箱点", Length = 6, NotNull = false)]
#endif
        public virtual string 提箱点编号
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false)]
#endif
        public virtual DateTime? 提箱时间要求
        {
            get;
            set;
        }

//#if SILVERLIGHT
//#else
//        [Property(NotNull = false)]
//#endif
//        public virtual DateTime? 提箱时间要求止
//        {
//            get;
//            set;
//        }

#if SILVERLIGHT
#else
        [ManyToOne(NotNull = false, Insert = false, Update = false, ForeignKey = "FK_任务_还箱进港点")]
        public virtual 人员 还箱进港点
        {
            get;
            set;
        }

        [Property(Column = "还箱进港点", Length = 6, NotNull = false)]
#endif
        public virtual string 还箱进港点编号
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false)]
#endif
        public virtual DateTime? 还箱进港时间要求
        {
            get;
            set;
        }

//#if SILVERLIGHT
//#else
//        [Property(NotNull = false)]
//#endif
//        public virtual DateTime? 还箱进港时间要求止
//        {
//            get;
//            set;
//        }

#if SILVERLIGHT
#else
        //[ManyToOne(NotNull = false, Insert = false, Update = false, ForeignKey = "FK_任务_装货地")]
        //public virtual 人员 装货地
        //{
        //    get;
        //    set;
        //}

        [Property(Column = "装货地", Length = 6, NotNull = false)]
#endif
        public virtual string 装货地编号
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(Column = "装货地详细地址", Length = 255, NotNull = false)]
#endif
        public virtual string 装货地详细地址
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false)]
#endif
        public virtual DateTime? 装货时间要求始
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false)]
#endif
        public virtual DateTime? 装货时间要求止
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false, Length = 50)]
#endif
        public virtual string 装货联系人
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false, Length = 50)]
#endif
        public virtual string 装货联系座机
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false, Length = 50)]
#endif
        public virtual string 装货联系手机
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        //[ManyToOne(NotNull = false, Insert = false, Update = false, ForeignKey = "FK_任务_卸货地")]
        //public virtual 人员 卸货地
        //{
        //    get;
        //    set;
        //}

        [Property(Column = "卸货地", Length = 6, NotNull = false)]
#endif
        public virtual string 卸货地编号
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(Column = "卸货地详细地址", Length = 255, NotNull = false)]
#endif
        public virtual string 卸货地详细地址
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false)]
#endif
        public virtual DateTime? 卸货时间要求始
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false)]
#endif
        public virtual DateTime? 卸货时间要求止
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false, Length = 50)]
#endif
        public virtual string 卸货联系人
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        // // ^((\d{3,4})?-?(\d{7,8})-(\d{1,4}))$|^((\d{3,4})?-?(\d{7,8}))$
        [Property(NotNull = false, Length = 50)]
#endif
        public virtual string 卸货联系座机
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false, Length = 50)]
#endif
        public virtual string 卸货联系手机
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false, Length = 255)]
#endif
        public virtual string 备注
        {
            get;
            set;
        }


        public virtual string 船名航次
        {
            get
            {
                return (string.IsNullOrEmpty(船名) ? string.Empty : 船名.ToString())
                    + '/'
                    + (string.IsNullOrEmpty(航次) ? string.Empty : 航次.ToString());
            }
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false, Length = 50)]
#endif
        public virtual string 船名
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false, Length = 50)]
#endif
        public virtual string 航次
        {
            get;
            set;
        }


#if SILVERLIGHT
#else
        [Property(NotNull = false, Length = 12)]
#endif
        public virtual string 提示性箱号
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false, Length = 50)]
#endif
        public virtual string 提单号
        {
            get;
            set;
        }


//#if SILVERLIGHT
//#else
//        [Property(NotNull = false)]
//#endif
//        public virtual bool? 是否监管箱
//        {
//            get;
//            set;
//        }

#if SILVERLIGHT
#else
        [Property(NotNull = true)]
#endif
        public virtual bool 是否拒绝
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(Length = 100)]
#endif
        public virtual string 拒绝原因
        {
            get;
            set;
        }

        //[Property(NotNull = false)]
        //public virtual DateTime? 疏港期限
        //{
        //    get;
        //    set;
        //}
#if SILVERLIGHT
#else
        [ManyToOne(NotNull = false, ForeignKey = "FK_任务_专家任务", Column = "专家任务", Cascade = "none")]
        public virtual 专家任务 专家任务
        {
            get;
            set;
        }

        public virtual string 起始地编号
        {
            get
            {
                if (任务性质 == Model.任务性质.进口拆箱
                    || 任务性质 == Model.任务性质.出口装箱)
                    return 提箱点编号;
                else
                    return 装货地编号;
            }
        }

        public virtual string 途径地编号
        {
            get
            {
                if (任务性质 == Model.任务性质.进口拆箱)
                    return 卸货地编号;
                else if (任务性质 == Model.任务性质.出口装箱)
                    return 装货地编号;
                else
                    return string.Empty;
            }
        }

        public virtual string 终止地编号
        {
            get
            {
                if (任务性质 == Model.任务性质.进口拆箱
                    || 任务性质 == Model.任务性质.出口装箱)
                    return 还箱进港点编号;
                else
                    return 卸货地编号;
            }
        }
#endif

#if SILVERLIGHT
#else
        [Property(NotNull = false)]
#endif
        public virtual bool 是否已导入
        {
            get;
            set;
        }

        [Property(NotNull = true)]
        public virtual int 缓急程度
        {
            get;
            set;
        }

        public virtual DateTime? 时间要求始
        {
            get
            {
                switch (任务性质)
                {
                    case Model.任务性质.E带货:
                    case Model.任务性质.I带货:
                        return 装货时间要求始;
                    case Model.任务性质.出口装箱:
                        return 装货时间要求始;
                    case Model.任务性质.进口拆箱:
                        return 提箱时间要求;
                }
                return null;
            }
        }
        public virtual DateTime? 时间要求止
        {
            get
            {
                switch (任务性质)
                {
                    case Model.任务性质.E带货:
                    case Model.任务性质.I带货:
                        return 卸货时间要求止;
                    case Model.任务性质.出口装箱:
                    case Model.任务性质.进口拆箱:
                        return 还箱进港时间要求;
                }
                return null;
            }
        }

        public virtual string 起始途径终止地
        {
            get
            {
                switch (this.任务性质)
                {
                    case Model.任务性质.进口拆箱:
                        return this.提箱点编号 + "," + this.卸货地编号 + "," + this.还箱进港点编号;
                    case Model.任务性质.出口装箱:
                        return this.提箱点编号 + "," + this.装货地编号 + "," + this.还箱进港点编号;
                    case Model.任务性质.E带货:
                    case Model.任务性质.I带货:
                        return this.装货地编号 + "," + this.卸货地编号;
                    default:
                        throw new ArgumentException("invalid 任务性质.");
                }
            }
        }

#if SILVERLIGHT
        [Required()]
#else
        [ManyToOne(NotNull = false, Insert = false, Update = false, ForeignKey = "FK_任务_卸货地单位")]
        public virtual 人员 卸货地单位
        {
            get;
            set;
        }

        [Property(Column = "卸货地单位", Length = 6, NotNull = false)]
#endif
        public virtual string 卸货地单位编号
        {
            get;
            set;
        }

#if SILVERLIGHT
        [Required()]
#else
        [ManyToOne(NotNull = false, Insert = false, Update = false, ForeignKey = "FK_任务_装货地单位")]
        public virtual 人员 装货地单位
        {
            get;
            set;
        }

        [Property(Column = "装货地单位", Length = 6, NotNull = false)]
#endif
        public virtual string 装货地单位编号
        {
            get;
            set;
        }
    }
}