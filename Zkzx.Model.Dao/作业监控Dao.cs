using System;
using System.Collections.Generic;
using System.Text;
using Feng;
using Feng.Map;

namespace Zkzx.Model
{
    public class 作业监控Dao : BaseDao<作业监控状态>
    {
        public void 更新作业监控状态2(IRepository rep, 车辆作业 车辆作业, DateTime 时间, string importantAreaId, string state)
        {
            if (!车辆作业.开始时间.HasValue)
                return;

            //车辆作业.最新作业状态 = rep.Get<最新作业状态>(车辆作业.最新作业状态.Id);
            rep.Refresh(车辆作业.最新作业状态);

            string importantAreaName = Feng.Utils.NameValueControlHelper.GetMultiString("人员单位_装卸货地_全部", importantAreaId);

            作业监控状态 entity = new 作业监控状态();
            entity.IsActive = true;
            entity.车辆作业 = 车辆作业;
            entity.时间 = 时间;
            entity.车辆重要区域 = string.IsNullOrEmpty(importantAreaName) ? null : importantAreaName;

            更新作业监控状态(rep, entity, 车辆作业.最新作业状态, importantAreaId, state);
            base.Save(rep, entity);

            Update最新状态(车辆作业, entity);
        }

        public bool 更新作业监控状态1(车辆作业 车辆作业, TrackPoint gpsData)
        {
            if (!车辆作业.开始时间.HasValue)
                return false;

            if (gpsData == null)
                return false;

            MapHelper.ChangeRepository(MapHelper.MapType.普通区域);
            var areaName = MapHelper.GetAreaFromGpsData(gpsData);
            var streetName = MapHelper.GetRoadFromGpsData(gpsData);

            if (!string.IsNullOrEmpty(areaName))
            {
                using (IRepository rep = this.GenerateRepository())
                {
                    try
                    {
                        rep.BeginTransaction();

                        rep.Refresh(车辆作业.最新作业状态);

                        作业监控状态 newEntity = new 作业监控状态();
                        newEntity.IsActive = true;
                        newEntity.车辆作业 = 车辆作业;
                        newEntity.时间 = gpsData.GpsTime;
                        newEntity.车辆区域 = areaName;
                        newEntity.车辆道路 = streetName;
                        newEntity.GpsData = gpsData.ID;

                        //作业地点, 作业状态, 任务进程
                        MapHelper.ChangeRepository(MapHelper.MapType.重要地点);
                        string importantAreaName = MapHelper.GetAreaFromGpsData(gpsData);
                        string importantRoadName = MapHelper.GetRoadFromGpsData(gpsData);

                        newEntity.车辆重要区域 = string.IsNullOrEmpty(importantAreaName) ? null : importantAreaName;
                        newEntity.车辆规划道路 = string.IsNullOrEmpty(importantRoadName) ? null : importantRoadName;

                        string importantAreaId = (string)Feng.Utils.NameValueControlHelper.GetMultiValue("人员单位_装卸货地_全部", importantAreaName);
                        更新作业监控状态(rep, newEntity, 车辆作业.最新作业状态, importantAreaId, null);

                        var preEntity = 车辆作业.最新作业状态;
                        if (preEntity == null || newEntity.作业地点 != preEntity.作业地点)
                        {
                            // add waypoint
                            WayPoint wayPoint = new WayPoint(gpsData);
                            wayPoint.Track = gpsData.Track;
                            wayPoint.Action = importantAreaName + "," + newEntity.作业状态;
                            (new WayPointDao()).Save(rep, wayPoint);

                            var t = Get预计动作时间(importantAreaName, newEntity.作业状态);
                            if (t.HasValue)
                            {
                                newEntity.预计到达时间 = System.DateTime.Now.Add(t.Value);
                            }
                        }
                        else
                        {
                            if (车辆作业.Track.HasValue)
                            {
                                var track = rep.Get<Track>(车辆作业.Track.Value);
                                if (track != null && track.Route != null)
                                {
                                    var t = Get预计到达时间(track);
                                    if (t.HasValue)
                                    {
                                        newEntity.预计到达时间 = System.DateTime.Now.Add(t.Value);
                                    }
                                }
                            }
                        }

                        检查车辆行驶异常(rep, newEntity, importantRoadName, importantAreaName, gpsData.GpsTime);

                        base.Save(rep, newEntity);

                        rep.CommitTransaction();
                        Update最新状态(车辆作业, newEntity);

                        return true;
                    }
                    catch (Exception)
                    {
                        rep.RollbackTransaction();
                        throw;
                    }
                }
            }
            return true;
        }

