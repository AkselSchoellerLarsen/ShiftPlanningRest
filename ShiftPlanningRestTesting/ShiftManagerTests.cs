using ShiftPlanningLibrary;
using ShiftPlanningRest.Managers;

namespace ShiftPlanningRestTesting {
    [TestClass]
    public class ShiftManagerTests {
        [TestMethod]
        public void ShiftManagerTestsGetShiftsPositive() {
            IShiftManager manager = new ShiftManager();

            manager.GetShifts();
        }

        [TestMethod]
        public void ShiftManagerTestsAddShiftPoitive() {
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