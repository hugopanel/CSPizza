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
            if (e.Key == Key.Enter) {
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

        private void txt_UserInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
