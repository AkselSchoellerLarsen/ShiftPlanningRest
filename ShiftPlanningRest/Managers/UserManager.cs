using ShiftPlanningLibrary;
using System.Data.SqlClient;
using ShiftPlanningLibrary;
using ShiftPlanningRest;
using ShiftPlanningRest.Managers;

namespace ShiftPlanningRest.Managers {
    public class UserManager : IUserManager {
        private List<IUser> _users;

        public UserManager() {
            _users = SelectUsers().Result;
        }

        #region GetUsers
        public List<IUser> GetUsers() {
            return new List<IUser>(_users);
        }

        private static string SQLSelectUsers = "SELECT * FROM dbo.iteration3_user;";

        private async static Task<List<IUser>> SelectUsers() {
            try {
                using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLSelectUsers, connection)) {
                        await command.Connection.OpenAsync();
                        List<IUser> users = new List<IUser>();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (reader.Read()) {
                            string email = reader.GetString(0);
                            string password = reader.GetString(1);
                            bool isAdmin = reader.GetBoolean(2);

                            IUser user = new User(email, password, isAdmin);

                            users.Add(user);
                        }

                        return users;
                    }
                }
            }
            catch (Exception e) {
                string s = e.StackTrace;
                Console.WriteLine(s);
                Console.Beep();
            }
            throw new Exception("UserManager failed to get users from database");
        }
        #endregion
        #region RegisterUser
        public bool RegisterUser(IUser user) {
            if(user.IsAdmin == false) {
                for(int i=0; i<_users.Count; i++) {
                    if (_users[i].IsAdmin) {
                        break;
                    } 
                    if(i+1==_users.Count) {
                        user.IsAdmin = true;
                    }
                }
            }
            _users.Add(user);
            return InsertUser(user).Result;
        }

        private static string SQLInsertUser = "INSERT INTO dbo.iteration3_user\n" +
            "VALUES(@email, @password, @isAdmin);";

        private static async Task<bool> InsertUser(IUser user) {
            try {
                using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLInsertUser, connection)) {
                        command.Parameters.AddWithValue($"@email", user.Email);
                        command.Parameters.AddWithValue($"@password", user.Password);
                        command.Parameters.AddWithValue($"@isAdmin", user.IsAdmin);

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
                //Console.Beep();
            }
            return false;
        }
        #endregion
        #region VerifyUser
        public bool VerifyUser(IUser user) {
            return _users.Contains(user);
        }
        #endregion
        #region MakeUserAdmin
        public bool MakeUserAdmin(string email) {
            return UpdateUserType(email).Result;
        }

        private static string SQLUpdateShift = "UPDATE dbo.iteration3_user\n" +
            "SET IsAdmin = 'true'\n" +
            "WHERE Email = @email";

        private static async Task<bool> UpdateUserType(string email) {
            try {
                using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLUpdateShift, connection)) {
                        command.Parameters.AddWithValue($"@email", email);

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
