using System;
using System.Collections.Generic;

namespace FacadePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var orderPage = new OrderPage(new CustomerRepository(), new TaxCalculationService(), new OrderRepository(), new ShippingService());
            orderPage.SubmitButtonClick();
        }
    }

    public class OrderPage
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITaxCalculationService _taxCalculationService;
        private readonly IOrderRepository _orderRepository;
        private readonly IShippingService _shippingService;

        public OrderPage(ICustomerRepository customerRepository, ITaxCalculationService taxCalculationService,
            IOrderRepository orderRepository, IShippingService shippingService)
        {
            _customerRepository = customerRepository;
            _taxCalculationService = taxCalculationService;
            _orderRepository = orderRepository;
            _shippingService = shippingService;
        }

        private Customer GetCustomerSubmittedInfo()
        {
            return new Customer
            {
                FirstName = "Bruno",
                LastName = "Barrette",
                Province = Province.Quebec,
                Adress = "1200 St-Martin Ouest"
            };
        }

        private Order GetOrderSubmittedInfo()
        {
            return new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Name = "Test",
                        Quantity = 1,
                        Price = 10m
                    },
                    new OrderItem
                    {
                        Name = "Other Test",
                        Quantity = 2,
                        Price = 10m
                    }
                }
            };
        }

        public void SubmitButtonClick()
        {
            var customer = GetCustomerSubmittedInfo();
            var order = GetOrderSubmittedInfo();

            if (customer.Id == null)
            {
                ICustomerRepository customerRepo = new CustomerRepository();
                customer.Id = customerRepo.CreateCustomer(customer);
            }
            order.CustomerId = customer.Id.Value;

            ITaxCalculationService taxCalculationService = new TaxCalculationService();
            var taxRate = taxCalculationService.GetTaxRate(customer.Province);
            order.Items.ForEach(item => item.Taxes = item.Quantity * item.Price * taxRate);

            IOrderRepository orderRepository = new OrderRepository();
            orderRepository.SaveOrder(order);

            IShippingService shippingService = new ShippingService();
            var trackingNumber = shippingService.ShipOrder(order.OrderNumber);

            Console.WriteLine($"Thank you for your order. Your tracking number is {trackingNumber}");
            Console.ReadKey();
        }
    }

    public interface IOrderFacade
    {
        string SubmitOrder(Customer customer, Order order);
    }

    public class OrderFacade : IOrderFacade
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITaxCalculationService _taxCalculationService;
        private readonly IOrderRepository _orderRepository;
        private readonly IShippingService _shippingService;

        public OrderFacade(ICustomerRepository customerRepository, ITaxCalculationService taxCalculationService,
            IOrderRepository orderRepository, IShippingService shippingService)
        {
            _customerRepository = customerRepository;
            _taxCalculationService = taxCalculationService;
            _orderRepository = orderRepository;
            _shippingService = shippingService;
        }

        public string SubmitOrder(Customer customer, Order order)
        {
            if (customer.Id == null)
            {
                customer.Id = _customerRepository.CreateCustomer(customer);
            }
            order.CustomerId = customer.Id.Value;
            
            var taxRate = _taxCalculationService.GetTaxRate(customer.Province);
            order.Items.ForEach(item => item.Taxes = item.Quantity * item.Price * taxRate);

            _orderRepository.SaveOrder(order);
            
            var trackingNumber = _shippingService.ShipOrder(order.OrderNumber);

            return trackingNumber;
        }
    }
}
