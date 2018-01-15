using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AshesLauncher {
    class ComboSetting {
        public class Setting {
            public string Name;
            public string Value;
            public Setting() { }
            public Setting(string _name, string _value) { Name = _name; Value = _value; }
        }

        /// <summary>
        /// Items as named in the GUI controls
        /// </summary>
        private readonly Dictionary<string, string> ItemsByGuiName = new Dictionary<string, string>();

        /// <summary>
        /// GUI item names
        /// </summary>
        public string[] ItemsByGuiNameKeys => ItemsByGuiName.Keys.ToArray();

        /// <summary>
        /// Items as named in the settings file
        /// </summary>
        private readonly Dictionary<string, string> ItemsByLineName = new Dictionary<string, string>();
        public string[] ItemsbyLineNameKeys => ItemsByLineName.Keys.ToArray();

        private string separatorValue;
        public bool MultiMode {  get { return separatorValue.Length > 0; } }

        public ComboSetting(string[,] _options, string _separatorValue) {
            separatorValue = _separatorValue;
            for (int i = 0; i <= _options.GetLength(0) - 1; i++) {
                ItemsByGuiName.Add(_options[i, 0], _options[i, 1]);
                ItemsByLineName.Add(_options[i, 1], _options[i, 0]);
            }
        }

        public string GetSettingName(string _name) {
            if (ItemsByLineName.TryGetValue(_name, out string value)) {
                return value;
            }
            return null;
        }

        public string GetSettingValue(string _name) {
            if (ItemsByGuiName.TryGetValue(_name, out string value)) {
                return value;
            }
            return null;
        }

        public Setting[] GetMultiSettings(string _name) {
            string source = GetSettingValue(_name);
            List<string> settingLines = new List<string>();
            SplitSettings(source, ref settingLines);

            //Seperator each setting into it's name and value pair
            List<Setting> settings = new List<Setting>();
            foreach (string s in settingLines) {
                settings.Add(new Setting(s.Substring(0, s.IndexOf("=")), s.Remove(0, s.IndexOf("=") + 1)));
            }

            return settings.ToArray();
        }

        public void SplitSettings(string _combination, ref List<string> settings) {
            settings.Clear();
            string temp;
            if (separatorValue.Length > 0) {
                while (_combination.Contains(separatorValue)) {
                    temp = _combination.Substring(0, _combination.IndexOf(separatorValue)); //Extract the first embedded setting
                    settings.Add(temp);                                         //Store it for later
                    _combination = _combination.Remove(0, temp.Length + 1);     //Remove it from the source
                }
                settings.Add(_combination);
            }
        }
    }
}
