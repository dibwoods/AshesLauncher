using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace AshesLauncher {
    public partial class InstallLocation : Form {
        public bool UsingSteam { get { return chbUseSteam.Checked; } }
        public string InstallPath { get { return llbInstallPath.Text; } }
        public string LaunchOptions { get { return tbLaunchOptions.Text.Trim(); } }
        private string checkForThisFile;

        public enum SteamGameUsage {
            No,
            Enabled,
            Disabled,
        }

        public InstallLocation(SteamGameUsage _usage, string _installPath, string _launchOptions, string _checkForFilePath) {
            InitializeComponent();

            if (_usage == SteamGameUsage.No) chbUseSteam.Enabled = false;
            else if (_usage == SteamGameUsage.Enabled) chbUseSteam.Checked = true;
            else if (_usage == SteamGameUsage.Disabled) chbUseSteam.Checked = false;

            chbUseSteam_CheckedChanged(null, null);
            llbInstallPath.Text = _installPath;

            int min = Math.Min(_launchOptions.Length, tbLaunchOptions.MaxLength);
            tbLaunchOptions.Text = _launchOptions.Substring(0, min).Trim();

            checkForThisFile = _checkForFilePath;
        }

        private void btnChangePath_Click(object sender, EventArgs e) {
            DialogResult result = fbdInstallFolder.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrEmpty(fbdInstallFolder.SelectedPath)) {
                llbInstallPath.Text = fbdInstallFolder.SelectedPath;
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (!chbUseSteam.Checked && !File.Exists(Path.Combine(llbInstallPath.Text, checkForThisFile))) {
                DialogResult dr = MessageBox.Show("The selected path appears to be invalid. \nDo you want to use this path?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No) return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void chbUseSteam_CheckedChanged(object sender, EventArgs e) {
            btnChangePath.Enabled = llbInstallPath.Enabled = lblInstalFolder.Enabled = !chbUseSteam.Checked;
            lblNote.Visible = chbUseSteam.Checked;
        }

        private void llbInstallPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (llbInstallPath.Text.Length == 0) return;

            try {
                Process.Start(llbInstallPath.Text);
            }
            catch (Exception ex) {
                Console.WriteLine("InstallLocation: Invalid file location to visit - ", llbInstallPath.Text, " Mess: ", ex.Message);
            }
        }
    }
}
