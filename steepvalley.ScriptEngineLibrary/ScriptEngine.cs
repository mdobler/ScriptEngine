using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace steepvalley.ScriptEngineLibrary
{
    public class ScriptEngine
    {
        /// <summary>
        /// builds a SQL Server connection string from the base information
        /// </summary>
        /// <param name="server"></param>
        /// <param name="database"></param>
        /// <param name="integratedSecurity"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string BuildConnectionString(string server, string database, bool integratedSecurity = false, string username = "", string password = "")
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.InitialCatalog = database;
            builder.DataSource = server;
            builder.IntegratedSecurity = integratedSecurity;
            builder.UserID = username;
            builder.Password = password;

            return builder.ConnectionString;
        }

        public static bool CanConnect(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// creates a number of delete statements based on the where clause
        /// statements are created ordered by the orderClause
        /// checkifExists creates an "if exists then..." clause for each record
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="keys"></param>
        /// <param name="whereClause"></param>
        /// <param name="orderClause"></param>
        /// <param name="connectionString"></param>
        /// <param name="checkIfExists"></param>
        /// <returns></returns>
        public static string CreateDeleteStatement(
            string tableName,
            string[] keys,
            string whereClause,
            string orderClause,
            string connectionString,
            bool checkIfExists = false)
        {
            StringBuilder output = new StringBuilder();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"select * from {tableName}";
                if (string.IsNullOrEmpty(whereClause) == false) {query += $" where {whereClause}"; }
                if (string.IsNullOrEmpty(orderClause) == false) {query += $" order by {orderClause}"; }
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                DataTable schemaTable = reader.GetSchemaTable();

                while (reader.Read())
                {
                    var recordWhere = CreateWhereClause((IDataRecord)reader, keys, schemaTable);

                    if (checkIfExists)
                    {
                        output.AppendLine($"if exists (select 1 from {tableName} where {recordWhere})");
                        output.AppendLine("begin");
                    }
                    output.AppendLine($"delete from {tableName} where {recordWhere}");
                    if (checkIfExists)
                    {
                        output.AppendLine("end");
                    }
                }

                reader.Close();
            }
            return output.ToString();
        }

        /// <summary>
        /// creates a number of select statements based on the where clause
        /// if hardcodeValues is selected the function creates pure selects
        /// with record values and without a from clause.
        /// if not selected, it creates a full "select .. from .. where .." statement
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="keys"></param>
        /// <param name="whereClause"></param>
        /// <param name="orderClause"></param>
        /// <param name="connectionString"></param>
        /// <param name="hardcodeValues"></param>
        /// <returns></returns>
        public static string CreateSelectStatement(
            string tableName,
            string[] keys,
            string whereClause,
            string orderClause,
            string connectionString,
            bool hardcodeValues)
        {
            StringBuilder output = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"select * from {tableName}";
                if (string.IsNullOrEmpty(whereClause) == false) { query += $" where {whereClause}"; }
                if (string.IsNullOrEmpty(orderClause) == false) { query += $" order by {orderClause}"; }
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                DataTable schemaTable = reader.GetSchemaTable();

                while (reader.Read())
                {
                    var recordWhere = CreateWhereClause((IDataRecord)reader, keys, schemaTable);
                    var recordSelect = CreateSelectClause((IDataRecord)reader, hardcodeValues, schemaTable);

                    output.Append($"select {recordSelect}");

                    if (!hardcodeValues)
                    {
                        output.Append($" from {tableName} where {recordWhere}");
                    }
                    output.AppendLine();
                }

                reader.Close();
            }
            return output.ToString();
        }

        /// <summary>
        /// creates a number of insert statements ordered by orderby clause
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="keys"></param>
        /// <param name="whereClause"></param>
        /// <param name="orderClause"></param>
        /// <param name="connectionString"></param>
        /// <param name="checkIfExists"></param>
        /// <returns></returns>
        public static string CreateInsertStatement(
            string tableName,
            string[] keys,
            string whereClause,
            string orderClause,
            string connectionString,
            bool checkIfExists = false)
        {
            StringBuilder output = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"select * from {tableName}";
                if (string.IsNullOrEmpty(whereClause) == false) { query += $" where {whereClause}"; }
                if (string.IsNullOrEmpty(orderClause) == false) { query += $" order by {orderClause}"; }
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                DataTable schemaTable = reader.GetSchemaTable();

                var insertClause = CreateInsertClause(schemaTable);

                while (reader.Read())
                {
                    if (checkIfExists)
                    {
                        var recordWhere = CreateWhereClause((IDataRecord)reader, keys, schemaTable);
                        output.AppendLine($"if not exists (select 1 from {tableName} where {recordWhere})");
                        output.AppendLine("begin");
                    }
                    var valuesClause = CreateValuesClause((IDataRecord)reader, schemaTable);
                    output.AppendLine($"insert into {tableName} ({insertClause}) values ({valuesClause})");
                    if (checkIfExists)
                    {
                        output.AppendLine("end");
                    }
                }

                reader.Close();
            }
            return output.ToString();
        }

        /// <summary>
        /// creates a number of update statements based on where clause
        /// statements are ordered based on order clause
        /// updateFields is a list of column names to be updated
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="keys"></param>
        /// <param name="updateFields"></param>
        /// <param name="whereClause"></param>
        /// <param name="orderClause"></param>
        /// <param name="connectionString"></param>
        /// <param name="checkIfExists"></param>
        /// <returns></returns>
        public static string CreateUpdateStatement(
            string tableName,
            string[] keys,
            string[] updateFields,
            string whereClause,
            string orderClause,
            string connectionString,
            bool checkIfExists = false)
        {
            StringBuilder output = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"select * from {tableName}";
                if (string.IsNullOrEmpty(whereClause) == false) { query += $" where {whereClause}"; }
                if (string.IsNullOrEmpty(orderClause) == false) { query += $" order by {orderClause}"; }
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                DataTable schemaTable = reader.GetSchemaTable();

                while (reader.Read())
                {
                    var recordWhere = CreateWhereClause((IDataRecord)reader, keys, schemaTable);

                    if (checkIfExists)
                    {
                        output.AppendLine($"if exists (select 1 from {tableName} where {recordWhere})");
                        output.AppendLine("begin");
                    }
                    var updateClause = CreateUpdateClause((IDataRecord)reader, updateFields, schemaTable);
                    output.AppendLine($"update {tableName} set {updateClause} where {recordWhere}");
                    if (checkIfExists)
                    {
                        output.AppendLine("end");
                    }
                }

                reader.Close();
            }
            return output.ToString();
        }

        /// <summary>
        /// recreates triggers, functions or procedures from a database
        /// </summary>
        /// <param name="routineName"></param>
        /// <param name="connectionString"></param>
        /// <param name="deleteAndRecreate"></param>
        /// <returns></returns>
        public static string ScriptRoutine(
            string routineName, 
            string connectionString, 
            bool deleteAndRecreate = true)
        {
            StringBuilder output = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"select o.name, o.type_desc, m.definition FROM sys.sql_modules m INNER JOIN sys.objects  o ON m.object_id = o.object_id where o.name like '{routineName}'";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (deleteAndRecreate)
                    {
                        var typedesc = reader.GetString(1);
                        var name = reader.GetString(0);

                        switch (typedesc)
                        {
                            case "SQL_STORED_PROCEDURE":
                                typedesc = "PROCEDURE";
                                break;
                            case "SQL_SCALAR_FUNCTION":
                            case "SQL_TABLE_VALUED_FUNCTION":
                                typedesc = "FUNCTION";
                                break;
                            case "SQL_TRIGGER":
                                typedesc = "TRIGGER";
                                break;
                            default:
                                break;
                        }

                        output.AppendLine($"if exists (select 1 from sysobjects where name = '{name}')");
                        output.AppendLine("begin");
                        output.AppendLine($"drop {typedesc} [{name}]");
                        output.AppendLine("end");
                        output.AppendLine("GO");
                        output.AppendLine("");
                    }

                    string content = reader.GetString(2);
                    output.Append(content);
                    output.AppendLine();
                    output.AppendLine("GO");
                    output.AppendLine("");
                }

                reader.Close();
            }
            return output.ToString();
        }

        public static string CreateWhereClause(IDataRecord dataRecord, string[] keys, DataTable schemaTable)
        {
            StringBuilder whereclause = new StringBuilder();
            foreach (string key in keys)
            {
                var field = schemaTable.Rows
                                .Cast<DataRow>()
                                .Where(x => x[0].ToString()
                                    .Equals(key, StringComparison.InvariantCultureIgnoreCase))
                                .FirstOrDefault();

                if (whereclause.Length != 0) { whereclause.Append(" AND "); }
                whereclause.Append($"{field["ColumnName"]} = {FormatValue(dataRecord[(int) field["ColumnOrdinal"]], field["DataTypeName"].ToString())}");
            }

            return whereclause.ToString();
            
        }

        public static string CreateSelectClause(IDataRecord dataRecord, bool hardcodeValues, DataTable schemaTable)
        {
            StringBuilder selectclause = new StringBuilder();
            foreach (var field in schemaTable.Rows.Cast<DataRow>())
            { 
                if (selectclause.Length != 0) { selectclause.Append(", "); }
                if (hardcodeValues)
                {
                    selectclause.Append($"{FormatValue(dataRecord[(int)field["ColumnOrdinal"]], field["DataTypeName"].ToString())} AS ");

                }
                selectclause.Append($"[{field["ColumnName"]}]");
            }
            return selectclause.ToString();
        }

        public static string CreateInsertClause(DataTable schemaTable)
        {
            StringBuilder insertclause = new StringBuilder();
            foreach (var field in schemaTable.Rows.Cast<DataRow>())
            {
                if (insertclause.Length != 0) { insertclause.Append(", "); }
                insertclause.Append($"[{field["ColumnName"]}]");
            }
            return insertclause.ToString();
        }

        public static string CreateValuesClause(IDataRecord dataRecord, DataTable schemaTable)
        {
            StringBuilder selectclause = new StringBuilder();
            foreach (var field in schemaTable.Rows.Cast<DataRow>())
            {
                if (selectclause.Length != 0) { selectclause.Append(", "); }
                selectclause.Append($"{FormatValue(dataRecord[(int)field["ColumnOrdinal"]], field["DataTypeName"].ToString())}");
            }
            return selectclause.ToString();
        }

        public static string CreateUpdateClause(IDataRecord dataRecord, string[] updateFields, DataTable schemaTable)
        {
            StringBuilder whereclause = new StringBuilder();
            foreach (string key in updateFields)
            {
                var field = schemaTable.Rows
                                .Cast<DataRow>()
                                .Where(x => x[0].ToString()
                                    .Equals(key, StringComparison.InvariantCultureIgnoreCase))
                                .FirstOrDefault();

                if (whereclause.Length != 0) { whereclause.Append(", "); }
                whereclause.Append($"[{field["ColumnName"]}] = {FormatValue(dataRecord[(int)field["ColumnOrdinal"]], field["DataTypeName"].ToString())}");
            }

            return whereclause.ToString();
        }


        public static string[] GetArrayOfFields(DataTable schemaTable)
        {
            var retval = new List<string>();
            StringBuilder insertclause = new StringBuilder();
            foreach (var field in schemaTable.Rows.Cast<DataRow>())
            {
                retval.Add(field["ColumnName"].ToString());
            }
            return retval.ToArray();
        }

        public static DataTable GetSchemaTable(string tableName, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"select top 1 * from {tableName}";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                DataTable schemaTable = reader.GetSchemaTable();

                reader.Close();
                return schemaTable;
            }
        }

        public static string FormatValue(object value, string dataTypeName)
        {
            if (value is DBNull) { return "NULL"; }

            switch (dataTypeName)
            {
                case "varchar":
                case "nvarchar":
                case "char":
                case "nchar":
                case "text":
                    string content = value.ToString().Replace("'", "''");
                    content = content.Replace("\n", "").Replace("\r", "");
                    return $"'{content}'";
                    break;
                case "uniqueidentifier":
                case "image":
                    return $"'{value.ToString().Replace("'", "''")}'";
                    break;
                case "datetime":
                    return $"'{((DateTime)value).ToString("o")}'";
                    break;
                default:
                    return value.ToString();
                    break;
            }

        }

        public static string[] GetTableTriggerNames(string tableName, string connectionString)
        {
            List<string> retval = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"select o.name FROM  sys.objects o where o.parent_object_id = object_id('{tableName}') and o.type = 'TR'";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    retval.Add(reader.GetString(0));
                }
                reader.Close();
            }
            return retval.ToArray();
        }
    }
}
