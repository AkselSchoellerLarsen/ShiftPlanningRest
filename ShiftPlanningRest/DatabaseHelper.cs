using ShiftPlanningLibrary;
using System.Data.SqlClient;

namespace ShiftPlanningRest {
    public static class DatabaseHelper {

        public static string ConnectionString {
            get {
                return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ShiftPlanningDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            }
        }
    }
}
