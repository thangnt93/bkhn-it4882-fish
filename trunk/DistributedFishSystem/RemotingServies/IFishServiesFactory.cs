﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fish.Remoting
{
    public interface IFishServiesFactory
    {
        IFishServies CreateServies();
    }
}
