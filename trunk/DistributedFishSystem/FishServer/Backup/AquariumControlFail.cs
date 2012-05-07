using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FishServer.Controls
{
    public partial class AquariumControlFail : UserControl
    {
        //------------------------------- Class members --------------------------//

        private List<ClientWindowFail1> _clientWindowList;
        private ClientWindowFail1 _activeClient;

        //------------------------------- Event delegates ------------------------//

        public event EventHandler ClientWindowAdded;

        //------------------------------- Constructors ---------------------------//

        public AquariumControlFail()
        {
            InitializeComponent();

            _clientWindowList = new List<ClientWindowFail1>();
        }

        //------------------------------- Public methods -------------------------//

        public void AddClientWindow(ClientWindowFail1 window)
        {
            _clientWindowList.Add(window);
            OnClientWindowAdded();
        }

        //------------------------------- Protected methods ----------------------//

        protected void OnClientWindowAdded()
        {
            if (ClientWindowAdded != null)
            {
                ClientWindowAdded(this, EventArgs.Empty);
            }
        }

        //------------------------------- Event handlers -------------------------//

        private void AquariumControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (_activeClient != null)
                _activeClient.MouseDownOnWindow(e);
        }

        private void AquariumControl_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (ClientWindowFail1 window in _clientWindowList)
            {
                if (window.ClientSize.Contains(e.Location))
                {
                    this.Cursor = Cursors.Hand;
                    _activeClient = window;
                    window.Paint();
                    _activeClient.MouseMoveOnWindow(e);
                    return;
                }
            }

            this.Cursor = Cursors.Default;
            _activeClient = null;
        }

        //------------------------------- Properties -----------------------------//

        public List<ClientWindowFail1> ClientWindows
        {
            get { return _clientWindowList; }
        }

        private void AquariumControl_Paint(object sender, PaintEventArgs e)
        {
            //this.Invalidate()
        }
    }
}
