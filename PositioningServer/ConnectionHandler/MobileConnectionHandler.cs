using PositioningServer.Common.Interface;

namespace PositioningServer.ConnectionHandler
{
    class MobileConnectionHandler : IConnectionHandler
    {
        private AsyncListener listener = null;

        public void Update()
        {
            if (listener == null)
            {
                listener = new AsyncListener();
            }
            listener.StartListening();
        }
    }
}
