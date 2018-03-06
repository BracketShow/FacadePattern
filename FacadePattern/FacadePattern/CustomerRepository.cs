namespace FacadePattern
{
    public class CustomerRepository : ICustomerRepository
    {
        public int CreateCustomer(Customer customer)
        {
            return customer.Id ?? 1;
        }
    }
}