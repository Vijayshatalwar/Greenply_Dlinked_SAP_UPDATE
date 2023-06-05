using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;


namespace GSPPL.common1.Repository
{
    public partial class DBContext<T> where T : BaseEntity
    {
        private SqlConnection _Pullconnection;
        private SqlConnection _Pushconnection;

        /// <summary>
        /// Constructor to init MySQL connection
        /// </summary>
        public DBContext()
        {
            _Pullconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDBConnectionPull"].ConnectionString);
            _Pushconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDBConnectionPush"].ConnectionString);
        }
        #region Pull
        /// <summary>
        /// map result set to Entity
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="propertyKeyMap"></param>
        /// <returns></returns>
        public virtual T PopulateRecord(SqlDataReader reader, IDictionary<string, string> propertyKeyMap = null)
        {
            if (reader != null)
            {
                var entity = GetInstance<T>();
                if (propertyKeyMap == null)
                {
                    foreach (var prop in entity.GetType().GetProperties())
                    {
                        if (HasColumn(reader, prop.Name))
                        {
                            if (reader[prop.Name] != DBNull.Value)
                            {
                                if (prop != null)
                                {
                                    Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                                    object safeValue = (reader[prop.Name] == null) ? null : Convert.ChangeType(reader[prop.Name], t);

                                    prop.SetValue(entity, safeValue, null);
                                }

                                //Type propType = prop.PropertyType;
                                //prop.SetValue(entity, Convert.ChangeType(reader[prop.Name], propType), null);
                            }
                        }
                    }
                    return entity;
                }
                else
                {
                    foreach (var propkey in propertyKeyMap)
                    {
                        var prop = entity.GetType().GetProperties().Where(m => m.Name.ToLower() == propkey.Key.ToLower()).FirstOrDefault();
                        if (HasColumn(reader, propkey.Value) && prop != null)
                        {
                            if (reader[propkey.Value] != DBNull.Value)
                            {
                                if (prop != null)
                                {
                                    Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                                    object safeValue = (reader[prop.Name] == null) ? null : Convert.ChangeType(reader[prop.Name], t);

                                    prop.SetValue(entity, safeValue, null);
                                }

                                //Type propType = prop.PropertyType;
                                //prop.SetValue(entity, Convert.ChangeType(reader[propkey.Value], propType), null);
                            }
                        }
                    }
                    return entity;
                }
            }
            return GetInstance<T>();
        }


        /// <summary>
        /// Get the istance of the entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected To GetInstance<To>()
        {
            return (To)FormatterServices.GetUninitializedObject(typeof(T));
        }


        /// <summary>
        /// Check if the coloum exsist in the Datareader
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        protected bool HasColumn(IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// get array of records for a entity
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns></returns>
        public IEnumerable<T> GetRecords(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _Pullconnection;
            if (_Pullconnection.State == ConnectionState.Closed)
                _Pullconnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                        list.Add(PopulateRecord(reader));
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _Pullconnection.Close();
            }
            return list;
        }

        /// <summary>
        /// get record for a entity
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns></returns>
        public T GetRecord(SqlCommand command)
        {
            T record = null;
            command.Connection = _Pullconnection;
            if (_Pullconnection.State == ConnectionState.Closed)
                _Pullconnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        record = PopulateRecord(reader);
                        break;
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _Pullconnection.Close();
            }
            return record;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteStoredProc(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _Pullconnection;
            command.CommandType = CommandType.StoredProcedure;
            if (_Pullconnection.State == ConnectionState.Closed)
                _Pullconnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        var record = PopulateRecord(reader);
                        if (record != null) list.Add(record);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _Pullconnection.Close();
            }
            return list;
        }

