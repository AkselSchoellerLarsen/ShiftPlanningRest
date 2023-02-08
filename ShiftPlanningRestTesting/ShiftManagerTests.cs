using Microsoft.VisualStudio.CodeCoverage;
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

            manager.GetShifts(new User("","",true));
        }

        [TestMethod]
        public void ShiftManagerTestsAddShiftPoitive() {
            DatabaseHelper.ShiftPlanningDatabase = testDatabaseConnectionString;

            IShiftManager manager = new ShiftManager();

            List<IShift> pre = manager.GetShifts(new User("", "", true));

            DateTime start = DateTime.Now;
            DateTime end = start.AddHours(1);
            IShift shift = new Shift(start, end);
            manager.AddShift(shift);

            List<IShift> post = manager.GetShifts(new User("", "", true));

            Assert.IsTrue(post.Contains(shift));
            Assert.IsTrue(pre.Count+1 == post.Count);
        }

        [TestMethod]
        public void ShiftManagerTestsUpdateShiftPoitive() {
            DatabaseHelper.ShiftPlanningDatabase = testDatabaseConnectionString;

            IShiftManager manager = new ShiftManager();

            List<IShift> pre = manager.GetShifts(new User("", "", true));
            IShift toUpdate = pre[0];

            DateTime start = DateTime.Now;
            DateTime end = start.AddHours(1);
            IShift updated = new Shift(toUpdate.Id, start, end);
            Assert.AreNotEqual(toUpdate, updated);
            manager.PutShift(updated);

            List<IShift> post = manager.GetShifts(new User("", "", true));

            Assert.IsFalse(post.Contains(toUpdate));
            Assert.IsTrue(post.Contains(updated));
        }

        [TestMethod]
        public void ShiftManagerTestsRemoveShiftPoitive() {
            DatabaseHelper.ShiftPlanningDatabase = testDatabaseConnectionString;

            IShiftManager manager = new ShiftManager();

            List<IShift> pre = manager.GetShifts(new User("", "", true));
            IShift toRemove = pre[0];

            DateTime start = DateTime.Now;
            DateTime end = start.AddHours(1);
            manager.RemoveShift(toRemove);

            List<IShift> post = manager.GetShifts(new User("", "", true));

            Assert.IsFalse(post.Contains(toRemove));
            Assert.IsTrue(pre.Count - 1 == post.Count);
        }
    }
}