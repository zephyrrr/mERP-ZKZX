using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;
using Feng;

namespace Zkzx.Model
{
    [Serializable]
    [Class(NameType = typeof(人员), Table = "参数备案_人员单位", OptimisticLock = OptimisticLockMode.Version)]
    public class 人员 : BaseEntity<string>
    {
        public override string Identity
        {
            get { return this.编号; }
        }

        [Id(0, Name = "编号", Length = 6)]
        [Generator(1, Class = "assigned")]
        public virtual string 编号
        {
            get;
            set;
        }

        ///<summary>
        ///简称
        ///</summary>
        [Property(Length = 20, NotNull = true, Unique = true, UniqueKey = "UK_人员_简称")]
        public virtual string 简称
        {
            get;
            set;
        }

        ///<summary>
        ///全称
        ///</summary>
        [Property(Length = 50, NotNull = true, Unique = true, UniqueKey = "UK_人员_全称")]
        public virtual string 全称
        {
            get;
            set;
        }

        ///<summary>
        ///角色用途
        ///</summary>
        [Property(Length = 100)]
        public virtual string 角色用途
        {
            get;
            set;
        }

        ///<summary>
        ///业务类型
        ///</summary>
        [Property(Length = 100)]
        public virtual string 业务类型
        {
            get;
            set;
        }

        ///<summary>
        ///字母简称
        ///</summary>
        [Property(Length = 20)]
        public virtual string 字母简称
        {
            get;
            set;
        }

        ///<summary>
        ///联系方式
        ///</summary>
        [Property(Length = 200)]
        public virtual string 联系方式
        {
            get;
            set;
        }

        ///<summary>
        ///所在地
        ///</summary>
        [Property(Length = 20)]
        public virtual string 所在地
        {
            get;
            set;
        }

        ///<summary>
        ///备注
        ///</summary>
        [Property(Length = 200)]
        public virtual string 备注
        {
            get;
            set;
        }
    }
}
