using System;
using System.IO;
using SQLite;
using TwoTypeExample.Droid.Implementations;
using TwoTypeExample.Helpers;
using static TwoTypeExample.GetDBConnection;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidSQLite))]
namespace TwoTypeExample.Droid.Implementations
{
    public class AndroidSQLite : ISQLite
    {
        private string _filespec;

        /// <summary>
        /// Gets the connection.
        /// If failed, then try another 2 times, and if no luck, return a null
        /// </summary>
        /// <returns>The connection.</returns>
        public ConnectionInfo GetConnection()
        {
            ConnectionInfo connectionInfo = new ConnectionInfo();
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            // Documents folder  
            _filespec = Path.Combine(documentsPath, DatabaseHelper.DbFileName);

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
