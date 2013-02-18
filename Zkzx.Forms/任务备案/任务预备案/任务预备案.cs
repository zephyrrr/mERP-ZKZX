using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Feng;
using Feng.Grid;
using Feng.Utils;
using Feng.Windows.Forms;
using Zkzx.Model;

namespace Zkzx.Forms
{
    /// <summary>
    /// 只用于备案界面Save函数
    /// </summary>
    internal enum SaveType
    {
        暂存待确认, 预录入发送, 正式备案确认, 拒绝确认
    }

    public partial class 任务预备案 : Feng.Windows.Forms.MyTemplateForm, IControlManagerContainer, IGridNamesContainer, IRefreshDataForm
    {
        IControlManager IControlManagerContainer.ControlManager
        {
            get { return m_cm; }
        }
        public string[] GridName
        {
            get { return new string[] { m_gridName }; }
        }

        public 任务预备案()
        {
            InitializeComponent();
        }


        private IWindowControlManager m_cm;
        private IWindowControlManager<任务> m_cmT;
        private string m_gridName = "实体类_任务";
        private ArchiveUnboundGrid m_rightGrid;

        private void 任务预备案_Load(object sender, EventArgs e)
        {
            m_rightGrid = base.AssociateArchiveGrid(pnl待确认区, "任务备案_任务预备案") as ArchiveUnboundGrid;
            var cm = m_rightGrid.ControlManager;
            m_cm = m_rightGrid.ControlManager as IWindowControlManager;
            m_cmT = m_cm as IWindowControlManager<任务>;

            MyTemplateForm.AddControl(pnl备案明细窗体, new 备案明细窗体(cm, m_gridName));
            cm.DisplayManager.DataControls["装货地编号"].SelectedDataValueChanged += new EventHandler(任务预备案_SelectedDataValueChanged);
            cm.DisplayManager.DataControls["卸货地编号"].SelectedDataValueChanged += new EventHandler(任务预备案_SelectedDataValueChanged);

            cm.StateControls.Add(new StateControl(btn预录入发送, false));
            cm.StateControls.Add(new StateControl(btn暂存待确认, false));
            cm.StateControls.Add(new StateControl(btn删除, StateType.View));
            cm.StateControls.Add(new StateControl(btn新增任务, true));
            cm.StateControls.Add(new StateControl(btn修改, StateType.View));
            cm.StateControls.Add(new StateControl(btn文件导入, true));

            cm.DisplayManager.PositionChanged += new EventHandler(DisplayManager_PositionChanged);
            cm.DisplayManager.PositionChanging += new CancelEventHandler(DisplayManager_PositionChanging);

            //MyTemplateForm.RestrictToUserAccess(m_cm.DisplayManager.SearchManager, "备案主管");

            Helper.SetGridDefault(this, m_rightGrid);

            cm.DisplayManager.SearchManager.LoadData();
        }

        void 任务预备案_SelectedDataValueChanged(object sender, EventArgs e)
        {
            IDataControl dc = sender as IDataControl;
            if (dc.Name == "装货地编号")
            {
                if (m_cm.DisplayManager.DataControls["装货地详细地址"].SelectedDataValue == null)
                {
                    m_cm.DisplayManager.DataControls["装货地详细地址"].SelectedDataValue = 
                        Feng.NameValueMappingCollection.Instance.FindColumn2FromColumn1("人员单位_装卸货地", "编号", "全称", dc.SelectedDataValue);
                }
            }
            else if (dc.Name == "卸货地编号")
            {
                if (m_cm.DisplayManager.DataControls["卸货地详细地址"].SelectedDataValue == null)
                {
                    m_cm.DisplayManager.DataControls["卸货地详细地址"].SelectedDataValue =
                        Feng.NameValueMappingCollection.Instance.FindColumn2FromColumn1("人员单位_装卸货地", "编号", "全称", dc.SelectedDataValue);
                }
            }
        }

        void DisplayManager_PositionChanged(object sender, EventArgs e)
        {
            // 在内部已经Display
            //m_cm.DisplayManager.DisplayCurrent();

            // 还未完全EndEdit就开始BeginEdit，错误
            //if (m_cm.DisplayManager.CurrentItem != null)
            //{
            //    m_cm.EditCurrent();
            //}
            任务 entity = m_cmT.DisplayManagerT.CurrentEntity;
            if (entity == null)
                return;

            if (entity.IsActive)
            {
                btn删除.Enabled = false;
                btn修改.Enabled = false;
            }
            else
            {
                btn删除.Enabled = true;
                btn修改.Enabled = true;
            }
        }

        void DisplayManager_PositionChanging(object sender, CancelEventArgs e)
        {
            e.Cancel = !m_cm.TryCancelEdit();
        }

        public void RefreshData()
        {
            m_rightGrid.ReloadData();
        }

