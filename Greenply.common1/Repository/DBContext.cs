using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.Serialization;


namespace Greenply.common1.Repository
{
    public partial class DBContext<T> where T : BaseEntity
    {
        private SqlConnection _Pullconnection;
        private SqlConnection _Pushconnection;
        private SqlConnection _PullconnectionRKT_Decor;
        private SqlConnection _PushconnectionRKT_Door;
        private SqlConnection _PushconnectionRKT_PLY;
        private SqlConnection _PushconnectionTIZIT;
        private SqlConnection _PushconnectionSandila;

        //----HUB
        private SqlConnection _Pushconnection_KHUB;
        private SqlConnection _Pushconnection_BHUB;
        private SqlConnection _Pushconnection_YHUB;
        private SqlConnection _Pushconnection_CHAKDAP1;
        private SqlConnection _Pushconnection_BAREILLY; 
        private SqlConnection _Pushconnection_CENTRAL; 


        /// <summary>
        /// Constructor to init SQL connection
        /// </summary>
        public DBContext()
        {
            _Pushconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDBConnectionPush"].ConnectionString);
            _Pullconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDBConnectionPull"].ConnectionString);//Kriparampur
            _PullconnectionRKT_Decor = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDBConnectionPullRKT_Decor"].ConnectionString);//Kriparampur Decor
            _PushconnectionRKT_Door = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDBConnectionPullRKT_Door"].ConnectionString);//Kriparampur Door
            _PushconnectionRKT_PLY = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDBConnectionPullRKT_PLY"].ConnectionString);//Kriparampur Ply
            _PushconnectionTIZIT = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDBConnectionPullTIZIT"].ConnectionString);//TIZIt
            _PushconnectionSandila = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDBConnectionPullSandila"].ConnectionString);//Sandila

            _Pushconnection_KHUB = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDBConnectionPull_KHUB"].ConnectionString);//Sandila
            _Pushconnection_BHUB = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDBConnectionPull_BHUB"].ConnectionString);//Sandila
            _Pushconnection_YHUB = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDBConnectionPull_YHUB"].ConnectionString);//Sandila
            _Pushconnection_CHAKDAP1 = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDBConnectionPull_CHAKDAP1"].ConnectionString);//Sandila
            _Pushconnection_CENTRAL = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDBConnectionPull_CENTRAL"].ConnectionString);//Sandila
        }

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

        #region Push
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
        }//in
        public object PExecuteProcedure(SqlCommand command)
        {
            object returnObj;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _Pushconnection;
            command.CommandType = CommandType.StoredProcedure;
            //_connection.Open();
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

