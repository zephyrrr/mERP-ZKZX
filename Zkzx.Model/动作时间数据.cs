using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Zkzx.Model
{
    [Serializable]
    [Class(NameType = typeof(动作时间数据), Table = "业务作业_动作时间数据", OptimisticLock = OptimisticLockMode.Version)]
    public class 动作时间数据 : Feng.BaseBOEntity
    {
        [ManyToOne(NotNull = true, ForeignKey = "FK_动作时间数据_车辆作业")]
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
        public virtual string 地点
        {
            get;
            set;
        }

        [Property(NotNull = true, Length = 50)]
        public virtual string 动作
        {
            get;
            set;
        }
    }
}
