using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace RemoteDAL
{
    public class Tools
    {
        public static bool ConvertToBool(object field)
        {
            bool value;
            if (Boolean.TryParse((Convert.ToString(field) ?? "").Trim(), out value))
            {
                return value;
            }
            return false;
        }

        public static decimal ConvertToDecimal(object field)
        {
            decimal val;
            if (Decimal.TryParse((Convert.ToString(field) ?? "").Trim(), out val))
            {
                return val;
            }
            return 0;
        }

        public static int ConvertToInt(object field)
        {
            int value;
            if (Int32.TryParse((Convert.ToString(field) ?? "").Trim(), out value))
            {
                return value;
            }
            return -1;
        }

        public static string ConvertToString(object field)
        {
            return String.Format("{0}", field);
        }

        internal static void AddOutputParam(SqlCommand sqlCommand, string paramName, SqlDbType dbType)
        {
            AddOutputParam(sqlCommand, paramName, dbType, null, null);
        }

        internal static void AddOutputParam(SqlCommand sqlCommand, string paramName, SqlDbType dbType, int? size, object value)
        {
            SqlParameter outPar = null;
            if (size.HasValue)
                outPar = new SqlParameter(paramName, dbType, size.Value);
            else
                outPar = new SqlParameter(paramName, dbType);
            outPar.Direction = ParameterDirection.InputOutput;
            if (value != null)
                outPar.Value = value;
            sqlCommand.Parameters.Add(outPar);
        }
    }
}