        //public void 更新作业监控状态1(车辆作业 车辆作业)
        //{
        //    using(IRepository rep = this.GenerateRepository())
        //    {
        //        var glist = rep.List<TrackPoint>("Track = @Track ORDER BY Id DESC", new Dictionary<string, object> { { "@Track", 车辆作业.Track } });
        //        //var slist = rep.List<作业监控状态>("车辆作业 = @车辆作业", new Dictionary<string, object> { { "@车辆作业", 车辆作业 } });
        //        foreach (var gpsData in glist)
        //        {
        //            bool b = 更新作业监控状态(rep, 车辆作业, gpsData);
        //            if (b)
        //                break;
        //        }
        //    }
        //}

        private void 检查车辆行驶异常(IRepository rep, 作业监控状态 entity, string importantRoad, string importantAreaName, DateTime gpsTime)
        {
            // 在二期等重要地点
            if (!string.IsNullOrEmpty(importantAreaName))
                return;

            if (string.IsNullOrEmpty(importantRoad))
            {
                if (entity.车辆作业.最新作业状态.异常情况 != "车辆脱离预定路线"
                    || entity.车辆作业.最新作业状态.异常参数 != entity.车辆位置)
                {
                    作业异常Dao 作业异常Dao = new 作业异常Dao();
                    作业异常Dao.新作业异常(rep, entity.车辆作业, "车辆脱离预定路线", entity.车辆位置, gpsTime);
                }
            }
            else
            {
                entity.车辆规划道路 = importantRoad;

                if (entity.车辆作业.Track.HasValue)
                {
                    var track = TrackDao.GetTrack(entity.车辆作业.Track.Value);
                    if (track.Route != null)
                    {
                        var nowRoute = RouteDao.GetRoute(track.Route.Name);
                        if (nowRoute != null)
                        {
                            if (!string.IsNullOrEmpty(nowRoute.DirectionReal))
                            {
                                var b = Feng.Utils.RouteHelper.IsInRoute(nowRoute.DirectionReal, importantRoad);
                                if (!b)
                                {
                                    if (entity.车辆作业.最新作业状态.异常情况 != "车辆脱离预定路线"
                                        || entity.车辆作业.最新作业状态.异常参数 != entity.车辆位置)
                                    {
                                        作业异常Dao 作业异常Dao = new 作业异常Dao();
                                        作业异常Dao.新作业异常(rep, entity.车辆作业, "车辆脱离预定路线", importantRoad, gpsTime);
                                    }
                                }
                            }
                        }
                    }
                }

                // Todo
                //var nowDirection = DirectionDao.GetDirection(importantRoad);
                //if (nowDirection != null && nowDirection.Time > 0)
                //{
                //    var oldList = (rep as Feng.NH.INHibernateRepository).Session.CreateCriteria<作业监控状态>()
                //        .Add(NHibernate.Criterion.Expression.Eq("车辆作业", entity.车辆作业))
                //        .Add(NHibernate.Criterion.Expression.Eq("车辆规划道路", importantRoad))
                //        .AddOrder(NHibernate.Criterion.Order.Asc("时间"))
                //        .SetMaxResults(1).List<作业监控状态>();
                //    if (oldList.Count > 0)
                //    {
                //        if ((gpsTime - oldList[0].时间).TotalMinutes > nowDirection.Time)
                //        {
                //            if (entity.车辆作业.最新作业状态.异常情况 != "车辆运行超出预定时间"
                //                || entity.车辆作业.最新作业状态.异常参数 != entity.车辆规划道路)
                //            {
                //                作业异常Dao 作业异常Dao = new 作业异常Dao();
                //                作业异常Dao.新作业异常(rep, entity.车辆作业, "车辆运行超出预定时间", entity.车辆规划道路, gpsTime);
                //            }
                //        }
                //    }
                //}
            }
        }

