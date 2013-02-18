using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Zkzx.Model
{
    //public enum 车辆类别
    //{
    //    自有车 = 1,
    //    代管车 = 2,
    //    挂靠车 = 3,
    //    外协车 = 4
    //}

    public enum 车辆当前状态
    {
        报停中 = 0,
        使用中 = 1,
    }

    [Serializable]
    [Class(NameType = typeof(车辆), Table = "参数备案_车辆", OptimisticLock = OptimisticLockMode.Version)]
    public class 车辆 : Feng.BaseBOEntity
    {
        //[Property(Length = 20, NotNull = true, Unique = true, UniqueKey = "UK_车辆_简称")]
        //public virtual string 简称
        //{
        //    get;
        //    set;
        //}

        //[Property(NotNull = true)]
        //public virtual 车辆类别 车辆类别
        //{
        //    get;
        //    set;
        //}


        [Property(Length = 20, NotNull = true)]
        public virtual string 车牌号
        {
            get;
            set;
        }

        [Property(Length = 20, NotNull = true)]
        public virtual string 车型
        {
            get;
            set;
        }

        [Property(Length = 20, NotNull = true)]
        public virtual string 核定载重
        {
            get;
            set;
        }

        public virtual bool 监管车
        {
            get;
            set;
        }

        [ManyToOne(NotNull = false, Insert = false, Update = false, ForeignKey = "FK_车辆_车主")]
        public virtual 人员 车主
        {
            get;
            set;
        }

        [Property(Column = "车主", Length = 6, NotNull = false)]
        public virtual string 车主编号
        {
            get;
            set;
        }

        [ManyToOne(NotNull = false, Insert = false, Update = false, ForeignKey = "FK_车辆_主驾驶员")]
        public virtual 人员 主驾驶员
        {
            get;
            set;
        }

        [Property(Column = "主驾驶员", Length = 6, NotNull = false)]
        public virtual string 主驾驶员编号
        {
            get;
            set;
        }

        [ManyToOne(NotNull = false, Insert = false, Update = false, ForeignKey = "FK_车辆_所属车队")]
        public virtual 人员 所属车队
        {
            get;
            set;
        }

        [Property(Column = "所属车队", Length = 6, NotNull = false)]
        public virtual string 所属车队编号
        {
            get;
            set;
        }

        [Property(NotNull = true)]
        public virtual int 车辆忠诚度
        {
            get;
            set;
        }

        [Property(NotNull = true, Length = 20)]
        public virtual string 车况
        {
            get;
            set;
        }

        [Property(NotNull = true)]
        public virtual bool 是否监管车
        {
            get;
            set;
        }

        [Property(NotNull = true)]
        public virtual 车辆当前状态 当前状态
        {
            get;
            set;
        }

        [Property(NotNull = true)]
        public virtual DateTime 状态时间起
        {
            get;
            set;
        }
        [Property(NotNull = false)]
        public virtual DateTime? 状态时间止
        {
            get;
            set;
        }

        //[Property(NotNull = false, Length = 50)]
        //public virtual string 后续作业计划
        //{
        //    get;
        //    set;
        //}

        //[Property(Length = 20, NotNull = false)]
        //public virtual string 挂车号
        //{
        //    get;
        //    set;
        //}

        //[ManyToOne(NotNull = true, Insert = false, Update = false, ForeignKey = "FK_车辆_车主")]
        //public virtual 人员 车主
        //{
        //    get;
        //    set;
        //}

        //[Property(Column = "车主", Length = 6, NotNull = true)]
        //public virtual string 车主编号
        //{
        //    get;
        //    set;
        //}



        //[Property(Length = 6, NotNull = true)]
        //public virtual string 马力
        //{
        //    get;
        //    set;
        //}

        //[Property(NotNull = false)]
        //public virtual bool 默认出车
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

        //[Property(NotNull = false)]
        //public virtual DateTime? 卖出时间
        //{
        //    get;
        //    set;
        //}

        [Property(Column = "车载Id号", Length = 7, NotNull = false)]
        public virtual string 车载Id号
        {
            get;
            set;
        }
    }
}
