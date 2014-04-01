using System;
using System.Data;
using System.Data.Common;

namespace foundation
{
    public sealed class dbManager : IDatabase<DbDataReader,DbConnection>
    {
        private DbConnection conn;
        private string dbname;
        private DbDataReader reader;
        private static dbManager instance;
        private DbCommand command;
        private DbTransaction transaction;

        DbProviderFactory fact;

        private dbManager(string dbname)
        {
            this.dbname = dbname;
            try
            {
                if (fact == null)
                {
                    fact = DbProviderFactories.GetFactory("System.Data.SQLite");
                }
            }
            catch (Exception e )
            {
                throw new Exception(e.Message);
            }
            
            conn = fact.CreateConnection();
            Conn.ConnectionString = "Data Source=" + dbname;
            command = fact.CreateCommand();
            command.Connection = Conn;
        }

        public static dbManager Instance()
        {
            if (instance == null)
            {
                instance = new dbManager("data.db");
            }
            return instance;
        }

        // open the connection to the database
        public void open(string dbname)
        {
            //string connString = "Data Source=" + dbname;
            try
            {
                Conn.Open();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            
            if (Conn.State == ConnectionState.Closed)
                throw new Exception("Connection is closed. Can't connect to the database");
        }

        public void close()
        {
            Conn.Close();
        }

        public void execute(string query)
        {
            if (Conn.State == ConnectionState.Closed)
                open(dbname);
            try
            {
                DbCommand command = fact.CreateCommand();
                command.Connection = Conn;
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void executeQuery(string query)
        {
            if (Conn.State == ConnectionState.Closed)
                open(dbname);
            try
            {
                DbCommand command = fact.CreateCommand();
                command.Connection = Conn;
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                command.ExecuteNonQuery();

                reader = command.ExecuteReader();
            }
            catch (DbException e )
            {
                throw new Exception(e.Message);
            }
        }

        public DataSet executeQueryDS(string query)
        {
            DataSet ds = new DataSet();
            DbDataAdapter da = fact.CreateDataAdapter();
            DbCommand command = null;

            if (Conn.State == ConnectionState.Closed)
                open(dbname);
            else
            {
                try
                {
                    command = fact.CreateCommand();
                    if (command != null)
                    {
                        command.Connection = Conn;
                        command.CommandType = CommandType.Text;
                        command.CommandText = query;
                        da.SelectCommand = command;
                    }
                    da.Fill(ds);
                    
                }
                catch (DbException e)
                {
                    throw new Exception(e.Message);
                }
            }
            
            ds.WriteXml("test.xml");

            return ds;
        }

        public void addParam(string parameter, object value)
        {
            if (!parameter.Equals(String.Empty))
            {
                DbParameter param = fact.CreateParameter();
                param.ParameterName = parameter;
                param.Value = value;
                command.Parameters.Add(param);
            }
        }

        public void setCommandText(string text)
        {
            command.CommandText = text;
        }

        public void executeCommand()
        {
            if (command != null)
            {
                command.Connection = Conn;
                command.ExecuteNonQuery();
            }
        }

        public void beginTransaction()
        {
            transaction = Conn.BeginTransaction();
        }

        public void endTransaction()
        {
            transaction.Commit();
        }

        public DbDataReader Reader
        {
            get { return reader; }
        }

        public DbConnection Conn
        {
            get { return conn; }
        }

        public string getLastRecord(string column, string table)
        {
            return "SELECT "+column+" FROM "+table+" ORDER BY "+column+" DESC LIMIT 1";
        }
    }
}

