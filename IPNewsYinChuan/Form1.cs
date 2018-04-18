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
        int totalSecond = 60;
        Timer timer = new Timer();
        Timer reConnectTimer = new Timer();


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
            timer.Interval = 4000;
            timer.Tick += (s, e1) => { timer.Enabled = false; i = 0; };
            reConnectTimer.Interval = 1000;
            reConnectTimer.Tick += (s, o) =>
            {
                totalSecond--;
                this.labErr.Text = string.Format("无法连接服务器，请检查网络！(点此重试。{0})", totalSecond);
                if (totalSecond==0)
                {
                   
                    ChcekInternet();
                }
                
            };
            this.BackColor = Color.FromArgb(34, 53, 74);            
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            ChcekInternet();       


        }



        private void ScreenTimer_Tick(object sender, EventArgs e)
        {
            if (t == 0)
            {
                t = Common.GetRandom(15, 60);
                byte[] bs = new byte[] { };
                int ClientID = Convert.ToInt32(this.Csid);
                string result = sendScreen(ClientID, null);
                if (string.IsNullOrEmpty(result))
                {
                    byte[] imgByte = GZip.Compress(Common.ImageGdi(PrintScreen.captureScreen()));
                    string s = sendScreen(ClientID, imgByte);
                }
            }
            else
            {
                t--;
            }
        }

        private string sendScreen(int clientId, byte[] imgbyte)
        {
            string result = "";
            try
            {
                result = ch.UpdateClientStatus(clientId, imgbyte, 1);

            }
            catch (Exception)
            {


            }
            return result;

        }

        private void ChcekInternet()
        {

            if (InternetHelper.IsConnectInternet())
            {
                this.labErr.Visible = false;
                this.reConnectTimer.Stop();
                this.reConnectTimer.Enabled = false;

                //如果已注册
                if (CheckAuth())
                {
                    
                    //string path = "http://111.113.15.126:3307/index.html?csid=278&pwd=1e4b496b5c7d49e58d4359477b8bc0d7";
                    string path = string.Format("{0}?csid={1}&pwd={2}", Properties.Settings.Default["Address"].ToString(), this.Csid, this.Pwd);
                    browser = new ChromiumWebBrowser(path)
                    {
                        Dock = DockStyle.Fill,//填充方式,
                        ContextMenu = null

                    };
                    browser.Dock = DockStyle.Fill;//填充方式                
                    this.Controls.Add(browser);
                    browser.Focus();

                    Timer screenTimer = new Timer();
                    screenTimer.Interval = 1000;
                    screenTimer.Tick += ScreenTimer_Tick;
                    screenTimer.Start();

                }
                else
                {
                    this.btnReg.Visible = true; 
                }
            }
            else
            {

                this.labErr.Visible = true;
                totalSecond = 60;
                this.reConnectTimer.Enabled = true;
                reConnectTimer.Start();



            }
        }

        private bool CheckAuth()
        {
            if (!File.Exists(AuthFilePath))
            {
                FormAuth formAuth = new FormAuth();
                if (formAuth.ShowDialog() == DialogResult.OK)
                {
                    this.Csid = formAuth.Csid;
                    this.Pwd = formAuth.Pwd;
                    this.btnReg.Visible = false;
                    return true;
                }
                else
                {
                    this.Csid = "";
                    this.Pwd = "";
                    this.btnReg.Visible = true;
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            i += 1;

            timer.Enabled = true;
            if (i % 3 == 0)
            {
                timer.Enabled = false;
                i = 0;
                FormExit em = new FormExit();
                em.ShowDialog();

            }
        }

        private void labErr_Click(object sender, EventArgs e)
        {
            ChcekInternet();
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            CheckAuth();
        }
    }
}
