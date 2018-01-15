using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AshesLauncher {
    class TrackBarSetting {
        private bool usesFloat;
        public bool UsesFloat { get { return usesFloat; } }

        private int displayMin, displayMax, displayTickFrequency;
        public int DisplayMin { get { return displayMin; } }
        public int DisplayMax {  get { return DisplayMax; } }
        public int DisplayTickFrequency {  get { return displayTickFrequency; } }

        private int offset;

        public TrackBarSetting(int _displayMin, int _displayMax, int _displayTickFrequency, int _offset, bool _usesFloat) {
            usesFloat = _usesFloat;
            displayMin = _displayMin;
            displayMax = _displayMax;
            displayTickFrequency = _displayTickFrequency;
            offset = _offset;
        }

        public string SettingValue(int _displayValue) {
            int value = (_displayValue / displayTickFrequency) - offset;
            if (usesFloat) {
                float f = ((value / 10f) + (offset / 10f));
                string v = f.ToString();
                if (!v.Contains(".")) v = string.Concat(v, ".0");
                return v;
            }
            return value.ToString();
        }

        public void SetTrackBar(ref TrackBar _trackBar) {
            _trackBar.Minimum = displayMin;
            _trackBar.Maximum = displayMax;
            _trackBar.TickFrequency = _trackBar.SmallChange = _trackBar.LargeChange = displayTickFrequency;
        }
    }
}
