using RibbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    internal class Message : RibbitMQ.IMessage<MessageType>
    {
        MessageType IMessage<MessageType>.MessageType { get; set; }
        object? IMessage<MessageType>.Content { get; set; }
        object? IMessage<MessageType>.From { get; set; }
        object? IMessage<MessageType>.To { get; set; }
        SendType? IMessage<MessageType>.SendType { get; set; }
    }
}
