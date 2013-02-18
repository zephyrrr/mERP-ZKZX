using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Zkzx.Model
{
    [Serializable]
    [Class(NameType = typeof(作业异常情况), Table = "业务作业_作业异常情况", OptimisticLock = OptimisticLockMode.Version)]
    public class 作业异常情况 : Feng.BaseDataEntity
    {
        [ManyToOne(NotNull = true, ForeignKey = "FK_异常情况_车辆作业")]
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

        [Property(NotNull = true, Length = 50)]
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

        [Property(NotNull = true, Length = 50)]
        public virtual string 处理状态
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual DateTime? 处理时间
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 255)]
        public virtual string 处理结果
        {
            get;
            set;
        }
    }
}
