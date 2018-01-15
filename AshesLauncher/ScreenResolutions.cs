using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AshesLauncher {
    class ScreenResolutions {
        private static readonly ScreenResolutions instance = new ScreenResolutions();
        static ScreenResolutions() { }
        private ScreenResolutions() {
            DEVMODE vDevMode = new DEVMODE();
            int i = 0;
            string rez;
            while (EnumDisplaySettings(null, i, ref vDevMode)) {
                rez = string.Concat(vDevMode.dmPelsWidth, ",", vDevMode.dmPelsHeight);
                if (!screenResolutions.Contains(rez)) screenResolutions.Add(rez);
                /*
                Console.WriteLine("Width:{0} Height:{1} Color:{2} Frequency:{3}",
                                        vDevMode.dmPelsWidth,
                                        vDevMode.dmPelsHeight,
                                        1 << vDevMode.dmBitsPerPel, vDevMode.dmDisplayFrequency
                                    );
    */            
                i++;
            }

        }
        public static ScreenResolutions Instance {
            get { return instance; }
        }

        private List<string> screenResolutions = new List<string>();
        public List<string> Resolutions {  get { return screenResolutions; } }

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(
              string deviceName, int modeNum, ref DEVMODE devMode);
        const int ENUM_CURRENT_SETTINGS = -1;

        const int ENUM_REGISTRY_SETTINGS = -2;

        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE {

            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;

        }
    }
}

