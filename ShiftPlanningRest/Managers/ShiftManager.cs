using ShiftPlanningLibrary;
using System.Data.SqlClient;

namespace ShiftPlanningRest.Managers {
    public class ShiftManager : IShiftManager {
        private List<IShift> _shifts;

        public ShiftManager() {
            _shifts = SelectShifts().Result;
        }

        #region GetShifts
        public List<IShift> GetShifts(IUser user) {
            List<IShift> re = new List<IShift>();
            
            if(user.IsAdmin) {
                re.AddRange(_shifts);
            } else {
                foreach(var shift in _shifts) {
                    if(!shift.HasUser || shift.UserEmail == user.Email) {
                        re.Add(shift);
                    }
                }
            }

            return re;
        }

        private static string SQLSelectShifts = "SELECT * FROM dbo.iteration3_shift;";

        private async static Task<List<IShift>> SelectShifts() {
            try {
                using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLSelectShifts, connection)) {
                        await command.Connection.OpenAsync();
                        List<IShift> shifts = new List<IShift>();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (reader.Read()) {
                            int id = reader.GetInt32(0);
                            string email = reader.GetString(1);
                            DateTime start = reader.GetDateTime(2);
                            DateTime end = reader.GetDateTime(3);

                            IShift shift = new Shift(id, email, start, end);

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
            throw new Exception("ShiftManager failed to get shifts from database");
        }
        #endregion
        #region AddShift
        public bool AddShift(IShift shift) {
            _shifts.Add(shift);
            return InsertShift(shift).Result;
        }

        private static string SQLInsertShift = "INSERT INTO dbo.iteration3_shift\n" +
            "VALUES(@id, @email, @start, @end);";

        private static async Task<bool> InsertShift(IShift shift) {
            try {
                using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLInsertShift, connection)) {
                        command.Parameters.AddWithValue($"@id", shift.Id);
                        command.Parameters.AddWithValue($"@email", shift.UserEmail);
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
        #region PutShift
        public bool PutShift(IShift shift) {
            _shifts.RemoveAll((s) => { return s.Id == shift.Id; });
            _shifts.Add(shift);
            return UpdateShift(shift).Result;
        }

        private static string SQLUpdateShift = "UPDATE dbo.iteration3_shift\n" +
            "SET Email = @email, _Start = @start, _End = @end\n" +
            "WHERE Id = @id";

        private static async Task<bool> UpdateShift(IShift shift) {
            try {
                using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLUpdateShift, connection)) {
                        command.Parameters.AddWithValue($"@id", shift.Id);
                        command.Parameters.AddWithValue($"@email", shift.UserEmail);
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
        #region RemoveShift
        public bool RemoveShift(int id) {
            _shifts.RemoveAll((s) => { return s.Id == id; });
            return DeleteShift(id).Result;
        }

        public bool RemoveShift(IShift shift) {
            return RemoveShift(shift.Id);
        }

        private static string SQLDeleteShift = "DELETE FROM dbo.iteration3_shift\n" +
            "WHERE Id = @id";

        private static async Task<bool> DeleteShift(int id) {
            try {
                using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLDeleteShift, connection)) {
                        command.Parameters.AddWithValue($"@id", id);

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
