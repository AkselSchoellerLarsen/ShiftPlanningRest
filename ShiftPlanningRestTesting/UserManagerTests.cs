using Microsoft.VisualStudio.CodeCoverage;
using ShiftPlanningLibrary;
using ShiftPlanningRest;
using ShiftPlanningRest.Managers;

namespace ShiftPlanningRestTesting {
    [TestClass]
    public class UserManagerTests {

        private static string testDatabaseConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ShiftPlanningDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        #region GetUsers
        [TestMethod]
        public void UserManagerTestsGetUsersPositive() {
            DatabaseHelper.ShiftPlanningDatabase = testDatabaseConnectionString;

            IUserManager manager = new UserManager();

            manager.GetUsers();
        }
        #endregion
        #region RegisterUser
        [TestMethod]
        public void UserManagerTestsRegisterUserPositive() {
            DatabaseHelper.ShiftPlanningDatabase = testDatabaseConnectionString;

            IUserManager manager = new UserManager();

            List<IUser> pre = manager.GetUsers();

            Random r = new Random();
            string email = $"{r.Next(100000, 1000000)}@{r.Next(1000, 10000)}.com";
            string password = $"not{r.Next(1000, 10000)}";
            IUser user = new User(email, password);
            Assert.IsTrue(manager.RegisterUser(user));

            List<IUser> post = manager.GetUsers();

            Assert.IsTrue(post.Contains(user));
            Assert.IsTrue(pre.Count + 1 == post.Count);
        }

        [TestMethod]
        public void UserManagerTestsRegisterUserNegative() {
            DatabaseHelper.ShiftPlanningDatabase = testDatabaseConnectionString;

            IUserManager manager = new UserManager();

            Random r = new Random();
            string email = $"{r.Next(100000, 1000000)}@{r.Next(1000, 10000)}.com";
            string password = $"not{r.Next(1000, 10000)}";
            IUser user = new User(email, password);
            Assert.IsTrue(manager.RegisterUser(user));
            Assert.IsFalse(manager.RegisterUser(user));
        }
        #endregion
        #region VerifyUser
        [TestMethod]
        public void UserManagerTestsVerifyUserPositive() {
            DatabaseHelper.ShiftPlanningDatabase = testDatabaseConnectionString;

            IUserManager manager = new UserManager();

            Random r = new Random();
            string email = $"{r.Next(100000, 1000000)}@{r.Next(1000, 10000)}.com";
            string password = $"not{r.Next(1000, 10000)}";
            IUser user = new User(email, password);

            Assert.IsFalse(manager.VerifyUser(user));
            Assert.IsTrue(manager.RegisterUser(user));
            Assert.IsTrue(manager.VerifyUser(user));
        }

        [TestMethod]
        public void UserManagerTestsVerifyUserNegativeNoSuchUser() {
            DatabaseHelper.ShiftPlanningDatabase = testDatabaseConnectionString;

            IUserManager manager = new UserManager();

            Random r = new Random();
            string email = $"{r.Next(100000, 1000000)}@{r.Next(1000, 10000)}.com";
            string password = $"not{r.Next(1000, 10000)}";
            IUser user = new User(email, password);

            Assert.IsFalse(manager.VerifyUser(user));
        }

        [TestMethod]
        public void UserManagerTestsVerifyUserNegativeWrongPassword() {
            DatabaseHelper.ShiftPlanningDatabase = testDatabaseConnectionString;

            IUserManager manager = new UserManager();

            Random r = new Random();
            string email = $"{r.Next(100000, 1000000)}@{r.Next(1000, 10000)}.com";
            string password = $"not{r.Next(1000, 10000)}";
            IUser user = new User(email, password);

            Assert.IsFalse(manager.VerifyUser(user));
            Assert.IsTrue(manager.RegisterUser(user));
            Assert.IsFalse(manager.VerifyUser(new User(email, "incorrect password")));
        }
        #endregion
        #region MakeUserAdmin
        [TestMethod]
        public void UserManagerTestsMakeUserAdminPositive() {
            DatabaseHelper.ShiftPlanningDatabase = testDatabaseConnectionString;

            IUserManager manager = new UserManager();

            Random r = new Random();
            string email = $"{r.Next(100000, 1000000)}@{r.Next(1000, 10000)}.com";
            string password = $"not{r.Next(1000, 10000)}";
            IUser user = new User(email, password, false);

            manager.RegisterUser(user);
            List<IUser> users = manager.GetUsers();
            IUser preUser = users.Find((u) => {
                if(u.Email == email) {
                    return true;
                }
                return false;
            }) ?? new User("", "", true);
            Assert.IsFalse(preUser.IsAdmin);

            manager.MakeUserAdmin(email);
            users = manager.GetUsers();
            IUser postUser = users.Find((u) => {
                if (u.Email == email) {
                    return true;
                }
                return false;
            }) ?? new User("", "", false);
            Assert.IsTrue(postUser.IsAdmin);
        }
        #endregion
        #region RemoveUser
        [TestMethod]
        public void UserManagerTestsRemoveUserPoitive() {
            DatabaseHelper.ShiftPlanningDatabase = testDatabaseConnectionString;

            IUserManager manager = new UserManager();

            Random r = new Random();
            string email = $"{r.Next(100000, 1000000)}@{r.Next(1000, 10000)}.com";
            string password = $"not{r.Next(1000, 10000)}";
            IUser user = new User(email, password);
            manager.RegisterUser(user);

            Assert.IsTrue(manager.VerifyUser(user));
            manager.RemoveUser(user);
            Assert.IsFalse(manager.VerifyUser(user));
        }
        #endregion
    }
}