using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FontAwesome.Sharp; // allows us to use native libraries of the os, user32lib

// tutorial: https://youtu.be/5AsJJl7Bhvc?t=981
// free icons: https://fontawesome.com/v5.15/icons?d=gallery&p=2&m=free
// logo maker: https://spark.adobe.com/express-apps/logo-maker/preview


// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace MyAnimeListDesktopApplication2
{
    public partial class FormAnimeListMenu : Form
    {
        // fields
        private IconButton currentBtn;
        private Form currentChildForm;
        private readonly Panel leftBorderBtn;

        // constructor
        public FormAnimeListMenu()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60); // 60 is the height of the button
            panelOptionsMenu.Controls.Add(leftBorderBtn);

            // removing title bar
            Text = string.Empty;
            ControlBox = false;
            DoubleBuffered = true;

            // when maximized, app wont take entire screen
            // this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        // methods

        // when a button is clicked on, will set it as the current button
        // and change the colour of the button, and related elements
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                // deactivating the previous button
                DisableButton();

                // activating the current button

                // changing aspects of the current button
                currentBtn = (IconButton) senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                // changing aspects of the left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                // the icon at the top used to show which page we are on
                iconCurrentChildFormIcon.IconChar = currentBtn.IconChar;
                iconCurrentChildFormIcon.IconColor = color;
            }
        }

        // when a differnet button is clicked on, will set all other buttons as deactived 
        // defaulting back to their original colours
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                // changing aspects of the current button back to their default values
                currentBtn.BackColor = Color.FromArgb(31, 30, 68); // original colour of the button
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        /// <summary>
        ///     Used to open a form/page related to a specific side menu button
        /// </summary>
        /// <param name="childForm"></param>
        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
                // closes all other forms when a new one is opened
                currentChildForm.Close();

            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            labelTitleChildForm.Text = childForm.Text;
        }

        private void btnAnimeList_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new FormAnimeList());
        }

        private void btnSeasonalAnime_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            OpenChildForm(new FormSeasonalAnime());
        }

        private void btnTopAnime_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            OpenChildForm(new FormTopAnime());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new FormSearch());
        }

        private void btnRecommendations_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);
            OpenChildForm(new FormRecommendations());
        }

        private void btnCalender_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new FormCalender());
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new FormProfile());
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            currentChildForm.Close();
            Reset();
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            // the icon at the top used to show which page we are on
            iconCurrentChildFormIcon.IconChar = IconChar.Home;
            iconCurrentChildFormIcon.IconColor = Color.MediumPurple;
            labelTitleChildForm.Text = "Home";
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lparam);

        /// <summary>
        ///     Used to drag the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                btnMaximize.IconChar = IconChar.WindowRestore;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                btnMaximize.IconChar = IconChar.WindowMaximize;
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // structs

        private struct RGBColors
        {
            public static readonly Color color1 = Color.FromArgb(172, 126, 241);
            public static readonly Color color2 = Color.FromArgb(249, 118, 176);
            public static readonly Color color3 = Color.FromArgb(253, 138, 114);
            public static readonly Color color4 = Color.FromArgb(95, 77, 221);
            public static readonly Color color5 = Color.FromArgb(249, 88, 155);
            public static readonly Color color6 = Color.FromArgb(24, 161, 251);
        }
    }
}