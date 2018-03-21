using System;
using System.IO;
using Catalog.Data;
using Catalog.Droid;
using Catalog.Infrastructure.Utils;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqLite))]
namespace Catalog.Droid
{
    public class SqLite : Disposabled, ISqLite
    {
        private const string DefaultDatabaseName = "Catalog.db3";
        private static SQLiteConnection _connection;
        private readonly IFileHelper _fileHelper;

        public SqLite()
        {
            _fileHelper = DependencyService.Get<IFileHelper>();

            DatabaseName = DefaultDatabaseName;
            DatabasePath = _fileHelper.GetLocalFilePath(DatabaseName);
        }

        public string DatabasePath { get; }

        public string DatabaseName { get; }

        public SQLiteConnection Connection
        {
            get
            {
                ThrowIfDisposed();

                return _connection ?? (_connection = new SQLiteConnection(DatabasePath));
            }
        }

        public void CloseConnection()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }

            //GC.Collect();
            //GC.WaitForPendingFinalizers();
        }

        public void DropDatabase()
        {
            try
            {
                CloseConnection();
            }
            finally
            {
                if (File.Exists(DatabasePath))
                {
                    File.Delete(DatabasePath);
                }
            }
        }

        protected override void DisposeManagedResources()
        {
            CloseConnection();
        }
    }
}