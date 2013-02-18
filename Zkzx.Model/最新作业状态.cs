using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Zkzx.Model
{
    [Class(NameType = typeof(最新作业状态), Table = "视图_车辆作业_最新状态", SchemaAction = "none", Mutable = false)]
    public class 最新作业状态 : Feng.IEntity
    {
        [Id(0, Name = "Id", Column = "Id")]
        public virtual Guid Id
        {
            get;
            set;
        }

        public virtual string 车辆位置
        {
            get
            {
                //if (!string.IsNullOrEmpty(车辆重要区域))
                //    return 车辆重要区域;
                if (string.IsNullOrEmpty(车辆区域))
                    return this.车辆道路;
                else if (string.IsNullOrEmpty(车辆道路))
                    return this.车辆区域;
                return 车辆区域 + "," + 车辆道路;
            }
        }

        [Property(NotNull = false, Length = 50)]
        public virtual string 车辆区域
        {
            get;
            set;
        }
        [Property(NotNull = false, Length = 50)]
        public virtual string 车辆道路
        {
            get;
            set;
        }
        [Property(NotNull = false, Length = 50)]
        public virtual string 车辆重要区域
        {
            get;
            set;
        }
        [Property(NotNull = false, Length = 50)]
        public virtual string 车辆规划道路
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 50)]
        public virtual string 作业地点
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 50)]
        public virtual string 任务进程
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 50)]
        public virtual string 作业状态
        {
            get;
            set;
        }
        [Property(NotNull = true)]
        public virtual int 作业进程序号
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual DateTime? 预计到达时间
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual long? 作业监控Id
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 50)]
        public virtual string 异常情况
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 255)]
        public virtual string 异常参数
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 50)]
        public virtual string 处理状态
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual long? 作业异常Id
        {
            get;
            set;
        }
    }
}
