using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPNewsYinChuan
{
    public partial class FormAuth : Form
    {
        public string Csid { get; set; }
        public string Pwd { get; set; }       
        private string ClientScreenName { get; set; }
        private string ShortDescription { get; set; }
        private string HDSerialNumber { get; set; }
        private string MachineModelName { get; set; }
        private string MachineCode { get; set; }
        private string Ip { get; set; }

        public FormAuth()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbScreenName.Text)||string.IsNullOrEmpty(this.tbDescription.Text)||string.IsNullOrEmpty(this.tbMMN.Text)||string.IsNullOrEmpty(this.tbMc.Text))
            {
                return;
            }
            else
            {
                this.ClientScreenName = this.tbScreenName.Text.Trim();
                this.ShortDescription = this.tbDescription.Text.Trim();
                this.MachineModelName = this.tbMMN.Text.Trim();
                this.MachineCode = this.tbMc.Text.Trim();
                Auth();
            }
        }
        private void Auth()
        {
            // string param = string.Format("op=AddClientScreen&clientScreenName={0}&shortDescription={1}&HDSerialNumber={2}", this.ClientScreenName,this.ShortDescription,this.HDSerialNumber);
            WebReference.ClientHelper clientHelper = new WebReference.ClientHelper();
            string[] arr = new string[] { };
            try
            {
                arr = clientHelper.AddClientScreen(this.ClientScreenName, this.ShortDescription, Common.GetHardDiskID(), this.MachineModelName, this.MachineCode, "");
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message, "注册失败,请稍后重试！");
               
            }
            if (arr != null && arr.Length > 0)
            {
                this.Csid = arr[0];
                this.Pwd = arr[1];


                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory+"user.txt", false, System.Text.Encoding.UTF8);
                sw.Write(arr[0] + ',' + arr[1]);
                sw.Flush();
                sw.Close();
                sw.Dispose();
                this.DialogResult= DialogResult.OK;

            }
            else
            {
                MessageBox.Show("注册失败,请稍后重试！");
                this.DialogResult= DialogResult.No;
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

      
    }
}
