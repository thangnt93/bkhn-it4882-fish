using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FishServer.Controls;

namespace FishServer
{
    public class ClientWindow
    {
        //------------------------- Class members ----------------------//

        private AquariumControl _aquariumControl;

        private Bitmap _image;
        private int _backgroundAlpha = 100;

        private Rectangle _currentSight;
        private Color _backgroundColor;
        private Color _borderColor;
        private Color _idColor;

        private Point _lastMouseDownLocation;

        private int _minimumWidth = 50;
        private int _minimumHeight = 50;

        private int _id;

        //------------------------- State members ----------------------//

        private bool _focuced;
        private MouseMode _mouseMode = MouseMode.None;

        //------------------------- Event delegates --------------------//

        public event EventHandler GotFocus;
        public event EventHandler LostFocus;
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseEnter;
        public event MouseEventHandler MouseLeave;
        public event MouseEventHandler MouseClick;
        public event ClientWindowLocationEventHandler LocationChanged;
        public event ClientWindowSizeEventHandler SizeChanged;

        //------------------------- Constructors -----------------------//

        public ClientWindow(int id, AquariumControl aquarium, Rectangle sight)
        {
            _id = id;
            _aquariumControl = aquarium;

            _currentSight = sight;
            if (_currentSight.Width < _minimumWidth) _currentSight.Width = _minimumWidth;
            if (_currentSight.Height < _minimumHeight) _currentSight.Height = _minimumHeight;

            if (_currentSight.Width > _aquariumControl.Width - _currentSight.X)
                _currentSight.Width = _aquariumControl.Width - _currentSight.X;
            if (_currentSight.Height > _aquariumControl.Height - _currentSight.Y)
                _currentSight.Height = _aquariumControl.Height - _currentSight.Y;

            _backgroundColor = Color.FromArgb(_backgroundAlpha, Color.LightYellow);
            _borderColor = Color.Red;
            _idColor = Color.GreenYellow;

            _image = createBackgroundImage(_currentSight.Width, _currentSight.Height, false);

            this.GotFocus += new EventHandler(ClientWindow_GotFocus);
            this.LostFocus += new EventHandler(ClientWindow_LostFocus);
            this.MouseClick += new MouseEventHandler(ClientWindow_MouseClick);
            this.MouseDown += new MouseEventHandler(ClientWindow_MouseDown);
            this.MouseMove += new MouseEventHandler(ClientWindow_MouseMove);
            this.LocationChanged += new ClientWindowLocationEventHandler(ClientWindow_LocationChanged);
            this.SizeChanged += new ClientWindowSizeEventHandler(ClientWindow_SizeChanged);
        }

        //------------------------- Public methods ---------------------//

        public bool Contains(Point point)
        {
            return _currentSight.Contains(point);
        }

        //------------------------- Public event methods ---------------//

        public void OnGotFocus(EventArgs e)
        {
            if (GotFocus != null)
            {
                GotFocus(this, e);
            }
        }

        public void OnLostFocus(EventArgs e)
        {
            if (LostFocus != null)
            {
                LostFocus(this, e);
            }
        }

        public void OnMouseDown(MouseEventArgs e)
        {
            if (MouseDown != null)
            {
                MouseDown(this, e);
            }
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            if (MouseMove != null)
            {
                MouseMove(this, e);
            }
        }

        public void OnMouseEnter(MouseEventArgs e)
        {
            if (MouseEnter != null)
            {
                MouseEnter(this, e);
            }
        }

        public void OnMouseLeave(MouseEventArgs e)
        {
            if (MouseLeave != null)
            {
                MouseLeave(this, e);
            }
        }

        public void OnMouseClick(MouseEventArgs e)
        {
            if (MouseClick != null)
            {
                MouseClick(this, e);
            }
        }

        public void OnLocationChanged(ClientWindowLocationEventArgs e)
        {
            if (LocationChanged != null)
            {
                LocationChanged(this, e);
            }
        }

        public void OnSizeChanged(ClientWindowSizeEventArgs e)
        {
            if (SizeChanged != null)
            {
                SizeChanged(this, e);
            }
        }

        //------------------------- Private methods --------------------//

        private void move(int dx, int dy)
        {
            bool moved = false;

            int newXPosition = _currentSight.X + dx;
            int newYPosition = _currentSight.Y + dy;

            if (newXPosition < 0) newXPosition = 0;
            if (newYPosition < 0) newYPosition = 0;

            if (newXPosition + _currentSight.Width > _aquariumControl.Width)
                newXPosition = _aquariumControl.Width - _currentSight.Width;
            if (newYPosition + _currentSight.Height > _aquariumControl.Height)
                newYPosition = _aquariumControl.Height - _currentSight.Height;

            if (newXPosition != _currentSight.X)
            {
                moved = true;
                _currentSight.X = newXPosition;
            }
            if (newYPosition != _currentSight.Y)
            {
                moved = true;
                _currentSight.Y = newYPosition;
            }

            if (moved)
                OnLocationChanged(new ClientWindowLocationEventArgs(_currentSight.X, _currentSight.Y));
        }

