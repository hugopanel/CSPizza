using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for ClientView.xaml
    /// </summary>
    public partial class ClientView : UserControl
    {
        public ClientView()
        {
            InitializeComponent();
            ClientDataGrid.ItemsSource = Pizzeria.Customers;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }

        public void UpdateDataGrid()
        {
            ClientDataGrid.ItemsSource = Pizzeria.Customers;
        }

        private DataGridCell GetCell(DataGrid dataGrid, int rowIndex, int columnIndex)
        {
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            if (row != null)
            {
                DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                if (presenter != null)
                {
                    DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
                    return cell;
                }
            }
            return null;
        }


        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                // Get the edited cell and its value
                DataGridCell cell = GetCell(ClientDataGrid, e.Row.GetIndex(), e.Column.DisplayIndex);
                TextBox textBox = e.EditingElement as TextBox;
                if (cell != null && textBox != null)
                {
                    string editedValue = textBox.Text;

                    // Access the Customer object
                    if (ClientDataGrid.SelectedItem is Customer selectedCustomer)
                    {
                        if (e.Column.Header.ToString() == "First Name")
                        {
                            // Update the FirstName property of the CustomerInfo
                            selectedCustomer.CustomerInfo.FirstName = editedValue;
                            selectedCustomer.OnPropertyChanged("CustomerInfo");
                        }
                        else if (e.Column.Header.ToString() == "Last Name")
                        {
                            selectedCustomer.CustomerInfo.Surname = editedValue;
                            selectedCustomer.OnPropertyChanged("CustomerInfo");
                        }
                        else if (e.Column.Header.ToString() == "Phone")
                        {
                            selectedCustomer.CustomerInfo.TelephoneNumber = editedValue;
                            selectedCustomer.OnPropertyChanged("CustomerInfo");
                        }
                    }
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton)
            {
                if (deleteButton.Tag is Customer customerToDelete)
                {
                    // Prompt the user for confirmation or perform the delete action immediately
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        // Delete the customer
                        Pizzeria.Customers.Remove(customerToDelete);
                        ClientDataGrid.ItemsSource = null;
                        ClientDataGrid.ItemsSource = Pizzeria.Customers;
                    }
                }
            }
        }
    }
}
