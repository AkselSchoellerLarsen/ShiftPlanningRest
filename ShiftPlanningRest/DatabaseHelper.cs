using Microsoft.Extensions.Configuration;

namespace ShiftPlanningRest {
    public static class DatabaseHelper {
        public static string? ShiftPlanningDatabase { private get; set; }

        public static string ConnectionString {
            get {
                if (ShiftPlanningDatabase == null) {
                    throw new Exception("DatabaseHelper must have Configuration defined before ConnectionString can be retrived from it.");
                }
                return ShiftPlanningDatabase!;
            }
        }
    }
}
