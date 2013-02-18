# -*- coding: UTF-8 -*-  
import clr
clr.AddReference("Feng.Data");
import Feng;

ss = ['delete 业务作业_作业异常情况', \
    'delete 业务作业_监控状态', \
    'delete 业务作业_动作时间数据', \
    'update 业务备案_专家任务 set 车辆作业 = null', \
    'delete 业务作业_车辆作业', \
    'delete 业务备案_转关箱排车计划', \
    'update 业务备案_任务 set 专家任务 = null, 任务号 = null, 预录入号 = null', \
    'delete 业务备案_专家任务', \
    'delete 业务作业_作业异常情况', \
    'delete 业务备案_任务', \
    'delete 业务备案_进口票', \
    ];
for i in ss:
    Feng.Data.DbHelper.Instance.ExecuteNonQuery(i);