        public IEnumerable<T> PUGetRecords(SqlCommand command)
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
                _Pushconnection.Close();
            }
            return list;
        }

        #endregion

        #region Pull Kriparampur
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
        }//in
        #endregion

        #region Pull Kriparampur Decor
        public IEnumerable<T> PDGetRecords(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _PullconnectionRKT_Decor;
            if (_PullconnectionRKT_Decor.State == ConnectionState.Closed)
                _PullconnectionRKT_Decor.Open();
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
                _PullconnectionRKT_Decor.Close();
            }
            return list;
        }
        public object PDExecuteProcedure(SqlCommand command)
        {
            object returnObj;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _PullconnectionRKT_Decor;
            command.CommandType = CommandType.StoredProcedure;
            //_PullconnectionRKT_Decor.Open();
            if (_PullconnectionRKT_Decor.State == ConnectionState.Closed)
                _PullconnectionRKT_Decor.Open();
            try
            {
                returnObj = command.ExecuteScalar();
            }
            finally
            {
                _PullconnectionRKT_Decor.Close();
            }

            return returnObj;
        }//in
        #endregion

        #region Pull Kriparampur Door
        public IEnumerable<T> PRDGetRecords(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _PushconnectionRKT_Door;
            if (_PushconnectionRKT_Door.State == ConnectionState.Closed)
                _PushconnectionRKT_Door.Open();
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
                _PushconnectionRKT_Door.Close();
            }
            return list;
        }
        public object PRDExecuteProcedure(SqlCommand command)
        {
            object returnObj;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _PushconnectionRKT_Door;
            command.CommandType = CommandType.StoredProcedure;
            //_PushconnectionRKT_Door.Open();
            if (_PushconnectionRKT_Door.State == ConnectionState.Closed)
                _PushconnectionRKT_Door.Open();
            try
            {
                returnObj = command.ExecuteScalar();
            }
            finally
            {
                _PushconnectionRKT_Door.Close();
            }

            return returnObj;
        }//in
        #endregion

        #region Pull Kriparampur PLY
        public IEnumerable<T> PPGetRecords(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _PushconnectionRKT_PLY;
            if (_PushconnectionRKT_PLY.State == ConnectionState.Closed)
                _PushconnectionRKT_PLY.Open();
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
                _PushconnectionRKT_PLY.Close();
            }
            return list;
        }
        public object PPExecuteProcedure(SqlCommand command)
        {
            object returnObj;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _PushconnectionRKT_PLY;
            command.CommandType = CommandType.StoredProcedure;
            //_PushconnectionRKT_PLY.Open();
            if (_PushconnectionRKT_PLY.State == ConnectionState.Closed)
                _PushconnectionRKT_PLY.Open();
            try
            {
                returnObj = command.ExecuteScalar();
            }
            finally
            {
                _PushconnectionRKT_PLY.Close();
            }

            return returnObj;
        }//in
        #endregion

        #region Pull Tizit
        public IEnumerable<T> PTGetRecords(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _PushconnectionTIZIT;
            if (_PushconnectionTIZIT.State == ConnectionState.Closed)
                _PushconnectionTIZIT.Open();
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
                _PushconnectionTIZIT.Close();
            }
            return list;
        }
        public object PTExecuteProcedure(SqlCommand command)
        {
            object returnObj;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _PushconnectionTIZIT;
            command.CommandType = CommandType.StoredProcedure;
            //_PushconnectionTIZIT.Open();
            if (_PushconnectionTIZIT.State == ConnectionState.Closed)
                _PushconnectionTIZIT.Open();
            try
            {
                returnObj = command.ExecuteScalar();
            }
            finally
            {
                _PushconnectionTIZIT.Close();
            }

            return returnObj;
        }//in
        #endregion

        #region Pull Sandila
        public IEnumerable<T> PSGetRecords(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _PushconnectionSandila;
            if (_PushconnectionSandila.State == ConnectionState.Closed)
                _PushconnectionSandila.Open();
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
                _PushconnectionSandila.Close();
            }
            return list;
        }
        public object PSExecuteProcedure(SqlCommand command)
        {
            object returnObj;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _PushconnectionSandila;
            command.CommandType = CommandType.StoredProcedure;
            //_PushconnectionSandila.Open();
            if (_PushconnectionSandila.State == ConnectionState.Closed)
                _PushconnectionSandila.Open();
            try
            {
                returnObj = command.ExecuteScalar();
            }
            finally
            {
                _PushconnectionSandila.Close();
            }

            return returnObj;
        }//in
        #endregion

        #region Pull Central
        public IEnumerable<T> CSGetRecords(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _Pushconnection_CENTRAL;
            if (_Pushconnection_CENTRAL.State == ConnectionState.Closed)
                _Pushconnection_CENTRAL.Open();
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
                _Pushconnection_CENTRAL.Close();
            }
            return list;
        }
        public object CSExecuteProcedure(SqlCommand command)
        {
            object returnObj;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _Pushconnection_CENTRAL;
            command.CommandType = CommandType.StoredProcedure;
            //_Pushconnection_CENTRAL.Open();
            if (_Pushconnection_CENTRAL.State == ConnectionState.Closed)
                _Pushconnection_CENTRAL.Open();
            try
            {
                returnObj = command.ExecuteScalar();
            }
            finally
            {
                _Pushconnection_CENTRAL.Close();
            }

            return returnObj;
        }//in
        #endregion

        #region Pull K Hub
        public IEnumerable<T> PKHUBGetRecords(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _Pushconnection_KHUB;
            if (_Pushconnection_KHUB.State == ConnectionState.Closed)
                _Pushconnection_KHUB.Open();
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
                _Pushconnection_KHUB.Close();
            }
            return list;
        }
        public object PKHUBExecuteProcedure(SqlCommand command)
        {
            object returnObj;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _Pushconnection_KHUB;
            command.CommandType = CommandType.StoredProcedure;
            //_Pushconnection_KHUB.Open();
            if (_Pushconnection_KHUB.State == ConnectionState.Closed)
                _Pushconnection_KHUB.Open();
            try
            {
                returnObj = command.ExecuteScalar();
            }
            finally
            {
                _Pushconnection_KHUB.Close();
            }

            return returnObj;
        }//in
        #endregion

        #region Pull K Hub
        public IEnumerable<T> BKHUBGetRecords(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _Pushconnection_BHUB;
            if (_Pushconnection_BHUB.State == ConnectionState.Closed)
                _Pushconnection_BHUB.Open();
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
                _Pushconnection_BHUB.Close();
            }
            return list;
        }
        public object BKHUBExecuteProcedure(SqlCommand command)
        {
            object returnObj;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _Pushconnection_BHUB;
            command.CommandType = CommandType.StoredProcedure;
            //_Pushconnection_BHUB.Open();
            if (_Pushconnection_BHUB.State == ConnectionState.Closed)
                _Pushconnection_BHUB.Open();
            try
            {
                returnObj = command.ExecuteScalar();
            }
            finally
            {
                _Pushconnection_BHUB.Close();
            }

            return returnObj;
        }//in
        #endregion

        #region Pull K Hub
        public IEnumerable<T> YKHUBGetRecords(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _Pushconnection_YHUB;
            if (_Pushconnection_YHUB.State == ConnectionState.Closed)
                _Pushconnection_YHUB.Open();
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
                _Pushconnection_YHUB.Close();
            }
            return list;
        }
        public object YKHUBExecuteProcedure(SqlCommand command)
        {
            object returnObj;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _Pushconnection_YHUB;
            command.CommandType = CommandType.StoredProcedure;
            //_Pushconnection_YHUB.Open();
            if (_Pushconnection_YHUB.State == ConnectionState.Closed)
                _Pushconnection_YHUB.Open();
            try
            {
                returnObj = command.ExecuteScalar();
            }
            finally
            {
                _Pushconnection_YHUB.Close();
            }

            return returnObj;
        }//in
        #endregion

        #region Pull Chakda
        public IEnumerable<T> CHAKDAP1GetRecords(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _Pushconnection_CHAKDAP1;
            if (_Pushconnection_CHAKDAP1.State == ConnectionState.Closed)
                _Pushconnection_CHAKDAP1.Open();
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
                _Pushconnection_CHAKDAP1.Close();
            }
            return list;
        }
        public object CHAKDAP1ExecuteProcedure(SqlCommand command)
        {
            object returnObj;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _Pushconnection_CHAKDAP1;
            command.CommandType = CommandType.StoredProcedure;
            //_Pushconnection_YHUB.Open();
            if (_Pushconnection_CHAKDAP1.State == ConnectionState.Closed)
                _Pushconnection_CHAKDAP1.Open();
            try
            {
                returnObj = command.ExecuteScalar();
            }
            finally
            {
                _Pushconnection_CHAKDAP1.Close();
            }

            return returnObj;
        }//in
        #endregion

        #region Pull Bareilly
        public IEnumerable<T> BAREILLYGetRecords(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _Pushconnection_BAREILLY;
            if (_Pushconnection_BAREILLY.State == ConnectionState.Closed)
                _Pushconnection_BAREILLY.Open();
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
                _Pushconnection_BAREILLY.Close();
            }
            return list;
        }
        public object BAREILLYExecuteProcedure(SqlCommand command)
        {
            object returnObj;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _Pushconnection_BAREILLY;
            command.CommandType = CommandType.StoredProcedure;
            //_Pushconnection_YHUB.Open();
            if (_Pushconnection_BAREILLY.State == ConnectionState.Closed)
                _Pushconnection_BAREILLY.Open();
            try
            {
                returnObj = command.ExecuteScalar();
            }
            finally
            {
                _Pushconnection_BAREILLY.Close();
            }

            return returnObj;
        }//in
        #endregion

    }

}
