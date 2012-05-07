using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using Fishes.Remoting;
using Fishes.Component;
using System.Runtime.Remoting;

namespace FishClient
{
    public partial class ClientForm : Form
    {
        //-------------------------- Class members --------------------------//

        IFishServicesFactory _factory = null;
        IFishServices _servies = null;

        //-------------------------- Constructors ---------------------------//

        public ClientForm()
        {
            InitializeComponent();

            ChannelServices.RegisterChannel(new TcpChannel(), false);
        }

        //-------------------------- Event handlers -------------------------//

        private void serverConnectButton_Click(object sender, EventArgs e)
        {
            if (serverConnectButton.Text == "Connect")
            {
                try
                {
                    _factory = (IFishServicesFactory)Activator.GetObject(typeof(IFishServicesFactory), string.Format("tcp://{0}:{1}/FishServicesFactory.rem", serverIpTextBox.Text, serverPortTextBox.Text));
                    _servies = _factory.CreateServies();
                    _servies.Connect();
                    serverConnectButton.Text = "Disconnect";

                    timer1.Start();
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot connect to server");
                }
            }
            else if (serverConnectButton.Text == "Disconnect")
            {
                try
                {
                    _servies.Disconnect();
                    _factory = null;
                    _servies = null;
                    serverConnectButton.Text = "Connect";

                    timer1.Stop();
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot connect to server");
                    serverConnectButton.Text = "Connect";
                }
            }
        }

        private void ClientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (_servies != null)
                {
                    _servies.Disconnect();
                }
            }
            catch (Exception)
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string location = _servies.GetBackgroundLocation();
            string[] coords = location.Split(' ');
            int x = Convert.ToInt32(coords[0]);
            int y = Convert.ToInt32(coords[1]);
            int width = Convert.ToInt32(coords[2]);
            int height= Convert.ToInt32(coords[3]);
            Rectangle r = new Rectangle(x, y, width, height);

            Bitmap background = Properties.Resources.Background;
            aquariumPictureBox.Image = background.Clone(r, background.PixelFormat);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string location = _servies.GetBackgroundLocation();
        }
    }
}

//private void getFishButton_Click(object sender, EventArgs e)
//{
//    if (_factory == null)
//    {
//        _factory = (IFishServicesFactory)Activator.GetObject(typeof(IFishServicesFactory), string.Format("tcp://{0}:{1}/FishServicesFactory.rem", serverIpTextBox.Text, serverPortTextBox.Text));
//    }

//    IFishServices remoteObject = _factory.CreateServies();

//    //using (Graphics g = aquariumPictureBox.CreateGraphics())
//    //{
//    //    IFishServices remoteObject = _factory.CreateServies();
//    //    Fish fish = remoteObject.GetFish();

//    //    g.DrawImage(Image.FromStream(fish.PictureStream), 0, 0);
//    //}
//}

//private void clearFishButton_Click(object sender, EventArgs e)
//{
//    using (Graphics g = aquariumPictureBox.CreateGraphics())
//    {
//        g.Clear(Color.Transparent);
//    }
//}

//private void button1_Click(object sender, EventArgs e)
//{
//    using (Graphics g = aquariumPictureBox.CreateGraphics())
//    {
//        IFishServices remoteObject = _factory.CreateServies();


//    }
//}