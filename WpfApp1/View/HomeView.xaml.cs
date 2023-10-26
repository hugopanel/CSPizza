﻿using Microsoft.Win32;
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
using WpfApp1.Modules;

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
        Pizza editedPizza;

        List<Drink> drinkDataList;
        public HomeView()
        {
            InitializeComponent();

            ComboPizzaName.ItemsSource = Pizzeria.PizzasMenu.Select(item => item.PizzaType.Name).Distinct();
            ComboPizzaSize.ItemsSource = Pizzeria.PizzasMenu.Select(item => item.PizzaSize.Name).Distinct();


            drinkDataList = Pizzeria.DrinksMenu.Select(item => item).ToList();
            ComboDrinkName.ItemsSource = Pizzeria.DrinksMenu.Select(item => item.Name).Distinct();
            ComboDrinkSize.ItemsSource = Pizzeria.DrinksMenu.Select(item => item.Size).Distinct();


            DataContext = this;
            DgPizzas.ItemsSource = Pizzas;
            DgDrinks.ItemsSource = Drinks;
            

            OrderDataGrid.ItemsSource = Pizzeria.Orders;
        }

        public void UpdateDataGrid()
        {
            DgPizzas.ItemsSource = null;
            DgPizzas.ItemsSource = Pizzas;
        }
        public void UpdateDataGrid1()
        {
            DgDrinks.ItemsSource = null;
            DgDrinks.ItemsSource = Drinks;
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
                                editedItem.PizzaType= PizzaTypes.FirstOrDefault(item => item.Name == newValue);
                                editedItem.UpdatePrice();
                                UpdateTotalPrice();
                                DgPizzas.ItemsSource = null;
                                DgPizzas.ItemsSource = Pizzas;
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
                                editedItem.PizzaSize = PizzaSizes.FirstOrDefault(item => item.Name == newValue);
                                editedItem.UpdatePrice();
                                UpdateTotalPrice();
                                DgPizzas.ItemsSource = null;
                                DgPizzas.ItemsSource = Pizzas;
                            }
                        }
                    }
                }
            }
        }

        private void DgDrinks_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit && e.Column is DataGridComboBoxColumn comboBoxColumn)
            {
                var editedItem = e.Row.Item as Drink;
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
                                editedItem.Name = newValue;
                                editedItem.UpdatePrice();
                                UpdateTotalPrice();
                                DgDrinks.ItemsSource = null;
                                DgDrinks.ItemsSource = Drinks;
                            }
                        }
                    }
                   else if (comboBoxColumn.Header.ToString() == "Size")
                    {
                        var comboBox = e.EditingElement as ComboBox;
                        if (comboBox != null)
                        {
                            var newValue = comboBox.SelectedValue as string;
                            if (editedItem.Size != newValue)
                            {
                                editedItem.Size = newValue;
                                editedItem.UpdatePrice();
                                UpdateTotalPrice();
                                DgDrinks.ItemsSource = null;
                                DgDrinks.ItemsSource = Drinks;
                            }
                        }
                    }
                }
            }
        }

        // permet d'ajouter une pizza à la commande 
        public void BtnAddPizza_Click(object sender, RoutedEventArgs e)
        {
            Pizza newPizza = new Pizza(PizzaSizes[0].Name + " " + PizzaTypes[0].Name, PizzaPreparationTimes[0], PizzaTypes[0], PizzaSizes[0]);
            Pizzas.Add(newPizza);
            UpdateTotalPrice();
            DgPizzas.Items.Refresh();
            foreach (Pizza pizza in Pizzas)
            {
                Console.WriteLine(pizza.PizzaType.Name);
                Console.WriteLine(pizza.PizzaSize.Name);
            }
        }
        public void BtnAddDrink_Click(object sender, RoutedEventArgs e)
        {
            Drink newDrink = new Drink(drinkDataList[0].Name, drinkDataList[0].Price, drinkDataList[0].Size);
            Drinks.Add(newDrink);
            UpdateTotalPrice();
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
        private void BtnRemovePizza_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton)
            {
                if (deleteButton.Tag is Pizza pizzaToDelete)
                {
                    // Prompt the user for confirmation or perform the delete action immediately
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this pizza?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        // Delete the customer
                        Pizzas.Remove(pizzaToDelete);
                        /*DgDrinks.Items.Refresh();*/
                        UpdateTotalPrice();
                        UpdateDataGrid();
                    }
                }
            }
        }
        private void BtnRemoveDrink_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton)
            {
                if (deleteButton.Tag is Drink drinkToDelete)
                {
                    // Prompt the user for confirmation or perform the delete action immediately
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this drink?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        // Delete the customer
                        Drinks.Remove(drinkToDelete);
                        /*DgDrinks.Items.Refresh();*/
                        UpdateTotalPrice();
                        UpdateDataGrid1();
                    }
                }
            }
        }

        // permet de mettre à jour le prix total de la commande sur l'affichage
        private void UpdateTotalPrice()
        {
            float totalPrice = 0;
            foreach (Pizza pizza in Pizzas)
            {
                totalPrice += pizza.Price;
            }
            foreach(Drink drink in Drinks)
            {
                totalPrice += drink.Price;
            }
            LblTotalPriceValue.Content = totalPrice;
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

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filePath = "orders.json"; // Replace with the full path to your JSON file.

                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);

                    var orders = JsonSerializer.Deserialize<List<Order>>(json);

                    // Prompt the user for the order index to delete using a MessageBox.
                    string input = Microsoft.VisualBasic.Interaction.InputBox("Enter the order index to delete:", "Delete Order", "");

                    if (int.TryParse(input, out int orderIndexToDelete) && orderIndexToDelete >= 0 && orderIndexToDelete < orders.Count)
                    {
                        // Remove the order from the list.
                        orders.RemoveAt(orderIndexToDelete);

                        // Serialize the updated list back to JSON.
                        var updatedJson = JsonSerializer.Serialize(orders);

                        // Write the updated JSON data back to the file.
                        File.WriteAllText(filePath, updatedJson);
                        FileModule.LoadOrders();
                        OrderDataGrid.ItemsSource = Pizzeria.Orders;

                        MessageBox.Show("Order deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Invalid order index. Please enter a valid order index to delete.");
                    }
                }
                else
                {
                    MessageBox.Show("The JSON file does not exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
