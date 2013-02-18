using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Zkzx.Model
{
    // 异常规则：以Route为准，1）判断当前位置是否在Route里的位置集合内； 2）判断是否上一状态是同位置或者下一位置；3）如果是同一位置，判断是否超时
    [Serializable]
    [Class(NameType = typeof(作业监控状态), Table = "业务作业_监控状态", OptimisticLock = OptimisticLockMode.Version)]
    public class 作业监控状态 : Feng.BaseDataEntity
    {
        [ManyToOne(NotNull = true, ForeignKey = "FK_监控状态_车辆作业")]
        public virtual 车辆作业 车辆作业
        {
            get;
            set;
        }

        [Property(NotNull = true)]
        public virtual DateTime 时间
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

        // 北仑二期,杭州余杭,杭州余杭,杭州余杭,北仑二期
        [Property(NotNull = false, Length = 50)]
        public virtual string 作业地点
        {
            get;
            set;
        }

        // 0/出口装箱-2/ E带货
        [Property(NotNull = true, Length = 50)]
        public virtual string 任务进程
        {
            get;
            set;
        }

        // 提箱途中
        [Property(NotNull = true, Length = 50)]
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
        public virtual long? GpsData
        {
            get;
            set;
        }
    }
}
