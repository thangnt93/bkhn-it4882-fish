using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Fishes.Remoting;
using System.IO;
using System.Drawing.Imaging;
using FishServer.Controls;
using Fishes.Component;

namespace FishServer
{
    public class FishServices : MarshalByRefObject, IFishServices
    {
        //------------------------- Class members ------------------------------//

        private AquariumControl _aquariumControl;
        private int _clientId;

        //------------------------- Constructor --------------------------------//

        public FishServices(AquariumControl aquariumControl, int clientId)
        {
            _aquariumControl = aquariumControl;
            _clientId = clientId;
        }

        //------------------------- Implement IFishServies ---------------------//

        public void Connect()
        {
            Rectangle sight = new Rectangle(_aquariumControl.Width / 4, _aquariumControl.Height / 4, _aquariumControl.Width / 2, _aquariumControl.Height / 2);
            ClientWindow window = new ClientWindow(_clientId, _aquariumControl, sight);
            _aquariumControl.AddClientWindow(window);
        }

        public void Disconnect()
        {
            ClientWindow window = _aquariumControl.GetClientWindow(_clientId);
            _aquariumControl.RemoveClientWindow(window);
        }

        public Background GetBackground()
        {
            return new Background(_aquariumControl.GetBackgroundImage(_clientId));
        }

        public Fishes.Component.Fish[] GetFishes()
        {
            throw new NotImplementedException();
        }

        public Fishes.Component.ServerState GetServerState()
        {
            throw new NotImplementedException();
        }


        public string GetBackgroundLocation()
        {
            Rectangle sight = _aquariumControl.GetClientWindow(_clientId).CurrentSight;
            return sight.X.ToString() + " " + sight.Y.ToString() + " " + sight.Width.ToString() + " " + sight.Height.ToString();
        }

        //------------------------- Properties ---------------------------------//

        public int ClientId
        {
            get { return _clientId; }
        }
    }
}
