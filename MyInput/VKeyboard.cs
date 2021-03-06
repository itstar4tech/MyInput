using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MyInput.Keyboard_Classes;
using MyInput.Utilities;
using System.Drawing.Drawing2D;

namespace MyInput
{
    public partial class VKeyboard : Form
    {
        public VKeyboard()
        {
            InitializeComponent();
        }
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
            int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;

        private void glassButton30_Click(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush lgb = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.Black, Color.FromArgb(45, 45, 45), LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(lgb, new Rectangle(0, 0, Width, Height));
            //base.OnPaint(e);
        }

        public void SetActiveScript(KeyProcessor kpr)
        {
            ScriptName = kpr.getscript();
            iop = mfm.iop;
        }

        private string ScriptName;
        public void SetActiveLayout(KeyboardLayout kly)
        {
            kl = kly;
            LayoutName = kl.getname();
            dkstate = "none";
            UpdateVisual();
        }

        internal void UpdateVisual()
        {
            string font = kl.getFont();
            double fonssize = Convert.ToDouble(kl.getFontSize());
            foreach (Object b in this.Controls)
            {
                if (typeof(Glass.GlassButton) == b.GetType())
                {
                    Glass.GlassButton g = (Glass.GlassButton)b;
                    g.Font = new Font(font, (float)fonssize);
                    if (g.VKCode != null)
                    {
                        Key k = new Key();
                        k.shift = shift;
                        k.alt = alt;
                        k.vkCode = Convert.ToInt32(g.VKCode, 16);
                        if (dkstate != "none")
                            k = kl.ProcessKey(k, dkstate);
                        else
                            k = kl.ProcessKey(k);
                        if (k.ch.StartsWith("[") && k.ch.EndsWith("]"))
                            g.Text = "\u25cc\t\t" + k.ch;
                        else
                            g.Text = k.ch;
                    }
                    else
                    {
                        if (shift)
                        {
                            if (g.Text == "SHIFT")
                                g.InnerBorderColor = Color.LightSkyBlue;
                        }
                        else
                        {
                            if (g.Text == "SHIFT")
                                g.InnerBorderColor = Color.FromArgb(64, 64, 64, 20);
                        }

                        if (alt)
                        {
                            if (g.Text == "ALT")
                                g.InnerBorderColor = Color.LightSkyBlue;
                        }
                        else
                        {
                            if (g.Text == "ALT")
                                g.InnerBorderColor = Color.FromArgb(64, 64, 64, 20);
                        }
                    }
                }
            }
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_MOUSEACTIVATE)
            {
                m.Result = (IntPtr)MA_NOACTIVATE;
            }
            base.WndProc(ref m);
        }

        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= (WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW);
                return cp;
            }
        }

        internal const uint WM_MOUSEACTIVATE = 0x21;
        internal const uint MA_ACTIVATE = 1;
        internal const uint MA_ACTIVATEANDEAT = 2;
        internal const uint MA_NOACTIVATE = 3;
        internal const uint MA_NOACTIVATEANDEAT = 4;


        private KeyboardLayout kl;
        private string LayoutName;
        private string Filter(string c)
        {
            string filter = "ါာိီုူဲဵံ့း်ၠၡၢၣၤၥၦၧၨၩၪၫၮၯၰၱၲၳၴၵၷၸၹၺၻၼၽၾၿႀႁႂႃႌႍႎ႐႑႒႔႕\u1075\u1076\u1077\u1078\u1079\u107a\u107b\u107c\u107d\u1089";
            foreach (char ch in filter.ToCharArray())
            {
                if (ch.ToString() == c)
                {
                    return "\u25cc" + c;
                }
            }
            if (c == "ႅ" || c == "ႄ" || c=="\u1080")
            {
                return c + "\u25cc";
            }
            return c;
        }
        private bool shift;
        private bool alt;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Activate();
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        IOProcessor iop;
        private void VKeyPress(object sender, MouseEventArgs e)
        {
            Glass.GlassButton btn = (Glass.GlassButton)sender;
            if (btn.Enabled == false) return;
            if (btn.Text == "SHIFT")
            {
                shift = !shift;
                UpdateVisual();
            }
            else if (btn.Text == "ALT")
            {
                alt = !alt;
                UpdateVisual();
            }
            else if (btn.Text == "bspace")
            {
                iop.Income("delete");
                if (shift || alt)
                {
                    shift = false;
                    alt = false;
                    UpdateVisual();
                }
            }
            else if (btn.Text.StartsWith("\u25cc\t\t"))
            {
                string dk = btn.Text.Substring(2).Trim();
                dk = dk.Replace("[", "").Replace("]", "");
                dkstate = dk;
                UpdateVisual();
            }
            else
            {
                string chr = btn.Text.Replace("◌", "").Trim();
                foreach (string s in iop.CompatibilityDecompose(chr))
                {
                    iop.Income(s);
                }
                if (shift || alt || (dkstate != "none"))
                {
                    shift = false;
                    alt = false;
                    dkstate = "none";
                    UpdateVisual();
                }
            }
            Log l = new Log();
            l.write("VKPRESS: " + btn.Text);
            eventInitiated = true;
            //MyInput.Keyboard_Classes.Buffer bf = new MyInput.Keyboard_Classes.Buffer();
            //glassButton40.Text = bf.getBuffer();
        }

        public bool eventInitiated;
        internal string dkstate;
        private void VKeyboard_Load(object sender, EventArgs e)
        {
            dkstate = "none";
            cfg = new Config("MyInput\\");
            int x = (Screen.GetWorkingArea(this).Width / 2) - (this.Width / 2);
            int y = (Screen.GetWorkingArea(this).Height - this.Height);
            string left = cfg.Read("vkx", x.ToString());
            string top = cfg.Read("vky", y.ToString());
            this.Top = Convert.ToInt32(top);
            this.Left = Convert.ToInt32(left);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            
        }

        public void ChangeState(bool shf, bool alt)
        {
            this.shift = shf;
            this.alt = alt;
            UpdateVisual();
        }
        private Config cfg;
        private void VKeyboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            cfg.Write("vkx", this.Left.ToString());
            cfg.Write("vky", this.Top.ToString());
        }

        private Main mfm;

        public void setHandle(Main mf)
        {
            mfm = mf;
            iop = mf.iop;
            iop.SetMainHandle(mfm);
        }

        private void glassButton44_Click(object sender, EventArgs e)
        {
            mfm.closeVK();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void glassButton44_Click(object sender, MouseEventArgs e)
        {
            mfm.closeVK();
        }
    }
}