        /// <summary>
        /// get array of records for a entity
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <param name="propertyMap">Maping of Class property to reader coloum.</param>
        /// <returns></returns>
        public IEnumerable<T> GetRecords(SqlCommand command, IDictionary<string, string> propertyMap)
        {
            var list = new List<T>();
            command.Connection = _Pullconnection;
            if (_Pullconnection.State == ConnectionState.Closed)
                _Pullconnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                        list.Add(PopulateRecord(reader, propertyMap));
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _Pullconnection.Close();
            }
            return list;
        }

        /// <summary>
        /// get record for a entity
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <param name="propertyMap">Maping of Class property to reader coloum.</param>
        /// <returns></returns>
        public T GetRecord(SqlCommand command, IDictionary<string, string> propertyMap)
        {
            T record = null;
            command.Connection = _Pullconnection;
            if (_Pullconnection.State == ConnectionState.Closed)
                _Pullconnection.Open();

            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        record = PopulateRecord(reader, propertyMap);
                        break;
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _Pullconnection.Close();
            }
            return record;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns>Return Datatable with all records</returns>
        public DataTable ExecuteStoredProcedure(SqlCommand command)
        {
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _Pullconnection;
            command.CommandType = CommandType.StoredProcedure;
            _Pullconnection.Open();
            try
            {
                try
                {
                    using (reader = command.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _Pullconnection.Close();
            }
            return table;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <param name="propertyMap">Maping of Class property to reader coloum.</param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteStoredProc(SqlCommand command, IDictionary<string, string> propertyMap)
        {
            var list = new List<T>();
            command.Connection = _Pullconnection;
            command.CommandType = CommandType.StoredProcedure;
            if (_Pullconnection.State == ConnectionState.Closed)
                _Pullconnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        var record = PopulateRecord(reader, propertyMap);
                        if (record != null) list.Add(record);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _Pullconnection.Close();
            }
            return list;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns>Return affected records count</returns>
        public int ExecuteProc(SqlCommand command)
        {
            int rowsAffected;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _Pullconnection;
            command.CommandType = CommandType.StoredProcedure;
            //_Pullconnection.Open();
            if (_Pullconnection.State == ConnectionState.Closed)
                _Pullconnection.Open();
            try
            {
                rowsAffected = Convert.ToInt32(command.ExecuteScalar());
            }
            finally
            {
                _Pullconnection.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns>Return affected records count</returns>
        public int ExecuteNonQueryProc(SqlCommand command)
        {
            int rowsAffected;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _Pullconnection;
            command.CommandType = CommandType.StoredProcedure;
            //_Pullconnection.Open();
            if (_Pullconnection.State == ConnectionState.Closed)
                _Pullconnection.Open();
            try
            {
                rowsAffected = command.ExecuteNonQuery();
            }
            finally
            {
                _Pullconnection.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns>Return affected records count</returns>
        public object ExecuteProcedure(SqlCommand command)
        {
            object returnObj;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _Pullconnection;
            command.CommandType = CommandType.StoredProcedure;
            //_Pullconnection.Open();
            if (_Pullconnection.State == ConnectionState.Closed)
                _Pullconnection.Open();
            try
            {
                returnObj = command.ExecuteScalar();
            }
            finally
            {
                _Pullconnection.Close();
            }

            return returnObj;
        }

        /// <summary>
        /// Execute A query
        /// </summary>
        /// <param name="command"> or query.</param>
        /// <returns>Return object</returns>
        public object ExecuteQuery(SqlCommand command)
        {
            object returnObj;
            //IDataReader reader = null;
            command.Connection = _Pullconnection;
            //_Pullconnection.Open();
            if (_Pullconnection.State == ConnectionState.Closed)
                _Pullconnection.Open();
            try
            {
                returnObj = command.ExecuteScalar();
            }
            finally
            {
                _Pullconnection.Close();
            }

            return returnObj;
        }

        ///Below 2 methods Added by Keshaw
        ///
        /// <summary>
        /// Execute A Query
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string query)
        {
            //var list = new List<T>();
            int rowsAdded;
            SqlCommand command = new SqlCommand(query);
            command.Connection = _Pullconnection;
            //command.CommandType = CommandType.;
            if (_Pullconnection.State == ConnectionState.Closed)
                _Pullconnection.Open();
            // _Pullconnection.Open();
            try
            {
                rowsAdded = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _Pullconnection.Close();
            }
            return rowsAdded;
        }

        #endregion

        #region Push
        /// <summary>
        /// map result set to Entity
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="propertyKeyMap"></param>
        /// <returns></returns>
        public virtual T PPopulateRecord(SqlDataReader reader, IDictionary<string, string> propertyKeyMap = null)
        {
            if (reader != null)
            {
                var entity = PGetInstance<T>();
                if (propertyKeyMap == null)
                {
                    foreach (var prop in entity.GetType().GetProperties())
                    {
                        if (PHasColumn(reader, prop.Name))
                        {
                            if (reader[prop.Name] != DBNull.Value)
                            {
                                if (prop != null)
                                {
                                    Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                                    object safeValue = (reader[prop.Name] == null) ? null : Convert.ChangeType(reader[prop.Name], t);

                                    prop.SetValue(entity, safeValue, null);
                                }

                                //Type propType = prop.PropertyType;
                                //prop.SetValue(entity, Convert.ChangeType(reader[prop.Name], propType), null);
                            }
                        }
                    }
                    return entity;
                }
                else
                {
                    foreach (var propkey in propertyKeyMap)
                    {
                        var prop = entity.GetType().GetProperties().Where(m => m.Name.ToLower() == propkey.Key.ToLower()).FirstOrDefault();
                        if (PHasColumn(reader, propkey.Value) && prop != null)
                        {
                            if (reader[propkey.Value] != DBNull.Value)
                            {
                                if (prop != null)
                                {
                                    Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                                    object safeValue = (reader[prop.Name] == null) ? null : Convert.ChangeType(reader[prop.Name], t);

                                    prop.SetValue(entity, safeValue, null);
                                }

                                //Type propType = prop.PropertyType;
                                //prop.SetValue(entity, Convert.ChangeType(reader[propkey.Value], propType), null);
                            }
                        }
                    }
                    return entity;
                }
            }
            return PGetInstance<T>();
        }


        /// <summary>
        /// Get the istance of the entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected To PGetInstance<To>()
        {
            return (To)FormatterServices.GetUninitializedObject(typeof(T));
        }


        /// <summary>
        /// Check if the coloum exsist in the Datareader
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        protected bool PHasColumn(IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// get array of records for a entity
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns></returns>
        public IEnumerable<T> PGetRecords(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _Pushconnection;
            if (_Pushconnection.State == ConnectionState.Closed)
                _Pushconnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                        list.Add(PPopulateRecord(reader));
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _Pushconnection.Close();
            }
            return list;
        }

        /// <summary>
        /// get record for a entity
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns></returns>
        public T PGetRecord(SqlCommand command)
        {
            T record = null;
            command.Connection = _Pushconnection;
            if (_Pushconnection.State == ConnectionState.Closed)
                _Pushconnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        record = PPopulateRecord(reader);
                        break;
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _Pushconnection.Close();
            }
            return record;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns></returns>
        public IEnumerable<T> PExecuteStoredProc(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _Pushconnection;
            command.CommandType = CommandType.StoredProcedure;
            if (_Pushconnection.State == ConnectionState.Closed)
                _Pushconnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        var record = PPopulateRecord(reader);
                        if (record != null) list.Add(record);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _Pushconnection.Close();
            }
            return list;
        }

        /// <summary>
        /// get array of records for a entity
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <param name="propertyMap">Maping of Class property to reader coloum.</param>
        /// <returns></returns>
        public IEnumerable<T> PGetRecords(SqlCommand command, IDictionary<string, string> propertyMap)
        {
            var list = new List<T>();
            command.Connection = _Pushconnection;
            if (_Pushconnection.State == ConnectionState.Closed)
                _Pushconnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                        list.Add(PPopulateRecord(reader, propertyMap));
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _Pushconnection.Close();
            }
            return list;
        }

        /// <summary>
        /// get record for a entity
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <param name="propertyMap">Maping of Class property to reader coloum.</param>
        /// <returns></returns>
        public T PGetRecord(SqlCommand command, IDictionary<string, string> propertyMap)
        {
            T record = null;
            command.Connection = _Pushconnection;
            if (_Pushconnection.State == ConnectionState.Closed)
                _Pushconnection.Open();

            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        record = PPopulateRecord(reader, propertyMap);
                        break;
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _Pushconnection.Close();
            }
            return record;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns>Return Datatable with all records</returns>
        public DataTable PExecuteStoredProcedure(SqlCommand command)
        {
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _Pushconnection;
            command.CommandType = CommandType.StoredProcedure;
            _Pushconnection.Open();
            try
            {
                try
                {
                    using (reader = command.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _Pushconnection.Close();
            }
            return table;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <param name="propertyMap">Maping of Class property to reader coloum.</param>
        /// <returns></returns>
        public IEnumerable<T> PExecuteStoredProc(SqlCommand command, IDictionary<string, string> propertyMap)
        {
            var list = new List<T>();
            command.Connection = _Pushconnection;
            command.CommandType = CommandType.StoredProcedure;
            if (_Pushconnection.State == ConnectionState.Closed)
                _Pushconnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        var record = PPopulateRecord(reader, propertyMap);
                        if (record != null) list.Add(record);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _Pushconnection.Close();
            }
            return list;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns>Return affected records count</returns>
        public int PExecuteProc(SqlCommand command)
        {
            int rowsAffected;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _Pushconnection;
            command.CommandType = CommandType.StoredProcedure;
            //_Pushconnection.Open();
            if (_Pushconnection.State == ConnectionState.Closed)
                _Pushconnection.Open();
            try
            {
                rowsAffected = Convert.ToInt32(command.ExecuteScalar());
            }
            finally
            {
                _Pushconnection.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns>Return affected records count</returns>
        public int PExecuteNonQueryProc(SqlCommand command)
        {
            int rowsAffected;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _Pushconnection;
            command.CommandType = CommandType.StoredProcedure;
            //_Pushconnection.Open();
            if (_Pushconnection.State == ConnectionState.Closed)
                _Pushconnection.Open();
            try
            {
                rowsAffected = command.ExecuteNonQuery();
            }
            finally
            {
                _Pushconnection.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns>Return affected records count</returns>
        public object PExecuteProcedure(SqlCommand command)
        {
            object returnObj;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _Pushconnection;
            command.CommandType = CommandType.StoredProcedure;
            //_Pushconnection.Open();
            if (_Pushconnection.State == ConnectionState.Closed)
                _Pushconnection.Open();
            try
            {
                returnObj = command.ExecuteScalar();
            }
            finally
            {
                _Pushconnection.Close();
            }

            return returnObj;
        }

        /// <summary>
        /// Execute A query
        /// </summary>
        /// <param name="command"> or query.</param>
        /// <returns>Return object</returns>
        public object PExecuteQuery(SqlCommand command)
        {
            object returnObj;
            //IDataReader reader = null;
            command.Connection = _Pushconnection;
            //_Pushconnection.Open();
            if (_Pushconnection.State == ConnectionState.Closed)
                _Pushconnection.Open();
            try
            {
                returnObj = command.ExecuteScalar();
            }
            finally
            {
                _Pushconnection.Close();
            }

            return returnObj;
        }

        ///Below 2 methods Added by Keshaw
        ///
        /// <summary>
        /// Execute A Query
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns></returns>
        public int PExecuteNonQuery(string query)
        {
            //var list = new List<T>();
            int rowsAdded;
            SqlCommand command = new SqlCommand(query);
            command.Connection = _Pushconnection;
            //command.CommandType = CommandType.;
            if (_Pushconnection.State == ConnectionState.Closed)
                _Pushconnection.Open();
            // _Pushconnection.Open();
            try
            {
                rowsAdded = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _Pushconnection.Close();
            }
            return rowsAdded;
        }

        #endregion

    }

}
