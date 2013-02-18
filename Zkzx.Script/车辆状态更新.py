# -*- coding: utf-8 -*-  
import clr
clr.AddReference("System")
clr.AddReference("Feng.Base");
clr.AddReference("Zkzx.Model")
clr.AddReference("Zkzx.Model.Dao")
import Feng;
import Zkzx.Model;

dao = Zkzx.Model.BaseDao[Zkzx.Model.车辆]();
with Feng.ServiceProvider.GetService[Feng.IRepositoryFactory]().GenerateRepository() as rep:
    cls = rep.List[Zkzx.Model.车辆]("from 车辆 where 状态时间止 is not null and 状态时间止 <= :状态时间止", \
        System.Collections.Generic.Dictionary[System.String, System.Object]( { "状态时间止": System.DateTime.Now}));
for i in cls:
    i.当前状态 = Zkzx.Model.车辆当前状态.报停中 if i.当前状态 == Zkzx.Model.车辆当前状态.使用中 else  Zkzx.Model.车辆当前状态.使用中;
    i.状态时间起 = System.DateTime.Now;
    i.状态时间止 = None;
    dao.Update(i);