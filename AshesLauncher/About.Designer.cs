namespace AshesLauncher {
    partial class About {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.btnOK = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.llbDiscord = new System.Windows.Forms.LinkLabel();
            this.llbYouTube = new System.Windows.Forms.LinkLabel();
            this.llbAshesForum = new System.Windows.Forms.LinkLabel();
            this.llbSteamForums = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(130, 203);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(322, 116);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Useful Links";
            // 
            // llbDiscord
            // 
            this.llbDiscord.AutoSize = true;
            this.llbDiscord.Location = new System.Drawing.Point(12, 147);
            this.llbDiscord.Name = "llbDiscord";
            this.llbDiscord.Size = new System.Drawing.Size(129, 13);
            this.llbDiscord.TabIndex = 5;
            this.llbDiscord.TabStop = true;
            this.llbDiscord.Tag = "https://discord.gg/CvyYw4j";
            this.llbDiscord.Text = "Ashes Discord Community";
            this.llbDiscord.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpenLabelLink);
            // 
            // llbYouTube
            // 
            this.llbYouTube.AutoSize = true;
            this.llbYouTube.Location = new System.Drawing.Point(181, 163);
            this.llbYouTube.Name = "llbYouTube";
            this.llbYouTube.Size = new System.Drawing.Size(153, 13);
            this.llbYouTube.TabIndex = 7;
            this.llbYouTube.TabStop = true;
            this.llbYouTube.Tag = "https://www.youtube.com/centaurianmudpig";
            this.llbYouTube.Text = "centaurianmudpig on YouTube";
            this.llbYouTube.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpenLabelLink);
            // 
            // llbAshesForum
            // 
            this.llbAshesForum.AutoSize = true;
            this.llbAshesForum.Location = new System.Drawing.Point(12, 163);
            this.llbAshesForum.Name = "llbAshesForum";
            this.llbAshesForum.Size = new System.Drawing.Size(134, 13);
            this.llbAshesForum.TabIndex = 8;
            this.llbAshesForum.TabStop = true;
            this.llbAshesForum.Tag = "https://forums.ashesofthesingularity.com/";
            this.llbAshesForum.Text = "Ashes Forums on Stardock";
            this.llbAshesForum.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpenLabelLink);
            // 
            // llbSteamForums
            // 
            this.llbSteamForums.AutoSize = true;
            this.llbSteamForums.Location = new System.Drawing.Point(12, 179);
            this.llbSteamForums.Name = "llbSteamForums";
            this.llbSteamForums.Size = new System.Drawing.Size(121, 13);
            this.llbSteamForums.TabIndex = 9;
            this.llbSteamForums.TabStop = true;
            this.llbSteamForums.Tag = "http://steamcommunity.com/app/507490/discussions/";
            this.llbSteamForums.Text = "Ashes Forums on Steam";
            this.llbSteamForums.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpenLabelLink);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(181, 147);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(148, 13);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Tag = "https://www.youtube.com/user/stardockgames";
            this.linkLabel1.Text = "Stardock Games on YouTube";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpenLabelLink);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(181, 179);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(75, 13);
            this.linkLabel2.TabIndex = 11;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Tag = "http://www.moddb.com/mods/aotsescalation-custom-launcher/tutorials/aotsescalation" +
    "-custom-launcher-help";
            this.linkLabel2.Text = "MODDB Page";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpenLabelLink);
            // 
            // About
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 235);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.llbSteamForums);
            this.Controls.Add(this.llbAshesForum);
            this.Controls.Add(this.llbYouTube);
            this.Controls.Add(this.llbDiscord);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel llbDiscord;
        private System.Windows.Forms.LinkLabel llbYouTube;
        private System.Windows.Forms.LinkLabel llbAshesForum;
        private System.Windows.Forms.LinkLabel llbSteamForums;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
    }
}