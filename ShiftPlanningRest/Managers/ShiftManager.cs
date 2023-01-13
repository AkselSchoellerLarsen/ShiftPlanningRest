using ShiftPlanningLibrary;
using System.Data.SqlClient;

namespace ShiftPlanningRest.Managers {
    public class ShiftManager : IShiftManager {
        private List<IShift> _shifts;

        public ShiftManager() {
            _shifts = SelectShifts().Result;
        }

        #region GetShifts
        public List<IShift> GetShifts() {
            return _shifts;
        }

        private static string SQLSelectShifts = "SELECT * FROM dbo.iteration1_shift";

        private async static Task<List<IShift>> SelectShifts() {
            try {
                using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLSelectShifts, connection)) {
                        await command.Connection.OpenAsync();
                        List<IShift> shifts = new List<IShift>();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (reader.Read()) {
                            int id = reader.GetInt32(0);
                            DateTime start = reader.GetDateTime(1);
                            DateTime end = reader.GetDateTime(2);

                            IShift shift = new Shift(id, start, end);

                            shifts.Add(shift);
                        }

                        return shifts;
                    }
                }
            }
            catch (Exception e) {
                string s = e.StackTrace;
                Console.WriteLine(s);
                Console.Beep();
            }
            return new List<IShift>();
        }
        #endregion
        #region AddShift
        public bool AddShift(IShift shift) {
            _shifts.Add(shift);
            return InsertShift(shift).Result;
        }

        private static string SQLInsertShift = "Insert INTO dbo.iteration1_shift" +
            "VALUES(@id, @start, @end)";

        private static async Task<bool> InsertShift(IShift shift) {
            try {
                using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLInsertShift, connection)) {
                        command.Parameters.AddWithValue($"@id", shift.Id);
                        command.Parameters.AddWithValue($"@start", shift.Start);
                        command.Parameters.AddWithValue($"@end", shift.End);

                        await command.Connection.OpenAsync();

                        int i = await command.ExecuteNonQueryAsync();
                        if (i != 1) {
                            return false;
                        } else {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e) {
                string s = e.StackTrace;
                Console.WriteLine(s);
                Console.Beep();
            }
            return false;
        }
        #endregion

    }
}
