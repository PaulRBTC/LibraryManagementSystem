using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repositories.Base
{
    public abstract class BaseRepository<T> where T : Models.BaseEntity, new()
    {

        protected Models.ConnectionString ConnectionString { get; private set; }
        protected string TableName { get; private set; }
        
        private Factories.Base.IEntityFactory<T> _entityFactory { get; set; }

        public BaseRepository(
            Models.ConnectionString connectionString,
            string tableName,
            Factories.Base.IEntityFactory<T> entityFactory
        )
        {
            ConnectionString = connectionString;
            TableName = tableName;
            _entityFactory = entityFactory;
        }

        public async Task<T?> Get(long id)
        {
            T result = null;
            using (var connection = new MySqlConnection($"{ConnectionString}"))
            {
                connection.Open();

                var cmd = new MySqlCommand($"SELECT * FROM {TableName} WHERE id = @id", connection);
                cmd.Parameters.AddWithValue("id", id);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        result = BuildEntityFromReader(reader);
                    }
                }
            }

            return result;
        }

        public async IAsyncEnumerable<T> GetAll()
        {
            using (var connection = new MySqlConnection($"{ConnectionString}"))
            {
                connection.Open();

                var cmd = new MySqlCommand($"SELECT * FROM {TableName}", connection);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while(reader.Read())
                    {
                        yield return BuildEntityFromReader(reader);
                    }
                }
            }
        }

        public async Task<bool> Update(T @new)
        {
            using (var connection = new MySqlConnection($"{ConnectionString}"))
            {
                connection.Open();

                string primaryKeyColumn = "";
                string primaryKeyValue = "";

                Dictionary<string, string> setColumns = new();
                string query = $"UPDATE {TableName} SET ";

                // get all properties which are public & non-static
                foreach (var property in typeof(T).GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance))
                {
                    var ignoreAttr = property.GetCustomAttribute<Models.Attributes.DbIgnoreAttribute>();
                    if (ignoreAttr != null)
                    {
                        continue;
                    }

                    var attr = property.GetCustomAttribute<Models.Attributes.DbColumnNameAttribute>();
                    if (attr != null)
                    {
                        var primaryKeyAttr = property.GetCustomAttribute<Models.Attributes.DbIsPrimaryKeyAttribute>();
                        if (string.IsNullOrEmpty(primaryKeyColumn))
                        {
                            if (primaryKeyAttr != null)
                            {
                                primaryKeyColumn = attr.ColumnName;
                                primaryKeyValue = property.GetValue(@new)?.ToString();
                                continue;
                            }
                        }
                        else if (!string.IsNullOrEmpty(primaryKeyColumn) && primaryKeyAttr != null)
                        {
                            throw new Exceptions.DbPrimaryKeyDefinedMoreThanOnceException(typeof(T).Name);
                        }

                        if (setColumns.ContainsKey(attr.ColumnName))
                        {
                            throw new Exceptions.DbColumnDefinedMoreThanOnceException(typeof(T).Name, attr.ColumnName);
                        }

                        object val = property.GetValue(@new);
                        string stringValue = "";
                        if (val is null)
                        {
                            stringValue = "NULL";
                        }
                        else
                        {
                            Type propertyType = property.PropertyType;
                            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                propertyType = propertyType.GetGenericArguments()[0];
                            }

                            if (propertyType == typeof(DateTime))
                            {
                                stringValue = $"'{(DateTime)val:yyyy-MM-dd HH:mm:ss}'";
                            }
                            else if (propertyType.IsPrimitive)
                            {
                                stringValue = $"{val}";
                            }
                            else
                            {
                                stringValue = $"'{val}'";
                            }
                        }
                        
                        setColumns.Add(attr.ColumnName, stringValue);
                    }
                }

                if (string.IsNullOrEmpty(primaryKeyColumn) || string.IsNullOrEmpty(primaryKeyValue))
                {
                    throw new Exceptions.DbPrimaryKeyColumnNotSetException(typeof(T).Name);
                }

                query += string.Join(", ", setColumns.Select(x => $"{x.Key} = {x.Value}"));
                query += $" WHERE {primaryKeyColumn} = {primaryKeyValue}";

                var cmd = new MySqlCommand(query, connection);
                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }

        public async Task<T> Create(T @new)
        {
            using (var connection = new MySqlConnection($"{ConnectionString}"))
            {
                connection.Open();

                List<string> columnNames = new();
                List<string> values = new();

                // get all properties which are public & non-static
                foreach (var property in typeof(T).GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance))
                {
                    var ignoreAttr = property.GetCustomAttribute<Models.Attributes.DbIgnoreAttribute>();
                    if (ignoreAttr != null)
                    {
                        continue;
                    }

                    var attr = property.GetCustomAttribute<Models.Attributes.DbColumnNameAttribute>();
                    if (attr != null)
                    {
                        var primaryKeyAttr = property.GetCustomAttribute<Models.Attributes.DbIsPrimaryKeyAttribute>();
                        if (primaryKeyAttr != null)
                        {
                            continue;
                        }

                        if (columnNames.Contains(attr.ColumnName))
                        {
                            throw new Exceptions.DbColumnDefinedMoreThanOnceException(typeof(T).Name, attr.ColumnName);
                        }

                        object val = property.GetValue(@new);
                        string stringValue = "";
                        if (val is null)
                        {
                            stringValue = "NULL";
                        }
                        else
                        {
                            Type propertyType = property.PropertyType;
                            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                propertyType = propertyType.GetGenericArguments()[0];
                            }

                            if (propertyType == typeof(DateTime))
                            {
                                stringValue = $"'{(DateTime)val:yyyy-MM-dd HH:mm:ss}'";
                            }
                            else if (propertyType.IsPrimitive)
                            {
                                stringValue = $"{val}";
                            }
                            else
                            {
                                stringValue = $"'{val}'";
                            }
                        }

                        columnNames.Add(attr.ColumnName);
                        values.Add(stringValue);
                    }
                }

                string query = $"INSERT INTO {TableName} ({string.Join(", ", columnNames)}) VALUES ({string.Join(", ", values)})";
                var cmd = new MySqlCommand(query, connection);

                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    foreach (var primaryKeyProp in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.GetCustomAttribute<Models.Attributes.DbIsPrimaryKeyAttribute>() != null))
                    {
                        if (primaryKeyProp.CanWrite)
                        {
                            primaryKeyProp.SetValue(@new, cmd.LastInsertedId);
                        }
                    }
                }

                return @new;
            }
        }

        public async Task<bool> Delete(long id)
        {
            using (var connection = new MySqlConnection($"{ConnectionString}"))
            {
                connection.Open();

                var cmd = new MySqlCommand($"DELETE FROM {TableName} WHERE id = @id", connection);
                cmd.Parameters.AddWithValue("id", id);

                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }

        public async Task<bool> CheckIn(long id)
        {
            using (var connection = new MySqlConnection($"{ConnectionString}"))
            {
                connection.Open();

                var cmd = new MySqlCommand($"UPDATE {TableName} SET checked_in_at = '{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}', checked_out_at = NULL WHERE id = @id", connection);
                cmd.Parameters.AddWithValue("id", id);

                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }

        public async Task<bool> CheckOut(long id)
        {
            using (var connection = new MySqlConnection($"{ConnectionString}"))
            {
                connection.Open();

                var cmd = new MySqlCommand($"UPDATE {TableName} SET checked_out_at = '{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}' WHERE id = @id", connection);
                cmd.Parameters.AddWithValue("id", id);

                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }

        private T BuildEntityFromReader(DbDataReader reader)
        {
            return this._entityFactory.BuildEntityFromReader(reader);
        }

    }
}
