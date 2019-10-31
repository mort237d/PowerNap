using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class ClientInfo
    {
        private int _portNumber;
        private string _clientIpAddress;

        public int PortNumber
        {
            get => _portNumber;
            set => _portNumber = value;
        }

        public string ClientIpAddress
        {
            get => _clientIpAddress;
            set => _clientIpAddress = value;
        }

        public ClientInfo(int portNumber, string clientIpAddress)
        {
            _portNumber = portNumber;
            _clientIpAddress = clientIpAddress;
        }

        public ClientInfo()
        {
            
        }
    }
}
