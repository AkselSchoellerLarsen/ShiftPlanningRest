using ShiftPlanningLibrary;
using ShiftPlanningRest;
using ShiftPlanningRest.Managers;

namespace ShiftPlanningRestTesting {
    [TestClass]
    public class ShiftManagerTests {

        private static string testDatabaseConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ShiftPlanningDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [TestMethod]
        public void ShiftManagerTestsGetShiftsPositive() {
            DatabaseHelper.ShiftPlanningDatabase = testDatabaseConnectionString;

            IShiftManager manager = new ShiftManager();

            manager.GetShifts();
        }

        [TestMethod]
        public void ShiftManagerTestsAddShiftPoitive() {
            DatabaseHelper.ShiftPlanningDatabase = testDatabaseConnectionString;

            IShiftManager manager = new ShiftManager();

            List<IShift> pre = manager.GetShifts();

            DateTime start = DateTime.Now;
            DateTime end = start.AddHours(1);
            IShift shift = new Shift(start, end);
            manager.AddShift(shift);

            List<IShift> post = manager.GetShifts();

            Assert.IsTrue(post.Contains(shift));
            Assert.IsTrue(pre.Count+1 == post.Count);
        }

    }
}