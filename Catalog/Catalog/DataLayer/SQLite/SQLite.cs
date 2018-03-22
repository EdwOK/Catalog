using System;
using System.IO;
using Catalog.Infrastructure.Utils;
using SQLite;
using Xamarin.Forms;

namespace Catalog.DataLayer.SQLite
{
    public class SQLite : Disposabled, ISQLite
    {
        private const string DefaultDatabaseName = "Catalog.db3";
        private static SQLiteConnection _connection;

        public SQLite()
        {
            DatabaseName = DefaultDatabaseName;
        }

        public SQLite(string dataBaseName)
        {
            DatabaseName = dataBaseName;
        }

        public string DatabasePath
        {
            get
            {
                string libraryPath = "";

                if (Device.RuntimePlatform == Device.Android)
                {
                    libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                }
                else if (Device.RuntimePlatform == Device.iOS)
                {
                    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    libraryPath = Path.Combine(documentsPath, "..", "Library", "Database");

                    if (!Directory.Exists(libraryPath))
                    {
                        Directory.CreateDirectory(libraryPath);
                    }
                }

                var path = Path.Combine(libraryPath, DefaultDatabaseName);
                return path;
            }
        }

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
