using System;
using System.Collections.Generic;
using Feng;
using Feng.Utils;

#if SILVERLIGHT
#else
using NHibernate.Mapping.Attributes;
#endif

namespace Zkzx.Model
{
#if SILVERLIGHT
#else
    [Serializable]
    [Auditable]
    [Class(Name = "进口票", Table = "业务备案_进口票")]
#endif
    public class 进口票 : BaseBOEntity,
         IMasterEntity<进口票, 任务>
    {
        #region "Interface"
        IList<任务> IMasterEntity<进口票, 任务>.DetailEntities
        {
            get { return 任务; }
            set { 任务 = value; }
        }
        #endregion

#if SILVERLIGHT
#else
        [Bag(0, Cascade = "none", Inverse = true, Lazy = CollectionLazy.False)]
        [Key(1, Column = "票")]
        [OneToMany(2, ClassType = typeof(任务), NotFound = NotFoundMode.Ignore)]
#endif
        public virtual IList<任务> 任务
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = true)]
#endif
        public virtual 任务性质 任务性质
        {
            get;
            set;
        }


#if SILVERLIGHT
#else
        [Property(NotNull = true)]
#endif
        public virtual 转关箱标志 转关箱标志
        {
            get;
            set;
        }


#if SILVERLIGHT
#else
        [Property(Column = "委托人", Length = 6, NotNull = false)]
#endif
        public virtual string 委托人编号
        {
            get;
            set;
        }


#if SILVERLIGHT
#else
        [Property(NotNull = false)]    
#endif
        public virtual DateTime? 委托时间
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(NotNull = false, Length = 255)]
#endif
        public virtual string 委托联系人
        {
            get;
            set;
        }

        //[Property(NotNull = false, Length = 255)]
        //public virtual string 卸货联系人
        //{
        //    get;
        //    set;
        //}

        //[Property(NotNull = false, Length = 50)]
        //public virtual string 卸货联系手机
        //{
        //    get;
        //    set;
        //}

        //[Property(NotNull = false, Length = 50)]
        //public virtual string 卸货联系座机
        //{
        //    get;
        //    set;
        //}

#if SILVERLIGHT
#else
        [Property(NotNull = false, Length = 20)]
#endif
        public virtual string 总箱量
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(Length = 50, NotNull = false)]
#endif
        public virtual string 提单号
        {
            get;
            set;
        }

        //[Property(Length = 12, NotNull = false)]
        //public virtual string 箱号
        //{
        //    get;
        //    set;
        //}

#if SILVERLIGHT
#else
        [Property(NotNull = false, Length = 12)]
#endif
        public virtual string 提示性箱号
        {
            get;
            set;
        }


        public virtual string 船名航次
        {
            get
            {
                return 船名.ToString() + '/' + 航次.ToString();
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
        [Property(NotNull = false, Length = 255)]
#endif
        public virtual string 备注
        {
            get;
            set;
        }

#if SILVERLIGHT
#else
        [Property(Column = "箱属船公司", Length = 6, NotNull = false)]
#endif
        public virtual string 箱属船公司编号
        {
            get;
            set;
        }

        public virtual string 货名
        {
            get
            {
                string m_货名 = string.Empty;
                if (任务 != null && 任务.Count > 0)
                {
                    foreach (任务 rw in 任务)
                    {
                        if (!string.IsNullOrEmpty(m_货名) && rw.货名 != null && !m_货名.Contains(rw.货名))
                        {
                            m_货名 += ", ";
                        }
                        if (rw.货名 != null && !m_货名.Contains(rw.货名))
                        {
                            m_货名 += rw.货名;
                        }
                    }
                }
                
                return m_货名;
            }
        }

        public virtual string 货物特征
        {
            get
            {
                string m_货物特征 = string.Empty;
                if (任务 != null && 任务.Count > 0)
                {
                    foreach (任务 rw in 任务)
                    {
                        if (!string.IsNullOrEmpty(m_货物特征) && rw.货物特征 != null && !m_货物特征.Contains(rw.货物特征))
                        {
                            m_货物特征 += ", ";
                        }
                        if (rw.货物特征 != null && !m_货物特征.Contains(rw.货物特征))
                        {
                            m_货物特征 += rw.货物特征;
                        }
                    }
                }
                return m_货物特征;
            }
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

        //[Property(NotNull = false)]
        //public virtual DateTime? 提箱时间要求止
        //{
        //    get;
        //    set;
        //}

#if SILVERLIGHT
#else
        [Property(NotNull = false)]
#endif
        public virtual DateTime? 还箱进港时间要求
        {
            get;
            set;
        }

        //[Property(NotNull = false)]
        //public virtual DateTime? 还箱进港时间要求止
        //{
        //    get;
        //    set;
        //}
    }
}
