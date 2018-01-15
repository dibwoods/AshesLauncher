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
    public partial class GameOptions : Form {
        private static GameOptions instance = new GameOptions();
        static GameOptions() { }
        public static GameOptions Instance {
            get {
                if (instance == null) instance = new GameOptions();
                return instance;
            }
        }

        public class Options {
            public class Game {
                public const string BIND_CURSOR = "BindCursor";
                public const string CURSOR_SCALE = "CursorScale";
                public const string PLAYER_ICON = "SteamAvatars";
                public const string CAMERA_PAN_MOUSE = "CameraPanAlt";
                public const string CAMERA_PAN_KEYS = "CameraPanKeys";
                public const string UI_SCALE = "UIScale";
                public const string CAMERA_PAN_SPEED = "CameraPanSpeed";
                public const string UPLOAD_REPLAY = "UploadReplay";
                public const string SKIP_INTRO_MOVIE = "SkipMovie";
                public const string HEALTH_BARS = "HealthBarsAlways";
                public const string ENGI_FORCE_STOP = "ForceStop";
                public const string QUICK_ARMY_FORMING = "QuickArmyAttach";
                public const string AUTO_LVL_DREADS = "AutoLevelT3";
                public const string AUTO_SAVE = "AutoSave";
                public const string DISABLE_WORLD_ICONS = "DisableIntermediateMode";
            }

            public class Video {
                public const string RESOLUTION = "Resolution";
                public const string SCREEN_MODE = "FullScreen";                     //Also linked to "EmulateFullscreen"
                public const string GRAPHICS_INTERFACE = "Api";                     //dx12, vulkan, dx11
                public const string MSAA = "MSAASamples";                           //1 (= Off), 2, 4, 8
                public const string POINT_LIGHT_QUALITY = "PointLights";            //Off, High
                public const string GLARE_QUALITY = "Glare";                        //Off, Low, Mid, High
                public const string TERRAIN_OBJECT_QUALITY = "TerrainDetailObjLevel";   //Off, Low, Mid (= High), High (= Ultra)
                public const string SHADING_SAMPLES = "ShadingSamples";                 //4 (= Low), 8 (= High), 16 (= Ultra)
                public const string TERRAIN_SHADING_SAMPLES = "TerrainShadingSamples";  //4 (= Low), 8 (= High), 12 (= Ultra)
                public const string SHADOW_QUALITY = "ShadowQuality";                   //Off, Low, Mid (= High), High (= Ultra)
                public const string TEXTURE_QUALITY = "MipsToRemove";                   //2 (= Low), 1 (= Mid), 0 (= High)
                public const string ENVIRONMENTAL_FX = "EnvFX";                         //1, 0
                public const string COSMETIC_CLOUDS = "Clouds";                         //1, 0
                public const string VSYNC = "VSync";
                public const string MULTIPLE_GPUS = "AFRGPU";
                public const string ALLOW_OVERLAYS = "AllowHooks";
                //EnvMap ??? 
                //Noise ???
            }

            public class Audio {
                public const string MUTE_ALL = "MuteAll";
                public const string MASTER_VOL = "Volume_Master";
                public const string MUSIC_VOL = "Volume_Music";
                public const string ENV_VOL = "Volume_Environmental";   //Not changeable in-game option
                public const string EFFECT_VOL = "Volume_Effect";
                public const string UI_VOL = "Volume_UI";
                public const string VOICE_VOL = "Volume_Voice";
            }

            public class Benchmark {
                public const string AUTO_RUN = "AutoBenchRun[Off,GPUFocused,CPUFocused]";
            }
        }

        public static readonly string MY_DOCS = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static readonly string MY_GAMES_LOCATION = Path.Combine(MY_DOCS, @"My Games\Ashes of the Singularity - Escalation");
        public static readonly string GAME_SETTINGS_LOCATION = Path.Combine(MY_GAMES_LOCATION, "settings.ini");
        public static readonly string STEAM_RUN_GAME_URL = @"steam://rungameid/507490";
        public static readonly string BENCHMARK_FILE_SEARCH_PATTERN = "Output_*";

        Dictionary<string, Dictionary<string, string>> graphicsProfiles;// = new Dictionary<string, Dictionary<string, string>>();

        private GameOptions() {
            InitializeComponent();

            TopLevel = false;
            FormBorderStyle = FormBorderStyle.None;
            Visible = true;
            Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
        }

        public void Init() { 
            OptionsManager.Instance.Init(GAME_SETTINGS_LOCATION);
            if (OptionsManager.Instance.HasSettings) SetupGameOptions();
            else btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        public void GetOpenNextTabOnSave(ref CheckBox _chb) {
            _chb = chbOpenNextTab;
        }

        public void ResetCancelButton() {
            //A hack fix because I should really hide OptionsManage behind GameOptions.
            btnCancel.Enabled = false;
        }

        public bool UnSavedChanges() {
            //If the cancel button is enabled, we have unsaved changes
            return btnCancel.Enabled;
        }

        void SetupGameOptions() {
            #region Options
            //Bind Cursor
            OptionsManager.Instance.SetupComboBox(Options.Game.BIND_CURSOR, 
                ref cbBindCursor, 
                new string[,] {
                    { "Bind Always", "Always" },
                    { "Bind Never", "Never" },
                    { "Bind Smart", "Smart" },
                    {  "Bind in FullScreen", "FullScreen" }
                }
            );

            //In game player icon
            OptionsManager.Instance.SetupComboBox(Options.Game.PLAYER_ICON, 
                ref cbPlayerIcon,
                new string[,] {
                    { "Ashes Icon", "0" },
                    { "Steam Avatar", "1"}
                }
            );

            //Camera pan mouse controls
            OptionsManager.Instance.SetupComboBox(Options.Game.CAMERA_PAN_MOUSE, 
                ref cbCameraMouseControls,
                new string[,] {
                    { "RMB - Pan, Key M - Rotate", "0" },
                    { "MMB - Pan, Key M - Rotate", "1" },
                    { "RMB - Pan, Key MMB - Rotate", "2" }
                }
            );

            //Camera pan key controls
            OptionsManager.Instance.SetupComboBox(Options.Game.CAMERA_PAN_KEYS, 
                ref cbCameraKeyControls,
                new string[,] {
                    { "Arrow Keys", "Arrows" },
                    { "WASD", "WASD" }
                }
            );

            //Cursor scale (Values 0, 1, 2)
            OptionsManager.Instance.SetupTrackBar(Options.Game.CURSOR_SCALE, 
                ref tbCursorScale, ref lblCursorScale,
                new TrackBarSetting(100, 200, 50, 2, false)
            );

            //UI Scale (Value 0.5 - 1.0 in 0.1 increments)
            OptionsManager.Instance.SetupTrackBar(Options.Game.UI_SCALE, 
                ref tbUiScale, ref lblUiScale,
                new TrackBarSetting(50, 100, 10, 1, true)
            );

            //Camera Pan Speed (Values = 0.5 - 3.0 in 0.1 increments)
            OptionsManager.Instance.SetupTrackBar(Options.Game.CAMERA_PAN_SPEED, 
                ref tbCameraPanSpeed, ref lblCameraPanSpeed,
                new TrackBarSetting(50, 300, 10, 3, true)
            );

            OptionsManager.Instance.SetupCheckBox(Options.Game.UPLOAD_REPLAY, ref ckbUploadRankedReplays);
            OptionsManager.Instance.SetupCheckBox(Options.Game.SKIP_INTRO_MOVIE, ref ckbSkipIntroMovie);
            OptionsManager.Instance.SetupCheckBox(Options.Game.HEALTH_BARS, ref ckbHealthBars);
            OptionsManager.Instance.SetupCheckBox(Options.Game.ENGI_FORCE_STOP, ref ckbEngineerRequiresStop);
            OptionsManager.Instance.SetupCheckBox(Options.Game.QUICK_ARMY_FORMING, ref ckbQuickArmyForming);
            OptionsManager.Instance.SetupCheckBox(Options.Game.AUTO_LVL_DREADS, ref ckbAutoLevelDreads);
            OptionsManager.Instance.SetupCheckBox(Options.Game.AUTO_SAVE, ref ckbAutoSave);
            OptionsManager.Instance.SetupCheckBox(Options.Game.DISABLE_WORLD_ICONS, ref ckbDisableWorldIcons);
            #endregion

            #region VideoOptions
            cbQualityProfile.Items.Clear();

            //Graphics Interface
            OptionsManager.Instance.SetupComboBox(Options.Video.GRAPHICS_INTERFACE,
                ref cbGraphicsInterface,
                new string[,] {
                    { "DirectX 11", "dx11" },
                    { "DirectX 12", "dx12" },
                    { "Vulkan", "vulkan" }
                }
            );

            //MSAA
            OptionsManager.Instance.SetupComboBox(Options.Video.MSAA,
                ref cbMsaa,
                new string[,] {
                    { "Off", "1"},
                    { "2x", "2" },
                    { "4x", "4" },
                    { "8x", "8" }
                }
            );

            //Point Light Quality
            OptionsManager.Instance.SetupComboBox(Options.Video.POINT_LIGHT_QUALITY,
                ref cbPointLightQuality,
                new string[,] {
                    { "Off", "Off" },
                    { "High", "High" }
                }
            );

            //Glare Quality
            OptionsManager.Instance.SetupComboBox(Options.Video.GLARE_QUALITY,
                ref cbGlareQuality,
                new string[,] {
                    { "Off", "Off" },
                    { "Low", "Low" },
                    { "Mid", "Mid" },
                    { "High", "High" }
                }
            );

            //Terrain Object Quality
            //Off, Low, Mid (= High), High (= Ultra)
            OptionsManager.Instance.SetupComboBox(Options.Video.TERRAIN_OBJECT_QUALITY,
                ref cbTerrainQuality,
                new string[,] {
                    { "Off", "Off" },
                    { "Low", "Low" },
                    { "High", "Mid" },
                    { "Ultra", "High" }
                }
            );

            //Shading Samples
            //4 (= Low), 8 (= High), 16 (= Ultra)
            OptionsManager.Instance.SetupComboBox(Options.Video.SHADING_SAMPLES,
                ref cbShadingSamples,
                new string[,] {
                    { "Low", "4" },
                    { "High", "8" },
                    { "Ultra", "16" }
                }
            );

            //Terrain Shading Samples
            ////4 (= Low), 8 (= High), 12 (= Ultra)
            OptionsManager.Instance.SetupComboBox(Options.Video.TERRAIN_SHADING_SAMPLES,
                ref cbTerrainSamples,
                new string[,] {
                    { "Low", "4" },
                    { "High", "8" },
                    { "Ultra", "12" }
                }
            );

            //Shadow Quality
            //Off, Low, Mid (= High), High (= Ultra)
            OptionsManager.Instance.SetupComboBox(Options.Video.SHADOW_QUALITY,
                ref cbShadowQuality,
                new string[,] {
                    { "Off", "Off" },
                    { "Low", "Low" },
                    { "High", "Mid" },
                    { "Ultra", "High" }
                }
            );

            //Texture Quality
            //2 (= Low), 1 (= Mid), 0 (= High)
            OptionsManager.Instance.SetupComboBox(Options.Video.TEXTURE_QUALITY,
                ref cbTextureQuality,
                new string[,] {
                    { "Low", "2" },
                    { "Mid", "1" },
                    { "High", "0" }
                }
            );

            OptionsManager.Instance.SetupCheckBox(Options.Video.VSYNC, ref ckbVSync);
            OptionsManager.Instance.SetupCheckBox(Options.Video.MULTIPLE_GPUS, ref ckbMultiGpu);
            OptionsManager.Instance.SetupCheckBox(Options.Video.ALLOW_OVERLAYS, ref ckbOverlays);
            OptionsManager.Instance.SetupCheckBox(Options.Video.ENVIRONMENTAL_FX, ref ckbEnvFx);
            OptionsManager.Instance.SetupCheckBox(Options.Video.COSMETIC_CLOUDS, ref ckbClouds);

            graphicsProfiles = new Dictionary<string, Dictionary<string, string>>();
            graphicsProfiles.Add("Low", DefineGraphicsProfile("Off", "Off", "Off", "Off", "Low", "Low", "Low", "Low"));
            graphicsProfiles.Add("Standard", DefineGraphicsProfile("Off", "High", "Low", "Low", "Low", "High", "High", "Mid"));
            graphicsProfiles.Add("High", DefineGraphicsProfile("Off", "High", "Low", "High", "High", "High", "High", "High"));
            graphicsProfiles.Add("Extreme", DefineGraphicsProfile("2x", "High", "High", "Ultra", "Ultra", "High", "Ultra", "High"));
            graphicsProfiles.Add("Crazy", DefineGraphicsProfile("4x", "High", "High", "Ultra", "Ultra", "Ultra", "Ultra", "High"));

            //Set the graphics profiles for selection in the Quality Profile combo box
            foreach (KeyValuePair<string, Dictionary<string, string>> profile in graphicsProfiles) {
                cbQualityProfile.Items.Add(profile.Key);
            }

            //Resolution
            int maxSize = ScreenResolutions.Instance.Resolutions.Count;
            string rez;
            List<string> validResolutions = new List<string>();
            int i;

            //Restrict resolution sizes
            for (i = 0; i < maxSize; i++) {
                rez = ScreenResolutions.Instance.Resolutions[i];
                if (Int32.Parse(rez.Substring(0, rez.IndexOf(','))) < 1280) continue;
                validResolutions.Add(rez);
            }

            //Format the 'allowed' resolutions
            maxSize = validResolutions.Count;
            string[,] formattedResolutions = new string[maxSize, 2];
            for (i = 0; i < maxSize; i++) {
                formattedResolutions[i, 0] = formattedResolutions[i, 1] = validResolutions[i];
                formattedResolutions[i, 0] = formattedResolutions[i, 0].Replace(",", " x ");
            }

            OptionsManager.Instance.SetupComboBox(Options.Video.RESOLUTION,
                ref cbResolution, formattedResolutions);

            //Mode
            //Mode = FullScreen: FullScreen=1, EmulateFullscreen=0
            //Mode = Borderless Window: FullScreen=0, EmulateFullscreen=1 
            //Mode = Windowed: FullScreen=0, EmulateFullscreen=0
            OptionsManager.Instance.SetupComboBox(Options.Video.SCREEN_MODE,
                ref cbMode, new string[,] {
                    { "Fullscreen", "FullScreen=1,EmulateFullscreen=0" },
                    { "Borderless Windows", "FullScreen=0,EmulateFullscreen=1" },
                    { "Windowed", "FullScreen=0,EmulateFullscreen=0" }
                }, 
                ","
            );
            #endregion
            
            #region AudioOptons 
            //Mute Audio
            OptionsManager.Instance.SetupCheckBox(Options.Audio.MUTE_ALL, ref ckbMuteAll);

            //Master Volume
            OptionsManager.Instance.SetupTrackBar(Options.Audio.MASTER_VOL,
                ref tbMasterVolume, ref lblMasterVolume,
                new TrackBarSetting(0, 100, 1, 0, false)
            );

            //Music Volume
            OptionsManager.Instance.SetupTrackBar(Options.Audio.MUSIC_VOL,
                ref tbMusicVolume, ref lblMusicVolume,
                new TrackBarSetting(0, 100, 1, 0, false)
            );

            //Effect Volume
            OptionsManager.Instance.SetupTrackBar(Options.Audio.EFFECT_VOL,
                ref tbEffectVolume, ref lblEffectVolume,
                new TrackBarSetting(0, 100, 1, 0, false)
            );

            //UI Volume
            OptionsManager.Instance.SetupTrackBar(Options.Audio.UI_VOL,
                ref tbUiVolume, ref lblUiVolume,
                new TrackBarSetting(0, 100, 1, 0, false)
            );

            //Voice Volume
            OptionsManager.Instance.SetupTrackBar(Options.Audio.VOICE_VOL,
                ref tbVoiceVolume, ref lblVoiceVolume,
                new TrackBarSetting(0, 100, 1, 0, false)
            );

            //Environmental Volume
            OptionsManager.Instance.SetupTrackBar(Options.Audio.ENV_VOL,
                ref tbEnvVolume, ref lblEnvVolume,
                new TrackBarSetting(0, 100, 1, 0, false)
            );
            #endregion

            #region Benchmark
            OptionsManager.Instance.SetupComboBox(Options.Benchmark.AUTO_RUN,
                ref cbBenchmark, new string[,] {
                    { "Off", "Off" },
                    { "GPU Focussed", "GPUFocused" },
                    { "CPU Focussed", "CPUFocused" }
                }
            );
            #endregion
        }

        private void TrackBar_Scroll(object sender, EventArgs e) {
            TrackBar tb = (TrackBar)sender;

            //Calculate the closest value we should set the scroll bar to
            int value = OptionsManager.Instance.CalculateTrackBarValue(tb.Value, tb.Minimum, tb.Maximum, tb.LargeChange);
            tb.Value = value;
            
            //Update the % value on the label text
            Label lbl = (Label)tb.Tag; 
            int breakPos = lbl.Text.IndexOf(":");
            string text = lbl.Text;
            text = text.Remove(breakPos + 1, text.Length - breakPos - 1);
            lbl.Text = string.Concat(text, " ", value, "%");
        }

        private void UpdateGameSetting(object sender, EventArgs e) {
            btnCancel.Enabled = true;

            Type t = sender.GetType();
            if (t == typeof(ComboBox)) {
                ComboBox cb = (ComboBox)sender;
                //Using tag as the setting key i.e. GameOptions.UI_SCALE
                string key = cb.Tag.ToString();
                OptionsManager.Instance.UpdateSettingValue(key, cb.SelectedItem.ToString());

                //Set the Graphics Profile to a matching profile otherwise say it's Custom
                if (IsLinkedToGraphicsProfile(key)) {
                    //Find out what profile we may match from changing a graphics option
                    string profileName = GraphicsProfileSelected();
                    //Add 'Custom' if an existing profile is NOT used
                    if (profileName == "Custom" && !cbQualityProfile.Items.Contains("Custom")) cbQualityProfile.Items.Add("Custom");
                    //Remove 'Custom' if am existing profile is used
                    else if (profileName != "Custom" && cbQualityProfile.Items.Contains("Custom")) cbQualityProfile.Items.Remove("Custom");
                    
                    //Select the matching profile
                    cbQualityProfile.SelectedItem = profileName;
                }

                if (key == Options.Video.SCREEN_MODE) {
                    if (cbMode.SelectedItem.ToString() == "Borderless Windows") {
                        cbResolution.Enabled = false;
                    }
                    else cbResolution.Enabled = true;
                }
            }
            else if (t == typeof(TrackBar)) {
                TrackBar tb = (TrackBar)sender;
                //Hack to get the setting key (stored in the label)
                Label lbl = (Label)tb.Tag;
                OptionsManager.Instance.UpdateSettingValue(lbl.Tag.ToString(), tb.Value.ToString());
            }
            else if (t == typeof(CheckBox)) {
                CheckBox chk = (CheckBox)sender;
                OptionsManager.Instance.UpdateSettingValue(chk.Tag.ToString(), chk.Checked ? "1" : "0");
            }
        }

        private void btnAccept_Click(object sender, EventArgs e) {
            //Only save if changed something
            if (btnCancel.Enabled) OptionsManager.Instance.SaveSettingsFile();
            if (chbOpenNextTab.Checked) AdvanceTab();
            btnCancel.Enabled = false;
        }

        private void AdvanceTab() {
            if (tcOptions.SelectedIndex < tcOptions.TabPages.Count - 1) tcOptions.SelectedIndex += 1;
            else tcOptions.SelectedIndex = 0;
        }

        private Dictionary<string, string> DefineGraphicsProfile(string _msaa, 
            string _pointLightQuality, string _glareQuality, string _terrainQuality,
            string _shadingSamples, string _terrainSamples, string _shadowQuality, string _textureQualtiy) {

            Dictionary<string, string> graphicsProfile = new Dictionary<string, string>();

            if (CheckValidOption(cbMsaa, _msaa))
                graphicsProfile.Add(Options.Video.MSAA, _msaa);

            if (CheckValidOption(cbPointLightQuality, _pointLightQuality))
                graphicsProfile.Add(Options.Video.POINT_LIGHT_QUALITY, _pointLightQuality);

            if (CheckValidOption(cbGlareQuality, _glareQuality))
                graphicsProfile.Add(Options.Video.GLARE_QUALITY, _glareQuality);

            if (CheckValidOption(cbTerrainQuality, _terrainQuality))
                graphicsProfile.Add(Options.Video.TERRAIN_OBJECT_QUALITY, _terrainQuality);

            if (CheckValidOption(cbShadingSamples, _shadingSamples))
                graphicsProfile.Add(Options.Video.SHADING_SAMPLES, _shadingSamples);

            if (CheckValidOption(cbTerrainSamples, _terrainSamples))
                graphicsProfile.Add(Options.Video.TERRAIN_SHADING_SAMPLES, _terrainSamples);

            if (CheckValidOption(cbShadowQuality, _shadowQuality))
                graphicsProfile.Add(Options.Video.SHADOW_QUALITY, _shadowQuality);

            if (CheckValidOption(cbTextureQuality, _textureQualtiy))
                graphicsProfile.Add(Options.Video.TEXTURE_QUALITY, _textureQualtiy);

            return graphicsProfile;
        }

        /// <summary>
        /// Check the option exists in the combo box
        /// </summary>
        /// <param name="_comboBox">Combo box to check</param>
        /// <param name="_item">Item to look for</param>
        /// <returns></returns>
        private bool CheckValidOption(ComboBox _comboBox, string _item) {
            if (_comboBox.Items.Contains(_item)) return true;

            Console.WriteLine(string.Concat("CheckValidOption: ", _comboBox.Name, " does not contain ", _item));
            return false;
        }

        /// <summary>
        /// Quality Profile combo box index has been changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbQualityProfile_SelectedIndexChanged(object sender, EventArgs e) {
            if (graphicsProfiles == null) return;
            if (graphicsProfiles.TryGetValue(cbQualityProfile.SelectedItem.ToString(), out Dictionary<string, string> profile)) {
                foreach(KeyValuePair<string,string> setting in profile) {
                    switch(setting.Key) {
                        case Options.Video.MSAA:
                            cbMsaa.SelectedItem = setting.Value; break;
                        case Options.Video.POINT_LIGHT_QUALITY:
                            cbPointLightQuality.SelectedItem = setting.Value; break;
                        case Options.Video.GLARE_QUALITY:
                            cbGlareQuality.SelectedItem = setting.Value; break;
                        case Options.Video.TERRAIN_OBJECT_QUALITY:
                            cbTerrainQuality.SelectedItem = setting.Value; break;
                        case Options.Video.SHADING_SAMPLES:
                            cbShadingSamples.SelectedItem = setting.Value; break;
                        case Options.Video.TERRAIN_SHADING_SAMPLES:
                            cbTerrainSamples.SelectedItem = setting.Value; break;
                        case Options.Video.SHADOW_QUALITY:
                            cbShadowQuality.SelectedItem = setting.Value; break;
                        case Options.Video.TEXTURE_QUALITY:
                            cbTextureQuality.SelectedItem = setting.Value; break;
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if the option is part of the Graphics Profiles
        /// </summary>
        /// <param name="settingKey"></param>
        /// <returns></returns>
        private bool IsLinkedToGraphicsProfile(string _settingKey) {
            switch (_settingKey) {
                case Options.Video.MSAA:
                case Options.Video.POINT_LIGHT_QUALITY:
                case Options.Video.GLARE_QUALITY:
                case Options.Video.TERRAIN_OBJECT_QUALITY:
                case Options.Video.SHADING_SAMPLES:
                case Options.Video.TERRAIN_SHADING_SAMPLES:
                case Options.Video.SHADOW_QUALITY:
                case Options.Video.TEXTURE_QUALITY:
                    return true;
                default:
                    return false;
            }
        }

        private string GraphicsProfileSelected() {
            if (graphicsProfiles != null) {
                foreach (KeyValuePair<string, Dictionary<string, string>> profile in graphicsProfiles) {
                    bool chosenProfile = true;
                    foreach (KeyValuePair<string, string> setting in profile.Value) {
                        switch (setting.Key) {
                            case Options.Video.MSAA:
                                if (cbMsaa.SelectedItem.ToString() != setting.Value) chosenProfile = false;
                                break;
                            case Options.Video.POINT_LIGHT_QUALITY:
                                if (cbPointLightQuality.SelectedItem.ToString() != setting.Value) chosenProfile = false;
                                break;
                            case Options.Video.GLARE_QUALITY:
                                if (cbGlareQuality.SelectedItem.ToString() != setting.Value) chosenProfile = false;
                                break;
                            case Options.Video.TERRAIN_OBJECT_QUALITY:
                                if (cbTerrainQuality.SelectedItem.ToString() != setting.Value) chosenProfile = false;
                                break;
                            case Options.Video.SHADING_SAMPLES:
                                if (cbShadingSamples.SelectedItem.ToString() != setting.Value) chosenProfile = false;
                                break;
                            case Options.Video.TERRAIN_SHADING_SAMPLES:
                                if (cbTerrainSamples.SelectedItem.ToString() != setting.Value) chosenProfile = false;
                                break;
                            case Options.Video.SHADOW_QUALITY:
                                if (cbShadowQuality.SelectedItem.ToString() != setting.Value) chosenProfile = false;
                                break;
                            case Options.Video.TEXTURE_QUALITY:
                                if (cbTextureQuality.SelectedItem.ToString() != setting.Value) chosenProfile = false;
                                break;
                            default: chosenProfile = false; break;
                        }
                    }
                    if (chosenProfile) return profile.Key;  //Return this profile name
                }
            }
            return "Custom";                            //Custom profile selected                
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            //Reload the options!
            //OptionsManager.Instance.Init(GAME_SETTINGS_LOCATION);
            Init();
        }
    }
}
