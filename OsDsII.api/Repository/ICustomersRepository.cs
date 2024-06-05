using OsDsII.api.Models;

namespace OsDsII.api.Repository
{
    public interface ICustomersRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> GetByIdAsync(int id);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer> FindUserByEmailAsync(string email);
        Task DeleteCustomerAsync(int id);
        Task UpdateCustomerAsync(Customer customer);
        Task AddCustomerAsync(Customer customer);
        void GetAllAsync();
    }
}
