using Catalog.DataLayer;
using Catalog.Infrastructure.Utils;
using Catalog.Models;

namespace Catalog.DataAccessLayer
{
    public class UnitOfWork : Disposabled
    {
        private readonly AppDbContext _dbContext;

        private GenericRepository<Customer> _customerRepository;
        private GenericRepository<Employee> _employeeRepository;
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<Item> _testRepository;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public GenericRepository<Order> OrdeRepository => 
            _orderRepository ?? (_orderRepository = new GenericRepository<Order>(_dbContext));

        public GenericRepository<Customer> CustomerRepository =>
            _customerRepository ?? (_customerRepository = new GenericRepository<Customer>(_dbContext));

        public GenericRepository<Product> ProductRepository =>
            _productRepository ?? (_productRepository = new GenericRepository<Product>(_dbContext));

        public GenericRepository<Employee> EmployeeRepository =>
            _employeeRepository ?? (_employeeRepository = new GenericRepository<Employee>(_dbContext));

        public GenericRepository<Item> TestRepository => 
            _testRepository ?? (_testRepository = new GenericRepository<Item>(_dbContext));

        public void BeginTransaction()
        {
            _dbContext.Connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Connection.Commit();
        }

        public void RollBackTransaction()
        {
            _dbContext.Connection.Rollback();
        }

        protected override void DisposeManagedResources()
        {
            _dbContext.Dispose();
        }
    }
}