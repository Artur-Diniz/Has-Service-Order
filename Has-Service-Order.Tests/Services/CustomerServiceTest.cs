using AutoMapper;
using Moq;
using OsDsII.api.Dtos;
using OsDsII.api.Repository;
using OsDsII.api.Services.Customers;

namespace CalculadoraSalario.Tests
{
    public class CustomersServiceTests
    {
        private readonly Mock<ICustomersRepository> _mockCustomersRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CustomersService _service;
        private List<CustomerDto> _lista;

        public CustomersServiceTests()
        {
            _mockCustomersRepository = new Mock<ICustomersRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new CustomersService(_mockCustomersRepository.Object, _mockMapper.Object);
            _lista = new List<CustomerDto>();

        }

        [Fact]
        public async void Should_Return_A_List_Of_Customers()
        {
            // gera uma lista estática de customersDto

            List<CustomerDto> CustomerDto = new List<CustomerDto>
            {
                new CustomerDto("João", "seila","121221", null)
                

            };

            _mockCustomersRepository.Setup(repository => repository.GetAllAsync()).ReturnsAsync(CustomerDto);
            var result = await _service.GetAllCustomerAsync();
            Assert.Equal(_lista, result);
        }
    }
}
