using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        List<Pizza> Pizzas = new List<Pizza>();
        List<Drink> Drinks = new List<Drink>();
        List<PizzaType> PizzaTypes = Pizzeria.PizzasMenu.Select(item => item.PizzaType).Distinct().ToList();
        List<PizzaSize> PizzaSizes = Pizzeria.PizzasMenu.Select(item => item.PizzaSize).Distinct().ToList();
        List<int> PizzaPreparationTimes = Pizzeria.PizzasMenu.Select(item => item.PreparationTime).Distinct().ToList();
        List<string> PizzaNames {  get; set; }
        List<string> PizzaSizeNames {  get; set; }
        private Pizza editedPizza;

        List<Drink> drinkDataList;
        public HomeView()
        {
            InitializeComponent();

            ComboPizzaName.ItemsSource = Pizzeria.PizzasMenu.Select(item => item.PizzaType.Name).Distinct();
            ComboPizzaSize.ItemsSource = Pizzeria.PizzasMenu.Select(item => item.PizzaSize.Name).Distinct();

            foreach(string name in Pizzeria.PizzasMenu.Select(item => item.PizzaType.Name).Distinct())
            {
                Console.WriteLine(name);
            }

            drinkDataList = Pizzeria.DrinksMenu.Select(item => item).ToList();
            ComboDrinkName.ItemsSource = Pizzeria.DrinksMenu.Select(item => item.Name).Distinct();
            ComboDrinkSize.ItemsSource = Pizzeria.DrinksMenu.Select(item => item.Size).Distinct();


            DataContext = this;
            DgPizzas.ItemsSource = Pizzas;
            DgDrinks.ItemsSource = Drinks;
            

            OrderDataGrid.ItemsSource = Pizzeria.Orders;
        }

        public void UpdateDataGridPizza()
        {
            DgPizzas.Items.Refresh();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DgPizzas_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit && e.Column is DataGridComboBoxColumn comboBoxColumn)
            {
                var editedItem = e.Row.Item as Pizza;
                if (editedItem != null)
                {
                    if (comboBoxColumn.Header.ToString() == "Name")
                    {
                        var comboBox = e.EditingElement as ComboBox;
                        if (comboBox != null)
                        {
                            var newValue = comboBox.SelectedValue as string;
                            if (editedItem.Name != newValue)
                            {
                                Console.WriteLine("Old value :" + editedItem.PizzaType.Name);
                                editedItem.PizzaType= PizzaTypes.FirstOrDefault(item => item.Name == newValue);
                                Console.WriteLine("New value :" + editedItem.PizzaType.Name);
                                editedItem.UpdatePrice();
                            }
                        }
                    }
                    else if (comboBoxColumn.Header.ToString() == "Size")
                    {
                        var comboBox = e.EditingElement as ComboBox;
                        if (comboBox != null)
                        {
                            var newValue = comboBox.SelectedValue as string;
                            if (editedItem.PizzaSize.Name != newValue)
                            {
                                // Size value has changed, update the PizzaSize object
                                editedItem.PizzaSize.Name = newValue;
                                Console.WriteLine("New value :" + editedItem.PizzaType.Name);
                                // Update other properties of Pizza if needed
                                // ...

                                // Refresh the DataGrid to update the display
                                // DgPizzas.Items.Refresh();
                            }
                        }
                    }
                }
            }
            // UpdateDataGridPizza();
        }

        // permet d'ajouter une pizza à la commande 
        public void BtnAddPizza_Click(object sender, RoutedEventArgs e)
        {
            Pizza newPizza = new Pizza(PizzaSizes[0].Name + " " + PizzaTypes[0].Name, PizzaPreparationTimes[0], PizzaTypes[0], PizzaSizes[0]);
            Pizzas.Add(newPizza);
            DgPizzas.Items.Refresh();
            foreach (Pizza pizza in Pizzas)
            {
                Console.WriteLine(pizza.PizzaType.Name);
                Console.WriteLine(pizza.PizzaSize.Name);
            }
        }
        public void BtnAddDrink_Click(object sender, RoutedEventArgs e)
        {
            Drink newDrink = new Drink("Premium Tap Water",7,"Small");
            Drinks.Add(newDrink);
            DgDrinks.Items.Refresh();
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

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Ignore case when deserializing properties.
                };

                // Load the JSON data into a list of order objects.
                var orders = JsonSerializer.Deserialize<List<Order>>(File.ReadAllText("orders.json"), options);

                // Prompt the user for the order index using a MessageBox.
                string input = Microsoft.VisualBasic.Interaction.InputBox("Enter the order index:", "Order Selection", "0");

                if (int.TryParse(input, out int orderIndexToLoad) && orderIndexToLoad >= 0 && orderIndexToLoad < orders.Count)
                {
                    var orderToDisplay = orders[orderIndexToLoad];

                    // Bind the order data to your XAML controls as needed.
                    // DgPizzas.ItemsSource = orderToDisplay.Pizzas;
                    Pizzas = orderToDisplay.Pizzas;
                    foreach(Pizza pizza in Pizzas)
                    {
                        Console.WriteLine(pizza.PizzaType.Name);
                        Console.WriteLine(pizza.PizzaSize.Name);
                    }
                    DgPizzas.ItemsSource = Pizzas;
                    DgPizzas.Items.Refresh();
                    // Bind other data like drinks, customer info, and total price to respective controls.

                    // Optionally, you can update the total price label:
                    LblTotalPriceValue.Content = orderToDisplay.Price;
                }
                else
                {
                    MessageBox.Show("Invalid order index. Please choose a valid order to display.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
