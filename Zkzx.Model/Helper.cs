using System;
using System.Collections.Generic;
using System.Text;

namespace Zkzx.Model
{
    public static class ModelHelper
    {
        public static string Get任务状态(专家任务 zjrw, out int[] taskIdx, out string[] importantAreas,
            out string[] importantTaskStatus, out string[] importantWorkStatus)
        {
            IList<任务> tasks = zjrw.任务;

            taskIdx = null;
            importantAreas = null;
            importantTaskStatus = null;
            importantWorkStatus = null;
            string workTitle = null;

            // Color: Blue, Gray, ControlText
            switch (zjrw.任务性质)
            {
                case 专家任务性质.静态优化套箱:
                case 专家任务性质.动态优化套箱:
                    {
                        System.Diagnostics.Debug.Assert(tasks.Count == 2);
                        System.Diagnostics.Debug.Assert(tasks[0].任务性质 == 任务性质.进口拆箱);
                        System.Diagnostics.Debug.Assert(tasks[1].任务性质 == 任务性质.出口装箱);

                        if (!zjrw.任务[0].转关箱标志.HasValue ||
                            zjrw.任务[0].转关箱标志.Value == 转关箱标志.非转关)
                        {
                            workTitle = "套箱（清关箱）";
                            taskIdx = new int[] { 0, 0, 1, 1 };
                            importantAreas = new string[] { tasks[0].提箱点编号, tasks[0].卸货地编号, tasks[1].装货地编号, tasks[1].还箱进港点编号 };
                            importantTaskStatus = new string[] { "0/进口拆箱-2/ 出口装箱", "0/进口拆箱-2/ 出口装箱", "1/进口拆箱-0/ 出口装箱", "1/进口拆箱-0/ 出口装箱" };
                            importantWorkStatus = new string[] { "港区提箱", "卸货", "出口装货", "进港" };
                        }
                        else
                        {
                            workTitle = "套箱（转关箱）";
                            taskIdx = new int[] { 0, 0, 0, 0, 1, 1 };
                            importantAreas = new string[] { tasks[0].提箱点编号, 
                                Feng.Utils.NameValueControlHelper.GetMultiString("港区指运地_施封地", tasks[0].提箱点编号),
                                Feng.Utils.NameValueControlHelper.GetMultiString("港区指运地_验封地", tasks[0].卸货地编号),
                                tasks[0].卸货地编号, tasks[1].装货地编号, tasks[1].还箱进港点编号 };
                            importantTaskStatus = new string[] { "0/进口拆箱-2/ 出口装箱", 
                                "0/进口拆箱-2/ 出口装箱", 
                                "0/进口拆箱-2/ 出口装箱", 
                                "0/进口拆箱-2/ 出口装箱", "1/进口拆箱-0/ 出口装箱", "1/进口拆箱-0/ 出口装箱" };
                            importantWorkStatus = new string[] { "港区提箱", "施关封", "验关封", "卸货", "出口装货", "进港" };
                        }
                    }
                    break;
                case 专家任务性质.静态优化进口箱带货:
                case 专家任务性质.动态优化进口箱带货:
                    {
                        System.Diagnostics.Debug.Assert(tasks.Count == 2);
                        System.Diagnostics.Debug.Assert(tasks[0].任务性质 == 任务性质.进口拆箱);
                        System.Diagnostics.Debug.Assert(tasks[1].任务性质 == 任务性质.I带货);

                        if (!zjrw.任务[0].转关箱标志.HasValue ||
                            zjrw.任务[0].转关箱标志.Value == 转关箱标志.非转关)
                        {
                            workTitle = "进口箱带货（清关箱）";
                            taskIdx = new int[] { 0, 0, 1, 1, 0 };
                            importantAreas = new string[] { tasks[0].提箱点编号, tasks[0].卸货地编号, tasks[1].装货地编号, tasks[1].卸货地编号, tasks[0].还箱进港点编号 };
                            importantTaskStatus = new string[] { "0/进口拆箱-2/ I带货", "0/进口拆箱-2/ I带货", "2/进口拆箱-0/ I带货", "2/进口拆箱-0/ I带货", "0/进口拆箱-1/ I带货" };
                            importantWorkStatus = new string[] { "港区提箱", "卸货", "带货装货", "带货卸货", "还箱" };
                        }
                        else
                        {
                            workTitle = "进口箱带货（转关箱）";
                            taskIdx = new int[] { 0, 0, 0, 0, 1, 1, 0 };
                            importantAreas = new string[] { tasks[0].提箱点编号, 
                                Feng.Utils.NameValueControlHelper.GetMultiString("港区指运地_施封地", tasks[0].提箱点编号),
                                Feng.Utils.NameValueControlHelper.GetMultiString("港区指运地_验封地", tasks[0].卸货地编号),
                                tasks[0].卸货地编号, tasks[1].装货地编号, tasks[1].卸货地编号, tasks[0].还箱进港点编号 };
                            importantTaskStatus = new string[] { "0/进口拆箱-2/ I带货", 
                                "0/进口拆箱-2/ I带货", 
                                "0/进口拆箱-2/ I带货", 
                                "0/进口拆箱-2/ I带货", "2/进口拆箱-0/ I带货", "2/进口拆箱-0/ I带货", "0/进口拆箱-1/ I带货" };
                            importantWorkStatus = new string[] { "港区提箱", "施关封", "验关封", "卸货", "带货装货", "带货卸货", "还箱" };
                        }
                    }
                    break;
                case 专家任务性质.静态优化出口箱带货:
                case 专家任务性质.动态优化出口箱带货:
                    {
                        System.Diagnostics.Debug.Assert(tasks.Count == 2);
                        System.Diagnostics.Debug.Assert(tasks[0].任务性质 == 任务性质.出口装箱);
                        System.Diagnostics.Debug.Assert(tasks[1].任务性质 == 任务性质.E带货);

                        workTitle = "出口箱带货";
                        taskIdx = new int[] { 0, 1, 1, 0, 0 };
                        importantAreas = new string[] { tasks[0].提箱点编号, tasks[1].装货地编号, tasks[1].卸货地编号, tasks[0].装货地编号, tasks[0].还箱进港点编号 };
                        importantTaskStatus = new string[] { "0/出口装箱-2/ E带货", "2/出口装箱-0/ E带货", "2/出口装箱-0/ E带货", "0/出口装箱-1/ E带货", "0/出口装箱-1/ E带货" };
                        importantWorkStatus = new string[] { "堆场提箱", "带货装货", "带货卸货", "装货", "进港" };
                    }
                    break;
                case 专家任务性质.静态优化进口箱对箱:
                case 专家任务性质.动态优化进口箱对箱:
                    {
                        workTitle = "进口箱对箱";
                        System.Diagnostics.Debug.Assert(tasks.Count == 2);
                        System.Diagnostics.Debug.Assert(tasks[0].任务性质 == 任务性质.进口拆箱);
                        System.Diagnostics.Debug.Assert(tasks[1].任务性质 == 任务性质.进口拆箱);

                        taskIdx = new int[] { 0, 1, 0, 1, 0, 1 };
                        importantAreas = new string[] { tasks[0].提箱点编号, tasks[1].提箱点编号, tasks[0].卸货地编号, tasks[1].卸货地编号, tasks[0].还箱进港点编号, tasks[1].还箱进港点编号 };
                        importantTaskStatus = new string[] { "0/进口拆箱-2/ 进口拆箱", "2/进口拆箱-0/ 进口拆箱", "0/进口拆箱-2/ 进口拆箱", "2/进口拆箱-0/ 进口拆箱", "0/进口拆箱-2/ 进口拆箱", "2/进口拆箱-0/ 进口拆箱" };
                        importantWorkStatus = new string[] { "港区提箱1", "港区提箱2", "卸货1", "卸货2", "还箱1", "还箱2" };
                    }
                    break;
                case 专家任务性质.静态优化出口箱对箱:
                case 专家任务性质.动态优化出口箱对箱:
                    {
                        System.Diagnostics.Debug.Assert(tasks.Count == 2);
                        System.Diagnostics.Debug.Assert(tasks[0].任务性质 == 任务性质.出口装箱);
                        System.Diagnostics.Debug.Assert(tasks[1].任务性质 == 任务性质.出口装箱);

                        workTitle = "出口箱对箱";
                        taskIdx = new int[] { 0, 1, 0, 1, 0, 1 };
                        importantAreas = new string[] { tasks[0].提箱点编号, tasks[1].提箱点编号, tasks[0].装货地编号, tasks[1].装货地编号, tasks[0].还箱进港点编号, tasks[1].还箱进港点编号 };
                        importantTaskStatus = new string[] { "0/出口装箱-2/ 出口装箱", "2/出口装箱-0/ 出口装箱", "0/出口装箱-2/ 出口装箱", "2/出口装箱-0/ 出口装箱", "0/出口装箱-2/ 出口装箱", "2/出口装箱-0/ 出口装箱" };
                        importantWorkStatus = new string[] { "堆场提箱1", "堆场提箱2", "装货1", "装货2", "进港1", "进港2" };
                    }
                    break;
                case 专家任务性质.静态优化进出口对箱:
                case 专家任务性质.动态优化进出口对箱:
                    {
                        System.Diagnostics.Debug.Assert(tasks.Count == 2);
                        System.Diagnostics.Debug.Assert(tasks[0].任务性质 == 任务性质.进口拆箱);
                        System.Diagnostics.Debug.Assert(tasks[1].任务性质 == 任务性质.出口装箱);

                        workTitle = "进出口箱对箱";
                        taskIdx = new int[] { 0, 1, 0, 1, 0, 1 };
                        importantAreas = new string[] { tasks[0].提箱点编号, tasks[1].提箱点编号, tasks[0].卸货地编号, tasks[1].装货地编号, tasks[0].还箱进港点编号, tasks[1].还箱进港点编号 };
                        importantTaskStatus = new string[] { "0/进口拆箱-2/ 出口装箱", "2/进口拆箱-0/ 出口装箱", "0/进口拆箱-2/ 出口装箱", "2/进口拆箱-0/ 出口装箱", "0/进口拆箱-2/ 出口装箱", "2/进口拆箱-0/ 出口装箱" };
                        importantWorkStatus = new string[] { "港区提箱", "堆场提箱", "卸货", "装货", "还箱", "进港" };
                    }
                    break;
                case 专家任务性质.无优化进口拆箱:
                    {
                        System.Diagnostics.Debug.Assert(tasks.Count == 1);
                        System.Diagnostics.Debug.Assert(tasks[0].任务性质 == 任务性质.进口拆箱);

                        if (!zjrw.任务[0].转关箱标志.HasValue ||
                            zjrw.任务[0].转关箱标志.Value == 转关箱标志.非转关)
                        {
                            workTitle = "进口拆箱（清关箱）";
                            taskIdx = new int[] { 0, 0, 0 };
                            importantAreas = new string[] { tasks[0].提箱点编号, tasks[0].卸货地编号, tasks[0].还箱进港点编号 };
                            importantTaskStatus = new string[] { "0/进口拆箱", "0/进口拆箱", "0/进口拆箱" };
                            importantWorkStatus = new string[] { "港区提箱", "卸货", "还箱" };
                        }
                        else
                        {
                            workTitle = "进口拆箱（转关箱）";
                            taskIdx = new int[] { 0, 0, 0, 0, 0 };
                            importantAreas = new string[] { tasks[0].提箱点编号, 
                                Feng.Utils.NameValueControlHelper.GetMultiString("港区指运地_施封地", tasks[0].提箱点编号),
                                Feng.Utils.NameValueControlHelper.GetMultiString("港区指运地_验封地", tasks[0].卸货地编号),
                                tasks[0].卸货地编号, tasks[0].还箱进港点编号 };
                            importantTaskStatus = new string[] { "0/进口拆箱", 
                                "0/进口拆箱", 
                                "0/进口拆箱", 
                                "0/进口拆箱", "0/进口拆箱" };
                            importantWorkStatus = new string[] { "港区提箱", "施关封", "验关封", "卸货", "还箱" };
                        }
                    }
                    break;
                case 专家任务性质.无优化出口装箱:
                    {
                        System.Diagnostics.Debug.Assert(tasks.Count == 1);
                        System.Diagnostics.Debug.Assert(tasks[0].任务性质 == 任务性质.出口装箱);

                        workTitle = "出口装箱";
                        taskIdx = new int[] { 0, 0, 0 };
                        importantAreas = new string[] { tasks[0].提箱点编号, tasks[0].装货地编号, tasks[0].还箱进港点编号 };
                        importantTaskStatus = new string[] { "0/出口装箱", "0/出口装箱", "0/出口装箱" };
                        importantWorkStatus = new string[] { "堆场提箱", "装货", "进港" };
                    }
                    break;
                case 专家任务性质.无优化I带货:
                    {
                        System.Diagnostics.Debug.Assert(tasks.Count == 1);
                        System.Diagnostics.Debug.Assert(tasks[0].任务性质 == 任务性质.I带货);

                        workTitle = "I带货";
                        taskIdx = new int[] { 0, 0 };
                        importantAreas = new string[] { tasks[0].装货地编号, tasks[0].卸货地编号 };
                        importantTaskStatus = new string[] { "0/I带货", "0/I带货" };
                        importantWorkStatus = new string[] { "装货", "卸货" };
                    }
                    break;
                case 专家任务性质.无优化E带货:
                    {
                        System.Diagnostics.Debug.Assert(tasks.Count == 1);
                        System.Diagnostics.Debug.Assert(tasks[0].任务性质 == 任务性质.E带货);

                        workTitle = "E带货";
                        taskIdx = new int[] { 0, 0 };
                        importantAreas = new string[] { tasks[0].装货地编号, tasks[0].卸货地编号 };
                        importantTaskStatus = new string[] { "0/E带货", "0/E带货" };
                        importantWorkStatus = new string[] { "装货", "卸货" };
                    }
                    break;
                default:
                    throw new InvalidOperationException("不合理的专家任务性质!");
            }

            System.Diagnostics.Debug.Assert(importantAreas.Length == importantTaskStatus.Length && importantTaskStatus.Length == importantWorkStatus.Length,
                            "important字段程度必须一致！");
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(workTitle), "必须设置作业标题。");

            return workTitle;
        }
    }
}
