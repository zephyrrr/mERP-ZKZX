using System;
using System.Collections.Generic;
using System.Text;
using Feng;
using Feng.Map;
using Feng.Data;
using Zkzx.Model;

namespace Zkzx.Service
{
    public class AutoUpdateFromGPS
    {
        // ADInfosUtil.exe -p SimulateClzy 车辆作业Id B9718324-3BE5-44A8-9ED8-9FFE00D1E0A6
        public static void SimulateTrackTest(string trackId)
        {
            SimulateTrack(Convert.ToInt64(trackId));
        }
        private static void SimulateTrack(long trackId, Action<TrackPoint> action4TrackPoint = null, Action<WayPoint> action4WayPoint = null)
        {
            System.Console.WriteLine("Start to simulateTrack.");

            var track = TrackDao.GetTrack(trackId);
            DateTime? endDate = track.EndTime;
            track.EndTime = null;
            (new TrackDao()).Update(track);

            var trackPointDao = new TrackPointDao();
            var wayPointDao = new WayPointDao();

            var trackPoints = TrackPointDao.GetTrackPoints(track);
            foreach (var p in trackPoints)
            {
                p.IsActive = false;
                trackPointDao.Update(p);
            }
            var wayPoints = WayPointDao.GetWaypoints(track);
            foreach (var p in wayPoints)
            {
                p.IsActive = false;
                wayPointDao.Update(p);
            }
            int wayPointIdx = 0;

            TrackPoint lastp = null;
            foreach (var p in trackPoints)
            {
                //if (lastp != null && p.GpsTime == lastp.GpsTime)
                //    continue;
                p.IsActive = true;
                trackPointDao.Update(p);

                if (action4TrackPoint != null)
                {
                    action4TrackPoint(p);
                }
                if (wayPointIdx < wayPoints.Count
                    && p.MessageTime >= wayPoints[wayPointIdx].MessageTime)
                {
                    wayPoints[wayPointIdx].IsActive = true;
                    wayPointDao.Update(wayPoints[wayPointIdx]);

                    action4WayPoint(wayPoints[wayPointIdx]);
                    wayPointIdx++;
                }

                System.Console.Write("Press any Key.");
                System.Console.ReadLine();
                //System.Threading.Thread.Sleep(5000);
                System.Console.WriteLine(string.Format("write trackpoint at {0}", p.GpsTime));

                lastp = p;
            }

            track.EndTime = endDate;
            (new TrackDao()).Update(track);
            System.Console.WriteLine("Finish.");
        }
        public static void Simulate车辆作业Test(string 车辆作业Id)
        {
            //System.Console.ReadLine();
            车辆作业 clzy; 
            using (var rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
            {
                clzy = rep.Get<车辆作业>(new Guid(车辆作业Id));
            }

            Simulate车辆作业(clzy);
        }

        public static void Simulate车辆作业(车辆作业 车辆作业)
        {
            if (!车辆作业.Track.HasValue)
                System.Console.WriteLine("There is no track.");

            var sql = new System.Data.SqlClient.SqlCommand("DELETE FROM 业务作业_监控状态 WHERE 车辆作业 = @车辆作业");
            sql.Parameters.AddWithValue("@车辆作业", 车辆作业.ID);
            Feng.Data.DbHelper.Instance.ExecuteNonQuery(sql);

            sql = new System.Data.SqlClient.SqlCommand("DELETE FROM 业务作业_作业异常情况 WHERE 车辆作业 = @车辆作业");
            sql.Parameters.AddWithValue("@车辆作业", 车辆作业.ID);
            Feng.Data.DbHelper.Instance.ExecuteNonQuery(sql);

            var trackId = 车辆作业.Track.Value;

            DBDataBuffer.Instance.LoadData();
            //NameValueMappingCollection.Instance.Reload();

            SimulateTrack(trackId, (trackPoint) =>
                {
                    bool b = m_作业监控Dao.更新作业监控状态1(车辆作业, trackPoint);
                }, (wayPoint) =>
                {

                });
        }

        private static 作业监控Dao m_作业监控Dao = new 作业监控Dao();
        public static void Update2(string vehicleName, TrackPoint tp)
        {
            IList<车辆作业> list;
            using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
            {
                list = rep.List<车辆作业>("结束时间 ISNOTNULL", null);
            }
            foreach (var i in list)
            {
                if (i.车辆.车牌号 == vehicleName)
                {
                    m_作业监控Dao.更新作业监控状态1(i, tp);
                }
            }
        }
    }
}
