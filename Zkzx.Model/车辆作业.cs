using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Zkzx.Model
{
    [Serializable]
    [Class(NameType = typeof(车辆作业), Table = "业务作业_车辆作业", OptimisticLock = OptimisticLockMode.Version)]
    public class 车辆作业 : Feng.BaseBOEntity
    {
        [Property(NotNull = true, Length = 14)]
        public virtual string 作业号
        {
            get;
            set;
        }

        [ManyToOne(NotNull = false, ForeignKey = "FK_车辆作业_专家任务")]
        public virtual 专家任务 专家任务
        {
            get;
            set;
        }

        [ManyToOne(NotNull = true, ForeignKey = "FK_车辆作业_车辆")]
        public virtual 车辆 车辆
        {
            get;
            set;
        }

        [ManyToOne(NotNull = false, ForeignKey = "FK_车辆作业_驾驶员", Insert = false, Update = false)]
        public virtual 人员 驾驶员
        {
            get;
            set;
        }
        [Property(Column = "驾驶员", Length = 6, NotNull = false)]
        public virtual string 驾驶员编号
        {
            get;
            set;
        }

        //[Property(NotNull = true)]
        //public virtual int 缓急程度
        //{
        //    get;
        //    set;
        //}

        [Property(Length = 500, NotNull = false)]
        public virtual string 备注
        {
            get;
            set;
        }

        [Bag(0, Cascade = "none", Inverse = true, OrderBy = "时间 DESC")]
        [Key(1, Column = "车辆作业")]
        [OneToMany(2, ClassType = typeof(作业监控状态), NotFound = NotFoundMode.Ignore)]
        public virtual IList<作业监控状态> 作业监控状态
        {
            get;
            set;
        }

        [Bag(0, Cascade = "none", Inverse = true, OrderBy = "时间 DESC")]
        [Key(1, Column = "车辆作业")]
        [OneToMany(2, ClassType = typeof(作业异常情况), NotFound = NotFoundMode.Ignore)]
        public virtual IList<作业异常情况> 作业异常情况
        {
            get;
            set;
        }

        [Bag(0, Cascade = "none", Inverse = true, OrderBy = "时间 DESC")]
        [Key(1, Column = "车辆作业")]
        [OneToMany(2, ClassType = typeof(动作时间数据), NotFound = NotFoundMode.Ignore)]
        public virtual IList<动作时间数据> 动作时间数据
        {
            get;
            set;
        }

        //[Property(NotNull = false)]
        //public virtual DateTime? 起始地时间
        //{
        //    get;
        //    set;
        //}

        //[Property(NotNull = false)]
        //public virtual DateTime? 途径地时间
        //{
        //    get;
        //    set;
        //}

        //[Property(NotNull = false)]
        //public virtual DateTime? 终止地时间
        //{
        //    get;
        //    set;
        //}

        [Property(NotNull = false)]
        public virtual DateTime? 开始时间
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual DateTime? 结束时间
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual long? Track
        {
            get;
            set;
        }

        //[ManyToOne(NotNull = false, Insert = false, Update = false, Column = "Id")]
        [OneToOne(Constrained = false)]
        public virtual 最新作业状态 最新作业状态
        {
            get;
            set;
        }

        [Property(Column = "车载Id号", Length = 7, NotNull = false)]
        public virtual string 车载Id号
        {
            get;
            set;
        }
    }
}
