using AgDataAPI.Models;
using System.Text.Json;
using static AgDataAPI.Services.CustomerService;

namespace AgDataAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly string _dataFilePath;

        public CustomerService()
        {
            // construct the full path to the customers.json file in the Data folder
            _dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "customers.json");
        }

        public async Task<customer> AddCustomerAsync(customer customer, address address)
        {

                try
                {

                    var customerData = await GetCustomerDataAsync();

                // Generate new CustomerId and AddressId
                customer.CustomerId = customerData.Customers.Any() ? customerData.Customers.Max(c => c.CustomerId) + 1 : 1;
                address.AddressId = customerData.Addresses.Any() ? customerData.Addresses.Max(a => a.AddressId) + 1 : 1;
                address.CustomerId = customer.CustomerId;  // Link address to customer using CustomerId

                // add customer and address to the lists
                customerData.Customers.Add(customer);
                customerData.Addresses.Add(address);

                // write the updated customers list to the JSON file
                await File.WriteAllTextAsync(_dataFilePath, JsonSerializer.Serialize(customerData, new JsonSerializerOptions { WriteIndented = true }));

                }
                catch (Exception ex)
                {
                    // Log the error details
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                }

            return customer;
        }

        public async Task<CustomerData> GetCustomerDataAsync()
        {
            if (!File.Exists(_dataFilePath))
            {
                // Create an empty JSON file if it doesn't exist
                return new CustomerData();
            }

            var json = await File.ReadAllTextAsync(_dataFilePath);
            return JsonSerializer.Deserialize<CustomerData>(json) ?? new CustomerData();

        }

        // define a structure to hold both customers and addresses in the JSON file
        public class CustomerData
        {
            public List<customer> Customers { get; set; } = new();
            public List<address> Addresses { get; set; } = new();
        }

    }
}
