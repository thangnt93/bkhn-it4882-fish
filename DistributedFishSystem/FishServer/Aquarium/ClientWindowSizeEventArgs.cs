using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FishServer
{
    public class ClientWindowSizeEventArgs : EventArgs
    {
        //------------------------ Class members ----------------------//

        private int _width;
        private int _height;

        //------------------------ Constructors -----------------------//

        public ClientWindowSizeEventArgs(int newWidth, int newHeight)
        {
            _width = newWidth;
            _height = newHeight;
        }

        //------------------------ Properties -------------------------//

        /// <summary>
        /// The new width of window 
        /// </summary>
        public int Width
        {
            get { return _width; }
        }

        /// <summary>
        /// The new height of window
        /// </summary>
        public int Height
        {
            get { return _height; }
        }
    }
}
