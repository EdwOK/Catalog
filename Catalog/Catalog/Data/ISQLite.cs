using System;
using SQLite;

namespace Catalog.Data
{
    public interface ISqLite : IDisposable
    {
        string DatabaseName { get; }

        string DatabasePath { get; }

        SQLiteConnection Connection { get; }

        void CloseConnection();

        void DropDatabase();
    }
}
