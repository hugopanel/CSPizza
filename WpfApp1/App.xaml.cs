﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using RibbitMQ;
using WpfApp1.Models;
using WpfApp1.Modules;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static RibbitMQ<MessageType> RibbitMq = new();
        internal static JsonSerializerOptions jsonOptions = new()
        {
            IncludeFields = true,
            PropertyNameCaseInsensitive = true,
            Converters = { new DateOnlyConverter() }
        };

        public App()
        {
            FileModule.LoadCustomers();
            FileModule.LoadOrders();
            FileModule.LoadWorkforce();
        }
    }

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
