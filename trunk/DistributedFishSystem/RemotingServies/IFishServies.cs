using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Fishes.Component;
using System.IO;
using Fishes.Component;

namespace Fishes.Remoting
{
    public interface IFishServices
    {
        void Connect();
        void Disconnect();

        Background GetBackground();
        string GetBackgroundLocation();

        Fish[] GetFishes();
        ServerState GetServerState();
    }
}
