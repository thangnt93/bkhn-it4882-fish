using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fishes.Component;

namespace FishServer.Controls
{
    public partial class AquariumControl : UserControl
    {
        //---------------------------- Class members ---------------------------//

        private List<Fish> _fishList;
        private List<ClientWindow> _clientWindowList;
        private ClientWindow _focusedClientWindow;

        //---------------------------- Event delegates -------------------------//

        public event EventHandler ClientWindowAdded;
        public event EventHandler ClientWindowRemoved;

        //---------------------------- Constructors ----------------------------//

        public AquariumControl()
        {
            InitializeComponent();

            _clientWindowList = new List<ClientWindow>();
            _fishList = new List<Fish>();

            this.ClientWindowAdded += new EventHandler(AquariumControl_ClientWindowAdded);
            this.ClientWindowRemoved += new EventHandler(AquariumControl_ClientWindowRemoved);
        }

        //---------------------------- Public methods --------------------------//

        public void AddClientWindow(ClientWindow window)
        {
            _clientWindowList.Add(window);
            OnClientWindowAdded(EventArgs.Empty);
        }

        public void RemoveClientWindow(ClientWindow window)
        {
            if (_clientWindowList.Remove(window))
                OnClientWindowRemoved(EventArgs.Empty);
        }

        public void AddFish(Fish fish)
        {
            _fishList.Add(fish);
        }

        public int NewClientId()
        {
            int id = 0;

            while (true)
            {
                bool found = false;
                foreach (ClientWindow window in _clientWindowList)
                {
                    if (window.Id == id)
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    id++;
                }
                else
                {
                    return id;
                }
            }
        }

        public ClientWindow GetClientWindow(int id)
        {
            ClientWindow window = null;

            foreach (ClientWindow cw in _clientWindowList)
            {
                if (cw.Id == id)
                {
                    window = cw;
                    break;
                }
            }

            return window;
        }

        public Bitmap GetBackgroundImage(int id)
        {
            ClientWindow window = GetClientWindow(id);

            if (window != null)
            {
                Bitmap background = Properties.Resources.Background;
                return background.Clone(window.CurrentSight, background.PixelFormat);
            }
            else
            {
                return null;
            }
        }

        //---------------------------- Public event methods --------------------//

        public void OnClientWindowAdded(EventArgs e)
        {
            if (ClientWindowAdded != null)
            {
                ClientWindowAdded(this, e);
            }
        }

        public void OnClientWindowRemoved(EventArgs e)
        {
            if (ClientWindowRemoved != null)
            {
                ClientWindowRemoved(this, e);
            }
        }

        //---------------------------- Event handlers --------------------------//

        private void AquariumControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (_focusedClientWindow != null)
            {
                _clientWindowList.Remove(_focusedClientWindow);
                _clientWindowList.Add(_focusedClientWindow);
                _focusedClientWindow.OnMouseDown(new MouseEventArgs(e.Button, e.Clicks, e.X - _focusedClientWindow.X, e.Y - _focusedClientWindow.Y, e.Delta));
            }
        }

        private void AquariumControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_focusedClientWindow != null)
                {
                    _focusedClientWindow.OnMouseMove(new MouseEventArgs(e.Button, e.Clicks, e.X - _focusedClientWindow.X, e.Y - _focusedClientWindow.Y, e.Delta));
                }

                return;
            }

            for (int i = _clientWindowList.Count - 1 ; i >= 0; i--)
            {
                ClientWindow window = _clientWindowList[i];
                if (window.Contains(e.Location))
                {
                    MouseEventArgs args = new MouseEventArgs(e.Button, e.Clicks, e.X - window.X, e.Y - window.Y, e.Delta);

                    if (window != _focusedClientWindow)
                    {
                        if (_focusedClientWindow != null)
                        {
                            _focusedClientWindow.Focused = false;
                            _focusedClientWindow.OnMouseLeave(args);
                        }

                        window.Focused = true;
                        _focusedClientWindow = window;
                        window.OnMouseEnter(args);
                    }
                    window.OnMouseMove(args);
                    return;
                }
            }

            if (_focusedClientWindow != null)
            {
                _focusedClientWindow.Focused = false;
                _focusedClientWindow.OnMouseLeave(new MouseEventArgs(e.Button, e.Clicks, e.X - _focusedClientWindow.X, e.Y - _focusedClientWindow.Y, e.Delta));
                _focusedClientWindow = null;
            }
        }

        private void AquariumControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (_focusedClientWindow != null)
            {
                _clientWindowList.Remove(_focusedClientWindow);
                _clientWindowList.Add(_focusedClientWindow);
                _focusedClientWindow.OnMouseClick(new MouseEventArgs(e.Button, e.Clicks, e.X - _focusedClientWindow.X, e.Y - _focusedClientWindow.Y, e.Delta));
            }
        }

        private void AquariumControl_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < _fishList.Count; i++)
            {
                Fish fish = _fishList[i];
                e.Graphics.DrawImage(fish.Picture, fish.X, fish.Y);
            }

            for (int i = 0; i < _clientWindowList.Count; i++)
            {
                ClientWindow window = _clientWindowList[i];
                e.Graphics.DrawImage(window.Image, window.CurrentSight);
            }
        }

        private void AquariumControl_ClientWindowAdded(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void AquariumControl_ClientWindowRemoved(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
