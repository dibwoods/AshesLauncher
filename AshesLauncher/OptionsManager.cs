using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AshesLauncher {
    class OptionsManager {
        /// <summary>
        /// Singleton 
        /// http://csharpindepth.com/Articles/General/Singleton.aspx
        /// </summary>
        private static readonly OptionsManager instance = new OptionsManager();
        static OptionsManager() { }
        private OptionsManager() {}
        public static OptionsManager Instance {
            get { return instance; }
        }

        //private string myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private string settingsFilePath;

        /// <summary>
        /// Settings is a copy of the settings.ini file
        /// </summary>
        private string[,] settings;

        /// <summary>
        /// All the settings with values (i.e. Fullscreen=0)
        /// Expect when there are duplcate settings (Multiplayer player settings)
        /// </summary>
        private Dictionary<string, string> userSettings;// = new Dictionary<string, string>();

        /// <summary>
        /// Settings that have multiple choices other than 1 and 0
        /// </summary>
        private Dictionary<string, object> userSettingRules;// = new Dictionary<string, object>();
        
        public bool HasSettings {  get { return settings.GetLength(0) > 0; } }

        public void Init(string _settingsFilePath) {
            userSettings = new Dictionary<string, string>();
            userSettingRules = new Dictionary<string, object>();
            settingsFilePath = _settingsFilePath;
            LoadSettingsFile();
        }

        public void SetupComboBox(string _settingKey, ref ComboBox _comboBox, string[,] _items) {
            SetupComboBox(_settingKey, ref _comboBox, _items, "");
        }

        public void SetupComboBox(string _settingKey, ref ComboBox _comboBox, string[,] _items, string _multiItemSeparator) {
            //Add the rules for the combo box
            try { userSettingRules.Add(_settingKey, new ComboSetting(_items, _multiItemSeparator)); }
            catch (Exception ex) { Console.WriteLine(string.Concat("SetupComboBox: ", _settingKey, " already exists.", ex.Message)); }

            _comboBox.Tag = _settingKey;
            _comboBox.Items.Clear();
            _comboBox.Items.AddRange(OptionsManager.Instance.GetPossibleGuiSettings(_settingKey));
            _comboBox.SelectedItem = OptionsManager.Instance.GetGuiSetting(_settingKey);
        }

        public void SetupTrackBar(string _settingKey, ref TrackBar _trackBar, ref Label _label, TrackBarSetting _trackBarSetting) {
            int value;

            //Add the rules for the track bar
            try { userSettingRules.Add(_settingKey, _trackBarSetting); }
            catch (Exception ex) { Console.WriteLine(string.Concat("SetupTrackBar: ", _settingKey, " already exists.", ex.Message)); }

            _trackBar.Tag = _label;                 //Store label as tag for future label updates
            _label.Tag = _settingKey;

            //Set the properties for the track bar
            _trackBarSetting.SetTrackBar(ref _trackBar);

            string settingValue = OptionsManager.Instance.GetGuiSetting(_settingKey);

            //Get the cursor scale from the settings file
            if (Int32.TryParse(settingValue, out value))                //Try converting to integer
                value = _trackBar.Minimum + (value * _trackBar.LargeChange);        //Calculate current value
            else if (float.TryParse(settingValue, out float result)) {  //Try converting to float
                value = (int)(result * 100);                            //Then get integer equivalent
            }
            else
                value = _trackBar.Minimum;                              //Assume the minimum value

            //Calculate the perceived scale percentage
            value = CalculateTrackBarValue(value, _trackBar.Minimum, _trackBar.Maximum, _trackBar.LargeChange);
            _trackBar.Value = value;

            //Update the % value on the label text
            int breakPos = _label.Text.IndexOf(":");
            string text = _label.Text;
            text = text.Remove(breakPos + 1, text.Length - breakPos - 1);
            _label.Text = string.Concat(text, " ", value, "%");
        }

        public void SetupCheckBox(string _settingKey, ref CheckBox _chb) {
            _chb.Tag = _settingKey;
            _chb.Checked = OptionsManager.Instance.GetGuiSettingFlag(_settingKey);
        }

        public int CalculateTrackBarValue(int _currValue, int _min, int _max, int _largeChange) {
            //TODO: Determine the direction of the scroll and change order of check accordingly
            //might fix glitchy-ness when reducing value
            int numIncrements = ((_max - _min) / _largeChange);

            for (int i = numIncrements; i >= 0; i--) {
                if (_currValue > _min + (i * _largeChange)) {
                    return _min + ((i + 1) * _largeChange);
                }
            }
            return _min;
        }

        private void LoadSettingsFile() {
            if (settingsFilePath.Length == 0) Console.WriteLine("OptionsManager.LoadSettingsFile: No file give.");
            userSettings.Clear();
            List<string> settingNames = new List<string>();
            List<string> settingValues = new List<string>();
            string line = "";
            int lineNum = 0;
            string settingName, settingValue;
            try {
                using (StreamReader sr = new StreamReader(settingsFilePath)) {
                    int breakPos;
                    while ((line = sr.ReadLine()) != null) {
                        //Grab the setting name and value
                        breakPos = line.IndexOf("=");
                        if (breakPos >= 0) {
                            settingName = line.Remove(breakPos, line.Length - breakPos);
                            settingValue = line.Remove(0, breakPos + 1);
                        }
                        //A header or blank line
                        else {
                            settingName = line;
                            settingValue = "";
                        }

                        //Temporarily store it
                        settingNames.Add(settingName);
                        settingValues.Add(settingValue);

                        //Check for duplicates
                        if (userSettings.ContainsKey(settingName))
                            Console.WriteLine(string.Concat("LoadSettings: ", settingName, " already exists."));

                        //Only store what has a setting value
                        else if (line.Contains("=")) {      
                            userSettings.Add(settingName, settingValue);
                            //Console.WriteLine(line.Remove(breakPos, line.Length - breakPos) + "=" + line.Remove(0, breakPos + 1) + ", " + lineNum);
                        }
                        lineNum++;
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(string.Concat(ex.Message, ": [", lineNum, "] ", line));
            }

            //Save the settings
            settings = new string[lineNum, 2];
            for(int i = 0; i < lineNum; i++) {
                settings[i, 0] = settingNames[i];
                settings[i, 1] = settingValues[i];
            }
        }

        public void SaveSettingsFile() {
            bool useUserSettings = true;
            int i = -1;
            try {
                using(StreamWriter sw = new StreamWriter(settingsFilePath)) {
                    int maxLength = settings.GetLength(0);
                    for (i = 0; i < maxLength; i++) {
                        //[SPGameSettings] has repeated setting names for each player
                        if (settings[i, 0] == "[SPGameSettings]") useUserSettings = false;
                        
                        //Write a setting value
                        if (useUserSettings && settings[i, 0].Length > 0 && settings[i, 1].Length > 0 && 
                                userSettings.TryGetValue(settings[i, 0], out string value)) {
                            sw.WriteLine(string.Concat(settings[i, 0], "=", value));
                        }
                        //Write back SPGameSettings that we don't change
                        else if (!useUserSettings && settings[i, 0].Length > 0 && settings[i, 1].Length > 0) {
                            sw.WriteLine(string.Concat(settings[i, 0], "=", settings[i, 1]));
                        }
                        //Write a header or blank line
                        else sw.WriteLine(settings[i, 0]);
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(string.Concat("SaveSettingsFile: [", i, "] ", ex.Message));
            }
        }

        /// <summary>
        /// ComboBox value
        /// </summary>
        /// <param name="_settingKey"></param>
        /// <param name="_settingChoice"></param>
        /// <returns></returns>
        public bool UpdateSettingValue(string _settingKey, string _settingChoice) {
            if (userSettings.ContainsKey(_settingKey)) {
                if (userSettingRules.TryGetValue(_settingKey, out object obj)) {
                    Type t = obj.GetType();
                    //ComboBox Setting
                    if (t == typeof(ComboSetting)) {
                        ComboSetting cbs = (ComboSetting)obj;
                        //Do we have multiple settings stored in a single setting line?
                        if (cbs.MultiMode) {
                            //Get the multiple settings
                            ComboSetting.Setting[] settings = cbs.GetMultiSettings(_settingChoice);
                            foreach (ComboSetting.Setting setting in settings) {
                                userSettings[setting.Name] = setting.Value;
                                Console.WriteLine(string.Concat(setting.Name, "=", setting.Value));
                            }
                        }
                        else userSettings[_settingKey] = cbs.GetSettingValue(_settingChoice);
                    }
                    //TrackBar Setting
                    else if (t == typeof(TrackBarSetting)) {
                        TrackBarSetting tbs = (TrackBarSetting)obj;
                        try {
                            userSettings[_settingKey] = tbs.SettingValue(Int32.Parse(_settingChoice));
                        }
                        catch (Exception ex) {
                            Console.WriteLine("UpdateSettingsValue: ", _settingKey, " failed to convert value ", _settingChoice, " to Int32/n", ex.Message);
                        }
                    }
                }
                else userSettings[_settingKey] = _settingChoice;

                Console.WriteLine(string.Concat(_settingKey, "=", userSettings[_settingKey]));

                return true;
            }
            return false;
        }

        public string GetGuiSetting(string _settingsKey) {
            if (userSettings.TryGetValue(_settingsKey, out string settingOption)) {
                if (userSettingRules.TryGetValue(_settingsKey, out object obj)) {
                    Type t = obj.GetType();
                    if (t == typeof(ComboSetting)) {
                        ComboSetting cbs = (ComboSetting)obj;
                        if (cbs.MultiMode) {
                            List<string> settings = new List<string>();
                            bool valid;

                            ComboSetting.Setting currSetting = new ComboSetting.Setting();
                            foreach (string combination in cbs.ItemsbyLineNameKeys) {
                                cbs.SplitSettings(combination, ref settings);
                                valid = true;
                                //Check if this combination of settings is used
                                foreach (string setting in settings) {
                                    currSetting.Name = setting.Substring(0, setting.IndexOf("="));
                                    currSetting.Value = setting.Remove(0, setting.IndexOf("=") + 1);
                                    if (userSettings[currSetting.Name] != currSetting.Value) {
                                        valid = false;
                                        break;
                                    }
                                }

                                //Get the name for this combination set
                                if (valid) return cbs.GetSettingName(combination);
                            }
                        }
                        else return cbs.GetSettingName(settingOption);
                    }
                }
                return settingOption;
            }
            return null;
        }

        public bool GetGuiSettingFlag(string _settingsKey) {
            if (GetGuiSetting(_settingsKey) == "1") return true;
            else return false;
        }

        public string[] GetPossibleGuiSettings(string _settingsKey) {
            if (userSettings.TryGetValue(_settingsKey, out string value)) {
                if (userSettingRules.TryGetValue(_settingsKey, out object obj)) {
                    Type t = obj.GetType();
                    if (t == typeof(ComboSetting))
                        return ((ComboSetting)obj).ItemsByGuiNameKeys;
                }
            }
            return null;
        }

        public string GetOriginalSetting(string _settingKey) {
            for(int i = 0; i < settings.GetLength(0); i++) {
                if (settings[i, 0] == _settingKey) return settings[i, 1];
            }

            return "";
        }
    }
}
