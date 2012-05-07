using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace FishServer
{
    public class ClientWindowFail1 : INotifyPropertyChanged
    {
        //-------------------------- Class members --------------------------//

        /// <summary>
        /// The image represent the client window.
        /// </summary>
        private Bitmap _image;

        /// <summary>
        /// The control that contain this client window.
        /// </summary>
        private Control _parent;

        /// <summary>
        /// Size of this client window, width and height in pixels.
        /// </summary>
        private Size _size;

        /// <summary>
        /// Location of this client window in its parent control.
        /// </summary>
        private Point _location;

        /// <summary>
        /// The rectangle represent the location and size of this window.
        /// </summary>
        private Rectangle _clientSize;

        private Point _clickLocation;

        //-------------------------- Event delegates ------------------------//

        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseMove;
        public event PropertyChangedEventHandler PropertyChanged;

        //-------------------------- Constructors ---------------------------//

        /// <summary>
        /// Construct new ClientWindow object.
        /// </summary>
        /// <param name="parent">The control which this client window store in</param>
        /// <param name="image">The background image of this client window</param>
        public ClientWindowFail1(Control parent, Bitmap image)
        {
            _parent = parent;
            _image = image;
            _size = image.Size;
            _location = new Point(0, 0);
            _clientSize = new Rectangle(_location, _size);

            this.MouseDown += new MouseEventHandler(ClientWindow_MouseDown);
            this.MouseMove += new MouseEventHandler(ClientWindow_MouseMove);
            this.PropertyChanged += new PropertyChangedEventHandler(ClientWindow_PropertyChanged);
        }

        //-------------------------- Public methods -------------------------//

        /// <summary>
        /// Paint background image on parent control.
        /// </summary>
        public void Paint()
        {
            using (Graphics g = _parent.CreateGraphics())
            {
                g.DrawImage(_image, _location);
            }
        }

        /// <summary>
        /// This method is called when this client window is active (mouse pointer
        /// is over this client-size) client window in aquarium, and mouse press
        /// </summary>
        /// <param name="e"></param>
        public void MouseDownOnWindow(MouseEventArgs e)
        {
            if (MouseDown != null)
            {
                MouseDown(this, e);
            }
        }

        /// <summary>
        /// This method is called when this client window is active (mouse pointer
        /// is over this client-size) client window in aquarium, and mouse move
        /// </summary>
        /// <param name="e"></param>
        public void MouseMoveOnWindow(MouseEventArgs e)
        {
            if (MouseMove != null)
            {
                MouseMove(this, e);
            }
        }

        //-------------------------- Protected methods ----------------------//

        protected void OnMouseDown(MouseEventArgs e)
        {
            if (MouseDown != null)
            {
                MouseDown(this, e);
            }
        }

        protected void OnMouseMove(MouseEventArgs e)
        {
            if (MouseMove != null)
            {
                MouseMove(this, e);
            }
        }

        //-------------------------- Event handlers -------------------------//

        private void ClientWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
        }

        private void ClientWindow_MouseDown(object sender, MouseEventArgs e)
        {
            _clickLocation = e.Location;
        }

        void ClientWindow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        //-------------------------- Properties -----------------------------//

        public Bitmap Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public Control Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public Rectangle ClientSize
        {
            get { return _clientSize; }
            set { _clientSize = value; }
        }

        public Point Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Location"));
                }
            }
        }
    }
}
