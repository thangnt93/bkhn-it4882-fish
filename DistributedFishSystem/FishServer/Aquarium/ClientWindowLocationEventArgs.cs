using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FishServer
{
    public class ClientWindowLocationEventArgs : EventArgs
    {
        //---------------------- Class members ----------------------//

        private int _x;
        private int _y;

        //---------------------- Constructors -----------------------//

        public ClientWindowLocationEventArgs(int newX, int newY)
        {
            _x = newX;
            _y = newY;
        }

        //---------------------- Properties -------------------------//

        /// <summary>
        /// The new X location of window
        /// </summary>
        public int X
        {
            get { return _x; }
        }

        /// <summary>
        /// The new Y location of window
        /// </summary>
        public int Y
        {
            get { return _y; }
        }
    }
}
