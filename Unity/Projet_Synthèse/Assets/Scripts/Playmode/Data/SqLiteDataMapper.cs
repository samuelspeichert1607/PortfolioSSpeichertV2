﻿using System.Data.Common;
using Harmony;

namespace ProjetSynthese
{
    public abstract class SqLiteDataMapper<T> : IDbDataMapper<T> where T : class
    {
        public long GetPrimaryKeyFromConnection(DbConnection connection)
        {
            DbCommand command = connection.CreateCommand();
            command.CommandText = "SELECT last_insert_rowid()";
            return (long) command.ExecuteScalar();
        }

        public abstract T GetObjectFromReader(DbDataReader reader);
    }
}