        private void btn新增任务_Click(object sender, EventArgs e)
        {
            m_cm.AddNew();
            UpdateContent(m_cm, m_gridName);

            任务 entity = m_cmT.DisplayManagerT.CurrentEntity;
            entity.任务来源 = 任务来源.手工;
            entity.IsActive = false;

            //m_cm.DisplayManager.DataControls["任务性质"].SelectedDataValue = 任务性质.进口拆箱;
            m_cm.DisplayManager.DataControls["委托时间"].SelectedDataValue = System.DateTime.Now;
            m_cm.DisplayManager.DataControls["装货时间要求始"].SelectedDataValue = System.DateTime.Now;
            m_cm.DisplayManager.DataControls["卸货时间要求始"].SelectedDataValue = System.DateTime.Now;
        }

        private void btn修改任务_Click(object sender, EventArgs e)
        {
            if (m_cm.DisplayManager.CurrentItem != null)
            {
                任务 entity = m_cmT.DisplayManagerT.CurrentEntity;
                if (entity.IsActive)
                {
                    MessageForm.ShowWarning("此任务已经预录入发送！");
                    return;
                }
                ArchiveOperationForm.DoEditS(m_cm, m_gridName);
                UpdateContent(m_cm, m_gridName);

                m_cm.DisplayManager.OnSelectedDataValueChanged(new SelectedDataValueChangedEventArgs("任务性质", m_cm.DisplayManager.DataControls["任务性质"]));
            }
        }

        private void btn删除_Click(object sender, EventArgs e)
        {
            if (m_cm.DisplayManager.CurrentItem != null)
            {
                任务 entity = m_cmT.DisplayManagerT.CurrentEntity;
                if (entity.IsActive)
                {
                    MessageForm.ShowWarning("此任务已经预录入发送！");
                    return;
                }

                ArchiveOperationForm.DoDeleteS(m_cm, m_gridName);
            }
        }

        private void btn暂存待确认_Click(object sender, EventArgs e)
        {
            if (m_cm.DisplayManager.CurrentItem != null)
            {
                任务预备案.RemoveAllValidation(m_cm);
                Save(SaveType.暂存待确认);
            }
        }
        private void btn预录入发送_Click(object sender, EventArgs e)
        {
            任务 entity = m_cmT.DisplayManagerT.CurrentEntity;
            if (entity != null)
            {
                //为了测试方便，删除验证
                //RemoveAllValidation();
                ArchiveDetailForm.AddValidations(m_cm, m_gridName);

                //if (m_cm.DisplayManager.DataControls["任务性质"].SelectedDataValue != null)
                //{
                //    任务性质 rwxz = (任务性质)m_cm.DisplayManager.DataControls["任务性质"].SelectedDataValue;
                //    if (rwxz != 任务性质.进口拆箱 && rwxz != 任务性质.出口装箱)
                //    {
                //        m_cm.RemoveValidation("箱型");
                //    }
                //}
                Save(SaveType.预录入发送);
            }
        }

        private void Save(SaveType saveType)
        {
            // 验证控件有效值（有些控件需移出焦点后对其他控件产生影响，如果这里不Validate，会先执行Save）
            this.ValidateChildren();
            
            if (m_cm.SaveCurrent())
            {
                任务 entity = m_cmT.DisplayManagerT.CurrentEntity;

                switch (saveType)
                {
                    case SaveType.暂存待确认:
                        任务Dao.生成预录入号(entity);
                        break;
                    case SaveType.预录入发送:
                        entity.IsActive = true;
                        entity.是否拒绝 = false;
                        任务Dao.生成预录入号(entity);
                        break;
                    default:
                        break;
                }
                m_cm.EndEdit(true);

                if (m_cm.State == StateType.View)
                {
                    ArchiveDetailForm.UpdateStatusDataControl(m_cm, m_gridName);

                    m_cm.DisplayManager.OnPositionChanged(System.EventArgs.Empty);
                }
            }
        }

