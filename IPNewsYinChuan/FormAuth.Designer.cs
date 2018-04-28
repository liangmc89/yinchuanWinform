namespace IPNewsYinChuan
{
    partial class FormAuth
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAuth));
            this.btnRegister = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbScreenName = new System.Windows.Forms.TextBox();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.btnCancle = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMc = new System.Windows.Forms.TextBox();
            this.tbMMN = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRegister
            // 
            this.btnRegister.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRegister.Location = new System.Drawing.Point(161, 199);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(100, 34);
            this.btnRegister.TabIndex = 0;
            this.btnRegister.Text = "注册";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "机器名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "描    述";
            // 
            // tbScreenName
            // 
            this.tbScreenName.Location = new System.Drawing.Point(107, 45);
            this.tbScreenName.Name = "tbScreenName";
            this.tbScreenName.Size = new System.Drawing.Size(164, 21);
            this.tbScreenName.TabIndex = 3;
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(107, 83);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(164, 21);
            this.tbDescription.TabIndex = 4;
            // 
            // btnCancle
            // 
            this.btnCancle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancle.Location = new System.Drawing.Point(26, 199);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(100, 34);
            this.btnCancle.TabIndex = 5;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "您尚未注册，请注册后使用";
            // 
            // tbMc
            // 
            this.tbMc.Location = new System.Drawing.Point(107, 159);
            this.tbMc.Name = "tbMc";
            this.tbMc.Size = new System.Drawing.Size(164, 21);
            this.tbMc.TabIndex = 10;
            // 
            // tbMMN
            // 
            this.tbMMN.Location = new System.Drawing.Point(107, 121);
            this.tbMMN.Name = "tbMMN";
            this.tbMMN.Size = new System.Drawing.Size(164, 21);
            this.tbMMN.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "TeamViewerPwd";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "TeamViewerId";
            // 
            // FormAuth
            // 
            this.AcceptButton = this.btnRegister;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 250);
            this.Controls.Add(this.tbMc);
            this.Controls.Add(this.tbMMN);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.tbScreenName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRegister);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 250);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 250);
            this.Name = "FormAuth";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "注册";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbScreenName;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMc;
        private System.Windows.Forms.TextBox tbMMN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}