# -*- coding: utf-8 -*-  
import clr
clr.AddReference("Feng.Base");
clr.AddReference("Feng.Windows");
clr.AddReference("Feng.Server")
clr.AddReference("Feng.Windows.Model")
import Feng

Feng.Server.Utils.SvcServiceCodeHelper.Initialize();

webServiceInfos = Feng.ADInfoBll.Instance.GetInfos[Feng.WebServiceInfo]();
for i in webServiceInfos:
    if (i.Type == Feng.WebServiceType.DataSearchView):
        wintabName = i.ExecuteParam;
        Feng.Server.Utils.SvcServiceCodeHelper.GenerateDataSearchViewWSFiles(wintabName);
    elif (i.Type == Feng.WebServiceType.DataSearch):
        srcType = Feng.Utils.ReflectionHelper.GetTypeFromName(i.ExecuteParam);
        Feng.Server.Utils.SvcServiceCodeHelper.GenerateDataSearchWSFiles(srcType);