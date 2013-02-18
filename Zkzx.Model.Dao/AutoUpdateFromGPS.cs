using System;
using System.Collections.Generic;
using System.Text;
using Feng;
using Feng.Map;
using Feng.Data;

namespace Zkzx.Model
{
    public class AutoUpdateFromGPS
    {
        // ADInfosUtil.exe -p SimulateClzy 车辆作业Id B9718324-3BE5-44A8-9ED8-9FFE00D1E0A6
        public static void SimulateTrackTest(string trackId)
        {
            SimulateTrack(Convert.ToInt64(trackId));
        }
        private static void SimulateTrack(long trackId, Action<TrackPoint> action = null)
        {
            System.Console.WriteLine("Start to simulateTrack.");

            var track = TrackDao.GetTrack(trackId);
            DateTime? endDate = track.EndTime;
            track.EndTime = null;
            (new TrackDao()).Update(track);

            var trackPointDao = new TrackPointDao();
            var point = TrackPointDao.GetTrackPoints(track);
            foreach (var p in point)
            {
                p.IsActive = false;
                trackPointDao.Update(p);
            }
            TrackPoint lastp = null;
            foreach (var p in point)
            {
                if (lastp != null && p.GpsTime == lastp.GpsTime)
                    continue;
                p.IsActive = true;
                trackPointDao.Update(p);

                if (action != null)
                {
                    action(p);
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
            sql.Parameters.AddWithValue("@车辆作业", 车辆作业.Id);
            Feng.Data.DbHelper.Instance.ExecuteNonQuery(sql);

            sql = new System.Data.SqlClient.SqlCommand("DELETE FROM 业务作业_作业异常情况 WHERE 车辆作业 = @车辆作业");
            sql.Parameters.AddWithValue("@车辆作业", 车辆作业.Id);
            Feng.Data.DbHelper.Instance.ExecuteNonQuery(sql);

            var trackId = 车辆作业.Track.Value;

            DBDataBuffer.Instance.LoadData();
            //NameValueMappingCollection.Instance.Reload();

            SimulateTrack(trackId, (trackPoint) =>
                {
                    using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
                    {
                        bool b = m_作业监控Dao.更新作业监控状态(rep, 车辆作业, trackPoint);
                    }
                });
        }
        private static 作业监控Dao m_作业监控Dao = new 作业监控Dao();
        public static void Update()
        {
            using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<车辆作业>())
            {
                IList<车辆作业> list = rep.List<车辆作业>("结束时间 ISNOTNULL", null);
                foreach (var i in list)
                {
                    m_作业监控Dao.更新作业监控状态(i);
                }
            }
        }
    }
}
