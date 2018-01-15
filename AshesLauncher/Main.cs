using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using SteamConfigHelper;

namespace AshesLauncher {
    public partial class Main : Form {
        private int tabIndex = 0;

        public Main() {
            InitializeComponent();

            GameOptions go = GameOptions.Instance;
            go.Init();
            tbpGameOptions.Controls.Add(go);
            
            Benchmark bm = Benchmark.Instance;
            tbpBenchmark.Controls.Add(bm);

            AppSettings.Instance.LoadAppSettings();

            //Open benchmark results
            CheckBox benchmarkOpenResults = null;
            Benchmark.Instance.GetOpenBenchMarkResults(ref  benchmarkOpenResults);
            AppSettings.Instance.SetupCheckBox(AppSettings.Settings.OPEN_BENCHMARK_RESULTS, ref benchmarkOpenResults);

            //Open the next tab on save
            CheckBox openNextTabOnSave = null;
            GameOptions.Instance.GetOpenNextTabOnSave(ref openNextTabOnSave);
            AppSettings.Instance.SetupCheckBox(AppSettings.Settings.NEXT_TAB_ON_SAVE, ref openNextTabOnSave);

            tabIndex = tbcMain.SelectedIndex;
        }

        private void installFolderToolStripMenuItem_Click(object sender, EventArgs e) {
            SteamLocalConfig steamConfig;
            bool hasSteamInstalled = false;

            bool steamGame = AppSettings.Instance.GetAppSetting(AppSettings.Settings.STEAM_GAME) == "1" ? true : false;
            try {
                steamConfig = new SteamLocalConfig();
                hasSteamInstalled = true;
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
                steamConfig = null;
            }
            
            string launchOptions = "";

            //Use launch options saved in Steam, or the apps settings if not a Steam game
            if (hasSteamInstalled && steamGame) steamConfig.GetFirstFoundAppSetting("507490", "LaunchOptions", out launchOptions);
            else launchOptions = AppSettings.Instance.GetAppSetting(AppSettings.Settings.LAUNCH_OPTIONS);

            //Determine how we are using Steam
            InstallLocation.SteamGameUsage usage;
            if (!hasSteamInstalled) usage = InstallLocation.SteamGameUsage.No;
            else if (steamGame) usage = InstallLocation.SteamGameUsage.Enabled;
            else usage = InstallLocation.SteamGameUsage.Disabled;

            InstallLocation popup = new InstallLocation(
                usage,
                AppSettings.Instance.GetAppSetting(AppSettings.Settings.GAME_LOCATION),
                launchOptions,
                AppSettings.GAME_EXE
            );

            if (popup.ShowDialog() == DialogResult.OK) {
                AppSettings.Instance.UpdateAppSetting(AppSettings.Settings.STEAM_GAME, popup.UsingSteam ? "1" : "0");
                AppSettings.Instance.UpdateAppSetting(AppSettings.Settings.GAME_LOCATION, popup.InstallPath);
                AppSettings.Instance.UpdateAppSetting(AppSettings.Settings.LAUNCH_OPTIONS, popup.LaunchOptions);
                //Try and save the launch options within the steam config file
                if (hasSteamInstalled) {
                    //Display a message to say failed to save settings
                    if (!steamConfig.SetAppSetting("507490", "LaunchOptions", popup.LaunchOptions))
                        MessageBox.Show("Failed to save the Launch Options into the Steam Config file.  Try running the game at least once or manually add the launch options within Steam", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private bool CheckSaveBeforeExiting() {
            if (GameOptions.Instance.UnSavedChanges()) {
                DialogResult dr = MessageBox.Show("You have unsaved changes.  Do you wish to save?", "Save changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                //Yes - Save changes
                if (dr == DialogResult.Yes) OptionsManager.Instance.SaveSettingsFile();
                //Cancel, don't exit the app
                else if (dr == DialogResult.Cancel) return false;
            }

            return true;
        }

        private bool CheckSaveChanges(out DialogResult _dialogueResult) {
            if (GameOptions.Instance.UnSavedChanges()) {
                _dialogueResult = MessageBox.Show("You have unsaved changes.  Do you wish to save?", "Save changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (DialogResult != DialogResult.Yes) return false;
            }
            _dialogueResult = DialogResult.Yes;
            return true;
        }

            private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            if (CheckSaveBeforeExiting()) Close();
        }

        private void btnExit_Click(object sender, EventArgs e) {
            if (CheckSaveBeforeExiting()) Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            About about = new About();
            about.ShowDialog();
        }

        private void btnPlay_Click(object sender, EventArgs e) {
            CheckSaveChanges(out DialogResult _dr);

            //Go back to the previous (GameOptions) tab
            if (_dr == DialogResult.Cancel) return;
            //Undo any unsaved changes
            else if (_dr == DialogResult.No) GameOptions.Instance.Init();
            //Save the settings
            else {
                //TODO hide OptionsManager.Instance.SaveSettingsFile behind GameOptions
                OptionsManager.Instance.SaveSettingsFile();
                //We saved so reset Cancel Button
                GameOptions.Instance.ResetCancelButton();
            }
            
            

            RunGame();
        }

        public static void RunGame() {
            string file;

            //Are we running the game from Steam or from the EXE?
            if (AppSettings.Instance.GetAppSetting(AppSettings.Settings.STEAM_GAME) == "1")
                file = GameOptions.STEAM_RUN_GAME_URL;
            else file = Path.Combine(
                AppSettings.Instance.GetAppSetting(AppSettings.Settings.GAME_LOCATION),
                AppSettings.GAME_EXE
            );

            //Any launch optoins to add?
            string launchOptions = AppSettings.Instance.GetAppSetting(AppSettings.Settings.LAUNCH_OPTIONS);

            //Run the game!
            Process.Start(file, launchOptions);
        }

        private void playGameToolStripMenuItem_Click(object sender, EventArgs e) {
            RunGame();
        }

        private void tbcMain_SelectedIndexChanged(object sender, EventArgs e) {
            //Not exiting, but changing tab (same diff :o) )
            if (tbcMain.SelectedIndex > 0) {
                CheckSaveChanges(out DialogResult _dr);
                //Go back to the previous (GameOptions) tab
                if (_dr == DialogResult.Cancel) tbcMain.SelectedIndex = tabIndex;
                //Undo any unsaved changes
                else if (_dr == DialogResult.No) GameOptions.Instance.Init();
                //Save the settings
                else OptionsManager.Instance.SaveSettingsFile();
            }
            else tabIndex = tbcMain.SelectedIndex;
        }
    }
}
