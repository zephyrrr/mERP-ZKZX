# -*- coding: utf-8 -*-  
import clr
clr.AddReference("System")
clr.AddReference("System.Windows.Forms")
clr.AddReference("Feng.Base")
clr.AddReference("Feng.Controller")
clr.AddReference("Feng.View")
clr.AddReference("Feng.Windows.Forms")
clr.AddReference("Feng.Grid")
from Feng import IDisplayManagerContainer, IControlManagerContainer, IControlWrapper
from Feng.Windows.Forms import MyTextBox, MyDateTimePicker, MyDatePicker, MyComboBox, MyFreeComboBox, MyNumericTextBox,MyDateTimeTextBox
import System

def getDm(masterForm):
    if isinstance(masterForm,IControlManagerContainer):
        dm = masterForm.ControlManager.DisplayManager;
    elif isinstance(masterform, IDisplayManagerContainer):
        dm = masterForm.DisplayManager;
    else:
        dm = None;
    return dm;

def setDefaultValue(dc, c):
	if (isinstance(c, MyTextBox)):
		dc.SelectedDataValue = "sample";
	elif (isinstance(c, MyDateTimePicker) or isinstance(c, MyDatePicker) or isinstance(c, MyDateTimeTextBox)):
		if (dc.Name.Contains('止')):
			dc.SelectedDataValue = System.DateTime.Now.AddDays(1);
		elif (dc.Name.Contains('还箱进港')):
			dc.SelectedDataValue = System.DateTime.Now.AddMonths(1);
		else:
			dc.SelectedDataValue = System.DateTime.Now;
	elif (isinstance(c, MyComboBox) or isinstance(c, MyFreeComboBox)):
		for i in c.DropDownControl.DataRows:
			if (i.Visible and i.Height > 0):
				c.SelectedIndex = i.Index;
				break;
	elif (isinstance(c, MyNumericTextBox)):
		dc.SelectedDataValue = 1;

def setAllValues(dm):
    for dc in dm.DataControls:
        if (isinstance(dc, IControlWrapper)):
            c = dc.Control;
        else:
            c = dc;
        if (c.ReadOnly):
            continue;
        if (dc.SelectedDataValue != None):
            continue;
        
        if (dc.Name == '箱号'):
            dc.SelectedDataValue = 'ABCD1234567';
        elif (dc.Name == '船名航次'):
            dc.SelectedDataValue = 'S1/S2';   
        elif (dc.Name.Contains('座机')):
            dc.SelectedDataValue = '88888888';
        elif (dc.Name.Contains('手机')):
            dc.SelectedDataValue = '13986868686';
        else:
            setDefaultValue(dc, c);
