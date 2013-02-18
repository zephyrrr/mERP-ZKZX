using System;
using System.Collections.Generic;
using System.Text;
using Feng.Data;

namespace Zkzx.Model
{
    public class 任务Dao : BaseDao<任务>
    {
        public 任务Dao()
        {
            this.EntityOperating += new EventHandler<Feng.OperateArgs<任务>>(任务Dao_EntityOperating);
        }

        void 任务Dao_EntityOperating(object sender, Feng.OperateArgs<任务> e)
        {
            if (e.OperateType == Feng.OperateType.Save || e.OperateType == Feng.OperateType.Update)
            {
                e.Entity.是否小箱 = e.Entity.箱型编号.HasValue && e.Entity.箱型编号.Value >= 20 && e.Entity.箱型编号.Value < 40;

                if (e.Entity.委托时间.HasValue)
                {
                    var result = (System.DateTime.Today - e.Entity.委托时间.Value).Days;
                    if (result > 5)
                        result = 5;
                    else if (result == 0)
                        result = 1;
                    e.Entity.缓急程度 = result;
                }

                if (e.Entity.票 != null)
                {
                    e.Repository.Attach(e.Entity);

                    e.Entity.任务性质 = e.Entity.票.任务性质;
                    e.Entity.转关箱标志 = e.Entity.票.转关箱标志;
                    e.Entity.委托人编号 = e.Entity.票.委托人编号;
                    e.Entity.委托时间 = e.Entity.票.委托时间;
                    e.Entity.委托联系人 = e.Entity.票.委托联系人;
                    e.Entity.提箱时间要求 = e.Entity.票.提箱时间要求;
                    e.Entity.还箱进港时间要求 = e.Entity.票.还箱进港时间要求;
                    e.Entity.箱属船公司编号 = e.Entity.票.箱属船公司编号;
                    e.Entity.提单号 = e.Entity.票.提单号;
                    e.Entity.备注 = e.Entity.票.备注;
                    e.Entity.船名 = e.Entity.票.船名;
                    e.Entity.航次 = e.Entity.票.航次;
                    e.Entity.提示性箱号 = e.Entity.票.提示性箱号;
                }
            }
        }

        public static void 生成预录入号(任务 entity)
        {
            生成预录入号(entity, 0); 
        }

        public static void 生成预录入号(任务 entity, int delta)
        {
            if (string.IsNullOrEmpty(entity.预录入号))
            {
                string idPre = null;
                switch (entity.任务性质)
                {
                    case 任务性质.进口拆箱:
                        idPre = "I";
                        break;
                    case 任务性质.出口装箱:
                        idPre = "E";
                        break;
                    case 任务性质.I带货:
                        idPre = "H";
                        break;
                    case 任务性质.E带货:
                        idPre = "D";
                        break;
                    default:
                        throw new NotSupportedException("不合理的任务性质！");
                }
                string idDate = PrimaryMaxIdGenerator.GetIdYearMonth();

                string idSource = null;
                if (entity.任务来源 == 任务来源.网上)
                {
                    idSource = "1";
                }
                else if (entity.任务来源 == 任务来源.手工 || entity.任务来源 == 任务来源.文件)
                {
                    idSource = "0";
                }
                else
                {
                    throw new NotSupportedException("不合理的任务来源!");
                }
                string preName = idPre + idDate + idSource;

                long maxInt = PrimaryMaxIdGenerator.GetMaxInt("业务备案_任务", "预录入号", 12, preName, "预录入号 LIKE '_" + idDate + idSource + "%'");
                entity.预录入号 = PrimaryMaxIdGenerator.GetMaxId(maxInt, 12, preName, delta);
            }
        }

        public static void 生成任务号(任务 entity)
        {
            生成任务号(entity, 0);
            entity.拒绝原因 = System.DateTime.Now.ToString("MM.dd-HH:mm");
        }

        public static void 生成任务号(任务 entity, int delta)
        {
            if (string.IsNullOrEmpty(entity.任务号))
            {
                string preName = null;
                switch (entity.任务性质)
                {
                    case 任务性质.进口拆箱:
                        preName = "I";
                        break;
                    case 任务性质.出口装箱:
                        preName = "E";
                        break;
                    case 任务性质.I带货:
                        preName = "H";
                        break;
                    case 任务性质.E带货:
                        preName = "D";
                        break;
                    default:
                        throw new NotSupportedException("不合理的任务性质！");
                }
                preName += PrimaryMaxIdGenerator.GetIdYearMonth();

                long maxInt = PrimaryMaxIdGenerator.GetMaxInt("业务备案_任务", "任务号", 12, preName, "任务号 LIKE '_" + preName.Substring(1) + "%'");
                entity.任务号 = PrimaryMaxIdGenerator.GetMaxId(maxInt, 12, preName, delta);
            }
        }


        //public void 确认(任务 rw)
        //{
        //    生成任务号(rw);
        //    base.Update(rw);
        //}
    }
}