        internal static void RemoveAllValidation(IWindowControlManager cm)
        {
            foreach (IDataControl dc in cm.DisplayManager.DataControls)
            {
                if (dc.Name == "任务性质")
                    continue;
                cm.RemoveValidation(dc.Name);
            }
            //m_cm.RemoveValidation("委托人编号");
            //m_cm.RemoveValidation("还箱进港时间要求始");
            //m_cm.RemoveValidation("还箱进港时间要求止");
            //m_cm.RemoveValidation("提箱时间要求始");
            //m_cm.RemoveValidation("提箱时间要求止");
            //m_cm.RemoveValidation("委托时间");
            //m_cm.RemoveValidation("卸货时间要求始");
            //m_cm.RemoveValidation("卸货时间要求止");
            //m_cm.RemoveValidation("装货时间要求始");
            //m_cm.RemoveValidation("装货时间要求止");
            //m_cm.RemoveValidation("重量");
            //m_cm.RemoveValidation("装货地编号");
            //m_cm.RemoveValidation("装货地详细地址");
            //m_cm.RemoveValidation("还箱进港点编号");
            //m_cm.RemoveValidation("提箱点编号");
            //m_cm.RemoveValidation("箱属船公司编号");
            //m_cm.RemoveValidation("卸货地编号");
            //m_cm.RemoveValidation("价值");
            //m_cm.RemoveValidation("船名航次");
            //m_cm.RemoveValidation("卸货联系手机");
            //m_cm.RemoveValidation("卸货联系座机");
            //m_cm.RemoveValidation("卸货联系人");
            //m_cm.RemoveValidation("委托联系人");
            //m_cm.RemoveValidation("装货联系手机");
            //m_cm.RemoveValidation("装货联系座机");
            //m_cm.RemoveValidation("装货联系人");
            //m_cm.RemoveValidation("箱号");
            //m_cm.RemoveValidation("箱型编号");
            //m_cm.RemoveValidation("货名");
            //m_cm.RemoveValidation("货物特征");
            //m_cm.RemoveValidation("提单号");
            //m_cm.RemoveValidation("转关箱标志");
        }

        private void btn进口箱任务批量录入_Click(object sender, EventArgs e)
        {
            ServiceProvider.GetService<IApplication>().ExecuteAction("任务备案_进口箱批量任务录入");
        }

