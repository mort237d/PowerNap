using System;
using System.Text.RegularExpressions;

namespace ModelLibrary
{
    [Serializable]
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
            set
            {
                Regex rgx = new Regex(@"\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.|$)){4}\b");

                if (!rgx.IsMatch(value))
                {
                    throw new ArgumentException("Not a valid IPv4 address.");
                }
                _clientIpAddress = value;
            }
        }

        public ClientInfo(int portNumber, string clientIpAddress)
        {
            _portNumber = portNumber;
            _clientIpAddress = clientIpAddress;
        }

        public ClientInfo()
        {
            
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                ClientInfo ci = (ClientInfo)obj;
                return (_portNumber == ci.PortNumber) && (_clientIpAddress == ci.ClientIpAddress);
            };
        }

        public override int GetHashCode()
        {
            return (_portNumber.GetHashCode() * 7) + (_clientIpAddress.GetHashCode() * 7);
        }
    }
}
