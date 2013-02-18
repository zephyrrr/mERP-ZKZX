# -*- coding: utf-8 -*-  
import clr
clr.AddReference("System")
clr.AddReference("Feng.Base");
clr.AddReference("Feng.Windows.Application");
clr.AddReference("Zkzx.Model.Dao")
import Feng;
import Zkzx.Model;

class 任务管理(object):
    @staticmethod
    def 撤销任务预发送(masterForm):
        entity = masterForm.DisplayManager.CurrentItem;
        if (entity == None or not entity.IsActive): 
            Feng.MessageForm.ShowWarning("还未发送，不需撤销！");
            return;

        if (entity.任务号 != None):
            Feng.MessageForm.ShowWarning("任务已生成任务号，请先撤销备案确认！");
            return;

        entity.IsActive = False;
        Zkzx.Model.任务Dao().Update(entity);
        Feng.MessageForm.ShowInfo("撤销成功。");
        Feng.Grid.UnBoundGridExtention.ResetRowData(masterForm.MasterGrid, masterForm.MasterGrid.CurrentDataRow);

    @staticmethod
    def 撤销任务备案确认(masterForm):
        entity = masterForm.DisplayManager.CurrentItem;
        if (entity == None or entity.任务号 == None): 
            Feng.MessageForm.ShowWarning("还未确认，不需撤销！");
            return;

        if (entity.专家任务 != None):
            Feng.MessageForm.ShowWarning("任务已安排专家任务，请先撤销专家任务！");
            return;

        entity.任务号 = None;
        Zkzx.Model.任务Dao().Update(entity);
        Feng.MessageForm.ShowInfo("撤销成功。");
        Feng.Grid.UnBoundGridExtention.ResetRowData(masterForm.MasterGrid, masterForm.MasterGrid.CurrentDataRow);

    @staticmethod
    def 撤销任务的专家任务(masterForm):
        entity = masterForm.DisplayManager.CurrentItem;
        if (entity == None or entity.专家任务 == None):
            Feng.MessageForm.ShowWarning("还未有专家任务，不需撤销！");
            return;

        if (entity.专家任务.车辆作业 != None):
            Feng.MessageForm.ShowWarning("任务专家任务已安排车辆作业，请先撤销车辆作业！");
            return;
        if (entity.专家任务.下达时间 != None):
            Feng.MessageForm.ShowWarning("任务专家任务已下达，请先撤销专家任务下达！");
            return;

        Zkzx.Model.专家任务Dao().撤销专家任务(entity);
        Feng.MessageForm.ShowInfo("撤销成功。");
        Feng.Grid.UnBoundGridExtention.ResetRowData(masterForm.MasterGrid, masterForm.MasterGrid.CurrentDataRow);