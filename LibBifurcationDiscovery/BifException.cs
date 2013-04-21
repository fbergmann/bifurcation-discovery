using SBW;

namespace LibBifurcationDiscovery
{
    public class BifException : SBWApplicationException
    {
        public BifException(string message, string detail) : base(message, detail)
        {
        }

    }
}