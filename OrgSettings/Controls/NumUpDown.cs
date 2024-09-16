using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Media;
using System.Drawing.Drawing2D;

namespace LinkeD365.OrgSettings
{
    public partial class NumUpDown : Control
    {
        private Button btnUp = new Button();
        private Button btnDown = new Button();
        public TextBox textbox = new TextBox();
        private double value;
        private Color btnColor;
        private bool dec;
        private Timer timer;

        public NumUpDown()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint, true);
            base.Width = 150;
            base.Height = 20;
            this.btnColor = Color.Black;
            this.value = 0;
            this.textbox.Text = this.value.ToString();
            this.dec = false;
            this.btnDown.Width = this.btnUp.Width = 40;
            this.btnDown.Parent = this.btnUp.Parent = this;
            this.btnDown.Paint += new PaintEventHandler(this.btnDown_Paint);
            this.btnDown.Click += new EventHandler(this.btnDown_Click);
            this.btnDown.MouseDown += new MouseEventHandler(this.btnDown_MouseDown);
            this.btnDown.MouseUp += new MouseEventHandler(this.btnDown_MouseUp);
            this.btnUp.MouseDown += new MouseEventHandler(this.btnUp_MouseDown);
            this.btnUp.MouseUp += new MouseEventHandler(this.btnUp_MouseUp);
            this.btnUp.Paint += new PaintEventHandler(this.btnUp_Paint);
            this.btnUp.Click += new EventHandler(this.btnUp_Click);

            this.textbox.KeyDown += new KeyEventHandler(this.textbox_KeyDown);
            this.textbox.KeyPress += new KeyPressEventHandler(this.textbox_KeyPress);
            this.textbox.Leave += new EventHandler(textbox_TextLeave);
            this.textbox.Parent = this;
            this.textbox.Location = new Point(3, 3);
            this.btnDown.Top = 0;
            this.textbox.BorderStyle = BorderStyle.None;
            this.btnUp.FlatAppearance.BorderSize = this.btnDown.FlatAppearance.BorderSize = 1;
            this.Font = new Font("Century Gothic", 12f);
            base.Invalidate();
            Timer timer = new Timer
            {
                Interval = 400
            };
            this.timer = timer;
            this.timer.Tick += new EventHandler(this.timer_Tick);
        }
        private void textbox_TextLeave(object sender, EventArgs e)
        {
            this.value = Value;
            this.textbox.Text = this.value.ToString();
            base.OnLeave(e);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            this.value = this.dec ? (this.value - 1.0) : (this.value + 1.0);
            this.textbox.Text = this.value.ToString();
            if (this.timer.Interval >= 50)
            {
                this.timer.Interval /= 2;
            }
        }

        private void textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.textbox.SelectedText.Length >= this.textbox.Text.Length)
            {
                this.textbox.Text = "";
            }
            if (((!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) && (e.KeyChar != '.')) && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '-') && ((sender as TextBox).Text.Length > 0))
            {
                e.Handled = true;
            }
        }

        private void textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SystemSounds.Asterisk.Play();
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            this.value++;
            this.textbox.Text = this.value.ToString();
        }

        private void btnUp_Paint(object sender, PaintEventArgs e)
        {
            if (base.Height > 0)
            {
                this.textbox.Font = new Font("Century Gothic", base.Height * 0.5f);
                this.textbox.Location = new Point(3, (base.Height / 2) - (this.textbox.Height / 2));
            }
            e.Graphics.SmoothingMode = (SmoothingMode)SmoothingMode.HighQuality;
            float x = (this.btnUp.Width / 2) - 1;
            float num2 = this.btnUp.Height / 3;
            PointF tf = new PointF((x / 2f) - 2f, num2 * 1.5f);
            PointF tf2 = new PointF(((3f * x) / 2f) + 2f, num2 * 1.5f);
            PointF tf3 = new PointF(x, num2 - 2f);
            PointF tf4 = new PointF(x, (2f * num2) + 2f);

            using (Pen pen = new Pen(this.btnColor, 3f))
            {
                e.Graphics.DrawLine(pen, tf, tf2);
                e.Graphics.DrawLine(pen, tf3, tf4);
            }
        }

        private void btnUp_MouseUp(object sender, MouseEventArgs e)
        {
            this.timer.Interval = 400;
            this.timer.Stop();
            this.dec = false;
        }

        private void btnUp_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnUp.Focus();
            this.dec = false;
            this.value = double.Parse(this.textbox.Text);
            this.timer.Start();
        }

        private void btnDown_MouseUp(object sender, MouseEventArgs e)
        {
            this.timer.Interval = 400;
            this.timer.Stop();
            this.dec = false;
        }

        private void btnDown_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnDown.Focus();
            this.dec = true;
            this.value = double.Parse(this.textbox.Text);
            this.timer.Start();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            this.value--;
            this.textbox.Text = this.value.ToString();
        }

        private void btnDown_Paint(object sender, PaintEventArgs e)
        {
            float num = this.btnUp.Width / 3;
            float num2 = this.btnUp.Height / 3;
            PointF tf = new PointF((this.btnDown.Width / 2) - (num / 2f), num2 * 1.5f);
            PointF tf2 = new PointF(tf.X + num, num2 * 1.5f);
            using (Pen pen = new Pen(this.btnColor, 3f))
            {
                e.Graphics.DrawLine(pen, tf, tf2);
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            this.textbox.Font = this.Font;
            base.Height = this.Font.Height * 2;
            base.OnFontChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.btnUp.Left = (base.Width - (this.btnUp.Width * 2)) + 1;
            this.btnDown.Left = base.Width - this.btnDown.Width;
            this.btnDown.Top = 0;
            this.btnUp.Height = this.btnDown.Height = base.Height - 1;
            e.Graphics.FillRectangle(Brushes.White, 0, 0, base.Width, base.Height);
            e.Graphics.DrawRectangle(Pens.Gray, 0, 0, base.Width - 1, base.Height - 1);
            this.textbox.Width = (base.Width - (2 * this.btnDown.Width)) - 4;
            using (Pen pen = new Pen(Color.Black, 2f))
            {
                base.OnPaint(e);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        public Color ButtonColor
        {
            get =>
                this.btnColor;
            set
            {
                this.btnColor = value;
                base.Invalidate();
            }
        }

        public double Value
        {
            get =>
                this.value;
            set
            {
                this.value = value;
                this.textbox.Text = value.ToString();
                base.Invalidate();
            }
        }

        public int BtnUpDownWidth
        {
            get =>
                this.btnDown.Width = this.btnUp.Width;
            set
            {
                this.btnDown.Width = this.btnUp.Width = value;
                Invalidate();
            }
        }
    }
}