        internal static void 更新作业监控状态(IRepository rep, 作业监控状态 newEntity, 最新作业状态 preEntity, string importantAreaId, string state)
        {
            // 任务进程, 作业状态
            rep.Attach(newEntity);
            rep.Attach(newEntity.车辆作业);

            newEntity.作业进程序号 = 0;

            int[] taskIdx = null;
            string[] importantAreas = null;
            string[] importantTaskStatus = null;
            string[] importantWorkStatus = null;

            ModelHelper.Get任务状态(newEntity.车辆作业.专家任务, out taskIdx, out importantAreas, out importantTaskStatus, out importantWorkStatus);

            int preIndex = 0;
            if (preEntity != null)
            {
                if (preEntity.作业进程序号 >= 0)
                {
                    preIndex = preEntity.作业进程序号;
                }
                else
                {
                    if (!string.IsNullOrEmpty(preEntity.作业地点))
                    {
                        string[] ss = preEntity.作业地点.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        preIndex = ss.Length;
                    }
                }
            }

            // 它的标准值是“提箱途中”、“提箱中”、“卸货途中”、“卸货中”、“装货途中”、“装货中”、“进港途中”、“进港中”、“还箱途中”、“还箱中”、“已完成”等。
            if (!string.IsNullOrEmpty(importantAreaId) || !string.IsNullOrEmpty(state))
            {
                var importantAreaName = Feng.Utils.NameValueControlHelper.GetMultiString("人员单位_装卸货地_全部", importantAreaId);
                for (int i = preIndex; i < Math.Min(preIndex + 2, importantAreas.Length); ++i)
                {
                    if (string.IsNullOrEmpty(importantAreaId) || importantAreaId == importantAreas[i])
                    {
                        if (preEntity != null && !string.IsNullOrEmpty(preEntity.作业地点))
                        {
                            string[] ss = preEntity.作业地点.Split(',');
                            if (ss[ss.Length - 1] != importantAreaName
                                || preEntity.作业进程序号 >= ss.Length)
                            {
                                newEntity.作业地点 = preEntity.作业地点 + "," + importantAreaName;
                            }
                            else
                            {
                                newEntity.作业地点 = preEntity.作业地点;
                            }
                        }
                        else
                        {
                            newEntity.作业地点 = importantAreaName;
                        }


                        if (string.IsNullOrEmpty(state))
                        {
                            newEntity.任务进程 = importantTaskStatus[i];
                            newEntity.作业状态 = importantWorkStatus[i] + "中";    // 途中
                            newEntity.作业进程序号 = i;
                        }
                        else
                        {
                            if (state == "开始")
                            {
                                newEntity.任务进程 = importantTaskStatus[i];
                                newEntity.作业状态 = importantWorkStatus[i] + "中";
                                newEntity.作业进程序号 = i;
                            }
                            else
                            {
                                if (i + 1 < importantAreas.Length)
                                {
                                    newEntity.任务进程 = importantTaskStatus[i + 1];
                                    newEntity.作业状态 = importantWorkStatus[i + 1] + "途中";
                                }
                                else
                                {
                                    newEntity.任务进程 = importantTaskStatus[i];
                                    newEntity.作业状态 = "已完成";
                                }

                                newEntity.作业进程序号 = i + 1;
                            }
                        }
                        break;
                    }
                }
            }


            // 中途, 查看上一状态
            if (string.IsNullOrEmpty(newEntity.作业地点))
            {
                if (preEntity == null || string.IsNullOrEmpty(preEntity.作业地点))
                {
                    newEntity.作业地点 = null;
                    newEntity.任务进程 = importantTaskStatus[0];
                    newEntity.作业状态 = importantWorkStatus[0] + "途中";
                    newEntity.作业进程序号 = 0;
                }
                else
                {
                    newEntity.作业地点 = preEntity.作业地点;
                    newEntity.任务进程 = importantTaskStatus[Math.Min(importantTaskStatus.Length - 1, preIndex)]; // preEntity.任务进程;

                    if (preEntity.作业状态.EndsWith("中") && !preEntity.作业状态.EndsWith("途中"))
                    {
                        string s = preEntity.作业状态.Substring(0, preEntity.作业状态.Length - 1);  // remove "中"

                        for (int i = preIndex; i < Math.Min(preIndex + 2, importantAreas.Length); ++i)
                        {
                            if (s == importantWorkStatus[i])
                            {
                                if (i < importantAreas.Length - 1)
                                {
                                    newEntity.作业状态 = importantWorkStatus[i + 1] + "途中";
                                }
                                else
                                {
                                    newEntity.作业状态 = "已完成";
                                }
                                newEntity.作业进程序号 = i + 1;
                                break;
                            }
                        }
                    }
                    else
                    {
                        newEntity.作业状态 = preEntity.作业状态;
                        newEntity.作业进程序号 = preEntity.作业进程序号;
                    }
                }
            }
        }

