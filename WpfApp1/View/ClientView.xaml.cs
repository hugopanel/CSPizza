using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for ClientView.xaml
    /// </summary>
    public partial class ClientView : UserControl
    {
        public ObservableCollection<Person> People { get; set; }
        public ClientView()
        {
            InitializeComponent();

            // Create sample data
            People = new ObservableCollection<Person>
            {
                new Person { ID = 1, FirstName = "John", LastName = "Doe", Phone = "555-123-4567", Address = "123 Main St", Purchases = 5, FirstOrder = new DateTime(2023, 1, 15) },
                new Person { ID = 2, FirstName = "Jane", LastName = "Smith", Phone = "555-987-6543", Address = "456 Elm St", Purchases = 8, FirstOrder = new DateTime(2023, 2, 20) }
                // Add more rows as needed
            };

            // Set the DataGrid's ItemsSource to your collection
            ClientDataGrid.ItemsSource = People;
        }


        public class Person
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public int Purchases { get; set; }
            public DateTime FirstOrder { get; set; }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
