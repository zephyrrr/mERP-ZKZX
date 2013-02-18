using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Feng;
using Feng.Grid;
using Feng.Windows.Forms;
using Zkzx.Model;

namespace Zkzx.Forms
{
    public partial class 虚拟手机终端 : MyTemplateForm, IRefreshDataForm
    {
        private string m_host = null;
        public 虚拟手机终端()
        {
            InitializeComponent();

            m_webProxy.Encoding = System.Text.Encoding.UTF8;
            m_webProxy.ContentType = "application/json";
            m_webProxy.Accept = "application/json";
        }

        private DataUnboundGrid m_车载ID当前使用情况集合;
        private void 虚拟手机终端_Load(object sender, EventArgs e)
        {
            m_车载ID当前使用情况集合 = base.AssociateBoundGrid(pnl车载ID, "实时监控_车辆作业_车载ID当前使用情况") as DataUnboundGrid;
            m_车载ID当前使用情况集合.DataRowTemplate.Cells["车辆位置"].DoubleClick += new EventHandler(作业监控区_DoubleClick);

            Helper.SetGridDefault(this, m_车载ID当前使用情况集合);
            
            m_车载ID当前使用情况集合.CurrentRowChanged += new EventHandler(m_车载ID当前使用情况集合_CurrentRowChanged);
        }
        void 作业监控区_DoubleClick(object sender, EventArgs e)
        {
            监控级调度主界面.OnCellDoubleClick(sender, e);
        }
        void m_车载ID当前使用情况集合_CurrentRowChanged(object sender, EventArgs e)
        {
            if (m_车载ID当前使用情况集合.CurrentRow == null)
            {
                LoadData(null);
            }
            else
            {
                车辆作业 clzy = m_车载ID当前使用情况集合.CurrentRow.Tag as 车辆作业;
                if (clzy != null)
                {
                    m_truckId = clzy.车载Id号;
                    if (!string.IsNullOrEmpty(m_truckId))
                    {
                        m_currentWorkIdx = 0;
                        LoadData(clzy.ID.ToString());
                    }
                }
            }
        }

        public void RefreshData()
        {
            m_车载ID当前使用情况集合.DisplayManager.SearchManager.LoadData();
        }

        private Feng.Net.WebProxy m_webProxy = new Feng.Net.WebProxy();
        private string m_truckId = null;
        private string m_currentWorkId = null;

