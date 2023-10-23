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
    /// Interaction logic for WorkerView.xaml
    /// </summary>
    public partial class WorkerView : UserControl
    {
        public ObservableCollection<WorkerPers> Workers { get; set; }
        public WorkerView()
        {
            InitializeComponent();

            Workers = new ObservableCollection<WorkerPers>
            {
                new WorkerPers { IDWork = 1, FirstNameWork = "John", LastNameWork = "Doe", PhoneWork = "555-123-4567", AddressWork = "123 Main St" },
                new WorkerPers { IDWork = 2, FirstNameWork = "Jane", LastNameWork = "Smith", PhoneWork = "555-987-6543", AddressWork = "456 Elm St" }
                // Add more rows as needed
            };

            // Set the DataGrid's ItemsSource to your collection
            WorkerDataGrid.ItemsSource = Workers;
        }


        public class WorkerPers
        {
            public int IDWork { get; set; }
            public string FirstNameWork { get; set; }
            public string LastNameWork { get; set; }
            public string PhoneWork { get; set; }
            public string AddressWork { get; set; }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
