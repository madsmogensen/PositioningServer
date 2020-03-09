using PositioningServer.Common.Interface;
using System;
using System.Threading;

namespace PositioningServer.ConnectionHandler
{
    class MobileConnectionHandler : IConnectionHandler
    {
        //private AsyncListener listener = AsyncListener.Instance;

        public void Instantiate()
        {
            //Thread listenerThread = new Thread(listener.StartListening);
            //listenerThread.Start();
            TCPConnection server = new TCPConnection();
        }

        public void Update()
        {
            /*if (listener is null)
            {
                //Console.WriteLine("Listener is null :(");
            }
            else
            {
                //Console.WriteLine("Listener is YAY");
            }*/
        }
    }
}
