using System;
using System.Collections.Generic;
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
using WpfApp1.Modules;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for WorkerView.xaml
    /// </summary>
    public partial class WorkerView : UserControl
    {
        public WorkerView()
        {
            InitializeComponent();
            WorkerDataGrid.ItemsSource = Pizzeria.Clerks;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void BtnShowClerks_Click(object sender, RoutedEventArgs e)
        {
            WorkerDataGrid.ItemsSource = Pizzeria.Clerks;
        }

        private void BtnShowDeliveryMen_Click(object sender, RoutedEventArgs e)
        {
            WorkerDataGrid.ItemsSource = Pizzeria.DeliveryMen;
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
            WorkerDataGrid.ItemsSource = Pizzeria.Customers;
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
                DataGridCell cell = GetCell(WorkerDataGrid, e.Row.GetIndex(), e.Column.DisplayIndex);
                TextBox textBox = e.EditingElement as TextBox;
                if (cell != null && textBox != null)
                {
                    string editedValue = textBox.Text;

                    if (WorkerDataGrid.SelectedItem is Worker selectedWorker)
                    {
                        if (e.Column.Header.ToString() == "Name")
                        {
                            selectedWorker.Name = editedValue;
                            selectedWorker.OnPropertyChanged("CustomerInfo");
                        }
                        else if (e.Column.Header.ToString() == "Address")
                        {
                            selectedWorker.Address = editedValue;
                            selectedWorker.OnPropertyChanged("CustomerInfo");
                        }
                        else if (e.Column.Header.ToString() == "Number of Orders")
                        {
                            selectedWorker.NbOrders = int.Parse(editedValue);
                            selectedWorker.OnPropertyChanged("CustomerInfo");
                        }
                    }
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton)
            {
                if (deleteButton.Tag is Clerk clerkToDelete)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this clerk?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        Pizzeria.Clerks.Remove(clerkToDelete);
                    }
                }

                if (deleteButton.Tag is DeliveryMan deliverymanToDelete)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this Delivery Man?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        Pizzeria.DeliveryMen.Remove(deliverymanToDelete);
                    }
                }
            }

        }
    }
}
