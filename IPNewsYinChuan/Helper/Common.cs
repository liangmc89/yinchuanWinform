using System;
using System.Management;
using System.Diagnostics;
using GetDiskInfo;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;
using IPNewsYinChuan;
using System.Dynamic;
using System.Threading;
using System.Windows.Forms;

namespace Helper
{
    public class Common
    {
        public static void ShowProcessing(string msg, Form owner, ParameterizedThreadStart work, object workArg = null)
        {
            FrmProcessing processingForm = new FrmProcessing(msg);
            dynamic expObj = new ExpandoObject();
            expObj.Form = processingForm;
            expObj.WorkArg = workArg;
            processingForm.SetWorkAction(work, expObj);
            processingForm.ShowDialog(owner);
            if (processingForm.WorkException != null)
            {
                throw processingForm.WorkException;
            }
        }
        #region 取硬盘号
        //取第一块硬盘号   
        public static string GetHardDiskID()
        {
            string strHardDiskID = string.Empty;
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT   *   FROM   Win32_PhysicalMedia");
                foreach (ManagementObject mo in searcher.Get())
                {
                    //SerialNumber,ModuleNumber   
                    strHardDiskID = mo["SerialNumber"].ToString().Trim();
                    if (!"".Equals(strHardDiskID))
                    {
                        break;
                    }
                }
                if (string.IsNullOrEmpty(strHardDiskID))
                    strHardDiskID = GetHardDiskID1();
            }
            catch
            {
                strHardDiskID = GetHardDiskID1();
            }
            return strHardDiskID;
        }

        /// <summary>
        /// 通过第三方dll文件获取硬盘号
        /// </summary>
        /// <returns></returns>
        public static string GetHardDiskID1()
        {
            string hddServialNumber = string.Empty;
            try
            {
                int iDiskNo = HDiskInfo.GetDriveCount();
                for (int i = 0; i < iDiskNo; i++)
                {
                    hddServialNumber = HDiskInfo.GetSerialNumber(i);
                    if (!string.IsNullOrEmpty(hddServialNumber.Trim()))
                        break;
                }

                if (string.IsNullOrEmpty(hddServialNumber.Trim()))
                    hddServialNumber = GetNetWorkNumber();
            }
            catch
            {
                hddServialNumber = GetNetWorkNumber();
            }
            return hddServialNumber;
        }

        //取第一块网卡号   
        private static string GetNetWorkNumber()
        {
            string mac = "";
            try
            {
                //获取网卡硬件地址
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
            }
            catch
            {
            }
            return mac;
        }
        #endregion

        /// <summary>
        /// 获取进程是否运行及是否响应
        /// </summary>
        /// <param name="ProcessesName">进程名称(该参数不包含.exe)</param>
        /// <returns>0-进程没有运行,1-进程正常高运行,2-进程没有响应</returns>
        public static int GetProcessesStateByName(string ProcessesName)
        {
            int ps = 0;
            Process[] p = Process.GetProcessesByName(ProcessesName);
            if (p.Length > 0)
            {
                Process _p = p[0];
                if (_p.Responding)
                    ps = 1;
                else
                    ps = 2;
            }
            return ps;
        }

        /// <summary>
        /// 获取进程是否运行
        /// </summary>
        /// <param name="ProcessesName">进程名称(该参数不包含.exe)</param>
        /// <returns>true-进程正在运行,false-进程没有运行</returns>
        public static bool GetProcessesExistByName(string ProcessesName)
        {
            bool ps = false;
            Process[] p = Process.GetProcessesByName(ProcessesName);
            if (p.Length > 0)
                ps = true;
            return ps;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        //public static string SendMessage(string msg)
        //{
        //    ClientSocket cs = new ClientSocket();
        //    return cs.SendMsg(msg);
        //}

        /// <summary>
        /// 获取minValue-maxValue之间随机数
        /// </summary>
        /// <returns></returns>
        public static int GetRandom(int minValue, int maxValue)
        {
            Random r = new Random();
            return r.Next(minValue, maxValue);
        }

        //执行命令
        public static void RunExe(string exe, string argument)
        {
            Process p = new Process();
            p.StartInfo.CreateNoWindow = true;//设定不显示窗口
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = exe;
            p.StartInfo.Arguments = argument;
            p.Start();
        }

        public static byte[] ImageGdi(Bitmap bmp)
        {
            Bitmap xbmp = new Bitmap(bmp);
            MemoryStream ms = new MemoryStream();
            xbmp.Save(ms, ImageFormat.Jpeg);
            byte[] buffer;
            ms.Flush();
            if (ms.Length > 20000)
            {
                //buffer = ms.GetBuffer();
                double new_width = 0;
                double new_height = 0;

                Image m_src_image = Image.FromStream(ms);
                if (m_src_image.Width >= m_src_image.Height)
                {
                    new_width = 800;
                    new_height = new_width * m_src_image.Height / (double)m_src_image.Width;
                }
                else if (m_src_image.Height >= m_src_image.Width)
                {
                    new_height = 600;
                    new_width = new_height * m_src_image.Width / (double)m_src_image.Height;
                }

                Bitmap bbmp = new Bitmap((int)new_width, (int)new_height, m_src_image.PixelFormat);
                Graphics m_graphics = Graphics.FromImage(bbmp);
                m_graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                m_graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                m_graphics.DrawImage(m_src_image, 0, 0, bbmp.Width, bbmp.Height);

                ms = new MemoryStream();

                bbmp.Save(ms, ImageFormat.Jpeg);
                buffer = ms.GetBuffer();
                ms.Close();

                return buffer;
            }
            else
            {
                buffer = ms.GetBuffer();
                ms.Close();
                return buffer;
            }
        }
    }
}
