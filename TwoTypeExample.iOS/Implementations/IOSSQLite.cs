using System;
using System.IO;
using SQLite;
using TwoTypeExample.Helpers;
using TwoTypeExample.iOS.Implementations;
using Xamarin.Forms;
using static TwoTypeExample.GetDBConnection;

[assembly: Dependency(typeof(IOSSQLite))]
namespace TwoTypeExample.iOS.Implementations
{
    public class IOSSQLite : ISQLite
    {
        private string _filespec;
        public ConnectionInfo GetConnection()
        {
            ConnectionInfo connectionInfo = new ConnectionInfo();

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder  
            _filespec = Path.Combine(documentsPath, DatabaseHelper.DbFileName);

            // Create the connection  
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    SQLiteConnection conn = new SQLiteConnection(_filespec,
                        SQLiteOpenFlags.SharedCache |
                        SQLiteOpenFlags.ReadWrite |
                        SQLiteOpenFlags.Create |
                        SQLiteOpenFlags.FullMutex,
                        true, null);

                    connectionInfo.ConnConnection = conn;
                }
                catch (System.Exception ex)
                {
                    connectionInfo.ConnException = ex;
                    connectionInfo.ConnConnection = null;

                    System.Diagnostics.Debug.WriteLine("GetConnection() - " +
                        " Try: " + i +
                        ". Failed to get connection. Filespec: '" +
                        _filespec + "' Exception: " + ex.StackTrace);
                }

                if (connectionInfo.ConnConnection != null)
                {
                    connectionInfo.ConnException = null;
                    break;
                }
            }

            // Return the database connection  
            return connectionInfo;
        }

        public string GetDBFile()
        {
            return _filespec;
        }
    }
}
