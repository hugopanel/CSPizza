using RibbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Message : RibbitMQ.IMessage<MessageType>
    {
        public MessageType MessageType { get; set; }
        public object? Content { get; set; }
        public object? From { get; set; }
        public object? To { get; set; }
        public SendType? SendType { get; set; } = RibbitMQ.SendType.All;

    }
}
