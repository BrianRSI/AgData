using AgDataAPI.Models;
using static AgDataAPI.Services.CustomerService;

namespace AgDataAPI.Services
{
    public interface ICustomerService
    {
        Task<customer> AddCustomerAsync(customer customer, address address);
        Task<CustomerData> GetCustomerDataAsync();
    }
}
