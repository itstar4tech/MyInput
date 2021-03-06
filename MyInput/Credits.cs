using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace MyInput
{
    public partial class Credits : Form
    {
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
            int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;

        public Credits()
        {
            InitializeComponent();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush lgb = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), BackColor, ForeColor, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(lgb, new Rectangle(0, 0, Width, Height));
        }

        private void Credits_Load(object sender, EventArgs e)
        {
            /*
            scrollingBox1.Items.Clear();
            scrollingBox1.Items.Add("MyInput Beta 2");
            scrollingBox1.Items.Add("\r\n");
            scrollingBox1.Items.Add("Produced by MyMyanmar");
            scrollingBox1.Items.Add("© Technomation Studios");
            scrollingBox1.Items.Add("All Rights Reserved.");
            scrollingBox1.Items.Add("\r\n");
            scrollingBox1.Items.Add("\r\n");
            scrollingBox1.Items.Add("Programmers");
            scrollingBox1.Items.Add("ထူး​မြင့်​နောင်​ - Htoo Myint Naung ");
            scrollingBox1.Items.Add("ထိန်​လင်း​ထူး​​ - Htein Lynn Htoo");
            scrollingBox1.Items.Add("\r\n\r\n");
            scrollingBox1.Items.Add("Designer\r\nကျော်​စွာ​လင်း​- Kyaw Swar Linn");
            scrollingBox1.Items.Add("\r\n\r\n");
            scrollingBox1.Items.Add("This program contains the results from following MyMyanmar Research Projects");
            scrollingBox1.Items.Add(@"Codename: YAN#
Codename: VOX#
Codename: butt
Codename: Yoga
MyBreak
MyMyanmar Input Frameworks
MyMyanmar Spelling Checking AI
Technomation Compiler Tools");
            scrollingBox1.Items.Add("\r\n\r\n");
            scrollingBox1.Items.Add("MyInput Keyboard Layout Language");
            scrollingBox1.Items.Add("MyInput Keyboard Scripting Language");
            scrollingBox1.Items.Add("MyInput");
            scrollingBox1.Items.Add("copyrighted MyMyanmar. All Right Reserved");
            scrollingBox1.Items.Add("\r\n");
            scrollingBox1.Items.Add("Special Thanks to");
            scrollingBox1.Items.Add("ကျေး​ဇူး​တင်​ပါ​သည်။​");
            scrollingBox1.Items.Add(@"ဦး​မြင့်​သူ​(ကွန်​ပျူ​တာ​ဂျာ​နယ်​)
ဦး​သိန်း​ထွဋ်​(Geocomp Computer)
မြန်​မာ​စာ​ဌာ​န​ (ရန်​ကုန်​ကွန်​ပျူ​တာ​တက္က​သိုလ်​)
Michael Everson (Evertype)
Martin Hosken (SIL International)
Marc Durdin (Keyman)
Emma Burrows
George Mamaladze");
            scrollingBox1.Items.Add("\r\n\r\n\r\n\r\n\r\n\r\n");
             * */

        }

        private void glassButton44_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void glassButton44_MouseUp(object sender, MouseEventArgs e)
        {
            Close();
        }
    }
}