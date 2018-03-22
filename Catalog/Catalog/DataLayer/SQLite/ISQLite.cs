using System;
using SQLite;

namespace Catalog.DataLayer.SQLite
{
    public interface ISQLite : IDisposable
    {
        string DatabaseName { get; }

        string DatabasePath { get; }

        SQLiteConnection Connection { get; }

        void CloseConnection();

        void DropDatabase();
    }
}
