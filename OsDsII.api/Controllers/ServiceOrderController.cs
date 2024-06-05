using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsDsII.api.Data;
using OsDsII.api.Models;
using OsDsII.api.Repository;
using OsDsII.api.Services.ServiceOrderService;

namespace OsDsII.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceOrdersController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IServiceOrderRepository _serviceOrderRepository;
        private readonly ServiceOrderService _serviceOrder;

        public ServiceOrdersController( IServiceOrderRepository serviceOrderRepository, DataContext dataContext, ServiceOrderService serviceOrder)
        {
            _serviceOrderRepository = serviceOrderRepository;
            _dataContext = dataContext;
            _serviceOrder = serviceOrder;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAllServiceOrderAsync()
        {
            try
            {
                List<ServiceOrder> serviceOrders = await _serviceOrderRepository.GetAllAsync();
                return Ok(serviceOrders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]



        public async Task<IActionResult> GetServiceOrderById(int id)
        {
            try
            {
                ServiceOrder serviceOrder = await _serviceOrderRepository.GetByIdAsync(id);
               
                _serviceOrder.GetServiceOrderById(id);

                return Ok(serviceOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CreateServiceOrderAsync(ServiceOrder serviceOrder)
        {
            try
            {
                

                Customer customer = await _dataContext.Customers.FirstOrDefaultAsync(c => serviceOrder.Customer.Id == c.Id);

                _serviceOrder.CreateServiceOrderAsync(serviceOrder, customer);

                await _serviceOrderRepository.AddAsync(serviceOrder);
                return Created(nameof(customer), customer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPut("{id}/status/finish")]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]


        public async Task<IActionResult> FinishServiceOrderAsync(int id)
        {
            try
            {
                ServiceOrder serviceOrder = await _serviceOrderRepository.GetByIdAsync(id);

                _serviceOrder.FinishserviceOrderAsync(id);

                serviceOrder.FinishOS();
                _serviceOrderRepository.FinishAsync(serviceOrder);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}/status/cancel")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CancelServiceOrder(int id)
        {
            try
            {

                ServiceOrder serviceOrder = await _serviceOrderRepository.GetByIdAsync(id);
                
                _serviceOrder.CancelServiceOrderAsync(id);

                serviceOrder.Cancel();
               _serviceOrderRepository.CancelAsync(serviceOrder);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}