        private void LoadData(string currentWorkId)
        {
            tableLayoutPanel1.Controls.Clear();
            lblTitle.Text = "";

            if (string.IsNullOrEmpty(currentWorkId))
            {
                return;
            }
            m_currentWorkId = currentWorkId;
            if (string.IsNullOrEmpty(m_host))
            {
                m_host = Feng.Windows.Utils.ConfigurationHelper.GetDefaultServerName();
            }

            string s;
            s = m_webProxy.GetToString(string.Format("http://{0}/CarTrackService/ZkzxDataService.svc/GetWorkSequence/{1}", m_host, m_currentWorkId));
            //s = JsonConvert.DeserializeObject<string>(s);
            s = (string)JObject.Parse(s)["Value"];
            if (string.IsNullOrEmpty(s))
                return;

            JObject o = JObject.Parse(s);
            string title = (string)o["Title"];
            JArray actions = (JArray)o["Actions"];
            JArray taskIdxs = (JArray)o["TaskIdxs"];
            int actionIdx = (int)o["ActionIdx"];
            int actionIdxIdx = (int)o["ActionIdxIdx"];

            JArray xianghaos = (JArray)o["Xianghao"];
            JArray fengzihaos = (JArray)o["Fengzihao"];

            lblTitle.Text = title;
            int rowIdx = 0;
            foreach (JToken i in actions)
            {
                int nowActionIdx = actions.IndexOf(i);

                Label l = new Label();
                l.Text = (string)i;
                tableLayoutPanel1.Controls.Add(l);
                tableLayoutPanel1.SetColumn(l, 0);
                tableLayoutPanel1.SetRow(l, rowIdx);

                Button b1 = new Button();
                b1.Text = "详情";
                tableLayoutPanel1.Controls.Add(b1);
                tableLayoutPanel1.SetColumn(b1, 1);
                tableLayoutPanel1.SetRow(b1, rowIdx);
                b1.Tag = nowActionIdx;
                b1.Click += new EventHandler(btn详情_Click);

                Button b2 = new Button();
                b2.Text = "开始";
                tableLayoutPanel1.Controls.Add(b2);
                tableLayoutPanel1.SetColumn(b2, 2);
                tableLayoutPanel1.SetRow(b2, rowIdx);
                b2.Click += new EventHandler(btn开始_Click);
                b2.Tag = nowActionIdx;
                b2.Enabled = nowActionIdx == actionIdx && actionIdxIdx != 0 && m_currentWorkIdx == 0;

                Button b3 = new Button();
                b3.Text = "结束";
                tableLayoutPanel1.Controls.Add(b3);
                tableLayoutPanel1.SetColumn(b3, 3);
                tableLayoutPanel1.SetRow(b3, rowIdx);
                b3.Click += new EventHandler(btn结束_Click);
                b3.Tag = nowActionIdx;
                b3.Enabled = nowActionIdx == actionIdx && actionIdxIdx != 1 && m_currentWorkIdx == 0;

                rowIdx++;
                if (title.Contains("出口") && l.Text.Contains("堆场提箱"))
                {
                    Label l2 = new Label();
                    string s1 = "";
                    if (l.Text.Contains("1"))
                        s1 = "1";
                    else if (l.Text.Contains("2"))
                        s1 = "2";
                    l2.Text = "箱号输入" + s1;
                    l2.ForeColor = Color.Red;
                    tableLayoutPanel1.Controls.Add(l2);
                    tableLayoutPanel1.SetColumn(l2, 0);
                    tableLayoutPanel1.SetRow(l2, rowIdx);

                    TextBox t1 = new TextBox();
                    t1.Text = string.Empty;
                    t1.CharacterCasing = CharacterCasing.Upper;
                    tableLayoutPanel1.Controls.Add(t1);
                    tableLayoutPanel1.SetColumn(t1, 1);
                    tableLayoutPanel1.SetColumnSpan(t1, 2);
                    tableLayoutPanel1.SetRow(t1, rowIdx);
                    //t1.Enabled = nowActionIdx == actionIdx;
                    //t1.Enabled = nowActionIdx < 0 || nowActionIdx >= actions.Count;

                    Button b4 = new Button();
                    b4.Text = "发送";
                    tableLayoutPanel1.Controls.Add(b4);
                    tableLayoutPanel1.SetColumn(b4, 3);
                    tableLayoutPanel1.SetRow(b4, rowIdx);
                    b4.Click += new EventHandler(发送箱号_Click);
                    b4.Tag = nowActionIdx;
                    //b4.Enabled = nowActionIdx == actionIdx;

                    string sx = (string)xianghaos[(int)taskIdxs[nowActionIdx]];
                    t1.Text = sx;
                    if (!string.IsNullOrEmpty(sx) || m_currentWorkIdx != 0)
                    {
                        //t1.Enabled = false;
                        //b4.Enabled = false;
                    }

                    rowIdx++;
                }
                if ((title.Contains("出口") || title.Contains("套箱")) && (l.Text.Contains("装货") && !(l.Text.Contains("带货"))))
                {
                    Label l2 = new Label();
                    string s1 = "";
                    if (l.Text.Contains("1"))
                        s1 = "1";
                    else if (l.Text.Contains("2"))
                        s1 = "2";
                    l2.Text = "封号输入" + s1;
                    l2.ForeColor = Color.Red;
                    tableLayoutPanel1.Controls.Add(l2);
                    tableLayoutPanel1.SetColumn(l2, 0);
                    tableLayoutPanel1.SetRow(l2, rowIdx);

                    TextBox t1 = new TextBox();
                    t1.Text = string.Empty;
                    t1.CharacterCasing = CharacterCasing.Upper;
                    tableLayoutPanel1.Controls.Add(t1);
                    tableLayoutPanel1.SetColumn(t1, 1);
                    tableLayoutPanel1.SetColumnSpan(t1, 2);
                    tableLayoutPanel1.SetRow(t1, rowIdx);
                    //t1.Enabled = nowActionIdx == actionIdx;

                    Button b4 = new Button();
                    b4.Text = "发送";
                    tableLayoutPanel1.Controls.Add(b4);
                    tableLayoutPanel1.SetColumn(b4, 3);
                    tableLayoutPanel1.SetRow(b4, rowIdx);
                    b4.Click += new EventHandler(发送封号_Click);
                    b4.Tag = nowActionIdx;
                    //b4.Enabled = nowActionIdx == actionIdx;

                    string sx = (string)fengzihaos[(int)taskIdxs[nowActionIdx]];
                    t1.Text = sx;
                    if (!string.IsNullOrEmpty(sx) || m_currentWorkIdx != 0)
                    {
                        //t1.Enabled = false;
                        //b4.Enabled = false;
                    }

                    rowIdx++;
                }
            }
        }

