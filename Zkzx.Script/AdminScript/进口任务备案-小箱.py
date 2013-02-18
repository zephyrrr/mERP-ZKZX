# -*- coding: utf-8 -*-  
import sys;
sys.path.append('.\AdminScript');
import clr
from 任务备案_Func import getDm, setAllValues;
clr.AddReference('Zkzx.Model');
import Zkzx.Model;

dm = getDm(masterForm);
dm.DataControls["任务性质"].SelectedDataValue = Zkzx.Model.任务性质.进口拆箱;
dm.DataControls["箱型编号"].SelectedDataValue = 20;
dm.DataControls["提箱点编号"].SelectedDataValue = '900002';
dm.DataControls["还箱进港点编号"].SelectedDataValue = '900075';
dm.DataControls["卸货地编号"].SelectedDataValue = '311400';
setAllValues(dm);
