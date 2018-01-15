using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AshesLauncher {
    public class AppSettings {
        private static readonly AppSettings instance = new AppSettings();
        static AppSettings() { }
        private AppSettings() { }
        public static AppSettings Instance {
            get { return instance; }
        }

        private static readonly string APP_DATA_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"GraceGraceMedia\AOTSE_Launcher");
        private static readonly string SETTINGS_FILE = Path.Combine(APP_DATA_PATH, "settings.ini");
        public static readonly string GAME_EXE = "AshesEscalation.exe";

        public class Settings {
            public const string APP_VERSION = "AppVersion";
            public const string STEAM_GAME = "SteamInstalledGame";
            public const string GAME_LOCATION = "GameLocation";
            public const string LAUNCH_OPTIONS = "GameLaunchOptions";
            public const string OPEN_BENCHMARK_RESULTS = "OpenBenchMarkResults";
            public const string NEXT_TAB_ON_SAVE = "SelectNextTabOnSave";
        }

        //TODO: When adding new settings, update the app version and write some code
        //that will keep an existing user options and migrate over to the newer version
        private string[,] settings = new string[,] {
            { Settings.APP_VERSION, "1" },
            { Settings.STEAM_GAME, "0" },
            { Settings.GAME_LOCATION, "" },
            { Settings.LAUNCH_OPTIONS, "" },
            { Settings.OPEN_BENCHMARK_RESULTS, "0" },
            { Settings.NEXT_TAB_ON_SAVE, "0" }
        };

        public void LoadAppSettings() {
            //Create the directory
            Directory.CreateDirectory(AppSettings.APP_DATA_PATH);

            //Check file exists
            if (File.Exists(AppSettings.SETTINGS_FILE)) {
                string line;
                int i = 0;
                using (StreamReader sr = new StreamReader(AppSettings.SETTINGS_FILE)) {
                    while ((line = sr.ReadLine()) != null) {
                        settings[i, 0] = line.Substring(0, line.IndexOf("="));      //Name
                        settings[i, 1] = line.Remove(0, settings[i, 0].Length + 1); //Value
                        i++;
                    }
                }
            }
        }

        public string GetAppSetting(string _name) {
            for (int i = 0; i < settings.GetLength(0); i++) {
                if (settings[i, 0] == _name) return settings[i, 1];
            }
            return "";
        }

        public void UpdateAppSetting(string _name, string _value) {
            for (int i = 0; i < settings.GetLength(0); i++) {
                if (settings[i, 0] == _name) {
                    settings[i, 1] = _value;
                    break;
                }
            }

            SaveAppSettings();
        }

        private void SaveAppSettings() {
            //Create the directory
            Directory.CreateDirectory(AppSettings.APP_DATA_PATH);

            //Write the file
            using (StreamWriter sw = new StreamWriter(AppSettings.SETTINGS_FILE)) {
                for (int i = 0; i < settings.GetLength(0); i++) {
                    sw.WriteLine(string.Concat(settings[i, 0], "=", settings[i, 1]));
                }
            }
        }

        public void SetupCheckBox(string _settingKey, ref CheckBox _chb) {
            _chb.Tag = _settingKey;
            _chb.Checked = GetAppSetting(_settingKey) == "1" ? true : false;
            _chb.CheckedChanged += UpdateAppSetting;
        }

        public void UpdateAppSetting(object sender, EventArgs e) {
            Type t = sender.GetType();
            if (t == typeof(CheckBox)) {
                CheckBox chb = (CheckBox)sender;
                AppSettings.Instance.UpdateAppSetting(chb.Tag.ToString(), chb.Checked ? "1" : "0");
            }
        }
    }
}