        private bool SendTruckState(string state)
        {
            if (string.IsNullOrEmpty(m_currentWorkId))
                return false;
            string s = m_webProxy.GetToString(string.Format("http://{0}/CarTrackService/ZkzxDataService.svc/SendTruckState/{1}/{2}", m_host, m_currentWorkId, state));
            s = (string)JObject.Parse(s)["Value"];
            return s == "Ok";
        }

        void btn详情_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(m_currentWorkId))
                return;
            int idx = (int)((Control)sender).Tag;
            string s = m_webProxy.GetToString(string.Format("http://{0}/CarTrackService/ZkzxDataService.svc/GetWorkDetails/{1}/{2}", m_host, m_currentWorkId, idx.ToString()));
            s = (string)JObject.Parse(s)["Value"];
            if (string.IsNullOrEmpty(s))
                return;
            JObject o = JObject.Parse(s);

            StringBuilder sb = new StringBuilder();
            sb.Append("地址:");
            sb.AppendLine((string)o["Address"]);
            sb.Append("电话:");
            sb.AppendLine((string)o["Tel"]);
            sb.Append("其他:");
            sb.AppendLine((string)o["Detail"]);

            MessageBox.Show(sb.ToString());
        }

        void 发送封号_Click(object sender, EventArgs e)
        {
            var pos = tableLayoutPanel1.GetCellPosition(sender as Control);
            var textBox = tableLayoutPanel1.GetControlFromPosition(1, pos.Row) as TextBox;
            if (string.IsNullOrEmpty(textBox.Text))
                return;
            int idx = (int)((Control)sender).Tag;
            bool r = SendTruckState(string.Format("封号-{0}-{1}", idx.ToString(), textBox.Text));
            if (r)
            {
                //textBox.Enabled = false;
                //(sender as Button).Enabled = false;
            }
        }

        private System.Text.RegularExpressions.Regex s_regex箱号 = new System.Text.RegularExpressions.Regex("^[A-Z]{4}[0-9]{7,8}$", System.Text.RegularExpressions.RegexOptions.Compiled);
        void 发送箱号_Click(object sender, EventArgs e)
        {
            var pos = tableLayoutPanel1.GetCellPosition(sender as Control);
            var textBox = tableLayoutPanel1.GetControlFromPosition(1, pos.Row) as TextBox;
            if (string.IsNullOrEmpty(textBox.Text))
                return;
            int idx = (int)((Control)sender).Tag;

            // 检查箱号
            var success = s_regex箱号.Match(textBox.Text);
            if (!success.Success)
            {
                MessageForm.ShowWarning("箱号格式不符，请重新输入！");
                return;
            }

            bool r = SendTruckState(string.Format("箱号-{0}-{1}", idx.ToString(), textBox.Text));
            if (r)
            {
                //textBox.Enabled = false;
                //(sender as Button).Enabled = false;
            }
        }

        void btn结束_Click(object sender, EventArgs e)
        {
            int idx = (int)((Control)sender).Tag;
            bool r = SendTruckState(string.Format("动作-{0}-{1}", idx.ToString(), "1"));

            LoadData(m_currentWorkId);
        }

        void btn开始_Click(object sender, EventArgs e)
        {
            int idx = (int)((Control)sender).Tag;
            bool r = SendTruckState(string.Format("动作-{0}-{1}", idx.ToString(), "0"));
            //if (r)
            //    (sender as Button).Enabled = false;
            LoadData(m_currentWorkId);
        }

        private void btn途中休息_Click(object sender, EventArgs e)
        {
            SendTruckState("途中休息");
        }

        private void btn堵车_Click(object sender, EventArgs e)
        {
            SendTruckState("堵车");
        }

        private void btn故障处理_Click(object sender, EventArgs e)
        {
            SendTruckState("故障处理");
        }

        private void btn当前作业_Click(object sender, EventArgs e)
        {
            m_currentWorkIdx = 0;
            LoadData();

            //var s = m_webProxy.GetToString(string.Format("http://{0}/CarTrackService/ZkzxDataService.svc/GetCurrentWorkerIdByTruckId/{1}", m_host, m_truckId));
            //if (string.IsNullOrEmpty(s))
            //    return;
            ////workId = JsonConvert.DeserializeObject<string>(workId);
            //s = (string)JObject.Parse(s)["Value"];
            //if (string.IsNullOrEmpty(s))
            //    return;
            //m_currentWorkId = s;
        }
        private int m_currentWorkIdx = 0;
        void btn后续作业_Click(object sender, System.EventArgs e)
        {
            m_currentWorkIdx++;
            LoadData();
        }
        private void LoadData()
        {
            var s = m_webProxy.GetToString(string.Format("http://{0}/CarTrackService/ZkzxDataService.svc/GetWorkerIdByTruckId/{1}/{2}", m_host, m_truckId, m_currentWorkIdx));
            if (string.IsNullOrEmpty(s))
                return;
            s = (string)JObject.Parse(s)["Value"];
            if (string.IsNullOrEmpty(s) || s == m_currentWorkId)
                return;
            m_currentWorkId = s;
            LoadData(m_currentWorkId);
        }
        private void btnSendGPS_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(m_currentWorkId))
                return;
            if (string.IsNullOrEmpty(txtGPSLat.Text) || string.IsNullOrEmpty(txtGPSLon.Text))
                return;

            string gpsData = string.Format("{0},{1},{2},0,0,0,0", dtpGPSTime.Value, txtGPSLat.Text, txtGPSLon.Text);
            string postData = string.Format(@"{0}""gpsData"":""{2}""{1}", "{", "}", gpsData); 
            string s = m_webProxy.PostToString(string.Format("http://{0}/CarTrackService/ZkzxDataService.svc/{1}/SendTrackPoint",
                m_host, m_truckId), postData);
            //System.Windows.Forms.MessageBox.Show(s);
            
            NextReadGpsData();
        }

        private void NextReadGpsData()
        {
            if (m_readGpsDataIdx < 0 || m_readGpsDataIdx >= m_readGpsData.Count)
            {
                txtGPSLat.Text = null;
                txtGPSLon.Text = null;
                dtpGPSTime.Value = System.DateTime.Now;
                return;
            }
            string s = m_readGpsData[m_readGpsDataIdx];
            string[] ss = s.Split(',');
            if (ss.Length != 3)
                return;
            dtpGPSTime.Value = Convert.ToDateTime(ss[0]);
            txtGPSLat.Text = ss[1].Trim();
            txtGPSLon.Text = ss[2].Trim();

            m_readGpsDataIdx++;
        }
        private List<string> m_readGpsData = new List<string>();
        private int m_readGpsDataIdx = 0;
        private void btnReadGpsData_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofg = new OpenFileDialog();
            if (ofg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_readGpsData.Clear();
                m_readGpsDataIdx = 0;
                using (StreamReader sr = new StreamReader(ofg.FileName))
                {
                    while (!sr.EndOfStream)
                    {
                        string s = sr.ReadLine();
                        if (!string.IsNullOrEmpty(s))
                        {
                            m_readGpsData.Add(s);
                        }
                    }
                }
                NextReadGpsData();
            }
        }

        private void ckbUseLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbUseLocal.Checked)
            {
                m_host = "localhost";
            }
            else
            {
                m_host = null;
            }
        }


    }
}
