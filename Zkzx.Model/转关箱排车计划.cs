using System;
using System.Collections.Generic;
using NHibernate.Mapping.Attributes;
using Feng;

namespace Zkzx.Model
{
    [Class(Name = "转关箱排车计划", Table = "业务备案_转关箱排车计划")]
    public class 转关箱排车计划 : BaseBOEntity
    {
        [ManyToOne(NotNull = false, ForeignKey = "FK_转关箱排车计划_暂存组", Column = "暂存组", Cascade = "none")]
        public virtual 转关箱排车暂存组 暂存组
        {
            get;
            set;
        }

        [ManyToOne(NotNull = true, ForeignKey = "FK_转关箱排车计划_车辆")]
        public virtual 车辆 车辆
        {
            get;
            set;
        }

        [Property(NotNull = true)]
        public virtual string 任务号
        {
            get;
            set;
        }

        [Property(NotNull = true)]
        public virtual int 天数序号
        {
            get;
            set;
        }


        /// <summary>
        /// 当确认时，取当前时间为第一天
        /// </summary>
        [Property(NotNull = false)]
        public virtual DateTime? 日期
        {
            get;
            set;
        }
    }
}
