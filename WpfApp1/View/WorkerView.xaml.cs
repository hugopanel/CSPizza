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
