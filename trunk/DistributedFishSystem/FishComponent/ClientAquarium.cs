using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace FishComponent
{
    public class ClientAquarium : MarshalByRefObject
    {
        //---------------------- Class members -------------------//

        private Bitmap _picture;

        //---------------------- Constructors --------------------//

        public ClientAquarium(Bitmap picture)
        {
            _picture = picture;
        }

        //---------------------- Properties ----------------------//

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
