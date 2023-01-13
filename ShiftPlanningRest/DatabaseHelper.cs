using Microsoft.Extensions.Configuration;

namespace ShiftPlanningRest {
    public static class DatabaseHelper {
        public static IConfiguration? Configuration { private get; set; }

        public static string ConnectionString {
            get {
                if(Configuration == null) {
                    return "";
                }
                return Configuration.GetConnectionString("ShiftPlanningDatabase")!;
            }
        }
    }
}
