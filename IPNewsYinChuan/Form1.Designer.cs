namespace IPNewsYinChuan
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnExit = new System.Windows.Forms.Button();
            this.labErr = new System.Windows.Forms.Label();
            this.btnReg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.Transparent;
            this.btnExit.Location = new System.Drawing.Point(0, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(30, 30);
            this.btnExit.TabIndex = 1;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // labErr
            // 
            this.labErr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labErr.Font = new System.Drawing.Font("宋体", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labErr.ForeColor = System.Drawing.Color.Red;
            this.labErr.Location = new System.Drawing.Point(0, 0);
            this.labErr.Name = "labErr";
            this.labErr.Size = new System.Drawing.Size(408, 388);
            this.labErr.TabIndex = 0;
            this.labErr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labErr.Visible = false;
            this.labErr.Click += new System.EventHandler(this.labErr_Click);
            // 
            // btnReg
            // 
            this.btnReg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReg.Location = new System.Drawing.Point(0, 0);
            this.btnReg.Name = "btnReg";
            this.btnReg.Size = new System.Drawing.Size(408, 388);
            this.btnReg.TabIndex = 2;
            this.btnReg.Text = "注册";
            this.btnReg.UseVisualStyleBackColor = true;
            this.btnReg.Visible = false;
            this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(408, 388);
            this.Controls.Add(this.btnReg);
            this.Controls.Add(this.labErr);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "银川日报客户端";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label labErr;
        private System.Windows.Forms.Button btnReg;
    }
}

