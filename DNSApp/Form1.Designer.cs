namespace DNSApp
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblLastTime = new System.Windows.Forms.Label();
            this.lblNewDNS = new System.Windows.Forms.Label();
            this.txtNewDNS = new System.Windows.Forms.TextBox();
            this.txtCurDNS = new System.Windows.Forms.TextBox();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnActivate = new System.Windows.Forms.Button();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.txtLastTime = new System.Windows.Forms.Label();
            this.txtInstallNum = new System.Windows.Forms.Label();
            this.txtUsedTime = new System.Windows.Forms.Label();
            this.linkGoogle = new System.Windows.Forms.LinkLabel();
            this.linkYahoo = new System.Windows.Forms.LinkLabel();
            this.linkBing = new System.Windows.Forms.LinkLabel();
            this.timerRunTime = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblLastTime
            // 
            this.lblLastTime.AutoSize = true;
            this.lblLastTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastTime.Location = new System.Drawing.Point(48, 20);
            this.lblLastTime.Name = "lblLastTime";
            this.lblLastTime.Size = new System.Drawing.Size(191, 29);
            this.lblLastTime.TabIndex = 4;
            this.lblLastTime.Text = "App installed on:";
            // 
            // lblNewDNS
            // 
            this.lblNewDNS.AutoSize = true;
            this.lblNewDNS.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewDNS.Location = new System.Drawing.Point(71, 212);
            this.lblNewDNS.Name = "lblNewDNS";
            this.lblNewDNS.Size = new System.Drawing.Size(82, 19);
            this.lblNewDNS.TabIndex = 2;
            this.lblNewDNS.Text = "New DNS:";
            this.lblNewDNS.Visible = false;
            // 
            // txtNewDNS
            // 
            this.txtNewDNS.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewDNS.Location = new System.Drawing.Point(159, 209);
            this.txtNewDNS.Name = "txtNewDNS";
            this.txtNewDNS.Size = new System.Drawing.Size(251, 27);
            this.txtNewDNS.TabIndex = 3;
            this.txtNewDNS.Visible = false;
            // 
            // txtCurDNS
            // 
            this.txtCurDNS.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurDNS.Location = new System.Drawing.Point(159, 166);
            this.txtCurDNS.Name = "txtCurDNS";
            this.txtCurDNS.Size = new System.Drawing.Size(251, 27);
            this.txtCurDNS.TabIndex = 1;
            this.txtCurDNS.Visible = false;
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrent.Location = new System.Drawing.Point(49, 168);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(104, 19);
            this.lblCurrent.TabIndex = 0;
            this.lblCurrent.Text = "Current DNS:";
            this.lblCurrent.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(81, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 29);
            this.label1.TabIndex = 6;
            this.label1.Text = "App Installed:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 29);
            this.label2.TabIndex = 8;
            this.label2.Text = "Has been used for:";
            // 
            // btnActivate
            // 
            this.btnActivate.BackColor = System.Drawing.Color.YellowGreen;
            this.btnActivate.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.btnActivate.FlatAppearance.BorderSize = 15;
            this.btnActivate.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivate.ForeColor = System.Drawing.Color.Red;
            this.btnActivate.Location = new System.Drawing.Point(288, 254);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(440, 114);
            this.btnActivate.TabIndex = 10;
            this.btnActivate.UseVisualStyleBackColor = false;
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // timerMain
            // 
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // txtLastTime
            // 
            this.txtLastTime.AutoSize = true;
            this.txtLastTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastTime.Location = new System.Drawing.Point(243, 20);
            this.txtLastTime.Name = "txtLastTime";
            this.txtLastTime.Size = new System.Drawing.Size(0, 29);
            this.txtLastTime.TabIndex = 11;
            // 
            // txtInstallNum
            // 
            this.txtInstallNum.AutoSize = true;
            this.txtInstallNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInstallNum.Location = new System.Drawing.Point(243, 63);
            this.txtInstallNum.Name = "txtInstallNum";
            this.txtInstallNum.Size = new System.Drawing.Size(0, 29);
            this.txtInstallNum.TabIndex = 12;
            // 
            // txtUsedTime
            // 
            this.txtUsedTime.AutoSize = true;
            this.txtUsedTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsedTime.Location = new System.Drawing.Point(243, 109);
            this.txtUsedTime.Name = "txtUsedTime";
            this.txtUsedTime.Size = new System.Drawing.Size(0, 29);
            this.txtUsedTime.TabIndex = 13;
            // 
            // linkGoogle
            // 
            this.linkGoogle.AutoSize = true;
            this.linkGoogle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkGoogle.Location = new System.Drawing.Point(59, 526);
            this.linkGoogle.Name = "linkGoogle";
            this.linkGoogle.Size = new System.Drawing.Size(213, 29);
            this.linkGoogle.TabIndex = 14;
            this.linkGoogle.TabStop = true;
            this.linkGoogle.Text = "Open google.com ";
            this.linkGoogle.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGoogle_LinkClicked);
            // 
            // linkYahoo
            // 
            this.linkYahoo.AutoSize = true;
            this.linkYahoo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkYahoo.Location = new System.Drawing.Point(418, 526);
            this.linkYahoo.Name = "linkYahoo";
            this.linkYahoo.Size = new System.Drawing.Size(196, 29);
            this.linkYahoo.TabIndex = 15;
            this.linkYahoo.TabStop = true;
            this.linkYahoo.Text = "Open yahoo.com";
            this.linkYahoo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkYahoo_LinkClicked);
            // 
            // linkBing
            // 
            this.linkBing.AutoSize = true;
            this.linkBing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkBing.Location = new System.Drawing.Point(793, 526);
            this.linkBing.Name = "linkBing";
            this.linkBing.Size = new System.Drawing.Size(178, 29);
            this.linkBing.TabIndex = 16;
            this.linkBing.TabStop = true;
            this.linkBing.Text = "Open bing.com";
            this.linkBing.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkBing_LinkClicked);
            // 
            // timerRunTime
            // 
            this.timerRunTime.Interval = 5000;
            this.timerRunTime.Tick += new System.EventHandler(this.timerRunTime_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1047, 580);
            this.Controls.Add(this.linkBing);
            this.Controls.Add(this.linkYahoo);
            this.Controls.Add(this.linkGoogle);
            this.Controls.Add(this.txtUsedTime);
            this.Controls.Add(this.txtInstallNum);
            this.Controls.Add(this.txtLastTime);
            this.Controls.Add(this.btnActivate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLastTime);
            this.Controls.Add(this.txtNewDNS);
            this.Controls.Add(this.lblNewDNS);
            this.Controls.Add(this.txtCurDNS);
            this.Controls.Add(this.lblCurrent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "DNS Application";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblLastTime;
        private System.Windows.Forms.Label lblNewDNS;
        private System.Windows.Forms.TextBox txtNewDNS;
        private System.Windows.Forms.TextBox txtCurDNS;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnActivate;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.Label txtLastTime;
        private System.Windows.Forms.Label txtInstallNum;
        private System.Windows.Forms.Label txtUsedTime;
        private System.Windows.Forms.LinkLabel linkGoogle;
        private System.Windows.Forms.LinkLabel linkYahoo;
        private System.Windows.Forms.LinkLabel linkBing;
        private System.Windows.Forms.Timer timerRunTime;
    }
}

