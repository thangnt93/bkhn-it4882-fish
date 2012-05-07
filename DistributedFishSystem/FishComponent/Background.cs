using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Fishes.Component
{
    public class Background : MarshalByRefObject
    {
        //------------------------ Class members ------------------------//

        private Bitmap _picture;

        //------------------------ Constructors -------------------------//

        public Background(Bitmap picture)
        {
            _picture = picture;
        }

        //------------------------ Properties ---------------------------//

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
