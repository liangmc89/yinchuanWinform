using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPNewsYinChuan
{
    public partial class FormExit : Form
    {
        private string Pwd { get; set; }
        const int CLOSE_SECONED= 90;
        Timer t;
        public FormExit()
        {
            InitializeComponent();
            Pwd = Properties.Settings.Default["ExitPwd"].ToString();
            t = new Timer();
            t.Interval = 2000;
            int i = 0;
            t.Tick += (s, o) => {
                i++;
                if (i>=CLOSE_SECONED)
                {
                    this.Close();
                }
            };
            t.Start();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string text = btn.Text;
            int num = 0;
            if (int.TryParse(text, out num))
            {
                this.textPwd.Text += text;
            }
            else if (text == "删除")
            {
                if (this.textPwd.Text.Length > 0)
                {
                    this.textPwd.Text = this.textPwd.Text.Remove(this.textPwd.Text.Length - 1, 1);
                }
            }
            else if (text == "退出")
            {
                if (this.textPwd.Text.Trim()==Pwd)
                {   
                    
                    Application.Exit();
                }
                
            }
        }
    }
}
