using Catalog.BusinessLayer.Entities;
using Catalog.DataLayer.SQLite;
using Catalog.Infrastructure.Utils;
using Catalog.Models;
using SQLite;

namespace Catalog.DataLayer
{
    public class AppDbContext : Disposabled
    {
        private readonly ISQLite _sqLite;

        public SQLiteConnection Connection => _sqLite.Connection;

        public AppDbContext(ISQLite sqLite)
        {
            this._sqLite = sqLite;
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