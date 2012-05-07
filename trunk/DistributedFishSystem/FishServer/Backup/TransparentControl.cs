using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FishServer
{
    public partial class transparentControl : UserControl
    {
        private Color _borderColor;
        private Color _backgroundColor;
        private Point _clickLocation;

        public transparentControl()
        {
            InitializeComponent();

            _borderColor = Color.Yellow;
            _backgroundColor = Color.Red;
        }

        public void PaintBackgroundColor()
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(100, _backgroundColor)), this.ClientRectangle);
                g.DrawRectangle(new Pen(_borderColor, 3), this.ClientRectangle);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, _backgroundColor)), this.ClientRectangle);
        //    e.Graphics.DrawRectangle(new Pen(_borderColor, 3), this.ClientRectangle);
        //}

        private void transparentControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - _clickLocation.X;
                this.Top += e.Y - _clickLocation.Y;
                Invalidate();
            }
        }

        private void transparentControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(10, _backgroundColor)), this.ClientRectangle);
            e.Graphics.DrawRectangle(new Pen(_borderColor, 3), this.ClientRectangle);
        }

        private void transparentControl_MouseDown(object sender, MouseEventArgs e)
        {
            _clickLocation = e.Location;
        }

    }
}
