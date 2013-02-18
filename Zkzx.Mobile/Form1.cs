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

namespace Zkzx.Mobile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(new Microsoft.Practices.ServiceLocation.ServiceLocatorProvider(
                delegate()
                {
                    var p = new Feng.DefaultServiceProvider();
                    p.EnableNHibernate();

                    p.SetDefaultService<ICache>(new HashtableCache());
                    p.SetDefaultService<IApplicationDirectory>(new WindowsDirectory());

                    return p;
                }));

            InitializeComponent();

            m_webProxy.Encoding = System.Text.Encoding.UTF8;
            m_webProxy.ContentType = "application/json";
            m_webProxy.Accept = "application/json";

            btn当前作业_Click(btn当前作业, System.EventArgs.Empty);
        }

        private Feng.Net.WebProxy m_webProxy = new Feng.Net.WebProxy();
        private string m_truckId = "浙B-8092H";
        private string m_host = "192.168.0.10";
        private string m_currentWorkId = null;
        private void LoadData()
        {
            //m_host = "localhost";

            tableLayoutPanel1.Controls.Clear();

            var workId = m_webProxy.GetToString(string.Format("http://{0}/CarTrackService/ZkzxDataService.svc/GetCurrentWorkerIdByTruckId/{1}", m_host, m_truckId));
            workId = JsonConvert.DeserializeObject<string>(workId);
            if (string.IsNullOrEmpty(workId))
                return;
            m_currentWorkId = workId;

            var s = m_webProxy.GetToString(string.Format("http://{0}/CarTrackService/ZkzxDataService.svc/GetWorkSequence/{1}", m_host, workId));
            s = JsonConvert.DeserializeObject<string>(s);
            JObject o = JObject.Parse(s);
            string title = (string)o["Title"];
            JArray actions = (JArray)o["Actions"];
            int actionIdx = (int)o["ActionIdx"];

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
                b2.Enabled = nowActionIdx == actionIdx;

                Button b3 = new Button();
                b3.Text = "结束";
                tableLayoutPanel1.Controls.Add(b3);
                tableLayoutPanel1.SetColumn(b3, 3);
                tableLayoutPanel1.SetRow(b3, rowIdx);
                b3.Click += new EventHandler(btn结束_Click);
                b3.Tag = nowActionIdx;
                b3.Enabled = nowActionIdx == actionIdx;

                rowIdx++;
                if (title.Contains("出口") && l.Text.Contains("堆场提箱"))
                {
                    Label l2 = new Label();
                    string s1 = "";
                    if (l.Text.Contains("1"))
                        s1 = "1";
                    else if (l.Text.Contains("2"))
                        s1 = "2";
                    l2.Text = "箱号输入"+s1;
                    l2.ForeColor = Color.Red;
                    tableLayoutPanel1.Controls.Add(l2);
                    tableLayoutPanel1.SetColumn(l2, 0);
                    tableLayoutPanel1.SetRow(l2, rowIdx);

                    TextBox T1 = new TextBox();
                    T1.Text = "";
                    tableLayoutPanel1.Controls.Add(T1);
                    tableLayoutPanel1.SetColumn(T1, 1);
                    tableLayoutPanel1.SetColumnSpan(T1, 2);
                    tableLayoutPanel1.SetRow(T1, rowIdx);

                    Button b4 = new Button();
                    b4.Text = "发送";
                    tableLayoutPanel1.Controls.Add(b4);
                    tableLayoutPanel1.SetColumn(b4, 3);
                    tableLayoutPanel1.SetRow(b4, rowIdx);
                    b4.Click += new EventHandler(发送箱号_Click);
                    b4.Tag = nowActionIdx;
                    b4.Enabled = nowActionIdx == actionIdx;

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
                    l2.Text = "封号输入"+s1;
                    l2.ForeColor = Color.Red;
                    tableLayoutPanel1.Controls.Add(l2);
                    tableLayoutPanel1.SetColumn(l2, 0);
                    tableLayoutPanel1.SetRow(l2, rowIdx);

                    TextBox T1 = new TextBox();
                    T1.Text = "";
                    tableLayoutPanel1.Controls.Add(T1);
                    tableLayoutPanel1.SetColumn(T1, 1);
                    tableLayoutPanel1.SetColumnSpan(T1, 2);
                    tableLayoutPanel1.SetRow(T1, rowIdx);

                    Button b4 = new Button();
                    b4.Text = "发送";
                    tableLayoutPanel1.Controls.Add(b4);
                    tableLayoutPanel1.SetColumn(b4, 3);
                    tableLayoutPanel1.SetRow(b4, rowIdx);
                    b4.Click += new EventHandler(发送封号_Click);
                    b4.Tag = nowActionIdx;
                    b4.Enabled = nowActionIdx == actionIdx;

                    rowIdx++;
                }
            }
        }

        private bool SendTruckState(string state)
        {
            if (string.IsNullOrEmpty(m_currentWorkId))
                return false;
            string s = m_webProxy.GetToString(string.Format("http://{0}/CarTrackService/ZkzxDataService.svc/SendTruckState/{1}/{2}", m_host, m_currentWorkId, state));
            s = JsonConvert.DeserializeObject<string>(s);
            return s == "Ok";
        }

        void btn详情_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(m_currentWorkId))
                return;
            int idx = (int)((Control)sender).Tag;
            string s = m_webProxy.GetToString(string.Format("http://{0}/CarTrackService/ZkzxDataService.svc/GetWorkDetails/{1}/{2}", m_host, m_currentWorkId, idx.ToString()));
            s = JsonConvert.DeserializeObject<string>(s);
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
            SendTruckState(string.Format("封号-{0}-{1}", idx.ToString(), textBox.Text));
        }

        void 发送箱号_Click(object sender, EventArgs e)
        {
            var pos = tableLayoutPanel1.GetCellPosition(sender as Control);
            var textBox = tableLayoutPanel1.GetControlFromPosition(1, pos.Row) as TextBox;
            if (string.IsNullOrEmpty(textBox.Text))
                return;
            int idx = (int)((Control)sender).Tag;
            SendTruckState(string.Format("箱号-{0}-{1}", idx.ToString(), textBox.Text));
        }

        void btn结束_Click(object sender, EventArgs e)
        {
            int idx = (int)((Control)sender).Tag;
            bool r = SendTruckState(string.Format("动作-{0}-{1}", idx.ToString(), "1"));

            LoadData();
        }

        void btn开始_Click(object sender, EventArgs e)
        {
            int idx = (int)((Control)sender).Tag;
            bool r = SendTruckState(string.Format("动作-{0}-{1}", idx.ToString(), "0"));

            LoadData();
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
            LoadData();
        }
    }
}
