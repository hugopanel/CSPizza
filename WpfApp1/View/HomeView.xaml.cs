using Microsoft.Win32;
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
using WpfApp1.Models;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    // permet de récupérer touts les elements initialisés dans MainWindow
    ///public MainView mainWindow = (MainView)System.Windows.Application.Current.MainWindow;
    ///public double TotalPrice => mainWindow.clerk.currentCommand.Price;
    public partial class HomeView : UserControl
    {

        public HomeView()
        {
            InitializeComponent();

            OrderDataGrid.ItemsSource = Pizzeria.Orders;
        }

        public void UpdateDataGrid()
        {
            OrderDataGrid.ItemsSource = Pizzeria.Orders;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // permet d'ajouter une pizza à la commande 
        public void BtnAddPizza_Click(object sender, RoutedEventArgs e)
        {
            /*mainWindow.clerk.AddPizza();
            UpdateTotalPrice();

            // permet de mettre à jour le prix total de la commande lorsque chaque nouvelle pizza est modifiée
            foreach (var pizza in mainWindow.clerk.currentCommand.Pizzas)
            {
                pizza.PriceChanged += UpdateTotalPrice;
            }
            foreach (var drink in mainWindow.clerk.currentCommand.Drinks)
            {
                drink.PriceChanged += UpdateTotalPrice;
            }*/
        }
        public void BtnAddDrink_Click(object sender, RoutedEventArgs e)
        {
            /*mainWindow.clerk.AddDrink();
            UpdateTotalPrice();
            foreach (var pizza in mainWindow.clerk.currentCommand.Pizzas)
            {
                pizza.PriceChanged += UpdateTotalPrice;
            }
            foreach (var drink in mainWindow.clerk.currentCommand.Drinks)
            {
                drink.PriceChanged += UpdateTotalPrice;
            }*/
        }

        // permet de supprimer une pizza de la commande
        public void BtnRemovePizza_Click(object sender, RoutedEventArgs e)
        {
            /*if (sender is System.Windows.Controls.Button button)
            {
                if (button.DataContext is PizzaViewModel pizza)
                {
                    mainWindow.clerk.RemovePizza(pizza);
                }
            }
            UpdateTotalPrice();*/
        }
        public void BtnRemoveDrink_Click(object sender, RoutedEventArgs e)
        {
            /*if (sender is System.Windows.Controls.Button button)
            {
                if (button.DataContext is DrinkViewModel addition)
                {
                    mainWindow.clerk.RemoveDrink(addition);
                }
            }
            UpdateTotalPrice();*/
        }


        // permet de mettre à jour le prix total de la commande sur l'affichage
        private void UpdateTotalPrice()
        {
            /* LblTotalPriceValue.Content = TotalPrice;*/
        }

        // permet de passer à la page suivante
        public void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            /*mainWindow.clerk.LaunchCurentCommand();
            DgPizzas.ItemsSource = mainWindow.clerk.currentCommand.Pizzas;
            DgDrinks.ItemsSource = mainWindow.clerk.currentCommand.Drinks;
            //mainWindow.NaviateToPage(0);*/
        }

        // permet de passer à la page de connection
        public void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            /*mainWindow.NaviateToPage(0);*/
        }

        public void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Fichiers JSON (*.json)|*.json|Tous les fichiers (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            /*if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // L'utilisateur a choisi un fichier
                string nomFichier = openFileDialog.FileName;
                mainWindow.clerk.LoadCommandFile(nomFichier);
                DgPizzas.ItemsSource = mainWindow.clerk.currentCommand.Pizzas;
                UpdateTotalPrice();

            }
            foreach (var pizza in mainWindow.clerk.currentCommand.Pizzas)
            {
                pizza.PriceChanged += UpdateTotalPrice;
            }
            foreach (var drink in mainWindow.clerk.currentCommand.Drinks)
            {
                drink.PriceChanged += UpdateTotalPrice;
            }*/
        }
    }
}
