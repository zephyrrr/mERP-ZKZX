# -*- coding: utf-8 -*-  
import clr
clr.AddReference("System")
clr.AddReference("Feng.Base");
clr.AddReference("Feng.Windows.Application");
clr.AddReference("Zkzx.Model.Dao")
import Feng;
import Zkzx.Model;

class 专家任务管理(object):
    @staticmethod
    def 撤销专家任务的车辆作业监控(masterForm):
        entity = masterForm.DisplayManager.CurrentItem;
        if (entity == None or entity.车辆作业 == None): 
            Feng.MessageForm.ShowWarning("还未开始监控，不需撤销！");
            return;

        #if (entity.车辆作业.结束时间 != None):
        #    Feng.MessageForm.ShowWarning("专家任务的车辆作业监控已结束，无法撤销！");
        #    return;

        Zkzx.Model.车辆作业Dao().撤销监控(entity.车辆作业);
        Feng.MessageForm.ShowInfo("撤销成功。");
        Feng.Grid.UnBoundGridExtention.ResetRowData(masterForm.MasterGrid, masterForm.MasterGrid.CurrentDataRow);

    @staticmethod
    def 撤销专家任务的车辆作业(masterForm):
        entity = masterForm.DisplayManager.CurrentItem;
        if (entity == None or entity.车辆作业 == None): 
            Feng.MessageForm.ShowWarning("还未有作业，不需撤销！");
            return;

        if (entity.车辆作业.开始时间 != None):
            Feng.MessageForm.ShowWarning("专家任务的车辆作业已开始监控，请先撤销监控！");
            return;

        Zkzx.Model.车辆作业Dao().撤销车辆作业(entity.车辆作业);
        Feng.MessageForm.ShowInfo("撤销成功。");
        Feng.Grid.UnBoundGridExtention.ResetRowData(masterForm.MasterGrid, masterForm.MasterGrid.CurrentDataRow);

    @staticmethod
    def 撤销专家任务下达(masterForm):
        entity = masterForm.DisplayManager.CurrentItem;
        if (entity == None or entity.下达时间 == None):
            Feng.MessageForm.ShowWarning("还未下达，不需撤销！");
            return;

        if (entity.车辆作业 != None):
            Feng.MessageForm.ShowWarning("专家任务已安排车辆作业，请先撤销车辆作业！");
            return;

        entity.下达时间 = None;
        Zkzx.Model.专家任务Dao().Update(entity);
        Feng.MessageForm.ShowInfo("撤销成功。");
        Feng.Grid.UnBoundGridExtention.ResetRowData(masterForm.MasterGrid, masterForm.MasterGrid.CurrentDataRow);