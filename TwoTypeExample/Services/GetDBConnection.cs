using System;
using SQLite;
using TwoTypeExample.Helpers;

namespace TwoTypeExample
{
    public class GetDBConnection
    {
        private SQLiteConnection _sqliteconnection;
        public SQLiteConnection Connection
        {
            get
            {
                return _sqliteconnection;
            }
        }

        public string Filespec { get; private set; }

        public Exception ConnException { get; private set; }

        public class ConnectionInfo
        {
            public SQLiteConnection ConnConnection;
            public Exception ConnException;
        }

        public GetDBConnection()
        {
            ConnectionInfo connectionInfo = Xamarin.Forms.DependencyService.Get<ISQLite>().GetConnection();
            _sqliteconnection = connectionInfo.ConnConnection;
            ConnException = connectionInfo.ConnException;
            Filespec = Xamarin.Forms.DependencyService.Get<ISQLite>().GetDBFile();
        }
    }
}
