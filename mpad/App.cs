using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using static mpad.mUtils;
using static mpad.Settings;

namespace mpad
{
    static class App
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!Directory.Exists(configPath) || !File.Exists(configPath + @"\mpad.json"))
            {
                Directory.CreateDirectory(configPath);

                mpadJSON defaultConfig = new mpadJSON
                { id = 1, theme = "Light", wrap = true, font = "Arial", saveTimer = 30000, fontSize = 12f, version = latestVer };

                SerializeConfig(defaultConfig);
            }

            DeserializeConfig();

            if (!File.Exists(fontSizePath)) Task.Run(createFontSizes);

            if (!File.Exists(extensionsPath)) Task.Run(createSupportedExtensions);

            currentVer = impConfig.version;
            if (currentVer < latestVer) UpdateConfig();

            mpadMain newMain = new mpadMain();
            newMain.Activate();

            updateExtensions(false);
            Application.Run(newMain);
        }
    }
}