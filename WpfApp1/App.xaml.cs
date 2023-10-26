using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using RibbitMQ;
using WpfApp1.Models;
using WpfApp1.Modules;
using WpfApp1.View;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Shared RibbitMq instance, to be use throughout the app.
        /// </summary>
        internal static RibbitMQ<MessageType> RibbitMq = new();

        /// <summary>
        /// Json Options for the FileModule. 
        /// </summary>
        internal static JsonSerializerOptions jsonOptions = new()
        {
            IncludeFields = true,
            PropertyNameCaseInsensitive = true,
            Converters = { new DateOnlyConverter() }
        };

        /// <summary>
        /// The Customer that logged in to the application.
        /// </summary>
        internal static Customer? CurrentCustomer;

        public App()
        {
            // Load the JSON files as soon as the app starts...
            FileModule.LoadCustomers();
            FileModule.LoadOrders();
            FileModule.LoadWorkforce();
            FileModule.LoadMenu();

            foreach (var clerk in Pizzeria.Clerks)
            {
                clerk.CheckIn();
            }

            foreach (var cook in Pizzeria.Cooks)
            {
                cook.CheckIn();
            }

            foreach (var deliveryMan in Pizzeria.DeliveryMen)
            {
                deliveryMan.CheckIn();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Save the JSON files when the app closes...
            FileModule.SaveCustomers();
            FileModule.SaveOrders();
            FileModule.SaveWorkforce();

            // Resume normal closing procedure
            base.OnExit(e);
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            var dialog = new WelcomeView();
            dialog.ShowDialog();
        }
    }

    /// <summary>
    /// Converts a JSON value to DateOnly when required. Used in App.jsonOptions.
    /// </summary>
    public class DateOnlyConverter : JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateOnly.TryParse(reader.GetString(), out DateOnly result))
                {
                    return result;
                }
            }

            return default; // Default value if parsing fails
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
