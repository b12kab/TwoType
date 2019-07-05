using System;
using static TwoTypeExample.GetDBConnection;

namespace TwoTypeExample.Helpers
{
    public interface ISQLite
    {
        ConnectionInfo GetConnection();
        string GetDBFile();
    }
}
