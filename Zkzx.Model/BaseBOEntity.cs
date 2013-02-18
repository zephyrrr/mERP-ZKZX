using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Feng;

#if SILVERLIGHT
#else
using NHibernate.Mapping.Attributes;
#endif

namespace Zkzx.Model
{
//    public abstract class BaseEntity<T> : AbstractBaseEntity<T>
//        where T : IComparable
//    {
//        #region "Log, MultiOrg, Version, Active"
//        /// <summary>
//        /// Version
//        /// </summary>
//#if SILVERLIGHT
//#else
//        [Version(Column = "Version", Type = "Int32", UnsavedValue = "0")]
//#endif
//        public int Version
//        {
//            get;
//            set;
//        }

//        /// <summary>
//        /// 创建者
//        /// </summary>
//#if SILVERLIGHT
//#else
//        [Property(Name = "CreatedBy", NotNull = true, Length = 20)]
//#endif
//        public string CreatedBy
//        {
//            get;
//            set;
//        }

//        /// <summary>
//        /// 创建时间
//        /// </summary>
//#if SILVERLIGHT
//#else
//        [Property(Name = "Created", NotNull = true)]
//#endif
//        public DateTime Created
//        {
//            get;
//            set;
//        }

//        /// <summary>
//        /// 修改者
//        /// </summary>
//#if SILVERLIGHT
//#else
//        [Property(Name = "UpdatedBy", NotNull = false, Length = 20)]
//#endif
//        public string UpdatedBy
//        {
//            get;
//            set;
//        }

//        /// <summary>
//        /// 修改时间
//        /// </summary>
//#if SILVERLIGHT
//#else
//        [Property(Name = "Updated", NotNull = false)]
//#endif
//        public DateTime? Updated
//        {
//            get;
//            set;
//        }

//        /// <summary>
//        /// IsActive
//        /// </summary>
//#if SILVERLIGHT
//#else
//        [Property(Name = "IsActive", NotNull = true)]
//#endif
//        public bool IsActive
//        {
//            get;
//            set;
//        }

//        ///// <summary>
//        ///// 相关客户，同<see cref="ClientId"/>
//        ///// </summary>
//        //[ManyToOne(Column = "ClientId", Insert = false, Update = false)]
//        //public virtual ClientInfo Client
//        //{
//        //    get;
//        //    set;
//        //}

//        /// <summary>
//        /// 此记录属于的客户Id。
//        /// 系统支持多客户组织。客户对应为公司，组织对应为部门，记录只能同客户的操作，同一客户内，部门通过不同权限操作记录。
//        /// 目前无用，默认为0
//        /// </summary>
//#if SILVERLIGHT
//#else
//        [Property(Name = "ClientId", NotNull = true)]
//#endif
//        public long ClientId
//        {
//            get;
//            set;
//        }

//        ///// <summary>
//        ///// 相关组织，同<see cref="OrgId"/>
//        ///// </summary>
//        //[ManyToOne(Column = "OrgId", Insert = false, Update = false)]
//        //public virtual OrganizationInfo Org
//        //{
//        //    get;
//        //    set;
//        //}

//        /// <summary>
//        /// 此记录属于的组织Id。
//        /// 客户组织的关系见<see cref="ClientId"/>
//        /// </summary>
//#if SILVERLIGHT
//#else
//        [Property(Name = "OrgId", NotNull = true)]
//#endif
//        public long OrgId
//        {
//            get;
//            set;
//        }
//        #endregion
//    }

//    public class BaseBOEntity : BaseEntity<Guid>
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        public sealed override Guid Identity
//        {
//            get { return this.Id; }
//        }

//#if SILVERLIGHT
//        [Key]
//        [ReadOnly(true)]
//#else
//        [Id(0, Name = "Id", Column = "Id")]
//        [Generator(1, Class = "guid.comb")]
//#endif
//        public virtual Guid Id
//        {
//            get;
//            set;
//        }
//    }

    /// <summary>
    /// 可提交的记录
    /// </summary>
    public abstract class SubmittedEntity : BaseBOEntity, ISubmittedEntity
    {
        /// <summary>
        /// 是否已提交
        /// </summary>
#if SILVERLIGHT
#else
        [Property(NotNull = true)]
#endif
        public virtual bool Submitted
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 可提交的记录
    /// </summary>
    public abstract class SubmittedEntity2 : SubmittedEntity, ISubmittedEntity2
    {
        /// <summary>
        /// 是否已提交
        /// </summary>
#if SILVERLIGHT
#else
        [Property(NotNull = true)]
#endif
        public virtual bool Submitted2
        {
            get;
            set;
        }
    }
}
