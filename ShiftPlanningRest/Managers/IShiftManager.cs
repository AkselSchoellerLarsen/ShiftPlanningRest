using ShiftPlanningLibrary;

namespace ShiftPlanningRest.Managers {
    public interface IShiftManager {
        public List<IShift> GetShifts();
        public bool AddShift(IShift shift);
    }
}
