using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting.Channels;
using System.Collections;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting;
using System.Net;
using System.Net.Sockets;
using Fishes.Component;

namespace FishServer
{
    public partial class ServerForm : Form
    {
        //--------------------------- Class members --------------------------//

        private TcpChannel _channel;

        //--------------------------- Constructors ---------------------------//
        
        public ServerForm()
        {
            InitializeComponent();

            initChannel();
            findServerIpAddress();

            ChannelServices.RegisterChannel(_channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(FishServiesFactory), "FishServicesFactory.rem", WellKnownObjectMode.Singleton);

            FishServiesFactory.AquariumControl = this.aquariumControl;
        }

        
        //--------------------------- Event handlers -------------------------//

        //--------------------------- Private methods ------------------------//

        private void initChannel()
        {
            BinaryServerFormatterSinkProvider bp = new BinaryServerFormatterSinkProvider();
            ClientIPServerSinkProvider csp = new ClientIPServerSinkProvider();
            csp.Next = bp;
            Hashtable ht = new Hashtable();
            ht.Add("port", Convert.ToInt32(serverPortTextBox.Text));

            _channel = new TcpChannel(ht, null, csp);
        }

        private void findServerIpAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    serverIpTextBox.Text = ip.ToString();
                    break;
                }
            }
        }
    }
}
