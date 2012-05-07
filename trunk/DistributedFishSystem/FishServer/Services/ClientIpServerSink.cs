using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Channels;
using System.Net;
using System.Runtime.Remoting.Messaging;

namespace FishServer
{
    public class ClientIpServerSink : BaseChannelObjectWithProperties, IServerChannelSink, IChannelSinkBase
    {
        //---------------------- Class members ----------------------------------//

        private IServerChannelSink _nextSink = null;

        //---------------------- Constructors -----------------------------------//

        public ClientIpServerSink(IServerChannelSink next)
        {
            _nextSink = next;
        }

        //---------------------- Implement IServerChannelSink -------------------//

        public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, System.Runtime.Remoting.Messaging.IMessage msg, ITransportHeaders headers, System.IO.Stream stream)
        {
            IPAddress ip = headers[CommonTransportKeys.IPAddress] as IPAddress;
            CallContext.SetData("ClientIpAddress", ip);
            sinkStack.AsyncProcessResponse(msg, headers, stream);
        }

        public System.IO.Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, System.Runtime.Remoting.Messaging.IMessage msg, ITransportHeaders headers)
        {
            return null;
        }

        public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, System.Runtime.Remoting.Messaging.IMessage requestMsg, ITransportHeaders requestHeaders, System.IO.Stream requestStream, out System.Runtime.Remoting.Messaging.IMessage responseMsg, out ITransportHeaders responseHeaders, out System.IO.Stream responseStream)
        {
            if (_nextSink != null)
            {
                IPAddress ip = requestHeaders[CommonTransportKeys.IPAddress] as IPAddress;
                CallContext.SetData("ClientIpAddress", ip);
                ServerProcessing spres = _nextSink.ProcessMessage(sinkStack, requestMsg, requestHeaders, requestStream, out responseMsg, out responseHeaders, out responseStream);

                return spres;
            }
            else
            {
                responseMsg = null;
                responseHeaders = null;
                responseStream = null;

                return new ServerProcessing();
            }
        }

        //---------------------- Implement IChannelSinkBase ---------------------//

        System.Collections.IDictionary IChannelSinkBase.Properties
        {
            get { throw new NotImplementedException(); }
        }

        //---------------------- Properties -------------------------------------//

        public IServerChannelSink NextChannelSink
        {
            get { return _nextSink; }
            set { _nextSink = value; }
        }
    }
}
