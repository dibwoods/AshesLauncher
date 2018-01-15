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
using System.Threading;

namespace AshesLauncher {
    public partial class WaitOnFile : Form {
        private string fileLocation, searchPattern;

        BackgroundWorker fileChecker;
        bool firstCheck;

        int numOfFiles;

        public WaitOnFile(string _fileLocation, string _searchPattern) {
            InitializeComponent();
            fileLocation = _fileLocation;
            searchPattern = _searchPattern;
            Wait();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (fileChecker != null) fileChecker.CancelAsync();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Wait() {
            if (fileLocation.Length == 0) {
                DialogResult = DialogResult.Abort;
                Close();
            }

            firstCheck = false;
            fileChecker = new BackgroundWorker();

            fileChecker.WorkerSupportsCancellation = true;
            fileChecker.DoWork += fileChecker_CheckFile;
            fileChecker.RunWorkerCompleted += fileChecker_Completed;
            fileChecker.RunWorkerAsync();
            //while (fileChecker.CancellationPending) { }
            //Close();
        }

        private void WaitOnFile_Shown(object sender, EventArgs e) {
            Wait();
        }

        private void fileChecker_Completed(object sender, RunWorkerCompletedEventArgs args) {
            Close();
        }

        private void fileChecker_CheckFile(object sender, DoWorkEventArgs args) {
            while(!fileChecker.CancellationPending)
            {
                if (!firstCheck) {
                    firstCheck = true;
                    numOfFiles = Directory.GetFiles(fileLocation, searchPattern).Length;
                }
                else {
                    Thread.Sleep(3000);
                    if (Directory.GetFiles(fileLocation, searchPattern).Length != numOfFiles) {
                        DialogResult = DialogResult.OK;
                        break;
                    }
                }
            }
            args.Cancel = true;
        }
    }
}
