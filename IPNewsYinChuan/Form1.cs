using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using System.IO;
using Helper;


namespace IPNewsYinChuan
{
    public partial class Form1 : Form
    {
        private ChromiumWebBrowser browser;
        WebReference.ClientHelper ch = new WebReference.ClientHelper();
        WebReference.ClientHelperSoapHeader cs = new WebReference.ClientHelperSoapHeader();

        int t = 10;
        int i = 0;


        string AuthFilePath = AppDomain.CurrentDomain.BaseDirectory + "user.txt";

       
    

        /// <summary>
        /// Csid
        /// </summary>
        public string Csid { get; set; }
        /// <summary>
        /// Pwd
        /// </summary>
        public string Pwd { get; set; }

       





        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            ChcekInternet();
            //如果已注册
            if (CheckAuth())
            {

                //string path = "http://111.113.15.126:3307/index.html?csid=278&pwd=1e4b496b5c7d49e58d4359477b8bc0d7";
                string path = string.Format("{0}?csid={1}&pwd={2}",Properties.Settings.Default["Address"].ToString(),this.Csid,this.Pwd);
                browser = new ChromiumWebBrowser(path)
                {
                    Dock = DockStyle.Fill,//填充方式,
                    ContextMenu = null

                };
                browser.Dock = DockStyle.Fill;//填充方式
                browser.Click += Browser_Click;
                this.Controls.Add(browser);
                ;

                Timer screenTimer = new Timer();
                screenTimer.Interval = 1000;
                screenTimer.Tick += ScreenTimer_Tick;

                

            }
           
        }

        private void Browser_Click(object sender, EventArgs e)
        {
            i += 1;
            Timer timer = new Timer();
            timer.Interval = 4000;
            timer.Tick += (s, e1) => { timer.Enabled = false; i = 0; };
            timer.Enabled = true;
            if (i % 3 == 0)
            {
                timer.Enabled = false;
                i = 0;
                FormExit em = new FormExit();
                em.ShowDialog();

            }
        }

        private void ScreenTimer_Tick(object sender, EventArgs e)
        {
            if (t == 0)
            {
                t = Common.GetRandom(15, 60);
                byte[] bs = new byte[] { };
                int ClientID = Convert.ToInt32(this.Csid);
                string result = ch.UpdateClientStatus(ClientID, null, 1);
                if (string.IsNullOrEmpty(result))
                {
                    byte[] imgByte = GZip.Compress(Common.ImageGdi(PrintScreen.captureScreen()));
                    string s = ch.UpdateClientStatus(ClientID, imgByte, 1);
                }
            }
            else
            {
                t--;
            }
        }

        private void ChcekInternet() {

            if (InternetHelper.IsConnectInternet())
            {
                return ;
            }
            else
            {
                Common.ShowProcessing("无法连接服务器,正在重新连接……", this, (obj) => {
                    Timer t = new Timer();
                    t.Interval = 30000;
                    t.Tick += (o, s) =>
                    {
                        if (InternetHelper.IsConnectInternet())
                        {
                            t.Stop();
                            t.Dispose();
                            return ;
                        }
                        else
                        {
                            ChcekInternet();
                        }

                    };
                },null);

            }
        }

        private bool CheckAuth()
        {
            if (!File.Exists(AuthFilePath))
            {
                FormAuth formAuth = new FormAuth();
                if (formAuth.ShowDialog()==DialogResult.OK)
                {
                    this.Csid = formAuth.Csid;
                    this.Pwd = formAuth.Pwd;
                    return true;
                }
                else
                {
                    this.Csid = "";
                    this.Pwd = "";
                    return false;
                }
            }
            else
            {
                StreamReader sr = new StreamReader(this.AuthFilePath);
                string result = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
                this.Csid = result.Split(',')[0];
                this.Pwd = result.Split(',')[1].Trim();
                return true;

                
            }
        }

        
    }
}
