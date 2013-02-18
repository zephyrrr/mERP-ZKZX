using System;
using System.Collections.Generic;
using NHibernate.Mapping.Attributes;
using Feng;

namespace Zkzx.Model
{
    // IsActive 表示是否确认
    [Class(Name = "转关箱排车暂存组", Table = "业务备案_转关箱排车暂存组")]
    public class 转关箱排车暂存组 : BaseBOEntity
    {
        [Bag(0, Cascade = "none", Inverse = true)]
        [Key(1, Column = "暂存组")]
        [OneToMany(2, ClassType = typeof(转关箱排车计划), NotFound = NotFoundMode.Ignore)]
        public virtual IList<转关箱排车计划> 转关箱排车
        {
            get;
            set;
        }

        [Property(NotNull = true)]
        public virtual DateTime 预排时间
        {
            get;
            set;
        }
    }
}
