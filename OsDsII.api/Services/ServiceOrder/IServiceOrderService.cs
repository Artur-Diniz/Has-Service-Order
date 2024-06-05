using OsDsII.api.Models;
using OsDsII.api.Services;

namespace OsDsII.api.Services.ServiceOrder
{
    public interface IServiceOrderService
    {

        public Task CancelServiceOrderAsync(int id);

        public Task FinishserviceOrderAsync(int id);

        public Task CreateServiceOrderAsync(Models.ServiceOrder serviceOrder, Customer customer);

        public Task GetServiceOrderById(int id);
    }
}
