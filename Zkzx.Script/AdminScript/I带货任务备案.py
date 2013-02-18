# -*- coding: utf-8 -*-  
import sys;
sys.path.append('.\AdminScript');
import clr
from 任务备案_Func import getDm, setAllValues;
clr.AddReference('Zkzx.Model');
import Zkzx.Model;

dm = getDm(masterForm);
dm.DataControls["任务性质"].SelectedDataValue = Zkzx.Model.任务性质.I带货;
dm.DataControls["装货地编号"].SelectedDataValue = '311400';
dm.DataControls["卸货地编号"].SelectedDataValue = '311400';
setAllValues(dm);
