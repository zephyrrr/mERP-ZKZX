# -*- coding: utf-8 -*-  
import clr
clr.AddReference("System")
clr.AddReference("Feng.Base");
clr.AddReference("Feng.Net");
clr.AddReference("Feng.Data");
clr.AddReference("Feng.GPS");
clr.AddReference("Zkzx.Model")
clr.AddReference("Zkzx.Model.Dao")
import System;
import Feng;
import Zkzx.Model;

m_webProxy = Feng.Net.WebProxy();
m_webProxy.Encoding = System.Text.Encoding.UTF8;
m_webProxy.ContentType = "application/json";
m_webProxy.Accept = "application/json";

m_host = "192.168.0.10";
#m_host = "localhost";
workId = 'ZOI12120000011'

with Feng.ServiceProvider.GetService[Feng.IRepositoryFactory]().GenerateRepository() as rep:
    clzy = rep.List[Zkzx.Model.车辆作业]("from 车辆作业 where 作业号 = '" + workId + "'")[0];
    if (clzy.开始时间 == None):
        raise Exception('invalid work', 'work')
    m_truckId = clzy.车载Id号;
    key = clzy.专家任务.任务性质.ToString() + ";" + clzy.专家任务.起始途径终止地;
    routes = rep.List[Feng.Route]("from Route where RouteKey = '" + key + "'");
    gpsFile = "d:\\" + clzy.作业号 + ".csv";
    if (routes.Count == 0):
        route = Feng.Route();
        route.Name = "S:" + workId;
        route.RouteKey = key;
        route.SampleTrack = clzy.Track;
        dao = Zkzx.Model.BaseDao[Feng.Route]();
        rep.BeginTransaction();
        dao.Save(rep, route);
        rep.CommitTransaction();
    print "TrackId = ", clzy.Track;

if (not System.IO.File.Exists(gpsFile)):
    ps = Feng.TrackPointDao.GetTrackPoints(Feng.TrackDao.GetTrack(clzy.Track));
    sw = System.IO.StreamWriter(gpsFile)
    sw.WriteLine("Time, X, Y");
    for i in ps:
        sw.WriteLine(System.String.Format("{0}, {2}, {1}, {3}, {4}, {5}, {6}", i.GpsTime.ToString("yyyy-MM-ddTHH:mm:ss"), i.Latitude, i.Longitude, i.Accuracy, i.Altitude, i.Heading, i.Speed));
    sw.Close();

Feng.Data.DbHelper.Instance.ExecuteNonQuery("delete from 业务作业_监控状态 where 车辆作业 = '" + clzy.ID.ToString() + "'");
Feng.Data.DbHelper.Instance.ExecuteNonQuery("delete from 业务作业_作业异常情况 where 车辆作业 = '" + clzy.ID.ToString() + "'");
Feng.Data.DbHelper.Instance.ExecuteNonQuery("delete from 业务作业_动作时间数据 where 车辆作业 = '" + clzy.ID.ToString() + "'");
Feng.Data.DbHelper.Instance.ExecuteNonQuery("delete from sd_track_point where Track = '" + clzy.Track.ID.ToString() + "'");
Feng.Data.DbHelper.Instance.ExecuteNonQuery("delete from sd_way_point where Track = '" + clzy.Track.ID.ToString() + "'");

sr = System.IO.StreamReader(gpsFile)
lastTime = System.DateTime.MinValue;
while(True):
    if (sr.EndOfStream):
        break;
    s = sr.ReadLine();
    if (System.String.IsNullOrEmpty(s)):
        continue;
    ss = s.Split(',');
    if (ss[0] == "Time"):
        continue;
    time = System.Convert.ToDateTime(ss[0]);
    if ((time - lastTime).TotalSeconds < 60):
        continue;
    gpsData = System.String.Format("{0},{1},{2},{3},{4},{5},{6}", ss[0], ss[2], ss[1], ss[3], ss[4], ss[5], ss[6]);
    postData = System.String.Format('{0}"gpsData":"{2}"{1}', "{", "}", gpsData);
    print postData
    ret = m_webProxy.PostToString(System.String.Format("http://{0}/CarTrackService/ZkzxDataService.svc/{1}/SendTrackPoint", m_host, m_truckId), postData);
sr.Close();

Feng.Utils.RouteHelper.GenerateRouteSampleTracks("S:" + workId);



