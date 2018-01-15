namespace AshesLauncher {
    partial class InstallLocation {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallLocation));
            this.llbInstallPath = new System.Windows.Forms.LinkLabel();
            this.lblInstalFolder = new System.Windows.Forms.Label();
            this.btnChangePath = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.fbdInstallFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.chbUseSteam = new System.Windows.Forms.CheckBox();
            this.lblLaunchOptions = new System.Windows.Forms.Label();
            this.tbLaunchOptions = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // llbInstallPath
            // 
            this.llbInstallPath.AutoSize = true;
            this.llbInstallPath.Location = new System.Drawing.Point(114, 40);
            this.llbInstallPath.Name = "llbInstallPath";
            this.llbInstallPath.Size = new System.Drawing.Size(162, 13);
            this.llbInstallPath.TabIndex = 0;
            this.llbInstallPath.TabStop = true;
            this.llbInstallPath.Text = "Path to non steam game location";
            this.llbInstallPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbInstallPath_LinkClicked);
            // 
            // lblInstalFolder
            // 
            this.lblInstalFolder.AutoSize = true;
            this.lblInstalFolder.Location = new System.Drawing.Point(42, 40);
            this.lblInstalFolder.Name = "lblInstalFolder";
            this.lblInstalFolder.Size = new System.Drawing.Size(66, 13);
            this.lblInstalFolder.TabIndex = 1;
            this.lblInstalFolder.Text = "Install Folder";
            // 
            // btnChangePath
            // 
            this.btnChangePath.Location = new System.Drawing.Point(12, 35);
            this.btnChangePath.Name = "btnChangePath";
            this.btnChangePath.Size = new System.Drawing.Size(24, 23);
            this.btnChangePath.TabIndex = 2;
            this.btnChangePath.Text = "...";
            this.btnChangePath.UseVisualStyleBackColor = true;
            this.btnChangePath.Click += new System.EventHandler(this.btnChangePath_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(11, 90);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(92, 90);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chbUseSteam
            // 
            this.chbUseSteam.AutoSize = true;
            this.chbUseSteam.Location = new System.Drawing.Point(12, 12);
            this.chbUseSteam.Name = "chbUseSteam";
            this.chbUseSteam.Size = new System.Drawing.Size(129, 17);
            this.chbUseSteam.TabIndex = 5;
            this.chbUseSteam.Text = "Steam Installed Game";
            this.chbUseSteam.UseVisualStyleBackColor = true;
            this.chbUseSteam.CheckedChanged += new System.EventHandler(this.chbUseSteam_CheckedChanged);
            // 
            // lblLaunchOptions
            // 
            this.lblLaunchOptions.AutoSize = true;
            this.lblLaunchOptions.Location = new System.Drawing.Point(9, 67);
            this.lblLaunchOptions.Name = "lblLaunchOptions";
            this.lblLaunchOptions.Size = new System.Drawing.Size(82, 13);
            this.lblLaunchOptions.TabIndex = 6;
            this.lblLaunchOptions.Text = "Launch Options";
            // 
            // tbLaunchOptions
            // 
            this.tbLaunchOptions.Location = new System.Drawing.Point(97, 64);
            this.tbLaunchOptions.MaxLength = 500;
            this.tbLaunchOptions.Name = "tbLaunchOptions";
            this.tbLaunchOptions.Size = new System.Drawing.Size(465, 20);
            this.tbLaunchOptions.TabIndex = 7;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(173, 95);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(370, 13);
            this.lblNote.TabIndex = 8;
            this.lblNote.Text = "NOTE: You may need to restart Steam for new Launch Options to take effect";
            // 
            // InstallLocation
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(574, 121);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.tbLaunchOptions);
            this.Controls.Add(this.lblLaunchOptions);
            this.Controls.Add(this.chbUseSteam);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnChangePath);
            this.Controls.Add(this.lblInstalFolder);
            this.Controls.Add(this.llbInstallPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstallLocation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AoTS Install Folder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel llbInstallPath;
        private System.Windows.Forms.Label lblInstalFolder;
        private System.Windows.Forms.Button btnChangePath;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FolderBrowserDialog fbdInstallFolder;
        private System.Windows.Forms.CheckBox chbUseSteam;
        private System.Windows.Forms.Label lblLaunchOptions;
        private System.Windows.Forms.TextBox tbLaunchOptions;
        private System.Windows.Forms.Label lblNote;
    }
}