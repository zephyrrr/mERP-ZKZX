using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Zkzx.Model
{
    [Serializable]
    [Class(NameType = typeof(网上委托任务), Table = "网上委托_任务", OptimisticLock = OptimisticLockMode.Version)]
    public class 网上委托任务 : Feng.BaseBOEntity
    {
        [Property(NotNull = true)]
        public virtual 任务来源 任务来源
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 12)]
        public virtual string 预录入号
        {
            get;
            set;
        }

        [Property(Length = 12)]
        public virtual string 任务号
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual 任务性质 任务性质
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual bool 是否小箱
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual 转关箱标志 转关箱标志
        {
            get;
            set;
        }

        [Property(Column = "委托人", Length = 6, NotNull = false)]
        public virtual string 委托人编号
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual DateTime? 委托时间
        {
            get;
            set;
        }

        [Property(Length = 255)]
        public virtual string 委托联系人
        {
            get;
            set;
        }

        [Property(Length = 12, NotNull = false)]
        public virtual string 箱号
        {
            get;
            set;
        }

        [Property(Column = "箱型", NotNull = false)]
        public virtual int? 箱型编号
        {
            get;
            set;
        }

        [Property(Column = "箱属船公司", Length = 6, NotNull = false)]
        public virtual string 箱属船公司编号
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 50)]
        public virtual string 封志号
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 50)]
        public virtual string 货名
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 50)]
        public virtual string 货物特征
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual decimal 价值
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual double 重量
        {
            get;
            set;
        }

        [Property(Column = "提箱点", Length = 6, NotNull = false)]
        public virtual string 提箱点编号
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual DateTime? 提箱时间要求
        {
            get;
            set;
        }

        //[Property(NotNull = false)]
        //public virtual DateTime? 提箱时间要求止
        //{
        //    get;
        //    set;
        //}

        [Property(Column = "还箱进港点", Length = 6, NotNull = false)]
        public virtual string 还箱进港点编号
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual DateTime? 还箱进港时间要求
        {
            get;
            set;
        }

        //[Property(NotNull = false)]
        //public virtual DateTime? 还箱进港时间要求止
        //{
        //    get;
        //    set;
        //}

        [Property(Column = "装货地", Length = 6, NotNull = false)]
        public virtual string 装货地编号
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual DateTime? 装货时间要求始
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual DateTime? 装货时间要求止
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 50)]
        public virtual string 装货联系人
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 50)]
        public virtual string 装货联系手机
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 50)]
        public virtual string 装货联系座机
        {
            get;
            set;
        }

        [Property(Column = "卸货地", Length = 6, NotNull = false)]
        public virtual string 卸货地编号
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual DateTime? 卸货时间要求始
        {
            get;
            set;
        }

        [Property(NotNull = false)]
        public virtual DateTime? 卸货时间要求止
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 50)]
        public virtual string 卸货联系人
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 50)]
        public virtual string 卸货联系手机
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 50)]
        public virtual string 卸货联系座机
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 255)]
        public virtual string 备注
        {
            get;
            set;
        }

        public virtual string 船名航次
        {
            get
            {
                return 船名.ToString() + '/' + 航次.ToString();
            }
        }


        [Property(NotNull = false, Length = 50)]
        public virtual string 船名
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 50)]
        public virtual string 航次
        {
            get;
            set;
        }


        [Property(NotNull = false, Length = 50)]
        public virtual string 提单号
        {
            get;
            set;
        }

        [Property(NotNull = false, Length = 12)]
        public virtual string 提示性箱号
        {
            get;
            set;
        }

        //[Property(NotNull = false)]
        //public virtual bool? 是否监管箱
        //{
        //    get;
        //    set;
        //}

        [Property(NotNull = true)]
        public virtual bool 是否受理
        {
            get;
            set;
        }
    }
}