        private void Update最新状态(车辆作业 clzy, 作业监控状态 entity)
        {
            clzy.最新作业状态.车辆区域 = entity.车辆区域;
            clzy.最新作业状态.车辆道路 = entity.车辆道路;
            clzy.最新作业状态.车辆规划道路 = entity.车辆规划道路;
            clzy.最新作业状态.车辆重要区域 = entity.车辆重要区域;
            clzy.最新作业状态.任务进程 = entity.任务进程;
            clzy.最新作业状态.预计到达时间 = entity.预计到达时间;
            clzy.最新作业状态.作业地点 = entity.作业地点;
            clzy.最新作业状态.作业状态 = entity.作业状态;
            clzy.最新作业状态.作业监控Id = entity.ID;
        }

        private static 车辆作业Dao m_车辆作业Dao = new 车辆作业Dao();
        private TrackDao m_trackDao = new TrackDao();

        public Route GetDefaultRoute(车辆作业 clzy)
        {
            string key = clzy.专家任务.任务性质.ToString() + ";" + clzy.专家任务.起始途径终止地;
            using (IRepository rep = this.GenerateRepository())
            {
                var list = rep.List<Route>("from Route where RouteKey = :key", new Dictionary<string, object> { { "key", key } });
                if (list.Count > 0)
                    return list[0];
                else
                    return null;
                //return rep.Get<Route>("ID420路线");
            }
        }

        public static TimeSpan? Get预计到达时间(Track nowTrack)
        {
            return Feng.Utils.RouteHelper.GetTimeLeftAccordSampleTrack(nowTrack);
        }
        public static TimeSpan? Get预计动作时间(string importantAreaName, string 作业状态)
        {
            string s = (string)Feng.NameValueMappingCollection.Instance.FindNameFromId("动作时间", importantAreaName + "," + 作业状态);
            if (string.IsNullOrEmpty(s))
                return null;
            else
                return TimeSpan.Parse(s);
        }

        public void 开始监控(车辆作业 车辆作业, DateTime 时间, string 驾驶员编号, string 车载Id)
        {
            using (IRepository rep = this.GenerateRepository())
            {
                try
                {
                    rep.BeginTransaction();

                    Track track = null;
                    if (!string.IsNullOrEmpty(车载Id))
                    {
                        track = new Track();
                        track.Name = "作业" + 车辆作业.作业号;
                        track.VehicleName = 车载Id;
                        track.StartTime = 时间;
                        track.IsActive = true;
                        track.Route = GetDefaultRoute(车辆作业);
                        m_trackDao.Save(rep, track);
                    }

                    车辆作业.驾驶员编号 = 驾驶员编号;
                    车辆作业.开始时间 = 时间;
                    车辆作业.车载Id号 = 车载Id;
                    m_车辆作业Dao.Update(rep, 车辆作业);
                    车辆作业.Track = track == null ? null : (long?)track.ID;

                    作业监控状态 entity = new 作业监控状态();
                    entity.IsActive = true;
                    entity.车辆作业 = 车辆作业;
                    entity.时间 = 时间;
                    更新作业监控状态(rep, entity, 车辆作业.最新作业状态, null, null);
                    if (track != null && track.Route != null && track.Route.Time.HasValue)
                    {
                        entity.预计到达时间 = System.DateTime.Now.AddMinutes(track.Route.Time.Value);
                    }
                    base.Save(rep, entity);

                    rep.CommitTransaction();
                    Update最新状态(车辆作业, entity);
                }
                catch (Exception)
                {
                    rep.RollbackTransaction();
                    throw;
                }
            }
        }

        public void 结束监控(车辆作业 车辆作业, DateTime 时间, string 备注)
        {
            using (IRepository rep = this.GenerateRepository())
            {
                try
                {
                    rep.BeginTransaction();

                    if (车辆作业.Track.HasValue)
                    {
                        Track track = rep.Get<Track>(车辆作业.Track.Value);
                        if (track != null)
                        {
                            track.EndTime = 时间;
                            m_trackDao.Update(rep, track);
                        }
                        else
                        {
                            车辆作业.Track = null;
                        }
                    }

                    车辆作业.结束时间 = 时间;
                    车辆作业.备注 = 备注;
                    m_车辆作业Dao.Update(rep, 车辆作业);

                    作业监控状态 entity = new 作业监控状态();
                    entity.IsActive = true;
                    entity.车辆作业 = 车辆作业;
                    entity.时间 = 时间;
                    更新作业监控状态(rep, entity, 车辆作业.最新作业状态, null, null);

                    base.Save(rep, entity);

                    rep.CommitTransaction();
                    Update最新状态(车辆作业, entity);
                }
                catch (Exception)
                {
                    rep.RollbackTransaction();
                    throw;
                }
            }
        }
    }
}
