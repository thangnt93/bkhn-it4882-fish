using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Fishes.Component
{
    [Serializable]
    public class Fish : MarshalByRefObject
    {
        //------------------------ Class members ---------------------//

        private int _x;
        private int _y;

        private int _vx;
        private int _vy;

        private Bitmap _picture;

        //------------------------ Constructors ----------------------//

        public Fish(Bitmap picture)
        {
            _x = 0;
            _y = 0;
            _vx = 10;
            _vy = 10;
            _picture = picture;
        }

        public Fish(Bitmap picture, int x, int y, int vx, int vy)
            : this(picture)
        {
            _x = x;
            _y = y;
            _vx = vx;
            _vy = vy;
        }

        //------------------------ Properties ------------------------//

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public int Vx
        {
            get { return _vx; }
            set { _vx = value; }
        }

        public int Vy
        {
            get { return _vy; }
            set { _vy = value; }
        }

        public Bitmap Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }

        public Stream PictureStream
        {
            get
            {
                Stream stream = new MemoryStream();
                _picture.Save(stream, ImageFormat.Png);

                return stream;
            }
        }
    }
}
