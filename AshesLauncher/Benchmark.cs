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
    public partial class Benchmark : Form {
        private static readonly Benchmark instance = new Benchmark();
        static Benchmark() { }
        public static Benchmark Instance {
            get { return instance; }
        }

        public void GetOpenBenchMarkResults(ref CheckBox _chb) {
            _chb = chbOpenBenchmarkResults;
        }

        private Benchmark() {
            InitializeComponent();

            TopLevel = false;
            FormBorderStyle = FormBorderStyle.None;
            Visible = true;
            Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
        }

        /*
        private void RunProcess(string _fileName) {
            Process proc = new Process();
            proc.StartInfo.FileName = _fileName;
            proc.Start();
        }
        */

        private void btnRunGpuFocussed_Click(object sender, EventArgs e) {
            if (OptionsManager.Instance.HasSettings) RunBenchmark("GPU Focussed");
        }

        private void btnRunCpuFocussed_Click(object sender, EventArgs e) {
            if (OptionsManager.Instance.HasSettings) RunBenchmark("CPU Focussed");
        }

        private void RunBenchmark(string _benchmark) {
            //OptionsManager.Instance.Init(Main.GAME_SETTINGS_LOCATION);
            string prevOption = OptionsManager.Instance.GetOriginalSetting(GameOptions.Options.Benchmark.AUTO_RUN);

            //Only update the settings file if we need to change an option
            bool updateSettings = _benchmark != prevOption;
            if (updateSettings) {
                OptionsManager.Instance.UpdateSettingValue(GameOptions.Options.Benchmark.AUTO_RUN, _benchmark);
                OptionsManager.Instance.SaveSettingsFile();
            }

            //Run the Steam game
            Main.RunGame();

            //Auto running benchmark saves output files in base directory :(
            //Wait for benchmark to finish
            bool completed = WaitForNewFileCreation(GameOptions.MY_GAMES_LOCATION, GameOptions.BENCHMARK_FILE_SEARCH_PATTERN);

            //Restore the previous saved Benchmark option
            if (updateSettings) {
                OptionsManager.Instance.UpdateSettingValue(GameOptions.Options.Benchmark.AUTO_RUN, prevOption);
                OptionsManager.Instance.SaveSettingsFile();
            }

            if (!completed) return;

            DirectoryInfo di = new DirectoryInfo(GameOptions.MY_GAMES_LOCATION);
            FileSystemInfo[] files = di.GetFileSystemInfos(GameOptions.BENCHMARK_FILE_SEARCH_PATTERN);
            var orderedFiles = files.OrderBy(f => f.CreationTime).ToArray();
            string recentFile = orderedFiles[orderedFiles.Length - 1].FullName;

            using (StreamReader sr = new StreamReader(recentFile)) {
                string line, header, value;
                bool cancel = false;
                while ((line = sr.ReadLine()) != null) {
                    if (line.Contains("Total Avg Results")) {
                        StringBuilder sb = new StringBuilder();
                        while ((line = sr.ReadLine()) != null) {
                            if (line.Contains("===")) break;

                            header = line.Substring(0, line.IndexOf("\t"));     //Heading
                            value = line.Remove(0, header.Length + 1);          //Value
                            value = value.Replace("\t", "");                    //Remove tabs

                            sb.Append(header);
                            sb.AppendLine();
                            sb.Append(value.Trim());
                            sb.AppendLine();
                            sb.AppendLine();
                        }
                        tbBenchmarkSummary.Text = sb.ToString().Trim();
                        cancel = true;
                    }
                    if (cancel) break;
                }
            }
            //Open the benchmark results file?
            if (chbOpenBenchmarkResults.Checked) Process.Start(recentFile);// RunProcess(recentFile);
        }

        private bool WaitForNewFileCreation(string _fileLocation, string _searchPattern) {
            WaitOnFile wait = new WaitOnFile(_fileLocation, _searchPattern);
            DialogResult dr = wait.ShowDialog();
            if (dr == DialogResult.OK) {
                return true;
            }

            return false;
        }

        private void btnOpenBenchmarkFolder_Click(object sender, EventArgs e) {
            Process.Start("explorer.exe", GameOptions.MY_GAMES_LOCATION);
        }
    }
}
