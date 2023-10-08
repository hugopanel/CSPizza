using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Modules
{
    internal static class CommunicationsModule
    {
        public static void Send(string queue, string message)
        {
            throw new NotImplementedException();
        }

        public static string Receive(string queue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Patron de la fonction callback, appelée lorsqu'un type de message est reçu. 
        /// </summary>
        /// <param name="origin_queue"></param>
        /// <param name="message"></param>
        public delegate void HandleMessage(string origin_queue, string message);

        public static void SubscribeToMessageType(HandleMessage callback, string messageType)
        {
            throw new NotImplementedException();
        }
    }
}