        private void resize(int newWidth, int newHeight)
        {
            bool changed = false;

            if (newWidth < _minimumWidth) newWidth = _minimumWidth;
            if (newHeight < _minimumHeight) newHeight = _minimumHeight;

            if (_currentSight.X + newWidth > _aquariumControl.Width)
                newWidth = _aquariumControl.Width - _currentSight.X;
            if (_currentSight.Y + newHeight > _aquariumControl.Height)
                newHeight = _aquariumControl.Height - _currentSight.Y;

            if (_currentSight.Width != newWidth || _currentSight.Height != newHeight)
                changed = true;

            _currentSight.Width = newWidth;
            _currentSight.Height = newHeight;

            if (changed)
                OnSizeChanged(new ClientWindowSizeEventArgs(newWidth, newHeight));
        }

        private Bitmap createBackgroundImage(int width, int height, bool withBorder)
        {
            Bitmap image = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(image))
            {
                g.FillRectangle(new SolidBrush(_backgroundColor), 0, 0, image.Width, image.Height);

                Font font = new Font("Arial", 25);
                string idString = _id.ToString();
                SizeF size = g.MeasureString(idString, font);
                g.DrawString(idString, font, new SolidBrush(_idColor), _currentSight.Width / 2 - size.Width / 2, _currentSight.Height / 2 - size.Height / 2);
                if (withBorder)
                {
                    g.DrawRectangle(new Pen(_borderColor, 5), 0, 0, image.Width, image.Height);
                }
            }

            return image;
        }

        private void validateMouseMode(MouseEventArgs e)
        {
            if (e.X < _currentSight.Width - 10 && e.Y < _currentSight.Height - 10)
            {
                _aquariumControl.Cursor = Cursors.Hand;
                _mouseMode = MouseMode.Move;
            }
            else if (e.X >= _currentSight.Width - 10 && e.Y < _currentSight.Height - 10)
            {
                _aquariumControl.Cursor = Cursors.SizeWE;
                _mouseMode = MouseMode.ResizeWidth;
            }
            else if (e.X < _currentSight.Width - 10 && e.Y >= _currentSight.Height - 10)
            {
                _aquariumControl.Cursor = Cursors.SizeNS;
                _mouseMode = MouseMode.ResizeHeight;
            }
            else
            {
                _aquariumControl.Cursor = Cursors.SizeNWSE;
                _mouseMode = MouseMode.ResizeBoth;
            }
        }

        //------------------------- Event handlers ---------------------//

        private void ClientWindow_GotFocus(object sender, EventArgs e)
        {
            _image = createBackgroundImage(_currentSight.Width, _currentSight.Height, true);
            _aquariumControl.Invalidate();
        }

        private void ClientWindow_LostFocus(object sender, EventArgs e)
        {
            using (Graphics g = Graphics.FromImage(_image))
            {
                _image = createBackgroundImage(_currentSight.Width, _currentSight.Height, false);
            }

            _aquariumControl.Cursor = Cursors.Default;
            _aquariumControl.Invalidate();
        }

        private void ClientWindow_MouseClick(object sender, MouseEventArgs e)
        {
            _aquariumControl.Invalidate();
        }

        private void ClientWindow_MouseDown(object sender, MouseEventArgs e)
        {
            _lastMouseDownLocation = e.Location;
            _aquariumControl.Invalidate();
        }

        private void ClientWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_mouseMode == MouseMode.Move)
                {
                    this.move(e.X - _lastMouseDownLocation.X, e.Y - _lastMouseDownLocation.Y);
                }
                else if (_mouseMode == MouseMode.ResizeWidth)
                {
                    this.resize(e.X, _currentSight.Height);
                }
                else if (_mouseMode == MouseMode.ResizeHeight)
                {
                    this.resize(_currentSight.Width, e.Y);
                }
                else if (_mouseMode == MouseMode.ResizeBoth)
                {
                    this.resize(e.X, e.Y);
                }
            }
            else
            {
                validateMouseMode(e);
            }
        }

        private void ClientWindow_LocationChanged(object sender, ClientWindowLocationEventArgs e)
        {
            _aquariumControl.Invalidate();
        }

        private void ClientWindow_SizeChanged(object sender, ClientWindowSizeEventArgs e)
        {
            _image = createBackgroundImage(e.Width, e.Height, true);
            _aquariumControl.Invalidate();
        }

        //------------------------- Properties -------------------------//

        public Bitmap Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public Rectangle CurrentSight
        {
            get { return _currentSight; }
            set { _currentSight = value; }
        }

        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; }
        }

        public Size Size
        {
            get { return _currentSight.Size; }
            set { _currentSight.Size = value; }
        }

        public Point Location
        {
            get { return _currentSight.Location; }
            set { _currentSight.Location = value; }
        }

        public int X
        {
            get { return _currentSight.X; }
            set { _currentSight.X = value; }
        }

        public int Y
        {
            get { return _currentSight.Y; }
            set { _currentSight.Y = value; }
        }

        public bool Focused
        {
            get { return _focuced; }
            set
            {
                _focuced = value;
                if (_focuced)
                {
                    OnGotFocus(EventArgs.Empty);
                }
                else
                {
                    OnLostFocus(EventArgs.Empty);
                }
            }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        //------------------------- Enumeration ------------------------//

        private enum MouseMode
        {
            None, Move,
            ResizeWidth, ResizeHeight, ResizeBoth
        }
    }
}
