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
using RibbitMQ;
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
        public ObservableCollection<Ordering> Orders { get; set; }
        public HomeView()
        {
            InitializeComponent();

            // Create sample data
            Orders = new ObservableCollection<Ordering>
            {
                new Ordering { ID = 1, Prize = 21, Status = "In Progress" },
                new Ordering { ID = 2, Prize = 14, Status = "Delivered" }

            };

            // Set the DataGrid's ItemsSource to your collection
            OrderDataGrid.ItemsSource = Orders;

            /* // permet de mettre à jour le prix total de la commande lorsque chaque pizza est modifiée
             foreach (var pizza in mainWindow.clerk.currentCommand.Pizzas)
             {
                 pizza.PriceChanged += UpdateTotalPrice;
             }
             foreach (var drink in mainWindow.clerk.currentCommand.Drinks)
             {
                 drink.PriceChanged += UpdateTotalPrice;
             }

             // permet de mettre à jour le prix total de la commande lorsque la page est chargée
             LblTotalPriceValue.Content = TotalPrice;
             DgPizzas.ItemsSource = mainWindow.clerk.currentCommand.Pizzas;
             DgDrinks.ItemsSource = mainWindow.clerk.currentCommand.Drinks;*/
        }

        public class Ordering
        {
            public int ID { get; set; }
            public int Prize { get; set; }
            public string Status { get; set; }

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

            // TODO: Appeler la fonction pour envoyer la commande au Clerk
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

        /// <summary>
        /// Envoie la commande au Clerk.
        /// </summary>
        private void SubmitOrderToClerk()
        {
            // TODO: Get the Order
            // In the meantime, we create a fake order:
            List<Pizza> pizzas = new();
            List<Drink> drinks = new();
            Order order = new(App.CurrentCustomer!, pizzas, drinks);
            
            App.RibbitMq.Subscribe(MessageType.ConfirmationSubmitOrder, HandleConfirmationSubmitOrder);
            App.RibbitMq.Send(new Message {MessageType = MessageType.SubmitOrder, Content = order});
        }

        /// <summary>
        /// Handles the ConfirmationSubmitOrder message event sent from the Clerk to confirm that our Order has been received.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task HandleConfirmationSubmitOrder(IMessage<MessageType> message)
        {
            App.RibbitMq.Unsubscribe(MessageType.ConfirmationSubmitOrder, HandleConfirmationSubmitOrder);

            // TODO: Add the Order to the table or change its status in the table from Taking to Taken

            // Now we can subscribe to events related to our Order
            App.RibbitMq.Subscribe(MessageType.ClerkOrderPreparing, HandleClerkOrderPreparing);
            App.RibbitMq.Subscribe(MessageType.ClerkOrderWaiting, HandleClerkOrderWaiting);
            App.RibbitMq.Subscribe(MessageType.ClerkOrderDelivering, HandleClerkOrderDelivering);
            App.RibbitMq.Subscribe(MessageType.ClerkOrderDelivered, HandleClerkOrderDelivered);
            App.RibbitMq.Subscribe(MessageType.ClerkOrderDropped, HandleClerkOrderDropped);
            App.RibbitMq.Subscribe(MessageType.ClerkOrderDelivered, HandleClerkOrderDelivered);
            App.RibbitMq.Subscribe(MessageType.ClerkOrderDone, HandleClerkOrderDone);
        }

        /// <summary>
        /// For when the Order is being prepared by a Cook.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task HandleClerkOrderPreparing(IMessage<MessageType> message)
        {
            App.RibbitMq.Unsubscribe(MessageType.ClerkOrderPreparing, HandleClerkOrderPreparing);

            throw new NotImplementedException();
        }

        /// <summary>
        /// For when the Order is waiting to be delivered.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task HandleClerkOrderWaiting(IMessage<MessageType> message)
        {
            App.RibbitMq.Unsubscribe(MessageType.ClerkOrderWaiting, HandleClerkOrderWaiting);

            throw new NotImplementedException();
        }

        /// <summary>
        /// For when the Order is being delivered by a DeliveryMan.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task HandleClerkOrderDelivering(IMessage<MessageType> message)
        {
            App.RibbitMq.Unsubscribe(MessageType.ClerkOrderDelivering, HandleClerkOrderDelivering);

            throw new NotImplementedException();
        }

        /// <summary>
        /// For when the Order has been dropped by the DeliveryMan. Possibly because of a wrong address.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task HandleClerkOrderDropped(IMessage<MessageType> message)
        {
            App.RibbitMq.Unsubscribe(MessageType.ClerkOrderDropped, HandleClerkOrderDropped);

            throw new NotImplementedException();
        }

        /// <summary>
        /// For when the Order has been delivered.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task HandleClerkOrderDelivered(IMessage<MessageType> message)
        {
            App.RibbitMq.Unsubscribe(MessageType.ClerkOrderDelivered, HandleClerkOrderDelivered);

            throw new NotImplementedException();
        }

        /// <summary>
        /// For when the DeliveryMan returned to the Pizzeria and the money has been cashed in. The order is complete.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task HandleClerkOrderDone(IMessage<MessageType> message)
        {
            App.RibbitMq.Unsubscribe(MessageType.ClerkOrderDone, HandleClerkOrderDone);

            throw new NotImplementedException();
        }
    }
}
