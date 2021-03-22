using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace mpad
{
    class Settings
    {
        internal class mpadJSON
        {
            public int id;
            public string theme;
            public bool wrap;
            public string font;
            public int saveTimer;
            public float fontSize;
            public float version;
        }

        internal static readonly string configPath =
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\mpad";

        internal static readonly string fontSizePath =
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\mpad\fontSizes.txt";

        internal static readonly string extensionsPath =
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
            @"\mpad\SupportedExtensions.txt";

        public static float currentVer = 0f;
        public const float latestVer = (float) 0.1;

        public static readonly mpadJSON impConfig = new mpadJSON();


        static void SetConfiguration(mpadJSON json)
        {
            impConfig.id = json.id;
            impConfig.theme = json.theme;
            impConfig.wrap = json.wrap;
            impConfig.font = json.font;
            impConfig.saveTimer = json.saveTimer;
            impConfig.fontSize = json.fontSize;
            impConfig.version = json.version;
        }

        internal static void DeserializeConfig()
        {
            string jsonObject = "";
            mpadJSON configJson = new mpadJSON
            {
                id = 1
            };

            using (FileStream fs = new FileStream(configPath + @"\mpad.json", FileMode.Open, FileAccess.Read,
                FileShare.ReadWrite))
            using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                jsonObject = sr.ReadToEnd();

            try
            {
                configJson = JsonConvert.DeserializeObject<mpadJSON>(jsonObject);
                SetConfiguration(configJson);
            }
            catch (Exception)
            {
                MessageBox.Show(MissingCheck(configJson).ToString());
                ResetJSON();
            }
        }


        internal static void SerializeConfig(mpadJSON currentConfig)
        {
            string exportString = JsonConvert.SerializeObject(currentConfig);

            using (FileStream fs = new FileStream(configPath + @"\mpad.json", FileMode.OpenOrCreate, FileAccess.Write,
                FileShare.ReadWrite))
            using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                sw.Write(exportString);
        }

        private static void ResetJSON()
        {
            mpadJSON defaultConfig = new mpadJSON
            {
                id = 1, theme = "Light", wrap = true, font = "Arial", saveTimer = 30000, fontSize = 12f,
                version = latestVer
            };

            string newJSON = JsonConvert.SerializeObject(defaultConfig);

            using (FileStream fs = new FileStream(configPath + @"\mpad.json", FileMode.OpenOrCreate, FileAccess.Write,
                FileShare.ReadWrite))
            using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                sw.Write(newJSON);
        }

        internal static void UpdateConfig()
        {
            if (impConfig.version == latestVer) return;
            createFontSizes();
            createSupportedExtensions();
            impConfig.version = latestVer;
        }

        private static bool MissingCheck(mpadJSON ins)
        {
            if (ReferenceEquals(null, ins))
                return true;

            var properties = ins.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (!prop.CanRead) continue;
                if (prop.PropertyType.IsValueType) continue;

                object value = prop.GetValue(prop.GetGetMethod().IsStatic ? null : ins);

                switch (value)
                {
                    case null:
                    case string str when str.Equals(""):
                        return false;
                }
            }

            return true;
        }

        internal static void createFontSizes()
        {
            using (FileStream fs = new FileStream(fontSizePath, FileMode.OpenOrCreate, FileAccess.Write,
                FileShare.Read))
            using (StreamWriter sw = new StreamWriter(fs))
                sw.Write(
                    "2, 3, 4, 6, 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 44, 48, 52, 56, 60, 66, 72, 80, 88, 96, 108, 120, 132, 144, 168");
        }

        internal static void createSupportedExtensions()
        {
            using (StreamWriter sw = new StreamWriter(extensionsPath, false))
                sw.Write(
                    ".txt,.json,.reg,.rtf,.csv,.lua,.cs,.cpp,.h,.zig,.js,.html,.cshtml,.css,.ts,.jsx,.tsx,.docx,.pdf");
        }

        internal static string getSupportedExtensions()
        {
            using (FileStream fs = new FileStream(extensionsPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (StreamReader sr = new StreamReader(fs))
                return sr.ReadToEnd();
        }
    }
}

/*
 *             string jsonObject = "";

            using (FileStream fs = new FileStream(configPath + @"\mpad.json", FileMode.Open, FileAccess.Read,
                FileShare.ReadWrite))
            using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                jsonObject = sr.ReadToEnd();

            try
            {
                mpadJSON outdatedJson = JsonConvert.DeserializeObject<mpadJSON>(jsonObject);

                if (outdatedJson.version == latestVer) return;

                MessageBox.Show("Please wait while mpad quickly updates...");

                //TRASHY VERSION OF AN UPDATER TILL IMPORTANT
                if (outdatedJson.font == string.Empty || outdatedJson.saveTimer < 30000 || outdatedJson.fontSize < 2.0f)
                    if (outdatedJson.version < latestVer)
                        ResetJSON();

                outdatedJson.version = latestVer;

            }
            catch (JsonException)
            {
                MessageBox.Show("Fail to update your JSON configuration. Please fix this Miau, dum dum.");
            }
*/