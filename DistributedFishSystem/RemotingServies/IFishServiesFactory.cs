using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fishes.Remoting
{
    public interface IFishServicesFactory
    {
        IFishServices CreateServies();
    }
}
