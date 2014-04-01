using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace foundation
{
    public interface IDatabase<T,V>
    {
        void open(string dbname);
        void close();
        void execute(string query);
        void executeQuery(string query);
        DataSet executeQueryDS(string query);
        void addParam(string parameter, object value);
        void setCommandText(string text);
        void executeCommand();
        void beginTransaction();
        void endTransaction();
        string getLastRecord(string column, string table);

        T Reader
        {
            get;
        }

        V Conn
        {
            get;
        }
    }
}
