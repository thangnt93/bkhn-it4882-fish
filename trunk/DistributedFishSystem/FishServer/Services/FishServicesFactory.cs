using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fishes.Remoting;
using System.Runtime.Remoting.Messaging;
using FishServer.Controls;
using System.Drawing;

namespace FishServer
{
    public class FishServiesFactory : MarshalByRefObject, IFishServicesFactory
    {
        //--------------------- Static members ----------------------//

        private static AquariumControl _aquariumControl;

        //--------------------- Constructors ------------------------//

        public IFishServices CreateServies()
        {
            string clientIp = CallContext.GetData("ClientIpAddress").ToString();
            return new FishServices(_aquariumControl, _aquariumControl.NewClientId());
        }

        //--------------------- Properties --------------------------//

        public static AquariumControl AquariumControl
        {
            get { return _aquariumControl; }
            set { _aquariumControl = value; }
        }
    }
}
