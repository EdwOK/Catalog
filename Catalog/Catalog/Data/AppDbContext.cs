using Catalog.Domain.Entities;
using Catalog.Infrastructure.Utils;
using Catalog.Models;
using SQLite;
using Xamarin.Forms;

namespace Catalog.Data
{
    public class AppDbContext : Disposabled
    {
        private readonly ISqLite _sqLite;

        public SQLiteConnection Connection => _sqLite.Connection;

        public AppDbContext()
        {
            _sqLite = DependencyService.Get<ISqLite>();
        }

        public void CreateDatabase()
        {
            CreateTable<Product>();
            CreateTable<Order>();
            CreateTable<Customer>();
            CreateTable<Employee>();
            CreateTable<Item>();
        }

        public void DropDatabase()
        {
            _sqLite.DropDatabase();
        }

        public void CloseDatabase()
        {
            _sqLite.CloseConnection();
        }

        private int CreateTable<TEntity>()
            where TEntity : new()
        {
            return Connection.CreateTable<TEntity>();
        }

        protected override void DisposeManagedResources()
        {
            _sqLite.Dispose();
        }
    }
}