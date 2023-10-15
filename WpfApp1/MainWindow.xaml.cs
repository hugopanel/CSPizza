using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            rtb_Customer.TextChanged += richTextBox_TextChanged;
        }

        private void OnTextBoxKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run(txt_UserInput.Text));
                rtb_Customer.Document.Blocks.Add(paragraph);
            }
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            rtb_Customer.ScrollToEnd();
            txt_UserInput.Clear();
        }
    }
}



/*using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IConnection connection;
        private IModel channel;
        public MainWindow()
        {
            InitializeComponent();

            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);
            var queueName = channel.QueueDeclare().QueueName;

            string routingKey = "info.client";
            channel.QueueBind(queue: queueName, exchange: "topic_logs", routingKey: routingKey);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);

                // Update the appropriate RichTextBox based on your requirements.
                Application.Current.Dispatcher.Invoke(() =>
                {
                    rtb_Customer.AppendText($"{DateTime.Now:HH:mm:ss} - {message}\n");
                });
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }

        private void OnTextBoxKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run(txt_UserInput.Text));
                rtb_Customer.Document.Blocks.Add(paragraph);
            }
        }

        *//*private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            rtb_Customer.ScrollToEnd();
            txt_UserInput.Clear();
        }*//*

        protected override void OnClosing(CancelEventArgs e)
        {
            // Clean up resources when the window is closed.
            channel.Close();
            connection.Close();

            base.OnClosing(e);
        }
    }
}
*/