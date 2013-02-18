# -*- coding: utf-8 -*-  
import sys;
sys.path.append('.\AdminScript');
import clr
clr.AddReference('System')
clr.AddReference('System.Core')
from System import Action, Func;
import System;

from 任务备案_Func import getDm, setAllValues;
clr.AddReference('Zkzx.Model');
import Zkzx.Model;
clr.AddReference("Feng.Windows");
import Feng;
clr.AddReference("System.Windows.Forms");
from System.Windows.Forms import Button;

btnFunc1 = Func[Button, System.Boolean](lambda btn: btn.Text == '新增任务');
btn1 = Feng.Utils.UserControlHelper.SearchChildControl[Button](masterForm, btnFunc1);
btnFunc2 = Func[Button, System.Boolean](lambda btn: btn.Text == '预录入发送');
btn2 = Feng.Utils.UserControlHelper.SearchChildControl[Button](masterForm, btnFunc2);
dm = getDm(masterForm);

n = 0;

for i in range(0,5):
    btn1.PerformClick();
    dm.DataControls["委托人编号"].SelectedDataValue = '900001';
    dm.DataControls["任务性质"].SelectedDataValue = Zkzx.Model.任务性质.进口拆箱;
    dm.DataControls["箱型编号"].SelectedDataValue = 40;
    dm.DataControls["提箱点编号"].SelectedDataValue = '900002';
    dm.DataControls["还箱进港点编号"].SelectedDataValue = '900075';
    dm.DataControls["卸货地编号"].SelectedDataValue = '311400';
    dm.DataControls["备注"].SelectedDataValue = n.ToString(); n = n + 1;
    setAllValues(dm);
    btn2.PerformClick();

for i in range(0,5):
    btn1.PerformClick();
    dm.DataControls["委托人编号"].SelectedDataValue = '900001';
    dm.DataControls["任务性质"].SelectedDataValue = Zkzx.Model.任务性质.出口装箱;
    dm.DataControls["箱型编号"].SelectedDataValue = 40;
    dm.DataControls["提箱点编号"].SelectedDataValue = '900075';
    dm.DataControls["还箱进港点编号"].SelectedDataValue = '900002';
    dm.DataControls["装货地编号"].SelectedDataValue = '311400';
    dm.DataControls["备注"].SelectedDataValue = n.ToString(); n = n + 1;
    setAllValues(dm);
    btn2.PerformClick();

for i in range(0,2):
    btn1.PerformClick();
    dm.DataControls["委托人编号"].SelectedDataValue = '900001';
    dm.DataControls["任务性质"].SelectedDataValue = Zkzx.Model.任务性质.I带货;
    dm.DataControls["箱型编号"].SelectedDataValue = 40;
    dm.DataControls["装货地编号"].SelectedDataValue = '311400';
    dm.DataControls["卸货地编号"].SelectedDataValue = '311400';
    dm.DataControls["备注"].SelectedDataValue = n.ToString(); n = n + 1;
    setAllValues(dm);
    btn2.PerformClick();

for i in range(0,2):
    btn1.PerformClick();
    dm.DataControls["委托人编号"].SelectedDataValue = '900001';
    dm.DataControls["任务性质"].SelectedDataValue = Zkzx.Model.任务性质.E带货;
    dm.DataControls["装货地编号"].SelectedDataValue = '311400';
    dm.DataControls["卸货地编号"].SelectedDataValue = '311400';
    dm.DataControls["备注"].SelectedDataValue = n.ToString(); n = n + 1;
    setAllValues(dm);
    btn2.PerformClick();

for i in range(0,6):
    btn1.PerformClick();
    dm.DataControls["委托人编号"].SelectedDataValue = '900001';
    dm.DataControls["任务性质"].SelectedDataValue = Zkzx.Model.任务性质.进口拆箱;
    dm.DataControls["箱型编号"].SelectedDataValue = 20;
    dm.DataControls["提箱点编号"].SelectedDataValue = '900002';
    dm.DataControls["还箱进港点编号"].SelectedDataValue = '900075';
    dm.DataControls["卸货地编号"].SelectedDataValue = '311400';
    dm.DataControls["备注"].SelectedDataValue = n.ToString(); n = n + 1;
    setAllValues(dm);
    btn2.PerformClick();

for i in range(0,6):
    btn1.PerformClick();
    dm.DataControls["委托人编号"].SelectedDataValue = '900001';
    dm.DataControls["任务性质"].SelectedDataValue = Zkzx.Model.任务性质.出口装箱;
    dm.DataControls["箱型编号"].SelectedDataValue = 20;
    dm.DataControls["提箱点编号"].SelectedDataValue = '900075';
    dm.DataControls["还箱进港点编号"].SelectedDataValue = '900002';
    dm.DataControls["装货地编号"].SelectedDataValue = '311400';
    dm.DataControls["备注"].SelectedDataValue = n.ToString(); n = n + 1;
    setAllValues(dm);
    btn2.PerformClick();


