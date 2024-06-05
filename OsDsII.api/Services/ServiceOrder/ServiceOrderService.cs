using AutoMapper;
using OsDsII.api.Data;
using OsDsII.api.Models;
using OsDsII.api.Repository;
using OsDsII.api.Services.Exceptions;
using OsDsII.api.Services.ServiceOrder;

namespace OsDsII.api.Services.ServiceOrderService
{
    public class ServiceOrderService
    {
        private readonly IServiceOrderRepository _serviceOrderRepository;
        private readonly IMapper _mapper;
        private readonly ServiceOrderRepository _serviceOrder;

        public ServiceOrderService(ICustomersRepository customersRepository, IMapper mapper, IServiceOrderRepository serviceOrder, ServiceOrderRepository serviceOrderRepository)
        {
            _serviceOrderRepository = serviceOrder;
            _mapper = mapper;
            _serviceOrder = serviceOrderRepository;
        }



        public async Task CancelServiceOrderAsync(int id)
        {

            var serviceOrder = await _serviceOrderRepository.GetByIdAsync(id);
            if (serviceOrder is null)
            {
                throw new NotFoundException("Order not founded");
            }
           
        }

        public async Task FinishserviceOrderAsync(int id)
        {
            var serviceOrder = await _serviceOrderRepository.GetByIdAsync(id);
            if (serviceOrder is null)
            {
                throw new NotFoundException("Order not founded");
            }
        }

        public async Task CreateServiceOrderAsync(Models.ServiceOrder serviceOrder, Customer customer)
        {

            if (serviceOrder is null)
            {
                throw new Exception("Service order cannot be null");
            }


            if (customer is null)
            {
                throw new ConflictException("Customer already exists");
            }

        }

        public async Task GetServiceOrderById(int id)
        {
            var serviceOrder = await _serviceOrderRepository.GetByIdAsync(id);
            if (serviceOrder is null)
            {
                throw new NotFoundException("Order not founded");
            }
        }

    }
}
