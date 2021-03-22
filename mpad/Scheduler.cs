using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using static mpad.mpadMain;

/*
Prop:
( 5. textbox (string), 6. text to find)

7. found instance? - ??????? not needed I think
 */

namespace mpad
{
    class Data
    {
        public static bool opened = false;
        public static bool saved = true;
        public static bool autoSaveCheck = false;

        public static string path = "";
        public static string content = "";
        public static string filename = "Untitled";

        public static List<string> exts = new List<string>();
    }

    internal class Scheduler
    {
        internal static void Open()
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Title = "Open Text File",
                Filter = "Text files|*.txt|All Files (*.)|*.*",
                RestoreDirectory = true
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;
            try
            {
                Data.path = Path.GetFullPath(ofd.FileName);
                Data.filename = ofd.SafeFileName;

                if (!File.Exists(Data.path)) return;
                using (FileStream fs = new FileStream(Data.path, FileMode.Open, FileAccess.ReadWrite,
                    FileShare.ReadWrite))
                using (StreamReader sr = new StreamReader(fs))
                {
                    var content = sr.ReadToEnd();
                    Data.content = content;
                }

                Data.saved = true;
                Data.opened = true;
            }
            catch (Exception)
            {
                Data.opened = false;
            }
        }

        internal static void Save(string text)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                FileName = "Untitled.txt",
                Filter = "Text File (*.txt)|*.txt|All Files (*.)|*.*",
                DefaultExt = ".txt",
                Title = "Save Text File",
                RestoreDirectory = true
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;
            {
                try
                {
                    Data.path = Path.GetFullPath(sfd.FileName);
                    Data.filename = sfd.FileName.Substring(sfd.FileName.LastIndexOf('\\') + 1);

                    using (StreamWriter sw = new StreamWriter(Data.path, false))
                    {
                        sw.Write(text);
                        Data.saved = true;
                        Data.opened = true;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("File could not be saved, error has been generated.", "This is a caption.",
                        MessageBoxButtons.OK);
                    Data.saved = false;
                }
            }
        }

        internal static void Opened()
        {
            using (FileStream fs = new FileStream(Data.path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (StreamReader sr = new StreamReader(fs))
                Data.content = sr.ReadToEnd();
        }

        internal static void PrintSetup()
        {
            PageSetupDialog psdg = new PageSetupDialog
            {
                AllowPaper = true,
                AllowOrientation = true,
                AllowMargins = true,
                AllowPrinter = true,
                ShowHelp = true,
                ShowNetwork = true,
            };
        }

        internal static void PrintText(PrintDocument pd)
        {
            PrintDialog pdg = new PrintDialog
            {
                AllowPrintToFile = true, AllowSomePages = true, AllowCurrentPage = true, ShowNetwork = true
            };

            pd.PrintPage += document_PrintPage;

            if (pdg.ShowDialog() == DialogResult.OK) pd.Print();
        }
    }
}