        private void btn文件导入_Click(object sender, EventArgs e)
        {
            任务Dao dao = m_cm.Dao as 任务Dao;
            if (openFileDialog文件导入.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IList<DataTable> dtList = Feng.Windows.Utils.ExcelHelper.ReadExcel(openFileDialog文件导入.FileName, true);
                int successCount = 0;//成功导入的任务数量

                using (IRepository rep = ServiceProvider.GetService<IRepositoryFactory>().GenerateRepository<任务>())
                {
                    try
                    {
                        rep.BeginTransaction();
                        //System.Reflection.PropertyInfo[] properties = typeof(任务).GetProperties();

                        foreach (DataTable dt in dtList)
                        {
                            if (!dt.Columns.Contains("任务性质"))
                            {
                                continue;
                            }

                            foreach (DataRow dr in dt.Rows)
                            {
                                //if (!dr.Table.Columns.Contains("提单号") || string.IsNullOrEmpty(dr["提单号"].ToString()))
                                //    continue;
                                //if (rep.List<任务>(string.Format("提单号 = {0}", dr["提单号"].ToString()), null).Count > 0)
                                //    continue;

                                任务 rw = new 任务();
                                rw.任务来源 = 任务来源.文件;
                                rw.IsActive = false;
                                rw.是否拒绝 = false;

                                rw.任务性质 = (string.IsNullOrEmpty(dr["任务性质"].ToString()) ? 任务性质.进口拆箱 : (任务性质)Enum.Parse(typeof(任务性质), dr["任务性质"].ToString(), true));
                                rw.转关箱标志 = string.IsNullOrEmpty(dr["转关箱标志"].ToString()) ? null : (转关箱标志?)Enum.Parse(typeof(转关箱标志), dr["转关箱标志"].ToString(), true);
                                rw.委托人编号 = (string)Feng.Utils.NameValueControlHelper.GetMultiValue("人员单位_全部", dr["委托人"].ToString());
                                rw.委托时间 = ConvertHelper.ToDateTime(dr["委托时间"]);
                                rw.委托联系人 = (string)Feng.Utils.NameValueControlHelper.GetMultiValue("人员单位_全部", dr["委托联系人"].ToString());
                                rw.提单号 = ConvertHelper.ToString(dr["提单号"]);
                                rw.箱号 = ConvertHelper.ToString(dr["箱号"]);
                                rw.箱型编号 = (int?)Feng.Utils.NameValueControlHelper.GetMultiValue("备案_箱型", dr["箱型"].ToString());
                                rw.船名 = ConvertHelper.ToString(dr["船名"]);
                                rw.航次 = ConvertHelper.ToString(dr["航次"]);
                                rw.箱属船公司编号 = (string)Feng.Utils.NameValueControlHelper.GetMultiValue("人员单位_全部", dr["箱属船公司"].ToString());
                                rw.货名 = ConvertHelper.ToString(dr["货物分类"]);
                                rw.货物特征 = ConvertHelper.ToString(dr["货物特征"]);
                                rw.价值 = ConvertHelper.ToDecimal(dr["价值"]);
                                rw.重量 = ConvertHelper.ToDouble(dr["重量"]);

                                if (rw.任务性质 == 任务性质.进口拆箱 || rw.任务性质 == 任务性质.出口装箱)
                                {
                                    rw.提箱点编号 = (string)Feng.Utils.NameValueControlHelper.GetMultiValue("人员单位_全部", dr["提箱点"].ToString());

                                    if (rw.任务性质 == 任务性质.进口拆箱)
                                    {
                                        rw.提箱时间要求 = ConvertHelper.ToDateTime(dr["疏港期限"]);
                                    }

                                    rw.还箱进港点编号 = (string)Feng.Utils.NameValueControlHelper.GetMultiValue("人员单位_全部", dr["还箱/进港点"].ToString());
                                    rw.还箱进港时间要求 = ConvertHelper.ToDateTime(dr["还箱/进港期限"]);
                                }

                                if (rw.任务性质 != 任务性质.进口拆箱)
                                {
                                    rw.装货地编号 = (string)Feng.Utils.NameValueControlHelper.GetMultiValue("人员单位_装卸货地", dr["装货地"].ToString());
                                    rw.装货时间要求始 = ConvertHelper.ToDateTime(dr["装货时间要求始"]);
                                    rw.装货时间要求止 = ConvertHelper.ToDateTime(dr["装货时间要求止"]);
                                    rw.装货联系人 = ConvertHelper.ToString(dr["装货联系人"]);
                                    rw.装货联系手机 = ConvertHelper.ToString(dr["装货联系电话（手机)"]);
                                    rw.装货联系座机 = ConvertHelper.ToString(dr["装货联系电话（座机)"]);
                                    rw.装货地详细地址 = ConvertHelper.ToString(dr["装货地详细地址"]);
                                }

                                if (rw.任务性质 != 任务性质.出口装箱)
                                {
                                    rw.卸货地编号 = (string)Feng.Utils.NameValueControlHelper.GetMultiValue("人员单位_装卸货地", dr["卸货地"].ToString());
                                    rw.卸货时间要求始 = ConvertHelper.ToDateTime(dr["卸货时间要求始"]);
                                    rw.卸货时间要求止 = ConvertHelper.ToDateTime(dr["卸货时间要求止"]);
                                    rw.卸货联系人 = ConvertHelper.ToString(dr["卸货联系人"]);
                                    rw.卸货联系手机 = ConvertHelper.ToString(dr["卸货联系电话（手机)"]);
                                    rw.卸货联系座机 = ConvertHelper.ToString(dr["卸货联系电话（座机)"]);
                                    rw.卸货地详细地址 = ConvertHelper.ToString(dr["卸货地详细地址"]);
                                }

                                rw.备注 = ConvertHelper.ToString(dr["备注"]);

                                //foreach (System.Reflection.PropertyInfo property in properties)
                                //{
                                //    if (dr.Table.Columns.Contains(property.Name))
                                //    {
                                //        string excelValue = dr[property.Name].ToString();
                                //        object realValue = null;
                                //        if (string.IsNullOrEmpty(excelValue))
                                //        {
                                //            continue;
                                //        }
                                //        if (property.PropertyType.Equals(typeof(任务性质)))
                                //        {
                                //            realValue = (任务性质)Enum.Parse(typeof(任务性质), excelValue.ToString(), true);
                                //        }
                                //        else if (property.PropertyType.Equals(typeof(string)))
                                //        {
                                //            realValue = excelValue.ToString();
                                //        }
                                //        else if (property.PropertyType.Equals(typeof(int)))
                                //        {
                                //            realValue = Convert.ToInt32(excelValue);
                                //        }
                                //        else if (property.PropertyType.Equals(typeof(decimal)))
                                //        {
                                //            realValue = Convert.ToDecimal(excelValue);
                                //        }
                                //        else if (property.PropertyType.Equals(typeof(double)))
                                //        {
                                //            realValue = Convert.ToDouble(excelValue);
                                //        }
                                //        else if (property.PropertyType.Equals(typeof(bool)))
                                //        {
                                //            realValue = excelValue == "是" ? true : false;
                                //            //realValue = Convert.ToBoolean(excelValue);
                                //        }
                                //        else if (property.PropertyType.Equals(typeof(DateTime)))
                                //        {
                                //            realValue = Convert.ToDateTime(excelValue);
                                //        }
                                //        property.SetValue(rw, realValue, null);
                                //    }
                                //}

                                if ((int)rw.任务性质 != 0)
                                {
                                    dao.Save(rep, rw);
                                    successCount++;
                                }
                            }
                        }
                        rep.CommitTransaction();

                        MessageForm.ShowInfo("成功导入 " + successCount + " 条任务。");
                        m_rightGrid.ReloadData();
                    }
                    catch (Exception ex)
                    {
                        rep.RollbackTransaction();
                        ExceptionProcess.ProcessWithNotify(ex);
                    }
                }
            }
        }
    }
}
