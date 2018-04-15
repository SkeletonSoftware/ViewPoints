using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewPoints.Backend.DataAccess.Database
{
    public static class DatabaseConfig
    {
        private static SQLiteAsyncConnection database;

        public static string DatabaseName => "database1.db3";

        public static SQLiteAsyncConnection Database
        {
            get
            {
                return database;
            }
            set
            {
                if (database == null)
                {
                    database = value;
                }
            }
        }

    }
}
