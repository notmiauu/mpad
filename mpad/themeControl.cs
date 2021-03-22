using Bunifu.UI.WinForms;
using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;
using static mpad.Settings;

namespace mpad
{
    public class themeControl
    {
        public static void setTheme(BunifuTextBox txtMain, Form frm)
        {
            switch (impConfig.theme)
            {
                case "Light":
                    txtMain.FillColor = Color.White;
                    txtMain.ForeColor = Color.Black;

                    foreach (Control c in frm.Controls)
                    {
                        switch (c)
                        {
                            case MenuStrip _:
                                c.ForeColor = Color.Black;
                                c.BackColor = Color.White;
                                break;
                            case BunifuTextBox _:
                                c.ForeColor = Color.Black;
                                c.BackColor = Color.White;
                                break;
                            case Label _:
                                c.ForeColor = Color.Black;
                                break;
                            case Form _:
                                c.BackColor = Color.White;
                                c.ForeColor = Color.Black;
                                break;
                        }
                    }

                    break;
                case "Dark":
                    txtMain.FillColor = Color.FromArgb(30, 30, 30);
                    txtMain.ForeColor = Color.White;

                    foreach (Control c in frm.Controls)
                    {
                        switch (c)
                        {
                            case MenuStrip _:
                                c.ForeColor = Color.White;
                                c.BackColor = Color.FromArgb(45, 45, 48);
                                break;
                            case BunifuTextBox _:
                                c.ForeColor = Color.White;
                                c.BackColor = Color.FromArgb(30, 30, 30);
                                break;
                            case Label _:
                                c.ForeColor = Color.White;
                                break;
                            case Form _:
                                c.ForeColor = Color.White;
                                c.BackColor = Color.DarkGray;
                                break;
                        }
                    }
                    break;
            }
        }

        public static void setTheme(Guna2Button btnConfirm, Form frm)
        {
            switch (impConfig.theme)
            {
                case "Light":
                    btnConfirm.FillColor = Color.FromArgb(30, 144, 255);

                    foreach (Control c in frm.Controls)
                    {
                        switch (c)
                        {
                            case Guna2Button _:
                                c.ForeColor = Color.Black;
                                c.BackColor = Color.FromArgb(30, 144, 255);
                                break;
                            case ComboBox _:
                                c.ForeColor = Color.Black;
                                c.BackColor = Color.White;
                                break;
                            case Form _:
                                c.ForeColor = Color.Black;
                                c.BackColor = Color.White;
                                break;
                        }
                    }
                    break;
                case "Dark":
                    btnConfirm.FillColor = Color.FromArgb(108, 99, 255);
                    frm.BackColor = Color.Black;

                    foreach (Control c in frm.Controls)
                    {
                        switch (c)
                        {
                            case Guna2Button _:
                                c.ForeColor = Color.White;
                                c.BackColor = Color.FromArgb(108, 99, 255);
                                break;
                            case ComboBox _:
                                c.ForeColor = Color.White;
                                c.BackColor = Color.FromArgb(45, 45, 48);
                                break;
                            case Form _:
                                c.ForeColor = Color.White;
                                c.BackColor = Color.FromArgb(30, 30, 30);
                                break;
                        }
                    }
                    break;
            }
        }

        //METHOD OVERLOADING WILL DO WHEN I FIGURE APPROPRIATE METHOD
    }
}