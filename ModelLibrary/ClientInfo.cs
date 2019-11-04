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
