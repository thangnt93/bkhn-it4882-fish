using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fishes.Component
{
    public class ServerState : MarshalByRefObject
    {
        //---------------------- Class members ----------------------//

        private bool _backgroundChanged;
        private bool _fishesChanged;

        //---------------------- Constructors -----------------------//

        public ServerState(bool backgroundChanged, bool fishesChanged)
        {
            _backgroundChanged = backgroundChanged;
            _fishesChanged = fishesChanged;
        }

        //---------------------- Properties -------------------------//

        public bool BackgroundChanged
        {
            get { return _backgroundChanged; }
            set { _backgroundChanged = value; }
        }

        public bool FishesChanged
        {
            get { return _fishesChanged; }
            set { _fishesChanged = value; }
        }
    }
}
