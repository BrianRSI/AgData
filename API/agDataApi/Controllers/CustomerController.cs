using AgDataAPI.Models;
using AgDataAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgDataAPI.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("create-customer")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerEntity customerEntity)
        {
            try
            {

            // Log the incoming request (for debugging purposes)
            Console.WriteLine($"Received Customer Data: {customerEntity.FirstName}, {customerEntity.LastName}, {customerEntity.Address1}, {customerEntity.City}");

            if (string.IsNullOrEmpty(customerEntity.LastName))
            {
                // again overkill shouldnt happen - its caught on the front-end
                return BadRequest("Last Name is a required entry!");
            }

            var customer = new customer
            {
                FirstName = customerEntity.FirstName,
                LastName = customerEntity.LastName
            };

            var address = new address
            {
                Address1 = customerEntity.Address1,
                Address2 = customerEntity.Address2,
                City = customerEntity.City,
                State = customerEntity.State,
                Zip = customerEntity.Zip
            };

            var newCustomer = await _customerService.AddCustomerAsync(customer, address);
            return Ok(newCustomer);

            }
            catch (Exception ex)
            {
                // Log the error details
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                // Return 500 Internal Server Error with the error message
                return StatusCode(500, string.Concat("An error occurred while creating the customer - ", ex.Message));
            }
        }

        // GET: Retrieve all customers with their associated addresses
        [HttpGet("get-customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var customerData = await _customerService.GetCustomerDataAsync();
            
            if (!customerData.Customers.Any())
            {
                return NotFound("No customers found.");
            }

            // Return the data grouped in the required structure
            var customersWithAddresses = new
            {
                customers = customerData.Customers,  // Return customers as they are
                addresses = customerData.Addresses   // Return addresses as they are
            };            

            return Ok(customersWithAddresses);
        }
    }

    public class CustomerEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}