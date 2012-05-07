using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Channels;
using System.Collections;

namespace FishServer
{
    public class ClientIPServerSinkProvider : IServerChannelSinkProvider
    {
        //-------------------- CLASS MEMBERS -------------------------------------//

        private IServerChannelSinkProvider _nextProvider = null;

        //-------------------- CONSTRUCTOR ---------------------------------------//

        public ClientIPServerSinkProvider()
        {

        }

        public ClientIPServerSinkProvider(IDictionary properties, ICollection providerData)
        {

        }

        //-------------------- IMPLEMENT IServerChannelSinkProvier ---------------//

        public IServerChannelSink CreateSink(IChannelReceiver channel)
        {
            IServerChannelSink nextSink = null;
            if (_nextProvider != null)
            {
                nextSink = _nextProvider.CreateSink(channel);
            }
            return new ClientIpServerSink(nextSink);
        }

        public void GetChannelData(IChannelDataStore channelData)
        {
            
        }

        //-------------------- PROPERTIES ----------------------------------------//

        public IServerChannelSinkProvider Next
        {
            get { return _nextProvider; }
            set { _nextProvider = value; }
        }
    }
}
